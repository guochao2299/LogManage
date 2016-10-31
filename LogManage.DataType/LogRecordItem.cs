using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 表示日志系统中的一列内容
    /// </summary>
    public class LogRecordItem
    {
        public LogRecordItem(int colIndex, string content)
        {
            m_colIndex = colIndex;
            m_content = content??string.Empty;
        }

        private int m_colIndex = ConstColumnValue.NullLogColumnIndex;
        private string m_content = string.Empty;

        public int ColumnIndex
        {
            get
            {
                return m_colIndex;
            }
        }

        public string Conetent
        {
            get
            {
                return m_content;
            }
        }

        public static LogRecordItem NullRecordItem
        {
            get
            {
                return new LogRecordItem(ConstColumnValue.NullLogColumnIndex, "这是一条错误的日志记录");
            }
        }
    }
}
