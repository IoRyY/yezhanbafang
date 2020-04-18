using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace yezhanbafang.fw.winform.classControlForm
{
    public partial class PropertyControl_dt : UserControl, PropertyInterface
    {
        public string PropertyType { get; set; }

        public PropertyControl_dt()
        {
            InitializeComponent();
        }

        public PropertyControl_dt(XElement pxml)
        {
            InitializeComponent();
            this.Width = Convert.ToInt32(pxml.Element("width").Value);
            this.Height = Convert.ToInt32(pxml.Element("height").Value);
            this.dateTimePicker1.Width = Convert.ToInt32(pxml.Element("textwidth").Value);
            this.dateTimePicker1.Left = Convert.ToInt32(pxml.Element("textstart").Value);
            this.label1.Text = pxml.Element("displayname").Value;
            this.toolTip1.SetToolTip(this.label1, pxml.Element("displayname").Value);
            this.PropertyType = pxml.Element("valuetype").Value;
            this.PropertyName = pxml.Element("name").Value;
            this.dateTimePicker1.Value = DateTime.Now;
            this.checkBox1.Checked = true;
        }

        public string PropertyName { get; set; }

        public string PropertyValue
        {
            get
            {
                if (this.checkBox1.Checked)
                {
                    return this.dateTimePicker1.Value.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value == null || value == "")
                {
                    this.checkBox1.Checked = false;
                    this.dateTimePicker1.Value = new DateTime(3000, 1, 1);
                    this.dateTimePicker1.Enabled = false;
                }
                else
                {
                    DateTime dt;
                    if (DateTime.TryParse(value, out dt))
                    {
                        this.checkBox1.Checked = true;
                        this.dateTimePicker1.Value = dt;
                    }
                    else
                    {
                        throw new Exception(this.label1.Text + "的值不能转换为DateTime");
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                this.dateTimePicker1.Value = new DateTime(3000, 1, 1);
                this.dateTimePicker1.Enabled = false;
            }
        }
    }
}
