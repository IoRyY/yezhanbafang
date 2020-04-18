namespace yezhanbafang.fw.winform.Demo.forms
{
    partial class PCconfig
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
            this.bt_IP = new System.Windows.Forms.Button();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.bt_global = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_chaxun
            // 
            this.bt_chaxun.Click += new System.EventHandler(this.bt_chaxun_Click);
            // 
            // bt_OK
            // 
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_global);
            this.groupBox2.Controls.Add(this.txt_IP);
            this.groupBox2.Controls.Add(this.bt_IP);
            this.groupBox2.Controls.SetChildIndex(this.bt_Add, 0);
            this.groupBox2.Controls.SetChildIndex(this.bt_change, 0);
            this.groupBox2.Controls.SetChildIndex(this.bt_IP, 0);
            this.groupBox2.Controls.SetChildIndex(this.txt_IP, 0);
            this.groupBox2.Controls.SetChildIndex(this.bt_global, 0);
            // 
            // bt_change
            // 
            this.bt_change.Location = new System.Drawing.Point(137, 42);
            this.bt_change.Click += new System.EventHandler(this.bt_change_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Text = "本地配置";
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // bt_IP
            // 
            this.bt_IP.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_IP.Location = new System.Drawing.Point(247, 42);
            this.bt_IP.Name = "bt_IP";
            this.bt_IP.Size = new System.Drawing.Size(123, 40);
            this.bt_IP.TabIndex = 19;
            this.bt_IP.Text = "IP新增配置";
            this.bt_IP.UseVisualStyleBackColor = true;
            this.bt_IP.Click += new System.EventHandler(this.bt_IP_Click);
            // 
            // txt_IP
            // 
            this.txt_IP.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_IP.Location = new System.Drawing.Point(395, 51);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(151, 26);
            this.txt_IP.TabIndex = 20;
            this.txt_IP.Text = "请填写IP";
            // 
            // bt_global
            // 
            this.bt_global.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_global.Location = new System.Drawing.Point(552, 42);
            this.bt_global.Name = "bt_global";
            this.bt_global.Size = new System.Drawing.Size(123, 40);
            this.bt_global.TabIndex = 21;
            this.bt_global.Text = "新增全局配置";
            this.bt_global.UseVisualStyleBackColor = true;
            this.bt_global.Click += new System.EventHandler(this.bt_global_Click);
            // 
            // PCconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 702);
            this.Name = "PCconfig";
            this.Text = "PCconfig";
            this.Load += new System.EventHandler(this.PCconfig_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_IP;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Button bt_global;
    }
}