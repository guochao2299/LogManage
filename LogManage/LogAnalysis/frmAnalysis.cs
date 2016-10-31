using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LogManage.DataType;
using LogManage.Services;
using LogManage.SelfDefineControl;
using LogManage.DataType.Rules;
using LogManage.DataType.Rules.Evaluation;

// 先做单张表的分析

namespace LogManage.LogAnalysis
{
    public partial class frmAnalysis : Form
    {
        public frmAnalysis()
        {
            InitializeComponent();

            Clean();

            this.DoubleBuffered = true;
        }

        private Thread m_prucedurerThread = null;
        private Thread m_consumerThread = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.ucSearchCartoon1.StartCartoon();           

            try
            {
                frmWaiting.ShowWaitingScreen(this);

                this.btnPause.Enabled = true;
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
                this.btnContinue.Enabled = false;

                LogRecordsPool.Instance.ResetFinishSign();

                // 启动线程
                m_prucedurerThread = new Thread(new ThreadStart(LogProcedurer));
                m_prucedurerThread.Start();

                m_consumerThread = new Thread(new ThreadStart(LogConsumer));
                m_consumerThread.Start();
            }
            catch(ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析日志失败，错误消息为：" + ex.Message);               
            }
        }

        private void UpdateInfo(string appName,string tableName,string index)
        {
            this.lblAppName.Text = "系统："+appName;
            this.lblTableName.Text = "表：" + tableName;
            this.lblStatus.Text = string.Format("正在分析系统的第{0}条记录", index);
        }

        delegate void UpdateInfoDelegate(string appName,string tableName,string index);

