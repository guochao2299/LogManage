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
    public partial class frmEditAppGroup : Form
    {
        private List<string> m_newGroups = null;
        private List<string> m_deleteGroups = null;

        public frmEditAppGroup()
        {
            InitializeComponent();

            m_deleteGroups = new List<string>();
            m_newGroups = new List<string>();
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr.Cells.Add(new DataGridViewCheckBoxCell(false));
            dgvr.Cells.Add(new DataGridViewTextBoxCell());
            string defaultCellValue="默认分组"+DateTime.Now.ToString("yyyyMMddhhmmss");
            dgvr.Cells[GroupNameIndex].Value = defaultCellValue;
            dgvr.Tag = defaultCellValue;
            this.dataGridView1.Rows.Add(dgvr);
            m_newGroups.Add(defaultCellValue);
        }

        private const int SelectedIndex = 0;
        private const int GroupNameIndex = 1;
        
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                for(int i=this.dataGridView1.Rows.Count-1;i>=0;i--)
                {
                    DataGridViewRow dgvr = this.dataGridView1.Rows[i];

                    if (Convert.ToBoolean(dgvr.Cells[SelectedIndex].Value))
                    {
                        string groupName=Convert.ToString(dgvr.Cells[GroupNameIndex].Value);

                        if(AppService.Instance.IsAppGroupHasApps(groupName))
                        {
                            sb.AppendLine(groupName);
                        }
                        else
                        {
                            m_deleteGroups.Add(groupName);
                            this.dataGridView1.Rows.RemoveAt(i);
                        }
                    }
                }

                if (sb.Length > 0)
                {
                    MessageBox.Show("下列分组没有被删除，因为分组下还有定义的应用程序，请先删除应用程序后再删除应用程序分组：\r\n"+sb.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除应用程序分组失败，错误消息为：" + ex.Message);
            }
        }

        private void SaveChanges()
        {
            List<LogAppGroup> lstOldGroups = new List<LogAppGroup>();
            List<LogAppGroup> lstNewGroups = new List<LogAppGroup>();

            List<LogAppGroup> lstInsertGroups = new List<LogAppGroup>();
            List<LogAppGroup> lstDeleteGroups = new List<LogAppGroup>();

            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                string newName = Convert.ToString(dgvr.Cells[GroupNameIndex].Value);
                string oldName = Convert.ToString(dgvr.Tag);

                if (string.Equals(newName, oldName))
                {
                    continue;
                }

                if (m_newGroups.Contains(oldName))
                {
                    lstInsertGroups.Add(new LogAppGroup(newName));
                    continue;
                }

                lstOldGroups.Add(new LogAppGroup(oldName));
                lstNewGroups.Add(new LogAppGroup(newName));
            }

            if (lstInsertGroups.Count > 0)
            {

                AppService.Instance.AddGroupName(lstInsertGroups);
            }

            if (lstOldGroups.Count > 0)
            {
                AppService.Instance.ReNameGroupNames(lstOldGroups, lstNewGroups);
            }

            if (m_deleteGroups.Count > 0)
            {
                foreach (string s in m_deleteGroups)
                {
                    lstDeleteGroups.Add(new LogAppGroup(s));
                }

                AppService.Instance.RemoveAppGroupNames(lstDeleteGroups);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChanges();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存应用程序分组失败，错误消息为：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmEditAppGroup_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            dataGridView1.SuspendLayout();

            try
            {
                Dictionary<string, LogAppGroup> lstGroups = AppService.Instance.ExistingAppGroups;

                dataGridView1.Rows.Clear();

                foreach (string s in lstGroups.Keys)
                {
                    dataGridView1.Rows.Add(new object[] { false, s });
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = s;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly = false;
                }

                dataGridView1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化应用程序分组失败，错误消息为：" + ex.Message);
            }
            finally
            {
                dataGridView1.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == GroupNameIndex)
            {
                if(string.IsNullOrEmpty(Convert.ToString(e.FormattedValue)))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText="应用程序分组名称不能为空";
                    e.Cancel=true;
                    return;
                }

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {
                        continue;
                    }

                    if(string.Equals(Convert.ToString(e.FormattedValue),Convert.ToString(this.dataGridView1.Rows[i].Cells[GroupNameIndex].Value), StringComparison.OrdinalIgnoreCase))
                    {
                        dataGridView1.Rows[e.RowIndex].ErrorText=string.Format("应用程序分组名称\"{0}\"已经存在",Convert.ToString(e.FormattedValue));
                        e.Cancel=true;
                        return;
                    }
                }                
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
        }
    }
}
