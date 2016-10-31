using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.Services;
using LogManage.SelfDefineControl;

namespace LogManage.AidedForms
{
    public partial class frmLogViewer : Form
    {
        public frmLogViewer(LogApp app, Dictionary<string,List<LogRecord>> lstRecord)
        {
            InitializeComponent();

            this.Text = "浏览从" + app.Name + "的日志文件中读取的日志";

            foreach (string s in lstRecord.Keys)
            {
                LogTable lt = app.GetTable(s);

                LogShowTabPage page = new LogShowTabPage(lt.Name);
                page.ResetCloumns(lt);
                page.SetLogs(lstRecord[s]);

                page.Parent = this.myTabControl1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定这些日志不保存到日志管理系统？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.button1.Focus();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
