using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace yezhanbafang.fw.MSSqlBakUp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class SMSreturn
    {
        public bool IsNormal { get; set; }
        public string errorInfo { get; set; }
    }

    public class XYSserver
    {
        public string serverName { get; set; }
        public string errorInfo { get; set; }
        public string phone { get; set; }
    }
}
