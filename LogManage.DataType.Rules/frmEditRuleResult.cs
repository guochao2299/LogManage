using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LogManage.DataType.Rules
{
    public partial class frmEditRuleResult : Form
    {
        private List<string> m_newItems = new List<string>();
        private List<string> m_deletedItems = new List<string>();
        private bool m_isEdit = false;
        private Dictionary<string, SecurityActionResult> m_initData = new Dictionary<string, SecurityActionResult>();

        /// <summary>
        /// 规则结果构造函数
        /// </summary>
        /// <param name="manager">数据管理对象</param>
        /// <param name="isEdit">true为编辑模式，false为选择模式</param>
        public frmEditRuleResult(IManageRuleData manager,bool isEdit)
        {
            InitializeComponent();

            if (manager == null)
            {
                throw new NullReferenceException("规则结果数据管理类对象引用不能为空!");
            }

            SecurityEventService.Instance.DBManager=manager;

            m_isEdit = isEdit;
            btnNewResult.Visible = m_isEdit;
            btnDelete.Visible = m_isEdit;

            FillRuleResult();
        }

        private void FillRuleResult()
        {
            foreach (SecurityActionResult result in SecurityEventService.Instance.DefinedActionResults.Values)
            {
                InsertControl(result);

                m_initData.Add(result.ResultGuid, result);
            }
        }

        private void InsertControl(SecurityActionResult result)
        {
            usEditResult ctl = new usEditResult();
            ctl.Width = this.flowLayoutPanel1.Width;
            ctl.SetActionResult(result);
            ctl.CanEdit = m_isEdit;

            this.flowLayoutPanel1.Controls.Add(ctl);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnNewResult_Click(object sender, EventArgs e)
        {
            SecurityActionResult result = SecurityActionResult.CreateNewSecurityActionResult();
            m_newItems.Add(result.ResultGuid);
            InsertControl(result);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.flowLayoutPanel1.Controls.Count - 1; i > 0; i--)
            {
                if(this.flowLayoutPanel1.Controls[i] is usEditResult && 
                    ((usEditResult)this.flowLayoutPanel1.Controls[i]).IsSelected)
                {
                    string ctlGuid=Convert.ToString(this.flowLayoutPanel1.Controls[i].Tag);

                    if (m_newItems.Contains(ctlGuid))
                    {
                        m_newItems.Remove(ctlGuid);
                    }
                    else
                    {
                        m_deletedItems.Add(ctlGuid);
                    }

                    this.flowLayoutPanel1.Controls.RemoveAt(i);
                }
            }
        }

        public List<SecurityActionResult> SelectedResult
        {
            get
            {
                List<SecurityActionResult> result = new List<SecurityActionResult>();

                foreach (Control ctl in this.flowLayoutPanel1.Controls)
                {
                    if (ctl is usEditResult)
                    {
                        usEditResult r = (usEditResult)ctl;

                        if (r.IsSelected)
                        {
                            result.Add(r.EditedResult);
                        }
                    }
                }

                return result;
            }
        }

        private void UpdateDatas()        
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                List<SecurityActionResult> newItems = new List<SecurityActionResult>();
                List<SecurityActionResult> modifyItems = new List<SecurityActionResult>();
               
                foreach (Control ctl in this.flowLayoutPanel1.Controls)
                {
                    if (ctl is usEditResult)
                    {
                        usEditResult r = (usEditResult)ctl;
                        SecurityActionResult tmpData=r.EditedResult;

                        if (m_newItems.Contains(tmpData.ResultGuid))
                        {
                            newItems.Add(tmpData);
                        }
                        else if(!m_initData[tmpData.ResultGuid].Equals(tmpData))
                        {
                            modifyItems.Add(tmpData);
                        }
                    }
                }

                if (newItems.Count > 0)
                {
                    SecurityEventService.Instance.InsertResults(newItems);
                }

                if (modifyItems.Count > 0)
                {
                    SecurityEventService.Instance.UpdateResults(modifyItems);
                }

                if (m_deletedItems.Count > 0)
                {
                    SecurityEventService.Instance.DeleteResults(m_deletedItems);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("更新数据失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_isEdit)
            {
                UpdateDatas();
            }
            else
            {
                if (SelectedResult.Count < 0)
                {
                    MessageBox.Show("请至少选择一行数据");
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
