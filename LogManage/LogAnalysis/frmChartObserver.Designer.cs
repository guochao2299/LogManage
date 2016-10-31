namespace LogManage.LogAnalysis
{
    partial class frmChartObserver
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGraph = new System.Windows.Forms.TabPage();
            this.drawBoard1 = new LogManage.SelfDefineControl.DrawBoard();
            this.tplog = new System.Windows.Forms.TabPage();
            this.myTabControl1 = new LogManage.SelfDefineControl.MyTabControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbBar = new System.Windows.Forms.RadioButton();
            this.rbPie = new System.Windows.Forms.RadioButton();
            this.lstDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbResult = new System.Windows.Forms.RadioButton();
            this.rbAction = new System.Windows.Forms.RadioButton();
            this.rbEvent = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGraph.SuspendLayout();
            this.tplog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.lstDetails);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(871, 494);
            this.splitContainer1.SplitterDistance = 573;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGraph);
            this.tabControl1.Controls.Add(this.tplog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(573, 494);
            this.tabControl1.TabIndex = 1;
            // 
            // tpGraph
            // 
            this.tpGraph.Controls.Add(this.drawBoard1);
            this.tpGraph.Location = new System.Drawing.Point(4, 26);
            this.tpGraph.Name = "tpGraph";
            this.tpGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpGraph.Size = new System.Drawing.Size(565, 464);
            this.tpGraph.TabIndex = 0;
            this.tpGraph.Text = "tabPage1";
            this.tpGraph.UseVisualStyleBackColor = true;
            // 
            // drawBoard1
            // 
            this.drawBoard1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.drawBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawBoard1.Location = new System.Drawing.Point(3, 3);
            this.drawBoard1.Name = "drawBoard1";
            this.drawBoard1.Size = new System.Drawing.Size(559, 458);
            this.drawBoard1.TabIndex = 0;
            this.drawBoard1.Text = "drawBoard1";
            this.drawBoard1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.drawBoard1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.drawBoard1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.drawBoard1_MouseDoubleClick);
            // 
            // tplog
            // 
            this.tplog.Controls.Add(this.myTabControl1);
            this.tplog.Location = new System.Drawing.Point(4, 26);
            this.tplog.Name = "tplog";
            this.tplog.Padding = new System.Windows.Forms.Padding(3);
            this.tplog.Size = new System.Drawing.Size(565, 464);
            this.tplog.TabIndex = 1;
            this.tplog.Text = "日志内容";
            this.tplog.UseVisualStyleBackColor = true;
            // 
            // myTabControl1
            // 
            this.myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myTabControl1.Location = new System.Drawing.Point(3, 3);
            this.myTabControl1.Name = "myTabControl1";
            this.myTabControl1.SelectedIndex = 0;
            this.myTabControl1.Size = new System.Drawing.Size(559, 458);
            this.myTabControl1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBar);
            this.groupBox2.Controls.Add(this.rbPie);
            this.groupBox2.Location = new System.Drawing.Point(158, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 93);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示方式";
            // 
            // rbBar
            // 
            this.rbBar.AutoSize = true;
            this.rbBar.Location = new System.Drawing.Point(33, 60);
            this.rbBar.Name = "rbBar";
            this.rbBar.Size = new System.Drawing.Size(59, 16);
            this.rbBar.TabIndex = 0;
            this.rbBar.TabStop = true;
            this.rbBar.Text = "柱状图";
            this.rbBar.UseVisualStyleBackColor = true;
            this.rbBar.CheckedChanged += new System.EventHandler(this.rbBar_CheckedChanged);
            // 
            // rbPie
            // 
            this.rbPie.AutoSize = true;
            this.rbPie.Checked = true;
            this.rbPie.Location = new System.Drawing.Point(33, 26);
            this.rbPie.Name = "rbPie";
            this.rbPie.Size = new System.Drawing.Size(47, 16);
            this.rbPie.TabIndex = 0;
            this.rbPie.TabStop = true;
            this.rbPie.Text = "饼图";
            this.rbPie.UseVisualStyleBackColor = true;
            this.rbPie.CheckedChanged += new System.EventHandler(this.rbPie_CheckedChanged);
            // 
            // lstDetails
            // 
            this.lstDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstDetails.FullRowSelect = true;
            this.lstDetails.HideSelection = false;
            this.lstDetails.Location = new System.Drawing.Point(13, 111);
            this.lstDetails.MultiSelect = false;
            this.lstDetails.Name = "lstDetails";
            this.lstDetails.Size = new System.Drawing.Size(269, 371);
            this.lstDetails.TabIndex = 1;
            this.lstDetails.UseCompatibleStateImageBehavior = false;
            this.lstDetails.View = System.Windows.Forms.View.Details;
            this.lstDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstDetails_MouseClick);
            this.lstDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDetails_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "意义";
            this.columnHeader1.Width = 148;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "比例";
            this.columnHeader2.Width = 59;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数量";
            this.columnHeader3.Width = 58;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbResult);
            this.groupBox1.Controls.Add(this.rbAction);
            this.groupBox1.Controls.Add(this.rbEvent);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分类方式";
            // 
            // rbResult
            // 
            this.rbResult.AutoSize = true;
            this.rbResult.Location = new System.Drawing.Point(34, 64);
            this.rbResult.Name = "rbResult";
            this.rbResult.Size = new System.Drawing.Size(71, 16);
            this.rbResult.TabIndex = 0;
            this.rbResult.TabStop = true;
            this.rbResult.Text = "行为后果";
            this.rbResult.UseVisualStyleBackColor = true;
            this.rbResult.CheckedChanged += new System.EventHandler(this.rbResult_CheckedChanged);
            // 
            // rbAction
            // 
            this.rbAction.AutoSize = true;
            this.rbAction.Location = new System.Drawing.Point(34, 42);
            this.rbAction.Name = "rbAction";
            this.rbAction.Size = new System.Drawing.Size(71, 16);
            this.rbAction.TabIndex = 0;
            this.rbAction.TabStop = true;
            this.rbAction.Text = "安全行为";
            this.rbAction.UseVisualStyleBackColor = true;
            this.rbAction.CheckedChanged += new System.EventHandler(this.rbAction_CheckedChanged);
            // 
            // rbEvent
            // 
            this.rbEvent.AutoSize = true;
            this.rbEvent.Checked = true;
            this.rbEvent.Location = new System.Drawing.Point(34, 20);
            this.rbEvent.Name = "rbEvent";
            this.rbEvent.Size = new System.Drawing.Size(71, 16);
            this.rbEvent.TabIndex = 0;
            this.rbEvent.TabStop = true;
            this.rbEvent.Text = "安全事件";
            this.rbEvent.UseVisualStyleBackColor = true;
            this.rbEvent.CheckedChanged += new System.EventHandler(this.rbEvent_CheckedChanged);
            // 
            // frmChartObserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 494);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChartObserver";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安全事件统计图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPieChartObserver_FormClosing);
            this.Load += new System.EventHandler(this.frmPieChartObserver_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpGraph.ResumeLayout(false);
            this.tplog.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbResult;
        private System.Windows.Forms.RadioButton rbAction;
        private System.Windows.Forms.RadioButton rbEvent;
        private System.Windows.Forms.ListView lstDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbBar;
        private System.Windows.Forms.RadioButton rbPie;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private SelfDefineControl.DrawBoard drawBoard1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGraph;
        private System.Windows.Forms.TabPage tplog;
        private SelfDefineControl.MyTabControl myTabControl1;
    }
}