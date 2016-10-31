using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.Services;
using LogManage.AidedForms;
using LogManage.DataType.Relations;
using LogManage.LogAnalysis;


namespace LogManage.SelfDefineControl
{
    public partial class ucAudit : UserControl
    {
        private LogApp m_app = LogApp.NullApplication;
        private Dictionary<string, LogShowTabPage> m_tabPages = null;
        private string m_suggestText = string.Empty;

        public ucAudit()
        {
            InitializeComponent();

            m_suggestText = "日志审计";
        }

        /// <summary>
        /// 根据当前应用程序得到的建议标题
        /// </summary>
        public string SuggestText
        {
            get
            {
                return m_suggestText;
            }
        }

        public void ResetApp(string appGuid)
        {
            this.DoubleBuffered = true;

            m_app = AppService.Instance.GetApp(appGuid);

            m_suggestText = m_app.Name + "的日志审计";

            this.btnImportNewLogs.Enabled = m_app.IsImportLogsFromFiles;

            InitLogSource();

            //FillComboColumn();

            ReCalculateCommonFilter();
        }

        private void InitLogSource()
        {
            this.m_tabPages = new Dictionary<string, LogShowTabPage>();
            this.lstLogSource.Items.Clear();
            m_tabPages = new Dictionary<string, LogShowTabPage>();

            foreach (LogTable lt in m_app.Tables)
            {
                LogShowTabPage page = new LogShowTabPage(lt.Name);
                page.Tag = lt.GUID;
                m_tabPages.Add(lt.GUID, page);
                page.Parent = this.myTabControl1;
                page.ResetCloumns(lt);
                page.AppGuid = m_app.AppGUID;
                page.TableGuid = lt.GUID;

                ListViewItem lvi = new ListViewItem(lt.Name);
                lvi.Tag = lt.GUID;
                lvi.Checked = true;

                this.lstLogSource.Items.Add(lvi);
            }
        }

        //private void FillComboColumn()
        //{
        //    DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)this.dgvConditions.Columns[2];
        //    column.Items.Add(CConstValue.StringDecimalCondition);
        //    column.Items.Add(CConstValue.DateCondition);
        //}

        private void lstLogSource_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.myTabControl1.SuspendLayout();

            try
            {
                LogShowTabPage page = m_tabPages[Convert.ToString(e.Item.Tag)];
                if (page.Parent == null && e.Item.Checked)
                {
                    page.Parent = this.myTabControl1;
                }
                else
                {
                    page.Parent = null;
                }

                ReCalculateCommonFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置日志来源失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.myTabControl1.ResumeLayout();
            }

        }

        private const int SelectColumnIndex = 0;
        private const int ColumnNameColumnIndex = 1;
        private const int RelationColumnIndex = 2;
        private const int ContentColumnIndex = 3;
        private const int StartDateColumnIndex = 4;
        private const int EndDateColumnIndex = 5;


