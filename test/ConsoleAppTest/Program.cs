using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using yezhanbafang;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string sqlmysql = "SELECT * FROM world.test_table;";
                string sqlmysql = "SELECT * FROM test_table where lie1='lie1s'; ";
                yezhanbafang.sd.MySQL.IoRyClass mysqlic = new yezhanbafang.sd.MySQL.IoRyClass();
                DataTable dtmysql = mysqlic.GetTable(sqlmysql);
                byte[] cun = (byte[])dtmysql.Rows[0]["lie4"];
                File.WriteAllBytes(@"D:\123.docx", cun);

                //                sqlmysql = $@"CREATE TABLE `mytable` (
                //  `id` int(11) NOT NULL AUTO_INCREMENT,
                //  `name` varchar(255) NOT NULL,
                //  `age` int(11) NOT NULL,
                //  `email` varchar(255) NOT NULL,
                //  PRIMARY KEY (`id`)
                //);";
                //sqlmysql = $@"insert into test_table (lie1,lie2,lie3) values ('lie1s','lie2s','lie3s');insert into test_table (lie4,lie2,lie3) values ('lie11','lie12','lie13');";
                //mysqlic.ExecuteSqlTran(sqlmysql);
                //sqlmysql = $@"update test_table set lie3='XXX' where lie1='lie1'";
                //sqlmysql = $@"delete from test_table where lie1='lie1'";
                //mysqlic.ExecuteSql(sqlmysql);

                byte[] binaryData = File.ReadAllBytes(@"D:\test.docx");
                //sqlmysql = $@"INSERT INTO test_table (lie1,lie2,lie3,lie4) VALUES ('lie1s','lie2s','lie3s',@file)";
                sqlmysql = $@"update test_table set lie4=@file where lie1='lie1s';";
                List<System.Data.Common.DbParameter> ld = new List<System.Data.Common.DbParameter>();
                MySqlParameter d1 = new MySqlParameter("@file", binaryData);
                ld.Add(d1);

                mysqlic.ExecuteSqlTran_DbParameter(sqlmysql, ld);

                Console.ReadLine();



                string sql = "select dept_name from dept_dict";
                yezhanbafang.sd.Oracle.IoRyClass ic = new yezhanbafang.sd.Oracle.IoRyClass();
                DataTable dt = ic.GetTable(sql);
                foreach (var item in dt.AsEnumerable())
                {
                    Console.WriteLine(item.Field<string>("dept_name"));
                }
                //Console.WriteLine(dt.Rows[0][0].ToString());
                Console.ReadLine();

                Console.WriteLine("Hello World!");
                IoRyTransaction it = new IoRyTransaction();
                Log_H lh = new Log_H();
                lh.str_opreater = "1";
                lh.Tran_IoRyAdd(it);
                Log_H lh2 = new Log_H();
                lh2.str_opreater = "2";
                lh2.Tran_IoRyAdd(it);
                it.Commit();
                Console.ReadLine();
                //    yezhanbafang.sd.WebAPI.DLL.Client.WebApiDLLClient wc = new yezhanbafang.sd.WebAPI.DLL.Client.WebApiDLLClient("https://localhost:44373/api/DLL", "config\\constring.xml");
                //    wc.GetDataSet_Syn(@"SELECT   IP_str as IP, UUID_GUID as UUID, key_str as [key], value_str as value, createtime_dt as 创建时间, changetime_dt as 修改时间, PC_config_GUID as ID
                //FROM      PC_config; ");
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
