using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using yezhanbafang.Core;

namespace yezhanbafang.ExcelAccess
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
        IDbConnection IoRyCon(string path)
        {
            switch (this.Contype)
            {
                case ConType.Null:
                    throw new Exception("配置文件错误!没有确定数据库连接字符串！");
                case ConType.Access:
                case ConType.Excel:
                    return new OleDbConnection(path);
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.MySQL:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
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
                case ConType.Access:
                case ConType.Excel:
                    try
                    {
                        int result = 0;
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
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
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.MySQL:
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
                case ConType.Access:
                case ConType.Excel:
                    try
                    {
                        int result = 0;
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
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
                case ConType.MySQL:
                case ConType.MSSQL:
                case ConType.Oracle:
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
                case ConType.Access:
                case ConType.Excel:
                    OleDbTransaction sqlTran = null;
                    int result = 0;
                    using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            OleDbCommand command = Con.CreateCommand();
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
                case ConType.MySQL:
                case ConType.Oracle:
                case ConType.MSSQL:
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
                case ConType.Access:
                case ConType.Excel:
                    OleDbTransaction sqlTran = null;
                    int result = 0;
                    using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            OleDbCommand command = Con.CreateCommand();
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
                case ConType.MySQL:
                case ConType.Oracle:
                case ConType.MSSQL:
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
                case ConType.Access:
                case ConType.Excel:
                    try
                    {
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
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
                case ConType.MySQL:
                case ConType.MSSQL:
                case ConType.Oracle:
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
                case ConType.Access:
                case ConType.Excel:
                    try
                    {
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
                        {
                            OleDbCommand com = new OleDbCommand(sql, Con);
                            com.Parameters.AddRange(DbParameterS.ToArray());
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
                case ConType.MySQL:
                case ConType.MSSQL:
                case ConType.Oracle:
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
                case ConType.Access:
                case ConType.Excel:
                    try
                    {
                        using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
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
                case ConType.MySQL:
                case ConType.Oracle:
                case ConType.MSSQL:
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

        /// <summary>
        /// 已过时
        /// 这个只能执行一个，而且必须是insert语句
        /// 由于GUID的应用,此函数基本用不到了
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>当前自增列的值，很有用</returns>
        public string GetTheValueOfNewAdd(string sql)
        {
            return this.GetTheValueOfNewAdd(this.Path, sql);
        }

        /// <summary>
        /// 这个只能执行一个，而且必须是insert语句
        /// </summary>
        /// <param name="path">数据库连接xml路径</param>
        /// <param name="sql">sql语句</param>
        /// <returns>当前自增列的值，很有用</returns>
        string GetTheValueOfNewAdd(string path, string sql)
        {
            switch (this.Contype)
            {
                case ConType.Access:
                case ConType.Excel:
                    OleDbTransaction sqlTran = null;
                    string result = null;
                    using (OleDbConnection Con = (OleDbConnection)this.IoRyCon(path))
                    {
                        try
                        {
                            Con.Open();
                            sqlTran = Con.BeginTransaction();
                            OleDbCommand command = Con.CreateCommand();
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
                case ConType.MSSQL:
                case ConType.Oracle:
                case ConType.MySQL:
                default:
                    throw new Exception("请根据数据库类型选择类库！");
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
}
