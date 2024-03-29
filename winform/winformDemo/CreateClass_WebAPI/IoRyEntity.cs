﻿﻿using System.Collections.Generic;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 2018-02 2020-4完善
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 用来取得表或者视图,并且转换成类的集合
 * VS版本 2010 2013 2015
 ***********************************************************************************/

namespace yezhanbafang
{
    class IoRyEntity<T> where T : IoRyRow, new()
    {
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
        /// !!!千万注意,用的同步方法,大数据量会报错,尽量异步取数据,然后用参数为DataTable的方法,这里适合字典等小table用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public BindingCollection<T> GetSortData_IoRyClass(string sql)
        {
            DataTable dt = IoRyFunction.IC.GetDataSet_Syn(sql).Tables[0];
            return this.GetSortData_IoRyClass(dt);
        }

        /// <summary>
        /// !!!千万注意,用的同步方法,大数据量会报错,尽量异步取数据,然后用参数为DataTable的方法,这里适合字典等小table用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cuser"></param>
        /// <returns></returns>
        public BindingCollection<T> GetSortData_IoRyClass(string sql, string cuser)
        {
            DataTable dt = IoRyFunction.IC.GetDataSet_Syn(sql, cuser).Tables[0];
            return this.GetSortData_IoRyClass(dt);
        }

        /// <summary>
        /// 取得List数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> GetData_IoRyClass(DataTable dt)
        {
            List<T> lt = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                t.SetData(dt.Rows[i]);
                lt.Add(t);
            }
            return lt;
        }

        /// <summary>
        /// !!!千万注意,用的同步方法,大数据量会报错,尽量异步取数据,然后用参数为DataTable的方法,这里适合字典等小table用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetData_IoRyClass(string sql)
        {
            DataTable dt = IoRyFunction.IC.GetDataSet_Syn(sql).Tables[0];
            return this.GetData_IoRyClass(dt);
        }

        /// <summary>
        /// !!!千万注意,用的同步方法,大数据量会报错,尽量异步取数据,然后用参数为DataTable的方法,这里适合字典等小table用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetData_IoRyClass(string sql, string cuser)
        {
            DataTable dt = IoRyFunction.IC.GetDataSet_Syn(sql, cuser).Tables[0];
            return this.GetData_IoRyClass(dt);
        }
    }
}
