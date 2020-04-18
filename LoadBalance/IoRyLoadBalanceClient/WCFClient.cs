using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using yezhanbafang.fw.Core;

namespace yezhanbafang.fw.WCF.LoadBalance.Client
{
    /// <summary>
    /// 服务器状态变化
    /// </summary>
    public delegate void ServerStatusChanging();

    [CallbackBehavior(IncludeExceptionDetailInFaults = true)]
    public class WCFClient : ImyCallBack, IDisposable
    {
        #region 服务器状态相关

        /// <summary>
        /// 服务器状态变化
        /// </summary>
        public event ServerStatusChanging ServerStatusChangingEvent;
        public bool ServerAlive { get; set; } = false;
        /// <summary>
        /// 服务器CPU使用率
        /// </summary>
        public float ServerCPUload { get; set; }
        /// <summary>
        /// 服务器执行耗时
        /// </summary>
        public double ServerExcuteCost { get; set; }
        /// <summary>
        /// 服务器CPU使用率级别 这里人为将CPU分为 0~20 20~40 40~60 60~100
        /// </summary>
        public int ServerCPUloadLevel { get; set; }

        public string ServerURL { get; set; }

        /// <summary>
        /// 这里人为将CPU分为 0~20 20~40 40~60 60~100
        /// </summary>
        /// <param name="cpuloadlast"></param>
        /// <param name="timecostlast"></param>
        void ResetCPU(float cpuloadlast, double timecostlast)
        {
            if (System.Math.Abs(this.ServerCPUload - cpuloadlast) > this.CPULevelNum)
            {
                this.ServerCPUload = cpuloadlast;
                this.ServerExcuteCost = timecostlast;
                this.ServerCPUloadLevel = Convert.ToInt32(this.ServerCPUload / this.CPULevelNum);
                if (this.ServerStatusChangingEvent != null)
                {
                    this.ServerStatusChangingEvent();
                }
            }
            else
            {
                this.ServerCPUload = cpuloadlast;
                this.ServerExcuteCost = timecostlast;
                this.ServerCPUloadLevel = Convert.ToInt32(this.ServerCPUload / this.CPULevelNum);
            }
        }

        void ResetCPU(float cpuloadlast)
        {
            if (System.Math.Abs(this.ServerCPUload - cpuloadlast) > this.CPULevelNum)
            {
                this.ServerCPUload = cpuloadlast;
                this.ServerCPUloadLevel = Convert.ToInt32(this.ServerCPUload / this.CPULevelNum);
                if (this.ServerStatusChangingEvent != null)
                {
                    this.ServerStatusChangingEvent();
                }
            }
            else
            {
                this.ServerCPUload = cpuloadlast;
                this.ServerCPUloadLevel = Convert.ToInt32(this.ServerCPUload / this.CPULevelNum);
            }
        }

        #endregion

        #region 初始化,维护状态相关

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="HearBeatSecond">这里默认9分钟,因为默认10分钟无消息就自动释放了.</param>
        public WCFClient(string url, int HearBeatSecond = 1000 * 60 * 9, int cpuLevelNum = 20)
        {
            try
            {
                this.ServerURL = url;
                address = new EndpointAddress(url);
                this._HearBeatSecond = HearBeatSecond;
                this.CPULevelNum = cpuLevelNum;

                //初始化
                string xl = myxml("", "", new List<string> { "" });

                DateTime TimeStart = DateTime.Now;
                string rxl = MyISC.SynMessage("GetServerStatus", xl, "system", "ydh");
                double timecost = DateTime.Now.Subtract(TimeStart).TotalMilliseconds;

                XElement xmdata = XElement.Parse(rxl);
                float CPUload = Convert.ToSingle(xmdata.Element("return").FirstNode.ToString());
                this.ResetCPU(CPUload, timecost);
                Console.WriteLine($"初始化 Name:{this.ServerURL}  CPUload:{CPUload}  timecost:{timecost} CPUlevel:{this.ServerCPUloadLevel}");
                this.ServerAlive = true;
            }
            catch (Exception me)
            {
                this.isc = null;
                Console.WriteLine($"初始化  Name:{url} 初始化错误:{me.Message}");
                //throw me;
            }
            finally
            {
                HBbw = new System.ComponentModel.BackgroundWorker();
                HBbw.DoWork += new System.ComponentModel.DoWorkEventHandler(KeepHearBeat);
                HBbw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 心跳刷新频率
        /// </summary>
        int _HearBeatSecond = 1000 * 60 * 9;
        /// <summary>
        /// 
        /// </summary>
        int CPULevelNum = 20;
        System.ComponentModel.BackgroundWorker HBbw;
        /// <summary>
        /// 这里正好用获取服务器状态当心跳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void KeepHearBeat(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(_HearBeatSecond);
                try
                {
                    string xl = myxml("", "", new List<string> { "" });
                    DateTime TimeStart = DateTime.Now;
                    string rxl = MyISC.SynMessage("GetServerStatus", xl, "system", "ydh");
                    double timecost = DateTime.Now.Subtract(TimeStart).TotalMilliseconds;
                    XElement xmdata = XElement.Parse(rxl);
                    float CPUload = Convert.ToSingle(xmdata.Element("return").FirstNode.ToString());
                    this.ResetCPU(CPUload, timecost);
                    Console.WriteLine($"ClientKeepHearBeat Name:{this.ServerURL} CPUload:{CPUload}  timecost:{timecost}  CPUlevel:{this.ServerCPUloadLevel}");
                }
                catch(Exception me)
                {
                    this.ServerAlive = false;
                    this.isc = null;
                    if (this.ServerStatusChangingEvent != null)
                    {
                        this.ServerStatusChangingEvent();
                    }
                    Console.WriteLine($"ClientKeepHearBeat Name:{this.ServerURL} 报错:{me.Message}");
                }
            }
        }

