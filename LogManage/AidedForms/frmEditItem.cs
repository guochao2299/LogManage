using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using LogManage.DataType;
using LogManage.Services;
using LogManage.DataType.Relations;

namespace LogManage.AidedForms
{
    public partial class frmEditItem : Form,ISearchColumn
    {
        public frmEditItem(bool isBrowse,List<int> lstBanedIndex)
        {
            InitializeComponent();
            InitComboBox();
            InitDataGridView(lstBanedIndex??new List<int>());
                        
            if (isBrowse)
            {
                this.Text = "选择日志列";
                this.button1.Visible = false;
                this.btnDeleteSelectedColumns.Visible = false;
            }
            else
            {
                this.Text = "编辑日志列";
            }
        }

        private List<int> m_deleteItems = new List<int>();
        private Dictionary<int, LogColumn> m_newItems = new Dictionary<int, LogColumn>();
        public const int LogColumnType = 4;// 列类型的列号
        public const int LogColumnNameIndex = 3;//列名的列号
        public const int LogColumnIndex = 2;// 列标号的列号
        public const int RowSelectIndex = 0;

        private void InitComboBox()
        {
            DataGridViewComboBoxColumn dgvc=(DataGridViewComboBoxColumn)this.dataGridView1.Columns[LogColumnType];

            foreach (string s in TableColumnTypeService.Instance.AvaliableTypes.Keys)
            {
                dgvc.Items.Add(s);
            }
        }

        private void InitDataGridView(List<int> lstBanedIndex)
        {
            Dictionary<int, LogColumn> lstColumns = LogColumnService.Instance.AvaliableColumns;

            int rowIndex = 1;

            foreach (int index in lstColumns.Keys)
            {
                if (lstBanedIndex.Contains(index))
                {
                    continue;
                }

                this.dataGridView1.Rows.Add(new object[] { false, rowIndex, lstColumns[index].Index, lstColumns[index].Name, lstColumns[index].Type });
                DataGridViewRow dgvr = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
                dgvr.ReadOnly = true;
                dgvr.DefaultCellStyle.BackColor =Color.Silver;
                dgvr.Cells[RowSelectIndex].ReadOnly = false;
                dgvr.Tag = index;

                rowIndex++;
             }
        }

        /// <summary>
        /// 返回选中的日志列的列号
        /// </summary>
        public List<int> SelectLogColumnIndex
        {
            get
            {
                List<int> lstDatas = new List<int>();

                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells[RowSelectIndex].Value))
                    {
                        lstDatas.Add(Convert.ToInt32(dgvr.Tag));
                    }
                }

                return lstDatas;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void UpdateLogColumnItems()
        {
            LogColumnService.Instance.RemoveLogColumns(m_deleteItems);

            LogColumnService.Instance.AddLogColumns(new List<LogColumn>(m_newItems.Values));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                UpdateLogColumnItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新日志列失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }            

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 新建的列允许更改列编号，但是需要检测编号是否重复

            LogColumn lc = LogColumnService.Instance.CreateNewLogColumn(ConstColumnValue.DefaultLogColumnName, ConstColumnValue.DefaultLogColumnType);
            this.dataGridView1.Rows.Add(new object[] { false, this.dataGridView1.Rows.Count+1, lc.Index, lc.Name, lc.Type });
            DataGridViewRow dgvr = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
            dgvr.Tag = lc.Index;
            dgvr.Cells[LogColumnIndex].ReadOnly = false;
            dgvr.Cells[LogColumnIndex].Style.BackColor = Color.White;

