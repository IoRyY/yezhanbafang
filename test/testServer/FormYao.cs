using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using yezhanbafang.fw.WCF.Client;

namespace Naml.MyForms
{
    public partial class FormYao : Form
    {
        public FormYao()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = this.openFileDialog1.FileName;
                string serveranme = DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("HHmmss") + "_" + Path.GetFileName(filename);
                WCFClientV5 wc = new WCFClientV5("", "test", this.textBox2.Text);
                wc.myProgressBar = this.progressBar2;
                wc.MyButtons = new List<Button>() { this.button1 };
                if (wc.SendFile2Server(filename, serveranme))
                {
                }
                else
                {
                    MessageBox.Show("已经存在此文件,不能覆盖");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string serveranme = this.textBox1.Text;
            this.saveFileDialog1.FileName = Path.GetFileName(serveranme);
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WCFClientV5 wc = new WCFClientV5("", "test", this.textBox2.Text);
                wc.myProgressBar = this.progressBar1;
                wc.MyButtons = new List<Button>() { this.button2 };
                if (wc.GetFileFromServer(this.saveFileDialog1.FileName, serveranme))
                {

                }
                else
                {
                    MessageBox.Show("未找到此文件,不能下载");
                }
            }
        }

        private void FormYao_Load(object sender, EventArgs e)
        {
        }

        private void bt_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("真的真的确认?????删除后无法撤回,请再仔细确认,不要误删除了别的病人信息!!!!!!!!!!!!", "真的真的确认????", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    WCFClientV5 wc1 = new WCFClientV5("", "test", this.textBox2.Text);
                    string serveranme = this.textBox1.Text;
                    wc1.DeleteServerFile(serveranme);
                }
            }
            catch
            {
                MessageBox.Show("只有基础数据能够删除,请正确选择要删除的数据!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WCFClientV5 wc1 = new WCFClientV5("", "test", this.textBox2.Text);
            wc1.DuxMessage += Wc1_DuxMessage;
            wc1.GetFolderFileXml("UpdateFiles");
        }

        private void Wc1_DuxMessage(string content, string Exstr)
        {
            if (Exstr == "GetFolderFileXml")
            {
                this.treeView1.Nodes.Clear();
                XElement xmdata = XElement.Parse(content);
                populateTreeControl(xmdata.Element("return"), this.treeView1.Nodes);
                this.treeView1.ExpandAll();
            }
        }

        private void populateTreeControl(XElement document, System.Windows.Forms.TreeNodeCollection nodes)
        {
            foreach (XElement node in document.Elements())
            {
                TreeNode new_child = null;
                if (node.Attributes().Count() > 0)
                {
                    new_child = new TreeNode(node.Attributes().First().Value);
                }
                else
                {
                    new_child = new TreeNode(node.Value);
                }
                nodes.Add(new_child);
                populateTreeControl(node, new_child.Nodes);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.textBox1.Text = this.treeView1.SelectedNode.Text.Replace("UpdateFiles", "");
        }
    }
}
