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
using LogManage.UndoRedo;

namespace LogManage.AidedForms
{
    public partial class frmEditAppStruct : Form
    {
        public frmEditAppStruct()
        {
            InitializeComponent();
            m_undoRedoBuffer = new CommandBox();

            InitTree();

            UpdateAppMenuStatus();
            UpdateLogTableMenuStatus();
            UpdateUndoRedoStatus();
        }

        private void InitTree()
        {
            this.tvApps.SuspendLayout();
            this.tvApps.Nodes.Clear();

            try
            {
                Dictionary<string, LogApp> apps = AppService.Instance.ExistingApps;

                foreach (LogAppGroup lag in AppService.Instance.ExistingAppGroups.Values)
                {
                    TreeNode tn = new TreeNode(lag.Name);
                    tn.Tag = lag.Name;

                    foreach (LogApp app in apps.Values)
                    {
                        if (!string.Equals(app.Group.Name, lag.Name))
                        {
                            continue;
                        }

                        TreeNode tnApp = new TreeNode(app.Name);
                        tnApp.Tag = app.AppGUID;

                        if (app.Tables.Count > 0)
                        {
                            foreach (LogTable table in app.Tables)
                            {
                                TreeNode tnSon = new TreeNode(table.Name);
                                tnSon.Tag = table.GUID;

                                tnApp.Nodes.Add(tnSon);
                            }
                        }

                        tn.Nodes.Add(tnApp);
                    }

                    this.tvApps.Nodes.Add(tn);
                }                

                if (this.tvApps.Nodes.Count > 0)
                {
                    this.tvApps.SelectedNode = this.tvApps.Nodes[0];

                    TreeNodeMouseClickEventArgs e=new TreeNodeMouseClickEventArgs(this.tvApps.Nodes[0],
                        MouseButtons.Left,1,0,0);
                    tvApps_NodeMouseClick(this.tvApps, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("构建应用程序树失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.tvApps.ResumeLayout();
            }            
        }

        private CommandBox m_undoRedoBuffer = null;

        private const int AppGroupNodeLevel = 0;
        private const int AppNodeLevel = 1;
        private const int LogStructNodeLevel = 2;

        private const int LogItemIsFilterIndex = 6;
        private const int LogColumnNameIndex = 1;
        private const int LogColumnIndexIndex = 3;
        private const int SelectIndex=0;
        private const int LogColumnNickNameIndex = 4;
        private const int LogColumnVisibleIndex = 5;

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string appGuid=Convert.ToString(tvApps.SelectedNode.Parent.Tag);
                string tableGuid=Convert.ToString(tvApps.SelectedNode.Tag);

                this.Cursor = Cursors.WaitCursor;

                List<Int32> lstExistingIndexes = new List<int>();
                foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
                {
                    lstExistingIndexes.Add(Convert.ToInt32(dgvr.Cells[LogColumnIndexIndex].Value));
                }

                frmEditItem fe = new frmEditItem(true, lstExistingIndexes);
                if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                {
                    List<LogTableItem> lstSelectItems = new List<LogTableItem>();

                    foreach (int lcIndex in fe.SelectLogColumnIndex)
                    {
                        lstSelectItems.Add(LogTableItem.CreateNewLogTableItem(lcIndex));
                    }

                    if (lstSelectItems.Count > 0)
                    {
                        AddNewTableColumnsCommand cmd = new AddNewTableColumnsCommand(appGuid, tableGuid, lstSelectItems);
                        cmd.UndoDone += new UndoRedoEventHandler(RemoveTableRows);
                        cmd.RedoDone += new UndoRedoEventHandler(AddTableRows);

                        cmd.Execute();
                        AddCommand(cmd);
                        AddTableRows(lstSelectItems);
                        UpdatePreView();                                            
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加新的日志列失败，错误消息为：" + ex.Message);
            }

            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void RemoveTableRows(UndoRedoEventArg e)
        {
            RemoveTableRows((List<LogTableItem>)e.Tag);
        }

        private void RemoveTableRows(List<LogTableItem> lstItems)
        {
            this.dataGridView1.SuspendLayout();

            try
            {
                List<int> lstIndexes=new List<int>();
                foreach(LogTableItem lti in lstItems)
                {
                    lstIndexes.Add(lti.LogColumnIndex);
                }

                for (int i = this.dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    int colIndex=Convert.ToInt32(this.dataGridView1.Rows[i].Cells[LogColumnIndexIndex].Value);

                    if (lstIndexes.Contains(colIndex))
                    {
                        this.dataGridView1.Rows.RemoveAt(i);
                    }
                }                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加日志列失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.dataGridView1.ResumeLayout();
            }


            UpdatePreView();
        }

        private void AddTableRows(UndoRedoEventArg e)
        {
            AddTableRows((List<LogTableItem>)e.Tag);
        }

        private void AddRow(LogTableItem item)
        { 
            InsertRow(this.dataGridView1.Rows.Count, false, item, LogColumnService.Instance.GetLogColumn(item.LogColumnIndex));
        }

        private void InsertRow(int rowIndex, bool isChecked, LogTableItem item, LogColumn lc)
        {            
            this.dataGridView1.Rows.Insert(rowIndex, new object[] { isChecked, lc.Name, lc.Type, lc.Index, item.NickName, item.Visible, item.IsFilterColumn });
        }

        private LogTableItem GetLogTableItemRow(int rowIndex)
        {
            LogTableItem newItem = LogTableItem.CreateNewLogTableItem(
                                       Convert.ToInt32(this.dataGridView1.Rows[rowIndex].Cells[LogColumnIndexIndex].Value),
                                       Convert.ToBoolean(this.dataGridView1.Rows[rowIndex].Cells[LogItemIsFilterIndex].Value));
            newItem.Visible = Convert.ToBoolean(this.dataGridView1.Rows[rowIndex].Cells[LogColumnVisibleIndex].Value);
            newItem.NickName = Convert.ToString(this.dataGridView1.Rows[rowIndex].Cells[LogColumnNickNameIndex].Value);

            return newItem;
        }

        private void SetRowContent(LogTable table,int rowIndex, bool isChecked)
        {
            LogTableItem item = GetLogTableItemRow(rowIndex);
            LogColumn lc = LogColumnService.Instance.GetLogColumn(item.LogColumnIndex);

            this.dataGridView1.Rows.RemoveAt(rowIndex);
            InsertRow(rowIndex,isChecked,item,lc);
        }

        private void AddTableRows(List<LogTableItem> lstItems)
        {
            this.dataGridView1.SuspendLayout();

            try
            {
                foreach (LogTableItem lti in lstItems)
                {
                    AddRow(lti);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加日志列失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.dataGridView1.ResumeLayout();
            }
            
            
            UpdatePreView();
        }

        private void UpdatePreView()
        {
            this.listView1.SuspendLayout();

            try
            {
                this.listView1.Columns.Clear();

                foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
                {
                    if (!Convert.ToBoolean(dgvr.Cells[LogColumnVisibleIndex].Value))
                    {
                        continue;
                    }

                    string nickName = Convert.ToString(dgvr.Cells[LogColumnNickNameIndex].Value);

                    ColumnHeader ch=new ColumnHeader();

                    if (string.IsNullOrWhiteSpace(nickName))
                    {
                         ch.Text=dgvr.Cells[LogColumnNameIndex].Value.ToString();                        
                    }
                    else
                    {
                        ch.Text=nickName;
                    }

                    ch.TextAlign = HorizontalAlignment.Center;
                    ch.Width = Convert.ToInt32(TextRenderer.MeasureText(ch.Text, this.listView1.Font).Width * 1.5f);

                    this.listView1.Columns.Add(ch);
                }                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成日志预览时出错，错误消息为：" + ex.Message);
            }
            finally
            {
                this.listView1.ResumeLayout();
            }
        }

        private void UpdateUndoRedoStatus()
        {
            menuUndo.Enabled = this.m_undoRedoBuffer.CanUndo;
            menuRedo.Enabled = this.m_undoRedoBuffer.CanRedo;
        }

        private void UpdateLogTableMenuStatus()
        {
            this.menuNewLog.Enabled = this.tvApps.SelectedNode != null;
            this.menuDeleteLog.Enabled = (this.tvApps.SelectedNode != null) && (this.tvApps.SelectedNode.Level == LogStructNodeLevel);
        }

        private void UpdateAppMenuStatus()
        {
            this.menuNewApp.Enabled = true;
            this.menuDeleteApp.Enabled = (this.tvApps.SelectedNode != null) && (this.tvApps.SelectedNode.Level == AppNodeLevel);
        }

        private void AddCommand(ICommand cmd)
        {
            m_undoRedoBuffer.AddCommand(cmd);
            UpdateUndoRedoStatus();
        }

        private TreeNode FindAppGroupNode(string groupName)
        {
            foreach (TreeNode tn in this.tvApps.Nodes)
            {
                if (string.Equals(groupName, tn.Text))
                {
                    return tn;
                }
            }

            return null;
        }

        #region 应用程序操作
        private void AddApp2Tree(UndoRedoEventArg e)
        {
            AddApp2Tree((LogApp)e.Tag);
        }
        private void AddApp2Tree(LogApp app)
        {
            TreeNode tnParent = FindAppGroupNode(app.Group.Name);

            if (tnParent == null)
            {
                MessageBox.Show("没有找到系统所属分组\"" + app.Group.Name + "\"，无法将应用程序添加到指定分组");
                return;
            }

            TreeNode tn = new TreeNode(app.Name);
            tn.Tag = app.AppGUID;
            tnParent.Nodes.Add(tn);
            this.tvApps.SelectedNode = tn;

            UpdateAppMenuStatus();
        }

        private void RefreshAppNodeName(UndoRedoEventArg e)
        {
            LogAppMemento memento = (LogAppMemento)e.Tag;
            RefreshAppNodeName(e.FirstLevelGuid, memento.LogAppName);
        }

        private void RefreshAppNodeName(string sysGuid, string newName)
        {
            foreach (TreeNode tn in this.tvApps.Nodes)
            {
                if (string.Equals(Convert.ToString(tn.Tag), sysGuid, StringComparison.OrdinalIgnoreCase))
                {
                    tn.Text = newName;
                    break;
                }
            }
        }

        private void RemoveAppFromTree(UndoRedoEventArg e)
        {
            RemoveAppFromTree((LogApp)e.Tag);
        }
        private void RemoveAppFromTree(LogApp app)
        {
            TreeNode tnParent = FindAppGroupNode(app.Group.Name);

            if (tnParent == null)
            {
                MessageBox.Show("没有找到系统所属分组\"" + app.Group.Name + "\"，无法将应用程序删除");
                return;
            }

            for (int i = tnParent.Nodes.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Convert.ToString(tnParent.Nodes[i].Tag), app.AppGUID, StringComparison.OrdinalIgnoreCase))
                {
                    tnParent.Nodes.RemoveAt(i);
                    break;
                }
            }

            this.tvApps.SelectedNode = tnParent;

            UpdateAppMenuStatus();
        }

        private void menuNewApp_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string groupName = string.Empty;

                if (tvApps.SelectedNode != null)
                {
                    TreeNode tnTop = tvApps.SelectedNode;

                    while (tnTop.Parent != null)
                    {
                        tnTop = tnTop.Parent;
                    }

                    groupName = tnTop.Text;
                }

                frmEditAppProperty fe = new frmEditAppProperty(ConstAppValue.DefaultAppName, ConstAppValue.DefaultIsLogsFromFile, groupName);
                fe.Text = "编辑应用程序名称";

                if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                {
                    LogApp la = LogApp.NewApplication;
                    la.Name = fe.EditedName;
                    la.IsImportLogsFromFiles = fe.IsImportLogsFromFile;
                    la.Group = fe.Group;

                    CreateNewAppCommand cmd = new CreateNewAppCommand(la);
                    cmd.UndoDone += new UndoRedoEventHandler(RemoveAppFromTree);
                    cmd.RedoDone += new UndoRedoEventHandler(AddApp2Tree);
                    cmd.Execute();
                    
                    AddCommand(cmd);
                    AddApp2Tree(la);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建新工艺失败,错误消息为：" + ex.Message);
            }
        }

        private void menuDeleteApp_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (MessageBox.Show(string.Format("确定删除应用程序\"{0}\"及下面的结构", this.tvApps.SelectedNode.Text),
                    "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string appGuid = Convert.ToString(this.tvApps.SelectedNode.Tag);
                DeleteAppCommand cmd = new DeleteAppCommand(AppService.Instance.GetApp(appGuid));
                cmd.RedoDone += new UndoRedoEventHandler(RemoveAppFromTree);
                cmd.UndoDone += new UndoRedoEventHandler(AddApp2Tree);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveAppFromTree(AppService.Instance.GetApp(appGuid));                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除新工艺失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region 日志表操作

        private bool FindParentNode(string appGuid, out TreeNode pNode)
        {
            LogApp app = AppService.Instance.GetApp(appGuid);

            TreeNode tnParent = FindAppGroupNode(app.Group.Name);

            if (tnParent == null)
            {
                MessageBox.Show("无法找到分组\"" + app.Group.Name + "\"的节点");
                pNode = null;
                return false;
            }

            pNode = null;

            foreach (TreeNode parentNode in tnParent.Nodes)
            {
                if (string.Equals(Convert.ToString(parentNode.Tag), appGuid, StringComparison.OrdinalIgnoreCase))
                {
                    pNode = parentNode;
                    return true;
                }
            }

            return false;
        }

        private void AddTable2Tree(UndoRedoEventArg e)
        {
            AddTable2Tree(e.FirstLevelGuid,e.SecondLevelGuid,Convert.ToString(e.Tag));
        }
        private void AddTable2Tree(string appGuid, string tableGuid,string tableName)
        {
            TreeNode tn = new TreeNode(tableName);
            tn.Tag = tableGuid;

            TreeNode pNode = null;
            if (!FindParentNode(appGuid,out pNode))
            {
                return;
            }

            pNode.Nodes.Add(tn);
            this.tvApps.SelectedNode = tn;

            UpdateLogTableMenuStatus();
        }

        private void RefreshTableNodeName(UndoRedoEventArg e)
        {
            RefreshTableNodeName(e.FirstLevelGuid,e.SecondLevelGuid, Convert.ToString(e.Tag));
        }

        private void RefreshTableNodeName(string appGuid, string tableGuid,string newName)
        {
            TreeNode pNode = null;
            if (!FindParentNode(appGuid, out pNode))
            {
                return;
            }

            foreach (TreeNode tn in pNode.Nodes)
            {
                if (string.Equals(Convert.ToString(tn.Tag), tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    tn.Text = newName;
                    break;
                }
            }
        }

        private void RemoveTableFromTree(UndoRedoEventArg e)
        {
            RemoveTableFromTree(e.FirstLevelGuid,e.SecondLevelGuid);
        }
        private void RemoveTableFromTree(string appGuid,string tableGuid)
        {
            TreeNode pNode = null;
            if (!FindParentNode(appGuid, out pNode))
            {
                return;
            }

            for (int i = pNode.Nodes.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Convert.ToString(pNode.Nodes[i].Tag), tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    pNode.Nodes.RemoveAt(i);
                    break;
                }
            }

            this.tvApps.SelectedNode = null;

            UpdateLogTableMenuStatus();
        }

        private void menuNewLog_Click(object sender, EventArgs e)
        {
            try
            {
                string appGuid = Convert.ToString((this.tvApps.SelectedNode.Level==AppNodeLevel)?
                    this.tvApps.SelectedNode.Tag : this.tvApps.SelectedNode.Parent.Tag);

                this.Cursor = Cursors.WaitCursor;
                frmEditTableProperty fe = new frmEditTableProperty(ConstTableValue.DefaultLogTableName);
                fe.Text = "编辑日志表名称";

                if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                {
                    LogTable lt = LogTable.NewLogTable;
                    lt.Name = fe.EditedName;

                    CreateNewLogStructCommand cmd = new CreateNewLogStructCommand(appGuid, lt);
                    cmd.UndoDone += new UndoRedoEventHandler(RemoveTableFromTree);
                    cmd.RedoDone += new UndoRedoEventHandler(AddTable2Tree);
                    cmd.Execute();
                    
                    AddCommand(cmd);
                    AddTable2Tree(appGuid,lt.GUID,lt.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建日志表失败,错误消息为：" + ex.Message);
            }
        }

        private void menuDeleteLog_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (MessageBox.Show(string.Format("确定删除应用程序\"{0}\"中的日志表\"{1}\"", this.tvApps.SelectedNode.Parent.Text, this.tvApps.SelectedNode.Text),
                    "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string appGuid = Convert.ToString(this.tvApps.SelectedNode.Parent.Tag);
                string tableGuid = Convert.ToString(this.tvApps.SelectedNode.Tag);
                DeleteLogStructCommand cmd = new DeleteLogStructCommand(appGuid,AppService.Instance.GetAppTable(appGuid,tableGuid));
                cmd.RedoDone += new UndoRedoEventHandler(RemoveTableFromTree);
                cmd.UndoDone += new UndoRedoEventHandler(AddTable2Tree);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveTableFromTree(appGuid,tableGuid);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除日志表失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void UpdateAppProperry()
        {
            this.Cursor = Cursors.WaitCursor;
            LogApp la = AppService.Instance.GetApp(Convert.ToString(tvApps.SelectedNode.Tag));

            frmEditAppProperty fe = new frmEditAppProperty(la.Name,la.IsImportLogsFromFiles,la.Group.Name);
            fe.Guid = la.AppGUID;

            if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
            {
                string appGuid = Convert.ToString(tvApps.SelectedNode.Tag);
                LogAppMemento memento = new LogAppMemento(fe.IsImportLogsFromFile, fe.EditedName,la.Group.Name);
                UpdateAppPropertiesCommand cmd = new UpdateAppPropertiesCommand(appGuid, memento);
                cmd.UndoDone += new UndoRedoEventHandler(RefreshAppNodeName);
                cmd.RedoDone += new UndoRedoEventHandler(RefreshAppNodeName);

                cmd.Execute();
                
                AddCommand(cmd);
                RefreshAppNodeName(appGuid, fe.EditedName);                
            }            
        }

        private void ReNameTableName()
        {
            this.Cursor = Cursors.WaitCursor;
            frmEditTableProperty fe = new frmEditTableProperty(tvApps.SelectedNode.Text);
            fe.Guid = Convert.ToString(tvApps.SelectedNode.Tag);

            if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
            {
                string appGuid = Convert.ToString(tvApps.SelectedNode.Parent.Tag);
                string tableGuid = Convert.ToString(tvApps.SelectedNode.Tag);

                ReNameTableCommand cmd = new ReNameTableCommand(appGuid, tableGuid,fe.EditedName);
                cmd.UndoDone += new UndoRedoEventHandler(RefreshTableNodeName);
                cmd.RedoDone += new UndoRedoEventHandler(RefreshTableNodeName);

                cmd.Execute();
                
                AddCommand(cmd);
                RefreshTableNodeName(appGuid, tableGuid,fe.EditedName);                
            }            
        }

        private void cmReName_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvApps.SelectedNode == null)
                {
                    return;
                }

                if (tvApps.SelectedNode.Level == AppNodeLevel)
                {
                    UpdateAppProperry();
                }
                else if (tvApps.SelectedNode.Level == LogStructNodeLevel)
                {
                    ReNameTableName();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("重命名操作失败,错误消息为：" + ex.Message);
            }            
        }

        private void ExportAppIdentity(string fileName)
        {
            LogApp la = AppService.Instance.GetApp(Convert.ToString(tvApps.SelectedNode.Tag));

        }

        private void ExportTableIdentity(string fileName)
        {
            string tableGuid = Convert.ToString(tvApps.SelectedNode.Tag);
        }

        private void cmExportIdentity_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvApps.SelectedNode == null)
                {
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "结构身份文件(*.xml)|*.xml";
                
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                
                if (tvApps.SelectedNode.Level == AppNodeLevel)
                {
                    ExportAppIdentity(sfd.FileName);
                }
                else if (tvApps.SelectedNode.Level == LogStructNodeLevel)
                {
                    ExportTableIdentity(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出应用程序结构信息操作失败,错误消息为：" + ex.Message);
            }            
        }

        private void ClearLogTablePanel()
        {
            this.dataGridView1.Rows.Clear();

            SetLogTablePanelButtonStatus(false);            
        }

        private void ClearDetailPanel()
        {
            this.lstDetails.Items.Clear();
        }

        private void SetLogTablePanelButtonStatus(bool enabled)
        {
            this.btnUp.Enabled = enabled;
            this.btnDown.Enabled = enabled;
            this.btnAddNew.Enabled = enabled;
            this.btnDelete.Enabled = enabled;
        }

        private void InitSubnodeDetails(TreeNode tn)
        {
            this.lstDetails.SuspendLayout();

            if (tn.Nodes.Count > 0)
            {
                foreach (TreeNode son in tn.Nodes)
                {
                    ListViewItem lvi = new ListViewItem(son.Text);
                    lvi.Tag = son.Tag;

                    this.lstDetails.Items.Add(lvi);
                }
            }

            this.lstDetails.ResumeLayout();
        }

        private void InitLogTablePanel(LogTable table)
        {
            SetLogTablePanelButtonStatus(true);

            this.dataGridView1.Rows.Clear();

            if (table.Columns.Count > 0)
            {
                this.dataGridView1.SuspendLayout();

                try
                {  
                    foreach (LogTableItem item in table.Columns)
                    {
                        AddRow(item);
                    }

                    UpdatePreView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化日志列项失败，错误消息为:" + ex.Message);
                }
                finally
                {
                    this.dataGridView1.ResumeLayout();
                }
            }
        }

        private void tvApps_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void menuUndo_Click(object sender, EventArgs e)
        {
            m_undoRedoBuffer.Undo();
            UpdateUndoRedoStatus();
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            m_undoRedoBuffer.Redo();
            UpdateUndoRedoStatus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                List<LogTableItem> lstSelectedItems = new List<LogTableItem>();

                foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells[SelectIndex].Value))
                    {
                        lstSelectedItems.Add(LogTableItem.CreateNewLogTableItem(
                            Convert.ToInt32(dgvr.Cells[LogColumnIndexIndex].Value),
                            Convert.ToBoolean(dgvr.Cells[LogItemIsFilterIndex].Value)));
                    }
                }

                if(lstSelectedItems.Count<=0)
                {
                    MessageBox.Show("请先勾选要删除的行");
                }

                string appGuid=Convert.ToString(tvApps.SelectedNode.Parent.Tag);
                string tableGuid=Convert.ToString(tvApps.SelectedNode.Tag);

                RemoveTableColumnsCommand cmd=new RemoveTableColumnsCommand(appGuid,tableGuid,lstSelectedItems);
                cmd.UndoDone += new UndoRedoEventHandler(AddTableRows);
                cmd.RedoDone += new UndoRedoEventHandler(RemoveTableRows);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveTableRows(lstSelectedItems);
                UpdatePreView();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除日志列操作失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UpdateGridViewContent(UndoRedoEventArg arg)
        {
            UpdateGridViewContent((LogTableItem)arg.Tag);
        }

        private void UpdateGridViewContent(LogTableItem item)
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                if (Convert.ToInt32(dgvr.Cells[LogColumnIndexIndex].Value) == item.LogColumnIndex)
                {
                    m_isUseCommand = false;
                    dgvr.Cells[LogItemIsFilterIndex].Value = item.IsFilterColumn;
                    dgvr.Cells[LogColumnNickNameIndex].Value = item.NickName;
                    dgvr.Cells[LogColumnVisibleIndex].Value = item.Visible;
                    break;
                }
            }

            UpdatePreView();
        }

        private bool m_isUseCommand = true;//  加这个变量主要是UpdateLogTableItemValueCommand
                                         //  undoredo的时候都会给datagridview赋值，这时会引起
                                         //  CellValueChanged事件，又会加一个命令

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <0 ||e.ColumnIndex == SelectIndex)
            {
                return;
            }

            if (!m_isUseCommand)
            {
                m_isUseCommand = true;
                return;
            }

            LogTableItem newItem = GetLogTableItemRow(e.RowIndex);

            string appGuid=Convert.ToString(tvApps.SelectedNode.Parent.Tag);
            string tableGuid=Convert.ToString(tvApps.SelectedNode.Tag);

            UpdateLogTableItemValueCommand cmd = new UpdateLogTableItemValueCommand(appGuid, tableGuid, newItem);
            cmd.UndoDone += new UndoRedoEventHandler(UpdateGridViewContent);
            cmd.RedoDone += new UndoRedoEventHandler(UpdateGridViewContent);

            cmd.Execute();

            UpdatePreView();
            
            AddCommand(cmd);            
        }        