        private void SetControlEnabled(bool isEnable)
        {
            this.lstLogSource.Enabled = isEnable;
            this.dgvConditions.Enabled = isEnable;
            this.btnAudit.Enabled = isEnable;
            this.btnExportExcel.Enabled = isEnable;
            this.btnImportNewLogs.Enabled = isEnable;
            this.btnAnalysis.Enabled = isEnable;
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                SetControlEnabled(false);

                List<LogFilterCondition> lstConditions = new List<LogFilterCondition>();
                LogFilterCondition condition = null;

                // 检查过滤条件是否完整
                #region 检查过滤条件是否完整

                List<OperateParam> lstParams=new List<OperateParam>();
                int rowIndex=0;
                foreach (DataGridViewRow dgvr in this.dgvConditions.Rows)
                {
                    rowIndex++;

                    if (Convert.ToBoolean(dgvr.Cells[SelectColumnIndex].Value))
                    {
                        lstParams.Clear();

                        LogColumn lc = LogColumnService.Instance.GetLogColumn(Convert.ToInt32(dgvr.Tag));

                        IColumnType colType = TableColumnTypeService.Instance.GetColumnType(lc.Type);
                        
                        IRelation relation = RelationService.Instance.GetRelation(lc.Type,
                            Convert.ToString(dgvr.Cells[RelationColumnIndex].Value));

                        // 目前仅支持与一个值比较或者两个值比较
                        // 这里还可以改的更通用一些，把if去掉
                        if (relation.ParamsCount == 1)
                        {
                            string value=Convert.ToString(dgvr.Cells[ContentColumnIndex].Value);

                            if (!colType.Validate(value))
                            {
                                dgvr.Cells[ContentColumnIndex].Selected = true;
                                throw new Exception(string.Format("行{0}的值有误，不符合类型“{1}”的格式要求", rowIndex, lc.Type));
                            }
                            
                            lstParams.Add(new OperateParam(value));
                            if (!relation.Validate(lstParams))
                            {
                                dgvr.Cells[ContentColumnIndex].Selected = true;
                                throw new Exception(string.Format("行{0}的值有误，不符合关系“{1}”的格式要求", rowIndex, relation.Group));
                            }

                            condition = new LogFilterCondition(lc.Index, lc.Type);
                            condition.Content = Convert.ToString(dgvr.Cells[ContentColumnIndex].Value);
                            condition.Relation = relation.Name;

                            lstConditions.Add(condition);
                        }
                        else if (relation.ParamsCount == 2)
                        {
                            string leftValue = Convert.ToString(dgvr.Cells[StartDateColumnIndex].Value);
                            if (!colType.Validate(leftValue))
                            {
                                dgvr.Cells[StartDateColumnIndex].Selected = true;
                                throw new Exception(string.Format("行{0}的值有误，不符合类型“{1}”的格式要求", rowIndex, lc.Type));
                            }

                            string rightValue = Convert.ToString(dgvr.Cells[EndDateColumnIndex].Value);

                            if (!colType.Validate(rightValue))
                            {
                                dgvr.Cells[EndDateColumnIndex].Selected = true;
                                throw new Exception(string.Format("行{0}的值有误，不符合类型“{1}”的格式要求", rowIndex, lc.Type));
                            }

                            lstParams.Add(new OperateParam(leftValue));
                            lstParams.Add(new OperateParam(rightValue));

                            if (!relation.Validate(lstParams))
                            {
                                dgvr.Cells[StartDateColumnIndex].Selected = true;
                                throw new Exception(string.Format("行{0}的值有误，不符合关系“{1}”的格式要求", rowIndex, relation.Group));
                            }

                            condition = new LogFilterCondition(lc.Index, lc.Type);
                            condition.LeftBound = leftValue;
                            condition.RightBound = rightValue;
                            condition.Relation = relation.Name;

                            lstConditions.Add(condition);
                        }
                    }
                }
                #endregion

                #region 开始检索日志
                foreach (LogShowTabPage page in m_tabPages.Values)
                {
                    if (page.Parent == null)
                    {
                        continue;
                    }

                    string tableGuid = Convert.ToString(page.TableGuid);

                    List<LogRecord> lstRecords = LogContentService.Instance.GetAppLogs(m_app.AppGUID, tableGuid, lstConditions);
                    page.SetLogs(lstRecords);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("检索{0}的日志时出错，错误消息为：{1}", m_app.Name, ex.Message));
            }
            finally
            {
                this.Cursor = Cursors.Default;
                SetControlEnabled(true);
            }
        }

        /// <summary>
        /// 计算几张表中相同的检索条件
        /// </summary>
        /// <returns></returns>
        private void ReCalculateCommonFilter()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                Dictionary<int, LogColumn> commonFilters = new Dictionary<int, LogColumn>();
                Dictionary<int, int> DctTime = new Dictionary<int, int>();// 用这个来保存命中次数
                int selectedTablesCount = 0;// 如果命中次数和选中的表的数量相同，则说明是共同条件

                foreach (ListViewItem lvi in this.lstLogSource.Items)
                {
                    if (lvi.Checked)
                    {
                        LogTable lt = m_app.GetTable(Convert.ToString(lvi.Tag));
                        selectedTablesCount++;

                        foreach (LogTableItem item in lt.Columns)
                        {
                            if (item.IsFilterColumn && item.Visible)
                            {
                                if (commonFilters.ContainsKey(item.LogColumnIndex))
                                {
                                    DctTime[item.LogColumnIndex]++;
                                }
                                else
                                {
                                    commonFilters.Add(item.LogColumnIndex, LogColumnService.Instance.GetLogColumn(item.LogColumnIndex));
                                    DctTime.Add(item.LogColumnIndex, 1);
                                }
                            }
                        }
                    }
                }

                this.dgvConditions.SuspendLayout();
                this.dgvConditions.Rows.Clear();

                foreach (int key in commonFilters.Keys)
                {
                    if (DctTime[key] == selectedTablesCount)
                    {
                        string[] avaliableRelations = RelationService.Instance.GetAvaliableRelations(commonFilters[key].Type).ToArray();

                        if (avaliableRelations.Length > 0)
                        {
                            IRelation relation = RelationService.Instance.GetRelation(commonFilters[key].Type,
                                avaliableRelations[0]);

                            this.dgvConditions.Rows.Add(new object[]{false,commonFilters[key].Name,
                            string.Empty,relation.ParamsCount==1?relation.DefaultValue:string.Empty,
                            relation.ParamsCount==1?string.Empty:relation.DefaultValue,
                            relation.ParamsCount==1?string.Empty:relation.DefaultValue});

                            DataGridViewRow dgvr = this.dgvConditions.Rows[this.dgvConditions.Rows.Count - 1];
                            dgvr.Tag = key;

                            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvr.Cells[RelationColumnIndex];
                            cell.Items.Clear();
                            cell.Items.AddRange(avaliableRelations);
                            cell.Value = cell.Items[0];
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置条件检索失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.dgvConditions.ResumeLayout();
            }
        }

        private void btnImportNewLogs_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ILogOperator opt = LogContentService.Instance.GetLogOperator(m_app.AppGUID);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = opt.Filter;

                this.Cursor = Cursors.Default;

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    // 导入文件后先显示，然后用户确认后再保存到数据库
                    Dictionary<string, List<LogRecord>> lstLogs = opt.ReadLogFromFiles(m_app.AppGUID, ofd.FileNames);

                    foreach (LogShowTabPage page in m_tabPages.Values)
                    {
                        if (page.Parent == null)
                        {
                            lstLogs.Remove(Convert.ToString(page.Tag));
                        }
                    }

                    this.Cursor = Cursors.Default;

                    if (lstLogs.Count > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        frmLogViewer flv = new frmLogViewer(m_app, lstLogs);
                        if (CGeneralFuncion.ShowWindow(null, flv, true) == System.Windows.Forms.DialogResult.OK)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            foreach (string tableGuid in lstLogs.Keys)
                            {
                                if (!LogContentService.Instance.SaveAppLogs(m_app.AppGUID, tableGuid, lstLogs[tableGuid]))
                                {
                                    throw new Exception("保存日志到数据库失败");
                                }

                                AddLogs2TabPage(tableGuid, lstLogs[tableGuid]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导入{0}的日志时出错，错误消息为:{1}", m_app.Name, ex.Message));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void AddLogs2TabPage(string tableGuid, List<LogRecord> records)
        {
            foreach (LogShowTabPage tp in this.myTabControl1.TabPages)
            {
                if (string.Equals(tableGuid, Convert.ToString(tp.Tag), StringComparison.OrdinalIgnoreCase))
                {
                    tp.SetLogs(records);
                }
            }
        }

        private void dgvConditions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == SelectColumnIndex ||
                e.ColumnIndex == RelationColumnIndex)
            {
                DataGridViewRow dgvr = this.dgvConditions.Rows[e.RowIndex];
                bool isSelect=Convert.ToBoolean(dgvr.Cells[SelectColumnIndex].Value);

                LogColumn lc = LogColumnService.Instance.GetLogColumn(Convert.ToInt32(dgvr.Tag));                        
                IRelation relation = RelationService.Instance.GetRelation(lc.Type,
                            Convert.ToString(dgvr.Cells[RelationColumnIndex].Value));

                dgvr.Cells[ColumnNameColumnIndex].ReadOnly=true;
                dgvr.Cells[ColumnNameColumnIndex].Style.BackColor = SystemColors.Control;

                dgvr.Cells[RelationColumnIndex].ReadOnly=!isSelect;
                dgvr.Cells[RelationColumnIndex].Style.BackColor = isSelect ? Color.White : SystemColors.Control;

                dgvr.Cells[ContentColumnIndex].ReadOnly = !isSelect || relation.ParamsCount == 2;
                dgvr.Cells[ContentColumnIndex].Style.BackColor = (isSelect && relation.ParamsCount==1)?Color.White : SystemColors.Control;

                dgvr.Cells[StartDateColumnIndex].ReadOnly = !isSelect || relation.ParamsCount == 1;
                dgvr.Cells[StartDateColumnIndex].Style.BackColor = (isSelect && relation.ParamsCount == 2) ? Color.White : SystemColors.Control;

                dgvr.Cells[EndDateColumnIndex].ReadOnly = !isSelect || relation.ParamsCount == 1;
                dgvr.Cells[EndDateColumnIndex].Style.BackColor = (isSelect && relation.ParamsCount == 2) ? Color.White : SystemColors.Control;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmAnalysis fa = new frmAnalysis();

                foreach (LogShowTabPage tp in m_tabPages.Values)
                {
                    if (tp.Parent != null && tp.LogRecordCount>0)
                    {
                        fa.AddLog(m_app.AppGUID, tp.TableGuid, tp.Records);
                    }
                }

                fa.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("日志分析失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
