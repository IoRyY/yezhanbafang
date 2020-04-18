namespace yezhanbafang.fw.winform.Demo.test
{
    partial class testForm1
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
            // bt_change
            // 
            this.bt_change.Click += new System.EventHandler(this.bt_change_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // testForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 702);
            this.Name = "testForm1";
            this.Text = "testForm1";
            this.Load += new System.EventHandler(this.testForm1_Load);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}