        ImyService isc;
        EndpointAddress address = null;
        ImyService MyISC
        {
            get
            {
                try
                {
                    if (isc == null)
                    {
                        isc = createClient();
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

        #endregion

        #region 其他

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

        #region 方法调用

        /// <summary>
        /// 这里来存放一些上下过程中调用端要传送的东西
        /// </summary>
        object _obj;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="json"></param>
        /// <param name="callOperator"></param>
        /// <param name="certificate"></param>
        public void CallWCF_Async(string FName, string json, string callOperator, string certificate,object obj)
        {
            this._obj = obj;
            string mxml = myxml("", "", new List<string> { json });
            MyISC.ClientSendMessage(FName, mxml, callOperator, certificate);
        }

        /// <summary>
        /// 同步调用方法
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="json"></param>
        /// <param name="callOperator"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public string CallWCF_Syn(string FName, string json, string callOperator, string certificate)
        {
            string mxml = myxml("", "", new List<string> { json });
            return MyISC.SynMessage(FName, mxml, callOperator, certificate);
        }

        #endregion

        #region ImyCallBack

        List<ydhDeliver> myList = new List<ydhDeliver>();
        public event GetDeliver eGetDeliver;
        public event AsnyCallBll eAsnyCallBll;

        bool ImyCallBack.ServerSendData(ydhDeliver dler)
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
                    case "Json":
                        string mjson = IoRyClass.BytesToString(mybt.ToArray());
                        if (eAsnyCallBll != null)
                        {
                            this.eAsnyCallBll(mjson, this._obj);
                        }
                        break;
                }
                myList.Clear();
            }

            if (eGetDeliver != null)
            {
                this.eGetDeliver(dler);
            }

            return true;
        }
        public event ServerSendMsg eServerSendMsg;
        void ImyCallBack.ServerSendMessage(string methodName, string xmlMsg)
        {
            if (eServerSendMsg != null)
            {
                this.eServerSendMsg(methodName, xmlMsg);   
            }
            XElement xmdata = XElement.Parse(xmlMsg);
            switch (methodName)
            {
                case "GetServerStatus":
                    float CPUload = Convert.ToSingle(xmdata.Element("return").FirstNode.ToString());
                    this.ResetCPU(CPUload);
                    Console.WriteLine($"ServerSendMessage GetServerStatus  Name:{this.ServerURL}  CPUload:{CPUload}  CPUlevel:{this.ServerCPUloadLevel}");
                    break;
                case "FilesCount":
                    int fcount = Convert.ToInt32(xmdata.Element("return").FirstNode.ToString());
                    Console.WriteLine($"ServerSendMessage FilesCount:{fcount}");
                    break;
                default:
                    break;
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
                    // TODO: 释放托管状态(托管对象)。
                    //isc = null;
                    //myList.Clear();
                    //myList = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~IoRyLoadBalanceClient()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

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
}
