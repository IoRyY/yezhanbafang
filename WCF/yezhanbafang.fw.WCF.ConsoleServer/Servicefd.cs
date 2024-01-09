using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System;
using System.ServiceModel.Channels;
using System.Xml;
using System.Diagnostics;
using System.Threading;
using log4net;
using System.Reflection;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using yezhanbafang.fw.Core;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//2013-6-7 把以前兼容代码,老得qq代码全部去掉
//2016-1-20 针对2016BigProject的服务,将之前反射调用IoRyClass的方法简化,太复杂没必要.
//2016-6-21 增加了新服务的升级部分,老的升级部分代码删除
//2016-6-23 增加qq部分代码,打算把这块让他们用起来.
namespace yezhanbafang.fw.WCF.Server
{
    public enum myTransport
    {
        ClientToService,
        ServiceToClient
    }
    /*
     * 6.4.0.0 增加了qq聊天部分的代码
     * 6.3.0.0 增加了自动升级部分,简化了升级流程,只用了UpdateFilesV3一个方法,其他老方法,删除
     * 6.2.0.0版本去除了所有服务器端关于心跳的东西,因为发现了WCF服务配置中receiveTimeout="00:00:10"就是控制PreSession的形势下每个Session的持续时间,
     * 到时间了就调用析构函数,如果在时间内调用了session中的函数,此时间就会刷新,不用我自己再多此一举写心跳,只需要在客户端需要保持的session中每隔一段时间,
     * 心跳一次即可.
     * 2015-1-8升级为5.0.0.0,除了异步的方法外,加入了同步方法,现在可以两种方法同时使用.
     * 
     * 由于想用MiniQQ,所以必须保证服务端不停止,有优化,最终还是决定用心跳包来控制session的释放,因为目前session不释放,造成内存越用越大
     * 心跳在写log里面实现就行,目前暂定10分钟没心跳就死.
     * 加上Disposable接口之后,发现只要客户端一断,Service会自动调用dispose,及时的释放,原先我没从这里继承,不应该啊.
     * 
     * 文件流动
File from Client to Server
SendClient:[FileName] Send --> Service:[FileName] Send to SendClient 返回唯一的文件名称,Rtf也临时保存
SendClient:Creat Thread,Send bytes:
     * 2016-6-21 现在看来,根本没必要传送[EachSendBytesMaxNum],因为只要拼接起来就好
SendClient:[EachSendBytesMaxNum] Send  --> Service:[EachSendBytesMaxNum] Send to SendClient 返回每次传送的Bytes
SendClient:Send Bytes --> when Finish --> Service:[UploadFinish] Send to SendClient
SendClient:[SendTo] Send  -->  Service:[SendTo] Send to ReciveClient

File From Server to Client
ReciveClient:[IsReceive] Send  -->  Service:[IsReceive] Send to SendClient
Service:Creat Thread,Send Bytes:
Service:[FileTransportCount] Send to ReciveClient
Service:Send Bytes to ReciveClient -->  When Finish ReciveClient:[IsDownLoadFinish] Send
Service:[IsDownLoadFinish] Send to SendClient
     * 
     *  传输类型汇总 2016-6-21 现在传送消息都用xml,确实规范多了
     *  //从这里可以看到所有的message种类
        //ClientSendMessage
        //[FileName]验证新上传的文件名称是否合法.分号后面需要验证名称,分号后面GUID
        //[EachSendBytesMaxNum] 每次传输的最大比特数,分号后面为每次传输的最大比特数
        //[IsLast] 是否最新版本 已注释
     *  //[IsLastV2] 是否最新版本(v2版本,不用配置文件 已注释
     *  //[IsLastV3] 是否最新版本(v3版本,可以对应多个不同的客户端程序)
     *  //[UpdateFiles] v2版本的升级命令 已注释
     *  //[UpdateFilesV3] v3升级命令
     *  //[HeartBeat] 用户心跳包.

        //[AdminMessage] 管理员消息.
        //[ServiceSend] 服务器推送消息. (特殊没在这里)

        //[FileTransportCount] 文件的传输次数,用来显示进度条.
        //[SendTo] 给某人发送附件,分号后面为人名,分号后面为文件名,分号后面为文件类型
        //[IsReceive] 是否接受附件,分好后为结果
        //[UploadFinish] 客户端上传文件完成.
        //[DownLoadFinish] 客户端下载文件完成
        //[IsDownLoadFinish] 客户端下载文件完成是否成功,分号后面为结果,分号后面为发送者
     * 
     *  //处理IoRyClass
     *  //[NewIoRyClass] 初始化参数

        //ServerSendMessage 散落在各处.
        //[Begin] 开始接受具体文件,分号后面代表此文件的byte count
        //[Finish] 具体文件接受完毕.无信息.
        //[AllFinish] 所有文件更新成功.
        //[Wrong] 出现错误,分号后面为错误信息

        //[FileName]返回合法文件名称,防止上传文件名称重复.分号后面验证后名称,分号后面GUID.
        //[TotleFilesNumber] 需要更新的总文件数,分号后面为需要更新的总文件数
        //[EachSendBytesMaxNum] 每次传输的最大比特数,分号后面为每次传输的最大比特数
        //[IsLast] 是否最新版本 已注释
     *  //[IsLastV2] 是否最新版本(v2版本,不用配置文件) 已注释
     *  //[UpdateFiles] v2版本的升级命令  已注释
     *  //[IsLastV3] 是否最新版本(v3版本,对应多个客户端)
     *  //[UpdateFilesV3] v3版本的升级命令

        //[AdminMessage] 管理员消息.
        //[ServiceSend] 服务器推送消息.
        
        //[FileTransportCount] 文件的传输次数,用来显示进度条.分号后为总Count,分号后为发送者.分号后为文件名称
        //[IsReceive] 是否接受附件,分好后为结果
        //[SendTo] 给某人发送附件,分号后面为人名,分号后面为文件名,分号后面为文件类型
        //[UploadFinish] 客户端上传文件完成.
        //[DownLoadFinish] 客户端下载文件完成
        //[IsDownLoadFinish] 客户端下载文件完成是否成功,分号后面为结果,分号后面为发送者,分号后为文件名
     */

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Servicefd : ImyService, IDisposable
    {

        #region 定义

        /// <summary>
        /// 非对称加密的公钥,秘钥
        /// </summary>
        RSA rsa = RSA.Create();

        //创建日志记录组件实例
        static ILog _ydhlog = null;

        //当宿主为WPF时,不能用配置文件,否则报错,我靠~!
        public static ILog Ydhlog
        {
            get
            {
                if (_ydhlog == null)
                {
                    log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();
                    appender.File = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                    appender.AppendToFile = true;
                    appender.StaticLogFileName = false;
                    appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
                    appender.DatePattern = "yyyyMMdd.TXT";
                    appender.Layout = new log4net.Layout.PatternLayout("时间:%date 线程:[%thread] 错误级别：%-5level 异常类：%logger 异常属性:[%property{NDC}] 异常信息：%message%newline");
                    log4net.Filter.LevelRangeFilter filter = new log4net.Filter.LevelRangeFilter();
                    appender.AddFilter(filter);//加入到 appender中
                    appender.ActivateOptions();// 这个要调用一下呢  
                    log4net.Config.BasicConfigurator.Configure(appender);
                    _ydhlog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                }
                return Servicefd._ydhlog;
            }
        }

        ImyCallBack mCB = OperationContext.Current.GetCallbackChannel<ImyCallBack>();

        public string SessionName = null;

        private string _ClientName = null;

        public string ClientName
        {
            get { return _ClientName; }
            set
            {
                if (ServiceSession[this.SessionName] == null)
                {
                    ServiceSession[this.SessionName] = value;
                    UserInfo uf = new UserInfo();
                    uf.SessionName = this.SessionName;
                    uf.uCommanFunction = CommanFunction;
                    CommanList.Add(this.SessionName, uf);

                    Console.WriteLine(string.Format(@"
Time{0};
Client:{1} into Session:{2};
$
", DateTime.Now.ToString(), value, this.SessionName));
                }

                _ClientName = value;
            }
        }

        //通用函数
        public delegate void CommanDelegate(string command, string Message);
        public static SortedList<string, UserInfo> CommanList = new SortedList<string, UserInfo>();
        public static SortedList<string, string> ServiceSession = new SortedList<string, string>();

        #endregion

        #region 析构,销毁,构造函数

        public Servicefd()
        {
            try
            {

                SessionName = DateTime.Now.ToShortTimeString() + ":" + Guid.NewGuid().ToString();
                Serviceinfo(SessionName, "Create");
                ServiceSession.Add(this.SessionName, this.ClientName);
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Serviceinfo(SessionName, "Servicefd() function :Exception:" + exmsg);
                Ydhlog.Error(SessionName + " Servicefd() function :" + exmsg);
            }
        }

        ~Servicefd()
        {
            Dispose(false);
        }

        private bool disposed = false;

        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

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
                    //必须使用委托控制UI线程
                    //或者匿名委托
                    ServiceSession.Remove(this.SessionName);
                    CommanList.Remove(this.SessionName);
                    Serviceinfo(SessionName, "Dispose");
                    this.mCB = null;
                    this.SessionName = null;
                    this._ClientName = null;
                }
                disposed = true;
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Ydhlog.Error(SessionName + " Dispose() function :" + exmsg);
                Serviceinfo(SessionName, " Dispose() function :Exception:" + exmsg);
            }
        }

