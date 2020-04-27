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
    public class Users : IoRyRow
    {
        int? _Users_GUID;
        /// <summary>
        /// 数据库Users_GUID字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public int? Users_GUID
        {
            get
            {
                return _Users_GUID;
            }
            set
            {
                _Users_GUID = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "Users_GUID").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "Users_GUID").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "Users_GUID").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "Users_GUID").First().ioryValue = Convert.ToString(value);
            }
        }

        string _loginname_str;
        /// <summary>
        /// 数据库loginname_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string loginname_str
        {
            get
            {
                return _loginname_str;
            }
            set
            {
                _loginname_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "loginname_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "loginname_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "loginname_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "loginname_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _pwd_str;
        /// <summary>
        /// 数据库pwd_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string pwd_str
        {
            get
            {
                return _pwd_str;
            }
            set
            {
                _pwd_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "pwd_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "pwd_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "pwd_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "pwd_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _pwd_encrypt_str;
        /// <summary>
        /// 数据库pwd_encrypt_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string pwd_encrypt_str
        {
            get
            {
                return _pwd_encrypt_str;
            }
            set
            {
                _pwd_encrypt_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "pwd_encrypt_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "pwd_encrypt_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "pwd_encrypt_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "pwd_encrypt_str").First().ioryValue = Convert.ToString(value);
            }
        }

        string _name_str;
        /// <summary>
        /// 数据库name_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string name_str
        {
            get
            {
                return _name_str;
            }
            set
            {
                _name_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "name_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "name_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "name_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "name_str").First().ioryValue = Convert.ToString(value);
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

        string _power_str;
        /// <summary>
        /// 数据库power_str字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string power_str
        {
            get
            {
                return _power_str;
            }
            set
            {
                _power_str = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "power_str").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "power_str").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "power_str").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "power_str").First().ioryValue = Convert.ToString(value);
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

        DateTime? _changetime_dt;
        /// <summary>
        /// 数据库changetime_dt字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? changetime_dt
        {
            get
            {
                return _changetime_dt;
            }
            set
            {
                _changetime_dt = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "changetime_dt").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "changetime_dt").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "changetime_dt").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "changetime_dt").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            Users_GUID = dr.Field<int?>("Users_GUID");
            loginname_str = dr.Field<string>("loginname_str");
            pwd_str = dr.Field<string>("pwd_str");
            pwd_encrypt_str = dr.Field<string>("pwd_encrypt_str");
            name_str = dr.Field<string>("name_str");
            type_str = dr.Field<string>("type_str");
            power_str = dr.Field<string>("power_str");
            createtime_dt = dr.Field<DateTime?>("createtime_dt");
            changetime_dt = dr.Field<DateTime?>("changetime_dt");
            foreach (var item in LIC)
            {
                item.ioryValueChange = false;
            }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public Users()
        {
            LIC.Add(new IoRyCol
            {
                ioryName = "Users_GUID",
                ioryType = "int?",
                IsIdentity = true,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "loginname_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "pwd_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "pwd_encrypt_str",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "name_str",
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
                ioryName = "power_str",
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
            LIC.Add(new IoRyCol
            {
                ioryName = "changetime_dt",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
        }

        string tablename = "Users";﻿
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