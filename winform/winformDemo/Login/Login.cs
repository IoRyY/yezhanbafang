using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace yezhanbafang.fw.winform.Demo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        void login()
        {
            //if (txt_username.Text == "")
            //{
            //    MessageBox.Show("用户名不能为空!");
            //    return;
            //}
            //if (txt_pwd.Text == "")
            //{
            //    MessageBox.Show("密码不能为空!");
            //    return;
            //}
            try
            {
                string Name = txt_username.Text.Replace("\'", "\'\'");
                string pass = txt_pwd.Text.Replace("\'", "\'\'");



                //如果是数据库直连,这里就直接就行
                //如果是WCF方式,参考下面注释代码

                //                IoRyWCFClient.WCFClient wl = new IoRyWCFClient.WCFClient(Program.myxml);
                //                wl.myProgressBar = this.progressBar1;
                //                this.label1.Enabled = false;
                //                wl.myAsnyGetDataSet += new IoRyWCFClient.AsnyGetDataSet(wl_myAsnyGetDataSet);
                //                string sql = string.Format(@"select * from Hospital_UsersInfo  
                //where [name]='{0}' and password='{1}'", Name, pass);
                //                wl.myGetDataSetAsync(sql, null);

                userClass uc = new userClass();
                uc.name_str = "admin";
                //这里给用户的东西复制,以便后面可以直接调用
                Program.uClass = uc;

                MainForm.MainForm mm = new MainForm.MainForm();
                mm.Show();
                this.Hide();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误信息");
                return;
            }
        }

        //void wl_myAsnyGetDataSet(DataSet DS, object obj)
        //{
        //    if (DS.Tables[0].Rows.Count > 0)
        //    {
        //        NamlMainClass.LegReSende();
        //        Program.HospitalID = DS.Tables[0].Rows[0]["Hos_ID"].ToString();
        //        Program.Power = Convert.ToString(DS.Tables[0].Rows[0]["Power"]);
        //        Program.UserName = this.txt_username.Text.Replace('\'', '"');
        //        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsExcelOut"]))
        //        {
        //            Naml.ExcelOut.Excelout el = new ExcelOut.Excelout();
        //            el.Show();
        //            this.Hide();
        //        }
        //        else
        //        {
        //            Cover myc = new Cover();
        //            myc.Show();
        //            this.Hide();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("用户名,密码错误!");
        //        this.txt_pwd.Text = "";
        //    }
        //    this.label1.Enabled = true;
        //}

        private void label1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.lb_v.Text = "v:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.lb_copyright.Text = System.Configuration.ConfigurationManager.AppSettings["copyright"];
            this.lb_title.Text = System.Configuration.ConfigurationManager.AppSettings["programName"];
            this.Text = System.Configuration.ConfigurationManager.AppSettings["programName"];
        }
    }
}
