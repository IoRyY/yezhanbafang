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
    public class log_data : IoRyRow
    {
        Guid? _log_data_GUID;
        /// <summary>
        /// 数据库log_data_GUID字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public Guid? log_data_GUID
        {
            get
            {
                return _log_data_GUID;
            }
            set
            {
                _log_data_GUID = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "log_data_GUID").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "log_data_GUID").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "log_data_GUID").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "log_data_GUID").First().ioryValue = Convert.ToString(value);
            }
        }

        string _sopreater_str;
        /// <summary>
        /// 数据库sopreater_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string sopreater_str
        {
            get
            {
                return _sopreater_str;
            }
            set
            {
                _sopreater_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "sopreater_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "sopreater_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "sopreater_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "sopreater_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _UUID_GUID_str;
        /// <summary>
        /// 数据库UUID_GUID_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string UUID_GUID_str
        {
            get
            {
                return _UUID_GUID_str;
            }
            set
            {
                _UUID_GUID_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "UUID_GUID_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "UUID_GUID_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "UUID_GUID_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "UUID_GUID_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _IP_str;
        /// <summary>
        /// 数据库IP_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string IP_str
        {
            get
            {
                return _IP_str;
            }
            set
            {
                _IP_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "IP_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "IP_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "IP_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "IP_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _type_str;
        /// <summary>
        /// 数据库type_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string type_str
        {
            get
            {
                return _type_str;
            }
            set
            {
                _type_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "type_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "type_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "type_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "type_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _tablename_str;
        /// <summary>
        /// 数据库tablename_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string tablename_str
        {
            get
            {
                return _tablename_str;
            }
            set
            {
                _tablename_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "tablename_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "tablename_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "tablename_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "tablename_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _SQL_str;
        /// <summary>
        /// 数据库SQL_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string SQL_str
        {
            get
            {
                return _SQL_str;
            }
            set
            {
                _SQL_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "SQL_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "SQL_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "SQL_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "SQL_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _olddata_str;
        /// <summary>
        /// 数据库olddata_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string olddata_str
        {
            get
            {
                return _olddata_str;
            }
            set
            {
                _olddata_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "olddata_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "olddata_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "olddata_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "olddata_str").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _createtime_dt;
        /// <summary>
        /// 数据库createtime_dt字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? createtime_dt
        {
            get
            {
                return _createtime_dt;
            }
            set
            {
                _createtime_dt = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "createtime_dt").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "createtime_dt").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "createtime_dt").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "createtime_dt").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            log_data_GUID = dr.Field<Guid?>("log_data_GUID");
            sopreater_str = dr.Field<string>("sopreater_str");
            UUID_GUID_str = dr.Field<string>("UUID_GUID_str");
            IP_str = dr.Field<string>("IP_str");
            type_str = dr.Field<string>("type_str");
            tablename_str = dr.Field<string>("tablename_str");
            SQL_str = dr.Field<string>("SQL_str");
            olddata_str = dr.Field<string>("olddata_str");
            createtime_dt = dr.Field<DateTime?>("createtime_dt");
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public log_data()
        {
            LIC.Add(new IoRyCol
            {
                ioryName = "log_data_GUID",
                ioryType = "Guid?",
                IsIdentity = false,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "sopreater_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "UUID_GUID_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "IP_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "type_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "tablename_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "SQL_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "olddata_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "createtime_dt",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
        }

        string tablename = "log_data";﻿
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
                    lscvalue.Add("'" + item.ioryValue.Replace("'", "''") + "'");
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
                    lscvalue.Add("'" + item.ioryValue.Replace("'", "''") + "'");
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