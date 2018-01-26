namespace StatsProcess
{
    partial class frmSettingInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_ZBLX = new System.Windows.Forms.ComboBox();
            this.comboBox_STable = new System.Windows.Forms.ComboBox();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.textBox_BM = new System.Windows.Forms.TextBox();
            this.textBox_MC = new System.Windows.Forms.TextBox();
            this.textBox_PID = new System.Windows.Forms.TextBox();
            this.textBox_SUnit = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "唯一标识";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "指标编码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "指标名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "所属唯一标识";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "指标类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "统计映射表";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "统计单位";
            // 
            // comboBox_ZBLX
            // 
            this.comboBox_ZBLX.FormattingEnabled = true;
            this.comboBox_ZBLX.Location = new System.Drawing.Point(134, 139);
            this.comboBox_ZBLX.Name = "comboBox_ZBLX";
            this.comboBox_ZBLX.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ZBLX.TabIndex = 1;
            // 
            // comboBox_STable
            // 
            this.comboBox_STable.FormattingEnabled = true;
            this.comboBox_STable.Location = new System.Drawing.Point(134, 165);
            this.comboBox_STable.Name = "comboBox_STable";
            this.comboBox_STable.Size = new System.Drawing.Size(121, 20);
            this.comboBox_STable.TabIndex = 1;
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(134, 28);
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(157, 21);
            this.textBox_ID.TabIndex = 2;
            this.textBox_ID.TextChanged += new System.EventHandler(this.textBox_ID_TextChanged);
            // 
            // textBox_BM
            // 
            this.textBox_BM.Location = new System.Drawing.Point(134, 58);
            this.textBox_BM.Name = "textBox_BM";
            this.textBox_BM.Size = new System.Drawing.Size(157, 21);
            this.textBox_BM.TabIndex = 2;
            // 
            // textBox_MC
            // 
            this.textBox_MC.Location = new System.Drawing.Point(134, 85);
            this.textBox_MC.Name = "textBox_MC";
            this.textBox_MC.Size = new System.Drawing.Size(157, 21);
            this.textBox_MC.TabIndex = 2;
            // 
            // textBox_PID
            // 
            this.textBox_PID.Location = new System.Drawing.Point(134, 112);
            this.textBox_PID.Name = "textBox_PID";
            this.textBox_PID.Size = new System.Drawing.Size(157, 21);
            this.textBox_PID.TabIndex = 2;
            // 
            // textBox_SUnit
            // 
            this.textBox_SUnit.Location = new System.Drawing.Point(134, 191);
            this.textBox_SUnit.Name = "textBox_SUnit";
            this.textBox_SUnit.Size = new System.Drawing.Size(67, 21);
            this.textBox_SUnit.TabIndex = 2;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(88, 226);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(181, 226);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // frmSettingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 261);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_SUnit);
            this.Controls.Add(this.textBox_PID);
            this.Controls.Add(this.textBox_MC);
            this.Controls.Add(this.textBox_BM);
            this.Controls.Add(this.textBox_ID);
            this.Controls.Add(this.comboBox_STable);
            this.Controls.Add(this.comboBox_ZBLX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSettingInfo";
            this.Text = "frmSettingInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_ZBLX;
        private System.Windows.Forms.ComboBox comboBox_STable;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.TextBox textBox_BM;
        private System.Windows.Forms.TextBox textBox_MC;
        private System.Windows.Forms.TextBox textBox_PID;
        private System.Windows.Forms.TextBox textBox_SUnit;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
    }
}