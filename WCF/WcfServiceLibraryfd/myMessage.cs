using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yezhanbafang.fw.WCF.Server
{
    public partial class myMessage : Form
    {
        public myMessage()
        {
            InitializeComponent();
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            foreach (var item in Servicefd.CommanList.Values)
            {
                try
                {
                    string mv = mycorrect(this.richTextBox1.Text, "ServiceSend");
                    item.uCommanFunction("[ServiceSend]", mv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 正确返回的xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string mycorrect(string data, string functionname)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + @"
<root>
  <correct>true</correct>
  <Exception></Exception>
  <Function>{1}</Function>
  <return>{0}</return>
</root>", data, functionname);
        }
    }
}
