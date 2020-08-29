﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using yezhanbafang.fw.Core;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 20200326 新增接口实现以及存储过程 20200418 修改命名空间
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 从类->数据库的操作,可以直接方式,可以wcf方式,可以webapi方式
 * VS版本 2010 2013 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    public static class IoRyFunction
    {
        static IoRyClass ic = null;
        public static string IoRyClassXmlPath = "constring.xml";
﻿﻿
        public static IoRyClass IC
        {
            get
            {
                if (ic == null)
                {
                    ic = new IoRyClass(AppDomain.CurrentDomain.BaseDirectory + IoRyClassXmlPath);
﻿                }
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
                IC.ExecuteSqlTran(sql);
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
                IC.Log_ExecuteSqlTran(sql, cuser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="SPname">存储过程名称</param>
        /// <param name="ld">入参</param>
        /// <returns></returns>
        public static DataSet CallIoRyProcedure(string SPname, List<DbParameter> ld)
        {
            return IoRyFunction.IC.ExecuteSP(SPname, ld);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="SPname">存储过程名称</param>
        /// <param name="ld">入参</param>
        /// <param name="cuser">执行者</param>
        /// <returns></returns>
        public static DataSet CallIoRyProcedure(string SPname, List<DbParameter> ld, string cuser)
        {
            return IoRyFunction.IC.Log_ExecuteSP(SPname, ld, cuser);
        }
    }
}