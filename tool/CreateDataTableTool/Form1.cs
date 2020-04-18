using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Security.Cryptography;
using yezhanbafang.fw.Core;

namespace yezhanbafang.fw.ORMTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bt_sqltest_Click(object sender, EventArgs e)
        {
            string cons = string.Format(
                "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                this.tb_ip.Text.Trim(), this.tb_ku.Text.Trim(), this.tb_name.Text.Trim(), this.tb_pw.Text.Trim());
            XElement xe = XElement.Load("constring.xml");
            //string a = xe.Element("type").Value;
            xe.Element("type").Value = "SQL";
            xe.Element("sqlserver").Element("simple").Value = cons;
            xe.Element("sqlserver").Element("ip").Value = this.tb_ip.Text.Trim();
            xe.Element("sqlserver").Element("databasename").Value = this.tb_ku.Text.Trim();
            xe.Element("sqlserver").Element("username").Value = this.tb_name.Text.Trim();
            xe.Element("sqlserver").Element("encryptKey").Value = "ydhydhnb";
            xe.Element("sqlserver").Element("passwordencryption").Value = IoRyClass.EncryptDES(this.tb_pw.Text.Trim(), "ydhydhnb");
            xe.Save("constring.xml");

            IoRyClass ic = new IoRyClass();
            try
            {
                ic.GetTable("select 1");
                MessageBox.Show("连接正常");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void mybind()
        {
            XElement xe = XElement.Load("DataTables.xml");
            var ml = xe.Elements("TableName").Select(x =>
                new { 表名称 = x.Value, 描述 = x.Attribute("description").Value }).ToList();
            this.dgv_tables.DataSource = ml;

            string tname = this.dgv_tables.Rows[0].Cells[0].Value.ToString();
            mycolbind(this.dgv_tables.Rows[0]);
            if (this.dgv_columns.Rows.Count > 0)
            {
                myclickcol(this.dgv_columns.Rows[0]);
            }
        }

        void mycolbind(DataGridViewRow dvr)
        {
            string tablename = dvr.Cells[0].Value.ToString();
            XElement xe = XElement.Load("DataTables.xml");
            var ml = xe.Elements(tablename).Select(x =>
                new
                {
                    列名称 = x.Attribute("columnname").Value,
                    列类型 = x.Attribute("columntype").Value,
                    是否为空 = x.Attribute("isnull").Value,
                    是否自增 = x.Attribute("identity").Value,
                    是否主键 = x.Attribute("iskey").Value
                }).ToList();
            this.dgv_columns.DataSource = ml;

            this.tb_tname.Text = tablename;
            this.tb_tdsp.Text = dvr.Cells[1].Value.ToString();

            //if (this.dgv_columns.Rows.Count > 0)
            //{
            //    myclickcol(this.dgv_columns.Rows[0]);
            //}
        }

        void myclickcol(DataGridViewRow drv)
        {
            this.tb_cname.Text = drv.Cells[0].Value.ToString();
            this.cb_ctype.Text = drv.Cells[1].Value.ToString();
            this.cb_cnull.Text = drv.Cells[2].Value.ToString();
            this.cb_cidentity.Text = drv.Cells[3].Value.ToString();
            this.cb_ckey.Text = drv.Cells[4].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mybind();
            this.cb_calltype.SelectedIndex = 1;
            this.Text += "    " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void dgv_tables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mycolbind(this.dgv_tables.CurrentRow);
        }

        private void bt_addtable_Click(object sender, EventArgs e)
        {

            XElement xe = XElement.Load("DataTables.xml");
            if (xe.Elements("TableName").Any(x => x.Value == this.tb_tname.Text.Trim()))
            {
                MessageBox.Show("表名称重复,插入失败!");
                return;
            }
            xe.Add(new XElement("TableName", new XAttribute("description", this.tb_tdsp.Text.Trim()), this.tb_tname.Text.Trim()));
            xe.Save("DataTables.xml");
            mybind();
        }

        private void bt_deltable_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要删除选定的表吗?", "确定?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string tname = this.dgv_tables.CurrentRow.Cells[0].Value.ToString();
                XElement xe = XElement.Load("DataTables.xml");
                xe.Elements(tname).Remove();
                xe.Elements("TableName").ToList()[this.dgv_tables.CurrentRow.Index].Remove();
                xe.Save("DataTables.xml");
                mybind();
            }

        }

        private void dgv_columns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            myclickcol(this.dgv_columns.CurrentRow);
        }

        private void bt_addcol_Click(object sender, EventArgs e)
        {
            string tname = this.dgv_tables.CurrentRow.Cells[0].Value.ToString();
            XElement xe = XElement.Load("DataTables.xml");
            if (xe.Elements(tname).Any(x => x.Attribute("columnname").Value == this.tb_cname.Text.Trim()))
            {
                MessageBox.Show("列名称重复,插入失败!");
                return;
            }
            xe.Add(new XElement(tname, new XAttribute("columnname", this.tb_cname.Text.Trim()), new XAttribute("columntype", this.cb_ctype.Text.Trim()),
                new XAttribute("identity", this.cb_cidentity.Text.Trim()), new XAttribute("isnull", this.cb_cnull.Text.Trim()),
                new XAttribute("iskey", this.cb_ckey.Text.Trim())));
            xe.Save("DataTables.xml");
            mycolbind(this.dgv_tables.CurrentRow);
        }

        private void bt_delcol_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要删除选定的表吗?", "确定?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string tname = this.dgv_tables.CurrentRow.Cells[0].Value.ToString();
                XElement xe = XElement.Load("DataTables.xml");
                xe.Elements(tname).ToList()[this.dgv_columns.CurrentRow.Index].Remove();
                xe.Save("DataTables.xml");
                mycolbind(this.dgv_tables.CurrentRow);
            }
        }

        private void bt_editcol_Click(object sender, EventArgs e)
        {
            string tname = this.dgv_tables.CurrentRow.Cells[0].Value.ToString();
            XElement xe = XElement.Load("DataTables.xml");
            if (xe.Elements(tname).Any(x => x.Attribute("columnname").Value == this.tb_cname.Text.Trim()))
            {
                MessageBox.Show("列名称重复,插入失败!");
                return;
            }
            var dxe = xe.Elements(tname).ToList()[this.dgv_columns.CurrentRow.Index];
            dxe.Attribute("columnname").Value = this.tb_cname.Text.Trim();
            dxe.Attribute("columntype").Value = this.cb_ctype.Text.Trim();
            dxe.Attribute("identity").Value = this.cb_cidentity.Text.Trim();
            dxe.Attribute("isnull").Value = this.cb_cnull.Text.Trim();
            dxe.Attribute("iskey").Value = this.cb_ckey.Text.Trim();
            xe.Save("DataTables.xml");
            mycolbind(this.dgv_tables.CurrentRow);
        }

        private void bt_adddb_Click(object sender, EventArgs e)
        {
            string sqlsub = "";
            string key = "";
            string tablename = this.dgv_tables.CurrentRow.Cells[0].Value.ToString();

            XElement xe = XElement.Load("DataTables.xml");
            var ml = xe.Elements(tablename).Select(x =>
                new
                {
                    列名称 = x.Attribute("columnname").Value,
                    列类型 = x.Attribute("columntype").Value,
                    是否为空 = x.Attribute("isnull").Value,
                    是否自增 = x.Attribute("identity").Value,
                    是否主键 = x.Attribute("iskey").Value
                }).ToList();

            foreach (var item in ml)
            {
                sqlsub += "[" + item.列名称 + "] ";
                sqlsub += item.列类型 + " ";
                if (item.是否自增 == "是")
                {
                    sqlsub += "IDENTITY(1,1) ";
                }
                if (item.是否为空 == "是")
                {
                    sqlsub += "NULL,";
                }
                else
                {
                    sqlsub += "NOT NULL,";
                }
                if (item.是否主键 == "是")
                {
                    key = item.列名称;
                }
            }

            string sql = string.Format(@"CREATE TABLE [{0}](
{1}
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[{2}] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]", tablename, sqlsub, key);

            IoRyClass ic = new IoRyClass();
            try
            {
                ic.ExecuteSql(sql);
                MessageBox.Show("创建表成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_create_Click(object sender, EventArgs e)
        {
            try
            {
                //删除文件夹
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CreateClass"))
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "CreateClass", true);
                }

                IoRyClass ic = new IoRyClass();
                common.inamespace = this.tb_namespace.Text.Trim();
                common.prefix = this.tb_prefix.Text.Trim();
                common.calltype = this.cb_calltype.Text.Trim();
                common.IoRyClassXML = this.tb_icxml.Text.Trim();
                common.WCFIPport = this.tb_wcfxml.Text.Trim();
                common.WebAPIURL = this.tb_webapi.Text.Trim();
                common.create(ic);
                MessageBox.Show("生成成功!");
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }

        private void bt_createxml_Click(object sender, EventArgs e)
        {
            string sql = @"
select b.name as tablename,a.name as cname,c.name as ctype,COLUMNPROPERTY( a.id,a.name,'IsIdentity') as IsIdentity,
 (case when (SELECT count(*) FROM sysobjects  
 WHERE (name in (SELECT name FROM sysindexes  
 WHERE (id = a.id) AND (indid in  
 (SELECT indid FROM sysindexkeys  
 WHERE (id = a.id) AND (colid in  
 (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  
 AND (xtype = 'PK'))>0 then 'true' else 'false' end) ckey,
 a.isnullable,b.xtype ,g.value as gnote from syscolumns a
join sysobjects b on a.id=b.id
join systypes c on a.xtype=c.xusertype
left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id
 where a.id in (select id from sysobjects where xtype in ('U'))
 order by a.id,a.colorder";
            IoRyClass ic = new IoRyClass();
            DataTable dt = ic.GetTable(sql);
            XElement xe = new XElement("classforms");
            foreach (var item in dt.AsEnumerable().Select(x=>x.Field<string>("tablename")).Distinct())
            {
                XElement xezi = new XElement("classform");
                xezi.Add(new XElement("className", item));
                int hang = 1;
                int lie = 1;
                foreach (var itemzi in dt.AsEnumerable().Where(x => x.Field<string>("tablename") == item))
                {
                    //这里控制行列,目前一列最多20个 以后可以改,目前这个也可以直接改xml文件
                    if (hang > 20)
                    {
                        lie++;
                        hang = hang - 20;
                    }

                    if (itemzi.Field<string>("gnote") != null && itemzi.Field<string>("gnote") != "")
                    {
                        XElement xezizi = new XElement("Property");
                        xezizi.Add(new XElement("column", lie));

                        xezizi.Add(new XElement("row", hang));
                        hang++;
                        xezizi.Add(new XElement("height", this.txt_height.Text));
                        xezizi.Add(new XElement("width", this.txt_width.Text));
                        xezizi.Add(new XElement("textstart", this.txt_textstart.Text));
                        xezizi.Add(new XElement("textwidth", this.txt_textwidth.Text));
                        xezizi.Add(new XElement("name", itemzi.Field<string>("cname")));
                        xezizi.Add(new XElement("displayname", itemzi.Field<string>("gnote")));
                        xezizi.Add(new XElement("valuetype", common.igetctype(itemzi.Field<string>("ctype"))));
                        xezizi.Add(new XElement("isPassword"), "");
                        xezizi.Add(new XElement("checkRepeat"), "");
                        xezizi.Add(new XElement("comboTable"), "");
                        xezizi.Add(new XElement("comboDisplay"), "");
                        xezizi.Add(new XElement("comboValue"), "");
                        xezizi.Add(new XElement("comboDefault"), "");
                        xezi.Add(xezizi);
                    }
                }
                xe.Add(xezi);
            }
            //删除文件夹
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CXML"))
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "CXML", true);
            }
            //创建文件夹
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CXML"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "CXML");
            }
            try
            {
                xe.Save(AppDomain.CurrentDomain.BaseDirectory + "CXML\\ClassXML.xml");
                MessageBox.Show("生成成功!");
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }





        /*
         SELECT (case when a.colorder=1 then d.name else null end) 表名,  
 a.colorder 字段序号,a.name 字段名,
 (case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end) 标识, 
 (case when (SELECT count(*) FROM sysobjects  
 WHERE (name in (SELECT name FROM sysindexes  
 WHERE (id = a.id) AND (indid in  
 (SELECT indid FROM sysindexkeys  
 WHERE (id = a.id) AND (colid in  
 (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  
 AND (xtype = 'PK'))>0 then '√' else '' end) 主键,b.name 类型,a.length 占用字节数,  
 COLUMNPROPERTY(a.id,a.name,'PRECISION') as 长度,  
 isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as 小数位数,(case when a.isnullable=1 then '√'else '' end) 允许空,  
 isnull(e.text,'') 默认值,isnull(g.[value], ' ') AS [说明]
 FROM  syscolumns a 
 left join systypes b on a.xtype=b.xusertype  
 inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
 left join syscomments e on a.cdefault=e.id  
 left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id
 left join sys.extended_properties f on d.id=f.class and f.minor_id=0
 where b.name is not null
 --WHERE d.name='要查询的表' --如果只查询指定表,加上此条件
 order by a.id,a.colorder
         * */
        //上面的注释为sqlserver查看表结构的sql语句
        //经过思考,认为目前需要用的字段有,
        //类型->决定类字段的类型,是否为空->决定插入函数的判断,是否自增->决定插入函数的判断
        //关于是否需要主键,目前觉得不需要,因为在修改的时候,并不一定要用到主键,所以在用修改和删除函数时注意where条件
    }
}
