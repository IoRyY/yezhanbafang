﻿﻿﻿﻿using System.Collections.Generic;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 20200325
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 泛型转换需要的interface,如果以后要换掉此方法,只要实现了此接口即可复用业务代码
 * VS版本 2010 2013 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    /// <summary>
    /// 视图的接口
    /// </summary>
    public interface IoRyView
    {
        /// <summary>
        /// 给行填充数据的方法
        /// </summary>
        /// <param name="dr">要填充的View</param>
        void SetData(DataRow dr);
    }

    /// <summary>
    /// 行处理接口
    /// </summary>
    public interface IoRyRow : IoRyView
    {
        /// <summary>
        /// 普通新增
        /// </summary>
        void IoRyAdd();

        /// <summary>
        /// 带日志的新增
        /// </summary>
        /// <param name="cuser">操作者</param>
        void IoRyAdd(string cuser);

        /// <summary>
        /// 普通修改 走默认主键
        /// </summary>
        void IoRyUpdate();

        /// <summary>
        /// 带日志的普通修改 走默认主键
        /// </summary>
        /// <param name="cuser">操作者</param>
        void IoRyUpdate(string cuser);

        /// <summary>
        /// 自定义where条件的修改
        /// </summary>
        /// <param name="keys">数据库表的字段的集合 and连接</param>
        void IoRyUpdate(List<string> keys);

        /// <summary>
        /// 带日志的 自定义where条件的修改
        /// </summary>
        /// <param name="keys">数据库表的字段的集合 and连接</param>
        /// <param name="cuser">操作者</param>
        void IoRyUpdate(List<string> keys, string cuser);

        /// <summary>
        /// 普通删除 走默认主键
        /// </summary>
        void IoRyDelete();

        /// <summary>
        /// 带日志的 普通删除 走默认主键
        /// </summary>
        /// <param name="cuser">操作者</param>
        void IoRyDelete(string cuser);

        /// <summary>
        /// 自定义where条件的删除
        /// </summary>
        /// <param name="keys">数据库表的字段的集合 and连接</param>
        void IoRyDelete(List<string> keys);

        /// <summary>
        /// 带日志的 自定义where条件的删除
        /// </summary>
        /// <param name="keys">数据库表的字段的集合 and连接</param>
        /// <param name="cuser">操作者</param>
        void IoRyDelete(List<string> keys, string cuser);
    }

    /// <summary>
    /// 存储过程处理接口
    /// </summary>
    public interface IoRyProcedure
    {
        /// <summary>
        /// 执行存储过程,返回dataset,提供out形式出参
        /// </summary>
        /// <returns></returns>
        DataSet IoRyExcute();
    }
}