        private void UndoRedoExchangeDateGridRow(UndoRedoEventArg arg)
        {
            List<int> lstSeq =(List<int>) arg.Tag;

            ExchangeDateGridRow(lstSeq[0], lstSeq[1]);
        }

        private void ExchangeDateGridRow(int firstRowIndex, int secondRowIndex)
        {

            string appGuid=Convert.ToString(tvApps.SelectedNode.Parent.Tag);
            string tableGuid=Convert.ToString(tvApps.SelectedNode.Tag);
            LogTable table=AppService.Instance.GetAppTable(appGuid,tableGuid);

            SetRowContent(table, firstRowIndex, false);
            SetRowContent(table, secondRowIndex, true);

            UpdatePreView();
        }

        private void MoveDataGridViewRow(bool isMoveUp)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                int rowIndex=-1;

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[SelectIndex].Value))
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex < 0)
                {
                    MessageBox.Show("请先勾选要移动的行，一次只能移动一行");
                    return;
                }

                if (isMoveUp && rowIndex == 0 || (!isMoveUp && rowIndex == this.dataGridView1.Rows.Count - 1))
                {
                    return;
                }

                int exchangeIndex = isMoveUp ? (rowIndex - 1) : (rowIndex + 1);

                string appGuid = Convert.ToString(tvApps.SelectedNode.Parent.Tag);
                string tableGuid = Convert.ToString(tvApps.SelectedNode.Tag);

                ExchangeTwoTableColumnPositionCommand cmd = new ExchangeTwoTableColumnPositionCommand(appGuid, tableGuid, rowIndex, exchangeIndex);
                cmd.UndoDone += new UndoRedoEventHandler(UndoRedoExchangeDateGridRow);
                cmd.RedoDone += new UndoRedoEventHandler(UndoRedoExchangeDateGridRow);

                cmd.Execute();
                
                AddCommand(cmd);
                ExchangeDateGridRow(rowIndex, exchangeIndex);                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("移动日志列失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveDataGridViewRow(true);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveDataGridViewRow(false);
        }

        private void tvApps_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateAppMenuStatus();
            UpdateLogTableMenuStatus();

            ClearLogTablePanel();
            ClearDetailPanel();

            switch (e.Node.Level)
            {
                case AppGroupNodeLevel:
                case AppNodeLevel:
                    this.tpTableStruct.Parent = null;

                    if (this.tpDetails.Parent == null)
                    {
                        this.tpDetails.Parent = this.tabControl1;
                    }

                    InitSubnodeDetails(e.Node);
                    break;

                case LogStructNodeLevel:
                    string appGuid=Convert.ToString(e.Node.Parent.Tag);
                    string tableGuid=Convert.ToString(e.Node.Tag);

                    this.tpDetails.Parent = null;

                    if (this.tpTableStruct.Parent == null)
                    {
                        this.tpTableStruct.Parent = this.tabControl1;
                    }

                    InitLogTablePanel(AppService.Instance.GetAppTable(appGuid, tableGuid));
                    break;
            }
        }

        private void menuExportTableStructs_Click(object sender, EventArgs e)
        {
            try
            {
                frmExportTableStructs ts = new frmExportTableStructs();                
                CGeneralFuncion.ShowWindow(this,ts,true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出日志表结构失败，错误消息为：" + ex.Message);
            }
        }

        private void UndoRedoImportTables(UndoRedoEventArg arg)
        {
            InitTree();
        }

        private void emnuImportTables_Click(object sender, EventArgs e)
        {
            XmlTextReader reader = null;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                OpenFileDialog pdf = new OpenFileDialog();
                pdf.Filter = ("日志结构文件(*.xml)|*.xml");

                if (pdf.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    reader = new XmlTextReader(pdf.FileName);
                    XmlSerializer serializer=new XmlSerializer(typeof(List<LogApp>));

                    List<LogApp> results=(List<LogApp>)serializer.Deserialize(reader);

                    frmImportTables it=new frmImportTables(results);

                    if(CGeneralFuncion.ShowWindow(this,it,true)== System.Windows.Forms.DialogResult.OK)
                    {
                        ImportAppStructsCommand cmd = new ImportAppStructsCommand(results);
                        cmd.UndoDone += new UndoRedoEventHandler(UndoRedoImportTables);
                        cmd.RedoDone += new UndoRedoEventHandler(UndoRedoImportTables);

                        cmd.Execute();
                        
                        AddCommand(cmd);
                        InitTree();                                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入日志表结构失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void lstDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = lstDetails.GetItemAt(e.X, e.Y);

            if (lvi == null)
            {
                return;
            }

            if (tvApps.SelectedNode == null)
            {
                return;
            }

            if (tvApps.SelectedNode.Nodes.Count < lvi.Index)
            {
                return;
            }

            if (!tvApps.SelectedNode.IsExpanded)
            {
                tvApps.SelectedNode.Expand();
            }

            TreeNode tn = tvApps.SelectedNode;

            tvApps.SelectedNode = tn.Nodes[lvi.Index];
        }        
    }
}
