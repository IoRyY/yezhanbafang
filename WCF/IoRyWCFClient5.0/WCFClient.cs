using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
using yezhanbafang.fw.Core;

namespace yezhanbafang.fw.WCF.Client
{
    /// <summary>
    /// 异步得到数据的委托
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="obj"></param>
    public delegate void AsnyGetDataSet(DataSet DS, object obj);

    /// <summary>
    /// 异步消息委托
    /// </summary>
    /// <param name="content"></param>
    /// <param name="Exstr"></param>
    public delegate void GetMessage(string content, string Exstr);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mytype"></param>
    /// <param name="mxml"></param>
    /// <param name="yd"></param>
    public delegate void QQSend(string mytype, string mxml, ydhDeliver yd);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="mcd"></param>
    public delegate void GetDeliver(ModelClassDeliver mcd);

    /// <summary>
    /// 主类
    /// 20200401 对整体对外接口进行了大的整理.
    /// </summary>
    [CallbackBehavior(IncludeExceptionDetailInFaults = true)]
    public class WCFClientV5 : ImyCallBack, IDisposable
    {
        #region 属性们

        /// <summary>
        /// 加密串的公钥
        /// </summary>
        string Encrypt = null;

        /// <summary>
        /// 执行WCF的人
        /// </summary>
        string cOperator = null;

        /// <summary>
        /// 远程wcf中调用的ioryclass的配置文件路径
        /// </summary>
        string _xml;

        string Xml
        {
            get { return _xml; }
            set { _xml = value; }
        }

        List<ydhDeliver> myList = new List<ydhDeliver>();

        /// <summary>
        /// 调用myAsnyGetDataSet的事件
        /// </summary>
        public event AsnyGetDataSet myAsnyGetDataSet;

        /// <summary>
        /// 调用ServerSendMessage的事件
        /// </summary>
        public event GetMessage DuxMessage;

        /// <summary>
        /// 
        /// </summary>
        public event QQSend QQSMsg;

        /// <summary>
        /// 
        /// </summary>
        public event GetDeliver eGetDeliver;

        DataGridView _DGV = null;

        /// <summary>
        /// 当调用myGetDataSetAsync方法,数据传输完成后,自动绑定datagridviw
        /// </summary>
        public DataGridView myDataGridView
        {
            get { return _DGV; }
            set { _DGV = value; }
        }

        ProgressBar _myProgressBar;

        /// <summary>
        /// 当调用myGetDataSetAsync方法,进度条自动读条
        /// </summary>
        public ProgressBar myProgressBar
        {
            get { return _myProgressBar; }
            set
            {
                _myProgressBar = value;
                if (_myProgressBar.Visible == true)
                {
                    _myProgressBar.Visible = false;
                }
            }
        }

        List<Button> _myButtons;

        /// <summary>
        /// 当调用异步方法时,数据传输没有结束的时候.
        /// 此属性里所有的button会变成disable,因为此时再调用,会造成进度条错误.
        /// </summary>
        public List<Button> MyButtons
        {
            get
            {
                if (_myButtons != null)
                {
                    return _myButtons;
                }
                else
                {
                    _myButtons = new List<Button>();
                    return _myButtons;
                }
            }
            set { _myButtons = value; }
        }

        ImyService isc;

        /// <summary>
        /// 2015-9-15增加判断客户端状态的方法,看看能否解决"System.ServiceModel.Channels.ServiceChannel 无法用于通信"的错误.
        /// 2016-1-22 在WCF服务端加了代码,貌似解决了此错误.
        /// </summary>
        ImyService MyISC
        {
            get
            {
                try
                {
                    if (isc == null)
                    {
                        isc = createClient();
                        //初始化
                        string xl = myxml("", "", new List<string> { "" });
                        string rxl = isc.SynMessage("GetEncrypt", xl, this.cOperator, "ydh");
                        XElement xmdata = XElement.Parse(rxl);
                        this.Encrypt = xmdata.Element("return").FirstNode.ToString();
                    }
                }
                catch (Exception me)
                {
                    throw me;
                }
                return isc;
            }
        }

        /// <summary>
        /// 生成透明代理类
        /// </summary>
        /// <returns></returns>
        ImyService createClient()
        {
            //设置nettcp
            NetTcpBinding ws = new NetTcpBinding();
            ws.MaxReceivedMessageSize = 2147483647;
            ws.MaxBufferPoolSize = 2147483647;
            ws.MaxReceivedMessageSize = 2147483647;
            ws.ReaderQuotas.MaxStringContentLength = 2147483647;
            ws.Security.Mode = SecurityMode.None;
            //通过回调函数,bingding,和address 就不用config了
            DuplexChannelFactory<ImyService> mcf =
    new DuplexChannelFactory<ImyService>(this, ws, address);

            return mcf.CreateChannel();
        }

