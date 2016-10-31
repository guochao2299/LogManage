namespace LogManage.AidedForms
{
    partial class frmEditAppStruct
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditAppStruct));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvApps = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmExportIdentity = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmReName = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDetails = new System.Windows.Forms.TabPage();
            this.lstDetails = new System.Windows.Forms.ListView();
            this.tpTableStruct = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.列名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuNewApp = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteApp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuNewLog = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuUndo = new System.Windows.Forms.ToolStripButton();
            this.menuRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExportTableStructs = new System.Windows.Forms.ToolStripButton();
            this.emnuImportTables = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpDetails.SuspendLayout();
            this.tpTableStruct.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(937, 481);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(937, 532);
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
            this.splitContainer1.Panel1.Controls.Add(this.tvApps);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(937, 481);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvApps
            // 
            this.tvApps.ContextMenuStrip = this.contextMenuStrip1;
            this.tvApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvApps.FullRowSelect = true;
            this.tvApps.HideSelection = false;
            this.tvApps.Location = new System.Drawing.Point(0, 0);
            this.tvApps.Name = "tvApps";
            this.tvApps.Size = new System.Drawing.Size(210, 481);
            this.tvApps.TabIndex = 0;
            this.tvApps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvApps_AfterSelect);
            this.tvApps.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvApps_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmExportIdentity,
            this.toolStripSeparator3,
            this.cmReName});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 54);
            // 
            // cmExportIdentity
            // 
            this.cmExportIdentity.Name = "cmExportIdentity";
            this.cmExportIdentity.Size = new System.Drawing.Size(142, 22);
            this.cmExportIdentity.Text = "导出身份信息";
            this.cmExportIdentity.Visible = false;
            this.cmExportIdentity.Click += new System.EventHandler(this.cmExportIdentity_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(139, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // cmReName
            // 
            this.cmReName.Name = "cmReName";
            this.cmReName.Size = new System.Drawing.Size(142, 22);
            this.cmReName.Text = "属性";
            this.cmReName.Click += new System.EventHandler(this.cmReName_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpDetails);
            this.tabControl1.Controls.Add(this.tpTableStruct);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(723, 481);
            this.tabControl1.TabIndex = 1;
            // 
            // tpDetails
            // 
            this.tpDetails.Controls.Add(this.lstDetails);
            this.tpDetails.Location = new System.Drawing.Point(4, 22);
            this.tpDetails.Name = "tpDetails";
            this.tpDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetails.Size = new System.Drawing.Size(715, 455);
            this.tpDetails.TabIndex = 0;
            this.tpDetails.Text = "详细信息";
            this.tpDetails.UseVisualStyleBackColor = true;
            // 
            // lstDetails
            // 
            this.lstDetails.BackColor = System.Drawing.SystemColors.Info;
            this.lstDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDetails.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstDetails.FullRowSelect = true;
            this.lstDetails.GridLines = true;
            this.lstDetails.HideSelection = false;
            this.lstDetails.Location = new System.Drawing.Point(3, 3);
            this.lstDetails.MultiSelect = false;
            this.lstDetails.Name = "lstDetails";
            this.lstDetails.Size = new System.Drawing.Size(709, 449);
            this.lstDetails.TabIndex = 0;
            this.lstDetails.UseCompatibleStateImageBehavior = false;
            this.lstDetails.View = System.Windows.Forms.View.Details;
            this.lstDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDetails_MouseDoubleClick);
            // 
            // tpTableStruct
            // 
            this.tpTableStruct.Controls.Add(this.tableLayoutPanel1);
            this.tpTableStruct.Location = new System.Drawing.Point(4, 22);
            this.tpTableStruct.Name = "tpTableStruct";
            this.tpTableStruct.Padding = new System.Windows.Forms.Padding(3);
            this.tpTableStruct.Size = new System.Drawing.Size(715, 455);
            this.tpTableStruct.TabIndex = 1;
            this.tpTableStruct.Text = "日志结构";
            this.tpTableStruct.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnUp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddNew, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 449);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(703, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "预览：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.列名,
            this.Column3,
            this.Column2,
            this.Column5,
            this.Column6,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.tableLayoutPanel1.SetRowSpan(this.dataGridView1, 4);
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(643, 386);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // 列名
            // 
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.Silver;
            this.列名.DefaultCellStyle = dataGridViewCellStyle26;
            this.列名.HeaderText = "列名";
            this.列名.Name = "列名";
            this.列名.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.Silver;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle27;
            this.Column3.HeaderText = "类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.Silver;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle28;
            this.Column2.HeaderText = "编号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "显示名称";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "显示";
            this.Column6.Name = "Column6";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "作为检索条件";
            this.Column4.Name = "Column4";
            // 
            // listView1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listView1, 2);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 415);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(703, 31);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.Location = new System.Drawing.Point(652, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(54, 92);
            this.btnUp.TabIndex = 4;
            this.btnUp.Text = "上移";
            this.toolTip1.SetToolTip(this.btnUp, "上移选中的行(上移一行，一次移动一列)");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Location = new System.Drawing.Point(652, 101);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(54, 92);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "下移";
            this.toolTip1.SetToolTip(this.btnDown, "下移选中的行(下移一行)，一次移动一列");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddNew.Location = new System.Drawing.Point(652, 199);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(54, 92);
            this.btnAddNew.TabIndex = 6;
            this.btnAddNew.Text = "添加  新列";
            this.toolTip1.SetToolTip(this.btnAddNew, "添加新的日志列，追加到最后");
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(652, 297);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(54, 92);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除 选中列";
            this.toolTip1.SetToolTip(this.btnDelete, "删除所有选中的行");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewApp,
            this.menuDeleteApp,
            this.toolStripSeparator1,
            this.menuNewLog,
            this.menuDeleteLog,
            this.toolStripSeparator2,
            this.menuUndo,
            this.menuRedo,
            this.toolStripSeparator4,
            this.menuExportTableStructs,
            this.emnuImportTables});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(621, 51);
            this.toolStrip1.TabIndex = 0;
            // 
            // menuNewApp
            // 
            this.menuNewApp.Image = ((System.Drawing.Image)(resources.GetObject("menuNewApp.Image")));
            this.menuNewApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewApp.Name = "menuNewApp";
            this.menuNewApp.Size = new System.Drawing.Size(81, 48);
            this.menuNewApp.Text = "新建应用程序";
            this.menuNewApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNewApp.Click += new System.EventHandler(this.menuNewApp_Click);
            // 
            // menuDeleteApp
            // 
            this.menuDeleteApp.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteApp.Image")));
            this.menuDeleteApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteApp.Name = "menuDeleteApp";
            this.menuDeleteApp.Size = new System.Drawing.Size(81, 48);
            this.menuDeleteApp.Text = "删除应用程序";
            this.menuDeleteApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteApp.Click += new System.EventHandler(this.menuDeleteApp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // menuNewLog
            // 
            this.menuNewLog.Image = ((System.Drawing.Image)(resources.GetObject("menuNewLog.Image")));
            this.menuNewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNewLog.Name = "menuNewLog";
            this.menuNewLog.Size = new System.Drawing.Size(81, 48);
            this.menuNewLog.Text = "新建日志结构";
            this.menuNewLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuNewLog.ToolTipText = "每种日志结构对应应用程序的一类日志";
            this.menuNewLog.Click += new System.EventHandler(this.menuNewLog_Click);
            // 
            // menuDeleteLog
            // 
            this.menuDeleteLog.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteLog.Image")));
            this.menuDeleteLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteLog.Name = "menuDeleteLog";
            this.menuDeleteLog.Size = new System.Drawing.Size(81, 48);
            this.menuDeleteLog.Text = "删除日志结构";
            this.menuDeleteLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteLog.ToolTipText = "目前删除结构不会删除已存在的日志";
            this.menuDeleteLog.Click += new System.EventHandler(this.menuDeleteLog_Click);
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
            this.menuUndo.Text = "撤消操作";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 51);
            // 
            // menuExportTableStructs
            // 
            this.menuExportTableStructs.Image = ((System.Drawing.Image)(resources.GetObject("menuExportTableStructs.Image")));
            this.menuExportTableStructs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuExportTableStructs.Name = "menuExportTableStructs";
            this.menuExportTableStructs.Size = new System.Drawing.Size(81, 48);
            this.menuExportTableStructs.Text = "导出日志结构";
            this.menuExportTableStructs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuExportTableStructs.Click += new System.EventHandler(this.menuExportTableStructs_Click);
            // 
            // emnuImportTables
            // 
            this.emnuImportTables.Image = ((System.Drawing.Image)(resources.GetObject("emnuImportTables.Image")));
            this.emnuImportTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.emnuImportTables.Name = "emnuImportTables";
            this.emnuImportTables.Size = new System.Drawing.Size(81, 48);
            this.emnuImportTables.Text = "导入日志结构";
            this.emnuImportTables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.emnuImportTables.Click += new System.EventHandler(this.emnuImportTables_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 500;
            // 
            // frmEditAppStruct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 532);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "frmEditAppStruct";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑应用系统日志结构";
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
            this.tabControl1.ResumeLayout(false);
            this.tpDetails.ResumeLayout(false);
            this.tpTableStruct.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton menuNewApp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvApps;
        private System.Windows.Forms.ToolStripButton menuDeleteApp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton menuNewLog;
        private System.Windows.Forms.ToolStripButton menuDeleteLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton menuUndo;
        private System.Windows.Forms.ToolStripButton menuRedo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmReName;
        private System.Windows.Forms.ToolStripMenuItem cmExportIdentity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton menuExportTableStructs;
        private System.Windows.Forms.ToolStripButton emnuImportTables;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 列名;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDetails;
        private System.Windows.Forms.TabPage tpTableStruct;
        private System.Windows.Forms.ListView lstDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}