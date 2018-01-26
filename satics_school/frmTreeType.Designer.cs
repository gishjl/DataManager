namespace satics_school
{
    partial class frmTreeType
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("乔木");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("灌木");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("其他（花卉、藤本等）");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("未定义分类");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.乔木ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.灌木ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他花卉藤本等ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点1";
            treeNode1.Text = "乔木";
            treeNode2.Name = "节点2";
            treeNode2.Text = "灌木";
            treeNode3.Name = "节点0";
            treeNode3.Text = "其他（花卉、藤本等）";
            treeNode4.Name = "节点3";
            treeNode4.Text = "未定义分类";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(541, 506);
            this.treeView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.乔木ToolStripMenuItem,
            this.灌木ToolStripMenuItem,
            this.其他花卉藤本等ToolStripMenuItem,
            this.toolStripSeparator1,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 120);
            // 
            // 乔木ToolStripMenuItem
            // 
            this.乔木ToolStripMenuItem.Name = "乔木ToolStripMenuItem";
            this.乔木ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.乔木ToolStripMenuItem.Text = "乔木";
            this.乔木ToolStripMenuItem.Click += new System.EventHandler(this.乔木ToolStripMenuItem_Click);
            // 
            // 灌木ToolStripMenuItem
            // 
            this.灌木ToolStripMenuItem.Name = "灌木ToolStripMenuItem";
            this.灌木ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.灌木ToolStripMenuItem.Text = "灌木";
            this.灌木ToolStripMenuItem.Click += new System.EventHandler(this.灌木ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 其他花卉藤本等ToolStripMenuItem
            // 
            this.其他花卉藤本等ToolStripMenuItem.Name = "其他花卉藤本等ToolStripMenuItem";
            this.其他花卉藤本等ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.其他花卉藤本等ToolStripMenuItem.Text = "其他（花卉、藤本等）";
            this.其他花卉藤本等ToolStripMenuItem.Click += new System.EventHandler(this.其他花卉藤本等ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // frmTreeType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 506);
            this.Controls.Add(this.treeView1);
            this.Name = "frmTreeType";
            this.Text = "树种更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTreeType_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 乔木ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 灌木ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他花卉藤本等ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}