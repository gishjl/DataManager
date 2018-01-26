namespace satics_school
{
    partial class frmRefreshSaticsData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRefreshSaticsData));
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_pwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_sid = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_conn_test = new System.Windows.Forms.Button();
            this.button_excute = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.公房ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.住房ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绿地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.树种更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.土地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.能耗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计来源设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名";
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(119, 31);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(100, 21);
            this.textBox_user.TabIndex = 2;
            this.textBox_user.Text = "wp_mis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "密  码";
            // 
            // textBox_pwd
            // 
            this.textBox_pwd.Location = new System.Drawing.Point(119, 58);
            this.textBox_pwd.Name = "textBox_pwd";
            this.textBox_pwd.PasswordChar = '*';
            this.textBox_pwd.Size = new System.Drawing.Size(100, 21);
            this.textBox_pwd.TabIndex = 2;
            this.textBox_pwd.Text = "wp_mis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "数据库SID";
            // 
            // textBox_sid
            // 
            this.textBox_sid.Location = new System.Drawing.Point(119, 85);
            this.textBox_sid.Name = "textBox_sid";
            this.textBox_sid.Size = new System.Drawing.Size(100, 21);
            this.textBox_sid.TabIndex = 2;
            this.textBox_sid.Text = "orcl";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button_conn_test);
            this.groupBox1.Controls.Add(this.textBox_user);
            this.groupBox1.Controls.Add(this.textBox_sid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_pwd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 131);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_conn_test
            // 
            this.button_conn_test.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_conn_test.Location = new System.Drawing.Point(236, 31);
            this.button_conn_test.Name = "button_conn_test";
            this.button_conn_test.Size = new System.Drawing.Size(176, 75);
            this.button_conn_test.TabIndex = 3;
            this.button_conn_test.Text = "打开数据库连接";
            this.button_conn_test.UseVisualStyleBackColor = true;
            this.button_conn_test.Click += new System.EventHandler(this.button_conn_test_Click);
            // 
            // button_excute
            // 
            this.button_excute.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_excute.Enabled = false;
            this.button_excute.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_excute.Location = new System.Drawing.Point(0, 194);
            this.button_excute.Name = "button_excute";
            this.button_excute.Size = new System.Drawing.Size(442, 35);
            this.button_excute.TabIndex = 3;
            this.button_excute.Text = "立即执行统计更新（已执行0次）";
            this.button_excute.UseVisualStyleBackColor = true;
            this.button_excute.Click += new System.EventHandler(this.button_excute_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(0, 175);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(442, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "自动执行统计函数（每天晚上12点左右执行统计函数）";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.公房ToolStripMenuItem,
            this.住房ToolStripMenuItem,
            this.绿地ToolStripMenuItem,
            this.土地ToolStripMenuItem,
            this.能耗ToolStripMenuItem,
            this.系统配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(442, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 公房ToolStripMenuItem
            // 
            this.公房ToolStripMenuItem.Name = "公房ToolStripMenuItem";
            this.公房ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.公房ToolStripMenuItem.Text = "公房";
            // 
            // 住房ToolStripMenuItem
            // 
            this.住房ToolStripMenuItem.Name = "住房ToolStripMenuItem";
            this.住房ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.住房ToolStripMenuItem.Text = "住房";
            this.住房ToolStripMenuItem.Click += new System.EventHandler(this.住房ToolStripMenuItem_Click);
            // 
            // 绿地ToolStripMenuItem
            // 
            this.绿地ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.树种更新ToolStripMenuItem});
            this.绿地ToolStripMenuItem.Name = "绿地ToolStripMenuItem";
            this.绿地ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.绿地ToolStripMenuItem.Text = "绿地";
            // 
            // 树种更新ToolStripMenuItem
            // 
            this.树种更新ToolStripMenuItem.Name = "树种更新ToolStripMenuItem";
            this.树种更新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.树种更新ToolStripMenuItem.Text = "树种更新";
            this.树种更新ToolStripMenuItem.Click += new System.EventHandler(this.树种更新ToolStripMenuItem_Click);
            // 
            // 土地ToolStripMenuItem
            // 
            this.土地ToolStripMenuItem.Name = "土地ToolStripMenuItem";
            this.土地ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.土地ToolStripMenuItem.Text = "土地";
            // 
            // 能耗ToolStripMenuItem
            // 
            this.能耗ToolStripMenuItem.Name = "能耗ToolStripMenuItem";
            this.能耗ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.能耗ToolStripMenuItem.Text = "能耗";
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.统计来源设置ToolStripMenuItem});
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.系统配置ToolStripMenuItem.Text = "系统配置";
            this.系统配置ToolStripMenuItem.Click += new System.EventHandler(this.系统配置ToolStripMenuItem_Click);
            // 
            // 统计来源设置ToolStripMenuItem
            // 
            this.统计来源设置ToolStripMenuItem.Name = "统计来源设置ToolStripMenuItem";
            this.统计来源设置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.统计来源设置ToolStripMenuItem.Text = "统计来源设置";
            this.统计来源设置ToolStripMenuItem.Click += new System.EventHandler(this.统计来源设置ToolStripMenuItem_Click);
            // 
            // frmRefreshSaticsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 229);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_excute);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRefreshSaticsData";
            this.Text = "统计数据刷新程序";
            this.Load += new System.EventHandler(this.frmRefreshSaticsData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_pwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_sid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_conn_test;
        private System.Windows.Forms.Button button_excute;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 公房ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 住房ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绿地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 树种更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 土地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 能耗ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计来源设置ToolStripMenuItem;
    }
}

