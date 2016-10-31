using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType.Rules.Evaluation;
using LogManage.Services;
using LogManage.DataType.Rules;
using LogManage.DataType;
using LogManage.SelfDefineControl;
using LogManage.LogAnalysis.SPI;

namespace LogManage.LogAnalysis
{
    public partial class frmChartObserver : Form,IObserver
    {
        private enum ShowLevel
        {
            SEvent,
            SAction,
            SResult
        }

        internal class StatisticsResult
        {
            public List<EvaluateResult> Results = new List<EvaluateResult>();
            public float Percentage = 0;
            public int Count = 0;
            public string Title = string.Empty;
            public Color BackColor = Color.White;
        }

        private List<EvaluateResult> m_result = null;
        private Dictionary<string, StatisticsResult> m_analysisResult = null;
        private IStatistics m_statistics = new NullStatistics();
        private IGraph m_drawGraph = new NullGraph();

        public frmChartObserver()
        {
            InitializeComponent();

            m_statistics = new EventStatistics();
            m_drawGraph = new PieChartGraph();
            InitGraph();

            this.tplog.Parent = null;

            this.rbEvent.Checked = true;
            this.DoubleBuffered = true;
        }

        private void CalculatePercentage()
        {
            if (this.Cursor != Cursors.WaitCursor)
            {
                this.Cursor = Cursors.WaitCursor;
            }

            try
            {
                if (m_result == null || m_result.Count <= 0)
                {
                    return;
                }

                m_analysisResult = m_statistics.Statistics(m_result);

                if (m_analysisResult == null)
                {
                    m_analysisResult = new Dictionary<string, StatisticsResult>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算日志分析结果的比例失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        #region IObserver Members

        private void ResetDetailList()
        {
            try
            {
                if(this.Cursor != Cursors.WaitCursor)
                {
                    this.Cursor=Cursors.WaitCursor;
                }

                this.lstDetails.SuspendLayout();
                this.lstDetails.Items.Clear();
                
                foreach (string key in m_analysisResult.Keys)
                {
                    StatisticsResult sr = m_analysisResult[key];
                    ListViewItem lvi=new ListViewItem(sr.Title);
                    lvi.SubItems.Add(sr.Percentage.ToString("P"));
                    lvi.SubItems.Add(sr.Count.ToString() + "条");
                    lvi.BackColor=sr.BackColor;
                    lvi.Tag=key;

                    int index=0;

                    foreach(ListViewItem lv in this.lstDetails.Items)
                    {
                        if (sr.Percentage > m_analysisResult[lv.Tag.ToString()].Percentage)
                        {
                            break;
                        }

                        index++;
                    }

                    this.lstDetails.Items.Insert(index,lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("重新排列日志分析结果失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.lstDetails.ResumeLayout();

                if(this.Cursor != Cursors.Default)
                {
                    this.Cursor=Cursors.Default;
                }
            }
        }

        public void Notify(List<DataType.Rules.Evaluation.EvaluateResult> newAnalysises)
        {
            m_result = newAnalysises;
            RefreshData();
        }

        private void RefreshData()
        {
            CalculatePercentage();
            ResetDetailList();

            m_drawGraph.InitData(m_analysisResult);
            this.drawBoard1.Refresh();
        }

        #endregion

        private void frmPieChartObserver_Load(object sender, EventArgs e)
        {
            try
            {
                AnalysisResultService.Instance.RegisterObserver(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载安全事件统计图窗口失败，错误消息为：" + ex.Message);
            }
        }

        private void frmPieChartObserver_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                AnalysisResultService.Instance.UnregisterObserver(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭安全事件统计图窗口失败，错误消息为：" + ex.Message);
            }
        }

        private void rbEvent_CheckedChanged(object sender, EventArgs e)
        {
            m_statistics = new EventStatistics();
            RefreshData();
        }

        private void rbAction_CheckedChanged(object sender, EventArgs e)
        {
            m_statistics = new ActionStatistics();
            RefreshData();
        }

        private void rbResult_CheckedChanged(object sender, EventArgs e)
        {
            m_statistics = new ResultStatistics();
            RefreshData();
        }

        private bool m_isErrorHappened = false;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (DesignMode || m_isErrorHappened)
                {
                    return;
                }

                e.Graphics.TranslateTransform(this.drawBoard1.AutoScrollPosition.X, this.drawBoard1.AutoScrollPosition.Y);

                m_drawGraph.DrawGraph(e.Graphics);
            }
            catch (Exception ex)
            {
                m_isErrorHappened = true;
                MessageBox.Show("绘制统计图失败，错误消息为：" + ex.Message);
            }

        }

        private void rbPie_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPie.Checked)
            {
                m_drawGraph = new PieChartGraph();
                InitGraph();
            }
        }

        private void rbBar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBar.Checked)
            {
                m_drawGraph = new BarChartGraph();
                InitGraph();
            }
        }

