namespace LogManage
{
    partial class frmMain
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDefineLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDefineAppStruct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDefineAppGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAppLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSecurityEvent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditActionResults = new System.Windows.Forms.ToolStripMenuItem();
            this.维护安全事件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSingleWin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOnlyWin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.显示动画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem,
            this.menuAppLogs,
            this.menuSecurityEvent,
            this.menuWin,
            this.menuTest});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDefineLogItem,
            this.menuDefineAppStruct,
            this.menuDefineAppGroup});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // menuDefineLogItem
            // 
            this.menuDefineLogItem.Name = "menuDefineLogItem";
            this.menuDefineLogItem.Size = new System.Drawing.Size(166, 22);
            this.menuDefineLogItem.Text = "定义日志列";
            this.menuDefineLogItem.Click += new System.EventHandler(this.menuDefineLogItem_Click);
            // 
            // menuDefineAppStruct
            // 
            this.menuDefineAppStruct.Name = "menuDefineAppStruct";
            this.menuDefineAppStruct.Size = new System.Drawing.Size(166, 22);
            this.menuDefineAppStruct.Text = "定义应用系统结构";
            this.menuDefineAppStruct.Click += new System.EventHandler(this.menuDefineAppStruct_Click);
            // 
            // menuDefineAppGroup
            // 
            this.menuDefineAppGroup.Name = "menuDefineAppGroup";
            this.menuDefineAppGroup.Size = new System.Drawing.Size(166, 22);
            this.menuDefineAppGroup.Text = "定义应用系统分组";
            this.menuDefineAppGroup.Click += new System.EventHandler(this.menuDefineAppGroup_Click);
            // 
            // menuAppLogs
            // 
            this.menuAppLogs.Name = "menuAppLogs";
            this.menuAppLogs.Size = new System.Drawing.Size(89, 20);
            this.menuAppLogs.Text = "应用系统日志";
            this.menuAppLogs.DropDownOpening += new System.EventHandler(this.menuAppLogs_DropDownOpening);
            this.menuAppLogs.Click += new System.EventHandler(this.menuAppLogs_Click);
            // 
            // menuSecurityEvent
            // 
            this.menuSecurityEvent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditActionResults,
            this.维护安全事件ToolStripMenuItem});
            this.menuSecurityEvent.Name = "menuSecurityEvent";
            this.menuSecurityEvent.Size = new System.Drawing.Size(89, 20);
            this.menuSecurityEvent.Text = "安全事件管理";
            // 
            // menuEditActionResults
            // 
            this.menuEditActionResults.Name = "menuEditActionResults";
            this.menuEditActionResults.Size = new System.Drawing.Size(166, 22);
            this.menuEditActionResults.Text = "维护安全事件结果";
            this.menuEditActionResults.Click += new System.EventHandler(this.menuEditActionResults_Click);
            // 
            // 维护安全事件ToolStripMenuItem
            // 
            this.维护安全事件ToolStripMenuItem.Name = "维护安全事件ToolStripMenuItem";
            this.维护安全事件ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.维护安全事件ToolStripMenuItem.Text = "维护安全事件";
            this.维护安全事件ToolStripMenuItem.Click += new System.EventHandler(this.维护安全事件ToolStripMenuItem_Click);
            // 
            // menuWin
            // 
            this.menuWin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSingleWin,
            this.menuOnlyWin});
            this.menuWin.Name = "menuWin";
            this.menuWin.Size = new System.Drawing.Size(41, 20);
            this.menuWin.Text = "窗口";
            // 
            // menuSingleWin
            // 
            this.menuSingleWin.Name = "menuSingleWin";
            this.menuSingleWin.Size = new System.Drawing.Size(118, 22);
            this.menuSingleWin.Text = "独立窗口";
            this.menuSingleWin.Click += new System.EventHandler(this.menuSingleWin_Click);
            // 
            // menuOnlyWin
            // 
            this.menuOnlyWin.Name = "menuOnlyWin";
            this.menuOnlyWin.Size = new System.Drawing.Size(118, 22);
            this.menuOnlyWin.Text = "唯一窗口";
            this.menuOnlyWin.Click += new System.EventHandler(this.menuOnlyWin_Click);
            // 
            // menuTest
            // 
            this.menuTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示动画ToolStripMenuItem});
            this.menuTest.Name = "menuTest";
            this.menuTest.Size = new System.Drawing.Size(41, 20);
            this.menuTest.Text = "测试";
            // 
            // 显示动画ToolStripMenuItem
            // 
            this.显示动画ToolStripMenuItem.Name = "显示动画ToolStripMenuItem";
            this.显示动画ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.显示动画ToolStripMenuItem.Text = "显示分析动画";
            this.显示动画ToolStripMenuItem.Click += new System.EventHandler(this.显示动画ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 424);
            this.tabControl1.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 470);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志管理与审计系统";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDefineLogItem;
        private System.Windows.Forms.ToolStripMenuItem menuDefineAppStruct;
        private System.Windows.Forms.ToolStripMenuItem menuAppLogs;
        private System.Windows.Forms.ToolStripMenuItem menuWin;
        private System.Windows.Forms.ToolStripMenuItem menuSingleWin;
        private System.Windows.Forms.ToolStripMenuItem menuOnlyWin;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem menuSecurityEvent;
        private System.Windows.Forms.ToolStripMenuItem menuEditActionResults;
        private System.Windows.Forms.ToolStripMenuItem 维护安全事件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTest;
        private System.Windows.Forms.ToolStripMenuItem 显示动画ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDefineAppGroup;

    }
}

