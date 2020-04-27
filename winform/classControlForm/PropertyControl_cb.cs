using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;

namespace yezhanbafang.fw.winform.classControlForm
{
    public partial class PropertyControl_cb : UserControl, PropertyInterface
    {
        public string PropertyType { get; set; }

        public PropertyControl_cb()
        {
            InitializeComponent();
        }

        public PropertyControl_cb(XElement pxml)
        {
            InitializeComponent();
            this.Width = Convert.ToInt32(pxml.Element("width").Value);
            this.Height = Convert.ToInt32(pxml.Element("height").Value);
            this.comboBox1.Width = Convert.ToInt32(pxml.Element("textwidth").Value);
            this.comboBox1.Left = Convert.ToInt32(pxml.Element("textstart").Value);
            this.label1.Text = pxml.Element("displayname").Value;
            this.toolTip1.SetToolTip(this.label1, pxml.Element("displayname").Value);
            this.PropertyType = pxml.Element("valuetype").Value;
            this.PropertyName = pxml.Element("name").Value;

            string sql = "select * from " + pxml.Element("comboTable").Value.Replace("'", "''");
            Assembly assembly = Assembly.GetEntryAssembly(); // 获取调用此dll的程序集 
            object obj = assembly.CreateInstance("yezhanbafang.IoRyEntity`1[yezhanbafang." + pxml.Element("comboTable").Value + "]");
            if (obj == null)
            {
                throw new Exception("创建[" + pxml.Element("comboTable").Value + "]实例失败,请查询是否存在此表或者视图,或者CreateClass是否创建!");
            }
            object obse = obj.GetType().GetMethod("GetData_IoRyClass", new Type[] { typeof(string) }).Invoke(obj, new object[] { sql });
            //必须先绑定字段,否则会有问题.
            this.comboBox1.DisplayMember = pxml.Element("comboDisplay").Value;
            this.comboBox1.ValueMember = pxml.Element("comboValue").Value;
            this.comboBox1.DataSource = obse;

            if (pxml.Element("comboDefault") != null && pxml.Element("comboDefault").Value.Trim() != "")
            {
                this.comboBox1.SelectedValue = pxml.Element("comboDefault").Value;
                switch (this.PropertyType)
                {
                    case "string":
                        this.comboBox1.SelectedValue = pxml.Element("comboDefault").Value.Trim();
                        break;
                    case "int?":
                        int inta;
                        if (int.TryParse(pxml.Element("comboDefault").Value.Trim(), out inta))
                        {
                            this.comboBox1.SelectedValue = inta;
                            break;
                        }
                        else
                        {
                            throw new Exception(this.label1.Text + " 在转换为整数时出现错误,请填写整数!");
                        }
                    case "decimal?":
                    case "double?":
                        decimal shuzi;
                        if (decimal.TryParse(pxml.Element("comboDefault").Value.Trim(), out shuzi))
                        {
                            this.comboBox1.SelectedValue = shuzi;
                            break;
                        }
                        else
                        {
                            throw new Exception(this.label1.Text + " 在转换为数字时出现错误,请填写数字!");
                        }
                    default:
                        throw new Exception(this.label1.Text + "报错,找不到此类型!");
                }
            }
        }

        public string PropertyName { get; set; }

        public string PropertyValue
        {
            get
            {
                if (this.comboBox1.SelectedValue != null)
                {
                    return this.comboBox1.SelectedValue.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                switch (this.PropertyType)
                {
                    case "string":
                        this.comboBox1.SelectedValue = value;
                        break;
                    case "int?":
                        int inta;
                        if (int.TryParse(value, out inta))
                        {
                            this.comboBox1.SelectedValue = inta;
                            break;
                        }
                        else
                        {
                            throw new Exception(this.label1.Text + " 在转换为整数时出现错误,请填写整数!");
                        }
                    case "decimal?":
                    case "double?":
                        decimal shuzi;
                        if (decimal.TryParse(value, out shuzi))
                        {
                            this.comboBox1.SelectedValue = shuzi;
                            break;
                        }
                        else
                        {
                            throw new Exception(this.label1.Text + " 在转换为数字时出现错误,请填写数字!");
                        }
                    default:
                        throw new Exception(this.label1.Text + "报错,找不到此类型!");
                }
            }
        }

        public string PropertyDisplay
        {
            get
            {
                if (this.comboBox1.Text != null)
                {
                    return this.comboBox1.Text;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.comboBox1.Text = value;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.toolTip2.SetToolTip(this.comboBox1, this.comboBox1.Text);
        }
    }
}
