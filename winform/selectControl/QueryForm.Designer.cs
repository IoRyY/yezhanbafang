namespace yezhanbafang.fw.winform.selectControl
{
    partial class QueryForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.c2015QueryS1 = new C2015QueryS();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(729, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "查  询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // c2015QueryS1
            // 
            this.c2015QueryS1.BackColor = System.Drawing.Color.White;
            this.c2015QueryS1.Location = new System.Drawing.Point(12, 12);
            this.c2015QueryS1.Name = "c2015QueryS1";
            this.c2015QueryS1.Size = new System.Drawing.Size(700, 60);
            this.c2015QueryS1.TabIndex = 0;
            this.c2015QueryS1.XmlPath = null;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(818, 87);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.c2015QueryS1);
            this.Name = "QueryForm";
            this.Text = "复杂查询窗体";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private C2015QueryS c2015QueryS1;
        private System.Windows.Forms.Button button1;
    }
}