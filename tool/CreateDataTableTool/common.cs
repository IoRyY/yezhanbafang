using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using yezhanbafang.fw.Core;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2018-1
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * VS版本 2010 2013 2015
 ***********************************************************************************/

/// <summary>
/// 打算加入存储过程的支持,在核心sql语句中加入除了U,V之外,加入P类型,
/// 20200326 增加了存储过程的out型函数的支持
/// </summary>
namespace yezhanbafang.fw.ORMTool
{
    /// <summary>
    /// 这个类是主要的类,生成的工作基本都在此类
    /// </summary>
    public static class common
    {
        #region 预设变量

        public static string inamespace = "CreateDataTableTool";
        public static string prefix = "i";
        public static string calltype = "IoRyClass";
        public static string IoRyClassXML = null;
        public static string WCFIPport = "";
        public static string WebAPIURL = "";

        #endregion

        #region 拼接的字符串 长的字符串都已经用txt的方式保存了,要不会死人的

        static string Table_Tou(string tablename)
        {
            return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2020-3
 * Email windy_23762872@126.com 253625488@qq.com
 * 博客 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * 作用 代码生成器生成的Table类
 * VS版本 2010 2013 2015 2019
 ***********************************************************************************/

namespace " + inamespace + @"
{
    /// <summary>
    /// 自定义前缀+表名称为类名
    /// </summary>
    public class " + prefix + tablename + @" : IoRyRow
    {";

        }

        static string View_Tou(string tablename)
        {
            return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2020-3
 * Email windy_23762872@126.com 253625488@qq.com
 * 博客 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * 作用 代码生成器生成的View类
 * VS版本 2010 2013 2015 2019
 ***********************************************************************************/

namespace " + inamespace + @"
{
    /// <summary>
    /// 自定义前缀+表名称为类名
    /// </summary>
    public class " + prefix + tablename + @" : IoRyView
    {";

        }

        static string Procedure_Tou(string tablename)
        {
            return @"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2018-1
 * Email windy_23762872@126.com 253625488@qq.com
 * 博客 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * 作用 代码生成器生成的View和Table类
 * VS版本 2010 2013 2015
 ***********************************************************************************/

namespace " + inamespace + @"
{
    /// <summary>
    /// 自定义前缀+存储过程名称为类名
    /// </summary>
    public class " + prefix + tablename + @" : IoRyProcedure
    {";

        }

        static string ViewWei = @"
        }
    }
}";
        #endregion

        /// <summary>
        /// 主函数,将目标库中所有的表和视图生成类,并且根据选择生成处理类
        /// </summary>
        /// <param name="iic"></param>
        public static void create(IoRyClass iic)
        {
            //获取数据库中的表,视图信息.
            string sql = @"
select b.name as tablename,a.name as cname,c.name as ctype,COLUMNPROPERTY( a.id,a.name,'IsIdentity') as IsIdentity,
 (case when (SELECT count(*) FROM sysobjects  
 WHERE (name in (SELECT name FROM sysindexes  
 WHERE (id = a.id) AND (indid in  
 (SELECT indid FROM sysindexkeys  
 WHERE (id = a.id) AND (colid in  
 (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  
 AND (xtype = 'PK'))>0 then 'true' else 'false' end) ckey,
 a.isnullable,b.xtype ,g.value as gnote,a.status from syscolumns a
join sysobjects b on a.id=b.id
join systypes c on a.xtype=c.xusertype
left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id
 where a.id in (select id from sysobjects where xtype in ('U','V','P'))
  order by a.id,a.colorder";
            DataTable dt = iic.GetTable(sql);

            //创建文件夹
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CreateClass"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "CreateClass");
            }

            //取得所有的表,视图,存储过程名称
            var ltnames = dt.AsEnumerable().Select(x => x.Field<string>("tablename")).Distinct();
            //遍历所有的表,视图,存储过程
            foreach (var item in ltnames)
            {
                CreatProcedureCS(dt, item);
                CreatViewCS(dt, item);
                CreatTableCS(dt, item);
            }
            //写其他的文件
            string mystr = "";
            switch (calltype)
            {
                case "IoRyClass":
                    //写IoRyFunction类
                    mystr = myRead("IoRyClass\\function1.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    mystr += @"
        public static string IoRyClassXmlPath = " + "\"" + IoRyClassXML + "\"" + @";
";
                    mystr += myRead("IoRyClass\\function2.txt");
                    myWrite(mystr, "IoRyFunction");
                    mystr = myRead("IoRyClass\\col.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyCol");
                    mystr = myRead("IoRyClass\\entity.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyEntity");
                    mystr = myRead("IoRyClass\\rowinterface.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyRowInterface");
                    mystr = myRead("IoRyClass\\SortBindingCollection.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "SortBindingCollection");
                    mystr = myRead("IoRyClass\\IoRyAttribute.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyAttribute");
                    //调试的时候先把生成DLL去了,总是影响跟踪代码
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CreateClass\\DLL"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "CreateClass\\DLL");
                    }
                    System.IO.File.Copy("IoRyClass\\IoRyClass.dll", "CreateClass\\DLL\\IoRyClass.dll", true);
                    System.IO.File.Copy("IoRyClass\\IoRyClass.xml", "CreateClass\\DLL\\IoRyClass.xml", true);
                    System.IO.File.Copy("constring.xml", "CreateClass\\" + IoRyClassXML, true);
                    break;
                case "WCF":
                    //写IoRyFunction类
                    mystr = myRead("WCFv5\\function1.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    mystr += @"
        public static string mxml = " + "\"" + IoRyClassXML + "\";" + @"
        public static string url = " + "\"" + WCFIPport + "\";";
                    mystr += myRead("WCFv5\\function2.txt");
                    myWrite(mystr, "IoRyFunction");
                    mystr = myRead("WCFv5\\col.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyCol");
                    mystr = myRead("WCFv5\\entity.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyEntity");
                    mystr = myRead("WCFv5\\rowinterface.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyRowInterface");
                    mystr = myRead("WCFv5\\SortBindingCollection.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "SortBindingCollection");
                    mystr = myRead("WCFv5\\IoRyAttribute.txt");
                    mystr = mystr.Replace("CreateDataTableTool", common.inamespace);
                    myWrite(mystr, "IoRyAttribute");
                    //调试的时候先把生成DLL去了,总是影响跟踪代码
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "CreateClass\\DLL"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "CreateClass\\DLL");
                    }
                    System.IO.File.Copy("WCFv5\\IoRyClass.dll", "CreateClass\\DLL\\IoRyClass.dll", true);
                    System.IO.File.Copy("WCFv5\\IoRyWCFClientV5.dll", "CreateClass\\DLL\\IoRyWCFClientV5.dll", true);
                    System.IO.File.Copy("WCFv5\\IoRyClass.xml", "CreateClass\\DLL\\IoRyClass.xml", true);
                    System.IO.File.Copy("WCFv5\\IoRyWCFClientV5.xml", "CreateClass\\DLL\\IoRyWCFClientV5.xml", true);
                    break;
                case "WebAPI":

                    break;

            }
        }


        /// <summary>
        /// 生成存储过程
        /// </summary>
        static void CreatProcedureCS(DataTable dtsql, string tablename)
        {
            var zidus = dtsql.AsEnumerable().Where(x => x.Field<string>("tablename") == tablename && x.Field<string>("xtype").Contains("P"));
            if (zidus.Count() == 0)
            {
                return;
            }
            string Zhong = "";
            string zhong2 = @"
        string tablename = " + "\"" + tablename + "\";" + @"

        /// <summary>
        /// LIC是列集合
        /// i前缀+列名为字段名
        /// </summary>
        List<IoRyCol> LIC = new List<IoRyCol>();";
            //处理初始化函数部分
            string Chushihua = @"
        /// <summary>
        /// 初始化函数
        /// </summary>
        public " + prefix + tablename + @"()
        {";

            foreach (var ziduitem in zidus)
            {
                string it = igetctype(ziduitem.Field<string>("ctype"));
                string name = ziduitem.Field<string>("cname").Replace("@", "");
                string mAttribute = ziduitem.Field<string>("gnote");
                //处理属性部分
                Zhong += @"
        " + it + " _" + prefix + name + @";
        /// <summary>
        /// 数据库" + name + @"字段
        /// </summary>
        [IoRyDisPlay(DisplayName =""" + mAttribute + @""")]
        public " + it + " " + prefix + name + @"
        {
            get
            {
                return _" + prefix + name + @";
            }
            set
            {
                _" + prefix + name + @" = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValue = Convert.ToString(value);
            }
        }
";
                //处理初始化函数部分
                Chushihua += @"
            LIC.Add(new IoRyCol
            {
                ioryName = " + "\"" + name + "\"" + @",
                ioryType = " + "\"" + it + "\"" + @",
                IsOut = " + (ziduitem.Field<Byte>("status") == 72).ToString().ToLower() + @",
                IsKey = " + ziduitem.Field<string>("ckey") + @",
                IsNull = " + Convert.ToBoolean(ziduitem.Field<int>("isnullable")).ToString().ToLower() + @",
                ioryValueNull = true,
                ioryValueChange = false
            });";

            }
            //处理初始化函数部分的尾巴
            Chushihua += @"
        }
";
            string wei = "";
            switch (calltype)
            {
                case "IoRyClass":
                    wei = "IoRyClass\\weiProcedure.txt";
                    break;
                case "WCF":
                    wei = "WCFv5\\weiProcedure.txt";
                    break;
                case "WebAPI":
                    wei = "WebAPI\\weiProcedure.txt";
                    break;

            }

            //string lei = Procedure_Tou(tablename) + Zhong + zhong2 + Chushihua + Zhong3 + Zhong4 + Zhong5 + myRead(wei);
            string lei = Procedure_Tou(tablename) + Zhong + zhong2 + Chushihua + myRead(wei);
            myWrite(lei, "Procedure_" + tablename);
        }

        /// <summary>
        /// 生成视图文件
        /// </summary>
        static void CreatViewCS(DataTable dtsql, string tablename)
        {
            var zidus = dtsql.AsEnumerable().Where(x => x.Field<string>("tablename") == tablename && x.Field<string>("xtype").Contains("V"));
            if (zidus.Count() == 0)
            {
                return;
            }
            string Zhong = "";
            string Zhong2 = @"
        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {";
            foreach (var ziduitem in zidus)
            {
                string it = igetctype(ziduitem.Field<string>("ctype"));
                Zhong += @"
        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public " + it + " " + prefix + ziduitem.Field<string>("cname") + @" { get; set; }
";
                Zhong2 += @"
            " + prefix + ziduitem.Field<string>("cname") + " = dr.Field<" + it + ">(\"" + ziduitem.Field<string>("cname") + "\");";
            }

            string lei = View_Tou(tablename) + Zhong + Zhong2 + ViewWei;
            myWrite(lei, "View_" + tablename);
        }

        /// <summary>
        /// 生成表文件
        /// </summary>
        static void CreatTableCS(DataTable dtsql, string tablename)
        {
            var zidus = dtsql.AsEnumerable().Where(x => x.Field<string>("tablename") == tablename && x.Field<string>("xtype").Contains("U"));
            if (zidus.Count() == 0)
            {
                return;
            }
            //处理属性部分
            string Zhong = "";
            //处理接口部分
            string Zhong2 = @"
        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {";
            //处理初始化函数部分
            string Zhong3 = @"
        /// <summary>
        /// 初始化函数
        /// </summary>
        public " + prefix + tablename + @"()
        {";
            foreach (var ziduitem in zidus)
            {
                string it = igetctype(ziduitem.Field<string>("ctype"));
                string name = ziduitem.Field<string>("cname");
                string mAttribute = ziduitem.Field<string>("gnote");

                //处理属性部分
                Zhong += @"
        " + it + " _" + prefix + name + @";
        /// <summary>
        /// 数据库" + name + @"字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="""+ mAttribute + @""")]
        public " + it + " " + prefix + name + @"
        {
            get
            {
                return _" + prefix + name + @";
            }
            set
            {
                _" + prefix + name + @" = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == " + "\"" + name + "\"" + @").First().ioryValue = Convert.ToString(value);
            }
        }
";
                //处理接口部分
                Zhong2 += @"
            " + prefix + name + " = dr.Field<" + it + ">(\"" + name + "\");";

                //处理初始化函数部分
                Zhong3 += @"
            LIC.Add(new IoRyCol
            {
                ioryName = " + "\"" + name + "\"" + @",
                ioryType = " + "\"" + it + "\"" + @",
                IsIdentity = " + Convert.ToBoolean(ziduitem.Field<int>("IsIdentity")).ToString().ToLower() + @",
                IsKey = " + ziduitem.Field<string>("ckey") + @",
                IsNull = " + Convert.ToBoolean(ziduitem.Field<int>("isnullable")).ToString().ToLower() + @",
                ioryValueNull = true,
                ioryValueChange = false
            });";
            }

            //处理接口部分的尾巴
            Zhong2 += @"
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }";
            //处理初始化函数部分的尾巴
            Zhong3 += @"
        }

        string tablename = " + "\"" + tablename + "\"" + ";";

            string wei = "";
            switch (calltype)
            {
                case "IoRyClass":
                    wei = "IoRyClass\\wei.txt";
                    break;
                case "WCF":
                    wei = "WCFv5\\wei.txt";
                    break;
                case "WebAPI":
                    wei = "WebAPI\\wei.txt";
                    break;

            }

            string lei = Table_Tou(tablename) + Zhong + Zhong2 + Zhong3 + myRead(wei);
            myWrite(lei, "Table_" + tablename);
        }

        /// <summary>
        /// 数据库,C#类型对应
        /// </summary>
        /// <param name="sqltype"></param>
        /// <returns></returns>
        public static string igetctype(string sqltype)
        {
            switch (sqltype)
            {
                case "nvarchar":
                case "varchar":
                case "char":
                case "ntext"://老类型 自己设计的数据库别找这个别扭
                    return "string";
                case "datetime":
                case "date":
                    return "DateTime?";
                case "int":
                    return "int?";
                case "tinyint"://网上查说和 Byte 对应,但是极少用到暂时用这个 自己设计的数据库别找这个别扭
                    return "Byte";
                case "decimal":
                case "numeric":
                case "money":
                    return "decimal?";
                case "float":
                    return "double?";
                case "image":
                    return "byte[]";
                case "bigint":
                    return "long?";
                case "uniqueidentifier":
                    return "Guid?";
                default:
                    return "未设置此类型";
            }
        }

        static void myWrite(string content, string tabelname)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "CreateClass\\" + tabelname + ".cs", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }

        static string myRead(string path)
        {
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                return myStr;
            }
        }

    }
}