        private void InitGraph()
        {
            tpGraph.Text = m_drawGraph.GraphName;
            m_drawGraph.SetClientSize(this.drawBoard1.ClientSize);
            m_drawGraph.InitData(m_analysisResult);            
            this.drawBoard1.AutoScrollMinSize = m_drawGraph.GetGraphMiniSize();
            this.drawBoard1.Refresh();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                StatisticsResult sr = m_drawGraph.HitTest(e.Location);

                for (int i = lstDetails.SelectedItems.Count - 1; i >= 0; i--)
                {
                    lstDetails.SelectedItems[i].Selected = false;
                }

                if (sr != null)
                {                
                    foreach (ListViewItem lvi in lstDetails.Items)
                    {
                        if (string.Equals(m_drawGraph.SelectedRegionKey, Convert.ToString(lvi.Tag), StringComparison.OrdinalIgnoreCase))
                        {
                            lvi.Selected = true;
                            break;
                        }
                    }
                }

                this.drawBoard1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("点击图标区域失败，错误消息为：" + ex.Message);
            }
        }

        private void ClearData()
        {
            myTabControl1.TabPages.Clear();
            GC.Collect();
            tplog.Parent = null;
            tplog.Text = "日志内容";
        }


        private class LogTmpInfo
        {
            public string AppGuid=string.Empty;
            public string TableGuid=string.Empty;
            public List<string> lstRecordGuid=new List<string>();
        }

        private void ShowLogData(StatisticsResult sr)
        {
            this.Cursor = Cursors.WaitCursor;
                        
            try
            {
                myTabControl1.SuspendLayout();
                myTabControl1.TabPages.Clear();          

                Dictionary<string, LogTmpInfo> dctInfos = new Dictionary<string, LogTmpInfo>();

                foreach (EvaluateResult er in sr.Results)
                {
                    string key = er.AppGuid + "@" + er.TableGuid;

                    if (dctInfos.ContainsKey(key))
                    {
                        dctInfos[key].lstRecordGuid.Add(er.LogGuid);
                    }
                    else
                    {
                        LogTmpInfo info = new LogTmpInfo();
                        info.AppGuid = er.AppGuid;
                        info.TableGuid = er.TableGuid;
                        info.lstRecordGuid.Add(er.LogGuid);

                        dctInfos.Add(key, info);
                    }
                }

                foreach (LogTmpInfo ltInfo in dctInfos.Values)
                {
                    List<LogRecord> lstRecords = LogContentService.Instance.GetAppLogs(ltInfo.AppGuid, ltInfo.TableGuid, ltInfo.lstRecordGuid);

                    if (lstRecords == null || lstRecords.Count <= 0)
                    {
                        continue;
                    }

                    LogTable t = AppService.Instance.GetAppTable(ltInfo.AppGuid, ltInfo.TableGuid);

                    LogShowTabPage page = new LogShowTabPage(t.Name);
                    page.Tag = t.GUID;                   
                    page.ResetCloumns(t);
                    page.AppGuid = ltInfo.AppGuid;
                    page.TableGuid = ltInfo.TableGuid;                    

                    this.myTabControl1.TabPages.Add(page);

                    page.SetLogs(lstRecords);
                }

                if (tplog.Parent == null)
                {
                    tplog.Parent = this.tabControl1;
                }

                tplog.Text = "\"" + sr.Title + "\"" + "的日志内容";
                this.tabControl1.SelectedTab = tplog;

                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("显示日志内容失败，错误消息为：" + ex.Message);
            }
            finally
            {
                myTabControl1.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        private void drawBoard1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                StatisticsResult sr = m_drawGraph.HitTest(e.Location);

                if (sr == null)
                {
                    ClearData();
                }
                else
                {
                    ShowLogData(sr);
                }

                this.drawBoard1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("显示鼠标位置对应的日志内容失败，错误消息为：" + ex.Message);
            }
        }

        private void lstDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem lvi= this.lstDetails.GetItemAt(e.X,e.Y);

                if (lvi != null)
                {
                    ShowLogData(m_analysisResult[Convert.ToString(lvi.Tag)]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("显示日志内容失败，错误消息为：" + ex.Message);
            }
        }

        private void lstDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstDetails.SelectedItems != null && lstDetails.SelectedItems.Count > 0)
            {
                

                this.drawBoard1.Refresh();
            }

            try
            {
                ListViewItem lvi = this.lstDetails.GetItemAt(e.X, e.Y);

                m_drawGraph.SelectedRegionKey =(lvi != null)? Convert.ToString(Convert.ToString(lvi.Tag)):
                    string.Empty; ;
                this.drawBoard1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("定位日志区域失败，错误消息为：" + ex.Message);
            }
        }
    }
}
