using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.IO;

using LogManage.AidedForms;
using LogManage.DataType;
using LogManage.Services;
using LogManage.DataType.Rules;
using LogManage.DataType.Rules.Evaluation;
using LogManage.LogAnalysis;
using LogManage.DataType.Relations;

namespace LogManage
{
    public partial class frmMain : Form
    {
        private IShowAppFormService m_formShowSrv = new NullShowAppFormService();
        public frmMain()
        {
            InitializeComponent();

            frmSplash.ShowSplashScreen(); 

#if SET_TEST_DATA
            InitTestData();
#endif

#if DEBUG
            this.menuSecurityEvent.Visible=true;
            this.menuTest.Visible = true;
#else
            this.menuSecurityEvent.Visible=false;
            this.menuTest.Visible=false;
#endif
        }

        public void InitTestData()
        {
            List<LogColumn> lstColumns = new List<LogColumn>();
            lstColumns.Add(LogColumnService.Instance.CreateNewLogColumn("人员", "字符串"));
            lstColumns.Add(LogColumnService.Instance.CreateNewLogColumn("部门", "字符串"));
            lstColumns.Add(LogColumnService.Instance.CreateNewLogColumn("审批人", "字符串"));
            lstColumns.Add(LogColumnService.Instance.CreateNewLogColumn("提交日期", "日期"));

            LogColumnService.Instance.AddLogColumns(lstColumns);
        }        

        private void menuDefineLogItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmEditItem fe = new frmEditItem(false,null);
                fe.WindowState = FormWindowState.Maximized;

                CGeneralFuncion.ShowWindow(this,fe, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }             
        }

        private void menuDefineAppStruct_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmEditAppStruct fe = new frmEditAppStruct();
                fe.WindowState = FormWindowState.Maximized;
                
                CGeneralFuncion.ShowWindow(this, fe, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }

        }

        private void menuAppLogs_DropDownOpening(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {                
                this.menuAppLogs.DropDownItems.Clear();

                m_formShowSrv.ShowAppsLog();
                //this.menuAppLogs.DropDownItems.Clear();

                //if (AppService.Instance.ExistingApps.Count <= 0)
                //{
                //    MessageBox.Show("暂时没有日志可以查看");
                //    return;
                //}

                //foreach (LogApp la in AppService.Instance.ExistingApps.Values)
                //{
                //    ToolStripMenuItem tsmi = new ToolStripMenuItem(la.Name);
                //    tsmi.Tag = la.AppGUID;
                //    tsmi.Click += new EventHandler(ShowAuditWindow);

                //    this.menuAppLogs.DropDownItems.Add(tsmi);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建日志菜单失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitServiceInterface()
        {
            SecurityEventService.Instance.DBManager = DBService.Instance;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Cursor=Cursors.WaitCursor;

            try
            {
                if (AppService.Instance.ExistingApps.Count > 0)
                {
                    Debug.WriteLine("已经加载了定义的应用系统");
                }

                this.tabControl1.Visible = false;

                this.WindowState = FormWindowState.Maximized;

                m_formShowSrv = new SingleWinShowAppFormService(this,this.menuAppLogs);
                this.menuSingleWin.Checked = true;

                InitServiceInterface();
            }
            catch(Exception ex)
            {
                MessageBox.Show("主窗口加载过程失败，错误消息为："+ex.Message);
            }
            finally
            {
                if (frmSplash.SplashScreen != null)
                {
                    frmSplash.SplashScreen.Dispose();
                }

                this.Cursor=Cursors.Default;
            }
            
        }


        private void menuSingleWin_Click(object sender, EventArgs e)
        {
            if(menuSingleWin.Checked)
            {
                return;
            }

            m_formShowSrv = new SingleWinShowAppFormService(this,this.menuAppLogs);
            this.menuSingleWin.Checked = true;
            this.menuOnlyWin.Checked = false;

            this.menuAppLogs.Visible = true;

            this.tabControl1.Visible = false;
            this.tabControl1.TabPages.Clear();

            GC.Collect();
        }

        private void menuOnlyWin_Click(object sender, EventArgs e)
        {
            if (menuOnlyWin.Checked)
            {
                return;
            }

            m_formShowSrv = new CentralizedTPShowAppFormService(this,this.tabControl1);
           
            this.menuOnlyWin.Checked = true;
            this.menuSingleWin.Checked = false;

            this.menuAppLogs.Visible = false;
            menuAppLogs_DropDownOpening(null, null);
        }

        private void menuAppLogs_Click(object sender, EventArgs e)
        {

        }

        private void menuEditActionResults_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmEditRuleResult fe = new frmEditRuleResult(DBService.Instance, true);

                CGeneralFuncion.ShowWindow(this, fe, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }            
        }

        private void 维护安全事件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmEditRules fe = new frmEditRules(DBService.Instance,new List<LogColumn>(LogColumnService.Instance.AvaliableColumns.Values));
                CGeneralFuncion.ShowWindow(this, fe, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void 显示动画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            frmAnalysis fa = new frmAnalysis();
            CGeneralFuncion.ShowWindow(this, fa, false);
#endif
        }

        // 定义程序分组
        private void menuDefineAppGroup_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                frmEditAppGroup fe = new frmEditAppGroup();
                CGeneralFuncion.ShowWindow(this, fe, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
