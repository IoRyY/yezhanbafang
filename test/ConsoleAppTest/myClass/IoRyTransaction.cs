﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yezhanbafang
{
    /// <summary>
    /// 大家百般要求的事务一致性
    /// </summary>
    public class IoRyTransaction
    {
        /// <summary>
        /// 要执行的sql语句
        /// </summary>
        public string Sql { get; set; } = "";
        /// <summary>
        /// 执行事务
        /// </summary>
        public void Commit()
        {
            IoRyFunction.CallIoRyClass(this.Sql);
        }
    }
}
