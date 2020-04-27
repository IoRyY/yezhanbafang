using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo.MainForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = System.Configuration.ConfigurationManager.AppSettings["programName"];
            this.lb_appname.Text = System.Configuration.ConfigurationManager.AppSettings["programName"];
            this.toolStripStatusLabel1.Text = "Version:" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.treeView1.ExpandAll();

            this.lb_user.Text = "用户:" + Convert.ToString(Program.uClass.name_str);

            bg b1 = new bg();
            b1.MdiParent = this;
            b1.Show();
            b1.Dock = DockStyle.Fill;

            //如果是用的WCF方式加滚动条,可以把toolStripProgressBar1传递给子窗体,用来显示滚动条.
        }

        test.testForm1 _mytform = null;

        public test.testForm1 MyTform
        {
            get
            {
                if (_mytform == null)
                {
                    _mytform = new test.testForm1();
                    _mytform.MdiParent = this;
                    _mytform.Dock = DockStyle.Fill;

                }
                return _mytform;
            }
        }

        forms.PCconfig _PCcon = null;
        public forms.PCconfig MyPCcon
        {
            get
            {
                if (_PCcon == null)
                {
                    _PCcon = new forms.PCconfig();
                    _PCcon.MdiParent = this;
                    _PCcon.Dock = DockStyle.Fill;

                }
                return _PCcon;
            }
        }

        forms.logdata _logdata = null;
        public forms.logdata MyLog
        {
            get
            {
                if (_logdata == null)
                {
                    _logdata = new forms.logdata();
                    _logdata.MdiParent = this;
                    _logdata.Dock = DockStyle.Fill;

                }
                return _logdata;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(e.Node.Name);
            switch (e.Node.Name)
            {
                case "节点0":
                    //MessageBox.Show(this.Height.ToString() + "  " + this.Width.ToString());
                    break;
                case "节点1":
                    this.lb_formname.Text = "用户管理";

                    MyTform.Show();
                    MyTform.Activate();

                    break;
                case "节点2":
                    this.lb_formname.Text = "PC配置";

                    MyPCcon.Show();
                    MyPCcon.Activate();

                    break;
                case "节点3":
                    this.lb_formname.Text = "查看Log";

                    MyLog.Show();
                    MyLog.Activate();

                    break;
            }
            
        }





        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
