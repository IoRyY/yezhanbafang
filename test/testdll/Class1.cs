using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testdll
{
    public class Class1 : yezhanbafang.fw.WCF.ServiceDllinterface
    {
        public string ServiceFunction(string json)
        {
            return jisuan(Convert.ToInt32(json.Split(',')[0]), Convert.ToInt32(json.Split(',')[0])).ToString();
        }


        int jisuan(int a, int b)
        {
            return a + b;
        }
    }
}
