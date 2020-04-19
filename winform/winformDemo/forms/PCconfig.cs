using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using yezhanbafang.fw.Core;

namespace yezhanbafang.fw.winform.Demo.forms
{
    public partial class PCconfig : BaseCommon
    {
        public PCconfig()
        {
            InitializeComponent();
        }

        private void PCconfig_Load(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT   IP_str as IP, UUID_GUID as UUID, key_str as [key], value_str as value, createtime_dt as 创建时间, changetime_dt as 修改时间, PC_config_GUID as ID
            FROM      PC_config; ");
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.LastSql = sql;
            this.dtp_start.Value = DateTime.Now.AddDays(-1);
            this.dtp_end.Value = DateTime.Now.AddDays(1);
            //datagridview虽然设置了protect但是依然不行,如果实在不行的话,这个控件不继承了
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string mindex = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string sql = string.Format("select * from PC_config where PC_config_GUID='{0}'", mindex);
                userChange(sql);
                freshsql();
            }
        }

        void freshsql()
        {
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, this.LastSql, IoRyFunction.IC);
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT   IP_str as IP, UUID_GUID as UUID, key_str as [key], value_str as value, createtime_dt as 创建时间, changetime_dt as 修改时间, PC_config_GUID as ID
            FROM      PC_config where createtime_dt between '{0}' and  '{1}'", this.dtp_start.Value, this.dtp_end.Value);
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.LastSql = sql;
        }

        private void bt_chaxun_Click(object sender, EventArgs e)
        {
            selectControl.QueryForm f = new selectControl.QueryForm(AppDomain.CurrentDomain.BaseDirectory + "config\\query\\PCQuery.xml");
            f.QueryEvent += F_QueryEvent;
            f.ShowDialog();
        }
        private void F_QueryEvent(string sql)
        {
            Base.MyTool.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.LastSql = sql;
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            XElement xe = XElement.Load("config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "PC_config").First(), "日志类");
            cf.AddEvent += Cf_AddEvent;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "本地配置新增";
            cf.ShowDialog();
            freshsql();
        }
        private bool Cf_RepeatEvent(string sql)
        {
            //单个的重复可以通过配置实现,但是这里的多条件重复必须单独写一下
            string ip = "";
            if (this.ip != null)
            {
                ip = this.ip;
            }
            else
            {
                ip = Program.PCIP;
            }
            sql += string.Format(" and IP_str='{0}'", ip);
            DataSet ds = Base.MyTool.GetDataSet(sql, IoRyFunction.IC);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Cf_AddEvent(object ob)
        {
            ((PC_config)ob).createtime_dt = DateTime.Now;
            ((PC_config)ob).IP_str = Program.PCIP;
            ((PC_config)ob).UUID_GUID = Guid.Parse(Program.UUID);
            ((PC_config)ob).PC_config_GUID = Guid.NewGuid();
            return true;
        }

        private void bt_change_Click(object sender, EventArgs e)
        {
            string mindex = this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString();
            string sql = string.Format("select * from PC_config where PC_config_GUID='{0}'", mindex);
            userChange(sql);
            freshsql();
        }
        void userChange(string sql)
        {
            IoRyEntity<PC_config> imu = new IoRyEntity<PC_config>();
            PC_config mu = imu.GetData_IoRyClass(sql).First();

            XElement xe = XElement.Load("config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "PC_config").First(), mu, "日志类");
            cf.UpdateEvent += Cf_UpdateEvent;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "配置修改";
            cf.ShowDialog();
        }
        private bool Cf_UpdateEvent(object ob)
        {
            ((PC_config)ob).changetime_dt = DateTime.Now;
            return true;
        }

        private void bt_IP_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from PC_config where IP_str='{0}'", this.txt_IP.Text.Trim().Replace("'", "''"));
            DataTable dt = Base.MyTool.GetDataSet(sql, IoRyFunction.IC).Tables[0];
            if (dt.Rows.Count == 0)
            {
                if (MessageBox.Show("找不到此IP的UUID记录!是否继续?", "是否继续?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (dt.Rows.Count > 0)
            {
                this.ip = Convert.ToString(dt.Rows[0]["IP_str"]);
                if (dt.Rows[0]["UUID_GUID"] != DBNull.Value)
                {
                    this.uuid = Convert.ToString(dt.Rows[0]["UUID_GUID"]);
                }
                
            }
            else
            {
                this.ip = this.txt_IP.Text.Trim().Replace("'", "''");
            }
            XElement xe = XElement.Load("config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "PC_config").First(), "日志类");
            cf.AddEvent += Cf_AddEvent2;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "本地配置新增";
            cf.ShowDialog();
            freshsql();
        }
        string ip = null;
        string uuid = null;
        private bool Cf_AddEvent2(object ob)
        {
            ((PC_config)ob).createtime_dt = DateTime.Now;
            ((PC_config)ob).IP_str = this.ip;
            if (this.uuid != null)
            {
                ((PC_config)ob).UUID_GUID = Guid.Parse(this.uuid);
            }
            ((PC_config)ob).PC_config_GUID = Guid.NewGuid();
            this.ip = null;
            this.uuid = null;
            return true;
        }

        private void bt_global_Click(object sender, EventArgs e)
        {
            //PC_config pc = new PC_config();
            //pc.PC_config_GUID = Guid.Parse("47EC7042-27E4-429D-98C2-70A4963E86F8");
            //pc.value_str = "select * from Users where loginname_str='{0}';";
            //pc.IoRyUpdate();

            XElement xe = XElement.Load("config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "PC_config").First(), "日志类");
            cf.AddEvent += Cf_AddEventglobal;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "本地配置新增";
            cf.ShowDialog();
            freshsql();
        }
        private bool Cf_AddEventglobal(object ob)
        {
            ((PC_config)ob).createtime_dt = DateTime.Now;
            ((PC_config)ob).IP_str = "*.*.*.*";
            ((PC_config)ob).PC_config_GUID = Guid.NewGuid();
            return true;
        }
    }
}
