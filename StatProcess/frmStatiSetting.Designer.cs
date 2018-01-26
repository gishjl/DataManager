namespace StatsProcess
{
    partial class frmStatiSetting
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
            this.btn_TreeNode = new System.Windows.Forms.Button();
            this.checkedListBox_Tree = new System.Windows.Forms.CheckedListBox();
            this.button_ExcelData = new System.Windows.Forms.Button();
            this.checkedListBox_Column = new System.Windows.Forms.CheckedListBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_TreeUP = new System.Windows.Forms.Button();
            this.button_TreeDown = new System.Windows.Forms.Button();
            this.button_ExcelUp = new System.Windows.Forms.Button();
            this.button_ExcelDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_TreeNode
            // 
            this.btn_TreeNode.Location = new System.Drawing.Point(12, 12);
            this.btn_TreeNode.Name = "btn_TreeNode";
            this.btn_TreeNode.Size = new System.Drawing.Size(75, 23);
            this.btn_TreeNode.TabIndex = 0;
            this.btn_TreeNode.Text = "树节点";
            this.btn_TreeNode.UseVisualStyleBackColor = true;
            this.btn_TreeNode.Click += new System.EventHandler(this.btn_TreeNode_Click);
            // 
            // checkedListBox_Tree
            // 
            this.checkedListBox_Tree.FormattingEnabled = true;
            this.checkedListBox_Tree.Location = new System.Drawing.Point(12, 73);
            this.checkedListBox_Tree.Name = "checkedListBox_Tree";
            this.checkedListBox_Tree.Size = new System.Drawing.Size(225, 372);
            this.checkedListBox_Tree.TabIndex = 1;
            this.checkedListBox_Tree.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_Tree_SelectedIndexChanged);
            // 
            // button_ExcelData
            // 
            this.button_ExcelData.Location = new System.Drawing.Point(328, 12);
            this.button_ExcelData.Name = "button_ExcelData";
            this.button_ExcelData.Size = new System.Drawing.Size(79, 23);
            this.button_ExcelData.TabIndex = 2;
            this.button_ExcelData.Text = "Excel数据";
            this.button_ExcelData.UseVisualStyleBackColor = true;
            this.button_ExcelData.Click += new System.EventHandler(this.button_ExcelData_Click);
            // 
            // checkedListBox_Column
            // 
            this.checkedListBox_Column.FormattingEnabled = true;
            this.checkedListBox_Column.Location = new System.Drawing.Point(328, 73);
            this.checkedListBox_Column.Name = "checkedListBox_Column";
            this.checkedListBox_Column.Size = new System.Drawing.Size(225, 372);
            this.checkedListBox_Column.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(489, 14);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 21);
            this.numericUpDown1.TabIndex = 3;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(601, 14);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(54, 21);
            this.numericUpDown2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(442, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "开始行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束行";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "统计指标";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "列名称";
            // 
            // button_TreeUP
            // 
            this.button_TreeUP.Location = new System.Drawing.Point(243, 73);
            this.button_TreeUP.Name = "button_TreeUP";
            this.button_TreeUP.Size = new System.Drawing.Size(25, 88);
            this.button_TreeUP.TabIndex = 5;
            this.button_TreeUP.Text = "上移";
            this.button_TreeUP.UseVisualStyleBackColor = true;
            this.button_TreeUP.Click += new System.EventHandler(this.button_TreeUP_Click);
            // 
            // button_TreeDown
            // 
            this.button_TreeDown.Location = new System.Drawing.Point(243, 167);
            this.button_TreeDown.Name = "button_TreeDown";
            this.button_TreeDown.Size = new System.Drawing.Size(25, 88);
            this.button_TreeDown.TabIndex = 5;
            this.button_TreeDown.Text = "下移";
            this.button_TreeDown.UseVisualStyleBackColor = true;
            this.button_TreeDown.Click += new System.EventHandler(this.button_TreeDown_Click);
            // 
            // button_ExcelUp
            // 
            this.button_ExcelUp.Location = new System.Drawing.Point(556, 73);
            this.button_ExcelUp.Name = "button_ExcelUp";
            this.button_ExcelUp.Size = new System.Drawing.Size(25, 88);
            this.button_ExcelUp.TabIndex = 5;
            this.button_ExcelUp.Text = "上移";
            this.button_ExcelUp.UseVisualStyleBackColor = true;
            this.button_ExcelUp.Click += new System.EventHandler(this.button_ExcelUp_Click);
            // 
            // button_ExcelDown
            // 
            this.button_ExcelDown.Location = new System.Drawing.Point(556, 167);
            this.button_ExcelDown.Name = "button_ExcelDown";
            this.button_ExcelDown.Size = new System.Drawing.Size(25, 88);
            this.button_ExcelDown.TabIndex = 5;
            this.button_ExcelDown.Text = "下移";
            this.button_ExcelDown.UseVisualStyleBackColor = true;
            this.button_ExcelDown.Click += new System.EventHandler(this.button_ExcelDown_Click);
            // 
            // frmStatiSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 525);
            this.Controls.Add(this.button_ExcelDown);
            this.Controls.Add(this.button_TreeDown);
            this.Controls.Add(this.button_ExcelUp);
            this.Controls.Add(this.button_TreeUP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button_ExcelData);
            this.Controls.Add(this.checkedListBox_Column);
            this.Controls.Add(this.checkedListBox_Tree);
            this.Controls.Add(this.btn_TreeNode);
            this.Name = "frmStatiSetting";
            this.Text = "数据处理";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_TreeNode;
        private System.Windows.Forms.CheckedListBox checkedListBox_Tree;
        private System.Windows.Forms.Button button_ExcelData;
        private System.Windows.Forms.CheckedListBox checkedListBox_Column;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_TreeUP;
        private System.Windows.Forms.Button button_TreeDown;
        private System.Windows.Forms.Button button_ExcelUp;
        private System.Windows.Forms.Button button_ExcelDown;

    }
}