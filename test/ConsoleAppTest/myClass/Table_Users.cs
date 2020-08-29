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
        int? _int_index;
        /// <summary>
        /// 数据库int_index字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public int? int_index
        {
            get
            {
                return _int_index;
            }
            set
            {
                _int_index = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "int_index").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "int_index").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "int_index").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "int_index").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_loginname;
        /// <summary>
        /// 数据库str_loginname字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_loginname
        {
            get
            {
                return _str_loginname;
            }
            set
            {
                _str_loginname = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_loginname").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_loginname").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_loginname").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_loginname").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_pwd;
        /// <summary>
        /// 数据库str_pwd字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_pwd
        {
            get
            {
                return _str_pwd;
            }
            set
            {
                _str_pwd = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_pwd").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_pwd").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_pwd").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_pwd").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_name;
        /// <summary>
        /// 数据库str_name字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_name
        {
            get
            {
                return _str_name;
            }
            set
            {
                _str_name = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_name").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_name").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_name").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_name").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_type;
        /// <summary>
        /// 数据库str_type字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_type
        {
            get
            {
                return _str_type;
            }
            set
            {
                _str_type = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_type").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_type").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_type").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_type").First().ioryValue = Convert.ToString(value);
            }
        }

        string _str_power;
        /// <summary>
        /// 数据库str_power字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public string str_power
        {
            get
            {
                return _str_power;
            }
            set
            {
                _str_power = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "str_power").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "str_power").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "str_power").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "str_power").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_createtime;
        /// <summary>
        /// 数据库dat_createtime字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_createtime
        {
            get
            {
                return _dat_createtime;
            }
            set
            {
                _dat_createtime = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_createtime").First().ioryValue = Convert.ToString(value);
            }
        }

        DateTime? _dat_changetime;
        /// <summary>
        /// 数据库dat_changetime字段
        /// </summary>
        [IoRyDisPlay(DisplayName ="")]
        public DateTime? dat_changetime
        {
            get
            {
                return _dat_changetime;
            }
            set
            {
                _dat_changetime = value;
                if (value == null)
                {
                    LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueNull = true;
                }
                else
                {
                    LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueNull = false;
                }
                LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValueChange = true;
                LIC.Where(x => x.ioryName == "dat_changetime").First().ioryValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// 实现IoRyTable的接口
        /// </summary>
        public void SetData(DataRow dr)
        {
            int_index = dr.Field<int?>("int_index");
            str_loginname = dr.Field<string>("str_loginname");
            str_pwd = dr.Field<string>("str_pwd");
            str_name = dr.Field<string>("str_name");
            str_type = dr.Field<string>("str_type");
            str_power = dr.Field<string>("str_power");
            dat_createtime = dr.Field<DateTime?>("dat_createtime");
            dat_changetime = dr.Field<DateTime?>("dat_changetime");
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
                ioryName = "int_index",
                ioryType = "int?",
                IsIdentity = true,
                IsKey = true,
                IsNull = false,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_loginname",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_pwd",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_name",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_type",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "str_power",
                ioryType = "string",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_createtime",
                ioryType = "DateTime?",
                IsIdentity = false,
                IsKey = false,
                IsNull = true,
                ioryValueNull = true,
                ioryValueChange = false
            });
            LIC.Add(new IoRyCol
            {
                ioryName = "dat_changetime",
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