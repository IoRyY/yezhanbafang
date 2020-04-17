using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;
using yezhanbafang.Core;

namespace yezhanbafang.MySQL
{
    public class IoRyClass : YezhanbafangCore
    {
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
        /// 返回Connection
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <returns></returns>
        IDbConnection IoRyCon
        {
            get
            {
                switch (this.Contype)
                {
                    case ConType.Null:
                        throw new Exception("配置文件错误!没有确定数据库连接字符串！");
                    case ConType.MySQL:
                        return new MySqlConnection(this.ConString);
                    case ConType.Access:
                    case ConType.Oracle:
                    case ConType.Excel:
                    case ConType.MSSQL:
                    default:
                        throw new Exception("请根据数据库类型选择类库！");
                }
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        public string ExecuteSql(string sql)
        {
            return this.ExecuteSql(this.Path, sql);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        string ExecuteSql(string path, string sql)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    try
                    {
                        int result = 0;
                        using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                        {
                            MySqlCommand com = new MySqlCommand(sql, Con);
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
                case ConType.MSSQL:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
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
            return this.ExecuteSql_DbParameter(this.Path, sql, DbParameterS);
        }

        /// <summary>
        /// 执行sql语句 带参数的
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns>受影响行数</returns>
        string ExecuteSql_DbParameter(string path, string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    try
                    {
                        int result = 0;
                        using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                        {
                            MySqlCommand com = new MySqlCommand(sql, Con);
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
                case ConType.MSSQL:
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
            }
        }

        /// <summary>
        /// 事务执行sql，只支持SqlServer有事务
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <returns>受影响行数</returns>
        public string ExecuteSqlTran(string sql)
        {
            return this.ExecuteSqlTran(this.Path, sql);
        }

        /// <summary>
        /// 事务执行sql，只支持SqlServer有事务
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        public string ExecuteSqlTran_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            return this.ExecuteSqlTran_DbParameter(this.Path, sql, DbParameterS);
        }

        /// <summary>
        /// 事务执行sql，Access没有事务,oracle不支持一次执行多条带;的sql语句
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句们</param>
        /// <returns>受影响行数</returns>
        string ExecuteSqlTran(string path, string sql)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    MySqlTransaction sqlTran = null;
                    int result = 0;
                    using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            MySqlCommand command = Con.CreateCommand();
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
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.Access:
                case ConType.Excel:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
            }
        }

        /// <summary>
        /// 事务执行sql带DbParameterS
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句们</param>
        /// <param name="DbParameterS">参数</param>
        /// <returns></returns>
        string ExecuteSqlTran_DbParameter(string path, string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    MySqlTransaction sqlTran = null;
                    int result = 0;
                    using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            MySqlCommand command = Con.CreateCommand();
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
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.Access:
                case ConType.Excel:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
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
            return this.GetDataSet(this.Path, sql);
        }

        /// <summary>
        ///  取得DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        public DataSet GetDataSet_DbParameter(string sql, List<DbParameter> DbParameterS)
        {
            return this.GetDataSet_DbParameter(this.Path, sql, DbParameterS);
        }

        /// <summary>
        /// 取得dataset
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        DataSet GetDataSet(string path, string sql)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    try
                    {
                        using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                        {
                            MySqlCommand com = new MySqlCommand(sql, Con);
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            MySqlDataAdapter ada = new MySqlDataAdapter(com);
                            DataSet myds = new DataSet();
                            ada.Fill(myds);
                            return myds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.MSSQL:
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
            }
        }

        /// <summary>
        /// 取得dataset
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句</param>
        /// <param name="DbParameterS">入参</param>
        /// <returns></returns>
        DataSet GetDataSet_DbParameter(string path, string sql, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    try
                    {
                        using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                        {
                            MySqlCommand com = new MySqlCommand(sql, Con);
                            com.Parameters.AddRange(DbParameterS.ToArray());
                            if (this.timeout != -1)
                            {
                                com.CommandTimeout = this.timeout;
                            }
                            MySqlDataAdapter ada = new MySqlDataAdapter(com);
                            DataSet myds = new DataSet();
                            ada.Fill(myds);
                            return myds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.MSSQL:
                case ConType.Access:
                case ConType.Oracle:
                case ConType.Excel:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
            }
        }

        /// <summary>
        /// 执行存储过程,用sqlparameter的方式,一般返回数据集  2012-4-17添加
        /// 注意out类型的入参 要设置dd.Direction = System.Data.ParameterDirection.Output; 存储过程中给out复制要 select @id=(select count(*) from log_data)
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="SPname">SP名称</param>
        /// <param name="DbParameterS">DbParameter的集合</param>
        /// <returns>一般返回数据集</returns>
        DataSet ExecuteSP(string path, string SPname, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    try
                    {
                        using (MySqlConnection Con = (MySqlConnection)this.IoRyCon)
                        {
                            MySqlCommand sc = new MySqlCommand();
                            if (this.timeout != -1)
                            {
                                sc.CommandTimeout = this.timeout;
                            }
                            sc.Connection = Con;
                            sc.CommandType = CommandType.StoredProcedure;
                            sc.CommandText = SPname;
                            sc.Parameters.AddRange(DbParameterS.ToArray());
                            DataSet ds = new DataSet();
                            MySqlDataAdapter SDA = new MySqlDataAdapter(sc);
                            SDA.Fill(ds);
                            return ds;
                        }
                    }
                    catch (Exception me)
                    {
                        throw me;
                    }
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.Access:
                case ConType.Excel:
                case ConType.Null:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
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
            return this.ExecuteSP(this.Path, SPname, DbParameterS);
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
            return this.Log_ExecuteSqlTran(this.Path, sql, username);
        }

        /// <summary>
        /// 事务执行sql并且生成Log,只支持Sqlserver,oracle不行,报错
        /// </summary>
        /// <param name="sql">sql语句们</param>
        /// <param name="DbParameterS">参数</param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        public string Log_ExecuteSqlTran_DbParameter(string sql, List<DbParameter> DbParameterS, string username)
        {
            return this.Log_ExecuteSqlTran_DbParameter(this.Path, sql, username, DbParameterS);
        }

        /// <summary>
        /// 事务执行sql并且生成Log,只支持Sqlserver,oracle不行,报错
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句们</param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns>受影响行数</returns>
        string Log_ExecuteSqlTran(string path, string sql, string username)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
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
                    return this.ExecuteSqlTran(path, newsql);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 事务执行sql并且生成Log,只支持Sqlserver,oracle不行,报错
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句们</param>
        /// <param name="username">执行sql语句的用户</param>
        /// <param name="DbParameterS">参数</param>
        /// <returns></returns>
        string Log_ExecuteSqlTran_DbParameter(string path, string sql, string username, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
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
                    return this.ExecuteSqlTran_DbParameter(path, newsql, DbParameterS);
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
            return this.Log_ExecuteSP(this.Path, SPname, DbParameterS, username);
        }

        /// <summary>
        /// 带日志的执行存储过程的方法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="SPname"></param>
        /// <param name="DbParameterS"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        DataSet Log_ExecuteSP(string path, string SPname, List<DbParameter> DbParameterS, string username)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
                    string newsql = "";
                    newsql = this.GetLogSP_IP(username, SPname, DbParameterS);
                    this.ExecuteSql(newsql);
                    return this.ExecuteSP(path, SPname, DbParameterS);
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
            return this.Log_GetDataSet(this.Path, sql, username);
        }

        /// <summary>
        /// 目前只支持sqlserver
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="DbParameterS"></param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        public DataSet Log_GetDataSet_DbParameter(string sql, List<DbParameter> DbParameterS, string username)
        {
            return this.Log_GetDataSet_DbParameter(this.Path, sql, username, DbParameterS);
        }

        /// <summary>
        /// 目前只支持sqlserver
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sql"></param>
        /// <param name="username">执行sql语句的用户</param>
        /// <returns></returns>
        DataSet Log_GetDataSet(string path, string sql, string username)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
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
                        return this.GetDataSet(path, newsql);
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
        /// <param name="path"></param>
        /// <param name="sql"></param>
        /// <param name="username">执行sql语句的用户</param>
        /// <param name="DbParameterS"></param>
        /// <returns></returns>
        DataSet Log_GetDataSet_DbParameter(string path, string sql, string username, List<DbParameter> DbParameterS)
        {
            switch (this.Contype)
            {
                case ConType.MySQL:
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
                        return this.GetDataSet_DbParameter(path, newsql, DbParameterS);
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
    }
}
