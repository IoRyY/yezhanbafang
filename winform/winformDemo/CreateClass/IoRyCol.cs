﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 用来控制每一列的各种属性,以便后续的增,删,改等处理
 * VS版本 2010 2013
 ***********************************************************************************/

namespace CreateDataTableTool
{
    public class IoRyCol
    {
        public string ioryName { get; set; }
        public string ioryType { get; set; }
        public bool IsKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNull { get; set; }
        public string ioryValue { get; set; }
        public bool ioryValueNull { get; set; }
        public bool ioryValueChange { get; set; }
    }


}
