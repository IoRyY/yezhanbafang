using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo
{
    public partial class BaseCommon : BaseChild
    {
        public BaseCommon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// WCF Core WebApi的方式一样 注意WCF还可以自动控制进度条与按钮状态
        /// </summary>
        /// <param name="sql"></param>
        protected void F_QueryEvent(string sql)
        {
            this.LastSql = sql;
            this.freshsql();

        }

        /// <summary>
        /// 
        /// </summary>
        protected void freshsql()
        {
            //统一的方式
            Base.MyToolCore.bindDataGridView_Async(this.dataGridView1, this.LastSql, IoRyFunction.IC, ((MainForm.MainForm)this.MdiParent).toolStripProgressBar1.ProgressBar, new List<Button> { this.bt_OK, this.bt_chaxun });

        }
    }
}
