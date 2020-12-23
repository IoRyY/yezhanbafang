using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Data.Common;
using System.Xml.Linq;
using System.Management;

namespace yezhanbafang.fw.Core
{
    /// <summary>
    /// 袁东辉 2010-11-1日 总结之前项目，提取有用的东西。
    /// 袁东辉 2010-11-11 修改了所有的方法，带path的方法和不带path的方法不能一样，带path要重新读路径获取连接方式等，不带的则读取默认
    /// 2010-11-23 捋了一下公司笔记本的之前的项目，唯一一个有点用处，但是没加到类库的方法就是webcatch了。
    /// 执行存储过程,用sqlparameter的方式,一般返回数据集  2012-4-17添加
    /// 2013-3-27 加入了Oracle数据库
    /// 2013-4-17 本想把XML操作也家进去,但是感觉实在没啥可以集成的.写如下提醒
    /// linqtoxml命名空间System.Xml.Linq;读取:XElement xe = XElement.Load("XMLFile1.xml");删除Remove();保存 xe.Save("XMLFile1.xml");
    /// 选择e.Elements("con").Select(x =>x.Attribute("a").Value).ToList();
    /// 新增xe.Add(new XElement("con", new XAttribute("a", "4"),new XAttribute("b","ydh")));
    /// 2013-10-25加入了各种对Oracle的支持，包括Oracle存储过程取得返回值
    /// 20190306 加入了设置sql连接超时时间的支持
    /// 2019年11月增加了带参数的SQL语句,解决bit类型的sql语句,也可以用作sql防注入
    /// 2020年2月30日新增oralce加密方法支持provide
    /// 20200302 加入Excel的OlEDB方式
    /// 20200318 把Excel操作当成比较普通的方式 注意 Excel的方式不能执行Delete语句
    /// 20200320 修改带log的表的结构,老数据用xml来表示
    /// 20200323 增加了日志表的本地IP和本地UUID
    /// 20200401 优化了public结构,增加了DbParameterS的getdatatale的方式以及执行事务方式,增加了带DbParameterS的日志支持
    /// 20200401 整体命名,结构大改,升级到2.0版本
    /// </summary>
    public class IoRyClass
    {
        #region 属性

        /// <summary>
        /// 数据库类型
        /// </summary>
        private ConType _Contype = ConType.Null;

        /// <summary>
        /// 数据库类型
        /// </summary>
        ConType Contype
        {
            get
            {
                if (this._Contype == ConType.Null)
                {
                    this.ReadConString();
                }
                return this._Contype;
            }
            set { this._Contype = value; }
        }

        /// <summary>
        /// 数据库超时时间
        /// </summary>
        int timeout = -1;

        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConString { get; set; }

        /// <summary>
        /// 默认路径
        /// </summary>
        private string _Path = System.AppDomain.CurrentDomain.BaseDirectory + "constring.xml";

        /// <summary>
        /// 设置或者获取默认路径
        /// </summary>
        string Path
        {
            get { return this._Path; }
            set { this._Path = value; }
        }

