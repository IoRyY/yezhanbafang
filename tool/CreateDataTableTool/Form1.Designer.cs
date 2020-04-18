namespace yezhanbafang.fw.ORMTool
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bt_sqltest = new System.Windows.Forms.Button();
            this.tb_pw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ku = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_columns = new System.Windows.Forms.DataGridView();
            this.dgv_tables = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_deltable = new System.Windows.Forms.Button();
            this.bt_addtable = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_tdsp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_tname = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_delcol = new System.Windows.Forms.Button();
            this.bt_editcol = new System.Windows.Forms.Button();
            this.cb_cidentity = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cb_cnull = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cb_ckey = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_ctype = new System.Windows.Forms.ComboBox();
            this.bt_addcol = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_cname = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_adddb = new System.Windows.Forms.Button();
            this.bt_create = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tb_prefix = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tb_webapi = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_wcfxml = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_icxml = new System.Windows.Forms.TextBox();
            this.cb_calltype = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_namespace = new System.Windows.Forms.TextBox();
            this.bt_createxml = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_textwidth = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_textstart = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_width = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_height = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_columns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tables)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(272, 185);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bt_sqltest);
            this.tabPage1.Controls.Add(this.tb_pw);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tb_name);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tb_ku);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tb_ip);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(264, 159);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SqlServer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bt_sqltest
            // 
            this.bt_sqltest.Location = new System.Drawing.Point(77, 121);
            this.bt_sqltest.Name = "bt_sqltest";
            this.bt_sqltest.Size = new System.Drawing.Size(158, 23);
            this.bt_sqltest.TabIndex = 8;
            this.bt_sqltest.Text = "测试并生成连接";
            this.bt_sqltest.UseVisualStyleBackColor = true;
            this.bt_sqltest.Click += new System.EventHandler(this.bt_sqltest_Click);
            // 
            // tb_pw
            // 
            this.tb_pw.Location = new System.Drawing.Point(77, 94);
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.PasswordChar = '*';
            this.tb_pw.Size = new System.Drawing.Size(158, 21);
            this.tb_pw.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "密码:";
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(77, 67);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(158, 21);
            this.tb_name.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名:";
            // 
            // tb_ku
            // 
            this.tb_ku.Location = new System.Drawing.Point(77, 40);
            this.tb_ku.Name = "tb_ku";
            this.tb_ku.Size = new System.Drawing.Size(158, 21);
            this.tb_ku.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "库名称:";
            // 
            // tb_ip
            // 
            this.tb_ip.Location = new System.Drawing.Point(77, 13);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(158, 21);
            this.tb_ip.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(264, 159);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Access";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(264, 159);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其他等等...";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "TablesList";
            // 
            // dgv_columns
            // 
            this.dgv_columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_columns.Location = new System.Drawing.Point(290, 34);
            this.dgv_columns.Name = "dgv_columns";
            this.dgv_columns.RowTemplate.Height = 23;
            this.dgv_columns.Size = new System.Drawing.Size(574, 435);
            this.dgv_columns.TabIndex = 3;
            this.dgv_columns.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_columns_CellClick);
            // 
            // dgv_tables
            // 
            this.dgv_tables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tables.Location = new System.Drawing.Point(12, 227);
            this.dgv_tables.Name = "dgv_tables";
            this.dgv_tables.RowTemplate.Height = 23;
            this.dgv_tables.Size = new System.Drawing.Size(272, 242);
            this.dgv_tables.TabIndex = 4;
            this.dgv_tables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tables_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_deltable);
            this.groupBox1.Controls.Add(this.bt_addtable);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_tdsp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_tname);
            this.groupBox1.Location = new System.Drawing.Point(870, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 104);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增表";
            // 
            // bt_deltable
            // 
            this.bt_deltable.Location = new System.Drawing.Point(83, 73);
            this.bt_deltable.Name = "bt_deltable";
            this.bt_deltable.Size = new System.Drawing.Size(75, 23);
            this.bt_deltable.TabIndex = 10;
            this.bt_deltable.Text = "删除选定表";
            this.bt_deltable.UseVisualStyleBackColor = true;
            this.bt_deltable.Click += new System.EventHandler(this.bt_deltable_Click);
            // 
            // bt_addtable
            // 
            this.bt_addtable.Location = new System.Drawing.Point(221, 73);
            this.bt_addtable.Name = "bt_addtable";
            this.bt_addtable.Size = new System.Drawing.Size(75, 23);
            this.bt_addtable.TabIndex = 9;
            this.bt_addtable.Text = "新增表";
            this.bt_addtable.UseVisualStyleBackColor = true;
            this.bt_addtable.Click += new System.EventHandler(this.bt_addtable_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "表描述:";
            // 
            // tb_tdsp
            // 
            this.tb_tdsp.Location = new System.Drawing.Point(83, 46);
            this.tb_tdsp.Name = "tb_tdsp";
            this.tb_tdsp.Size = new System.Drawing.Size(213, 21);
            this.tb_tdsp.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "表名称:";
            // 
            // tb_tname
            // 
            this.tb_tname.Location = new System.Drawing.Point(83, 19);
            this.tb_tname.Name = "tb_tname";
            this.tb_tname.Size = new System.Drawing.Size(213, 21);
            this.tb_tname.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(290, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "ColumnsList";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_delcol);
            this.groupBox2.Controls.Add(this.bt_editcol);
            this.groupBox2.Controls.Add(this.cb_cidentity);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cb_cnull);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cb_ckey);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cb_ctype);
            this.groupBox2.Controls.Add(this.bt_addcol);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_cname);
            this.groupBox2.Location = new System.Drawing.Point(870, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 186);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编辑表";
            // 
            // bt_delcol
            // 
            this.bt_delcol.Location = new System.Drawing.Point(107, 150);
            this.bt_delcol.Name = "bt_delcol";
            this.bt_delcol.Size = new System.Drawing.Size(93, 23);
            this.bt_delcol.TabIndex = 18;
            this.bt_delcol.Text = "删除选中列";
            this.bt_delcol.UseVisualStyleBackColor = true;
            this.bt_delcol.Click += new System.EventHandler(this.bt_delcol_Click);
            // 
            // bt_editcol
            // 
            this.bt_editcol.Location = new System.Drawing.Point(8, 150);
            this.bt_editcol.Name = "bt_editcol";
            this.bt_editcol.Size = new System.Drawing.Size(93, 23);
            this.bt_editcol.TabIndex = 17;
            this.bt_editcol.Text = "编辑选中列";
            this.bt_editcol.UseVisualStyleBackColor = true;
            this.bt_editcol.Click += new System.EventHandler(this.bt_editcol_Click);
            // 
            // cb_cidentity
            // 
            this.cb_cidentity.FormattingEnabled = true;
            this.cb_cidentity.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cb_cidentity.Location = new System.Drawing.Point(83, 124);
            this.cb_cidentity.Name = "cb_cidentity";
            this.cb_cidentity.Size = new System.Drawing.Size(213, 20);
            this.cb_cidentity.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "是否自增:";
            // 
            // cb_cnull
            // 
            this.cb_cnull.FormattingEnabled = true;
            this.cb_cnull.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cb_cnull.Location = new System.Drawing.Point(83, 98);
            this.cb_cnull.Name = "cb_cnull";
            this.cb_cnull.Size = new System.Drawing.Size(213, 20);
            this.cb_cnull.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "是否为空:";
            // 
            // cb_ckey
            // 
            this.cb_ckey.FormattingEnabled = true;
            this.cb_ckey.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cb_ckey.Location = new System.Drawing.Point(83, 72);
            this.cb_ckey.Name = "cb_ckey";
            this.cb_ckey.Size = new System.Drawing.Size(213, 20);
            this.cb_ckey.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "是否主键:";
            // 
            // cb_ctype
            // 
            this.cb_ctype.FormattingEnabled = true;
            this.cb_ctype.Items.AddRange(new object[] {
            "nvarchar(50)",
            "datetime",
            "int"});
            this.cb_ctype.Location = new System.Drawing.Point(83, 46);
            this.cb_ctype.Name = "cb_ctype";
            this.cb_ctype.Size = new System.Drawing.Size(213, 20);
            this.cb_ctype.TabIndex = 10;
            // 
            // bt_addcol
            // 
            this.bt_addcol.Location = new System.Drawing.Point(221, 150);
            this.bt_addcol.Name = "bt_addcol";
            this.bt_addcol.Size = new System.Drawing.Size(75, 23);
            this.bt_addcol.TabIndex = 9;
            this.bt_addcol.Text = "新增列";
            this.bt_addcol.UseVisualStyleBackColor = true;
            this.bt_addcol.Click += new System.EventHandler(this.bt_addcol_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "列类型:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "列名称:";
            // 
            // tb_cname
            // 
            this.tb_cname.Location = new System.Drawing.Point(83, 19);
            this.tb_cname.Name = "tb_cname";
            this.tb_cname.Size = new System.Drawing.Size(213, 21);
            this.tb_cname.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_adddb);
            this.groupBox3.Location = new System.Drawing.Point(870, 314);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 155);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库操作";
            // 
            // bt_adddb
            // 
            this.bt_adddb.Location = new System.Drawing.Point(8, 31);
            this.bt_adddb.Name = "bt_adddb";
            this.bt_adddb.Size = new System.Drawing.Size(288, 23);
            this.bt_adddb.TabIndex = 10;
            this.bt_adddb.Text = "将选中表新增至数据库";
            this.bt_adddb.UseVisualStyleBackColor = true;
            this.bt_adddb.Click += new System.EventHandler(this.bt_adddb_Click);
            // 
            // bt_create
            // 
            this.bt_create.Location = new System.Drawing.Point(866, 18);
            this.bt_create.Name = "bt_create";
            this.bt_create.Size = new System.Drawing.Size(288, 23);
            this.bt_create.TabIndex = 11;
            this.bt_create.Text = "数据库表-->类";
            this.bt_create.UseVisualStyleBackColor = true;
            this.bt_create.Click += new System.EventHandler(this.bt_create_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.tb_prefix);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.tb_webapi);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.tb_wcfxml);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.tb_icxml);
            this.groupBox4.Controls.Add(this.bt_create);
            this.groupBox4.Controls.Add(this.cb_calltype);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.tb_namespace);
            this.groupBox4.Location = new System.Drawing.Point(12, 475);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1160, 108);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "生成类操作";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(20, 77);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 12);
            this.label19.TabIndex = 25;
            this.label19.Text = "类型属性前缀:";
            // 
            // tb_prefix
            // 
            this.tb_prefix.Location = new System.Drawing.Point(121, 74);
            this.tb_prefix.Name = "tb_prefix";
            this.tb_prefix.Size = new System.Drawing.Size(213, 21);
            this.tb_prefix.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(370, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 12);
            this.label18.TabIndex = 23;
            this.label18.Text = "WebAPI URL:";
            // 
            // tb_webapi
            // 
            this.tb_webapi.Location = new System.Drawing.Point(471, 74);
            this.tb_webapi.Name = "tb_webapi";
            this.tb_webapi.Size = new System.Drawing.Size(213, 21);
            this.tb_webapi.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(370, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 12);
            this.label17.TabIndex = 21;
            this.label17.Text = "WCF IP port:";
            // 
            // tb_wcfxml
            // 
            this.tb_wcfxml.Location = new System.Drawing.Point(471, 47);
            this.tb_wcfxml.Name = "tb_wcfxml";
            this.tb_wcfxml.Size = new System.Drawing.Size(213, 21);
            this.tb_wcfxml.TabIndex = 20;
            this.tb_wcfxml.Text = "net.tcp://127.0.0.1:8090/yuan";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(370, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "IoRyClassXML:";
            // 
            // tb_icxml
            // 
            this.tb_icxml.Location = new System.Drawing.Point(471, 20);
            this.tb_icxml.Name = "tb_icxml";
            this.tb_icxml.Size = new System.Drawing.Size(213, 21);
            this.tb_icxml.TabIndex = 18;
            this.tb_icxml.Text = "constring.xml";
            // 
            // cb_calltype
            // 
            this.cb_calltype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_calltype.FormattingEnabled = true;
            this.cb_calltype.Items.AddRange(new object[] {
            "IoRyClass",
            "WCF",
            "WebAPI"});
            this.cb_calltype.Location = new System.Drawing.Point(121, 47);
            this.cb_calltype.Name = "cb_calltype";
            this.cb_calltype.Size = new System.Drawing.Size(213, 20);
            this.cb_calltype.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 9;
            this.label15.Text = "调用数据库方式:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "namespace:";
            // 
            // tb_namespace
            // 
            this.tb_namespace.Location = new System.Drawing.Point(121, 20);
            this.tb_namespace.Name = "tb_namespace";
            this.tb_namespace.Size = new System.Drawing.Size(213, 21);
            this.tb_namespace.TabIndex = 6;
            this.tb_namespace.Text = "CreateDataTableTool";
            // 
            // bt_createxml
            // 
            this.bt_createxml.Location = new System.Drawing.Point(866, 20);
            this.bt_createxml.Name = "bt_createxml";
            this.bt_createxml.Size = new System.Drawing.Size(288, 23);
            this.bt_createxml.TabIndex = 26;
            this.bt_createxml.Text = "类--->XML";
            this.bt_createxml.UseVisualStyleBackColor = true;
            this.bt_createxml.Click += new System.EventHandler(this.bt_createxml_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txt_textwidth);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.txt_textstart);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.txt_width);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.txt_height);
            this.groupBox5.Controls.Add(this.bt_createxml);
            this.groupBox5.Location = new System.Drawing.Point(12, 589);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1160, 160);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "XML生成";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(370, 52);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 12);
            this.label23.TabIndex = 34;
            this.label23.Text = "txt默认长度";
            // 
            // txt_textwidth
            // 
            this.txt_textwidth.Location = new System.Drawing.Point(471, 49);
            this.txt_textwidth.Name = "txt_textwidth";
            this.txt_textwidth.Size = new System.Drawing.Size(213, 21);
            this.txt_textwidth.TabIndex = 33;
            this.txt_textwidth.Text = "200";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(20, 52);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 12);
            this.label22.TabIndex = 32;
            this.label22.Text = "txt默认起始";
            // 
            // txt_textstart
            // 
            this.txt_textstart.Location = new System.Drawing.Point(121, 49);
            this.txt_textstart.Name = "txt_textstart";
            this.txt_textstart.Size = new System.Drawing.Size(213, 21);
            this.txt_textstart.TabIndex = 31;
            this.txt_textstart.Text = "110";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(370, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 12);
            this.label21.TabIndex = 30;
            this.label21.Text = "控件默认长度";
            // 
            // txt_width
            // 
            this.txt_width.Location = new System.Drawing.Point(471, 22);
            this.txt_width.Name = "txt_width";
            this.txt_width.Size = new System.Drawing.Size(213, 21);
            this.txt_width.TabIndex = 29;
            this.txt_width.Text = "320";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(20, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 28;
            this.label20.Text = "控件默认高度";
            // 
            // txt_height
            // 
            this.txt_height.Location = new System.Drawing.Point(121, 22);
            this.txt_height.Name = "txt_height";
            this.txt_height.Size = new System.Drawing.Size(213, 21);
            this.txt_height.TabIndex = 27;
            this.txt_height.Text = "28";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_tables);
            this.Controls.Add(this.dgv_columns);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "数据库表结构生成工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_columns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tables)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tb_pw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ku;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgv_columns;
        private System.Windows.Forms.Button bt_sqltest;
        private System.Windows.Forms.DataGridView dgv_tables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_tdsp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_tname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bt_addtable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cb_cidentity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cb_cnull;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cb_ckey;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_ctype;
        private System.Windows.Forms.Button bt_addcol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_cname;
        private System.Windows.Forms.Button bt_delcol;
        private System.Windows.Forms.Button bt_editcol;
        private System.Windows.Forms.Button bt_deltable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bt_adddb;
        private System.Windows.Forms.Button bt_create;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_namespace;
        private System.Windows.Forms.ComboBox cb_calltype;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_icxml;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_wcfxml;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tb_webapi;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tb_prefix;
        private System.Windows.Forms.Button bt_createxml;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_width;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_height;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_textwidth;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_textstart;
    }
}

