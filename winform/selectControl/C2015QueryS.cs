using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace yezhanbafang.fw.winform.selectControl
{
    public partial class C2015QueryS : UserControl
    {

        public string XmlPath { get; set; }
        public string XmlString { get; set; }

        public C2015QueryS()
        {
            InitializeComponent();
        }

        List<C2015Query> LC = new List<C2015Query>();

        private void bt_add_Click(object sender, EventArgs e)
        {
            C2015Query c2 = new C2015Query();
            if (this.XmlPath == null || this.XmlPath == "")
            {
                c2.XmlString(this.XmlString);
            }
            else
            {
                c2.XmlPath(this.XmlPath);
            }
            LC.Add(c2);
            myreset();
        }

        private void bt_del_Click(object sender, EventArgs e)
        {
            if (LC.Count > 0)
            {
                LC[LC.Count - 1].Dispose();
                LC.RemoveAt(LC.Count - 1);
                myreset();
            }
            else
            {
                MessageBox.Show("至少需要一个条件!");
            }
        }

        void myreset()
        {
            this.Height = 60 + LC.Count * 30;
            for (int i = 0; i < LC.Count; i++)
            {
                this.Controls.Add(LC[i]);
                LC[i].Location = new System.Drawing.Point(0, 59 + i * 30);
            }
        }

        private void C2015QueryS_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.c2015Query1.myset(true);
                //this.c2015Query1.XmlPath(this.XmlPath);
                if (this.XmlPath == null || this.XmlPath == "")
                {
                    this.c2015Query1.XmlString(this.XmlString);
                }
                else
                {
                    this.c2015Query1.XmlPath(this.XmlPath);
                }
            }
        }

        public string CreatSql()
        {
            XElement xe = null;
            if (this.XmlPath == null || this.XmlPath == "")
            {
                xe = XElement.Parse(this.XmlString);
            }
            else
            {
                xe = XElement.Load(this.XmlPath);
            }
            //XElement xe = XElement.Load(XmlPath);
            string sql = xe.Elements("Sql").Select(x => x.Attribute("value").Value.ToString()).FirstOrDefault();
            //2017-11-3加入sql的安全监测
            sql = SafeString(sql);
            string sqlwhere = this.c2015Query1.GetSqlwhere();
            foreach (C2015Query item in LC)
            {
                sqlwhere += item.GetSqlwhere();
            }
            string csql = sql + sqlwhere;
            csql = csql + ";";
            return csql;
        }

        public string SafeString(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("--", "");
            str = str.Replace(";", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.ToLower().Replace("insert", "");
            str = str.ToLower().Replace("delete", "");
            str = str.ToLower().Replace("update", "");
            return str;
        }
    }
}
