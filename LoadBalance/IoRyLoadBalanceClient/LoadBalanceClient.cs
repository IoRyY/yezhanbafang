using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace yezhanbafang.fw.WCF.LoadBalance.Client
{
    public delegate void GetDeliver(ydhDeliver mcd);
    public delegate void AsnyCallBll(string json, object obj);
    public delegate void ServerSendMsg(string methodName, string xmlMsg);
    public class LoadBalanceClient
    {
        WCFClient _BestWCFClient;
        public WCFClient BestWCFClient
        {
            get 
            {
                if (_BestWCFClient == null)
                {
                    var jieguo = lw.Where(x => x.ServerAlive).OrderBy(x => x.ServerCPUloadLevel).OrderBy(x => x.ServerExcuteCost);
                    if (jieguo.Count() == 0)
                    {
                        throw new Exception("所有的服务连接全部断开!");
                    }
                    else
                    {
                        this._BestWCFClient = jieguo.First();
                    }
                }
                return _BestWCFClient;
            }
            set 
            {
                this._BestWCFClient = value;
            }
        }
        List<WCFClient> lw = new List<WCFClient>();
        int CPULevelNum = Convert.ToInt32(ConfigurationManager.AppSettings["CPULevelNum"]);
        int HearBeatSecond = Convert.ToInt32(ConfigurationManager.AppSettings["HearBeatSecond"]);
        public LoadBalanceClient()
        {
            string WCFaddress = ConfigurationManager.AppSettings["WCFaddress"];
            foreach (var item in WCFaddress.Split(';'))
            {
                System.ComponentModel.BackgroundWorker HBbw = new System.ComponentModel.BackgroundWorker();
                HBbw.DoWork += new System.ComponentModel.DoWorkEventHandler(init);
                HBbw.RunWorkerAsync(item);
            }
        }

        public event GetDeliver eGetDeliver;
        public event AsnyCallBll eAsnyCallBll;

        void init(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            WCFClient wc = new WCFClient(Convert.ToString(e.Argument), HearBeatSecond, CPULevelNum);
            wc.eGetDeliver += Wc_eGetDeliver;
            wc.eAsnyCallBll += Wc_eAsnyCallBll;
            wc.ServerStatusChangingEvent += Wc_ServerStatusChangingEvent;
            lw.Add(wc);
        }

        private void Wc_eAsnyCallBll(string json, object csobj)
        {
            if (this.eAsnyCallBll != null)
            {
                XElement xmdata = XElement.Parse(json);
                if (xmdata.Element("correct").Value == "false")
                {
                    throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
                }
                string jsonr = xmdata.Element("return").FirstNode.ToString();
                BllClass bc = JsonConvert.DeserializeObject<BllClass>(jsonr);
                double timecost = DateTime.Now.Subtract(this.TimeStart_Async).TotalMilliseconds;
                bc.TimeCost = timecost;
                this.eAsnyCallBll(JsonConvert.SerializeObject(bc), csobj);
            }
        }

        private void Wc_eGetDeliver(ydhDeliver mcd)
        {
            if (this.eGetDeliver != null)
            {
                this.eGetDeliver(mcd);
            }
        }

        private void Wc_ServerStatusChangingEvent()
        {
            var jieguo = lw.Where(x => x.ServerAlive).OrderBy(x => x.ServerExcuteCost).OrderBy(x => x.ServerCPUloadLevel);
            if (jieguo.Count() == 0)
            {
                throw new Exception("所有的服务连接全部断开!");
            }
            else
            {
                this._BestWCFClient = jieguo.First();
            }
        }

        DateTime TimeStart_Async = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="json"></param>
        /// <param name="callOperator"></param>
        /// <param name="certificate"></param>
        public void CallWCF_Async(string json,object csobj)
        {
            TimeStart_Async = DateTime.Now;
            this.BestWCFClient.CallWCF_Async("CallBLL", json, "LoadBalanceClient", "ydh", csobj);
        }

        /// <summary>
        /// 同步调用方法
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="json"></param>
        /// <param name="callOperator"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public string CallWCF_Syn(string json)
        {
            DateTime TimeStart = DateTime.Now;
            string wcfxml = this.BestWCFClient.CallWCF_Syn("CallBLL", json, "LoadBalanceClient", "ydh");
            XElement xmdata = XElement.Parse(wcfxml);
            if (xmdata.Element("correct").Value == "false")
            {
                throw new Exception(xmdata.Element("Exception").FirstNode.ToString());
            }
            string jsonr = xmdata.Element("return").FirstNode.ToString();
            BllClass bc = JsonConvert.DeserializeObject<BllClass>(jsonr);
            double timecost = DateTime.Now.Subtract(TimeStart).TotalMilliseconds;
            bc.TimeCost = timecost;
            return JsonConvert.SerializeObject(bc);

        }
    }
}
