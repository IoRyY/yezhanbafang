using System;
using System.Diagnostics;
using System.ServiceModel;

namespace yezhanbafang.fw.WCF.Host.Console
{
    class Program
    {
        static ServiceHost sh = null;
        static void Main(string[] args)
        {
            try
            {
                FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo("yezhanbafang.fw.WCF.ConsoleServer.dll");
                string banben = "宿主版本:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    + "\r\nDLL版本:" + myFileVersion.ProductVersion;
                System.Console.WriteLine(banben);
                if (sh == null)
                {
                    sh = new ServiceHost(typeof(yezhanbafang.fw.WCF.Server.Servicefd));
                    sh.Open();
                    System.Console.WriteLine("服务已成功启动...");
                }

                //FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo("yezhanbafang.fw.WCF.LoadBalance.Server.dll");
                //string banben = "宿主版本:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                //    + "\r\nDLL版本:" + myFileVersion.ProductVersion;
                //System.Console.WriteLine(banben);
                //if (sh == null)
                //{
                //    sh = new ServiceHost(typeof(LoadBalanceService));
                //    sh.Open();
                //    System.Console.WriteLine("服务已成功启动...");
                //}
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            while(true)
            {
                System.Console.ReadLine();
            }
           
        }
    }
}
