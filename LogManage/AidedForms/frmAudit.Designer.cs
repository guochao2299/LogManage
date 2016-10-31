namespace LogManage.AidedForms
{
    partial class frmAudit
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ucAudit1 = new LogManage.SelfDefineControl.ucAudit();
            this.SuspendLayout();
            // 
            // ucAudit1
            // 
            this.ucAudit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAudit1.Location = new System.Drawing.Point(0, 0);
            this.ucAudit1.Name = "ucAudit1";
            this.ucAudit1.Size = new System.Drawing.Size(988, 499);
            this.ucAudit1.TabIndex = 0;
            // 
            // frmAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 499);
            this.Controls.Add(this.ucAudit1);
            this.Name = "frmAudit";
            this.Text = "frmAudit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private SelfDefineControl.ucAudit ucAudit1;
    }
}