        EndpointAddress address = null;

        bool IsLongKeep = false;

        System.ComponentModel.BackgroundWorker HBbw;

        #endregion

        #region 初始化函数

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="_xml">WCF调用的IoRyClass的配置文件路径</param>
        /// <param name="callOperator">执行者</param>
        /// <param name="url">WCF对外公布的执行路径 net.tcp://</param>
        public WCFClientV5(string _xml, string callOperator, string url)
        {
            try
            {
                Xml = _xml;
                this.cOperator = callOperator;
                address = new EndpointAddress(url);
                //走一遍初始化,必须走,和秘钥有关
                MyISC.GetType();
            }
            catch (Exception me)
            {
                throw me;
            }
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="_xml">WCF调用的IoRyClass的配置文件路径</param>
        /// <param name="callOperator">执行者</param>
        /// <param name="url">WCF对外公布的执行路径 net.tcp://</param>
        /// <param name="IsKeep">此客户端是否和服务端长时间保持连接</param>
        public WCFClientV5(string _xml, string callOperator, string url, bool IsKeep)
        {
            try
            {
                Xml = _xml;
                this.cOperator = callOperator;
                address = new EndpointAddress(url);
                IsLongKeep = IsKeep;
                //走一遍初始化,必须走,和秘钥有关
                MyISC.GetType();
                //目前默认9分钟心跳一次
                if (IsKeep)
                {
                    HBbw = new System.ComponentModel.BackgroundWorker();
                    HBbw.DoWork += new System.ComponentModel.DoWorkEventHandler(KeepHearBeat);
                    HBbw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(HBbw_RunWorkerCompleted);
                    HBbw.RunWorkerAsync();
                }
            }
            catch (Exception me)
            {
                throw me;
            }
        }

        //持续的访问
        void HBbw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            HBbw.RunWorkerAsync();
        }

        /// <summary>
        /// 心跳 默认每9分钟一次,一旦发现有情况,就创建新的连接.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void KeepHearBeat(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.Sleep(60000 * 9);
            try
            {
                MyISC.SynMessage("", "", "", "KeepHearBeat");
            }
            catch 
            {
                isc = createClient();
                //初始化
                string xl = myxml("", "", new List<string> { "" });
                string rxl = isc.SynMessage("GetEncrypt", xl, this.cOperator, "ydh");
                XElement xmdata = XElement.Parse(rxl);
                this.Encrypt = xmdata.Element("return").FirstNode.ToString();
            }
        }

        /// <summary>
        /// 拼接WCF所用到的xml
        /// </summary>
        /// <param name="xmlpath">IoRy类的配置文件路径</param>
        /// <param name="functionname">要执行的函数名称</param>
        /// <param name="myparams">执行的函数的参数们</param>
        /// <returns></returns>
        string myxml(string xmlpath, string functionname, List<string> myparams)
        {
            string myxml =
"<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <xmlPath>{0}</xmlPath>
  <Function>{1}</Function>
{2}
</root>";
            return string.Format(myxml, xmlpath, functionname,
                string.Join("", myparams.Select(x => "<Param>" + x + "</Param>")));
        }

        #endregion

        #region QQ相关方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="xml"></param>
        public void QQfunction(string fname, string xml)
        {
            isc.ClientSendMessage(fname, xml, this.cOperator, "ydh");
        }

        #endregion

        #region Callback 成员

