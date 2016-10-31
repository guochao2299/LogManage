using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 表示应用程序的一个日志结构
    /// </summary>
    [Serializable()]
    public class LogTable:ICloneable
    {
        public LogTable()
        {
            m_columns = new List<LogTableItem>();
        }

        public LogTable(string guid):this()
        {
            m_guid = guid;
        }

        private string m_guid;

        /// <summary>
        /// 表的唯一标示符
        /// </summary>
        public string GUID
        {
            get
            {
                return m_guid;
            }
            set
            {
                m_guid = value;
            }
        }

        private string m_name;

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;

                if (string.IsNullOrEmpty(m_name))
                {
                    throw new NullReferenceException("日志表名不能为空!");
                }
            }
        }

        private List<LogTableItem> m_columns;

        public List<LogTableItem> Columns
        {
            get
            {
                return m_columns;
            }
            set
            {
                m_columns = value;
            }
        }

        /// <summary>
        /// 按顺序返回各列的列号
        /// </summary>
        [XmlIgnore]
        public List<int> ColIndexesASC
        {
            get
            {
                List<int> lstindexes = new List<int>();

                for (int i = 0; i < Columns.Count; i++)
                {
                    lstindexes.Add(Columns[i].LogColumnIndex);
                }

                return lstindexes;
            }
        }

        public object Clone()
        {
            LogTable lt = new LogTable();
            lt.m_guid = this.GUID;
            lt.Name = this.Name;

            foreach (LogTableItem lc in this.Columns)
            {
                lt.Columns.Add((LogTableItem)lc.Clone());
            }

            return lt;
        }

        public void AddColumns(List<LogTableItem> items)
        {
            foreach (LogTableItem lti in items)
            {
                Columns.Add((LogTableItem)lti.Clone());
            }
        }

        public void RemoveColumns(List<int> lstColIndex)
        {
            for (int i = Columns.Count - 1; i >= 0; i--)
            {
                if (lstColIndex.Contains(Columns[i].LogColumnIndex))
                {
                    Columns.RemoveAt(i);
                }
            }
        }

        public LogTableItem GetColumn(int colIndex)
        {
            foreach (LogTableItem lti in Columns)
            {
                if (lti.LogColumnIndex == colIndex)
                {
                    return lti;
                }
            }

            throw new Exception(string.Format("指定的日志列在日志表中不存在，列号为{0}", colIndex));
        }

        public void UpdateSequence(List<int> lstColIndexes)
        {

            List<LogTableItem> lstNewValues = new List<LogTableItem>();

            for (int i = 0; i < lstColIndexes.Count; i++)
            {
                lstNewValues.Add((LogTableItem)GetColumn(lstColIndexes[i]).Clone());
            }
            
            m_columns.Clear();
            m_columns.AddRange(lstNewValues);
            GC.Collect();
        }

        [XmlIgnore]
        public static LogTable NullLogTable
        {
            get
            {
                LogTable lt = new LogTable();
                lt.Name = ConstTableValue.NullLogTableName;
                lt.m_guid = string.Empty;

                return lt;
            }
        }

        [XmlIgnore]
        public static LogTable NewLogTable
        {
            get
            {
                LogTable lt = new LogTable();
                lt.Name = ConstTableValue.DefaultLogTableName;
                lt.m_guid = Guid.NewGuid().ToString();

                return lt;
            }
        }

        public static LogTable CreateNewLogTable(string tableName)
        {
            LogTable lt = new LogTable();
            lt.Name = tableName;
            lt.m_guid = Guid.NewGuid().ToString();

            return lt;
        }

        public static LogTable CreateLogTable(string tableName,string guid)
        {
            LogTable lt = new LogTable();
            lt.Name = tableName;
            lt.m_guid = guid;

            return lt;
        }
    }
}
