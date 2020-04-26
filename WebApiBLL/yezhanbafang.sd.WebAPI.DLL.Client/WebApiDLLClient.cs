using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using yezhanbafang.sd.MSSQL;

namespace yezhanbafang.sd.WebAPI.DLL.Client
{
    public class WebApiDLLClient
    {
        string Url { get; set; }
        string ConfigPath { get; set; }

        WebApiClient WC = null;

        public WebApiDLLClient(string encrypt,string url,string configpath)
        {
            this.Url = url;
            this.ConfigPath = configpath;
            WC = new WebApiClient(url, configpath);
        }

        #region 和WebApi交互的方法

        /// <summary>
        /// 异步的得到DataSet的方法,可以和按钮与进度条相关联用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="myobj">可以传送到事件里面的东西</param>
        public async Task<DataSet> GetDataSet_Async(string sql)
        {
            BllClass bc = await WC.CallWebAPI_Async(sql, "GetDataSet");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 异步的得到DataSet的方法,可以和按钮与进度条相关联用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <param name="myobj">可以传送到事件里面的东西</param>
        public async Task<DataSet> GetDataSet_Async(string sql, string cuser)
        {
            BllClass bc = await WC.CallWebAPI_Async(sql, cuser, "GetDataSet");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        public async Task<string> ExcutSqlTran_Async(string sql)
        {
            BllClass bc = await WC.CallWebAPI_Async(sql, "ExcutSqlTran");
            return bc.JsonOut;
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        public async Task<string> ExcutSqlTran_Async(string sql, string cuser)
        {
            BllClass bc = await WC.CallWebAPI_Async(sql, cuser, "ExcutSqlTran_Log");
            return bc.JsonOut;
        }

        /// <summary>
        /// 异步的执行事务的方法.
        /// </summary>
        /// <param name="sql"></param>
        public async Task<DataSet> ExcutSP_Async(string SPname, List<DbParameter> DbParameterS)
        {
            BllClass bc = await WC.CallWebAPI_Async(SPname, DbParameterS, "ExcutSP");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }


        /// <summary>
        /// 同步的取得DataSet的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet_Syn(string sql)
        {
            BllClass bc = WC.CallWebAPI_Syn(sql, "GetDataSet");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 同步的取得DataSet的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <returns></returns>
        public DataSet GetDataSet_Syn(string sql, string cuser)
        {
            BllClass bc = WC.CallWebAPI_Syn(sql, cuser, "GetDataSet_Log");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        /// <summary>
        /// 同步的执行事务的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExcutSqlTran_Syn(string sql)
        {
            BllClass bc = WC.CallWebAPI_Syn(sql, "ExcutSqlTran");
            return bc.JsonOut;
        }

        /// <summary>
        /// 同步的执行事务的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        /// <returns></returns>
        public string ExcutSqlTran_Syn(string sql, string cuser)
        {
            BllClass bc = WC.CallWebAPI_Syn(sql, cuser, "ExcutSqlTran_Log");
            return bc.JsonOut;
        }

        /// <summary>
        /// 执行存储过程,注意,调用服务的就没有out形式的入参了,这个失效
        /// </summary>
        /// <param name="SPname">存储过程名</param>
        /// <param name="DbParameterS">参数</param>
        /// <returns></returns>
        public DataSet ExcutSP_Syn(string SPname, List<DbParameter> DbParameterS)
        {
            BllClass bc = WC.CallWebAPI_Syn(SPname, DbParameterS, "ExcutSP");
            byte[] mbs = IoRyClass.StringToBytes(bc.JsonOut);
            DataSet ds = IoRyClass.RetrieveXmlDataSet(mbs);
            return ds;
        }

        #endregion
    }
}
