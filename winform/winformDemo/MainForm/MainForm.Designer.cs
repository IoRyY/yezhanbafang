namespace yezhanbafang.fw.winform.Demo.MainForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("用户管理例子");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("PC本地配置");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("查看Log");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("窗体集合", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_user = new System.Windows.Forms.Label();
            this.lb_formname = new System.Windows.Forms.Label();
            this.lb_appname = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 717);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1274, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel1.Text = "Version:1.0.0.0";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_user);
            this.panel1.Controls.Add(this.lb_formname);
            this.panel1.Controls.Add(this.lb_appname);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 717);
            this.panel1.TabIndex = 3;
            // 
            // lb_user
            // 
            this.lb_user.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_user.Location = new System.Drawing.Point(15, 42);
            this.lb_user.Name = "lb_user";
            this.lb_user.Size = new System.Drawing.Size(165, 16);
            this.lb_user.TabIndex = 3;
            this.lb_user.Text = "用户:";
            this.lb_user.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_formname
            // 
            this.lb_formname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_formname.Location = new System.Drawing.Point(15, 70);
            this.lb_formname.Name = "lb_formname";
            this.lb_formname.Size = new System.Drawing.Size(165, 16);
            this.lb_formname.TabIndex = 2;
            this.lb_formname.Text = "程序首页";
            this.lb_formname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_appname
            // 
            this.lb_appname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_appname.Location = new System.Drawing.Point(12, 14);
            this.lb_appname.Name = "lb_appname";
            this.lb_appname.Size = new System.Drawing.Size(168, 16);
            this.lb_appname.TabIndex = 1;
            this.lb_appname.Text = "系统名称或图片";
            this.lb_appname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ItemHeight = 40;
            this.treeView1.Location = new System.Drawing.Point(7, 100);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点1";
            treeNode1.Text = "用户管理例子";
            treeNode1.ToolTipText = "用户管理例子";
            treeNode2.Name = "节点2";
            treeNode2.Text = "PC本地配置";
            treeNode2.ToolTipText = "PC本地配置";
            treeNode3.Name = "节点3";
            treeNode3.Text = "查看Log";
            treeNode3.ToolTipText = "查看Log";
            treeNode4.Name = "节点0";
            treeNode4.Text = "窗体集合";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Scrollable = false;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(190, 728);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 739);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lb_appname;
        private System.Windows.Forms.Label lb_formname;
        private System.Windows.Forms.Label lb_user;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}