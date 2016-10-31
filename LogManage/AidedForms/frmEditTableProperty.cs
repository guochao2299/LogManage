using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage.AidedForms
{
    public partial class frmEditTableProperty : Form
    {
        public frmEditTableProperty(string name)
        {
            InitializeComponent();

            this.txtName.Text = name;
            this.txtGuid.Text = string.Empty;

            this.txtName.Focus();
        }

        public string EditedName
        {
            get
            {
                return txtName.Text;
            }
        }

        public string Guid
        {
            set
            {
                this.txtGuid.Text = value;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("名称不能为空!");
                txtName.Text = "默认名称";
                txtName.SelectAll();
                txtName.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
