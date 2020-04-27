﻿using System;
using yezhanbafang.fw.WebAPI.DLL.Client;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 2020-4修改
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 从类->数据库的操作,可以直接方式,可以wcf方式,可以webapi方式
 * VS版本 2010 2013 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    public static class IoRyFunction
    {
        public static string IoRyClassXmlPath = "config\\constring.xml";
        public static string WebApiUrl = "https://localhost:44373/api/DLL";﻿
        static WebApiDLLClient ic = null;

        public static WebApiDLLClient IC
        {
            get
            {
                if (ic == null)
                {
                    ic = new WebApiDLLClient(IoRyFunction.WebApiUrl, IoRyFunction.IoRyClassXmlPath);
                }
                return ic;
            }
        }

        /// <summary>
        /// 执行sql语句们(仅对sqlserver)
        /// </summary>
        /// <param name="sql"></param>
        public static void CallIoRyClass(string sql)
        {
            try
            {
                IC.ExcutSqlTran_Syn(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句with Log
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser">执行者</param>
        public static void CallIoRyClass(string sql, string cuser)
        {
            try
            {
                IC.ExcutSqlTran_Syn(sql, cuser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}