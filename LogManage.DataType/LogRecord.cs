using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    public class LogRecord
    {
        private LogRecord()
        {
            m_items = new Dictionary<int, LogRecordItem>();
        }

        private Dictionary<int, LogRecordItem> m_items = null;

        /// <summary>
        /// 该日志记录中包含的项，与日志结构对应
        /// </summary>
        public Dictionary<int, LogRecordItem> Items
        {
            get
            {
                return m_items;
            }
        }

        public string GetItemValue(int colIndex)
        {
            if (Items.ContainsKey(colIndex))
            {
                return Items[colIndex].Conetent;
            }

            return string.Empty;
        }

        private string m_appGuid=string.Empty;
        public string AppGuid
        {
            get
            {
                return m_appGuid;
            }
        }

        private string m_tableGuid=string.Empty;
        public string TableGuid
        {
            get
            {
                return m_tableGuid;
            }
        }

        private string m_recordGuid=string.Empty;
        public string RecordGuid
        {
            get
            {
                return m_recordGuid;
            }
            set
            {
                m_recordGuid=value;
            }
        }

        /// <summary>
        /// 创建新的日志记录，假定记录已经存在唯一的GUID
        /// </summary>
        /// <param name="appGuid">日志所属应用程序GUID</param>
        /// <param name="tableGuid">日志所属表格GUID</param>
        /// <param name="recordGuid">日志的Guid</param>
        /// <returns>返回创建的GUID</returns>
        public static LogRecord CreateNewLogRecord(string appGuid, string tableGuid, string recordGuid)
        {
            LogRecord lr = new LogRecord();
            lr.m_appGuid = appGuid;
            lr.m_recordGuid = recordGuid;
            lr.m_tableGuid = tableGuid;

            return lr;
        }

        /// <summary>
        /// 创建新的日志记录，并未该记录创建唯一的GUID
        /// </summary>
        /// <param name="appGuid">日志所属应用程序GUID</param>
        /// <param name="tableGuid">日志所属表格GUID</param>
        /// <returns>返回创建的GUID</returns>
        public static LogRecord CreateNewLogRecord(string appGuid, string tableGuid)
        {
            LogRecord lr = new LogRecord();
            lr.m_appGuid = appGuid;
            lr.m_recordGuid = Guid.NewGuid().ToString();
            lr.m_tableGuid = tableGuid;

            return lr;
        }

        public static LogRecord NullLogRecord
        {
            get
            {
                LogRecord lr = new LogRecord();

                return lr;
            }
        }
    }
}
