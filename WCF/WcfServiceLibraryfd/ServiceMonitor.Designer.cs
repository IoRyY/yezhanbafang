using System.Windows.Forms;
namespace yezhanbafang.fw.WCF.Server
{
    partial class ServiceMonitor
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
            if (MessageBox.Show("关闭服务以前,请不要关闭此窗口,关闭此窗体后,请关闭Service程序", "真的确认??", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
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
            this.label1 = new System.Windows.Forms.Label();
            this.LB_Session = new System.Windows.Forms.Label();
            this.Bt_Clear = new System.Windows.Forms.Button();
            this.bt_auto = new System.Windows.Forms.Button();
            this.rtb_message = new System.Windows.Forms.RichTextBox();
            this.bt_advance = new System.Windows.Forms.Button();
            this.bt_message = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_MessageNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "现在服务的Session数量:";
            // 
            // LB_Session
            // 
            this.LB_Session.AutoSize = true;
            this.LB_Session.Location = new System.Drawing.Point(155, 18);
            this.LB_Session.Name = "LB_Session";
            this.LB_Session.Size = new System.Drawing.Size(11, 12);
            this.LB_Session.TabIndex = 1;
            this.LB_Session.Text = "0";
            // 
            // Bt_Clear
            // 
            this.Bt_Clear.Location = new System.Drawing.Point(425, 13);
            this.Bt_Clear.Name = "Bt_Clear";
            this.Bt_Clear.Size = new System.Drawing.Size(75, 23);
            this.Bt_Clear.TabIndex = 3;
            this.Bt_Clear.Text = "Clear";
            this.Bt_Clear.UseVisualStyleBackColor = true;
            this.Bt_Clear.Click += new System.EventHandler(this.Bt_Clear_Click);
            // 
            // bt_auto
            // 
            this.bt_auto.Location = new System.Drawing.Point(524, 12);
            this.bt_auto.Name = "bt_auto";
            this.bt_auto.Size = new System.Drawing.Size(107, 23);
            this.bt_auto.TabIndex = 4;
            this.bt_auto.Text = "Log(top1000)";
            this.bt_auto.UseVisualStyleBackColor = true;
            this.bt_auto.Click += new System.EventHandler(this.bt_auto_Click);
            // 
            // rtb_message
            // 
            this.rtb_message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_message.Location = new System.Drawing.Point(0, 42);
            this.rtb_message.Name = "rtb_message";
            this.rtb_message.Size = new System.Drawing.Size(1016, 692);
            this.rtb_message.TabIndex = 5;
            this.rtb_message.Text = "";
            // 
            // bt_advance
            // 
            this.bt_advance.Location = new System.Drawing.Point(661, 12);
            this.bt_advance.Name = "bt_advance";
            this.bt_advance.Size = new System.Drawing.Size(75, 23);
            this.bt_advance.TabIndex = 6;
            this.bt_advance.Text = "高级查询";
            this.bt_advance.UseVisualStyleBackColor = true;
            this.bt_advance.Click += new System.EventHandler(this.bt_advance_Click);
            // 
            // bt_message
            // 
            this.bt_message.Location = new System.Drawing.Point(767, 12);
            this.bt_message.Name = "bt_message";
            this.bt_message.Size = new System.Drawing.Size(104, 23);
            this.bt_message.TabIndex = 7;
            this.bt_message.Text = "强行推送消息";
            this.bt_message.UseVisualStyleBackColor = true;
            this.bt_message.Click += new System.EventHandler(this.bt_message_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "现在消息数量:";
            // 
            // lb_MessageNum
            // 
            this.lb_MessageNum.AutoSize = true;
            this.lb_MessageNum.Location = new System.Drawing.Point(284, 18);
            this.lb_MessageNum.Name = "lb_MessageNum";
            this.lb_MessageNum.Size = new System.Drawing.Size(11, 12);
            this.lb_MessageNum.TabIndex = 9;
            this.lb_MessageNum.Text = "0";
            // 
            // ServiceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.lb_MessageNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_message);
            this.Controls.Add(this.bt_advance);
            this.Controls.Add(this.rtb_message);
            this.Controls.Add(this.bt_auto);
            this.Controls.Add(this.Bt_Clear);
            this.Controls.Add(this.LB_Session);
            this.Controls.Add(this.label1);
            this.Name = "ServiceMonitor";
            this.Text = "ServiceMonitor";
            this.Load += new System.EventHandler(this.ServiceMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label LB_Session;
        private Button Bt_Clear;
        private Button bt_auto;
        public RichTextBox rtb_message;
        private Button bt_advance;
        private Button bt_message;
        private Label label2;
        private Label lb_MessageNum;
    }
}