using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo.forms
{
    public partial class logdata : BaseCommon
    {
        public logdata()
        {
            InitializeComponent();
        }

        private void logdata_Load(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT top 200  IP_str as 操作IP, sopreater_str as 操作者, type_str as 类型, tablename_str as 表名, SQL_str as [SQL],olddata_str as 旧数据, createtime_dt as 创建时间,UUID_GUID_str as UUID, log_data_GUID as ID
            FROM      log_data order by createtime_dt desc; ");
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.LastSql = sql;
            this.dtp_start.Value = DateTime.Now.AddDays(-1);
            this.dtp_end.Value = DateTime.Now.AddDays(1);
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT  IP_str as 操作IP, sopreater_str as 操作者, type_str as 类型, tablename_str as 表名, SQL_str as [SQL],olddata_str as 旧数据, createtime_dt as 创建时间,UUID_GUID_str as UUID, log_data_GUID as ID
            FROM      log_data where createtime_dt between '{0}' and  '{1}'", this.dtp_start.Value, this.dtp_end.Value);
            //Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC,
                ((MainForm.MainForm)this.MdiParent).toolStripProgressBar1.ProgressBar, new List<Button> { this.bt_OK, this.bt_chaxun });
            this.LastSql = sql;
        }

        private void bt_chaxun_Click(object sender, EventArgs e)
        {
            selectControl.QueryForm f = new selectControl.QueryForm(AppDomain.CurrentDomain.BaseDirectory + "config\\query\\logQuery.xml");
            f.QueryEvent += F_QueryEvent;
            f.ShowDialog();
        }
        private void F_QueryEvent(string sql)
        {
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.LastSql = sql;
        }
    }
}
