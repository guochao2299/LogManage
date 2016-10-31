using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;

namespace LogManage.SelfDefineControl
{
    internal class LogShowTabPage:TabPage
    {
        private List<LogRecord> m_records = null;

        public LogShowTabPage()
            : base()
        {
            InitControl();
        }

        public LogShowTabPage(string text)
            : base(text)
        {
            InitControl();
        }

        private void InitControl()
        {
            m_showLog = new usShowLog();
            m_showLog.Dock = DockStyle.Fill;

            this.Controls.Add(m_showLog);
        }
        private usShowLog m_showLog = null;

        public void ResetCloumns(LogTable lt)
        {
            m_showLog.ResetCloumns(lt);
        }

        public void SetLogs(List<LogRecord> lstRecords)
        {
            m_showLog.SetLog(lstRecords);
            
            // 这里把日志集合暂时记录一下，以便日志分析时又重新获取一遍
            m_records = lstRecords;
        }

        /// <summary>
        /// 保存的日志记录
        /// </summary>
        public List<LogRecord> Records
        {
            get
            {
                return m_records;
            }
            set
            {
                m_records = value;
            }
        }

        private string m_appGuid = string.Empty;

        /// <summary>
        /// EventGuid和TableGuid用来记录与日志相关的系统和表信息
        /// </summary>
        public string AppGuid
        {
            get
            {
                return m_appGuid;
            }
            set
            {
                m_appGuid = value;
            }
        }

        private string m_tableGuid = string.Empty;
        public string TableGuid
        {
            get
            {
                return m_tableGuid;
            }
            set
            {
                m_tableGuid = value;
            }
        }

        public int LogRecordCount
        {
            get
            {
                return (m_records==null)?0:m_records.Count;
            }
        }
    }
}
