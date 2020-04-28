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

namespace yezhanbafang.fw.winform.selectControl
{
    /// <summary>
    /// QueryDelegate
    /// </summary>
    /// <param name="ob"></param>
    public delegate void QueryDelegate(string sql);

    public partial class QueryForm : Form
    {
        public event QueryDelegate QueryEvent;
        public QueryForm(InitType inittype, string xml)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                switch (inittype)
                {
                    case InitType.XmlPath:
                        this.c2015QueryS1.XmlPath = xml;
                        break;
                    case InitType.XmlString:
                        this.c2015QueryS1.XmlString = xml;
                        break;
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = this.c2015QueryS1.CreatSql();
            if (this.QueryEvent != null)
            {
                this.QueryEvent(q);
            }
            this.Close();
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            this.Text = "复杂查询窗体" + "  " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
    public enum InitType
    {
        XmlPath = 1,
        XmlString = 2
    }
}
