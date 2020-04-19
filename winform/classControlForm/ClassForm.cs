using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace yezhanbafang.fw.winform.classControlForm
{
    /// <summary>
    /// ClassFormDelegate
    /// </summary>
    /// <param name="ob"></param>
    /// <returns>返回false则停止运行</returns>
    public delegate bool ClassFormDelegate(object ob);


    /// <summary>
    /// RepeatDelegate
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>返回false则停止运行</returns>
    public delegate bool RepeatDelegate(string sql);

    public partial class ClassForm : Form
    {
        public event ClassFormDelegate AddEvent;
        public event ClassFormDelegate UpdateEvent;
        public event RepeatDelegate RepeatEvent;
        /// <summary>
        /// PropertyControl的list
        /// </summary>
        List<PropertyInterface> lpc = new List<PropertyInterface>();

        /// <summary>
        /// 临时存储类
        /// </summary>
        object bj = null;

        /// <summary>
        /// 类的名称
        /// </summary>
        string classname = null;

        /// <summary>
        /// 传入的XElement
        /// </summary>
        XElement xel = null;

        /// <summary>
        /// 重复报错的repeatDisplayName
        /// </summary>
        string repeatDisplayName = null;

        /// <summary>
        /// update检测重复用的list
        /// </summary>
        SortedList<string, string> updatelist = null;

        /// <summary>
        /// 日志用户
        /// </summary>
        string logUser = null;

        /// <summary>
        /// 普通新增
        /// </summary>
        /// <param name="cxml">xml</param>
        public ClassForm(XElement cxml)
        {
            InitializeComponent();
            xel = cxml;
            Assembly assembly = Assembly.GetEntryAssembly(); // 获取调用此dll的程序集 
            bj = assembly.CreateInstance("yezhanbafang.fw." + cxml.Element("className").Value);
            if (bj == null)
            {
                throw new Exception("创建[" + cxml.Element("className").Value + "]实例失败,请查询是否存在此表或者视图,或者CreateClass是否创建!");
            }
            this.coreCreate(cxml, "init");

            this.bt_update.Enabled = false;
            this.bt_del.Enabled = false;
        }

        /// <summary>
        /// 普通新增带log
        /// </summary>
        /// <param name="cxml">xml</param>
        /// <param name="loguser">操作者</param>
        public ClassForm(XElement cxml,string loguser)
        {
            InitializeComponent();
            xel = cxml;
            Assembly assembly = Assembly.GetEntryAssembly(); // 获取调用此dll的程序集 
            bj = assembly.CreateInstance("yezhanbafang.fw." + cxml.Element("className").Value);
            if (bj == null)
            {
                throw new Exception("创建[" + cxml.Element("className").Value + "]实例失败,请查询是否存在此表或者视图,或者CreateClass是否创建!");
            }
            this.coreCreate(cxml, "init");

            this.bt_update.Enabled = false;
            this.bt_del.Enabled = false;
            this.logUser = loguser;
        }

        /// <summary>
        /// 普通更新
        /// </summary>
        /// <param name="cxml"></param>
        /// <param name="obj"></param>
        public ClassForm(XElement cxml, object obj)
        {
            InitializeComponent();
            xel = cxml;
            bj = obj;
            this.coreCreate(cxml, "fill");

            this.bt_add.Enabled = false;
        }

        /// <summary>
        /// 普通更新带log
        /// </summary>
        /// <param name="cxml">xml</param>
        /// <param name="obj">要更新的类</param>
        /// <param name="loguser">操作者</param>
        public ClassForm(XElement cxml, object obj, string loguser)
        {
            InitializeComponent();
            xel = cxml;
            bj = obj;
            this.coreCreate(cxml, "fill");

            this.bt_add.Enabled = false;
            this.logUser = loguser;
        }

        /// <summary>
        /// 可以设置更新和删除按钮的,有的地方只能跟新不能删除.
        /// </summary>
        /// <param name="cxml">xml</param>
        /// <param name="obj">要更新的类</param>
        /// <param name="enableUpdate">是否能更新</param>
        /// <param name="enableDelete">是否能删除</param>
        public ClassForm(XElement cxml, object obj, bool enableUpdate, bool enableDelete)
        {
            InitializeComponent();
            xel = cxml;
            bj = obj;
            this.coreCreate(cxml, "fill");

            this.bt_add.Enabled = false;
            this.bt_update.Enabled = enableUpdate;
            this.bt_del.Enabled = enableDelete;
        }

        /// <summary>
        /// 带日志的,可以设置更新和删除按钮的,有的地方只能跟新不能删除.
        /// </summary>
        /// <param name="cxml">xml</param>
        /// <param name="obj">要更新的类</param>
        /// <param name="enableUpdate">是否能更新</param>
        /// <param name="enableDelete">是否能删除</param>
        /// <param name="loguser">操作者</param>
        public ClassForm(XElement cxml, object obj, bool enableUpdate, bool enableDelete, string loguser)
        {
            InitializeComponent();
            xel = cxml;
            bj = obj;
            this.coreCreate(cxml, "fill");

            this.bt_add.Enabled = false;
            this.bt_update.Enabled = enableUpdate;
            this.bt_del.Enabled = enableDelete;
            this.logUser = loguser;
        }

        void coreCreate(XElement cxml, string mtype)
        {
            classname = cxml.Element("className").Value;
            var ps = cxml.Elements("Property");
            int rowmax = ps.Select(x => Convert.ToInt32(x.Element("row").Value)).Max();
            int colmax = ps.Select(x => Convert.ToInt32(x.Element("column").Value)).Max();
            int widthmax = ps.Select(x => Convert.ToInt32(x.Element("width").Value)).Max();
            int heightmax = ps.Select(x => Convert.ToInt32(x.Element("height").Value)).Max();
            this.Width = colmax * widthmax + 100;
            this.Height = rowmax * heightmax + 200;
            this.groupBox1.Width = colmax * widthmax + 60;
            this.groupBox1.Height = rowmax * heightmax + 60;

            updatelist = new SortedList<string, string>();

            for (int i = 1; i <= colmax; i++)
            {
                var pscol = ps.Where(x => x.Element("column").Value == i.ToString());
                for (int j = 1; j <= rowmax; j++)
                {

                    var pscolrowList = pscol.Where(x => x.Element("row").Value == j.ToString());
                    if (pscolrowList.Count() > 0)
                    {
                        var pscolrow = pscolrowList.First();
                        //DateTime是一种控件
                        if (pscolrow.Element("valuetype").Value.Contains("DateTime"))
                        {
                            PropertyControl_dt pc = new PropertyControl_dt(pscolrow);
                            lpc.Add(pc);
                            this.groupBox1.SuspendLayout();
                            this.SuspendLayout();
                            this.groupBox1.Controls.Add(pc);

                            pc.Left = 12 + (i - 1) * pc.Width;
                            pc.Top = 30 + (j - 1) * pc.Height;

                            if (mtype == "fill")
                            {
                                string mvalue = Convert.ToString(bj.GetType().GetProperty(pc.PropertyName).GetValue(bj, null));
                                if (mvalue != null)
                                {
                                    pc.PropertyValue = mvalue;
                                }

                                updatelist.Add(pc.PropertyName, pc.PropertyValue);
                            }
                        }
                        else
                        {
                            //包含comboTable的是一种控件
                            if (pscolrow.Element("comboTable") != null && pscolrow.Element("comboTable").Value.Trim() != "")
                            {
                                PropertyControl_cb pc = new PropertyControl_cb(pscolrow);
                                lpc.Add(pc);
                                this.groupBox1.SuspendLayout();
                                this.SuspendLayout();
                                this.groupBox1.Controls.Add(pc);

                                pc.Left = 12 + (i - 1) * pc.Width;
                                pc.Top = 30 + (j - 1) * pc.Height;

                                if (mtype == "fill")
                                {
                                    string mvalue = Convert.ToString(bj.GetType().GetProperty(pc.PropertyName).GetValue(bj, null));
                                    if (mvalue != null)
                                    {
                                        pc.PropertyValue = mvalue;
                                    }

                                    updatelist.Add(pc.PropertyName, pc.PropertyValue);
                                }
                            }
                            //其他的是标准控件
                            else
                            {
                                PropertyControl pc = new PropertyControl(pscolrow);
                                lpc.Add(pc);
                                this.groupBox1.SuspendLayout();
                                this.SuspendLayout();
                                this.groupBox1.Controls.Add(pc);

                                pc.Left = 12 + (i - 1) * pc.Width;
                                pc.Top = 30 + (j - 1) * pc.Height;

                                if (mtype == "fill")
                                {
                                    string mvalue = Convert.ToString(bj.GetType().GetProperty(pc.PropertyName).GetValue(bj, null));
                                    if (mvalue != null)
                                    {
                                        pc.PropertyValue = mvalue;
                                    }

                                    updatelist.Add(pc.PropertyName, pc.PropertyValue);
                                }
                            }
                        }

                        this.groupBox1.ResumeLayout(false);
                        this.ResumeLayout(false);
                    }
                }
            }
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkrepeat())
                {
                    MessageBox.Show("数据库中的[" + repeatDisplayName + "]中已经存在您输入的值," + repeatDisplayName + "在数据库中不能重复,请输入其他值!");
                    return;
                }
                foreach (var item in lpc)
                {
                    if (item.PropertyValue != null)
                    {
                        switch (item.PropertyType)
                        {
                            case "string":
                                bj.GetType().GetProperty(item.PropertyName).SetValue(bj, item.PropertyValue, null);
                                break;
                            case "int?":
                                bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToInt32(item.PropertyValue), null);
                                break;
                            case "decimal?":
                                bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDecimal(item.PropertyValue), null);
                                break;
                            case "double?":
                                bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDouble(item.PropertyValue), null);
                                break;
                            case "DateTime?":
                            case "DateTime":
                                bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDateTime(item.PropertyValue), null);
                                break;
                            default:
                                throw new Exception(item.PropertyName + "报错,找不到此类型!");
                        }
                    }
                    //bj.GetType().GetProperty(item.PropertyName).SetValue(bj, item.PropertyValue, null);

                }
                if (this.AddEvent != null)
                {
                    bool jg = this.AddEvent(bj);
                    if (!jg)
                    {
                        return;
                    }
                }
                if (this.logUser == null)
                {
                    bj.GetType().GetMethod("IoRyAdd", new Type[] { }).Invoke(bj, null);
                }
                else
                {
                    bj.GetType().GetMethod("IoRyAdd", new Type[] { typeof(string) }).Invoke(bj, new object[] { this.logUser });
                }

                MessageBox.Show("新增成功!");
                this.Close();
                this.Dispose();
            }
            catch (Exception me)
            {
                string exmsg = "";
                while (me.InnerException != null)
                {
                    exmsg += me.Message + " ------> ";
                    me = me.InnerException;
                }
                exmsg += me.Message;
                MessageBox.Show(exmsg);
            }

        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认?", "确认要修改吗?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (checkrepeat())
                    {
                        MessageBox.Show("数据库中的[" + repeatDisplayName + "]中已经存在您输入的值," + repeatDisplayName + "在数据库中不能重复,请输入其他值!");
                        return;
                    }
                    foreach (var item in lpc)
                    {
                        if (item.PropertyValue != null)
                        {
                            switch (item.PropertyType)
                            {
                                case "string":
                                    bj.GetType().GetProperty(item.PropertyName).SetValue(bj, item.PropertyValue, null);
                                    break;
                                case "int?":
                                    bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToInt32(item.PropertyValue), null);
                                    break;
                                case "decimal?":
                                    bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDecimal(item.PropertyValue), null);
                                    break;
                                case "double?":
                                    bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDouble(item.PropertyValue), null);
                                    break;
                                case "DateTime?":
                                case "DateTime":
                                    bj.GetType().GetProperty(item.PropertyName).SetValue(bj, Convert.ToDateTime(item.PropertyValue), null);
                                    break;
                                default:
                                    throw new Exception(item.PropertyName + "报错,找不到此类型!");
                            }
                            //bj.GetType().GetProperty(item.PropertyName).SetValue(bj, item.PropertyValue, null);
                        }
                        else
                        {
                            bj.GetType().GetProperty(item.PropertyName).SetValue(bj, null, null);
                        }
                    }
                    if (this.UpdateEvent != null)
                    {
                        bool jg = this.UpdateEvent(bj);
                        if (!jg)
                        {
                            return;
                        }
                    }
                    if (this.logUser == null)
                    {
                        bj.GetType().GetMethod("IoRyUpdate", new Type[] { }).Invoke(bj, null);
                    }
                    else
                    {
                        bj.GetType().GetMethod("IoRyUpdate", new Type[] { typeof(string) }).Invoke(bj, new object[] { this.logUser });
                    }
                    MessageBox.Show("修改成功!");
                    this.Close();
                    this.Dispose();
                }
                catch (Exception me)
                {
                    string exmsg = "";
                    while (me.InnerException != null)
                    {
                        exmsg += me.Message + " ------> ";
                        me = me.InnerException;
                    }
                    exmsg += me.Message;
                    MessageBox.Show(exmsg);
                }
            }
        }

        private void bt_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认?", "确认要删除吗?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (this.logUser == null)
                    {
                        bj.GetType().GetMethod("IoRyDelete", new Type[] { }).Invoke(bj, null);
                    }
                    else
                    {
                        bj.GetType().GetMethod("IoRyDelete", new Type[] { typeof(string) }).Invoke(bj, new object[] { this.logUser });
                    }
                    MessageBox.Show("删除成功!");
                    this.Close();
                    this.Dispose();
                }
                catch (Exception me)
                {
                    string exmsg = "";
                    while (me.InnerException != null)
                    {
                        exmsg += me.Message + " ------> ";
                        me = me.InnerException;
                    }
                    exmsg += me.Message;
                    MessageBox.Show(exmsg);
                }
            }
        }

        bool checkrepeat(string mProperty, string mvalue)
        {
            string sql = string.Format("select * from {0} where {1} ='{2}'", classname, mProperty, mvalue);
            //if (mic == null)
            //{
            //    throw new Exception("检查重复功能必须要设置IoRyNP.IoRyClass!");
            //}
            //DataTable dt = mic.GetTable(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            if (this.RepeatEvent == null)
            {
                throw new Exception("检查重复功能必须要设置RepeatEvent!");
            }
            return this.RepeatEvent(sql);
        }

        bool checkrepeat()
        {
            foreach (var item in xel.Elements("Property"))
            {
                if (item.Element("checkRepeat").Value == "true")
                {

                    string mp = item.Element("name").Value;
                    string mv = lpc.Where(x => x.PropertyName == mp).First().PropertyValue;
                    if (updatelist.Count != 0)
                    {
                        if (updatelist[mp] != mv)
                        {
                            if (checkrepeat(mp, mv))
                            {
                                repeatDisplayName = item.Element("displayname").Value;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (checkrepeat(mp, mv))
                        {
                            repeatDisplayName = item.Element("displayname").Value;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void ClassForm_Load(object sender, EventArgs e)
        {
            this.groupBox1.Text += "    " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
