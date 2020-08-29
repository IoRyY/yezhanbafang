using System;
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

namespace yezhanbafang
{
    /// <summary>
    /// 自定义前缀+表名称为类名
    /// </summary>
    public class View_1 : IoRyView
    {
        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public int? int_index { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string str_opreater { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string str_Type { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string str_tablename { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string str_Sql { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string str_Old { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public DateTime? dat_time { get; set; }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            int_index = dr.Field<int?>("int_index");
            str_opreater = dr.Field<string>("str_opreater");
            str_Type = dr.Field<string>("str_Type");
            str_tablename = dr.Field<string>("str_tablename");
            str_Sql = dr.Field<string>("str_Sql");
            str_Old = dr.Field<string>("str_Old");
            dat_time = dr.Field<DateTime?>("dat_time");
        }
    }
}