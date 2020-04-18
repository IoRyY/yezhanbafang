using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yezhanbafang.fw.MSSqlBakUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = "databakup" + DateTime.Now.ToFileTime() + ".bak"; 
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string ph = this.saveFileDialog1.FileName;
                    IoRyNP.IoRyClass ic = new IoRyNP.IoRyClass();
                    string sql = string.Format(@"
BACKUP DATABASE logisticsManager
TO DISK ='{0}'", ph);
                    ic.ExecuteSql(sql);
                }
                catch (Exception me)
                {
                    MessageBox.Show("导出失败:" + me.Message);
                    return;
                }
                MessageBox.Show("导出成功!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        DateTime dt = DateTime.Now.AddDays(-1).Date;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dt != DateTime.Now.Date)
                {
                    string CopyPath = System.Configuration.ConfigurationManager.AppSettings["CopyPath"];
                    string ToPath = System.Configuration.ConfigurationManager.AppSettings["ToPath"];
                    string Directorys = System.Configuration.ConfigurationManager.AppSettings["Directory"];

                    if (Directory.Exists(CopyPath))
                    {
                        if (Directorys != null && Directorys != "")
                        {
                            foreach (var item in Directorys.Split(','))
                            {
                                string newpathCopy = CopyPath + item;
                                string newpathTo = ToPath + item;
                                if (Directory.Exists(newpathCopy))
                                {
                                    DirectoryInfo di = new DirectoryInfo(newpathCopy);

                                    Ydhlog.Info(newpathCopy);

                                    FileInfo lastfile = di.GetFiles().OrderByDescending(x => x.CreationTime).First();

                                    if (lastfile.CreationTime < DateTime.Now.AddDays(-2))
                                    {
                                        sendmsg("备份失败!" + lastfile.FullName);
                                        break;
                                    }

                                    if (!Directory.Exists(newpathTo))
                                    {
                                        Directory.CreateDirectory(newpathTo);
                                    }

                                    Ydhlog.Info(newpathTo + "\\" + lastfile.Name);

                                    lastfile.CopyTo(newpathTo + "\\" + lastfile.Name, true);

                                    DirectoryInfo did = new DirectoryInfo(newpathTo);

                                    Ydhlog.Info(newpathTo);

                                    var lastfiledel = did.GetFiles().OrderByDescending(x => x.CreationTime).Skip(3);
                                    foreach (var itemd in lastfiledel)
                                    {
                                        Ydhlog.Info(itemd.FullName);

                                        itemd.Delete();
                                    }
                                }
                                else
                                {
                                    sendmsg("找不到" + newpathCopy);
                                }
                            }
                        }
                        else
                        {
                            DirectoryInfo did = new DirectoryInfo(ToPath);
                            foreach (var item in did.GetFiles())
                            {
                                item.Delete();
                            }
                            DirectoryInfo di = new DirectoryInfo(CopyPath);
                            foreach (var item in di.GetFiles())
                            {
                                item.CopyTo(ToPath + item.Name, true);
                            }
                        }
                    }
                    else
                    {
                        sendmsg("CopyPath报错!");
                    }
                    dt = DateTime.Now.Date;
                    sendmsg("异地备份成功!");
                }
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + "->";
                    me = me.InnerException;
                }
                exmsg += me.Message;

                sendmsg(exmsg);
                Ydhlog.Error(exmsg);
                dt = DateTime.Now.Date;
            }
        }

        async Task<SMSreturn> sendmsg(string msg)
        {
            if (msg.Length > 20)
            {
                msg = msg.Substring(0, 20);
            }
            XYSserver xs = new XYSserver();
            xs.errorInfo = msg;
            xs.phone = System.Configuration.ConfigurationManager.AppSettings["phone"];
            xs.serverName = System.Configuration.ConfigurationManager.AppSettings["serverName"];

            string mdata = Newtonsoft.Json.JsonConvert.SerializeObject(xs);
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(mdata);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string url = System.Configuration.ConfigurationManager.AppSettings["SMSurl"];
                HttpResponseMessage response = await client.PostAsync(url, content);//改成自己的
                response.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = await response.Content.ReadAsStringAsync();
                SMSreturn fh = Newtonsoft.Json.JsonConvert.DeserializeObject<SMSreturn>(responseBody);
                return fh;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            this.button3.Text = "异地拷贝开启";
            timer1_Tick(null, null);
        }

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
                return _ydhlog;
            }
        }
    }
}