        #endregion

        #region WriteLog

        public void Serviceinfo(string Message, string command)
        {
            try
            {
                string mg = string.Format(@"
Time:{0};
{2} Session:{1};
$            
", DateTime.Now.ToString(), Message, command);
                Console.WriteLine(mg);
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Console.WriteLine("Serviceinfo " + exmsg);
                Ydhlog.Error("Serviceinfo " + exmsg);
            }
        }

        public void GetUserInfo(string Message, myTransport mt)
        {
            try
            {
                if (this.ClientName == null)
                {
                    OperationContext context = OperationContext.Current;
                    MessageProperties messageProperties = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpointProperty =
                    messageProperties[RemoteEndpointMessageProperty.Name]
                        as RemoteEndpointMessageProperty;
                    this.ClientName = string.Format("[IP:{0},Port:{1}]", endpointProperty.Address, endpointProperty.Port);
                }
                string mg = "";
                if (mt == myTransport.ClientToService)
                {
                    mg = string.Format(
                        @"
Time:{0};
Client:{1} ---> Service,Session:{2};
Message:{3}
$"
                        , DateTime.Now.ToString(), this.ClientName, this.SessionName, Message);
                }
                else
                {
                    mg = string.Format(
                        @"
Time:{0};
Service,Session:{1} ---> Client:{2};
Message:{3}
$"
                        , DateTime.Now.ToString(), this.SessionName, this.ClientName, Message);
                }
                Console.WriteLine(mg);
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Console.WriteLine(SessionName + " GetUserInfo() function :" + exmsg);
                Ydhlog.Error(SessionName + " GetUserInfo() function :" + exmsg);
            }
        }

        public void GetUserInfo(string Userinfo, string Message, myTransport mt)
        {
            try
            {
                string str;
                if (mt == myTransport.ClientToService)
                {
                    str = string.Format(
                        @"
Time:{0};
Client:{1} ---> Service,Session:{2};
Message:{3}
$"
                        , DateTime.Now.ToString(), Userinfo, this.SessionName, Message);
                }
                else
                {
                    str = string.Format(
                       @"
Time:{0};
Service,Session:{2} ---> Client:{1};
Message:{3}
$"
                       , DateTime.Now.ToString(), Userinfo, this.SessionName, Message);
                }
                Console.WriteLine(str);
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Console.WriteLine(SessionName + " GetUserInfo() function :" + exmsg);
                Ydhlog.Error(SessionName + " GetUserInfo() function :" + exmsg);
            }
        }

        #endregion

        #region Common ClientSendMessage

        void CommanFunction(string command, string Message)
        {
            //不知道如何判断当时是否能通信,只能用trycatch
            try
            {
                mCB.ServerSendMessage(Message);
                GetUserInfo(Message, myTransport.ServiceToClient);
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Ydhlog.Error(SessionName + " Service send " + Message + " command:" + exmsg);
                CommanList.Remove(this.SessionName);
                GetUserInfo("服务发送:" + Message + "命令报错:" + exmsg, myTransport.ServiceToClient);
            }
        }

        public void ClientSendMessage(string methodName, string xmlParam, string callOperator, string certificate)
        {
            //验证验证码
            if (certificate != Servicefd.GetConfigValue("MyCheck"))
            {
                mCB.ServerSendMessage(mywrong("certificate is wrong!"));
                return;
            }
            try
            {
                GetUserInfo("用户调用ClientSendMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam, myTransport.ClientToService);

                XElement xmdata = XElement.Parse(xmlParam);
                string fname = xmdata.Element("Function").Value;
                string fpath = xmdata.Element("xmlPath").Value;
                bool IsQQ = false;
                if (xmdata.Elements("QQ").Count() > 0)
                {
                    IsQQ = true;
                }
                if (!IsQQ)
                {
                    switch (methodName)
                    {
                        //                        //20201129新增下载文件
                        //else if (command.StartsWith("[GetFile]"))
                        //    {
                        //        string path = System.AppDomain.CurrentDomain.BaseDirectory + Files + command.Split(';')[1];
                        //        if (File.Exists(path))
                        //        {
                        //            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
                        //            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(FileGet_DoWork);
                        //            bw.RunWorkerAsync(command.Split(';')[1] + ";" + command.Split(';')[2]);
                        //            mCB.ServerSendMessage("[GetFile];True");
                        //        }
                        //        else
                        //        {
                        //            mCB.ServerSendMessage("[GetFile];False");
                        //        }
                        //    }
                        case "GetFile":
                            //解密服务器端的filename
                            string Sfilename = IoRyClass.DecryptRSA_long(xmdata.Element("Param").Element("ServerFilePath").Value, rsa.ToXmlString(true));
                            string Cfilename = IoRyClass.DecryptRSA_long(xmdata.Element("Param").Element("ClientFilePath").Value, rsa.ToXmlString(true));

                            GetUserInfo("上面命令的解密字符串为:" + Sfilename + "  " + Cfilename, myTransport.ClientToService);
                            string path = System.AppDomain.CurrentDomain.BaseDirectory + Files + Sfilename;
                            if (File.Exists(path))
                            {
                                ydhDeliver acgf = new ydhDeliver();
                                acgf.Context = File.ReadAllBytes(path);
                                acgf.DataType = "bytes";
                                acgf.Name = Cfilename;
                                acgf.FunctionName = "File";
                                acgf.Max = (acgf.Context.Length / Maxupload) + 1;
                                System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
                                bw.DoWork += new System.ComponentModel.DoWorkEventHandler(Async_DoWork);
                                bw.RunWorkerAsync(acgf);
                                mCB.ServerSendMessage(mycorrect("True", fname));
                            }
                            else
                            {
                                mCB.ServerSendMessage(mycorrect("False", fname));
                            }
                            break;
                        case "GetEncrypt":
                            mCB.ServerSendMessage(mycorrect(rsa.ToXmlString(false), fname));
                            GetUserInfo("服务发送命令:[GetEncrypt];" + rsa.ToXmlString(false), myTransport.ServiceToClient);
                            break;
                        case "GetFolderXml":
                            //><folder path="test\abc" /><
                            XElement myroot = XElement.Parse("<folder path=\"" + fpath + "\" >" + "</folder>");
                            XElement xmlpin = recursive_FolderXml(AppDomain.CurrentDomain.BaseDirectory + fpath, myroot);
                            string pathxml = xmlpin.ToString();
                            //这里由于怕xml特别大,所以采用分步传输
                            //mCB.ServerSendMessage(mycorrect(pathxml, fname));
                            ydhDeliver ac = new ydhDeliver();
                            ac.Context = IoRyClass.StringToBytes(mycorrect(pathxml, fname));
                            ac.DataType = "string";
                            ac.Name = "GetFolderXml";
                            ac.FunctionName = "GetFolderXml";
                            ac.Max = (ac.Context.Length / Maxupload) + 1;
                            System.ComponentModel.BackgroundWorker bwGetFolderXml = new System.ComponentModel.BackgroundWorker();
                            bwGetFolderXml.DoWork += new System.ComponentModel.DoWorkEventHandler(Async_DoWork);
                            bwGetFolderXml.RunWorkerAsync(ac);
                            GetUserInfo("服务发送命令:[GetFolderXml];" + mycorrect(pathxml, fname), myTransport.ServiceToClient);
                            break;
                            //20221202 新增获取文件夹以及文件的xml
                        case "GetFolderFileXml":
                            //><folder path="test\abc" /><
                            XElement myrootf = XElement.Parse("<folder path=\"" + fpath + "\" >" + "</folder>");
                            XElement xmlpinf = recursive_FolderFileXml(AppDomain.CurrentDomain.BaseDirectory + fpath, myrootf);
                            string pathxmlf = xmlpinf.ToString();
                            ydhDeliver acf = new ydhDeliver();
                            acf.Context = IoRyClass.StringToBytes(mycorrect(pathxmlf, fname));
                            acf.DataType = "string";
                            acf.Name = "GetFolderFileXml";
                            acf.FunctionName = "GetFolderFileXml";
                            acf.Max = (acf.Context.Length / Maxupload) + 1;
                            System.ComponentModel.BackgroundWorker bwGetFolderXmlF = new System.ComponentModel.BackgroundWorker();
                            bwGetFolderXmlF.DoWork += new System.ComponentModel.DoWorkEventHandler(Async_DoWork);
                            bwGetFolderXmlF.RunWorkerAsync(acf);
                            GetUserInfo("服务发送命令:[GetFolderFileXml];" + mycorrect(pathxmlf, fname), myTransport.ServiceToClient);
                            break;
                        case "CheckUpdateFiles":
                            XElement up = Checkfiles(fpath, xmdata.Element("Param").ToString());
                            GetUserInfo("服务发送命令:[CheckUpdateFiles];" + mycorrect(up.ToString(), fname), myTransport.ServiceToClient);
                            System.ComponentModel.BackgroundWorker bw4 = new System.ComponentModel.BackgroundWorker();
                            bw4.DoWork += new System.ComponentModel.DoWorkEventHandler(Update_DoWorkV4);
                            bw4.RunWorkerAsync(up);
                            break;
                        case "InvokeMember":
                            List<object> args = new List<object>();
                            foreach (var item in xmdata.Elements("Param"))
                            {
                                string DecryptString = IoRyClass.DecryptRSA_long(item.Value, rsa.ToXmlString(true));
                                args.Add(DecryptString);
                            }
                            //2015-12-15 增加解密的串 2018-02-09调整位置,以便下面报错的情况这里依然可以显示.
                            GetUserInfo("上面命令的解密字符串为:" + string.Join(",", args), myTransport.ClientToService);

                            IoRyClass ic = new IoRyClass(fpath);
                            object ret = ic.GetType().InvokeMember(fname, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, ic, args.ToArray());
                            if (ret != null)
                            {
                                if (ret.GetType() == typeof(DataSet))
                                {
                                    ydhDeliver acgf = new ydhDeliver();
                                    acgf.Context = IoRyClass.GetXmlFormatDataSet((DataSet)ret);
                                    acgf.DataType = "DataSet";
                                    acgf.Name = "DataSet";
                                    acgf.FunctionName = "DataSet";
                                    acgf.Max = (acgf.Context.Length / Maxupload) + 1;
                                    //byte[] bts = IoRyClass.GetXmlFormatDataSet((DataSet)ret);
                                    //2015-12-15 增加解密的串
                                    GetUserInfo("服务查询到的结果:\r\n" + IoRyClass.BytesToString(acgf.Context), myTransport.ServiceToClient);
                                    System.ComponentModel.BackgroundWorker bw1 = new System.ComponentModel.BackgroundWorker();
                                    bw1.DoWork += new System.ComponentModel.DoWorkEventHandler(Async_DoWork);
                                    bw1.RunWorkerAsync(acgf);
                                }
                                else if (ret.GetType() == typeof(string))
                                {
                                    mCB.ServerSendMessage(mycorrect(ret.ToString(), fname));
                                }
                            }
                            else
                            {
                                mCB.ServerSendMessage(mycorrect("True", fname));
                            }
                            break;
                        //调用存储过程的实在是有点麻烦,就不跟上面的InvokeMember合并到一起了
                        case "ExcutSP":
                            string spname = "";
                            List<DbParameter> DbParameterS = new List<DbParameter>();
                            foreach (var item in xmdata.Elements("Param"))
                            {
                                if (item.Element("SPname") != null)
                                {
                                    string DecryptString = IoRyClass.DecryptRSA_long(item.Element("ParameterName").Value, rsa.ToXmlString(true));
                                    spname = DecryptString;
                                }
                                if (item.Element("ParameterName") != null&& item.Element("Value") != null)
                                {
                                    SqlParameter sp = new SqlParameter();
                                    sp.Value = item.Element("Value").Value;
                                    sp.ParameterName = item.Element("ParameterName").Value;
                                    DbParameterS.Add(sp);
                                }
                            }
                            GetUserInfo("上面命令的解密字符串[SPname]为:" + spname, myTransport.ClientToService);

                            IoRyClass ic1 = new IoRyClass(fpath);
                            DataSet ds = ic1.ExecuteSP(spname, DbParameterS);
                            if (ds != null)
                            {
                                ydhDeliver acgf = new ydhDeliver();
                                acgf.Context = IoRyClass.GetXmlFormatDataSet((DataSet)ds);
                                acgf.DataType = "DataSet";
                                acgf.Name = "DataSet";
                                acgf.FunctionName = "DataSet";
                                acgf.Max = (acgf.Context.Length / Maxupload) + 1;
                                //byte[] bts = IoRyClass.GetXmlFormatDataSet(ds);
                                //2015-12-15 增加解密的串
                                GetUserInfo("服务查询到的结果:\r\n" + IoRyClass.BytesToString(acgf.Context), myTransport.ServiceToClient);
                                System.ComponentModel.BackgroundWorker bw1 = new System.ComponentModel.BackgroundWorker();
                                bw1.DoWork += new System.ComponentModel.DoWorkEventHandler(Async_DoWork);
                                bw1.RunWorkerAsync(acgf);
                            }
                            else
                            {
                                mCB.ServerSendMessage(mycorrect("True", fname));
                            }
                            break;
                        default:
                            Serviceinfo(SessionName, "ClientSendMessage." + methodName + ".Exception:" + "methodName错误");
                            Ydhlog.Error(SessionName + "ClientSendMessage." + methodName + ":" + "methodName错误");
                            break;
                    }
                }
                else
                {
                    //QQ部分
                    if (methodName == "Join")
                    {
                        string mname = xmdata.Element("Param").Value;
                        if (CommanList.Values.Any(x => x.QQName == mname))
                        {
                            mCB.ServerSendMessage(myQQcorrect("已经有此用户", "info"));
                        }
                        CommanList.Values.Where(x => x.SessionName == this.SessionName).First().QQName = mname;
                        GetUserInfo("用户" + mname + "登录", myTransport.ClientToService);
                        //发送login消息
                        if (CommanList.Count != 0)
                        {
                            foreach (var item in CommanList.Values)
                            {
                                item.uCommanFunction("OtherJoin", myQQcorrect(mname, "OtherJoin"));
                            }
                        }
                        mCB.ServerSendMessage(myQQcorrect(string.Join(";", CommanList.Values.Select(x => x.QQName)), "AllName"));
                    }
                }
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Serviceinfo(SessionName, "ClientSendMessage." + methodName + ".Exception:" + exmsg);
                Ydhlog.Error(SessionName + "ClientSendMessage." + methodName + ":" + exmsg);
                mCB.ServerSendMessage(mywrong("ClientSendMessage." + methodName + ":" + exmsg));
            }
        }

        #endregion

        #region 数据传输部分 ClientSendData

        string temp = "Temp\\";
        string Files = "UpdateFiles\\";
        SortedList<string, List<ydhDeliver>> myListList = new SortedList<string, List<ydhDeliver>>();
        //每次传输的最大比特数,分号后面为每次传输的最大比特数
        int Maxupload = Convert.ToInt32(Servicefd.GetConfigValue("Max"));

        public bool ClientSendData(ydhDeliver dler)
        {
            try
            {
                if (!myListList.Keys.Contains(dler.Name))
                {
                    if (dler.Index == 0)
                    {
                        List<ydhDeliver> ly = new List<ydhDeliver>();
                        myListList.Add(dler.Name, ly);
                    }
                    else
                    {
                        mCB.ServerSendMessage(mywrong("DataSend Failed!;网络不通畅,发生丢包!!"));
                        GetUserInfo("用户上传文件丢包,网络情况不好:" + dler.Name, myTransport.ClientToService);
                        //以后再处理吧
                        return false;
                    }
                }
                List<ydhDeliver> myList = myListList[dler.Name];
                //检测上传是否重复.
                if (myList.Any(x => x.Index == dler.Index))
                {
                    foreach (var item in myList.Where(x => x.Name == dler.Name && x.Index == dler.Index))
                    {
                        myList.Remove(item);
                    }
                }
                //保存
                myList.Add(dler);
                //最终拼接.
                if (dler.IsFinish)
                {
                    var re = myList.Where(x => x.Name == dler.Name).OrderBy(x => x.Index);
                    List<byte> mybt = new List<byte>();
                    foreach (var item in re)
                    {
                        mybt.AddRange(item.Context.ToList());
                    }
                    try
                    {
                        switch(dler.FunctionName)
                        {
                            case "ClientSendFile":
                                string path = System.AppDomain.CurrentDomain.BaseDirectory + Files + dler.Name;
                                if (!Directory.Exists(Path.GetDirectoryName(path)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                                }
                                File.WriteAllBytes(path, mybt.ToArray());
                                GetUserInfo("用户上传文件完成:" + dler.Name, myTransport.ClientToService);
                                mCB.ServerSendMessage(mycorrectAsync(dler.Name, dler.FunctionName, "UploadFinish"));
                                break;
                            default:
                                File.WriteAllBytes(System.AppDomain.CurrentDomain.BaseDirectory + temp + this.ClientName + dler.Name, mybt.ToArray());
                                GetUserInfo("用户上传文件完成:" + dler.Name, myTransport.ClientToService);
                                mCB.ServerSendMessage(mycorrectAsync(dler.Name, dler.FunctionName, "UploadFinish"));
                                break;
                        }
                    }
                    catch (Exception me)
                    {
                        string exmsg = "";
                        while (me.InnerException != null)
                        {
                            exmsg += me.Message + "\r\n------>\r\n";
                            me = me.InnerException;
                        }
                        exmsg += me.Message;
                        GetUserInfo("用户上传文件完成:" + dler.Name + ",错误:" + exmsg, myTransport.ClientToService);
                        Ydhlog.Error(SessionName + "  ClientSendData():" + exmsg);
                    }
                    finally
                    {
                        myList.Clear();
                        myListList.Remove(dler.Name);
                    }
                }
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Ydhlog.Error(SessionName + "  ServerReceiveData():" + exmsg);
                return false;
            }
            return true;
        }

        //20221201 集中到Async_DoWork 这个废弃
        //传送byte[]类
        //void Bytes_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    byte[] bb = (byte[])e.Argument;
        //    int maxchangdu = (bb.Length / Maxupload) + 1;
        //    mCB.ServerSendMessage(mycorrectAsync(maxchangdu.ToString(), "DataSet", "FileTransportCount"));
        //    ydhDeliver mydl = new ydhDeliver();
        //    mydl.Name = "DataSet";
        //    mydl.FunctionName = "DataSet";
        //    mydl.DataType = "DataSet";
        //    mydl.Max = maxchangdu;
        //    //2015-11-13 修改了一个边界问题,去掉了下面的等于号
        //    for (int i = 0; i * Maxupload < bb.LongLength; i++)
        //    {
        //        if (bb.LongLength > (i + 1) * Maxupload)
        //        {
        //            //通过计算int的范围,可知目前最大支持传2G的文件.
        //            //mydl.Context = bb.Skip(i * Maxupload).Take(Maxupload).ToArray();
        //            //就这几行代码,大数据量传输效率提高了近百倍
        //            byte[] newbyte = new byte[Maxupload];
        //            Array.Copy(bb, i * Maxupload, newbyte, 0, Maxupload);
        //            mydl.Context = newbyte.ToArray();
        //            mydl.IsFinish = false;
        //        }
        //        else
        //        {
        //            mydl.Context = bb.Skip(i * Maxupload).Take(Convert.ToInt32(bb.LongLength) - i * Maxupload).ToArray();
        //            mydl.IsFinish = true;
        //        }
        //        mydl.Index = i;
        //        mydl.Now = i;

        //        int mycount = 0;
        //        try
        //        {
        //            while (!mCB.ServerSendData(mydl))
        //            {
        //                mCB.ServerSendData(mydl);
        //                mycount++;
        //                if (mycount > 10)
        //                {
        //                    mCB.ServerSendMessage(mywrong("DataSend Failed!;网络不通畅,连续重发10次失败"));
        //                    return;
        //                }
        //            }
        //        }
        //        catch (Exception me)
        //        {
        //            string exmsg = "";
        //            while (me.InnerException != null)
        //            {
        //                exmsg += me.Message + "\r\n------>\r\n";
        //                me = me.InnerException;
        //            }
        //            exmsg += me.Message;
        //            Ydhlog.Error(SessionName + " Bytes_DoWork():" + exmsg);
        //            //有时候断了,就根本发不出去了,这里会异常
        //            try
        //            {
        //                mCB.ServerSendMessage(mywrong("DataSend Failed!;" + exmsg));
        //            }
        //            catch { }
        //            return;
        //        }
        //    }
        //    MonitorWrite MW = new MonitorWrite(GetUserInfo);
        //    myMonitor.BeginInvoke(MW, new object[] { this.ClientName, "用户完成接收bytes", myTransport.ServiceToClient });
        //}

        /// <summary>
        /// 20200317
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Async_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ydhDeliver ac = (ydhDeliver)e.Argument;
            byte[] bb = ac.Context;
            mCB.ServerSendMessage(mycorrectAsync(ac.Max.ToString(), ac.FunctionName, "FileTransportCount"));
            ydhDeliver mydl = new ydhDeliver();
            mydl.Name = ac.Name;
            mydl.DataType = ac.DataType;
            mydl.FunctionName = ac.FunctionName;
            mydl.Max = ac.Max;
            //2015-11-13 修改了一个边界问题,去掉了下面的等于号
            for (int i = 0; i * Maxupload < bb.LongLength; i++)
            {
                if (bb.LongLength > (i + 1) * Maxupload)
                {
                    //通过计算int的范围,可知目前最大支持传2G的文件.
                    //mydl.Context = bb.Skip(i * Maxupload).Take(Maxupload).ToArray();
                    //就这几行代码,大数据量传输效率提高了近百倍
                    byte[] newbyte = new byte[Maxupload];
                    Array.Copy(bb, i * Maxupload, newbyte, 0, Maxupload);
                    mydl.Context = newbyte.ToArray();
                    mydl.IsFinish = false;
                }
                else
                {
                    mydl.Context = bb.Skip(i * Maxupload).Take(Convert.ToInt32(bb.LongLength) - i * Maxupload).ToArray();
                    mydl.IsFinish = true;
                }
                mydl.Index = i;
                mydl.Now = i;
                int mycount = 0;
                try
                {
                    while (!mCB.ServerSendData(mydl))
                    {
                        mCB.ServerSendData(mydl);
                        mycount++;
                        if (mycount > 10)
                        {
                            mCB.ServerSendMessage(mywrong("DataSend Failed!;网络不通畅,连续重发10次失败"));
                            return;
                        }
                    }
                }
                catch (Exception me)
                {
                    string exmsg = "";
                    while (me.InnerException != null)
                    {
                        exmsg += me.Message + "\r\n------>\r\n";
                        me = me.InnerException;
                    }
                    exmsg += me.Message;
                    Ydhlog.Error(SessionName + " Bytes_DoWork():" + exmsg);
                    //有时候断了,就根本发不出去了,这里会异常
                    try
                    {
                        mCB.ServerSendMessage(mywrong("DataSend Failed!;" + exmsg));
                    }
                    catch { }
                    return;
                }
            }
            GetUserInfo(this.ClientName, "用户完成接收bytes", myTransport.ServiceToClient);
        }

        /// <summary>
        /// 20200317
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Update_DoWorkV4(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            XElement xmdata = XElement.Parse(e.Argument.ToString());
            string path = xmdata.Element("path").Value;
            var update = xmdata.Elements("file");
            mCB.ServerSendMessage(mycorrectAsync(update.Count().ToString(), "CheckUpdateFiles", "TotleFilesNumber"));
            //传输数据
            foreach (var item in update)
            {
                byte[] bb = File.ReadAllBytes(System.AppDomain.CurrentDomain.BaseDirectory + path + "\\" + item.Attribute("Name").Value);
                //20200317 这里由于要把 服务器base路径+ 客户端配置路径 + 遍历路径 更新到 客户端base+遍历路径里去,这里少一个设置路径,所以必须处理一下
                //20200318 客户端也建立相应的文件夹,
                string clientpath = path + "\\" + item.Attribute("Name").Value;
                //string clientpath = "";
                //if (path.Contains("\\"))
                //{
                //    int weizhi = path.IndexOf("\\");
                //    clientpath = Path.Combine(path.Substring(weizhi) + "\\" + item.Attribute("Name").Value);
                //}
                //else
                //{
                //    clientpath = item.Attribute("Name").Value;
                //}
                int maxchangdu = (bb.Length / Maxupload) + 1;
                //mCB.ServerSendMessage(mycorrectAsync(clientpath, "CheckUpdateFiles", "FileName"));
                mCB.ServerSendMessage(mycorrectAsync(maxchangdu.ToString(), "CheckUpdateFiles", "Begin"));
                ydhDeliver mydl = new ydhDeliver();
                mydl.Name = clientpath;
                mydl.DataType = "bytes";
                mydl.FunctionName = "CheckUpdateFiles";
                mydl.Max = maxchangdu;
                
                //2015-11-13 修改了一个边界问题,去掉了下面的等于号
                for (int i = 0; i * Maxupload < bb.LongLength; i++)
                {
                    if (bb.LongLength > (i + 1) * Maxupload)
                    {
                        //通过计算int的范围,可知目前最大支持传2G的文件.
                        //mydl.Context = bb.Skip(i * Maxupload).Take(Maxupload).ToArray();
                        //就这几行代码,大数据量传输效率提高了近百倍
                        byte[] newbyte = new byte[Maxupload];
                        Array.Copy(bb, i * Maxupload, newbyte, 0, Maxupload);
                        mydl.Context = newbyte.ToArray();
                        mydl.IsFinish = false;
                    }
                    else
                    {
                        mydl.Context = bb.Skip(i * Maxupload).Take(Convert.ToInt32(bb.LongLength) - i * Maxupload).ToArray();
                        mydl.IsFinish = true;
                    }
                    mydl.Index = i;
                    mydl.Now = i;
                    int mycount = 0;
                    try
                    {
                        while (!mCB.ServerSendData(mydl))
                        {
                            mCB.ServerSendData(mydl);
                            mycount++;
                            if (mycount > 10)
                            {
                                mCB.ServerSendMessage(mywrong("DataSend Failed!;网络不通畅,连续重发10次失败"));
                                return;
                            }
                        }
                    }
                    catch (Exception me)
                    {
                        string exmsg = "";
                        while (me.InnerException != null)
                        {
                            exmsg += me.Message + "\r\n------>\r\n";
                            me = me.InnerException;
                        }
                        exmsg += me.Message;
                        Ydhlog.Error(this.ClientName + " Update_DoWork():" + exmsg);
                        try
                        {
                            mCB.ServerSendMessage(mywrong("DataSend Failed!;" + exmsg));
                        }
                        catch { }
                        return;
                    }
                }
                mCB.ServerSendMessage(mycorrectAsync("DataSend Success!", "CheckUpdateFiles", "End"));
                GetUserInfo("服务发送命令:[Finish];用户完成下载文件:" + mydl.Name, myTransport.ServiceToClient);

            }
            mCB.ServerSendMessage(mycorrectAsync("DataSend Success!", "CheckUpdateFiles", "AllFinish"));
            GetUserInfo("服务发送命令:[AllFinish];用户完成全部下载文件.", myTransport.ServiceToClient);
        }

        delegate void MonitorWrite(string name, string Message, myTransport mt);

        #endregion

        #region 同步方法部分 SynMessage

        public string SynMessage(string methodName, string xmlParam, string callOperator, string certificate)
        {
            //验证验证码
            if (certificate != Servicefd.GetConfigValue("MyCheck"))
            {
                return mywrong("certificate is wrong!");
            }
            GetUserInfo("用户调用SynMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam, myTransport.ClientToService);
            string rst = null;
            try
            {
                XElement xmdata = XElement.Parse(xmlParam);
                string fnname = xmdata.Element("Function").Value;
                string fpath = xmdata.Element("xmlPath").Value;

                switch (methodName)
                {
                    //20221201 新增服务器端文件删除的方法
                    case "DeleteFile":
                        string filenamedel = IoRyClass.DecryptRSA_long(xmdata.Element("Param").Element("ServerFilePath").Value, rsa.ToXmlString(true));
                        GetUserInfo("上面命令的解密字符串为:" + filenamedel, myTransport.ClientToService);
                        string pathdel = System.AppDomain.CurrentDomain.BaseDirectory + Files + filenamedel;
                        if (File.Exists(pathdel))
                        {
                            File.Delete(pathdel);
                            return mycorrect("True", fnname);
                        }
                        else
                        {
                            return mycorrect("False", fnname);

                        }
                    //20221201 新增检查服务器端文件的方法
                    case "CheckFile":
                        //解密服务器端的filename
                        string filename = IoRyClass.DecryptRSA_long(xmdata.Element("Param").Element("ServerFilePath").Value, rsa.ToXmlString(true));
                        GetUserInfo("上面命令的解密字符串为:" + filename, myTransport.ClientToService);
                        string path = System.AppDomain.CurrentDomain.BaseDirectory + Files + filename;
                        bool rstb = File.Exists(path);
                        GetUserInfo("服务器返回SynMessage:[CheckFile];" + rstb, myTransport.ServiceToClient);
                        return mycorrect(rstb.ToString(), fnname);
                    case "GetEncrypt":
                        GetUserInfo("服务器返回SynMessage:[GetEncrypt];" + rsa.ToXmlString(false), myTransport.ServiceToClient);
                        return mycorrect(rsa.ToXmlString(false), fnname);
                    case "EachSendBytesMaxNum":
                        GetUserInfo("服务器返回SynMessage:" + "[EachSendBytesMaxNum];" + Maxupload.ToString(), myTransport.ServiceToClient);
                        return mycorrect(Maxupload.ToString(), fnname);
                    case "InvokeMember":
                        List<object> args = new List<object>();
                        foreach (var item in xmdata.Elements("Param"))
                        {
                            string DecryptString = IoRyClass.DecryptRSA_long(item.Value, rsa.ToXmlString(true));
                            args.Add(DecryptString);
                        }
                        //2015-12-15 增加解密的串
                        GetUserInfo("上面命令的解密字符串为:" + string.Join(",", args), myTransport.ClientToService);
                        IoRyClass ic = new IoRyClass(fpath);
                        object ret = ic.GetType().InvokeMember(fnname, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, ic, args.ToArray());
                        if (ret != null)
                        {
                            if (ret.GetType() == typeof(DataSet))
                            {
                                //这里用到了DataSet转化为string的标准方法.
                                byte[] mbs = IoRyClass.GetXmlFormatDataSet((DataSet)ret);
                                string StringDataSet = IoRyClass.BytesToString(mbs);
                                //去掉xml的声明,因为外层还有一个xml
                                rst = StringDataSet.Replace("<?xml version=\"1.0\"?>", "");
                            }
                            else if (ret.GetType() == typeof(string))
                            {
                                rst = ret.ToString();
                            }
                        }
                        GetUserInfo("服务器返回SynMessage,发送:" + rst, myTransport.ServiceToClient);
                        return mycorrect(rst, fnname);
                    //调用存储过程的实在是有点麻烦,就不跟上面的InvokeMember合并到一起了
                    case "ExcutSP":
                        string spname = "";
                        List<DbParameter> DbParameterS = new List<DbParameter>();
                        foreach (var item in xmdata.Elements("Param"))
                        {
                            if (item.Element("SPname") != null)
                            {
                                string DecryptString = IoRyClass.DecryptRSA_long(item.Element("ParameterName").Value, rsa.ToXmlString(true));
                                spname = DecryptString;
                            }
                            if (item.Element("ParameterName") != null && item.Element("Value") != null)
                            {
                                SqlParameter sp = new SqlParameter();
                                sp.Value = item.Element("Value").Value;
                                sp.ParameterName = item.Element("ParameterName").Value;
                                DbParameterS.Add(sp);
                            }
                        }
                        GetUserInfo("上面命令的解密字符串[SPname]为:" + spname, myTransport.ClientToService);

                        IoRyClass ic1 = new IoRyClass(fpath);
                        DataSet ds = ic1.ExecuteSP(spname, DbParameterS);
                        if (ds != null)
                        {
                            //这里用到了DataSet转化为string的标准方法.
                            byte[] mbs = IoRyClass.GetXmlFormatDataSet((DataSet)ds);
                            string StringDataSet = IoRyClass.BytesToString(mbs);
                            //去掉xml的声明,因为外层还有一个xml
                            rst = StringDataSet.Replace("<?xml version=\"1.0\"?>", "");
                        }
                        else
                        {
                            rst = "";
                        }
                        return mycorrect(rst, fnname);
                    default:
                        return mywrong("服务不提供此方法!");
                }
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                GetUserInfo("服务器返回SynMessage报错:" + exmsg, myTransport.ServiceToClient);
                Ydhlog.Error(SessionName + " " + methodName + ":" + exmsg);
                return mywrong(me.Message);
            }
        }

        #endregion

        #region 其他函数

        /// <summary>
        /// 正确返回的xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string mycorrect(string data, string functionname)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <correct>true</correct>
  <Exception></Exception>
  <Function>{1}</Function>
  <return>{0}</return>
</root>", data, functionname);
        }

        /// <summary>
        /// 正确返回的xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string mycorrectAsync(string data, string functionname ,string AsyncName)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <correct>true</correct>
  <Exception></Exception>
  <Function>{1}</Function>
  <return>{0}</return>
  <Async>{2}</Async>
</root>", data, functionname, AsyncName);
        }

        /// <summary>
        /// 正确返回的xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string myQQcorrect(string data, string functionname)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <QQ>true</QQ>
  <correct>true</correct>
  <Exception></Exception>
  <Function>{1}</Function>
  <return>{0}</return>
</root>", data, functionname);
        }

