namespace yezhanbafang.fw.winform.selectControl
{
	partial class C2015Query
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.cb_zd = new System.Windows.Forms.ComboBox();
            this.cb_ys = new System.Windows.Forms.ComboBox();
            this.tb_nr = new System.Windows.Forms.TextBox();
            this.cb_gx = new System.Windows.Forms.ComboBox();
            this.dtp_nr = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // cb_zd
            // 
            this.cb_zd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_zd.FormattingEnabled = true;
            this.cb_zd.Location = new System.Drawing.Point(76, 4);
            this.cb_zd.Name = "cb_zd";
            this.cb_zd.Size = new System.Drawing.Size(175, 20);
            this.cb_zd.TabIndex = 1;
            this.cb_zd.SelectedIndexChanged += new System.EventHandler(this.cb_zd_SelectedIndexChanged);
            // 
            // cb_ys
            // 
            this.cb_ys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ys.FormattingEnabled = true;
            this.cb_ys.Items.AddRange(new object[] {
            "包含",
            "大于",
            "小于",
            "等于",
            "不等于"});
            this.cb_ys.Location = new System.Drawing.Point(257, 4);
            this.cb_ys.Name = "cb_ys";
            this.cb_ys.Size = new System.Drawing.Size(67, 20);
            this.cb_ys.TabIndex = 2;
            // 
            // tb_nr
            // 
            this.tb_nr.Location = new System.Drawing.Point(330, 4);
            this.tb_nr.Name = "tb_nr";
            this.tb_nr.Size = new System.Drawing.Size(200, 21);
            this.tb_nr.TabIndex = 3;
            this.tb_nr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_nr.TextChanged += new System.EventHandler(this.tb_nr_TextChanged);
            // 
            // cb_gx
            // 
            this.cb_gx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_gx.FormattingEnabled = true;
            this.cb_gx.Location = new System.Drawing.Point(3, 4);
            this.cb_gx.Name = "cb_gx";
            this.cb_gx.Size = new System.Drawing.Size(67, 20);
            this.cb_gx.TabIndex = 4;
            // 
            // dtp_nr
            // 
            this.dtp_nr.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtp_nr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_nr.Location = new System.Drawing.Point(536, 4);
            this.dtp_nr.Name = "dtp_nr";
            this.dtp_nr.Size = new System.Drawing.Size(200, 21);
            this.dtp_nr.TabIndex = 5;
            this.dtp_nr.Visible = false;
            // 
            // C2015Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtp_nr);
            this.Controls.Add(this.cb_gx);
            this.Controls.Add(this.tb_nr);
            this.Controls.Add(this.cb_ys);
            this.Controls.Add(this.cb_zd);
            this.Name = "C2015Query";
            this.Size = new System.Drawing.Size(700, 28);
            this.Load += new System.EventHandler(this.C2015Query_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ComboBox cb_zd;
        private System.Windows.Forms.ComboBox cb_ys;
        private System.Windows.Forms.TextBox tb_nr;
        private System.Windows.Forms.ComboBox cb_gx;
        private System.Windows.Forms.DateTimePicker dtp_nr;
	}
}
