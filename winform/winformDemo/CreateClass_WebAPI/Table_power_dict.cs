using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1 最后修改时间：2020-3
 * Email windy_23762872@126.com 253625488@qq.com
 * 博客 2016BigProject http://blog.csdn.net/yuandonghuia/article/details/50514985
 * 作用 代码生成器生成的Table类
 * VS版本 2010 2013 2015 2019
 ***********************************************************************************/

namespace yezhanbafang
{
    /// <summary>
    /// 自定义前缀+表名称为类名
    /// </summary>
    public class power_dict : IoRyRow
    {
        string _power_dict_str;
        /// <summary>
        /// 数据库power_dict_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string power_dict_str
        {
            get
            {
                return _power_dict_str;
            }
            set
            {
                _power_dict_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "power_dict_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "power_dict_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "power_dict_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "power_dict_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _power_display_str;
        /// <summary>
        /// 数据库power_display_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string power_display_str
        {
            get
            {
                return _power_display_str;
            }
            set
            {
                _power_display_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "power_display_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "power_display_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "power_display_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "power_display_str").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            power_dict_str = dr.Field<string>("power_dict_str");
            power_display_str = dr.Field<string>("power_display_str");
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public power_dict()
        {
            LIC.Add(new IoRyCol
            {
                ioryName = "power_dict_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "power_display_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
        }

        string tablename = "power_dict";﻿
        /// <summary>
        /// LIC是列集合
        /// i前缀+列名为字段名
        /// </summary>
        List<IoRyCol> LIC = new List<IoRyCol>();

        /// <summary>
        /// 获取新增方法的Sql语句
        /// </summary>
        /// <returns></returns>
        string IoRyAdd_Sql()
        {
            string sqlp = " insert into " + tablename + " ({0}) values ({1})";
            List<string> lscname = new List<string>();
            List<string> lscvalue = new List<string>();
            foreach (IoRyCol item in this.LIC)
            {
                if (item.ioryValueNull == false && item.IsIdentity == false)
                {
                    lscname.Add(item.ioryName);
                    lscvalue.Add("'" + item.ioryValue.Replace("'", "''") + "'");
                }
            }
            if (lscname.Count == 0)
            {
                throw new Exception("新增的类必须有值!");
            }
            string sql = string.Format(sqlp, string.Join(",", lscname), string.Join(",", lscvalue));
            return sql;
        }

        /// <summary>
        /// 普通新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd()
        {
            IoRyFunction.CallIoRyClass(this.IoRyAdd_Sql());
        }

        /// <summary>
        /// 普通新增 事务
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyAdd(IoRyTransaction tran)
        {
            tran.Sql += this.IoRyAdd_Sql() + " ;";
        }

        /// <summary>
        /// 带Log新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd(string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyAdd_Sql(), cuser);
        }

        /// <summary>
        /// 获取更新方法的Sql语句
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string IoRyUpdate_Sql(List<string> keys)
        {
            string sqlp = "update " + tablename + " set {0} where {1}";
            List<string> lsset = new List<string>();
            List<string> lswhere = new List<string>();
            if (LIC.Any(x => x.ioryValueChange == true))
            {
                foreach (var item in LIC)
                {
                    if (item.ioryValueChange == true)
                    {
                        if (!keys.Contains(item.ioryName))
                        {
                            if (item.ioryValueNull)
                            {
                                lsset.Add(item.ioryName + " = null ");
                            }
                            else
                            {
                                lsset.Add(item.ioryName + "='" + item.ioryValue.Replace("'", "''") + "'");
                            }
                        }
                    }
                }
                foreach (var item in keys)
                {
                    string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                    lswhere.Add(item + "='" + mv + "'");
                }
                string sql = string.Format(sqlp, string.Join(",", lsset), string.Join(" and ", lswhere));
                return sql;
            }
            else
            {
                throw new Exception("此数据没有修改!");
            }
        }

        /// <summary>
        /// 自定义where 修改
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyUpdate(List<string> keys)
        {
            IoRyFunction.CallIoRyClass(this.IoRyUpdate_Sql(keys));
        }

        /// <summary>
        /// 自定义where 修改 事务
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyUpdate(IoRyTransaction tran, List<string> keys)
        {
            tran.Sql += this.IoRyUpdate_Sql(keys) + " ;";
        }

        /// <summary>
        /// 自定义where 带Log 修改
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyUpdate(List<string> keys, string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyUpdate_Sql(keys), cuser);
        }

        /// <summary>
        /// 普通修改 以keys为where
        /// </summary>
        public void IoRyUpdate()
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyUpdate(ls);
        }

        /// <summary>
        /// 普通修改 事务 以keys为where
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyUpdate(IoRyTransaction tran)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            tran.Sql += this.IoRyUpdate_Sql(ls) + " ;";
        }

        /// <summary>
        /// 带Log修改 以keys为where
        /// </summary>
        /// <param name="cuser"></param>
        public void IoRyUpdate(string cuser)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyUpdate(ls, cuser);
        }

        /// <summary>
        /// 获取删除方法的Sql语句
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string IoRyDelete_Sql(List<string> keys)
        {
            string sqlp = "delete " + tablename + " where {0}";
            List<string> lswhere = new List<string>();
            foreach (var item in keys)
            {
                string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                lswhere.Add(item + "='" + mv + "'");
            }
            string sql = string.Format(sqlp, string.Join(" and ", lswhere));
            return sql;
        }


        /// <summary>
        /// 普通删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyDelete(List<string> keys)
        {
            IoRyFunction.CallIoRyClass(this.IoRyDelete_Sql(keys));
        }

        /// <summary>
        /// 普通删除 事务 自定义where
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="keys"></param>
        public void Tran_IoRyDelete(IoRyTransaction tran, List<string> keys)
        {
            tran.Sql += this.IoRyDelete_Sql(keys) + " ;";
        }

        /// <summary>
        /// 带Log删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyDelete(List<string> keys, string cuser)
        {
            IoRyFunction.CallIoRyClass(this.IoRyDelete_Sql(keys), cuser);
        }

        /// <summary>
        /// 普通删除 以keys为where 
        /// </summary>
        public void IoRyDelete()
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyDelete(ls);
        }

        /// <summary>
        /// 普通删除 事务 以keys为where 
        /// </summary>
        /// <param name="tran"></param>
        public void Tran_IoRyDelete(IoRyTransaction tran)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            tran.Sql += this.IoRyDelete_Sql(ls) + " ;";
        }

        /// <summary>
        /// 带Log删除 以keys为where
        /// </summary>
        /// <param name="cuser"></param>
        public void IoRyDelete(string cuser)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyDelete(ls, cuser);
        }
    }
}