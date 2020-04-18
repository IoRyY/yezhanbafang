using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo
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
            Application.Run(new Login());
        }

        static public userClass uClass { get; set; }

        static string _PCIP = null;
        static public string PCIP
        {
            get
            {
                if (_PCIP == null)
                {
                    SelectQuery query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            if (Convert.ToBoolean(item["IPEnabled"]))
                            {
                                _PCIP = ((string[])item["IPAddress"])[0];
                            }
                        }
                    }
                }
                return _PCIP;
            }
        }

        static string _UUID = null;
        static public string UUID
        {
            get
            {
                if (_UUID == null)
                {
                    SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (var item in searcher.Get())
                        {
                            _UUID = item["UUID"].ToString();
                        }
                    }
                }
                return _UUID;
            }
        }
    }
}
