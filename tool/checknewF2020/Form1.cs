using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using yezhanbafang.fw.WCF.Client;

namespace yezhanbafang.fw.WCF.AutoUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string openfile = ConfigurationManager.AppSettings["openfile"];
        string updatefile = ConfigurationManager.AppSettings["updatefile"];
        string WCFaddress = ConfigurationManager.AppSettings["WCFaddress"];

        private void Form1_Load(object sender, EventArgs e)
        {
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo("yezhanbafang.fw.WCF.Client.dll");
            this.Text = "正在链接升级服务器......                   " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "  IoRyWCFClientV5.dll:" + myFileVersion.ProductVersion;

            BackgroundWorker bw = new BackgroundWorker();
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();

        }

        WCFClientV5 wc = null;
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                wc = new WCFClientV5("", "", WCFaddress);
                wc.DuxMessage += Wc_DuxMessage;
                wc.eGetDeliver += Wc_eGetDeliver;
                wc.GetFoldersPathXML(updatefile);
                while (_xml == null)
                {
                    Thread.Sleep(1000);
                }
                XElement xe = XElement.Parse(_xml);
                recursive_update(xe.Element("return"));
            }
            catch
            {
                this.Invoke((Action)delegate
                {
                    this.label1.Text = "链接升级服务器失败,直接进入程序...";
                });
                Thread.Sleep(2000);
            }
            finally
            {
                Thread.Sleep(1000);
                Process.Start(updatefile + "\\" + openfile, "yuandonghui");
                this.Invoke((Action)delegate
                {
                    this.Close();
                });
            }

        }

        private void Wc_eGetDeliver(ModelClassDeliver mcd)
        {
            switch (mcd.FunctionName)
            {
                case "CheckUpdateFiles":
                    if (mcd.Now == 0)
                    {
                        this.Invoke((Action)delegate
                        {
                            this.label1.Text = "正在更新:" + mcd.Name;
                            this.progressBar2.Maximum = mcd.Max;
                        });
                    }
                    this.Invoke((Action)delegate
                    {
                        this.progressBar2.Value++;
                    });
                    if (mcd.IsFinish)
                    {
                        this.Invoke((Action)delegate
                        {
                            this.progressBar2.Value = 0;
                            this.progressBar1.Value++;
                        });
                    }
                    break;
            }
        }

        string _xml = null;

        private void Wc_DuxMessage(string content, string Exstr)
        {
            switch (Exstr)
            {
                case "GetFolderXml":
                    _xml = content;
                    break;
                //case "FileName":
                //    this.Invoke((Action)delegate
                //    {
                //        this.label1.Text = "正在更新:" + content;
                //    });
                //    break;
                case "TotleFilesNumber":
                    this.Invoke((Action)delegate
                    {
                        this.progressBar1.Visible = true;
                        this.progressBar2.Visible = true;
                        this.progressBar1.Maximum = Convert.ToInt32(content);
                    });
                    break;
                case "AllFinish":
                    isupdate = false;
                    this.Invoke((Action)delegate
                    {
                        this.progressBar1.Value = 0;
                        this.progressBar2.Value = 0;
                    });
                    break;
                case "Error":
                    this.Invoke((Action)delegate
                    {
                        MessageBox.Show(content);
                    });
                    break;
                    //case "End":
                    //    this.Invoke((Action)delegate
                    //    {
                    //        this.progressBar2.Value = 0;
                    //        this.progressBar1.Value++;
                    //    });
                    //    break;
                    //case "ServerSendData":
                    //    this.Invoke((Action)delegate
                    //    {
                    //        this.progressBar2.Value++;
                    //    });
                    //    break;
            }
        }

        bool isupdate = false;
        void recursive_update(XElement xein)
        {
            var xh = xein.Elements("folder");
            foreach (var item in xh)
            {
                string myPath = item.Attribute("path").Value;

                this.Invoke((Action)delegate
                {
                    this.label1.Text = "检测文件夹:" + myPath;
                });

                wc.CheckUpdateFiles(myPath);
                isupdate = true;
                while (isupdate)
                {
                    Thread.Sleep(1000);
                }
                var jx = item.Elements("folder");
                if (jx.Count() > 0)
                {
                    recursive_update(item);
                }
            }
        }
    }
}