        /// <summary>
        /// Callback 成员
        /// </summary>
        /// <param name="dler"></param>
        /// <returns></returns>
        public bool ServerSendData(ydhDeliver dler)
        {
            //检测上传是否重复.
            if (myList.Any(x => x.Name == dler.Name && x.Index == dler.Index))
            {
                foreach (var item in myList.Where(x => x.Name == dler.Name && x.Index == dler.Index))
                {
                    myList.Remove(item);
                }
            }
            //保存
            myList.Add(dler);
            if (myProgressBar != null)
            {
                if (myProgressBar.Value < myProgressBar.Maximum)
                {
                    myProgressBar.Value++;
                }
            }
            //最终拼接.
            if (dler.IsFinish)
            {
                //这里再2015-11-13发现了一个边界bug,当传输的数据正好到达边界后,服务端还会再传输一个byte[0]的dler过来,需要处理掉
                if (dler.Context.Count() == 0)
                {
                    return true;
                }

                var re = myList.Where(x => x.Name == dler.Name).OrderBy(x => x.Index);
                List<byte> mybt = new List<byte>();
                foreach (var item in re)
                {
                    mybt.AddRange(item.Context.ToList());
                }
                switch (dler.FunctionName)
                {
                    case "DataSet":
                        DataSet DS = IoRyClass.RetrieveXmlDataSet(mybt.ToArray());
                        if (myDataGridView != null)
                        {
                            myDataGridView.DataSource = DS.Tables[0];
                        }
                        if (myAsnyGetDataSet != null)
                        {
                            this.myAsnyGetDataSet(DS, obj);
                        }
                        break;
                    case "GetFolderXml":
                        string xml = IoRyClass.BytesToString(mybt.ToArray());
                        if (DuxMessage != null)
                        {
                            this.DuxMessage(xml, "GetFolderXml");
                        }
                        break;
                    case "CheckUpdateFiles":
                        File.WriteAllBytes(System.AppDomain.CurrentDomain.BaseDirectory + dler.Name, mybt.ToArray());
                        break;
                }
                if (myProgressBar != null)
                {
                    myProgressBar.Visible = false;
                    myProgressBar.Value = 0;
                }
                if (MyButtons.Count != 0)
                {
                    foreach (Button item in MyButtons)
                    {
                        item.Enabled = true;
                    }
                }
                myList.Clear();
            }

            if (eGetDeliver != null)
            {
                this.eGetDeliver(ConvertDeliver(dler));
            }

            return true;
        }

        public ModelClassDeliver ConvertDeliver(ydhDeliver yd)
        {
            ModelClassDeliver md = new ModelClassDeliver();
            md.DataType = yd.DataType;
            md.Exstr = yd.Exstr;
            md.FunctionName = yd.FunctionName;
            md.Index = yd.Index;
            md.IsFinish = yd.IsFinish;
            md.Max = yd.Max;
            md.Name = yd.Name;
            md.Now = yd.Now;
            return md;
        }

