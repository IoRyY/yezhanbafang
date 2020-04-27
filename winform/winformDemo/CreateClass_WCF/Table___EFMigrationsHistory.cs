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
    public class __EFMigrationsHistory : IoRyRow
    {
        string _MigrationId;
        /// <summary>
        /// 数据库MigrationId字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string MigrationId
        {
            get
            {
                return _MigrationId;
            }
            set
            {
                _MigrationId = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "MigrationId").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "MigrationId").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "MigrationId").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "MigrationId").First().ioryValue = Convert.ToString(value);
            }
        }

        string _ProductVersion;
        /// <summary>
        /// 数据库ProductVersion字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string ProductVersion
        {
            get
            {
                return _ProductVersion;
            }
            set
            {
                _ProductVersion = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "ProductVersion").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "ProductVersion").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "ProductVersion").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "ProductVersion").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            MigrationId = dr.Field<string>("MigrationId");
            ProductVersion = dr.Field<string>("ProductVersion");
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public __EFMigrationsHistory()
        {
            LIC.Add(new IoRyCol
            {
                ioryName = "MigrationId",
                ioryType = "string",
                IsIdentity = false,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "ProductVersion",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
        }

        string tablename = "__EFMigrationsHistory";﻿
        /// <summary>
        /// LIC是列集合
        /// i前缀+列名为字段名
        /// </summary>
        List<IoRyCol> LIC = new List<IoRyCol>();

        /// <summary>
        /// 普通新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd()
        {
            string sqlp = " insert into " + tablename + " ({0}) values ({1})";
            List<string> lscname = new List<string>();
            List<string> lscvalue = new List<string>();
            foreach (IoRyCol item in this.LIC)
            {
                if (item.ioryValueNull == false && item.IsIdentity == false)
                {
                    lscname.Add(item.ioryName);
                    lscvalue.Add("'" + item.ioryValue.Replace('\'', '"') + "'");
                }
            }
            if (lscname.Count == 0)
            {
                throw new Exception("新增的类必须有值!");
            }
            string sql = string.Format(sqlp, string.Join(",", lscname), string.Join(",", lscvalue));
            IoRyFunction.CallIoRyClass(sql);
        }

        /// <summary>
        /// 带Log新增
        /// </summary>
        /// <returns></returns>
        public void IoRyAdd(string cuser)
        {
            string sqlp = " insert into " + tablename + " ({0}) values ({1})";
            List<string> lscname = new List<string>();
            List<string> lscvalue = new List<string>();
            foreach (IoRyCol item in this.LIC)
            {
                if (item.ioryValueNull == false && item.IsIdentity == false)
                {
                    lscname.Add(item.ioryName);
                    lscvalue.Add("'" + item.ioryValue.Replace('\'', '"') + "'");
                }
            }
            if (lscname.Count == 0)
            {
                throw new Exception("新增的类必须有值!");
            }
            string sql = string.Format(sqlp, string.Join(",", lscname), string.Join(",", lscvalue));
            IoRyFunction.CallIoRyClass(sql, cuser);
        }

        /// <summary>
        /// 自定义where 修改
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyUpdate(List<string> keys)
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
                                lsset.Add(item.ioryName + "='" + item.ioryValue.Replace('\'', '"') + "'");
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
                IoRyFunction.CallIoRyClass(sql);
            }
            else
            {
                throw new Exception("此数据没有修改!");
            }
        }

        /// <summary>
        /// 自定义where 带Log 修改
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyUpdate(List<string> keys, string cuser)
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
                                lsset.Add(item.ioryName + "='" + item.ioryValue.Replace('\'', '"') + "'");
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
                IoRyFunction.CallIoRyClass(sql, cuser);
            }
            else
            {
                throw new Exception("此数据没有修改!");
            }
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
        /// 带Log修改 以keys为where
        /// </summary>
        /// <param name="cuser"></param>
        public void IoRyUpdate(string cuser)
        {
            List<string> ls = LIC.Where(x => x.IsKey == true).Select(x => x.ioryName).ToList();
            this.IoRyUpdate(ls, cuser);
        }

        /// <summary>
        /// 普通删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        public void IoRyDelete(List<string> keys)
        {
            string sqlp = "delete " + tablename + " where {0}";
            List<string> lswhere = new List<string>();
            foreach (var item in keys)
            {
                string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                lswhere.Add(item + "='" + mv + "'");
            }
            string sql = string.Format(sqlp, string.Join(" and ", lswhere));
            IoRyFunction.CallIoRyClass(sql);
        }

        /// <summary>
        /// 带Log删除 自定义where
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cuser"></param>
        public void IoRyDelete(List<string> keys, string cuser)
        {
            string sqlp = "delete " + tablename + " where {0}";
            List<string> lswhere = new List<string>();
            foreach (var item in keys)
            {
                string mv = LIC.Where(x => x.ioryName == item).First().ioryValue;
                lswhere.Add(item + "='" + mv + "'");
            }
            string sql = string.Format(sqlp, string.Join(" and ", lswhere));
            IoRyFunction.CallIoRyClass(sql, cuser);
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