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
using yezhanbafang.fw.WCF.Client;

namespace yezhanbafang.fw.winform.Demo.test
{
    public partial class testForm1 : BaseCommon
    {
        public testForm1()
        {
            InitializeComponent();
        }

        private void bt_chaxun_Click(object sender, EventArgs e)
        {
            //selectControl.QueryForm f = new selectControl.QueryForm(AppDomain.CurrentDomain.BaseDirectory + "config\\query\\OutQuery.xml");
            selectControl.QueryForm f = Base.MyToolCore.Create_QueryForm(AppDomain.CurrentDomain.BaseDirectory + "config\\query\\OutQuery.xml", IoRyFunction.IC);
            f.QueryEvent += F_QueryEvent;
            f.ShowDialog();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT   index_int as 序号, loginname_str as 登录名, name_str as 姓名, type_str as 用户类型, power_display_str as 权限, createtime_dt as 创建时间, changetime_dt as 修改时间
FROM      V_user where createtime_dt between '{0}' and  '{1}'", this.dtp_start.Value, this.dtp_end.Value);
            this.LastSql = sql;
            //统一的方式
            //Base.MyToolCore.bindDataGridView_Async(this.dataGridView1, sql, IoRyFunction.IC);
            this.freshsql();
        }

        private void testForm1_Load(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT   index_int as 序号, loginname_str as 登录名, name_str as 姓名, type_str as 用户类型, power_display_str as 权限, createtime_dt as 创建时间, changetime_dt as 修改时间
            FROM      V_user ");
            this.LastSql = sql;

            this.freshsql();

            this.dtp_start.Value = DateTime.Now.AddDays(-1);
            this.dtp_end.Value = DateTime.Now.AddDays(1);
            //datagridview虽然设置了protect但是依然不行,如果实在不行的话,这个控件不继承了
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

        }


        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string mindex = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string sql = string.Format("select * from users where index_int='{0}'", mindex);
                userChange(sql);
                this.freshsql();
            }
        }

        private void bt_change_Click(object sender, EventArgs e)
        {
            string mindex = this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            string sql = string.Format("select * from users where index_int='{0}'", mindex);
            userChange(sql);
            this.freshsql();
        }

        void userChange(string sql)
        {
            IoRyEntity<Users> imu = new IoRyEntity<Users>();
            Users mu = imu.GetData_IoRyClass(sql).First();

            XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "Users").First(), mu);
            cf.UpdateEvent += Cf_UpdateEvent;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "用户修改";
            cf.ShowDialog();
        }

        private bool Cf_UpdateEvent(object ob)
        {
            ((Users)ob).changetime_dt = DateTime.Now;
            return true;
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            XElement xe = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "Users").First());
            cf.AddEvent += Cf_AddEvent;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "用户新增";
            cf.ShowDialog();
            this.freshsql();
        }

        private bool Cf_AddEvent(object ob)
        {
            ((Users)ob).createtime_dt = DateTime.Now;
            return true;
        }

        private bool Cf_RepeatEvent(string sql)
        {
            //统一的方法
            DataSet ds = Base.MyToolCore.GetDataSet(sql, IoRyFunction.IC);


            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
