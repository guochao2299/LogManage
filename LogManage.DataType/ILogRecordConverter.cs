using System;
using System.Text;
using System.Collections.Generic;

namespace LogManage.DataType
{
    public interface ILogRecordConverter<T>
    {
        string TableGuid { get; }

        LogRecord ToLogRecord(string appGuid, T originalData);

        List<LogRecord> ToLogRecord(string appGuid, List<T> originalData);

        void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, LogRecord record);

        void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, List<LogRecord> record);
    }

    public class NullLogRecordConverter:ILogRecordConverter<object>
    {
        private NullLogRecordConverter()
        { }

        private static NullLogRecordConverter m_instance = null;

        public static NullLogRecordConverter Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new NullLogRecordConverter();
                }

                return m_instance;
            }
        }

        #region ILogRecordConverter<object> Members

        public string  TableGuid
        {
	        get 
            {
                return string.Empty;
            }
        }

        public LogRecord ToLogRecord(string appGuid, Object originalData)
        {
 	        return LogRecord.NullLogRecord;
        }

        public void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, LogRecord record)
        { 	        
        }

        public void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, List<LogRecord> record)
        {
 
        }

        public List<LogRecord> ToLogRecord(string appGuid, List<Object> originalData)
        {
            return new List<LogRecord>();
        }
        #endregion
    }

    public abstract class LogRecordConverterBase<T> : ILogRecordConverter<T>
    {
        #region ILogRecordConverter<SafeBoxRecord> Members

        public abstract string TableGuid
        {
            get;
        }

        public abstract LogRecord ToLogRecord(string appGuid, T originalData);

        public void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, LogRecord record)
        {
            if (!buffer.ContainsKey(TableGuid))
            {
                buffer.Add(TableGuid, new List<LogRecord>());
            }

            buffer[TableGuid].Add(record);
        }

        public void AddRecord2Dictionary(Dictionary<string, List<LogRecord>> buffer, List<LogRecord> record)
        {
            if (!buffer.ContainsKey(TableGuid))
            {
                buffer.Add(TableGuid, new List<LogRecord>());
            }

            buffer[TableGuid].AddRange(record);
        }

        public List<LogRecord> ToLogRecord(string appGuid, List<T> originalData)
        {
            List<LogRecord> lstResult = new List<LogRecord>();

            foreach (T b in originalData)
            {
                lstResult.Add(ToLogRecord(appGuid, b));
            }

            return lstResult;
        }

        #endregion
    }
}