        /// <summary>
        /// 错误返回的xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string mywrong(string data)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <correct>false</correct>
  <Exception>{0}</Exception>
  <return></return>
</root>", data);
        }

        /// <summary>
        /// 通过这个函数读取config的值可以即时读取,不用重启程序.
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        static string GetConfigValue(string appKey)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                string HostName = Path.GetFileName(Assembly.GetEntryAssembly().Location);
                xDoc.Load(HostName + ".config");

                XmlNode xNode;
                XmlElement xElem;

                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                Ydhlog.Error("GetConfigValue():" + exmsg);
                return "读取xml错误:" + exmsg;
            }
        }


        /// <summary>
        /// 改进更新的方法,最终只需要将更新的文件拷贝到文件夹即可,没有任何附加操作.
        /// 改进更新的方法,exe和dll比较版本号,其他文件太难搞定,目前只比较长度,修改配置文件时记得人为增加减少长度.
        /// 20200316 改进的方法,不能比较版本号的,比较文件的修改时间
        /// </summary>
        /// <param name="path"></param>
        /// <param name="xmls"></param>
        /// <returns></returns>
        XElement Checkfiles(string path, string xmls)
        {
            XElement upxe = XElement.Parse(string.Format("<update></update>"));
            XElement pathxml = XElement.Parse(string.Format("<path>{0}</path>", path));
            upxe.Add(pathxml);
            if (xmls == "")
            {
                xmls = "<files />";
            }
            XElement xml = XElement.Parse(xmls);
            DirectoryInfo TheFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + path + "\\");
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                var jieguo = xml.Element("files").Elements("file").Where(x => x.Attribute("Name").Value.ToLower() == NextFile.Name.ToLower());
                //为零说明服务器上有此文件,本地没有,需要更新
                if (jieguo.Count() == 0)
                {
                    XElement fx = new XElement("file");
                    fx.Add(new XAttribute("Name", NextFile.Name));
                    upxe.Add(fx);
                }
                else
                {
                    if (jieguo.First().Attribute("Extension").Value.ToLower() == ".exe" || jieguo.First().Attribute("Extension").Value.ToLower() == ".dll")
                    {
                        FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(NextFile.FullName);
                        //也有.net认不出版本号的dll
                        if (myFileVersion.ProductVersion == null)
                        {
                            if (jieguo.First().Attribute("MD5").Value != IoRyClass.MD5(File.ReadAllBytes(NextFile.FullName)))
                            {
                                XElement fx = new XElement("file");
                                fx.Add(new XAttribute("Name", NextFile.Name));
                                upxe.Add(fx);
                            }
                        }
                        else
                        {
                            if (jieguo.First().Attribute("ProductVersion").Value != myFileVersion.ProductVersion)
                            {
                                XElement fx = new XElement("file");
                                fx.Add(new XAttribute("Name", NextFile.Name));
                                upxe.Add(fx);
                            }
                        }
                    }
                    else
                    {
                        if (jieguo.First().Attribute("MD5").Value != IoRyClass.MD5(File.ReadAllBytes(NextFile.FullName)))
                        {
                            XElement fx = new XElement("file");
                            fx.Add(new XAttribute("Name", NextFile.Name));
                            upxe.Add(fx);
                        }
                    }
                }
            }

            return upxe;
        }

        XElement recursive_FolderXml(string TheFolderPath , XElement root)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(TheFolderPath);
            foreach (DirectoryInfo item in TheFolder.GetDirectories())
            {
                XElement fx = new XElement("folder");
                fx.Add(new XAttribute("path", item.FullName.Replace(AppDomain.CurrentDomain.BaseDirectory, "")));
                XElement xml = recursive_FolderXml(item.FullName, fx);
                root.Add(fx);
            }
            return root;
        }

        XElement recursive_FolderFileXml(string TheFolderPath, XElement root)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(TheFolderPath);
            foreach (FileInfo item in TheFolder.GetFiles())
            {
                XElement fx = new XElement("file", item.FullName.Replace(AppDomain.CurrentDomain.BaseDirectory, ""));                
                root.Add(fx);
            }
            foreach (DirectoryInfo item in TheFolder.GetDirectories())
            {
                XElement fx = new XElement("folder");
                fx.Add(new XAttribute("path", item.FullName.Replace(AppDomain.CurrentDomain.BaseDirectory, "")));
                XElement xml = recursive_FolderFileXml(item.FullName, fx);
                root.Add(fx);
            }
            return root;
        }

        #endregion

    }
}
