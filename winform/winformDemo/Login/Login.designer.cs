namespace yezhanbafang.fw.winform.Demo
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.bt_OK = new System.Windows.Forms.Button();
            this.lb_title = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_copyright = new System.Windows.Forms.Label();
            this.lb_v = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(539, 395);
            this.txt_username.Multiline = true;
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(159, 22);
            this.txt_username.TabIndex = 0;
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(539, 432);
            this.txt_pwd.Multiline = true;
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.PasswordChar = '*';
            this.txt_pwd.Size = new System.Drawing.Size(159, 22);
            this.txt_pwd.TabIndex = 1;
            // 
            // bt_OK
            // 
            this.bt_OK.BackColor = System.Drawing.Color.Transparent;
            this.bt_OK.Location = new System.Drawing.Point(-826, 266);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(66, 59);
            this.bt_OK.TabIndex = 3;
            this.bt_OK.UseVisualStyleBackColor = false;
            this.bt_OK.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.Transparent;
            this.lb_title.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(268, 312);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(530, 33);
            this.lb_title.TabIndex = 4;
            this.lb_title.Text = "XXXXXXXXXX信息系统";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(265, 266);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(533, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(704, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 59);
            this.label1.TabIndex = 6;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lb_copyright
            // 
            this.lb_copyright.BackColor = System.Drawing.Color.Transparent;
            this.lb_copyright.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_copyright.ForeColor = System.Drawing.Color.White;
            this.lb_copyright.Location = new System.Drawing.Point(715, 727);
            this.lb_copyright.Name = "lb_copyright";
            this.lb_copyright.Size = new System.Drawing.Size(295, 33);
            this.lb_copyright.TabIndex = 7;
            this.lb_copyright.Text = "版权所属:XXXXXXXXXXXX";
            this.lb_copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_v
            // 
            this.lb_v.AutoSize = true;
            this.lb_v.BackColor = System.Drawing.Color.Transparent;
            this.lb_v.ForeColor = System.Drawing.SystemColors.Window;
            this.lb_v.Location = new System.Drawing.Point(903, 9);
            this.lb_v.Name = "lb_v";
            this.lb_v.Size = new System.Drawing.Size(59, 12);
            this.lb_v.TabIndex = 8;
            this.lb_v.Text = "v:0.0.0.0";
            // 
            // Login
            // 
            this.AcceptButton = this.bt_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1014, 764);
            this.Controls.Add(this.lb_v);
            this.Controls.Add(this.lb_copyright);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.bt_OK);
            this.Controls.Add(this.txt_pwd);
            this.Controls.Add(this.txt_username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XXXXXXXX信息系统";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_copyright;
        private System.Windows.Forms.Label lb_v;
    }
}

