﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace yezhanbafang.fw.WCF.Server
{
    public class UserInfo
    {
        public string SessionName { get; set; }
        public string QQName { get; set; }
        public Servicefd.CommanDelegate uCommanFunction { get; set; }
    }

    //20221201废弃
    //public class AsyncClass
    //{
    //    public byte[] bytes { get; set; }
    //    public string bytetype { get; set; }
    //    public string Name { get; set; }
    //}
}
