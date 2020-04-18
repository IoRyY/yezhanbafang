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
    public partial class Details : Form
    {
        ServiceMonitor SM = null;

        public Details()
        {
            InitializeComponent();
        }

        public Details(ServiceMonitor SMin)
        {
            InitializeComponent();
            SM = SMin;
        }

        private void Details_Load(object sender, EventArgs e)
        {
            try
            {
                treeView1.Nodes.AddRange(Servicefd.ServiceSession.Select(x => new TreeNode(x.Value)).ToArray());
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                this.richTextBox1.Text = e.Node.Text + "\r\n";
                //this.richTextBox1.Text += "Client:" + WcfServiceLibrary.Servicefd.ServiceSession[e.Node.Text];
                this.richTextBox1.Text += string.Join("\r\n", SM.rtb_message.Text.Split('$').
                    Where(x => x.Trim() != "").
                    Where(x => x.Split(';')[1].Contains(e.Node.Text)));
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }
    }
}
