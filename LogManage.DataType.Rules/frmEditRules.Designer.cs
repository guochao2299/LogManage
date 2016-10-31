namespace LogManage.DataType.Rules
{
    partial class frmEditRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditRules));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvEvents = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuNewEvent = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteEvent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuNewUserAction = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteUserAction = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuNewCondition = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteCondition = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuUndo = new System.Windows.Forms.ToolStripButton();
            this.menuRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExportEventStruct = new System.Windows.Forms.ToolStripButton();
            this.menuImportEventStruct = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(908, 572);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(908, 623);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvEvents);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(908, 572);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvEvents
            // 
            this.tvEvents.ContextMenuStrip = this.contextMenuStrip1;
            this.tvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvEvents.FullRowSelect = true;
            this.tvEvents.HideSelection = false;
            this.tvEvents.Location = new System.Drawing.Point(0, 0);
            this.tvEvents.Name = "tvEvents";
            this.tvEvents.Size = new System.Drawing.Size(245, 572);
            this.tvEvents.TabIndex = 0;
            this.tvEvents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEvents_AfterSelect);
            this.tvEvents.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvEvents_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmProperties});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            // 
            // cmProperties
            // 
            this.cmProperties.Name = "cmProperties";
            this.cmProperties.Size = new System.Drawing.Size(94, 22);
            this.cmProperties.Text = "属性";
            this.cmProperties.DropDownOpening += new System.EventHandler(this.cmProperties_DropDownOpening);
            this.cmProperties.Click += new System.EventHandler(this.cmProperties_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(659, 572);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewEvent,
            this.menuDeleteEvent,
            this.toolStripSeparator1,
            this.menuNewUserAction,
            this.menuDeleteUserAction,
            this.toolStripSeparator4,
            this.menuNewCondition,
            this.menuDeleteCondition,
            this.toolStripSeparator2,
            this.menuUndo,
            this.menuRedo,
            this.toolStripSeparator3,
            this.menuExportEventStruct,
            this.menuImportEventStruct});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(741, 51);
            this.toolStrip1.TabIndex = 0;
            // 
            // menuNewEvent
            // 
            this.menuNewEvent.Image = ((System.Drawing.Image)(resources.GetObject("menuNewEvent.Image")));
            this.menuNewEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewEvent.Name = "menuNewEvent";
            this.menuNewEvent.Size = new System.Drawing.Size(81, 48);
            this.menuNewEvent.Text = "新建安全事件";
            this.menuNewEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNewEvent.Click += new System.EventHandler(this.menuNewEvent_Click);
            // 
            // menuDeleteEvent
            // 
            this.menuDeleteEvent.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteEvent.Image")));
            this.menuDeleteEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteEvent.Name = "menuDeleteEvent";
            this.menuDeleteEvent.Size = new System.Drawing.Size(81, 48);
            this.menuDeleteEvent.Text = "删除安全事件";
            this.menuDeleteEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteEvent.Click += new System.EventHandler(this.menuDeleteEvent_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // menuNewUserAction
            // 
            this.menuNewUserAction.Image = ((System.Drawing.Image)(resources.GetObject("menuNewUserAction.Image")));
            this.menuNewUserAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewUserAction.Name = "menuNewUserAction";
            this.menuNewUserAction.Size = new System.Drawing.Size(81, 48);
            this.menuNewUserAction.Text = "新建安全行为";
            this.menuNewUserAction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNewUserAction.Click += new System.EventHandler(this.menuNewUserAction_Click);
            // 
            // menuDeleteUserAction
            // 
            this.menuDeleteUserAction.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteUserAction.Image")));
            this.menuDeleteUserAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteUserAction.Name = "menuDeleteUserAction";
            this.menuDeleteUserAction.Size = new System.Drawing.Size(81, 48);
            this.menuDeleteUserAction.Text = "删除安全行为";
            this.menuDeleteUserAction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteUserAction.Click += new System.EventHandler(this.menuDeleteUserAction_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 51);
            // 
            // menuNewCondition
            // 
            this.menuNewCondition.Image = ((System.Drawing.Image)(resources.GetObject("menuNewCondition.Image")));
            this.menuNewCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewCondition.Name = "menuNewCondition";
            this.menuNewCondition.Size = new System.Drawing.Size(57, 48);
            this.menuNewCondition.Text = "新建条件";
            this.menuNewCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNewCondition.Click += new System.EventHandler(this.menuNewCondition_Click);
            // 
            // menuDeleteCondition
            // 
            this.menuDeleteCondition.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteCondition.Image")));
            this.menuDeleteCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteCondition.Name = "menuDeleteCondition";
            this.menuDeleteCondition.Size = new System.Drawing.Size(57, 48);
            this.menuDeleteCondition.Text = "删除条件";
            this.menuDeleteCondition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteCondition.Click += new System.EventHandler(this.menuDeleteCondition_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 51);
            // 
            // menuUndo
            // 
            this.menuUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuUndo.Image")));
            this.menuUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.Size = new System.Drawing.Size(57, 48);
            this.menuUndo.Text = "撤销操作";
            this.menuUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUndo.Click += new System.EventHandler(this.menuUndo_Click);
            // 
            // menuRedo
            // 
            this.menuRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuRedo.Image")));
            this.menuRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuRedo.Name = "menuRedo";
            this.menuRedo.Size = new System.Drawing.Size(57, 48);
            this.menuRedo.Text = "重复操作";
            this.menuRedo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuRedo.Click += new System.EventHandler(this.menuRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 51);
            // 
            // menuExportEventStruct
            // 
            this.menuExportEventStruct.Image = ((System.Drawing.Image)(resources.GetObject("menuExportEventStruct.Image")));
            this.menuExportEventStruct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuExportEventStruct.Name = "menuExportEventStruct";
            this.menuExportEventStruct.Size = new System.Drawing.Size(81, 48);
            this.menuExportEventStruct.Text = "导出事件结构";
            this.menuExportEventStruct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuImportEventStruct
            // 
            this.menuImportEventStruct.Image = ((System.Drawing.Image)(resources.GetObject("menuImportEventStruct.Image")));
            this.menuImportEventStruct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuImportEventStruct.Name = "menuImportEventStruct";
            this.menuImportEventStruct.Size = new System.Drawing.Size(81, 48);
            this.menuImportEventStruct.Text = "导入事件结构";
            this.menuImportEventStruct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmEditRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 623);
            this.Controls.Add(this.toolStripContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditRules";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护安全事件";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvEvents;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton menuNewEvent;
        private System.Windows.Forms.ToolStripButton menuDeleteEvent;
        private System.Windows.Forms.ToolStripButton menuNewUserAction;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton menuDeleteUserAction;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton menuExportEventStruct;
        private System.Windows.Forms.ToolStripButton menuImportEventStruct;
        private System.Windows.Forms.ToolStripButton menuUndo;
        private System.Windows.Forms.ToolStripButton menuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton menuNewCondition;
        private System.Windows.Forms.ToolStripButton menuDeleteCondition;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmProperties;
    }
}