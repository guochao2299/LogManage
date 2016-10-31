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
    public partial class frmEditEventProperty : Form
    {
        public frmEditEventProperty()
        {
            InitializeComponent();
        }

        public frmEditEventProperty(SecurityEvent ev)
        {
            InitializeComponent();

            this.txtDesc.Text = ev.Description;
            this.txtGuid.Text = ev.EventGuid;
            this.txtName.Text = ev.Name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public string EventName
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

        public string Description
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
    }
}
