using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using yezhanbafang.fw.Core;

namespace yezhanbafang.fw.LoadBalance.Server
{
    /// <summary>
    /// 服务消息委托
    /// </summary>
    /// <param name="msg"></param>
    public delegate void ServiceSendMsg(string msg);
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LoadBalanceService : ImyService, IDisposable
    {
        #region 消息日志相关

        /// <summary>
        /// 服务消息事件
        /// </summary>
        public event ServiceSendMsg ServiceSendMsgEvent;

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
                return LoadBalanceService._ydhlog;
            }
        }

        void ConsoleMsg(string msg)
        {
            Console.WriteLine(msg);
            if (this.ServiceSendMsgEvent != null)
            {
                this.ServiceSendMsgEvent(msg);
            }
        }
        void ConsoleErrorMsg(string msg)
        {
            Console.WriteLine(msg);
            Ydhlog.Error(SessionName + " Servicefd() function :" + msg);
            if (this.ServiceSendMsgEvent != null)
            {
                this.ServiceSendMsgEvent(msg);
            }
        }
        static void ConsoleErrorMsg_static(string msg)
        {
            Console.WriteLine(msg);
            Ydhlog.Error(msg);
        }

        string AddMsgService(string SessionName, string command)
        {
            string mg = string.Format(@"
Time:{0};
{2} Session:{1};
$            
", DateTime.Now.ToString(), SessionName, command);
            return mg;
        }

        string AddMsgClient(string Message, myTransport mt)
        {
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
            return mg;
        }

        #endregion

        #region 消息xml相关

        string mywrong(string data)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <correct>false</correct>
  <Exception>{0}</Exception>
  <return></return>
</root>", data);
        }

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

        #endregion

        #region 其他函数

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
                {
                    return xElem.GetAttribute("value");
                }
                else
                {
                    return "";
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
                ConsoleErrorMsg_static("GetConfigValue() function :Exception:" + exmsg);
                return null;
            }
        }

        /// <summary>
        /// CPU使用率
        /// </summary>
        public static float CPULoad { get; set; } = 0;

        #endregion

        #region 实现ImyService

        bool ImyService.ClientSendData(ydhDeliver dler)
        {
            throw new NotImplementedException();
        }

        void ImyService.ClientSendMessage(string methodName, string xmlParam, string callOperator, string certificate)
        {
            ConsoleMsg(AddMsgClient("用户调用ClientSendMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam, myTransport.ClientToService));
            //验证验证码
            if (certificate != LoadBalanceService.GetConfigValue("MyCheck"))
            {
                mCB.ServerSendMessage(methodName, mywrong("certificate is wrong!"));
                return;
            }
            try
            {
                XElement xmdata = XElement.Parse(xmlParam);
                string fname = xmdata.Element("Function").Value;
                string fpath = xmdata.Element("xmlPath").Value;
                string fparam = xmdata.Element("Param").Value;
                switch (methodName)
                {
                    case "CallBLL":
                        BllClass bc = JsonConvert.DeserializeObject<BllClass>(fparam);
                        Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + bc.DLLName);
                        object obj = assembly.CreateInstance(bc.ClassName);
                        object ret = obj.GetType().InvokeMember(bc.Function, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, obj, new object[] { fparam });

                        byte[] bts = IoRyClass.StringToBytes(mycorrect(ret.ToString(), methodName));
                        System.ComponentModel.BackgroundWorker bw1 = new System.ComponentModel.BackgroundWorker();
                        bw1.DoWork += new System.ComponentModel.DoWorkEventHandler(Bytes_DoWork);
                        bw1.RunWorkerAsync(bts);

                        ConsoleMsg(AddMsgClient(mycorrect(ret.ToString(), methodName), myTransport.ServiceToClient));
                        break;
                    default:
                        ConsoleErrorMsg(AddMsgClient("用户调用ClientSendMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam + "\r\n" + "Error报错:" + "服务不提供此方法!", myTransport.ServiceToClient));
                        mCB.ServerSendMessage(methodName, mywrong("服务不提供此方法!"));
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
                ConsoleErrorMsg(AddMsgClient("用户调用ClientSendMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam + "\r\n" + "Error报错:" + exmsg, myTransport.ServiceToClient));
                mCB.ServerSendMessage(methodName, mywrong(exmsg));
            }
        }

        int Maxupload = Convert.ToInt32(LoadBalanceService.GetConfigValue("Max"));
        //传送byte[]类
        void Bytes_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            byte[] bb = (byte[])e.Argument;
            int maxchangdu = (bb.Length / Maxupload) + 1;
            mCB.ServerSendMessage("FilesCount", mycorrect(maxchangdu.ToString(), "FilesCount"));
            ydhDeliver mydl = new ydhDeliver();
            mydl.Name = "Json";
            mydl.FunctionName = "Json";
            mydl.DataType = "string";
            mydl.Max = maxchangdu;
            //2015-11-13 修改了一个边界问题,去掉了下面的等于号
            for (int i = 0; i * Maxupload < bb.LongLength; i++)
            {
                if (bb.LongLength > (i + 1) * Maxupload)
                {
                    //通过计算int的范围,可知目前最大支持传2G的文件.
                    mydl.Context = bb.Skip(i * Maxupload).Take(Maxupload).ToArray();
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
                            mCB.ServerSendMessage("FilesDelivering", mywrong("DataSend Failed!;网络不通畅,连续重发10次失败!"));
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
                    ConsoleErrorMsg(AddMsgClient("用户调用Bytes_DoWork,Error报错:" + exmsg, myTransport.ServiceToClient));
                    //有时候断了,就根本发不出去了,这里会异常
                    try
                    {
                        mCB.ServerSendMessage("FilesDelivering", mywrong("DataSend Failed!;" + exmsg));
                    }
                    catch { }
                    return;
                }
            }
        }

        string ImyService.SynMessage(string methodName, string xmlParam, string callOperator, string certificate)
        {
            ConsoleMsg(AddMsgClient("用户调用SynMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam, myTransport.ClientToService));
            //验证验证码
            if (certificate != LoadBalanceService.GetConfigValue("MyCheck"))
            {
                return mywrong("certificate is wrong!");
            }
            try
            {
                XElement xmdata = XElement.Parse(xmlParam);
                string XmlFunction = xmdata.Element("Function").Value;
                string fpath = xmdata.Element("xmlPath").Value;
                string fparam = xmdata.Element("Param").Value;
                switch (methodName)
                {
                    case "GetServerStatus":
                        string msgxml = this.mycorrect(LoadBalanceService.CPULoad.ToString(), methodName);
                        ConsoleMsg(AddMsgClient(msgxml, myTransport.ServiceToClient));
                        return msgxml;
                    case "CallBLL":
                        BllClass bc = JsonConvert.DeserializeObject<BllClass>(fparam);
                        Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + bc.DLLName);
                        object obj = assembly.CreateInstance(bc.ClassName);
                        object ret = obj.GetType().InvokeMember(bc.Function, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, obj, new object[] { fparam });
                        string cbreturn = mycorrect(ret.ToString(), methodName);
                        ConsoleMsg(AddMsgClient(mycorrect(ret.ToString(), methodName), myTransport.ServiceToClient));
                        return cbreturn;
                    default:
                        ConsoleErrorMsg(AddMsgClient("用户调用SynMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam + "\r\n" + "Error报错:" + "服务不提供此方法!", myTransport.ServiceToClient));
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
                ConsoleErrorMsg(AddMsgClient("用户调用SynMessage,发送命令:" + methodName + " 用户:" + callOperator + "\r\n" + xmlParam + "\r\n" + "Error报错:" + exmsg, myTransport.ServiceToClient));
                return mywrong(exmsg);
            }

        }

        #endregion

        #region 消息实例 委托链相关

        ImyCallBack mCB = OperationContext.Current.GetCallbackChannel<ImyCallBack>();

        public string SessionName = null;

        //通用函数
        public delegate void CommanDelegate(string command, string Message);
        public static SortedList<string, UserInfo> CommanList = new SortedList<string, UserInfo>();
        public static SortedList<string, string> ServiceSession = new SortedList<string, string>();

        private string _ClientName = null;

        public string ClientName
        {
            get
            {
                if (this._ClientName == null)
                {
                    OperationContext context = OperationContext.Current;
                    MessageProperties messageProperties = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpointProperty =
                    messageProperties[RemoteEndpointMessageProperty.Name]
                        as RemoteEndpointMessageProperty;
                    this. _ClientName = string.Format("[IP:{0},Port:{1}]", endpointProperty.Address, endpointProperty.Port);
                }
                return this._ClientName;
            }
            set
            {
                this._ClientName = value;
            }
        }

        public LoadBalanceService()
        {
            try
            {
                SessionName = DateTime.Now.ToShortTimeString() + ":" + Guid.NewGuid().ToString();
                ServiceSession.Add(this.SessionName, this.ClientName);
                UserInfo uf = new UserInfo();
                uf.SessionName = this.SessionName;
                uf.uCommanFunction = CommanFunction;
                CommanList.Add(this.SessionName, uf);
                ConsoleMsg(AddMsgService(this.SessionName, "Create"));
                ConsoleMsg(string.Format(@"
Time{0};
Client:{1} into Session:{2};
$
", DateTime.Now.ToString(), this.ClientName, this.SessionName));

                //先异步发一个CPU状态
                string msgxml = mycorrect(LoadBalanceService.CPULoad.ToString(), "GetServerStatus");
                mCB.ServerSendMessage("GetServerStatus", msgxml);
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
                ConsoleErrorMsg(AddMsgService(SessionName, "LoadBalanceService() function :Exception:" + exmsg));
            }
        }
        static System.ComponentModel.BackgroundWorker HBbw;
        static int CPULevelNum = 20;
        static LoadBalanceService()
        {
            CPULevelNum = Convert.ToInt32(ConfigurationManager.AppSettings["CPULevelNum"]);
            HBbw = new System.ComponentModel.BackgroundWorker();
            HBbw.DoWork += new System.ComponentModel.DoWorkEventHandler(KeepChecking);
            HBbw.RunWorkerAsync();
        }
        static void KeepChecking(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                PerformanceCounter pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                pcCpuLoad.NextValue();
                Thread.Sleep(1000);
                float cpuLoad = pcCpuLoad.NextValue();
                //这里如果CPU使用率变化超过多少,主动通知所有客户端 cpuload的范围是0~100
                if (System.Math.Abs(LoadBalanceService.CPULoad - cpuLoad) > CPULevelNum)
                {
                    //通知
                    LoadBalanceService.SendtoAll("GetServerStatus", cpuLoad.ToString());
                }
                LoadBalanceService.CPULoad = cpuLoad;
            }
        }

        void CommanFunction(string command, string Message)
        {
            //不知道如何判断当时是否能通信,只能用trycatch
            string msgxml = mycorrect(Message, command);
            try
            {
                mCB.ServerSendMessage(command, msgxml);
                ConsoleMsg(AddMsgClient(msgxml, myTransport.ServiceToClient));
            }
            catch (Exception me)
            {
                CommanList.Remove(this.SessionName);
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "\r\n------>\r\n";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                ConsoleErrorMsg(AddMsgClient("服务发送:" + msgxml + "\r\n命令报错:" + exmsg, myTransport.ServiceToClient));
            }
        }

        static void SendtoAll(string command, string msg)
        {
            foreach (var item in LoadBalanceService.CommanList)
            {
                try
                {
                    item.Value.uCommanFunction(command, msg);
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
                    ConsoleErrorMsg_static(item.Key + ":SendtoAll() function :Exception:" + exmsg);
                }
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ServiceSession.Remove(this.SessionName);
                    CommanList.Remove(this.SessionName);
                    this.mCB = null;
                    this.SessionName = null;
                    this._ClientName = null;
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~LoadBalanceService()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    #region 其他的类,枚举

    public class UserInfo
    {
        public string SessionName { get; set; }
        public string QQName { get; set; }
        public LoadBalanceService.CommanDelegate uCommanFunction { get; set; }
    }

    public enum myTransport
    {
        ClientToService,
        ServiceToClient
    }

    #endregion
}
