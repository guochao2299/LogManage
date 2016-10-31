using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.AidedForms
{
    public partial class frmImportTables : Form
    {
        private StringBuilder m_errors = new StringBuilder();

        public frmImportTables(List<LogApp> lstApps)
        {
            InitializeComponent();

            this.treeView1.SuspendLayout();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                bool isExisted = false;

                foreach (LogApp app in lstApps)
                {
                    isExisted=AppService.Instance.IsAppExist(app.AppGUID);

                    TreeNode tn = new TreeNode(app.Name);
                    //tn.Tag = app.AppGUID;
                    tn.BackColor = isExisted ? picExisted.BackColor : SystemColors.Window;
                    tn.Tag = isExisted ? string.Format("应用程序\"{0}\"的GUID:\"{1}\"已经存在", app.Name, app.AppGUID) :
                        app.Name;

                    bool isNeedAppendLine = false;

                    if (isExisted)
                    {
                        m_errors.AppendLine(Convert.ToString(tn.Tag));
                        isNeedAppendLine = true;
                    }
                    
                    foreach (LogTable table in app.Tables)
                    {
                        string appInfo = AppService.Instance.IsTableExist(table.GUID);
                        isExisted = !string.IsNullOrEmpty(appInfo);

                        TreeNode tnSon = new TreeNode(table.Name);
                        //tnSon.Tag = table.GUID;
                        tnSon.BackColor = isExisted ? picExisted.BackColor : SystemColors.Window;
                        tnSon.Tag = isExisted ? String.Format("在\"{0}\"中已经存在表GUID:\"{1}\"", appInfo, table.GUID) :
                            table.Name;

                        if (isExisted)
                        {
                            m_errors.AppendLine("\t"+Convert.ToString(tnSon.Tag));
                            isNeedAppendLine = true;
                        }

                        foreach (LogTableItem item in table.Columns)
                        {
                            isExisted = LogColumnService.Instance.IsColumnIndexExist(item.LogColumnIndex);

                            TreeNode tnGrandson = new TreeNode(item.ColumnName);
                            //tnGrandson.Tag = item.LogColumnIndex;
                            tnGrandson.BackColor = isExisted ? SystemColors.Window : picNonExisting.BackColor;
                            tnGrandson.Tag = isExisted ? item.ColumnName :
                                string.Format("列编号为\"{0}\"的日志列\"{1}\"没有定义", item.LogColumnIndex, item.ColumnName);

                            if (!isExisted)
                            {
                                m_errors.AppendLine("\t\t"+Convert.ToString(tnGrandson.Tag));
                                isNeedAppendLine = true;
                            }

                            tnSon.Nodes.Add(tnGrandson);
                        }

                        tn.Nodes.Add(tnSon);                       
                    }

                    this.treeView1.Nodes.Add(tn);

                    if (isNeedAppendLine)
                    {
                        m_errors.AppendLine();
                        isNeedAppendLine = false;
                    }
                }

                if (this.treeView1.Nodes.Count > 0)
                {
                    this.treeView1.Nodes[0].ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建日志表结构视图失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.treeView1.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_errors.Length > 0)
            {                
                MessageBox.Show("存在以下问题，请解决后再进行导入：\r\n"+m_errors.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolTip1.SetToolTip(this.treeView1,Convert.ToString(e.Node.Tag));
        }

        private void cmNextError_Click(object sender, EventArgs e)
        {
            int index1st = 0;
            int index2st = 0;
            int index3st = 0;

            if (this.treeView1.SelectedNode != null)
            {
                switch (this.treeView1.SelectedNode.Level)
                {
                    case 0:
                        index1st = this.treeView1.SelectedNode.Index;
                        break;

                    case 1:
                        index2st = this.treeView1.SelectedNode.Index;
                        index1st = this.treeView1.SelectedNode.Parent.Index;
                        break;

                    case 2:
                        index3st = this.treeView1.SelectedNode.Index;
                        index2st = this.treeView1.SelectedNode.Parent.Index;
                        index1st = this.treeView1.SelectedNode.Parent.Parent.Index;
                        break;
                }
            }

            //for (int i = index1st; i < this.treeView1.Nodes.Count; i++)
            //{
            //    for (int j = index2st; j < this.treeView1.Nodes[i].Nodes.Count; j++)
            //    {
            //        for (int k = index3st; k < this.treeView1.Nodes[i].Nodes[j].Nodes.Count; k++)
            //        {
            //            if (this.treeView1.Nodes[i].Nodes[j].Nodes[k].BackColor != SystemColors.Window)
            //            {
            //                this.treeView1.
            //            }
            //        }
            //    }
            //}
        }
    }
}
