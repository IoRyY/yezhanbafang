using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using System.Configuration;
using System.Diagnostics;

namespace Service
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost sh = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sh == null)
                {
                    sh = new ServiceHost(typeof(yezhanbafang.fw.WCF.Server.Servicefd));
                    sh.Open();
                    this.label1.Content = "服务已成功启动...";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sh != null)
                {
                    sh.Abort();
                    sh.Close();
                    sh = null;
                    this.label1.Content = "服务已关闭...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 增加了打开窗体自启动服务,这样可以重启服务器后服务直接启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo("yezhanbafang.fw.WCF.Server.dll");

                this.label.Content = "宿主版本:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    + "\r\nDLL版本:" + myFileVersion.ProductVersion;
                this.Title = ConfigurationManager.AppSettings["name"];
                this.label2.Content = ConfigurationManager.AppSettings["name"];
                if (sh == null)
                {
                    sh = new ServiceHost(typeof(yezhanbafang.fw.WCF.Server.Servicefd));
                    sh.Open();
                    this.label1.Content = "服务已成功启动...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
