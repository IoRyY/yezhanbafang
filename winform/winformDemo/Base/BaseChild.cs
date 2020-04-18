using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo
{
    public partial class BaseChild : Form
    {
        /// <summary>
        /// 记录最后一次执行的select sql
        /// </summary>
        public string LastSql { get; set; }

        public BaseChild()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 复杂查询界面回调的函数
        /// </summary>
        /// <param name="sql"></param>
        public virtual void fzcx(string sql)
        {
        }

        /// <summary>
        /// 设置经纬度界面的回调函数
        /// </summary>
        /// <param name="jingdu"></param>
        /// <param name="weidu"></param>
        public virtual void setjw(string jingdu, string weidu)
        {
        }

        /// <summary>
        /// 存储sql语句
        /// </summary>
        protected string Csql { get; set; }

        /// <summary>
        /// 控制界面居中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseChild_SizeChanged(object sender, EventArgs e)
        {
            //1077为默认窗体宽度 1055为panel5的默认宽度 改了一次默认高度变为711
            //当窗体扩大时,居中操作
            if (this.Width >= 1077)
            {
                this.BasetableLayoutPanel.ColumnStyles[0].Width = (this.Width - 1077) / 2;
                this.BasetableLayoutPanel.ColumnStyles[2].Width = (this.Width - 1077) / 2;
                this.panel5.Width = 1055;
            }
            else
            {
                this.BasetableLayoutPanel.ColumnStyles[0].Width = 0;
                this.BasetableLayoutPanel.ColumnStyles[2].Width = 0;
                //当窗体缩写时的滚动条控制
                this.panel5.Width = 1055 - (1077 - this.Width);
            }
            //this.Text = "this:" + this.Width.ToString() + " panel5:" + this.panel5.Width.ToString();
        }
    }
}
