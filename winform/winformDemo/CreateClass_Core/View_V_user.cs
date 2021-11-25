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
    public class V_user : IoRyView
    {
        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public Guid? index_int { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string loginname_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string pwd_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string pwd_encrypt_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string name_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string type_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public string power_display_str { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public DateTime? createtime_dt { get; set; }

        /// <summary>
        /// i前缀+列名为字段名
        /// </summary>
        public DateTime? changetime_dt { get; set; }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            index_int = dr.Field<Guid?>("index_int");
            loginname_str = dr.Field<string>("loginname_str");
            pwd_str = dr.Field<string>("pwd_str");
            pwd_encrypt_str = dr.Field<string>("pwd_encrypt_str");
            name_str = dr.Field<string>("name_str");
            type_str = dr.Field<string>("type_str");
            power_display_str = dr.Field<string>("power_display_str");
            createtime_dt = dr.Field<DateTime?>("createtime_dt");
            changetime_dt = dr.Field<DateTime?>("changetime_dt");
        }
    }
}