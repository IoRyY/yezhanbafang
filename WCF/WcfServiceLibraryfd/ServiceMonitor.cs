using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace yezhanbafang.fw.WCF.Server
{
    /// <summary>
    /// 2015-12-15 由于实时的反应log会卡,所以改成点一次看一次的.
    /// </summary>
    public partial class ServiceMonitor : Form
    {
        public ServiceMonitor()
        {
            InitializeComponent();
        }

        string tem = "";
        public int ServiceSession
        {
            get
            {
                return Convert.ToInt32(this.LB_Session.Text);
            }
            set
            {
                this.LB_Session.Text = value.ToString();
            }
        }

        public void writeMessage(string message)
        {
            try
            {
                //if (IsAuto)
                //{
                //    this.rtb_message.Text += "\r\n" + message;
                //    if (this.ActiveControl != this.rtb_message)
                //    {
                //        this.rtb_message.SelectionStart = this.rtb_message.Text.Length;
                //        this.rtb_message.SelectionLength = 0;
                //        this.rtb_message.ScrollToCaret();
                //    }
                //    this.rtb_message.Text += message + "\r\n" + this.rtb_message.Text;
                //}
                //else
                //{
                    tem = tem + message;
                //}
                this.lb_MessageNum.Text = (Convert.ToInt32(this.lb_MessageNum.Text) + 1).ToString();
                //目前记录大于10000条自动清零
                //10000条还是太多了,1000条吧.反正有log了.
                if (Convert.ToInt32(this.lb_MessageNum.Text) > 1000)
                {
                    this.rtb_message.Text = "";
                    this.lb_MessageNum.Text = "0";
                    tem = "";
                }
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }

        private void ServiceMonitor_Load(object sender, EventArgs e)
        {
            this.Text = ConfigurationManager.AppSettings["name"] + "    " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            bt_auto.Select();
            this.bt_auto.Text = "Log(top1000)";
        }

        private void Bt_Clear_Click(object sender, EventArgs e)
        {
            this.rtb_message.Text = "";
            this.lb_MessageNum.Text = "0";
            tem = "";
        }

        private void bt_auto_Click(object sender, EventArgs e)
        {
            this.rtb_message.Text = "";
            this.rtb_message.Text += tem;
            //if (IsAuto)
            //{
            //    this.bt_auto.Text = "开启自动滚动";
            //    IsAuto = false;
            //}
            //else
            //{
            //    this.bt_auto.Text = "停止自动滚动";
            //    IsAuto = true;
            //    this.rtb_message.Text += tem;
            //    if (this.ActiveControl != this.rtb_message)
            //    {
            //        this.rtb_message.SelectionStart = this.rtb_message.Text.Length;
            //        this.rtb_message.SelectionLength = 0;
            //        this.rtb_message.ScrollToCaret();
            //    }
            //    tem = "";
            //}
        }

        private void bt_advance_Click(object sender, EventArgs e)
        {
            this.rtb_message.Text += tem;
            Details dl = new Details(this);
            dl.Show();
        }

        private void bt_message_Click(object sender, EventArgs e)
        {
            myMessage mM = new myMessage();
            mM.Show();
        }
    }
}
