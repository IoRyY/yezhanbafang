﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using yezhanbafang.fw.WCF.Client;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 2020-4修改
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 从类->数据库的操作,可以直接方式,可以wcf方式,可以webapi方式
 * VS版本 2010 2013
 ***********************************************************************************/

namespace yezhanbafang
{
    /// <summary>
    /// 此静态类执行执行同步,异步的请
    /// IoRyWCFClientV5.WCFClientV5 irf = new IoRyWCFClientV5.WCFClientV5(IoRyFunction.mxml, IoRyFunction.cOperator, IoRyFunction.url);
    /// irf.myAsnyGetDataSet += IC_myAsnyGetDataSet;
    /// irf.myProgressBar = this.progressBar1;
    /// irf.myGetDataSetAsync("select * from log_H", "vvv");
    /// void IC_myAsnyGetDataSet(DataSet DS, object obj)
    /// </summary>
    public static class IoRyFunction
    {
        public static string cOperator = Dns.GetHostName();
        public static string mxml = "erdai.xml";
        public static string url = "net.tcp://erdai.7xbj.com:9093/yuan";
        static WCFClientV5 ic = null;

        public static WCFClientV5 IC
        {
            get
            {
                if (ic == null)
                {
                    ic = new WCFClientV5(IoRyFunction.mxml, IoRyFunction.cOperator, IoRyFunction.url, true);
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