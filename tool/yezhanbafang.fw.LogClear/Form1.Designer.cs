
namespace yezhanbafang.fw.LogClear
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bt_lj2 = new System.Windows.Forms.Button();
            this.lb_lj2 = new System.Windows.Forms.Label();
            this.bt_lj3 = new System.Windows.Forms.Button();
            this.lb_lj3 = new System.Windows.Forms.Label();
            this.bt_lj4 = new System.Windows.Forms.Button();
            this.lb_lj4 = new System.Windows.Forms.Label();
            this.bt_lj5 = new System.Windows.Forms.Button();
            this.lb_lj5 = new System.Windows.Forms.Label();
            this.bt_lj6 = new System.Windows.Forms.Button();
            this.lb_lj6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志路径";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "选择路径";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(150, 227);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(74, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "120";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 311);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "确  认";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "执行间隔(分钟)";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "未开启";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "日志保留(天)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(150, 267);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(74, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "14";
            // 
            // bt_lj2
            // 
            this.bt_lj2.Location = new System.Drawing.Point(27, 50);
            this.bt_lj2.Name = "bt_lj2";
            this.bt_lj2.Size = new System.Drawing.Size(106, 23);
            this.bt_lj2.TabIndex = 10;
            this.bt_lj2.Text = "选择路径2";
            this.bt_lj2.UseVisualStyleBackColor = true;
            this.bt_lj2.Click += new System.EventHandler(this.bt_lj2_Click);
            // 
            // lb_lj2
            // 
            this.lb_lj2.AutoSize = true;
            this.lb_lj2.Location = new System.Drawing.Point(139, 55);
            this.lb_lj2.Name = "lb_lj2";
            this.lb_lj2.Size = new System.Drawing.Size(53, 12);
            this.lb_lj2.TabIndex = 9;
            this.lb_lj2.Text = "日志路径";
            // 
            // bt_lj3
            // 
            this.bt_lj3.Location = new System.Drawing.Point(27, 79);
            this.bt_lj3.Name = "bt_lj3";
            this.bt_lj3.Size = new System.Drawing.Size(106, 23);
            this.bt_lj3.TabIndex = 12;
            this.bt_lj3.Text = "选择路径3";
            this.bt_lj3.UseVisualStyleBackColor = true;
            this.bt_lj3.Click += new System.EventHandler(this.bt_lj3_Click);
            // 
            // lb_lj3
            // 
            this.lb_lj3.AutoSize = true;
            this.lb_lj3.Location = new System.Drawing.Point(139, 84);
            this.lb_lj3.Name = "lb_lj3";
            this.lb_lj3.Size = new System.Drawing.Size(53, 12);
            this.lb_lj3.TabIndex = 11;
            this.lb_lj3.Text = "日志路径";
            // 
            // bt_lj4
            // 
            this.bt_lj4.Location = new System.Drawing.Point(27, 108);
            this.bt_lj4.Name = "bt_lj4";
            this.bt_lj4.Size = new System.Drawing.Size(106, 23);
            this.bt_lj4.TabIndex = 14;
            this.bt_lj4.Text = "选择路径4";
            this.bt_lj4.UseVisualStyleBackColor = true;
            this.bt_lj4.Click += new System.EventHandler(this.bt_lj4_Click);
            // 
            // lb_lj4
            // 
            this.lb_lj4.AutoSize = true;
            this.lb_lj4.Location = new System.Drawing.Point(139, 113);
            this.lb_lj4.Name = "lb_lj4";
            this.lb_lj4.Size = new System.Drawing.Size(53, 12);
            this.lb_lj4.TabIndex = 13;
            this.lb_lj4.Text = "日志路径";
            // 
            // bt_lj5
            // 
            this.bt_lj5.Location = new System.Drawing.Point(27, 137);
            this.bt_lj5.Name = "bt_lj5";
            this.bt_lj5.Size = new System.Drawing.Size(106, 23);
            this.bt_lj5.TabIndex = 16;
            this.bt_lj5.Text = "选择路径5";
            this.bt_lj5.UseVisualStyleBackColor = true;
            this.bt_lj5.Click += new System.EventHandler(this.bt_lj5_Click);
            // 
            // lb_lj5
            // 
            this.lb_lj5.AutoSize = true;
            this.lb_lj5.Location = new System.Drawing.Point(139, 142);
            this.lb_lj5.Name = "lb_lj5";
            this.lb_lj5.Size = new System.Drawing.Size(53, 12);
            this.lb_lj5.TabIndex = 15;
            this.lb_lj5.Text = "日志路径";
            // 
            // bt_lj6
            // 
            this.bt_lj6.Location = new System.Drawing.Point(27, 166);
            this.bt_lj6.Name = "bt_lj6";
            this.bt_lj6.Size = new System.Drawing.Size(106, 23);
            this.bt_lj6.TabIndex = 18;
            this.bt_lj6.Text = "选择路径6";
            this.bt_lj6.UseVisualStyleBackColor = true;
            this.bt_lj6.Click += new System.EventHandler(this.bt_lj6_Click);
            // 
            // lb_lj6
            // 
            this.lb_lj6.AutoSize = true;
            this.lb_lj6.Location = new System.Drawing.Point(139, 171);
            this.lb_lj6.Name = "lb_lj6";
            this.lb_lj6.Size = new System.Drawing.Size(53, 12);
            this.lb_lj6.TabIndex = 17;
            this.lb_lj6.Text = "日志路径";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 366);
            this.Controls.Add(this.bt_lj6);
            this.Controls.Add(this.lb_lj6);
            this.Controls.Add(this.bt_lj5);
            this.Controls.Add(this.lb_lj5);
            this.Controls.Add(this.bt_lj4);
            this.Controls.Add(this.lb_lj4);
            this.Controls.Add(this.bt_lj3);
            this.Controls.Add(this.lb_lj3);
            this.Controls.Add(this.bt_lj2);
            this.Controls.Add(this.lb_lj2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "日志自动清除";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bt_lj2;
        private System.Windows.Forms.Label lb_lj2;
        private System.Windows.Forms.Button bt_lj3;
        private System.Windows.Forms.Label lb_lj3;
        private System.Windows.Forms.Button bt_lj4;
        private System.Windows.Forms.Label lb_lj4;
        private System.Windows.Forms.Button bt_lj5;
        private System.Windows.Forms.Label lb_lj5;
        private System.Windows.Forms.Button bt_lj6;
        private System.Windows.Forms.Label lb_lj6;
    }
}

