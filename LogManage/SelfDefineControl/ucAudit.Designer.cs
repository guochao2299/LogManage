namespace LogManage.SelfDefineControl
{
    partial class ucAudit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnImportNewLogs = new System.Windows.Forms.Button();
            this.lstLogSource = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgvConditions = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.左边界 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.右边界 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAudit = new System.Windows.Forms.Button();
            this.myTabControl1 = new LogManage.SelfDefineControl.MyTabControl();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.Controls.Add(this.btnExportExcel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnImportNewLogs, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lstLogSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvConditions, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAudit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.myTabControl1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAnalysis, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 585);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Location = new System.Drawing.Point(791, 83);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(99, 34);
            this.btnExportExcel.TabIndex = 6;
            this.btnExportExcel.Text = "导出Excel文件";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnImportNewLogs
            // 
            this.btnImportNewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImportNewLogs.Location = new System.Drawing.Point(791, 43);
            this.btnImportNewLogs.Name = "btnImportNewLogs";
            this.btnImportNewLogs.Size = new System.Drawing.Size(99, 34);
            this.btnImportNewLogs.TabIndex = 5;
            this.btnImportNewLogs.Text = "导入新日志文件";
            this.btnImportNewLogs.UseVisualStyleBackColor = true;
            this.btnImportNewLogs.Click += new System.EventHandler(this.btnImportNewLogs_Click);
            // 
            // lstLogSource
            // 
            this.lstLogSource.CheckBoxes = true;
            this.lstLogSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstLogSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLogSource.Location = new System.Drawing.Point(3, 3);
            this.lstLogSource.Name = "lstLogSource";
            this.tableLayoutPanel1.SetRowSpan(this.lstLogSource, 4);
            this.lstLogSource.Size = new System.Drawing.Size(194, 154);
            this.lstLogSource.TabIndex = 0;
            this.lstLogSource.UseCompatibleStateImageBehavior = false;
            this.lstLogSource.View = System.Windows.Forms.View.Details;
            this.lstLogSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstLogSource_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日志来源";
            this.columnHeader1.Width = 184;
            // 
            // dgvConditions
            // 
            this.dgvConditions.AllowUserToAddRows = false;
            this.dgvConditions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConditions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.左边界,
            this.右边界});
            this.dgvConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditions.Location = new System.Drawing.Point(203, 3);
            this.dgvConditions.MultiSelect = false;
            this.dgvConditions.Name = "dgvConditions";
            this.dgvConditions.RowHeadersWidth = 25;
            this.tableLayoutPanel1.SetRowSpan(this.dgvConditions, 4);
            this.dgvConditions.RowTemplate.Height = 23;
            this.dgvConditions.Size = new System.Drawing.Size(582, 154);
            this.dgvConditions.TabIndex = 3;
            this.dgvConditions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditions_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "检索项";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "关系";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "内容";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 左边界
            // 
            this.左边界.HeaderText = "左边界";
            this.左边界.Name = "左边界";
            this.左边界.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.左边界.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.左边界.Width = 150;
            // 
            // 右边界
            // 
            this.右边界.HeaderText = "右边界";
            this.右边界.Name = "右边界";
            this.右边界.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.右边界.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.右边界.Width = 150;
            // 
            // btnAudit
            // 
            this.btnAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAudit.Location = new System.Drawing.Point(791, 3);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(99, 34);
            this.btnAudit.TabIndex = 4;
            this.btnAudit.Text = "日志检索";
            this.btnAudit.UseVisualStyleBackColor = true;
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // myTabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.myTabControl1, 3);
            this.myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl1.Location = new System.Drawing.Point(3, 163);
            this.myTabControl1.Name = "myTabControl1";
            this.myTabControl1.SelectedIndex = 0;
            this.myTabControl1.Size = new System.Drawing.Size(887, 419);
            this.myTabControl1.TabIndex = 7;
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnalysis.Location = new System.Drawing.Point(791, 123);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(99, 34);
            this.btnAnalysis.TabIndex = 8;
            this.btnAnalysis.Text = "分析日志";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // ucAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucAudit";
            this.Size = new System.Drawing.Size(893, 585);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnImportNewLogs;
        private System.Windows.Forms.ListView lstLogSource;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.DataGridView dgvConditions;
        private System.Windows.Forms.Button btnAudit;
        private SelfDefineControl.MyTabControl myTabControl1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn 左边界;
        private System.Windows.Forms.DataGridViewTextBoxColumn 右边界;
        private System.Windows.Forms.Button btnAnalysis;
    }
}
