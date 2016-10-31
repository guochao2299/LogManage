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
    public partial class frmSearchRow : Form
    {
        private int m_dgvColumnIndex = frmEditItem.LogColumnNameIndex;
        private ISearchColumn m_searcher = null;
        private int m_startRowIndex = 0;

        public frmSearchRow(ISearchColumn schCol)
        {
            InitializeComponent();

            if (schCol == null)
            {
                throw new Exception("条件检索对象不能为空");
            }

            m_searcher = schCol;
            m_startRowIndex = -1;

            UpdateButtonStatus();
        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbName.Checked)
            {
                m_dgvColumnIndex = frmEditItem.LogColumnNameIndex;
            }
        }

        private void rbColIndex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbColIndex.Checked)
            {
                m_dgvColumnIndex = frmEditItem.LogColumnIndex;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            m_startRowIndex++;
            string result = m_searcher.SearchColumnDown(ref m_startRowIndex, m_dgvColumnIndex, txtSearchContent.Text);
            this.lblResult.Text = result;
            UpdateButtonStatus();
        }

        private void UpdateButtonStatus()
        {
            this.btnNext.Enabled = m_startRowIndex < m_searcher.RowsCount;
            this.btnPre.Enabled = m_startRowIndex > 0;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            m_startRowIndex--;
            string result = m_searcher.SearchColumnUp(ref m_startRowIndex, m_dgvColumnIndex, txtSearchContent.Text);
            this.lblResult.Text = result;
            UpdateButtonStatus();
        }


    }
}
