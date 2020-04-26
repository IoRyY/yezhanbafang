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
                yezhanbafang.sd.WebAPI.DLL.Client.WebApiDLLClient wc = new yezhanbafang.sd.WebAPI.DLL.Client.WebApiDLLClient("https://localhost:44373/api/DLL", "config\\constring.xml");
                wc.GetDataSet_Syn(@"SELECT   IP_str as IP, UUID_GUID as UUID, key_str as [key], value_str as value, createtime_dt as 创建时间, changetime_dt as 修改时间, PC_config_GUID as ID
            FROM      PC_config; ");
                while (true)
                {
                    ////string sql = Console.ReadLine();
                    //string sql = "select name_str from uvt_data";
                    //yezhanbafang.sd.MSSQL.IoRyClass msic = new yezhanbafang.sd.MSSQL.IoRyClass("msconstring.xml");
                    //yezhanbafang.sd.OleDb.IoRyClass oleic = new yezhanbafang.sd.OleDb.IoRyClass("excelconstring");
                    ////ic1.Excel_Get()
                    //DataSet ds = msic.GetDataSet(sql);
                    //sql = "select * from [sheet1$]";
                    //DataSet ds1 = oleic.Excel_Get("H:\\a.xlsx", sql);
                    //string dts = yezhanbafang.sd.Core.YezhanbafangCore.BytesToString(yezhanbafang.sd.Core.YezhanbafangCore.GetXmlFormatDataSet(ds));
                    //Console.WriteLine(dts);
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
