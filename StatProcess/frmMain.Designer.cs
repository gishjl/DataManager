namespace StatsProcess
{
    partial class frmMain
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
            this.comboBox_Group = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Sheng = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_FillData = new System.Windows.Forms.Button();
            this.listView_data = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ComboBox_Indicator = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.初始化数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.初始化选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加指标数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excel数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认数值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建区域空表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.省级模拟数据生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.市级模拟数据生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.县级模拟数据生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量执行SQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成实际数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Date = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox_Shi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_DefaultValue = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_EAContent = new System.Windows.Forms.TextBox();
            this.checkBox_Filter = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_BuildData = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_Bu = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Group
            // 
            this.comboBox_Group.FormattingEnabled = true;
            this.comboBox_Group.Location = new System.Drawing.Point(81, 39);
            this.comboBox_Group.Name = "comboBox_Group";
            this.comboBox_Group.Size = new System.Drawing.Size(108, 20);
            this.comboBox_Group.TabIndex = 0;
            this.comboBox_Group.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "指标分组";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "指标分项";
            // 
            // comboBox_Sheng
            // 
            this.comboBox_Sheng.FormattingEnabled = true;
            this.comboBox_Sheng.Location = new System.Drawing.Point(161, 91);
            this.comboBox_Sheng.Name = "comboBox_Sheng";
            this.comboBox_Sheng.Size = new System.Drawing.Size(132, 20);
            this.comboBox_Sheng.TabIndex = 0;
            this.comboBox_Sheng.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "省";
            // 
            // button_FillData
            // 
            this.button_FillData.Location = new System.Drawing.Point(289, 454);
            this.button_FillData.Name = "button_FillData";
            this.button_FillData.Size = new System.Drawing.Size(75, 36);
            this.button_FillData.TabIndex = 1;
            this.button_FillData.Text = "入库";
            this.button_FillData.UseVisualStyleBackColor = true;
            this.button_FillData.Click += new System.EventHandler(this.button_FillData_Click);
            // 
            // listView_data
            // 
            this.listView_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView_data.FullRowSelect = true;
            this.listView_data.Location = new System.Drawing.Point(12, 178);
            this.listView_data.Name = "listView_data";
            this.listView_data.Size = new System.Drawing.Size(711, 270);
            this.listView_data.TabIndex = 4;
            this.listView_data.UseCompatibleStateImageBehavior = false;
            this.listView_data.View = System.Windows.Forms.View.Details;
            this.listView_data.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView_data.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView_data.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "地区";
            this.columnHeader1.Width = 94;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "地区编码";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "归属地区编码";
            this.columnHeader3.Width = 96;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "分项指标";
            this.columnHeader4.Width = 114;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "评价值";
            this.columnHeader5.Width = 74;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "时间";
            this.columnHeader6.Width = 48;
            // 
            // ComboBox_Indicator
            // 
            this.ComboBox_Indicator.FormattingEnabled = true;
            this.ComboBox_Indicator.Location = new System.Drawing.Point(81, 65);
            this.ComboBox_Indicator.Name = "ComboBox_Indicator";
            this.ComboBox_Indicator.Size = new System.Drawing.Size(108, 20);
            this.ComboBox_Indicator.TabIndex = 0;
            this.ComboBox_Indicator.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Indicator_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.初始化数据ToolStripMenuItem,
            this.添加指标数据ToolStripMenuItem,
            this.创建区域空表ToolStripMenuItem,
            this.批量执行SQLToolStripMenuItem,
            this.生成实际数据ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(735, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 初始化数据ToolStripMenuItem
            // 
            this.初始化数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.初始化选项ToolStripMenuItem});
            this.初始化数据ToolStripMenuItem.Name = "初始化数据ToolStripMenuItem";
            this.初始化数据ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.初始化数据ToolStripMenuItem.Text = "初始化数据";
            // 
            // 初始化选项ToolStripMenuItem
            // 
            this.初始化选项ToolStripMenuItem.Name = "初始化选项ToolStripMenuItem";
            this.初始化选项ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.初始化选项ToolStripMenuItem.Text = "初始化选项";
            this.初始化选项ToolStripMenuItem.Click += new System.EventHandler(this.初始化选项ToolStripMenuItem_Click);
            // 
            // 添加指标数据ToolStripMenuItem
            // 
            this.添加指标数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excel数据ToolStripMenuItem,
            this.默认数值ToolStripMenuItem});
            this.添加指标数据ToolStripMenuItem.Name = "添加指标数据ToolStripMenuItem";
            this.添加指标数据ToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.添加指标数据ToolStripMenuItem.Text = "添加指标数据";
            this.添加指标数据ToolStripMenuItem.Click += new System.EventHandler(this.添加指标数据ToolStripMenuItem_Click);
            // 
            // excel数据ToolStripMenuItem
            // 
            this.excel数据ToolStripMenuItem.Name = "excel数据ToolStripMenuItem";
            this.excel数据ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.excel数据ToolStripMenuItem.Text = "Excel数据";
            this.excel数据ToolStripMenuItem.Click += new System.EventHandler(this.excel数据ToolStripMenuItem_Click);
            // 
            // 默认数值ToolStripMenuItem
            // 
            this.默认数值ToolStripMenuItem.Name = "默认数值ToolStripMenuItem";
            this.默认数值ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.默认数值ToolStripMenuItem.Text = "默认数值";
            this.默认数值ToolStripMenuItem.Click += new System.EventHandler(this.默认数值ToolStripMenuItem_Click);
            // 
            // 创建区域空表ToolStripMenuItem
            // 
            this.创建区域空表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.省级模拟数据生成ToolStripMenuItem,
            this.市级模拟数据生成ToolStripMenuItem,
            this.县级模拟数据生成ToolStripMenuItem});
            this.创建区域空表ToolStripMenuItem.Name = "创建区域空表ToolStripMenuItem";
            this.创建区域空表ToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.创建区域空表ToolStripMenuItem.Text = "批量模拟数据生成";
            this.创建区域空表ToolStripMenuItem.Click += new System.EventHandler(this.创建区域空表ToolStripMenuItem_Click);
            // 
            // 省级模拟数据生成ToolStripMenuItem
            // 
            this.省级模拟数据生成ToolStripMenuItem.Name = "省级模拟数据生成ToolStripMenuItem";
            this.省级模拟数据生成ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.省级模拟数据生成ToolStripMenuItem.Text = "省级模拟数据生成";
            this.省级模拟数据生成ToolStripMenuItem.Click += new System.EventHandler(this.省级模拟数据生成ToolStripMenuItem_Click);
            // 
            // 市级模拟数据生成ToolStripMenuItem
            // 
            this.市级模拟数据生成ToolStripMenuItem.Name = "市级模拟数据生成ToolStripMenuItem";
            this.市级模拟数据生成ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.市级模拟数据生成ToolStripMenuItem.Text = "市级模拟数据生成";
            this.市级模拟数据生成ToolStripMenuItem.Click += new System.EventHandler(this.市级模拟数据生成ToolStripMenuItem_Click);
            // 
            // 县级模拟数据生成ToolStripMenuItem
            // 
            this.县级模拟数据生成ToolStripMenuItem.Name = "县级模拟数据生成ToolStripMenuItem";
            this.县级模拟数据生成ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.县级模拟数据生成ToolStripMenuItem.Text = "县级模拟数据生成";
            this.县级模拟数据生成ToolStripMenuItem.Click += new System.EventHandler(this.县级模拟数据生成ToolStripMenuItem_Click);
            // 
            // 批量执行SQLToolStripMenuItem
            // 
            this.批量执行SQLToolStripMenuItem.Name = "批量执行SQLToolStripMenuItem";
            this.批量执行SQLToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.批量执行SQLToolStripMenuItem.Text = "批量执行SQL";
            this.批量执行SQLToolStripMenuItem.Click += new System.EventHandler(this.批量执行SQLToolStripMenuItem_Click);
            // 
            // 生成实际数据ToolStripMenuItem
            // 
            this.生成实际数据ToolStripMenuItem.Name = "生成实际数据ToolStripMenuItem";
            this.生成实际数据ToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.生成实际数据ToolStripMenuItem.Text = "生成实际数据";
            this.生成实际数据ToolStripMenuItem.Click += new System.EventHandler(this.生成实际数据ToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "设置时间";
            // 
            // textBox_Date
            // 
            this.textBox_Date.Location = new System.Drawing.Point(256, 120);
            this.textBox_Date.Name = "textBox_Date";
            this.textBox_Date.Size = new System.Drawing.Size(108, 21);
            this.textBox_Date.TabIndex = 6;
            this.textBox_Date.Text = "2013";
            this.textBox_Date.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Date_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "添加分组";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "添加指标";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(519, 147);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(100, 21);
            this.txtInput.TabIndex = 10;
            this.txtInput.Visible = false;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(279, 147);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(234, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Visible = false;
            // 
            // comboBox_Shi
            // 
            this.comboBox_Shi.FormattingEnabled = true;
            this.comboBox_Shi.Location = new System.Drawing.Point(322, 94);
            this.comboBox_Shi.Name = "comboBox_Shi";
            this.comboBox_Shi.Size = new System.Drawing.Size(72, 20);
            this.comboBox_Shi.TabIndex = 0;
            this.comboBox_Shi.SelectedIndexChanged += new System.EventHandler(this.comboBox_SubRegion_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "市";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "默认数值";
            // 
            // textBox_DefaultValue
            // 
            this.textBox_DefaultValue.Location = new System.Drawing.Point(81, 120);
            this.textBox_DefaultValue.Name = "textBox_DefaultValue";
            this.textBox_DefaultValue.Size = new System.Drawing.Size(108, 21);
            this.textBox_DefaultValue.TabIndex = 6;
            this.textBox_DefaultValue.Text = "4322";
            this.textBox_DefaultValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Date_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_EAContent);
            this.groupBox1.Controls.Add(this.checkBox_Filter);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button_BuildData);
            this.groupBox1.Location = new System.Drawing.Point(401, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择性生成数据";
            // 
            // textBox_EAContent
            // 
            this.textBox_EAContent.Location = new System.Drawing.Point(63, 40);
            this.textBox_EAContent.Multiline = true;
            this.textBox_EAContent.Name = "textBox_EAContent";
            this.textBox_EAContent.Size = new System.Drawing.Size(180, 54);
            this.textBox_EAContent.TabIndex = 1;
            // 
            // checkBox_Filter
            // 
            this.checkBox_Filter.AutoSize = true;
            this.checkBox_Filter.Location = new System.Drawing.Point(110, 20);
            this.checkBox_Filter.Name = "checkBox_Filter";
            this.checkBox_Filter.Size = new System.Drawing.Size(108, 16);
            this.checkBox_Filter.TabIndex = 0;
            this.checkBox_Filter.Text = "筛选性批量数据";
            this.checkBox_Filter.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "评价指标";
            // 
            // button_BuildData
            // 
            this.button_BuildData.Location = new System.Drawing.Point(247, 49);
            this.button_BuildData.Name = "button_BuildData";
            this.button_BuildData.Size = new System.Drawing.Size(69, 38);
            this.button_BuildData.TabIndex = 7;
            this.button_BuildData.Text = "生成模拟数据";
            this.button_BuildData.UseVisualStyleBackColor = true;
            this.button_BuildData.Click += new System.EventHandler(this.button_BuildData_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "部";
            // 
            // comboBox_Bu
            // 
            this.comboBox_Bu.FormattingEnabled = true;
            this.comboBox_Bu.Items.AddRange(new object[] {
            "全国"});
            this.comboBox_Bu.Location = new System.Drawing.Point(81, 91);
            this.comboBox_Bu.Name = "comboBox_Bu";
            this.comboBox_Bu.Size = new System.Drawing.Size(51, 20);
            this.comboBox_Bu.TabIndex = 0;
            this.comboBox_Bu.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bu_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 519);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_DefaultValue);
            this.Controls.Add(this.textBox_Date);
            this.Controls.Add(this.listView_data);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_FillData);
            this.Controls.Add(this.ComboBox_Indicator);
            this.Controls.Add(this.comboBox_Bu);
            this.Controls.Add(this.comboBox_Shi);
            this.Controls.Add(this.comboBox_Sheng);
            this.Controls.Add(this.comboBox_Group);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Group;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Sheng;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_FillData;
        private System.Windows.Forms.ListView listView_data;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ComboBox ComboBox_Indicator;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 初始化数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 初始化选项ToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Date;
        private System.Windows.Forms.ToolStripMenuItem 添加指标数据ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem 创建区域空表ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox_Shi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem excel数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认数值ToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_DefaultValue;
        private System.Windows.Forms.ToolStripMenuItem 市级模拟数据生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 县级模拟数据生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 省级模拟数据生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量执行SQLToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_EAContent;
        private System.Windows.Forms.CheckBox checkBox_Filter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_BuildData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_Bu;
        private System.Windows.Forms.ToolStripMenuItem 生成实际数据ToolStripMenuItem;
    }
}

