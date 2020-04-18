﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/************************************************************************************
 * 作者 袁东辉 时间：2016-1
 * Email windy_23762872@126.com 253625488@qq.com
 * 作用 泛型转换需要的interface
 * VS版本 2010 2013
 ***********************************************************************************/

namespace CreateDataTableTool
{
    public interface IoRyRow
    {
        void SetData(DataRow dr);
    }
}
