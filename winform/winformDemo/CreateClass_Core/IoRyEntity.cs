﻿using System.Collections.Generic;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 2017-09-05完善 2018-02-05完善 2020-3完善
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 用来取得表或者视图,并且转换成类的集合
 * VS版本 2010 2013 2015 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    public class IoRyEntity<T> where T : IoRyView, new()
    {

        /// <summary>
        /// 取得可以排序的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public BindingCollection<T> GetSortData_IoRyClass(string sql, string cuser)
        {
            DataTable dt = IoRyFunction.IC.Log_GetDataSet(sql, cuser).Tables[0];
            List<T> lt = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            IList<T> list = (IList<T>)lt;
            BindingCollection<T> bc = new BindingCollection<T>(list);
            return bc;
        }

        /// <summary>
        /// 取得可以排序的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public BindingCollection<T> GetSortData_IoRyClass(string sql)
        {
            DataTable dt = IoRyFunction.IC.GetTable(sql);
            List<T> lt = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            IList<T> list = (IList<T>)lt;
            BindingCollection<T> bc = new BindingCollection<T>(list);
            return bc;
        }

        /// <summary>
        /// 取得可以排序的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public BindingCollection<T> GetSortData_IoRyClass(DataTable dt)
        {
            List<T> lt = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            IList<T> list = (IList<T>)lt;
            BindingCollection<T> bc = new BindingCollection<T>(list);
            return bc;
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetData_IoRyClass(string sql)
        {
            List<T> lt = new List<T>();
            DataTable dt = IoRyFunction.IC.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            return lt;
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser"></param>
        /// <returns></returns>
        public List<T> GetData_IoRyClass(string sql, string cuser)
        {
            List<T> lt = new List<T>();
            DataTable dt = IoRyFunction.IC.Log_GetDataSet(sql, cuser).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            return lt;
        }
    }
}