        private void LogConsumer()
        {
            try
            {
                int count = 1;
                string previousTableGuid = string.Empty;
                LogApp app = null;
                LogTable table = null;
                List<EvaluateResult> lstResults = new List<EvaluateResult>();

                LogRecordsPool.Instance.ResetBreakSign();

                while ((!LogRecordsPool.Instance.GetFinishSign()) ||
                    (!LogRecordsPool.Instance.IsPoolEmpty()))
                {
                    LogRecord record = LogRecordsPool.Instance.GetOneLogRecord();

                    if (record == null)
                    {                       
                        continue;
                    }

                    if (string.Compare(previousTableGuid, record.TableGuid, true) != 0)
                    {
                        app = AppService.Instance.GetApp(record.AppGuid);
                        table = app.GetTable(record.TableGuid);

                        previousTableGuid = record.TableGuid;
                        count = 1;
                    }                    

                    this.lblStatus.Invoke(new UpdateInfoDelegate(UpdateInfo), new object[]{app.Name,table.Name,count.ToString()});

                    foreach (IRuleProcessor p in RulesService.Instance.ImmediatelyReturnProcessors)
                    {        
                        p.Execute(record,LogColumnService.Instance);
                        lstResults.AddRange(p.Result);
                    }

                    foreach (IRuleProcessor p in RulesService.Instance.FinalReturnProcessors)
                    {
                        p.Execute(record, LogColumnService.Instance);
                    }

                    count++;
                }

                foreach (IRuleProcessor p in RulesService.Instance.FinalReturnProcessors)
                {
                    p.Analysis();
                    lstResults.AddRange(p.Result);
                }

#if DEBUG
                Console.WriteLine("show rule result");
#endif                

                if (!LogRecordsPool.Instance.GetBreakSign())
                {
                    this.BeginInvoke(new ShowResultDelegate(ShowResult),
                        new object[]{lstResults});
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("日志分析失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Invoke(new CleanDelegate(Clean));                
            }
        }

        private delegate void ShowResultDelegate(List<EvaluateResult> results);

        private void ShowResult(List<EvaluateResult> lstAnalysisResults)
        {
            try
            {
                if (Cursor != Cursors.WaitCursor)
                {
                    this.Cursor = Cursors.WaitCursor;
                }

                AnalysisResultService.Instance.ResetAnalysisResults(lstAnalysisResults);

                frmChartObserver observer = new frmChartObserver();
                CGeneralFuncion.ShowWindow(this, observer, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("显示分析结果失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        
        private delegate void CleanDelegate();
        private void Clean()
        {
            try
            {
                if (frmWaiting.SplashScreen != null)
                {
                    frmWaiting.SplashScreen.Dispose();
                }

                this.ucSearchCartoon1.ResetCartoon();

                this.btnStart.Enabled = true;
                this.btnPause.Enabled = false;
                this.btnContinue.Enabled = false;
                this.btnStop.Enabled = false;

                this.lblAppName.Text = string.Empty;
                this.lblTableName.Text = string.Empty;
                UpdateLogInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("清理工作失败，错误消息为：" + ex.Message);
            }
        }

        private void LogProcedurer()
        {
            try
            {
                 // 准备数据
                foreach (LogShowTabPage tp in this.tabControl1.TabPages)
                {
                    foreach (LogRecord record in tp.Records)
                    {
                        LogRecordsPool.Instance.AddLogRecord(record);
                    }
                }

                
            }
            catch (ThreadAbortException)
            {                
                Thread.ResetAbort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("日志数据准备失败，错误消息为：" + ex.Message);
            }
            finally
            {
                LogRecordsPool.Instance.SetFinishSign();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                this.ucSearchCartoon1.PauseCartoon();
                this.btnContinue.Enabled = true;
                this.btnPause.Enabled = false;
                this.btnStop.Enabled = false;
                LogRecordsPool.Suspend();
            }
            catch (Exception ex)
            {
                MessageBox.Show("暂停日志分析失败，错误消息为：" + ex.Message);
            }
        }

        public void AddLog(string appGuid, string tableGuid, List<LogRecord> records)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                LogTable table = AppService.Instance.GetAppTable(appGuid, tableGuid);
                LogShowTabPage tp = new LogShowTabPage(table.Name);
                tp.ResetCloumns(table);
                tp.SetLogs(records);
                tp.AppGuid = appGuid;
                tp.TableGuid = tableGuid;
                this.tabControl1.TabPages.Add(tp);

                UpdateLogInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加日志记录失败，错误消息为:" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UpdateLogInfo()
        {
            int logCount = 0;
            
            foreach(TabPage tp in this.tabControl1.TabPages)
            {
                logCount += ((LogShowTabPage)tp).LogRecordCount;
            }

            this.lblStatus.Text = string.Format("共有{0}张表,总计{1}条日志记录", this.tabControl1.TabPages.Count, logCount);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.ucSearchCartoon1.ResetCartoon();            

            StopThread();

            Clean();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.ucSearchCartoon1.ContinueCartoon();
                this.btnPause.Enabled = true;
                this.btnContinue.Enabled = false;
                this.btnStop.Enabled = true;
                LogRecordsPool.Resume();
            }
            catch (Exception ex)
            {
                MessageBox.Show("重启日志分析失败，错误消息为：" + ex.Message);
            }
        }

        private void frmAnalysis_Load(object sender, EventArgs e)
        {
            try
            {

                List<IRuleProcessor> processors = RulesService.Instance.ImmediatelyReturnProcessors;

                foreach (IRuleProcessor p in processors)
                {
                    p.InitialRules(new List<SecurityEvent>(SecurityEventService.Instance.AvaliableEvents.Values));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化策略失败，错误消息为：" + ex.Message);
            }
        }

        private void StopThread()
        {
            try
            {
                LogRecordsPool.Instance.ClearPool();
                LogRecordsPool.Instance.SetBreakSign();
                LogRecordsPool.Instance.SetFinishSign();


                
                while((m_prucedurerThread!=null && m_prucedurerThread.IsAlive) ||
                    (m_consumerThread != null && m_consumerThread.IsAlive))
               {
                   Application.DoEvents();
                }

                //Thread.Sleep(1000);

                //Thread.

                //if (m_prucedurerThread.IsAlive)
                //{
                //    m_prucedurerThread.Abort();
                //}

                //if (m_consumerThread.IsAlive)
                //{
                //    m_consumerThread.Abort();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("停止线程失败，错误消息为：" + ex.Message);
            }
        }

        private void frmAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                StopThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭分析窗口失败，错误消息为：" + ex.Message);
            }
        }
    }
}
