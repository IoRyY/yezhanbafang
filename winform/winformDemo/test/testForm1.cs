using CreateDataTableTool;
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
            selectControl.QueryForm f = new selectControl.QueryForm(AppDomain.CurrentDomain.BaseDirectory + "config\\query\\OutQuery.xml");
            f.QueryEvent += F_QueryEvent;
            f.ShowDialog();
        }

        private void F_QueryEvent(string sql)
        {
            //IoRyEntity<Users> iu = new CreateDataTableTool.IoRyEntity<CreateDataTableTool.Users>();
            //BindingCollection<Users> lu = iu.GetSortdata_IoRyClass(sql);
            //this.dataGridView1.DataSource = lu;
            this.dataGridView1.DataSource = IoRyFunction.IC.GetTable(sql);
            this.LastSql = sql;
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT   index_int as 序号, loginname_str as 登录名, name_str as 姓名, type_str as 用户类型, power_display_str as 权限, createtime_dt as 创建时间, changetime_dt as 修改时间
FROM      V_user where createtime_dt between '{0}' and  '{1}'", this.dtp_start.Value, this.dtp_end.Value);
            //IoRyEntity<Users> iu = new CreateDataTableTool.IoRyEntity<CreateDataTableTool.Users>();
            //BindingCollection<Users> lu = iu.GetSortdata_IoRyClass(sql);
            //this.dataGridView1.DataSource = lu;
            this.dataGridView1.DataSource = IoRyFunction.IC.GetTable(sql);
            this.LastSql = sql;
        }

        private void testForm1_Load(object sender, EventArgs e)
        {
            //绑定方式datagridview方式1,createclass产生类集合,绑定,这样需要自己设置中文名
            //绑定方式datagridview方式2,直接通过SQL语句产生datatable,绑定datatable
            //            string sql = string.Format(@"SELECT   index_int as 序号, loginname_str as 登录名, name_str as 姓名, type_str as 用户类型, power_str as 权限, createtime_dt as 创建时间, changetime_dt as 修改时间
            //FROM      dbo.Users ");
            //            IoRyEntity<Users> iu = new CreateDataTableTool.IoRyEntity<CreateDataTableTool.Users>();
            //            BindingCollection<Users> lu = iu.GetSortdata_IoRyClass(sql);
            //            this.dataGridView1.DataSource = lu;
            string sql = string.Format(@"SELECT   index_int as 序号, loginname_str as 登录名, name_str as 姓名, type_str as 用户类型, power_display_str as 权限, createtime_dt as 创建时间, changetime_dt as 修改时间
            FROM      V_user ");
            this.dataGridView1.DataSource = IoRyFunction.IC.GetTable(sql);
            this.LastSql = sql;
            //IoRyWCFClientV5.WCFClientV5的WCF例子
            //IoRyWCFClientV5.WCFClientV5 wc5 = new IoRyWCFClientV5.WCFClientV5(IoRyFunction.mxml, IoRyFunction.cOperator, IoRyFunction.url);
            //wc5.myAsnyGetDataSet += Wc5_myAsnyGetDataSet;
            //wc5.myProgressBar = ((MainForm.MainForm)this.MdiParent).toolStripProgressBar1.ProgressBar;
            //wc5.MyButtons = new List<Button> { this.bt_OK, this.bt_chaxun };
            //wc5.myGetDataSetAsync(sql, null);

            this.dtp_start.Value = DateTime.Now.AddDays(-1);
            this.dtp_end.Value = DateTime.Now.AddDays(1);
            //datagridview虽然设置了protect但是依然不行,如果实在不行的话,这个控件不继承了
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

        }

        //private void Wc5_myAsnyGetDataSet(DataSet DS, object obj)
        //{
        //    this.dataGridView1.DataSource = DS.Tables[0];
        //}

        void freshsql()
        {
            //IoRyWCFClientV5.WCFClientV5 wc5 = new IoRyWCFClientV5.WCFClientV5(IoRyFunction.mxml, IoRyFunction.cOperator, IoRyFunction.url);
            //wc5.myAsnyGetDataSet += Wc5_myAsnyGetDataSet;
            //wc5.myProgressBar = ((MainForm.MainForm)this.MdiParent).toolStripProgressBar1.ProgressBar;
            //wc5.MyButtons = new List<Button> { this.bt_OK, this.bt_chaxun };
            //wc5.myGetDataSetAsync(this.LastSql, null);
            IoRyClass ic = IoRyFunction.IC;
            this.dataGridView1.DataSource = ic.GetTable(this.LastSql);
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string mindex = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string sql = string.Format("select * from users where index_int='{0}'", mindex);
                userChange(sql);
                freshsql();
            }
        }

        private void bt_change_Click(object sender, EventArgs e)
        {
            string mindex = this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            string sql = string.Format("select * from users where index_int='{0}'", mindex);
            userChange(sql);
            freshsql();
        }

        void userChange(string sql)
        {
            IoRyEntity<Users> imu = new IoRyEntity<Users>();
            Users mu = imu.GetData_IoRyClass(sql).First();

            XElement xe = XElement.Load("config\\ClassXML.xml");
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
            XElement xe = XElement.Load("config\\ClassXML.xml");
            var xee = xe.Elements("classform");
            classControlForm.ClassForm cf = new classControlForm.ClassForm(xee.Where(x => x.Element("className").Value == "Users").First());
            cf.AddEvent += Cf_AddEvent;
            cf.RepeatEvent += Cf_RepeatEvent;
            cf.Text = "用户新增";
            cf.ShowDialog();
            freshsql();
        }

        private bool Cf_AddEvent(object ob)
        {
            ((Users)ob).createtime_dt = DateTime.Now;
            return true;
        }

        private bool Cf_RepeatEvent(string sql)
        {
            DataSet ds = IoRyFunction.IC.GetDataSet(sql);
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