        /// <summary>
        /// Callback 成员
        /// </summary>
        /// <param name="mesg"></param>
        public void ServerSendMessage(string mesg)
        {
            XElement xmdata = XElement.Parse(mesg);
            bool IsQQ = false;
            if (xmdata.Elements("QQ").Count() > 0)
            {
                IsQQ = true;
            }
            if (IsQQ)
            {
                if (QQSMsg != null)
                {
                    QQSMsg("", mesg, null);
                }
            }
            else
            {
                if (xmdata.Element("correct").Value == "false")
                {
                    throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
                }
                string fname = xmdata.Element("Function").Value;
                string msg = xmdata.Element("return").FirstNode.ToString();

                string asyncname = "";
                var myasync = xmdata.Element("Async");
                if (myasync != null)
                {
                    asyncname = xmdata.Element("Async").Value;
                }
                switch (fname)
                {
                    case "DataSet":
                        switch (asyncname)
                        {
                            case "FileTransportCount":
                                if (myProgressBar != null)
                                {
                                    myProgressBar.Maximum = Convert.ToInt32(msg);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case "CheckUpdateFiles":
                        switch (asyncname)
                        {
                            case "TotleFilesNumber":
                                this.DuxMessage(msg, "TotleFilesNumber");
                                break;
                            case "AllFinish":
                                this.DuxMessage(msg, "AllFinish");
                                break;
                            default:
                                break;
                        }
                        break;
                    case "GetEncrypt":
                        this.Encrypt = msg;
                        break;
                    case "ServiceSend":
                        if (this.DuxMessage != null)
                        {
                            this.DuxMessage(msg, "ServiceSend");
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        #endregion

        #region 和WCF交互的方法

        /// <summary>
        /// 为了在myGetDataSetAsync方法中传递一些参数
        /// </summary>
        object obj;

        /// <summary>
        /// 调整进度条以及按钮的状态
        /// </summary>
        void checkProgressBarAndButtons()
        {
            if (myProgressBar != null)
            {
                if (myProgressBar.Visible == false)
                {
                    myProgressBar.Visible = true;
                }
            }
            if (MyButtons.Count != 0)
            {
                foreach (Button item in MyButtons)
                {
                    item.Enabled = false;
                }
            }
        }


        /// <summary>
        /// 异步的得到DataSet的方法,可以和按钮与进度条相关联用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="myobj">可以传送到事件里面的东西</param>
        public void GetDataSet_Async(string sql, object myobj)
        {
            obj = myobj;
            this.checkProgressBarAndButtons();
            string EncryptString = IoRyClass.EncryptRSA_long(sql, Encrypt);
            string xl = myxml(Xml, "GetDataSet", new List<string> { EncryptString });
            MyISC.ClientSendMessage("InvokeMember", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 异步的得到DataSet的方法,可以和按钮与进度条相关联用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <param name="myobj">可以传送到事件里面的东西</param>
        public void GetDataSet_Async(string sql, string cuser, object myobj)
        {
            obj = myobj;
            this.checkProgressBarAndButtons();
            string EncryptString = IoRyClass.EncryptRSA_long(sql, Encrypt);
            cuser = IoRyClass.EncryptRSA_long(cuser, Encrypt);
            string xl = myxml(Xml, "Log_GetDataSet", new List<string> { EncryptString, cuser });
            MyISC.ClientSendMessage("InvokeMember", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        public void ExcutSqlTran_Async(string sql)
        {
            this.checkProgressBarAndButtons();
            string EncryptString = IoRyClass.EncryptRSA_long(sql, Encrypt);
            string xl = myxml(Xml, "ExecuteSqlTran", new List<string> { EncryptString });
            MyISC.ClientSendMessage("InvokeMember", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        public void ExcutSqlTran_Async(string sql, string cuser)
        {
            this.checkProgressBarAndButtons();
            string EncryptString = IoRyClass.EncryptRSA_long(sql, Encrypt);
            cuser = IoRyClass.EncryptRSA_long(cuser, Encrypt);
            string xl = myxml(Xml, "Log_ExecuteSqlTran", new List<string> { sql, cuser });
            MyISC.ClientSendMessage("InvokeMember", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        public void ExcutSP_Async(string SPname, List<DbParameter> DbParameterS, object myobj)
        {
            obj = myobj;
            this.checkProgressBarAndButtons();
            List<string> rc = new List<string>();
            string jmapname = "<SPname>" + IoRyClass.EncryptRSA_long(SPname, Encrypt) + "</SPname>";
            rc.Add(jmapname);
            foreach (var item in DbParameterS)
            {
                string cs = "<ParameterName>" + item.ParameterName + "</ParameterName><Value>" + item.Value.ToString() + "</Value>";
                rc.Add(cs);
            }
            string xl = myxml(Xml, "ExecuteSP", rc);
            MyISC.ClientSendMessage("ExcutSP", xl, this.cOperator, "ydh");
        }


        /// <summary>
        /// 同步的取得DataSet的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet_Syn(string sql)
        {
            sql = IoRyClass.EncryptRSA_long(sql, Encrypt);
            string xl = myxml(Xml, "GetDataSet", new List<string> { sql });
            string rst = MyISC.SynMessage("InvokeMember", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            byte[] mbs = IoRyClass.StringToBytes(xmdata.Element("return").FirstNode.ToString());
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 同步的取得DataSet的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <returns></returns>
        public DataSet GetDataSet_Syn(string sql, string cuser)
        {
            sql = IoRyClass.EncryptRSA_long(sql, Encrypt);
            cuser = IoRyClass.EncryptRSA_long(cuser, Encrypt);
            string xl = myxml(Xml, "Log_GetDataSet", new List<string> { sql, cuser });
            string rst = MyISC.SynMessage("InvokeMember", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            byte[] mbs = IoRyClass.StringToBytes(xmdata.Element("return").FirstNode.ToString());
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 同步的执行事务的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExcutSqlTran_Syn(string sql)
        {
            sql = IoRyClass.EncryptRSA_long(sql, Encrypt);
            string xl = myxml(Xml, "ExecuteSqlTran", new List<string> { sql });
            string rst = MyISC.SynMessage("InvokeMember", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            return xmdata.Element("return").FirstNode.ToString();
        }

        /// <summary>
        /// 同步的执行事务的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <returns></returns>
        public string ExcutSqlTran_Syn(string sql, string cuser)
        {
            sql = IoRyClass.EncryptRSA_long(sql, Encrypt);
            cuser = IoRyClass.EncryptRSA_long(cuser, Encrypt);
            string xl = myxml(Xml, "Log_ExecuteSqlTran", new List<string> { sql, cuser });
            string rst = MyISC.SynMessage("InvokeMember", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            return xmdata.Element("return").FirstNode.ToString();
        }

        /// <summary>
        /// 同步的执行Sql的方法,Oracle不支持事务
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExcutSql_Syn(string sql)
        {
            sql = IoRyClass.EncryptRSA_long(sql, Encrypt);
            string xl = myxml(Xml, "ExecuteSql", new List<string> { sql });
            string rst = MyISC.SynMessage("InvokeMember", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            return xmdata.Element("return").FirstNode.ToString();
        }

        /// <summary>
        /// 执行存储过程,注意,调用服务的就没有out形式的入参了,这个失效
        /// </summary>
        /// <param name="SPname">存储过程名</param>
        /// <param name="DbParameterS">参数</param>
        /// <returns></returns>
        public DataSet ExcutSP_Syn(string SPname, List<DbParameter> DbParameterS)
        {
            List<string> rc = new List<string>();
            string jmapname = "<SPname>" + IoRyClass.EncryptRSA_long(SPname, Encrypt) + "</SPname>";
            rc.Add(jmapname);
            foreach (var item in DbParameterS)
            {
                string cs = "<ParameterName>" + item.ParameterName + "</ParameterName><Value>" + item.Value.ToString() + "</Value>";
                rc.Add(cs);
            }
            string xl = myxml(Xml, "ExecuteSP", rc);
            string rst = MyISC.SynMessage("ExcutSP", xl, this.cOperator, "ydh");
            XElement xmdata = XElement.Parse(rst);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            byte[] mbs = IoRyClass.StringToBytes(xmdata.Element("return").FirstNode.ToString());
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        #endregion

        #region 自动更新相关

        /// <summary>
        /// 每次传输的最大量
        /// </summary>
        int? Maxupload = null;

        /// <summary>
        /// 目前来看这个函数没必要做成异步
        /// </summary>
        /// <returns></returns>
        string get_EachSendBytesMaxNum()
        {
            string xl = myxml("", "EachSendBytesMaxNum", new List<string> { "" });
            return MyISC.SynMessage("EachSendBytesMaxNum", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 获取文件夹的xml结构
        /// </summary>
        /// <param name="re_path"></param>
        public void GetFoldersPathXML(string re_path)
        {
            if (this.Maxupload == null)
            {
                this.Maxupload = Convert.ToInt32(get_EachSendBytesMaxNum());
            }
            string xl = myxml(re_path, "GetFolderXml", new List<string> { "" });
            //string rxl = isc.SynMessage("EachSendBytesMaxNum", xl, this.cOperator, "ydh");
            MyISC.ClientSendMessage("GetFolderXml", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 获取当前文件夹下的文件是否都是最新
        /// </summary>
        /// <param name="path"></param>
        public void CheckUpdateFiles(string path)
        {
            string jieguo = GetFolderFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + path));
            string xl = myxml(path, "CheckUpdateFiles", new List<string> { jieguo });
            MyISC.ClientSendMessage("CheckUpdateFiles", xl, this.cOperator, "ydh");
        }

        /// <summary>
        /// 获取单一文件夹下所有的文件及其属性
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetFolderFiles(string path)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            if (!Directory.Exists(path))
            {
                TheFolder.Create();
            }
            XElement fxs = new XElement("files");
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                XElement fx = new XElement("file");
                fx.Add(new XAttribute("Extension", NextFile.Extension));
                fx.Add(new XAttribute("Name", NextFile.Name));
                if (NextFile.Extension.ToLower() == ".exe" || NextFile.Extension.ToLower() == ".dll")
                {
                    FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(NextFile.FullName);
                    //也有.net认不出版本号的dll
                    if (myFileVersion.ProductVersion != null)
                    {
                        fx.Add(new XAttribute("ProductVersion", myFileVersion.ProductVersion));
                    }
                    else
                    {
                        fx.Add(new XAttribute("MD5", IoRyClass.MD5(File.ReadAllBytes(NextFile.FullName))));
                    }
                }
                else
                {
                    fx.Add(new XAttribute("MD5", IoRyClass.MD5(File.ReadAllBytes(NextFile.FullName))));
                }
                fxs.Add(fx);
            }
            return fxs.ToString();
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            try
            {
                if (disposed)
                {
                    return;
                }
                if (disposing)
                {
                    isc = null;
                    myList.Clear();
                    myList = null;
                }
                disposed = true;
            }
            catch
            {
            }
        }

        #endregion
    }
}
