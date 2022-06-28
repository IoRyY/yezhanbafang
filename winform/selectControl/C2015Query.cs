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
    public partial class C2015Query : UserControl
    {
        public C2015Query()
        {
            InitializeComponent();
        }

        string _xmlpath = "";
        string _xmlstring = "";

        private void C2015Query_Load(object sender, EventArgs e)
        {
            this.cb_ys.SelectedIndex = 0;
            this.cb_gx.Items.Add("并且");
            this.cb_gx.Items.Add("或者");
            this.cb_gx.SelectedIndex = 0;
        }

        public void myset(bool IsFirst)
        {
            if (IsFirst)
            {
                this.cb_gx.Items.Clear();
                this.cb_gx.Items.Add("无");
                this.cb_gx.SelectedIndex = 0;
            }
        }

        public void XmlPath(string path)
        {
            _xmlpath = path;
            XElement xe = XElement.Load(_xmlpath);
            this.cb_zd.DisplayMember = "mdisplay";
            this.cb_zd.ValueMember = "mvalue";
            this.cb_zd.DataSource = xe.Elements("MyQuery").Select(x => new { mdisplay = x.Attribute("display").Value, mvalue = x.Attribute("value").Value }).ToList();
        }

        public void XmlString(string xmlstring)
        {
            _xmlstring = xmlstring;
            XElement xe = XElement.Parse(xmlstring);
            this.cb_zd.DisplayMember = "mdisplay";
            this.cb_zd.ValueMember = "mvalue";
            this.cb_zd.DataSource = xe.Elements("MyQuery").Select(x => new { mdisplay = x.Attribute("display").Value, mvalue = x.Attribute("value").Value }).ToList();
        }

        public string GetSqlwhere()
        {
            string zhi = "";
            if (mtype == "datetime")
            {
                zhi = this.dtp_nr.Value.ToString();
            }
            else
            {
                zhi = this.tb_nr.Text.Replace("'", "''");
            }
            if (this.cb_gx.Text == "无")
            {
                if (this.cb_ys.Text == "包含" || this.cb_ys.Text == "不包含")
                {
                    return this.cb_zd.SelectedValue + getys() + "'%" + zhi + "%' ";
                }
                else
                {
                    return this.cb_zd.SelectedValue + getys() + "'" + zhi + "' ";
                }
            }
            else if (this.cb_gx.Text == "并且")
            {
                if (this.cb_ys.Text == "包含" || this.cb_ys.Text == "不包含")
                {
                    return " and " + this.cb_zd.SelectedValue + getys() + "'%" + zhi + "%' ";
                }
                else
                {
                    return " and " + this.cb_zd.SelectedValue + getys() + "'" + zhi + "' ";
                }
            }
            else
            {
                if (this.cb_ys.Text == "包含" || this.cb_ys.Text == "不包含")
                {
                    return " or " + this.cb_zd.SelectedValue + getys() + "'%" + zhi + "%' ";
                }
                else
                {
                    return " or " + this.cb_zd.SelectedValue + getys() + "'" + zhi + "' ";
                }
            }

        }

        string getys()
        {
            switch (this.cb_ys.Text)
            {
                case "等于":
                    return " = ";
                case "不等于":
                    return " <> ";
                case "大于":
                    return " > ";
                case "小于":
                    return " < ";
                case "包含":
                    return " like ";
                case "不包含":
                    return " not like ";
            }
            return "";
        }

        string mtype = "";

        private void cb_zd_SelectedIndexChanged(object sender, EventArgs e)
        {
            XElement xe = null;
            if (this._xmlpath == null || this._xmlpath == "")
            {
                xe = XElement.Parse(_xmlstring);
            }
            else
            {
                xe = XElement.Load(_xmlpath);
            }
            var rst = xe.Elements("MyQuery").Where(x => x.Attribute("display").Value == this.cb_zd.Text && x.Attribute("value").Value == this.cb_zd.SelectedValue.ToString()).
                Select(x => x.Attribute("type").Value);
            if (rst.Count() == 1)
            {
                mtype = rst.First();
                switch (rst.First())
                {
                    case "string":
                    case "int":
                    case "decimal":
                        this.tb_nr.Visible = true; ;
                        this.dtp_nr.Visible = false;
                        break;
                    case "datetime":
                        this.tb_nr.Visible = false;
                        this.dtp_nr.Visible = true;
                        this.dtp_nr.Location = new Point(330, 4);
                        break;
                }
            }
        }

        private void tb_nr_TextChanged(object sender, EventArgs e)
        {
            if (this.tb_nr.Text.Trim() != "")
            {
                if (mtype == "int")
                {
                    int a;
                    if (!int.TryParse(this.tb_nr.Text, out a))
                    {
                        MessageBox.Show(this.cb_zd.Text + "字段只能输入整数!");
                        this.tb_nr.Text = "";
                        return;
                    }
                }
                else if (mtype == "decimal")
                {
                    decimal b;
                    if (!decimal.TryParse(this.tb_nr.Text, out b))
                    {
                        MessageBox.Show(this.cb_zd.Text + "字段只能输入数字!");
                        this.tb_nr.Text = "";
                        return;
                    }
                }
            }
        }
    }
}
