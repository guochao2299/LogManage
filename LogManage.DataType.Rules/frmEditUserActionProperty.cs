using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage.DataType.Rules
{
    public partial class frmEditUserActionProperty : Form
    {
        public frmEditUserActionProperty()
        {
            InitializeComponent();
        }

        public frmEditUserActionProperty(SecurityAction sa)
        {
            InitializeComponent();

            this.txtName.Text = sa.Name;
            this.txtDesc.Text = sa.Description;
            this.txtGuid.Text = sa.ActionGuid;

            if (!string.IsNullOrWhiteSpace(sa.ResultGuid))
            {
                SecurityActionResult result = SecurityEventService.Instance.GetSecurityActionResult(sa.ResultGuid);

                this.txtResult.BackColor = Color.FromArgb(result.BackgroundColor);
                this.txtResult.Text = result.Description;
                this.txtResult.Tag = result.ResultGuid;
            }
        }

        public string ActionName
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        }

        public string ActionGuid
        {
            set
            {
                txtGuid.Text = value;
            }
        }

        public string ActionDesc
        {
            get
            {
                return txtDesc.Text;
            }
            set
            {
                txtDesc.Text = value;
            }
        }

        public string ActionResultGuid
        {
            get
            {
                if(string.IsNullOrWhiteSpace(txtResult.Text))
                {
                    return string.Empty;
                }

                return Convert.ToString(txtResult.Tag);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private bool SynatxCheck()
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.Show("安全事件名称不能为空!");
                this.txtName.SelectAll();
                this.txtName.Focus();
                return false;
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!SynatxCheck())
            {
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtResult.BackColor = SystemColors.Control;
            this.txtResult.Text = string.Empty;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.Cursor=Cursors.WaitCursor;

            try
            {
                frmEditRuleResult fer = new frmEditRuleResult(SecurityEventService.Instance.DBManager, false);
                if (CGeneralFuncion.ShowWindow(this, fer, true) == System.Windows.Forms.DialogResult.OK)
                {
                    SecurityActionResult sar = fer.SelectedResult[0];

                    this.txtResult.BackColor = Color.FromArgb(sar.BackgroundColor);
                    this.txtResult.Text = sar.Description;
                    this.txtResult.Tag = sar.ResultGuid;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("选择安全行为结果失败，错误消息为："+ex.Message);
            }
            finally
            {
                this.Cursor=Cursors.Default;
            }
            
        }
    }
}