        static string _PCIP = null;
        /// <summary>
        /// 本地电脑IP
        /// </summary>
        static public string PCIP
        {
            get
            {
                if (_PCIP == null)
                {
                    SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            if (Convert.ToBoolean(item["IPEnabled"]))
                            {
                                _PCIP = ((string[])item["IPAddress"])[0];
                            }
                        }
                    }
                }
                return _PCIP;
            }
        }

        static string _UUID = null;
        /// <summary>
        /// 本地电脑UUID
        /// </summary>
        static public string UUID
        {
            get
            {
                if (_UUID == null)
                {
                    SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            _UUID = item["UUID"].ToString();
                        }
                    }
                }
                return _UUID;
            }
        }

        #endregion

        #region 操纵数据库

        /// <summary>
        /// 默认构造
        /// </summary>
        public IoRyClass()
        {
        }

        /// <summary>
        /// 设置超时时间 设置为0 无限等待
        /// </summary>
        /// <param name="TimeOut">超时时间 设置为0 无限等待</param>
        public IoRyClass(int TimeOut)
        {
            this.timeout = TimeOut;
        }

        /// <summary>
        /// 带参数的构造
        /// </summary>
        /// <param name="Path"></param>
        public IoRyClass(string Path)
        {
            this.Path = Path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path">xml文件路径</param>
        /// <param name="TimeOut">超时时间 设置为0 无限等待</param>
        public IoRyClass(string Path, int TimeOut)
        {
            this.Path = Path;
            this.timeout = TimeOut;
        }

        /// <summary>
        /// 读取xml自定义的链接字符串
        /// </summary>
        /// <returns></returns>
        void ReadConString()
        {
            this.ReadConString(this.Path);
        }

        /// <summary>
        /// 读取xml自定义的链接字符串
        /// </summary>
        /// <param name="path">xml文件的名称</param>
        /// <returns></returns>
        void ReadConString(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //判断是否存在此xml
            if (!File.Exists(path))
            {
                throw new Exception("不能找到IoRyClass类的XML配置文件！");
            }
            xmlDoc.Load(path);
            //判断连接字符的类型
            XmlNode contype = xmlDoc.SelectSingleNode("constring/type");
            if (contype.InnerText.Trim() != "MSSQL" && contype.InnerText.Trim() != "ACCESS" && contype.InnerText.Trim() != "Oracle" && contype.InnerText.Trim() != "Excel")
            {
                throw new Exception("数据连接类型没填写,或者填写错误,只能填写SQL;ACCESS;Oracle;Excel并且区分大小写!");
            }

            if (contype.InnerText.Trim() == "MSSQL")//sql
            {
                this._Contype = ConType.Sql;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/sqlserver/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/ip");
                    string ip = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/databasename");
                    string databasename = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/username");
                    string username = mynode.FirstChild.Value;
                    string password = null;
                    mynode = xmlDoc.SelectSingleNode("constring/sqlserver/passwordencryption");
                    //判断数据库字符串是否加密
                    if (mynode.InnerText.Trim() != "")
                    {
                        XmlNode key = xmlDoc.SelectSingleNode("constring/sqlserver/encryptKey");
                        password = IoRyClass.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                    }
                    else
                    {
                        mynode = xmlDoc.SelectSingleNode("constring/sqlserver/password");
                        password = mynode.FirstChild.Value;
                    }
                    string con = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ip, databasename, username, password);
                    this.ConString = con;
                }
            }
            else if (contype.InnerText.Trim() == "ACCESS") //access
            {
                this._Contype = ConType.Access;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/access/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/access/path");
                    string accpath = mynode.FirstChild.Value;
                    if (File.Exists(accpath))
                    {
                        string password = null;
                        mynode = xmlDoc.SelectSingleNode("constring/access/passwordencryption");
                        //判断数据库字符串是否加密
                        if (mynode.InnerText.Trim() != "")
                        {
                            XmlNode key = xmlDoc.SelectSingleNode("constring/access/encryptKey");
                            password = IoRyClass.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                        }
                        else
                        {
                            mynode = xmlDoc.SelectSingleNode("constring/access/password");
                            password = mynode.FirstChild.Value;
                        }
                        string con = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", accpath, password);
                        this.ConString = con;
                    }
                    else
                    {
                        throw new Exception("找不到文件:" + accpath);
                    }
                }
            }
            else if (contype.InnerText.Trim() == "Oracle") //Oracle
            {
                this._Contype = ConType.Oracle;
                //判断是否用简单的直接写字符串的方式
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Oracle/simple");
                if (mynode.InnerText.Trim() != "")
                {
                    this.ConString = mynode.FirstChild.Value;
                }
                else
                {
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/DataSource");
                    string DBServer = mynode.FirstChild.Value;
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/username");
                    string username = mynode.FirstChild.Value;
                    string password = null;
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/passwordencryption");
                    //判断数据库字符串是否加密
                    if (mynode.InnerText.Trim() != "")
                    {
                        XmlNode key = xmlDoc.SelectSingleNode("constring/Oracle/encryptKey");
                        password = IoRyClass.DecryptDES(mynode.FirstChild.Value, key.FirstChild.Value);
                    }
                    else
                    {
                        mynode = xmlDoc.SelectSingleNode("constring/Oracle/password");
                        password = mynode.FirstChild.Value;
                    }
                    mynode = xmlDoc.SelectSingleNode("constring/Oracle/Provider");
                    string Provider = mynode.FirstChild.Value;
                    string con;
                    if (Provider == "OraOledb.Oracle")
                    {
                        con = string.Format("Provider=OraOledb.Oracle;Data Source={0};User ID={1};Password={2}", DBServer, username, password);
                    }
                    else
                    {
                        con = string.Format("Provider=MSDAORA;Data Source={0};User ID={1};Password={2}", DBServer, username, password);
                    }

                    this.ConString = con;
                }
            }
            else if (contype.InnerText.Trim() == "Excel")
            {
                this.Contype = ConType.Excel;
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Excel/path");
                string Excelpath = mynode.FirstChild.Value;
                if (File.Exists(Excelpath))
                {
                    this.ConString = this.GetExcelReadonlyConnStr(Excelpath);
                }
                else
                {
                    throw new Exception("找不到文件:" + Excelpath);
                }

            }
            else
            {
                this.ConString = null;
            }
        }

        /// <summary>
        /// 可以写入的Excel链接字符串
        /// </summary>
        void ExcelWriteConString()
        {
            string path = this.Path;
            XmlDocument xmlDoc = new XmlDocument();
            //判断是否存在此xml
            if (!File.Exists(path))
            {
                throw new Exception("不能找到IoRyClass类的XML配置文件！");
            }
            xmlDoc.Load(path);
            //判断连接字符的类型
            XmlNode contype = xmlDoc.SelectSingleNode("constring/type");
            if (contype.InnerText.Trim() != "SQL" && contype.InnerText.Trim() != "ACCESS" && contype.InnerText.Trim() != "Oracle" && contype.InnerText.Trim() != "Excel")
            {
                throw new Exception("数据连接类型没填写,或者填写错误,只能填写SQL;ACCESS;Oracle;Excel并且区分大小写!");
            }

            if (contype.InnerText.Trim() == "Excel")
            {
                XmlNode mynode = xmlDoc.SelectSingleNode("constring/Excel/path");
                string Excelpath = mynode.FirstChild.Value;
                if (File.Exists(Excelpath))
                {
                    this.ConString = this.GetExcelConnStr(Excelpath);
                }
                else
                {
                    throw new Exception("找不到文件:" + Excelpath);
                }
            }
            else
            {
                this.ConString = null;
            }
        }

        /// <summary>
        /// 只读Excel连接串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetExcelReadonlyConnStr(string filePath)
        {
            string connStr = string.Empty;

            if (filePath.Contains(".xlsx"))
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1;'";
            }
            else if (filePath.Contains(".xls"))
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1;'";
            }
            else
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Remove(filePath.LastIndexOf("\\") + 1) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            }

            return connStr;
        }

        /// <summary>
        /// Excel连接串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetExcelConnStr(string filePath)
        {
            string connStr = string.Empty;

            if (filePath.Contains(".xlsx"))
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=0;'";
            }
            else if (filePath.Contains(".xls"))
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=0;'";
            }
            else
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Remove(filePath.LastIndexOf("\\") + 1) + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
            }

            return connStr;
        }

        /// <summary>
        /// 返回Connection
        /// </summary>
        /// <returns></returns>
        IDbConnection IoRyCon()
        {
            switch (this.Contype)
            {
                case ConType.Null:
                    throw new Exception("配置文件错误!没有确定数据库连接字符串！");
                case ConType.Sql:
                    return new SqlConnection(this.ConString);
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                    return new OleDbConnection(this.ConString);

                default:
                    return null;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        public string ExecuteSql(string sql)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        int result = 0;
                        using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                        {
                            SqlCommand com = new SqlCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            Con.Open();
                            result = com.ExecuteNonQuery();
                        }
                        return Convert.ToString(result);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Access:
                    try
                    {
                        int result = 0;
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon())
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            Con.Open();
                            result = com.ExecuteNonQuery();
                        }
                        return Convert.ToString(result);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Oracle:
                    try
                    {
                        int result = 0;
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon())
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            Con.Open();
                            result = com.ExecuteNonQuery();
                        }
                        return Convert.ToString(result);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Excel:
                    try
                    {
                        int result = 0;
                        //这里必须要更换能更新的连接字符串
                        this.ExcelWriteConString();
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon())
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            Con.Open();
                            result = com.ExecuteNonQuery();
                        }
                        return Convert.ToString(result);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// 执行sql语句 带参数的
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns>受影响行数</returns>
        public string ExecuteSql_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        int result = 0;
                        using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                        {
                            SqlCommand com = new SqlCommand(sql, Con);
                            com.Parameters.AddRange(DbParameterS.ToArray());
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            Con.Open();
                            result = com.ExecuteNonQuery();
                        }
                        return Convert.ToString(result);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                    throw new Exception("目前不支持!");
                default:
                    return null;
            }
        }

        /// <summary>
        /// 事务执行sql，只支持SqlServer有事务
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <returns>受影响行数</returns>
        public string ExecuteSqlTran(string sql)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    SqlTransaction sqlTran = null;
                    int result = 0;
                    using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            SqlCommand command = Con.CreateCommand();
                            if (this.timeout != -1)
                            {
                                command.CommandTimeout = this.timeout;
                            }
                            command.Transaction = sqlTran;
                            command.CommandText = sql;
                            result = command.ExecuteNonQuery();

                            sqlTran.Commit();
                        }
                        catch (Exception me)
                        {
                            if (sqlTran != null)
                            {
                                sqlTran.Rollback();
                            }
                            throw me;
                        }
                    }
                    return Convert.ToString(result);

                case ConType.Oracle:
                    throw new Exception("oracle不支持一次执行多条带;的sql语句！请使用ExecuteSql的方法,每次执行一条sql语句.");
                case ConType.Access:
                    throw new Exception("-.-#  Access没有事务，大哥~！");
                case ConType.Excel:
                    throw new Exception("Excel目前不支持");
                default:
                    return null;
            }
        }

        /// <summary>
        /// 事务执行sql，只支持SqlServer有事务
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        public string ExecuteSqlTran_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    SqlTransaction sqlTran = null;
                    int result = 0;
                    using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            SqlCommand command = Con.CreateCommand();
                            command.Parameters.AddRange(DbParameterS.ToArray());
                            if (this.timeout != -1)
                            {
                                command.CommandTimeout = this.timeout;
                            }
                            command.Transaction = sqlTran;
                            command.CommandText = sql;
                            result = command.ExecuteNonQuery();

                            sqlTran.Commit();
                        }
                        catch (Exception me)
                        {
                            if (sqlTran != null)
                            {
                                sqlTran.Rollback();
                            }
                            throw me;
                        }
                    }
                    return Convert.ToString(result);

                case ConType.Oracle:
                    throw new Exception("oracle不支持一次执行多条带;的sql语句！请使用ExecuteSql的方法,每次执行一条sql语句.");
                case ConType.Access:
                    throw new Exception("-.-#  Access没有事务，大哥~！");
                case ConType.Excel:
                    throw new Exception("Excel目前不支持");
                default:
                    return null;
            }
        }

        /// <summary>
        /// 取得datateble,DataSet的第一张表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetTable(string sql)
        {
            return this.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 取得datateble,DataSet的第一张表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        public DataTable GetTable_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            return this.GetDataSet_DbParameter(sql, DbParameterS).Tables[0];
        }

        /// <summary>
        /// 取得DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                        {
                            SqlCommand com = new SqlCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            SqlDataAdapter ada = new SqlDataAdapter(com);
                            DataSet myds = new DataSet();
                            ada.Fill(myds);
                            return myds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                    try
                    {
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon())
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            OleDbDataAdapter ada = new OleDbDataAdapter(com);
                            DataSet myds = new DataSet();
                            ada.Fill(myds);
                            return myds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        ///  取得DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        public DataSet GetDataSet_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                        {
                            SqlCommand com = new SqlCommand(sql, Con);
                            com.Parameters.AddRange(DbParameterS.ToArray());
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            SqlDataAdapter ada = new SqlDataAdapter(com);
                            DataSet myds = new DataSet();
                            ada.Fill(myds);
                            return myds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                    throw new Exception("目前不支持这些方法的带DbParameterS方式!");
                default:
                    return null;
            }
        }

        /// <summary>
        /// 执行存储过程,用sqlparameter的方式,一般返回数据集 2012-4-17添加
        /// 注意out类型的入参 要设置dd.Direction = System.Data.ParameterDirection.Output; 存储过程中给out复制要 select @id=(select count(*) from log_data)
        /// </summary>
        /// <param name="SPname">SP名称</param>
        /// <param name="DbParameterS">DbParameter的集合</param>
        /// <returns>一般返回数据集</returns>
        public DataSet ExecuteSP(string SPname, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.Access:
                case ConType.Excel:
                    throw new Exception("-.-#  Access,Excel还想用这种方法?");
                case ConType.Sql:
                    try
                    {
                        using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                        {
                            SqlCommand sc = new SqlCommand();
                            if (this.timeout != -1)
                            {
                                sc.CommandTimeout = this.timeout;
                            }
                            sc.Connection = Con;
                            sc.CommandType = CommandType.StoredProcedure;
                            sc.CommandText = SPname;
                            sc.Parameters.AddRange(DbParameterS.ToArray());
                            DataSet ds = new DataSet();
                            SqlDataAdapter SDA = new SqlDataAdapter(sc);
                            SDA.Fill(ds);
                            return ds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Oracle:
                    try
                    {
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon())
                        {
                            OleDbCommand sc = new OleDbCommand();
                            if (this.timeout != -1)
                            {
                                sc.CommandTimeout = this.timeout;
                            }
                            sc.Connection = Con;
                            sc.CommandType = CommandType.StoredProcedure;
                            sc.CommandText = SPname;
                            sc.Parameters.AddRange(DbParameterS.ToArray());
                            DataSet ds = new DataSet();
                            OleDbDataAdapter SDA = new OleDbDataAdapter(sc);
                            SDA.Fill(ds);
                            return ds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.Null:
                default:
                    throw new Exception("ConType错误");
            }
        }

        /// <summary>
        /// 已过时
        /// 这个只能执行一个，而且必须是insert语句
        /// 由于GUID的应用,此函数基本用不到了
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>当前自增列的值，很有用</returns>
        public string GetTheValueOfNewAdd(string sql)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    SqlTransaction sqlTran = null;
                    string result = null;
                    using (SqlConnection Con = (SqlConnection)this.IoRyCon())
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            SqlCommand command = Con.CreateCommand();
                            if (this.timeout != -1)
                            {
                                command.CommandTimeout = this.timeout;
                            }
                            command.Transaction = sqlTran;
                            //精髓，加上这个就能得到
                            sql = sql + ";select scope_identity();";
                            command.CommandText = sql;
                            result = Convert.ToString(command.ExecuteScalar());
                            sqlTran.Commit();
                        }
                        catch (Exception me)
                        {
                            if (sqlTran != null)
                            {
                                sqlTran.Rollback();
                            }
                            throw me;
                        }
                    }
                    return result;

                case ConType.Access:
                    throw new Exception("-.-#  Access没有事务，大哥~！");
                case ConType.Oracle:
                    throw new Exception("-.-#  本DLL不支持Oracle的此操作");
                case ConType.Excel:
                    throw new Exception("-.-#  本DLL不支持Excel的此操作");
                default:
                    throw new Exception("ConType错误");
            }
        }

        #endregion

        #region 加密

        #region 可逆向运算的加密方式，需要密钥 对称加密

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region 非对称加密

        //RSA rsa = RSA.Create();
        //rsa.ToXmlString(false)公钥
        //rsa.ToXmlString(true)秘钥
        //string enData = EnRSA(加密串, rsa.ToXmlString(false));
        //string bbb = DeRSA(解密串, rsa.ToXmlString(true));

        /// <summary>
        /// 非对称加密 加密 默认的有长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publickey">RSA rsa = RSA.Create();rsa.ToXmlString(false)</param>
        /// <returns></returns>
        public static string EnRSA(string data, string publickey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// 非对称加密 加密 无长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publickey"></param>
        /// <returns></returns>
        public static String EncryptRSA_long(string data, string publickey)
        {
            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                Byte[] PlaintextData = Encoding.UTF8.GetBytes(data);
                RSACryptography.FromXmlString(publickey);
                int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

                if (PlaintextData.Length <= MaxBlockSize)
                {
                    return Convert.ToBase64String(RSACryptography.Encrypt(PlaintextData, false));
                }

                using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
                {
                    using (MemoryStream CrypStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToEncrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                            Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                            CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                            BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
                    }
                }
            }
        }

        /// <summary>
        /// 非对称加密 解密 默认的有长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privatekey">RSA rsa = RSA.Create();rsa.ToXmlString(true)</param>
        /// <returns></returns>
        public static string DeRSA(string data, string privatekey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);
            return Encoding.UTF8.GetString(cipherbytes);
        }

        /// <summary>
        /// 非对称加密 解密 无长度限制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        public static String DecryptRSA_long(string data, string privatekey)
        {
            using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
            {
                byte[] CiphertextData = Convert.FromBase64String(data);
                RSACryptography.FromXmlString(privatekey);
                int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                if (CiphertextData.Length <= MaxBlockSize)
                {
                    return Encoding.UTF8.GetString(RSACryptography.Decrypt(CiphertextData, false));
                }

                using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
                {
                    using (MemoryStream PlaiStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToDecrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                            Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                            PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                            BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Encoding.UTF8.GetString(PlaiStream.ToArray());
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 这个函数用来给数据进行md5加密 不可逆向运算 2016-3-9改为UFT8编码
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>输出</returns>
        public static string MD5(string strIn)
        {
            byte[] MainClass = System.Text.Encoding.UTF8.GetBytes(strIn);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(MainClass);
            return BitConverter.ToString(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteIn"></param>
        /// <returns></returns>
        public static string MD5(byte[] byteIn)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(byteIn);
            return BitConverter.ToString(result);
        }

        #endregion

        #region 字节转化，压缩


        /// <summary>
        /// datatable序列化string
        /// </summary>
        /// <param name="pDt"></param>
        /// <returns></returns>
        public static string SerializeDataTableXml(DataTable pDt)
        {
            //  序列化DataTable
            pDt.TableName = pDt.TableName;
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }
        ///   <summary>
        ///  反序列化DataTable string
        ///   </summary>
        ///   <param name="pXml"> 序列化的DataTable </param>
        ///   <returns> DataTable </returns>
        public static DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }

        /// <summary>
        /// 本地文件转化成Base64
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string FileToBase64(string FilePath)
        {
            FileStream files = new FileStream(FilePath, FileMode.Open);
            byte[] bt = new byte[files.Length];
            files.Read(bt, 0, bt.Length);
            string base64Str = Convert.ToBase64String(bt);
            return base64Str;
        }

        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static byte[] CompressData(byte[] Data)
        {
            if (Data != null)
            {
                MemoryStream ms = new MemoryStream();
                DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Compress, false);
                deflateStream.Write(Data, 0, Data.Length);
                deflateStream.Close();
                return ms.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 解压缩数据
        /// </summary>
        /// <param name="Data">解压的数据</param>
        /// <param name="nMaxScale">由于无法获取实际解压后的大小,设置一个相对于原始数据大小的比例值</param>
        /// <returns></returns>
        public static byte[] DecompressData(byte[] Data, int nMaxScale)
        {
            MemoryStream ms = new MemoryStream(Data);

            #region -获取解压后的大小,要改进(太慢了)

            //DeflateStream deflateStreamSize = new DeflateStream(ms, CompressionMode.Decompress,false);
            //int nLenght    = new int();
            //nLenght        = 0;
            //while (deflateStreamSize.ReadByte() != -1)
            //{
            //    nLenght = nLenght + 1;
            //}

            #endregion

            //byte[] dezipArray = new byte[nLenght];
            //ms.Position = 0;
            // 由于无法获取实际解压后的大小,这里设置一个估算值
            byte[] dezipArray = new byte[Data.Length * nMaxScale];
            DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress, false);
            int nL = deflateStream.Read(dezipArray, 0, dezipArray.Length);
            byte[] resultData = new byte[nL];
            Array.Copy(dezipArray, resultData, nL);
            deflateStream.Close();
            return resultData;
        }

        /// <summary>
        /// 取得DataSet的二进制XML数据(仅用于WebService序列化使用,可以提高传输速度约%40)
        /// </summary>
        /// <param name="dsOriginal">序列化的DataSet</param>
        /// <returns>二进制形式的XML</returns>
        public static byte[] GetBinaryFormatDataSet(DataSet dsOriginal)
        {
            if (dsOriginal != null)
            {
                byte[] binaryDataResult = null;
                using (MemoryStream memStream = new MemoryStream())        // 需要一个内存流对象
                {
                    IFormatter brFormatter = new BinaryFormatter();        // 二进制序列化对象
                    dsOriginal.RemotingFormat = SerializationFormat.Binary;// DataSet必须拥有此属性

                    brFormatter.Serialize(memStream, dsOriginal);
                    binaryDataResult = memStream.ToArray();
                    memStream.Close();
                    return binaryDataResult;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将二进制的DataSetXML反序列化为标准XML DataSet
        /// </summary>
        /// <param name="binaryData">需要反序列化的DatSet</param>
        /// <returns>标准XML的DataSet</returns>
        public static DataSet RetrieveDataSet(byte[] binaryData)
        {
            if (binaryData != null)
            {
                using (MemoryStream memStream = new MemoryStream(binaryData))
                {
                    IFormatter brFormatter = new BinaryFormatter();
                    DataSet ds = (DataSet)brFormatter.Deserialize(memStream);
                    memStream.Close();
                    return ds;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 序列化DataSet对象
        /// </summary>
        /// <param name="dsOriginal"></param>
        /// <returns></returns>
        public static byte[] GetXmlFormatDataSet(DataSet dsOriginal)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(typeof(DataSet));
                xs.Serialize(memStream, dsOriginal);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// 反序列化DataSet对象
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public static DataSet RetrieveXmlDataSet(byte[] binaryData)
        {
            using (MemoryStream memStream = new MemoryStream(binaryData))
            {
                XmlSerializer xs = new XmlSerializer(typeof(DataSet));
                DataSet ds = xs.Deserialize(memStream) as DataSet;
                return ds;
            }
        }

        /// <summary>
        /// 将string转化成utf8的bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 将bytes转化成string
        /// </summary>
        /// <param name="mybyte"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] mybyte)
        {
            return System.Text.Encoding.UTF8.GetString(mybyte);
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str">输入</param>
        /// <returns>压缩后的byte[]</returns>
        public static byte[] StringToZip(string str)
        {
            byte[] myby = IoRyClass.StringToBytes(str);
            return IoRyClass.CompressData(myby);
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static string ZipToString(byte[] mybytes)
        {
            byte[] myte = IoRyClass.DecompressData(mybytes, 10);
            return IoRyClass.BytesToString(myte);
        }

        /// <summary>
        /// 压缩DataTable
        /// </summary>
        /// <param name="mydt">DataTable</param>
        /// <returns></returns>
        public static byte[] DataTableToZip(DataTable mydt)
        {
            DataSet myds = new DataSet();
            myds.Tables.Add(mydt.Copy());
            byte[] mybt = IoRyClass.GetBinaryFormatDataSet(myds);
            return IoRyClass.CompressData(mybt);
        }

        /// <summary>
        /// 解压DataTable
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static DataTable ZipToDateTable(byte[] mybytes)
        {
            byte[] myby = IoRyClass.DecompressData(mybytes, 50);
            DataSet myds = IoRyClass.RetrieveDataSet(myby);
            return myds.Tables[0];
        }

        /// <summary>
        /// 压缩DataSet
        /// </summary>
        /// <param name="myds">DateSet</param>
        /// <returns></returns>
        public static byte[] DataSetToZip(DataSet myds)
        {
            byte[] mybys = IoRyClass.GetBinaryFormatDataSet(myds);
            return IoRyClass.CompressData(mybys);
        }

        /// <summary>
        /// 解压DataSet
        /// </summary>
        /// <param name="mybytes"></param>
        /// <returns></returns>
        public static DataSet ZipToDataSet(byte[] mybytes)
        {
            byte[] myby = IoRyClass.DecompressData(mybytes, 50);
            return IoRyClass.RetrieveDataSet(myby);
        }

        #endregion

        #region Log 跟winformDemo里查看log是一套

        //20200320 修改了数据库,并且放到了tool和demo里,不再用下面的
        //这里Log对应的数据库设计 
        /*
CREATE TABLE [dbo].[Log_H](
	[int_index] [int] IDENTITY(1,1) NOT NULL,
	[str_opreater] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[str_Type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
    [str_tablename] [nvarchar](50)  COLLATE Chinese_PRC_CI_AS NULL,
	[str_Sql] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[str_Old] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[dat_time] [datetime] NULL,
 CONSTRAINT [PK_Log_H] PRIMARY KEY CLUSTERED 
(
	[int_index] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
        */

        /// <summary>
        /// 生成带本地IP与UUID的sql日志语句 20200323 上面的不带本地ip的用不到了 
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        string GetLogSql_IP(string username, string sql)
        {
            IoRyClass ic = new IoRyClass(this.Path);
            string tablename = "";
            if (sql.ToLower().Contains("insert"))
            {
                tablename = sql.ToLower().Replace("insert", "").Replace("into", "").Split('(')[0].Trim();
                return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID) 
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "新增", sql.Replace("'", "''"), "", "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
            }
            if (sql.ToLower().Contains("update"))
            {
                tablename = sql.ToLower().Split(new string[] { "set" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("update", "").Trim();
                string sqlold = "select * from " + tablename + " where " + sql.ToLower().Split(new string[] { "where" }, StringSplitOptions.None)[1];
                DataTable oldtable = ic.GetTable(sqlold);
                if (oldtable.Rows.Count == 0)
                {
                    return ";";
                }
                else
                {
                    //string old = "";
                    XElement xolddata = new XElement("OldData");
                    for (int j = 0; j < oldtable.Rows.Count; j++)
                    {
                        XElement row = new XElement("Row");
                        for (int i = 0; i < oldtable.Columns.Count; i++)
                        {
                            XElement col = new XElement("Column");
                            col.Add(new XAttribute("colName", oldtable.Columns[i].ColumnName));
                            col.Value = oldtable.Rows[j][i].ToString();
                            row.Add(col);
                            //old = old + " [ " + oldtable.Columns[i].ColumnName + " | " + oldtable.Rows[0][i].ToString() + " ]; ";
                        }
                        xolddata.Add(row);
                    }

                    return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID) 
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "修改", sql.Replace("'", "''"), xolddata.ToString().Replace("'", "''"), "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
                }
            }
            if (sql.ToLower().Contains("delete"))
            {
                string sqlold = "";
                if (sql.ToLower().Contains("from"))
                {
                    if (sql.ToLower().Contains("where"))
                    {
                        tablename = sql.ToLower().Replace("delete", "").Replace("from", "").Split(new string[] { "where" }, StringSplitOptions.None)[0].Trim();
                    }
                    else
                    {
                        tablename = sql.ToLower().Replace("delete", "").Replace("from", "").Trim();
                    }
                    sqlold = sql.ToLower().Replace("delete", "select * ");
                }
                else
                {
                    if (sql.ToLower().Contains("where"))
                    {
                        tablename = sql.ToLower().Replace("delete", "").Split(new string[] { "where" }, StringSplitOptions.None)[0].Trim();
                    }
                    else
                    {
                        tablename = sql.ToLower().Replace("delete", "").Trim();
                    }
                    sqlold = sql.ToLower().Replace("delete", "select * from ");
                }
                DataTable oldtable = ic.GetTable(sqlold);
                if (oldtable.Rows.Count > 0)
                {
                    //string old = "";
                    //for (int i = 0; i < oldtable.Columns.Count; i++)
                    //{
                    //    old = old + " [ " + oldtable.Columns[i].ColumnName + " | " + oldtable.Rows[0][i].ToString() + " ]; ";
                    //}

                    XElement xolddata = new XElement("OldData");
                    for (int j = 0; j < oldtable.Rows.Count; j++)
                    {
                        XElement row = new XElement("Row");
                        for (int i = 0; i < oldtable.Columns.Count; i++)
                        {
                            XElement col = new XElement("Column");
                            col.Add(new XAttribute("colName", oldtable.Columns[i].ColumnName));
                            col.Value = oldtable.Rows[j][i].ToString();
                            row.Add(col);
                        }
                        xolddata.Add(row);
                    }


                    return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID)
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "删除", sql.Replace("'", "''"), xolddata.ToString().Replace("'", "''"), "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
                }
                else
                {
                    return "";
                }
            }
            if (sql.ToLower().Contains("select"))
            {
                if (sql.ToLower().Contains("where"))
                {
                    tablename = sql.ToLower().Split(new string[] { "where" }, StringSplitOptions.None)[0].Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                }
                else
                {
                    tablename = sql.ToLower().Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                }
                return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID)
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "查询", sql.Replace("'", "''"), "", "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
            }
            return "";
        }

        string GetLogSql_IP(string username, string sql, List<DbParameter> DbParameterS)
        {
            IoRyClass ic = new IoRyClass(this.Path);
            string ps = "<Parameters><SQL>" + sql + "</SQL>";
            foreach (var item in DbParameterS)
            {
                string cs = "<ParameterName>" + item.ParameterName + "</ParameterName><Value>" + item.Value.ToString() + "</Value>";
                ps += cs;
            }
            ps += "</Parameters>";
            string tablename = "";
            if (sql.ToLower().Contains("insert"))
            {
                tablename = sql.ToLower().Replace("insert", "").Replace("into", "").Split('(')[0].Trim();
                return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID) 
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "新增", ps.Replace("'", "''"), "", "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
            }
            if (sql.ToLower().Contains("update"))
            {
                tablename = sql.ToLower().Split(new string[] { "set" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("update", "").Trim();
                string sqlold = "select * from " + tablename + " where " + sql.ToLower().Split(new string[] { "where" }, StringSplitOptions.None)[1];
                DataTable oldtable = ic.GetTable_DbParameter(sqlold, DbParameterS);
                if (oldtable.Rows.Count == 0)
                {
                    return ";";
                }
                else
                {
                    //string old = "";
                    XElement xolddata = new XElement("OldData");
                    for (int j = 0; j < oldtable.Rows.Count; j++)
                    {
                        XElement row = new XElement("Row");
                        for (int i = 0; i < oldtable.Columns.Count; i++)
                        {
                            XElement col = new XElement("Column");
                            col.Add(new XAttribute("colName", oldtable.Columns[i].ColumnName));
                            col.Value = oldtable.Rows[j][i].ToString();
                            row.Add(col);
                            //old = old + " [ " + oldtable.Columns[i].ColumnName + " | " + oldtable.Rows[0][i].ToString() + " ]; ";
                        }
                        xolddata.Add(row);
                    }

                    return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID) 
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "修改", ps.Replace("'", "''"), xolddata.ToString().Replace("'", "''"), "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
                }
            }
            if (sql.ToLower().Contains("delete"))
            {
                string sqlold = "";
                if (sql.ToLower().Contains("from"))
                {
                    if (sql.ToLower().Contains("where"))
                    {
                        tablename = sql.ToLower().Replace("delete", "").Replace("from", "").Split(new string[] { "where" }, StringSplitOptions.None)[0].Trim();
                    }
                    else
                    {
                        tablename = sql.ToLower().Replace("delete", "").Replace("from", "").Trim();
                    }
                    sqlold = sql.ToLower().Replace("delete", "select * ");
                }
                else
                {
                    if (sql.ToLower().Contains("where"))
                    {
                        tablename = sql.ToLower().Replace("delete", "").Split(new string[] { "where" }, StringSplitOptions.None)[0].Trim();
                    }
                    else
                    {
                        tablename = sql.ToLower().Replace("delete", "").Trim();
                    }
                    sqlold = sql.ToLower().Replace("delete", "select * from ");
                }
                DataTable oldtable = ic.GetTable_DbParameter(sqlold, DbParameterS);
                if (oldtable.Rows.Count > 0)
                {
                    XElement xolddata = new XElement("OldData");
                    for (int j = 0; j < oldtable.Rows.Count; j++)
                    {
                        XElement row = new XElement("Row");
                        for (int i = 0; i < oldtable.Columns.Count; i++)
                        {
                            XElement col = new XElement("Column");
                            col.Add(new XAttribute("colName", oldtable.Columns[i].ColumnName));
                            col.Value = oldtable.Rows[j][i].ToString();
                            row.Add(col);
                        }
                        xolddata.Add(row);
                    }


                    return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID)
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "删除", ps.Replace("'", "''"), xolddata.ToString().Replace("'", "''"), "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
                }
                else
                {
                    return "";
                }
            }
            if (sql.ToLower().Contains("select"))
            {
                if (sql.ToLower().Contains("where"))
                {
                    tablename = sql.ToLower().Split(new string[] { "where" }, StringSplitOptions.None)[0].Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                }
                else
                {
                    tablename = sql.ToLower().Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                }
                return string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID)
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "查询", ps.Replace("'", "''"), "", "getdate()", tablename, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
            }
            return "";
        }

        /// <summary>
        /// 生成带本地IP与UUID的执行sp日志语句
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="SPName">存储过程名</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        string GetLogSP_IP(string username, string SPName, List<DbParameter> DbParameterS)
        {
            IoRyClass ic = new IoRyClass(this.Path);
            string ps = "<Parameters><SPName>" + SPName + "</SPName>";
            foreach (var item in DbParameterS)
            {
                string cs = "<ParameterName>" + item.ParameterName + "</ParameterName><Value>" + item.Value.ToString() + "</Value>";
                ps += cs;
            }
            ps += "</Parameters>";
            string sql = string.Format(@";insert into log_data (sopreater_str,type_str,SQL_str,olddata_str,createtime_dt,tablename_str,UUID_GUID_str,IP_str,log_data_GUID) 
values ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');", username, "存储过程", ps.Replace("'", "''"), "", "getdate()", SPName, IoRyClass.UUID, IoRyClass.PCIP, Guid.NewGuid());
            return sql;
        }

        /// <summary>
        /// 事务执行sql并且生成Log,只支持Sqlserver,oracle不行,报错
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns>受影响行数</returns>
        public string Log_ExecuteSqlTran(string sql, string username)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    string newsql = "";
                    if (sql.Contains(";"))
                    {
                        foreach (string sqls in sql.Split(';'))
                        {
                            newsql += this.GetLogSql_IP(username, sqls) + sqls;
                        }
                    }
                    else
                    {
                        newsql = this.GetLogSql_IP(username, sql) + sql;
                    }
                    return this.ExecuteSqlTran(newsql);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 事务执行sql并且生成Log,只支持Sqlserver,oracle不行,报错
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <param name="DbParameterS">参数</param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        public string Log_ExecuteSqlTran(string sql, List<DbParameter> DbParameterS, string username)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    string newsql = "";
                    if (sql.Contains(";"))
                    {
                        foreach (string sqls in sql.Split(';'))
                        {
                            newsql += this.GetLogSql_IP(username, sqls, DbParameterS) + sqls;
                        }
                    }
                    else
                    {
                        newsql = this.GetLogSql_IP(username, sql, DbParameterS) + sql;
                    }
                    return this.ExecuteSqlTran_DbParameter(newsql, DbParameterS);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 带日志的执行存储过程的方法
        /// </summary>
        /// <param name="SPname">存储过程名</param>
        /// <param name="DbParameterS">参数</param>
        /// <param name="username">执行用户</param>
        /// <returns></returns>
        public DataSet Log_ExecuteSP(string SPname, List<DbParameter> DbParameterS, string username)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    string newsql = "";
                    newsql = this.GetLogSP_IP(username, SPname, DbParameterS);
                    this.ExecuteSql(newsql);
                    return this.ExecuteSP(SPname, DbParameterS);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 目前只支持sqlserver
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        public DataSet Log_GetDataSet(string sql, string username)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        string newsql = "";
                        if (sql.Contains(";"))
                        {
                            foreach (string sqls in sql.Split(';'))
                            {
                                newsql += this.GetLogSql_IP(username, sqls) + sqls;
                            }
                        }
                        else
                        {
                            newsql = this.GetLogSql_IP(username, sql) + sql;
                        }
                        return this.GetDataSet(newsql);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// 目前只支持sqlserver
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="DbParameterS"></param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        public DataSet Log_GetDataSet(string sql, List<DbParameter> DbParameterS, string username)
        {
            switch (this.Contype)
            {
                case ConType.Sql:
                    try
                    {
                        string newsql = "";
                        if (sql.Contains(";"))
                        {
                            foreach (string sqls in sql.Split(';'))
                            {
                                newsql += this.GetLogSql_IP(username, sqls, DbParameterS) + sqls;
                            }
                        }
                        else
                        {
                            newsql = this.GetLogSql_IP(username, sql, DbParameterS) + sql;
                        }
                        return this.GetDataSet_DbParameter(newsql, DbParameterS);
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                default:
                    return null;
            }
        }

        #endregion

        #region 获取PCconfig 跟winformDemo里PCconfig是一套

        /// <summary>
        /// 获取全局变量,跟IP无关
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string PCconfigGlobalValue(string key)
        {
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", key.Replace('\'', ' '));
            return PCconfigValue_Sql(sql);
        }

        /// <summary>
        /// 通过IP和UUID获取PCconfig 跟winformDemo里PCconfig是一套
        /// </summary>
        /// <param name="key">config的key</param>
        /// <returns></returns>
        public string PCconfigValue(string key)
        {
            string sql = string.Format("select value_str from PC_config where IP_str='{0}' and UUID_GUID='{1}' and key_str='{2}'", IoRyClass.PCIP, IoRyClass.UUID, key.Replace('\'', ' '));
            return PCconfigValue_Sql(sql);
        }

        /// <summary>
        /// 通过本地IP获取PCconfig 跟winformDemo里PCconfig是一套
        /// </summary>
        /// <param name="key">config的key</param>
        /// <returns></returns>
        public string PCconfigValueByIP(string key)
        {
            string sql = string.Format("select value_str from PC_config where IP_str='{0}' and key_str='{1}' ", IoRyClass.PCIP, key.Replace('\'', ' '));
            return PCconfigValue_Sql(sql);
        }

        /// <summary>
        /// 通过IP获取PCconfig 跟winformDemo里PCconfig是一套
        /// </summary>
        /// <param name="key">config的key</param>
        /// <param name="IP"></param>
        /// <returns></returns>
        public string PCconfigValueByIP(string key, string IP)
        {
            string sql = string.Format("select value_str from PC_config where IP_str='{0}' and key_str='{1}'", IP.Replace('\'', ' '), key.Replace('\'', ' '));
            return PCconfigValue_Sql(sql);
        }

        /// <summary>
        /// 获取PCconfig 跟winformDemo里PCconfig是一套
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>如果找不到的话,则返回null</returns>
        string PCconfigValue_Sql(string sql)
        {
            DataTable dt = this.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            if (dt.Rows.Count > 1)
            {
                throw new Exception("找到多个value,请处理数据库!");
            }
            else
            {
                return Convert.ToString(dt.Rows[0]["value_str"]);
            }
        }

        #endregion

        #region CSV文件
        /// <summary>
        /// 注意这里用的是gb2312,符合现在一般Excel打开CSV的格式
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public void CSV_Create(DataTable dt, string path)
        {
            try
            {
                System.IO.FileStream fs = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                StreamWriter sw = new StreamWriter(fs, gb2312);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i == dt.Columns.Count - 1)
                    {
                        sw.WriteLine();
                    }
                    else
                    {
                        sw.Write(",");
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sw.Write(Convert.ToString(dt.Rows[i][j]));
                        if (j < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    if (i < dt.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception me)
            {
                throw me;
            }
        }

        #endregion

        #region OLEDB读取Excel 注意要安装 https://www.microsoft.com/zh-CN/download/details.aspx?id=13255 的x86版本

        /// <summary>
        /// OLEDB读取Excel 注意要安装 https://www.microsoft.com/zh-CN/download/details.aspx?id=13255 的x86版本
        /// sql示例:Select * From [Sheet1$]
        /// 20200318 增加了Excel的其他操作,如果单独读取,也可以用这个方式
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="sql">sql示例:Select * From [Sheet1$]</param>
        /// <returns></returns>
        public DataSet Excel_Get(string filePath, string sql)
        {
            try
            {
                // 获得连接
                string connStr = this.GetExcelReadonlyConnStr(filePath);

                if (File.Exists(filePath))
                {
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;
                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        myCommand = new OleDbDataAdapter(sql, conn);
                        ds = new DataSet();
                        myCommand.Fill(ds, "Sheet");
                        return ds;
                    }
                }
                else
                {
                    throw new Exception("找不到文件:" + filePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }

    #region 枚举
    /// <summary>
    /// 链接类型
    /// </summary>
    public enum ConType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// sql
        /// </summary>
        Sql,
        /// <summary>
        /// access
        /// </summary>
        Access,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// Excel
        /// </summary>
        Excel
    }

    #endregion
}
