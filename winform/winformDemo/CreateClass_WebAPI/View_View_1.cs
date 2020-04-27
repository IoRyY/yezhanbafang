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
        public decimal? ceshi { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public double? cc { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string b { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string c { get; set; }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            int_index = dr.Field<int?>("int_index");
            ceshi = dr.Field<decimal?>("ceshi");
            cc = dr.Field<double?>("cc");
            b = dr.Field<string>("b");
            c = dr.Field<string>("c");
        }
    }
}