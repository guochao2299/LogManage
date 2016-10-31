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

namespace LogManage.SelfDefineControl
{
    public partial class usShowLog : UserControl
    {
        public usShowLog()
        {
            InitializeComponent();

            m_lstColsSeq=new List<int>();
        }

        // 保存日志从左到右的排列顺序
        private List<int> m_lstColsSeq=null;

        private DataGridViewColumn CreateColumn(string name)
        {
            DataGridViewColumn dgvc = new DataGridViewColumn();           

            dgvc.HeaderText = name;
            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;          

            DataGridViewCell cell = new DataGridViewTextBoxCell();
            cell.Style.BackColor = Color.Cornsilk;
            cell.Style.Alignment= DataGridViewContentAlignment.MiddleLeft;
            cell.Style.NullValue = string.Empty;

            dgvc.CellTemplate = cell;

            return dgvc;
        }

        public void ResetCloumns(LogTable lt)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            m_lstColsSeq.Clear();

            GC.Collect();

            // 加一个序号列
            DataGridViewColumn dgvc = CreateColumn("序号");                     

            this.dataGridView1.Columns.Add(dgvc);

            foreach (LogTableItem lti in lt.Columns)
            {
                LogColumn lc=LogColumnService.Instance.GetLogColumn(lti.LogColumnIndex);

                dgvc = CreateColumn(string.IsNullOrWhiteSpace(lti.NickName) ? lc.Name : lti.NickName);                
                dgvc.Visible = lti.Visible;
                dgvc.Tag = lc.Index;

                m_lstColsSeq.Add(lc.Index);

                this.dataGridView1.Columns.Add(dgvc);
            }
        }

        public void SetLog(List<LogRecord> lstRecords)
        {
            this.SuspendLayout();
            this.Cursor=Cursors.WaitCursor;

            try
            {
                this.dataGridView1.Rows.Clear();

                int index = 1;

                //// 同时计算每列的最大宽度
                //int[] lstWidth = new int[this.listView1.Columns.Count];
                //for (int i = 0; i < this.listView1.Columns.Count; i++)
                //{
                //    lstWidth[i] = this.listView1.Columns[i].Width;
                //}

                List<object> rowContents = new List<object>();

                foreach(LogRecord lr in lstRecords)
                {
                    rowContents.Clear();

                    rowContents.Add(index.ToString());                    

                    for(int i=0;i<m_lstColsSeq.Count;i++)
                    {
                        string content = lr.GetItemValue(m_lstColsSeq[i]);
                        rowContents.Add(content);
                    }

                    this.dataGridView1.Rows.Add(rowContents.ToArray());

                    index++;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入日志时出错，错误消息为："+ex.Message);
            }
            finally
            {
                this.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 当前显示的日志条数
        /// </summary>
        public int LogsCount
        {
            get
            {
                return this.dataGridView1.Rows.Count;
            }
        }
    }
}
