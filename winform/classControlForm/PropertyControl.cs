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
    public partial class PropertyControl : UserControl, PropertyInterface
    {
        public string PropertyType { get; set; }

        public PropertyControl()
        {
            InitializeComponent();
        }

        public PropertyControl(XElement pxml)
        {
            InitializeComponent();
            this.Width = Convert.ToInt32(pxml.Element("width").Value);
            this.Height = Convert.ToInt32(pxml.Element("height").Value);
            this.textBox1.Width = Convert.ToInt32(pxml.Element("textwidth").Value);
            this.textBox1.Left = Convert.ToInt32(pxml.Element("textstart").Value);
            this.label1.Text = pxml.Element("displayname").Value;
            this.toolTip1.SetToolTip(this.label1, pxml.Element("displayname").Value);
            this.PropertyType = pxml.Element("valuetype").Value;
            this.PropertyName = pxml.Element("name").Value;

            if (pxml.Element("isPassword").Value == "true")
            {
                this.textBox1.PasswordChar = '*';
            }
        }

        public string PropertyName { get; set; }

        public string PropertyValue
        {
            get
            {
                if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
                {
                    switch (this.PropertyType)
                    {
                        case "string":
                            return this.textBox1.Text;
                        case "int?":
                            int inta;
                            if (int.TryParse(this.textBox1.Text, out inta))
                            {
                                return inta.ToString();
                            }
                            else
                            {
                                throw new Exception(this.label1.Text + " 在转换为整数时出现错误,请填写整数!");
                            }
                        case "decimal?":
                        case "double?":
                            decimal shuzi;
                            if (decimal.TryParse(this.textBox1.Text, out shuzi))
                            {
                                return shuzi.ToString();
                            }
                            else
                            {
                                throw new Exception(this.label1.Text + " 在转换为数字时出现错误,请填写数字!");
                            }
                        default:
                            throw new Exception(this.label1.Text + "报错,找不到此类型!");
                    }
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
    }
}
