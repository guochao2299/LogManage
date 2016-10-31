using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.AidedForms
{
    public partial class frmEditAppProperty : Form
    {
        public frmEditAppProperty(string name,bool isImportLogsFromFile,string groupName)
        {
            InitializeComponent();

            this.txtName.Text = name;
            this.cbIsImportLogsFromFile.Checked = isImportLogsFromFile;
            this.txtGuid.Text = string.Empty;

            InitAppGroup(groupName);

            this.txtName.Focus();
        }

        private void InitAppGroup(string groupName)
        {
            foreach (LogAppGroup lag in AppService.Instance.ExistingAppGroups.Values)
            {
                this.comboBox1.Items.Add(lag.Name);
            }

            if (string.IsNullOrEmpty(groupName) || !AppService.Instance.IsAppGroupNameExist(groupName))
            {
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.Text = groupName;
            }
        }

        public string EditedName
        {
            get
            {
                return txtName.Text;
            }
        }

        public LogAppGroup Group
        {
            get
            {
                return new LogAppGroup(comboBox1.Text);
            }
        }

        public string Guid
        {
            set
            {
                this.txtGuid.Text = value;
            }
        }

        public bool IsImportLogsFromFile
        {
            get
            {
                return cbIsImportLogsFromFile.Checked; ;
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

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("所属分组不能为空!");
                comboBox1.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
