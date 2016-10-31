namespace LogManage.DataType.Rules
{
    partial class ucCondition
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCondition));
            this.combo1st = new System.Windows.Forms.ComboBox();
            this.comboRelation = new System.Windows.Forms.ComboBox();
            this.rbConstant = new System.Windows.Forms.RadioButton();
            this.rbAnotherCol = new System.Windows.Forms.RadioButton();
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtLeftBound = new System.Windows.Forms.TextBox();
            this.txtRightBound = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConstant = new System.Windows.Forms.TabPage();
            this.tpCol = new System.Windows.Forms.TabPage();
            this.comboRightBound = new System.Windows.Forms.ComboBox();
            this.comboLeftBound = new System.Windows.Forms.ComboBox();
            this.comboContent = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblExpression = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExpand = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpConstant.SuspendLayout();
            this.tpCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // combo1st
            // 
            this.combo1st.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo1st.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo1st.FormattingEnabled = true;
            this.combo1st.Location = new System.Drawing.Point(4, 82);
            this.combo1st.Name = "combo1st";
            this.combo1st.Size = new System.Drawing.Size(114, 20);
            this.combo1st.TabIndex = 0;
            this.combo1st.SelectedIndexChanged += new System.EventHandler(this.combo1st_SelectedIndexChanged);
            this.combo1st.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // comboRelation
            // 
            this.comboRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRelation.FormattingEnabled = true;
            this.comboRelation.Location = new System.Drawing.Point(125, 82);
            this.comboRelation.Name = "comboRelation";
            this.comboRelation.Size = new System.Drawing.Size(114, 20);
            this.comboRelation.TabIndex = 1;
            this.comboRelation.SelectedIndexChanged += new System.EventHandler(this.comboRelation_SelectedIndexChanged);
            // 
            // rbConstant
            // 
            this.rbConstant.AutoSize = true;
            this.rbConstant.Checked = true;
            this.rbConstant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbConstant.Location = new System.Drawing.Point(246, 56);
            this.rbConstant.Name = "rbConstant";
            this.rbConstant.Size = new System.Drawing.Size(64, 19);
            this.rbConstant.TabIndex = 2;
            this.rbConstant.TabStop = true;
            this.rbConstant.Text = "固定值";
            this.rbConstant.UseVisualStyleBackColor = true;
            this.rbConstant.CheckedChanged += new System.EventHandler(this.rbConstant_CheckedChanged);
            this.rbConstant.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // rbAnotherCol
            // 
            this.rbAnotherCol.AutoSize = true;
            this.rbAnotherCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbAnotherCol.Location = new System.Drawing.Point(246, 82);
            this.rbAnotherCol.Name = "rbAnotherCol";
            this.rbAnotherCol.Size = new System.Drawing.Size(64, 19);
            this.rbAnotherCol.TabIndex = 3;
            this.rbAnotherCol.Text = "日志列";
            this.rbAnotherCol.UseVisualStyleBackColor = true;
            this.rbAnotherCol.CheckedChanged += new System.EventHandler(this.rbAnotherCol_CheckedChanged);
            this.rbAnotherCol.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // cbSelected
            // 
            this.cbSelected.AutoSize = true;
            this.cbSelected.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbSelected.Location = new System.Drawing.Point(3, 3);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(84, 23);
            this.cbSelected.TabIndex = 2;
            this.cbSelected.Text = "条件未选中";
            this.cbSelected.UseVisualStyleBackColor = true;
            this.cbSelected.CheckedChanged += new System.EventHandler(this.cbSelected_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "内容：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "左边界：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "右边界：";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(107, 4);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(155, 21);
            this.txtContent.TabIndex = 4;
            this.txtContent.TextChanged += new System.EventHandler(this.combo1st_Leave);
            this.txtContent.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // txtLeftBound
            // 
            this.txtLeftBound.Location = new System.Drawing.Point(107, 27);
            this.txtLeftBound.Name = "txtLeftBound";
            this.txtLeftBound.Size = new System.Drawing.Size(155, 21);
            this.txtLeftBound.TabIndex = 5;
            this.txtLeftBound.TextChanged += new System.EventHandler(this.combo1st_Leave);
            this.txtLeftBound.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // txtRightBound
            // 
            this.txtRightBound.Location = new System.Drawing.Point(107, 50);
            this.txtRightBound.Name = "txtRightBound";
            this.txtRightBound.Size = new System.Drawing.Size(155, 21);
            this.txtRightBound.TabIndex = 6;
            this.txtRightBound.TextChanged += new System.EventHandler(this.combo1st_Leave);
            this.txtRightBound.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(4, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "主体列：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(125, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "关系：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rbConstant, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.rbAnotherCol, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboRelation, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.combo1st, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 105);
            this.tableLayoutPanel1.TabIndex = 6;
            this.tableLayoutPanel1.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(246, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "比较对象";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpConstant);
            this.tabControl1.Controls.Add(this.tpCol);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(317, 4);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel1.SetRowSpan(this.tabControl1, 4);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(334, 97);
            this.tabControl1.TabIndex = 7;
            // 
            // tpConstant
            // 
            this.tpConstant.BackColor = System.Drawing.Color.Transparent;
            this.tpConstant.Controls.Add(this.txtContent);
            this.tpConstant.Controls.Add(this.txtRightBound);
            this.tpConstant.Controls.Add(this.label1);
            this.tpConstant.Controls.Add(this.txtLeftBound);
            this.tpConstant.Controls.Add(this.label2);
            this.tpConstant.Controls.Add(this.label3);
            this.tpConstant.Location = new System.Drawing.Point(4, 22);
            this.tpConstant.Name = "tpConstant";
            this.tpConstant.Padding = new System.Windows.Forms.Padding(3);
            this.tpConstant.Size = new System.Drawing.Size(326, 71);
            this.tpConstant.TabIndex = 0;
            this.tpConstant.Text = "固定值";
            this.tpConstant.Click += new System.EventHandler(this.tpConstant_Click);
            // 
            // tpCol
            // 
            this.tpCol.BackColor = System.Drawing.Color.Transparent;
            this.tpCol.Controls.Add(this.comboRightBound);
            this.tpCol.Controls.Add(this.comboLeftBound);
            this.tpCol.Controls.Add(this.comboContent);
            this.tpCol.Controls.Add(this.label7);
            this.tpCol.Controls.Add(this.label8);
            this.tpCol.Controls.Add(this.label9);
            this.tpCol.Location = new System.Drawing.Point(4, 22);
            this.tpCol.Name = "tpCol";
            this.tpCol.Padding = new System.Windows.Forms.Padding(3);
            this.tpCol.Size = new System.Drawing.Size(326, 71);
            this.tpCol.TabIndex = 1;
            this.tpCol.Text = "日志列";
            // 
            // comboRightBound
            // 
            this.comboRightBound.FormattingEnabled = true;
            this.comboRightBound.Location = new System.Drawing.Point(107, 47);
            this.comboRightBound.Name = "comboRightBound";
            this.comboRightBound.Size = new System.Drawing.Size(155, 20);
            this.comboRightBound.TabIndex = 9;
            this.comboRightBound.SelectedIndexChanged += new System.EventHandler(this.combo1st_Leave);
            this.comboRightBound.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // comboLeftBound
            // 
            this.comboLeftBound.FormattingEnabled = true;
            this.comboLeftBound.Location = new System.Drawing.Point(107, 26);
            this.comboLeftBound.Name = "comboLeftBound";
            this.comboLeftBound.Size = new System.Drawing.Size(155, 20);
            this.comboLeftBound.TabIndex = 8;
            this.comboLeftBound.SelectedIndexChanged += new System.EventHandler(this.combo1st_Leave);
            this.comboLeftBound.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // comboContent
            // 
            this.comboContent.FormattingEnabled = true;
            this.comboContent.Location = new System.Drawing.Point(107, 3);
            this.comboContent.Name = "comboContent";
            this.comboContent.Size = new System.Drawing.Size(155, 20);
            this.comboContent.TabIndex = 7;
            this.comboContent.SelectedIndexChanged += new System.EventHandler(this.combo1st_Leave);
            this.comboContent.Leave += new System.EventHandler(this.combo1st_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "内容：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "左边界：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(58, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "右边界：";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.LightSalmon;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Location = new System.Drawing.Point(125, 1);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 25);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(246, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 19);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "更    新";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblExpression
            // 
            this.lblExpression.AutoSize = true;
            this.lblExpression.BackColor = System.Drawing.Color.Transparent;
            this.lblExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpression.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExpression.Location = new System.Drawing.Point(123, 0);
            this.lblExpression.Name = "lblExpression";
            this.lblExpression.Size = new System.Drawing.Size(499, 29);
            this.lblExpression.TabIndex = 9;
            this.lblExpression.Text = "条件表达式";
            this.lblExpression.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(655, 138);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.lblExpression, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExpand, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbSelected, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(655, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnExpand
            // 
            this.btnExpand.BackColor = System.Drawing.Color.Cyan;
            this.btnExpand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpand.ImageIndex = 0;
            this.btnExpand.ImageList = this.imageList1;
            this.btnExpand.Location = new System.Drawing.Point(628, 3);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(24, 23);
            this.btnExpand.TabIndex = 0;
            this.btnExpand.UseVisualStyleBackColor = false;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Collapse_large.bmp");
            this.imageList1.Images.SetKeyName(1, "Expand_large.bmp");
            // 
            // ucCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "ucCondition";
            this.Size = new System.Drawing.Size(655, 138);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpConstant.ResumeLayout(false);
            this.tpConstant.PerformLayout();
            this.tpCol.ResumeLayout(false);
            this.tpCol.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combo1st;
        private System.Windows.Forms.ComboBox comboRelation;
        private System.Windows.Forms.RadioButton rbAnotherCol;
        private System.Windows.Forms.RadioButton rbConstant;
        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txtLeftBound;
        private System.Windows.Forms.TextBox txtRightBound;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpConstant;
        private System.Windows.Forms.TabPage tpCol;
        private System.Windows.Forms.ComboBox comboRightBound;
        private System.Windows.Forms.ComboBox comboLeftBound;
        private System.Windows.Forms.ComboBox comboContent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblExpression;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.ImageList imageList1;
    }
}
