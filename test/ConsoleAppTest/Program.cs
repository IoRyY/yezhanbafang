using System;
using System.Data;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                while (true)
                {
                    string sql = Console.ReadLine();
                    yezhanbafang.Oracle.IoRyClass ic = new yezhanbafang.Oracle.IoRyClass();
                    DataSet ds = ic.GetDataSet(sql);
                    string dts = yezhanbafang.Core.YezhanbafangCore.BytesToString(yezhanbafang.Core.YezhanbafangCore.GetXmlFormatDataSet(ds));
                    Console.WriteLine(dts);
                }
            }
            catch (Exception me)
            {
                Console.WriteLine(me.Message);
                Console.ReadLine();
            }
            
        }
    }
}
