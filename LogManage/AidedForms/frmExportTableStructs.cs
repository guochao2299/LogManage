using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.AidedForms
{
    public partial class frmExportTableStructs : Form
    {
        public frmExportTableStructs()
        {
            InitializeComponent();
        }

        private void InitTreeView()
        {
            this.Cursor = Cursors.WaitCursor;
            this.treeView1.SuspendLayout();            

            try
            {
                this.treeView1.Nodes.Clear();

                foreach (LogApp app in AppService.Instance.ExistingApps.Values)
                {
                    if (app.Tables.Count > 0)
                    {
                        TreeNode tn = new TreeNode(app.Name);
                        tn.Tag = app.AppGUID;

                        foreach (LogTable table in app.Tables)
                        {
                            TreeNode tnSon = new TreeNode(table.Name);
                            tnSon.Tag = table.GUID;
                            tn.Nodes.Add(tnSon);
                        }

                        this.treeView1.Nodes.Add(tn);
                    }
                }

                if (this.treeView1.Nodes.Count > 0)
                {
                    this.treeView1.Nodes[0].Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化日志结构失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.treeView1.ResumeLayout();
                this.Cursor = Cursors.Default;
            }           
        }

        private void frmExportTableStructs_Load(object sender, EventArgs e)
        {
            InitTreeView();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }
            }           
        }

        private bool IsAnyLogTableSelected()
        {
            foreach (TreeNode tn in this.treeView1.Nodes)
            {
                foreach (TreeNode tnSon in tn.Nodes)
                {
                    if (tnSon.Checked)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsAnyLogTableSelected())
            {
                MessageBox.Show("请先选中要导入结构的日志表");
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "日志表结构.xml";
            sfd.Filter = "xml 文件(*.xml)|*.xml";

            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                lblStatus.Text = sfd.FileName;
                XmlTextWriter writer = null;

                try
                {
                    List<LogApp> lstApps = new List<LogApp>();
                    List<LogTable> lstTables = null;

                    foreach (TreeNode tn in this.treeView1.Nodes)
                    {
                        lstTables = null;
                        string appGuid = Convert.ToString(tn.Tag);

                        foreach (TreeNode tnSon in tn.Nodes)
                        {
                            if (tnSon.Checked)
                            {
                                if (lstTables == null)
                                {
                                    lstTables = new List<LogTable>();
                                }

                                string tableGuid = Convert.ToString(tnSon.Tag);

                                LogTable table = (LogTable)AppService.Instance.GetAppTable(appGuid, tableGuid).Clone();

                                foreach (LogTableItem item in table.Columns)
                                {
                                    item.ColumnName = LogColumnService.Instance.GetColumnName(item.LogColumnIndex);
                                }

                                lstTables.Add(table);
                            }
                        }

                        if (lstTables != null)
                        {
                            LogApp srcApp = AppService.Instance.GetApp(appGuid);
                            LogApp app = LogApp.CreateApp(srcApp.Name, srcApp.AppGUID, srcApp.IsImportLogsFromFiles);
                            app.Tables.AddRange(lstTables);
                            lstApps.Add(app);

                            lstTables = null;
                        }
                    }

                    writer = new XmlTextWriter(sfd.FileName, Encoding.Default);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<LogApp>));
                    serializer.Serialize(writer, lstApps);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出日志表结构失败,错误消息为:" + ex.Message);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }
    }
}
