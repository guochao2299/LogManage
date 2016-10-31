using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.AidedForms;
using LogManage.SelfDefineControl;

// 日志窗口展示服务
// 最初设想的的是每个APP都有一个独立的窗口，进行日志导入导出及分析
// 后来通过讨论，说也可以做出多个APP在同一个界面情况，类似于Chrome浏览器
namespace LogManage.Services
{
    /// <summary>
    /// 定义显示应用程序
    /// </summary>
    internal interface IShowAppFormService
    {
        void ShowAppsLog();
    }

    internal class NullShowAppFormService : IShowAppFormService
    {
        public void ShowAppsLog()
        { }
    }

    internal class SingleWinShowAppFormService : IShowAppFormService
    {
        private ToolStripMenuItem m_menuParent = null;
        private Form m_formParent = null;

        public SingleWinShowAppFormService(Form parent, ToolStripMenuItem item)
        {
            m_menuParent = item;
            m_formParent = parent;
        }

        #region IShowAppFormService Members

        public void ShowAppsLog()
        {
            if (m_formParent != null)
            {
                m_formParent.Cursor = Cursors.WaitCursor;
            }

            try
            {
                m_menuParent.DropDownItems.Clear();

                foreach (LogAppGroup lag in AppService.Instance.ExistingAppGroups.Values)
                {
                    ToolStripMenuItem menuParent = new ToolStripMenuItem(lag.Name);
                    menuParent.Tag = lag.Name;

                    foreach (LogApp la in AppService.Instance.ExistingApps.Values)
                    {
                        if (!string.Equals(la.Group.Name, lag.Name))
                        {
                            continue;
                        }

                        ToolStripMenuItem tsmi = new ToolStripMenuItem(la.Name);
                        tsmi.Tag = la.AppGUID;
                        tsmi.Click += new EventHandler(ShowAuditWindow);

                        menuParent.DropDownItems.Add(tsmi);
                    }

                    m_menuParent.DropDownItems.Add(menuParent);
                }

                if (AppService.Instance.ExistingApps.Count <= 0)
                {
                    MessageBox.Show("暂时没有日志可以查看");
                    return;
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception("创建日志菜单失败，错误消息为：" + ex.Message, ex);
            }
            finally
            {
                if (m_formParent != null)
                {
                    if (m_formParent.Cursor != Cursors.Default)
                    {
                        m_formParent.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void ShowAuditWindow(object sender, EventArgs e)
        {
            if (m_formParent != null)
            {
                m_formParent.Cursor = Cursors.WaitCursor;
            }

            try
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                frmAudit fs = new frmAudit(Convert.ToString(tsmi.Tag));
                fs.WindowState = FormWindowState.Maximized;

                CGeneralFuncion.ShowWindow(m_formParent, fs, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (m_formParent != null)
                {
                    if (m_formParent.Cursor != Cursors.Default)
                    {
                        m_formParent.Cursor = Cursors.Default;
                    }
                }                
            }

        }

        #endregion
    }

    internal class CentralizedTPShowAppFormService : IShowAppFormService
    {
        private TabControl m_tabParent = null;
        private Form m_formParent = null;

        public CentralizedTPShowAppFormService(Form parent, TabControl ctl)
        {
            m_tabParent = ctl;
            m_formParent = parent;
        }

        #region IShowAppFormService Members        

        public void ShowAppsLog()
        {
            if (m_formParent != null)
            {
                m_formParent.Cursor = Cursors.WaitCursor;
            }

            if (m_tabParent.Visible)
            {
                return;
            }

            try
            {
                m_tabParent.TabPages.Clear();
                GC.Collect();


                m_tabParent.Visible = true;


                if (AppService.Instance.ExistingApps.Count <= 0)
                {
                    MessageBox.Show("暂时没有日志可以查看");
                    return;
                }

                foreach (LogApp la in AppService.Instance.ExistingApps.Values)
                {
                    m_tabParent.TabPages.Add(new AuditTabPage(la.AppGUID));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("创建日志页失败，错误消息为：" + ex.Message, ex);
            }
            finally
            {
                if (m_formParent != null)
                {
                    if (m_formParent.Cursor != Cursors.Default)
                    {
                        m_formParent.Cursor = Cursors.Default;
                    }
                }
            }
        }

        #endregion
    }
}