            m_newItems.Add(lc.Index,lc);
        }

        private void btnDeleteSelectedColumns_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = this.dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[RowSelectIndex].Value))
                    {
                        int logIndex = Convert.ToInt32(this.dataGridView1.Rows[i].Tag);

                        if (m_newItems.ContainsKey(logIndex))
                        {
                            m_newItems.Remove(logIndex);
                        }
                        else
                        {
                            m_deleteItems.Add(logIndex);
                        }

                        this.dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除日志列失败，错误消息为：" + ex.Message);
            }
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0)
            {
                return;
            }

            DataGridViewCell dgvc = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            

            switch (e.ColumnIndex)
            {
                case LogColumnNameIndex:
                    #region 列名称
                    if (string.IsNullOrEmpty(Convert.ToString(dgvc.Value)))
                    {
                        MessageBox.Show("列名不能为空");
                        dgvc.Value = ConstColumnValue.DefaultLogColumnName;
                        dgvc.Selected = true;
                        break;
                    }                

                    m_newItems[Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[LogColumnIndex].Value)].Name = Convert.ToString(dgvc.Value);
                    #endregion
                    break;

                case LogColumnType:
                    #region 列类型
                    m_newItems[Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[LogColumnIndex].Value)].Type = Convert.ToString(dgvc.Value);
                    #endregion
                    break;

                case LogColumnIndex:
                    #region 列编号
                    int newColIndex = 0;
                    int oldColIndex=Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Tag);

                    try
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dgvc.Value)))
                        {
                            throw new Exception("列编号不能为空");                            
                        }

                        if (!int.TryParse(Convert.ToString(dgvc.Value), out newColIndex))
                        {
                            throw new Exception("列编号必须是数字");                            
                        }

                        if (LogColumnService.Instance.IsColumnIndexExist(newColIndex) ||
                            (oldColIndex!=newColIndex && m_newItems.ContainsKey(newColIndex)))
                        {
                            throw new Exception("该列编号已经存在，请重新设定");
                        }

                        LogColumn col = m_newItems[oldColIndex];
                        m_newItems.Remove(oldColIndex);
                        col.Index = newColIndex;
                        m_newItems.Add(newColIndex, col);
                        this.dataGridView1.Rows[e.RowIndex].Tag = newColIndex;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("更改列编号失败，错误消息为："+ex.Message);
                        dgvc.Selected = true;
                        dgvc.Value = oldColIndex;
                        break;
                    }                    
    #endregion
                    break;
            }            
        }



        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                dgvr.Cells[RowSelectIndex].Value = true;
            }
        }

        private void btnSelectReverse_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                bool isSelected=Convert.ToBoolean(dgvr.Cells[RowSelectIndex].Value);
                dgvr.Cells[RowSelectIndex].Value = !isSelected;
            }
        }

        private void cmExportSelectedColumns_Click(object sender, EventArgs e)
        {
            XmlTextWriter writer = null;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                List<LogColumn> lstColumns = new List<LogColumn>();

                foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells[RowSelectIndex].Value))
                    {
                        LogColumn lc = LogColumnService.Instance.CreateNewLogColumn(
                            Convert.ToString(dgvr.Cells[LogColumnNameIndex].Value),
                            Convert.ToString(dgvr.Cells[LogColumnType].Value));
                        lc.Index = Convert.ToInt32(dgvr.Cells[LogColumnIndex].Value);

                        lstColumns.Add(lc);
                    }
                }

                if (lstColumns.Count <= 0)
                {
                    throw new Exception("没有选中任一列，请选中要导出的列集合");
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "日志列定义集合.xml";
                sfd.Filter = "XML 文件(*.xml)|*.xml";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    writer = new XmlTextWriter(fileName, Encoding.Default);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<LogColumn>));
                    serializer.Serialize(writer, lstColumns);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("导出日志列集合失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;

                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void cmImportColumns_Click(object sender, EventArgs e)
        {
            XmlTextReader reader = null;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                StringBuilder sb = new StringBuilder();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML 文件(*.xml)|*.xml";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    reader = new XmlTextReader(ofd.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<LogColumn>));
                    List<LogColumn> columns = (List<LogColumn>)serializer.Deserialize(reader);

                    foreach (LogColumn col in columns)
                    {
                        if (LogColumnService.Instance.IsColumnIndexExist(col.Index) ||
                            m_newItems.ContainsKey(col.Index))
                        {
                            sb.AppendLine(string.Format("日志列\"{0}\"的编号\"{1}\"已经存在，无法导入",col.Name,col.Index));
                            continue;
                        }

                        LogColumn lc =(LogColumn)col.Clone();
                        this.dataGridView1.Rows.Add(new object[] { false, this.dataGridView1.Rows.Count+1, lc.Index, lc.Name, lc.Type });
                        DataGridViewRow dgvr = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];
                        dgvr.Tag = lc.Index;

                        m_newItems.Add(lc.Index,lc);
                    }

                    if(sb.Length>0)
                    {
                        MessageBox.Show(sb.ToString(), "警告", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("导入日志列集合失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;

                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {           
        }

        private bool IsAnyRowSelected()
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[RowSelectIndex].Value))
                {
                    return true;
                }
            }

            return false;
        }

        private void DeSelectAllRows()
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                dgvr.Selected = false;
            }
        }

        private void cmColumns_Opening(object sender, CancelEventArgs e)
        {
            this.cmExportSelectedColumns.Enabled = IsAnyRowSelected();
        }

        private void cmColumnSearch_Click(object sender, EventArgs e)
        {
            frmSearchRow sr = new frmSearchRow(this);
            sr.Show(this);
        }

        #region ISearchColumn Members

        public string SearchColumnDown(ref int startRowIndex, int columnIndex, string searchContent)
        {
            DeSelectAllRows();

            int initRow = startRowIndex;

            for (; startRowIndex < this.dataGridView1.Rows.Count; startRowIndex++)
            {
                DataGridViewRow dgvr=this.dataGridView1.Rows[startRowIndex];
                if (string.Equals(Convert.ToString(dgvr.Cells[columnIndex].Value), searchContent))
                {
                    dgvr.Selected = true;
                    dgvr.Cells[RowSelectIndex].Selected = true;
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = startRowIndex;
                    return string.Empty;
                }
            }

            return string.Format("自第{0}行向下没有搜索到指定的日志列", initRow + 1);
        }

        public string SearchColumnUp(ref int startRowIndex, int columnIndex, string searchContent)
        {
            DeSelectAllRows();

            int initRow = startRowIndex;

            for (; startRowIndex >=0; startRowIndex--)
            {
                DataGridViewRow dgvr = this.dataGridView1.Rows[startRowIndex];
                if (string.Equals(Convert.ToString(dgvr.Cells[columnIndex].Value), searchContent))
                {
                    dgvr.Selected = true;
                    dgvr.Cells[RowSelectIndex].Selected = true;
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = startRowIndex;
                    return string.Empty;
                }
            }

            return string.Format("自第{0}行向上没有搜索到指定的日志列", startRowIndex + 1);
        }


        public int RowsCount
        {
            get
            {
                return this.dataGridView1.Rows.Count;
            }
        }
        #endregion
    }
}
