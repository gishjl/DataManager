namespace StatsProcess
{
    partial class frmTree
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
            this.ctrlTree_Stats = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_Tree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tree_AddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_DeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_ModifyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tree_ViewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip_Tree.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlTree_Stats
            // 
            this.ctrlTree_Stats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTree_Stats.Location = new System.Drawing.Point(0, 0);
            this.ctrlTree_Stats.Name = "ctrlTree_Stats";
            this.ctrlTree_Stats.Size = new System.Drawing.Size(384, 573);
            this.ctrlTree_Stats.TabIndex = 0;
            this.ctrlTree_Stats.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ctrlTree_Stats_NodeMouseClick);
            this.ctrlTree_Stats.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ctrlTree_Stats_NodeMouseDoubleClick);
            // 
            // contextMenuStrip_Tree
            // 
            this.contextMenuStrip_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tree_ViewItem,
            this.toolStripSeparator1,
            this.Tree_AddItem,
            this.Tree_DeleteItem,
            this.Tree_ModifyItem,
            this.toolStripSeparator2});
            this.contextMenuStrip_Tree.Name = "contextMenuStrip_Tree";
            this.contextMenuStrip_Tree.Size = new System.Drawing.Size(99, 104);
            // 
            // Tree_AddItem
            // 
            this.Tree_AddItem.Name = "Tree_AddItem";
            this.Tree_AddItem.Size = new System.Drawing.Size(152, 22);
            this.Tree_AddItem.Text = "添加";
            this.Tree_AddItem.Click += new System.EventHandler(this.Tree_AddItem_Click);
            // 
            // Tree_DeleteItem
            // 
            this.Tree_DeleteItem.Name = "Tree_DeleteItem";
            this.Tree_DeleteItem.Size = new System.Drawing.Size(152, 22);
            this.Tree_DeleteItem.Text = "删除";
            this.Tree_DeleteItem.Click += new System.EventHandler(this.Tree_DeleteItem_Click);
            // 
            // Tree_ModifyItem
            // 
            this.Tree_ModifyItem.Name = "Tree_ModifyItem";
            this.Tree_ModifyItem.Size = new System.Drawing.Size(152, 22);
            this.Tree_ModifyItem.Text = "编辑";
            this.Tree_ModifyItem.Click += new System.EventHandler(this.Tree_ModifyItem_Click);
            // 
            // Tree_ViewItem
            // 
            this.Tree_ViewItem.Name = "Tree_ViewItem";
            this.Tree_ViewItem.Size = new System.Drawing.Size(152, 22);
            this.Tree_ViewItem.Text = "查看";
            this.Tree_ViewItem.Click += new System.EventHandler(this.Tree_ViewItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(95, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(95, 6);
            // 
            // frmTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 573);
            this.Controls.Add(this.ctrlTree_Stats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTree";
            this.Text = "frm";
            this.contextMenuStrip_Tree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ctrlTree_Stats;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Tree;
        private System.Windows.Forms.ToolStripMenuItem Tree_ViewItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Tree_AddItem;
        private System.Windows.Forms.ToolStripMenuItem Tree_DeleteItem;
        private System.Windows.Forms.ToolStripMenuItem Tree_ModifyItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}