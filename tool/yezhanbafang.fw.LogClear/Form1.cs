using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yezhanbafang.fw.LogClear
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label1.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(this.textBox1.Text,out a))
            {
                MessageBox.Show("时间间隔请填写整数!");
                return;
            }
            if (!int.TryParse(this.textBox2.Text, out a))
            {
                MessageBox.Show("日志保留时间请填写整数!");
                return;
            }
            if (this.label1.Text == "日志路径")
            {
                MessageBox.Show("请选择日志文件夹!");
                return;
            }
            this.timer1.Interval = Convert.ToInt32(this.textBox1.Text) * 60 * 1000;
            this.timer1.Enabled = true;
            timer1_Tick(null, null);
            this.label3.Text = "日志自动清除已开启!";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Directory.Exists(this.label1.Text))
            {
                DirectoryInfo did = new DirectoryInfo(this.label1.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }
            if (this.lb_lj2.Text != "日志路径")
            {
                DirectoryInfo did = new DirectoryInfo(this.lb_lj2.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }
            if (this.lb_lj3.Text != "日志路径")
            {
                DirectoryInfo did = new DirectoryInfo(this.lb_lj3.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }
            if (this.lb_lj4.Text != "日志路径")
            {
                DirectoryInfo did = new DirectoryInfo(this.lb_lj4.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }
            if (this.lb_lj5.Text != "日志路径")
            {
                DirectoryInfo did = new DirectoryInfo(this.lb_lj5.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }
            if (this.lb_lj6.Text != "日志路径")
            {
                DirectoryInfo did = new DirectoryInfo(this.lb_lj6.Text);
                foreach (var item in did.GetFiles().Where(x => x.CreationTime < DateTime.Now.AddDays(-Convert.ToInt32(this.textBox2.Text))))
                {
                    item.Delete();
                }
            }

        }

        private void bt_lj2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_lj2.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void bt_lj3_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_lj3.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void bt_lj4_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_lj4.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void bt_lj5_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_lj5.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void bt_lj6_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_lj6.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
