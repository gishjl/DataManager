namespace StatsProcess
{
    partial class frmEvaluatorSelector
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
            this.checkedListBox_EA = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // checkedListBox_EA
            // 
            this.checkedListBox_EA.FormattingEnabled = true;
            this.checkedListBox_EA.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox_EA.Name = "checkedListBox_EA";
            this.checkedListBox_EA.Size = new System.Drawing.Size(260, 420);
            this.checkedListBox_EA.TabIndex = 0;
            // 
            // frmEvaluatorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 444);
            this.Controls.Add(this.checkedListBox_EA);
            this.Name = "frmEvaluatorSelector";
            this.Text = "frmEvaluatorSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_EA;

    }
}