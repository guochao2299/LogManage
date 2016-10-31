using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

using LogManage.DataType;
using LogManage.DataType.Relations;

namespace LogManage.Services
{
    /// <summary>
    /// 提供有关日志列的服务
    /// </summary>
    public class LogColumnService : ILogColumnService
    {
        private LogColumnService()
        {
            //m_columnDataTypes = new List<string>();
            //InitColumnDataTypes();
        }

        private static LogColumnService m_instance = null;

        public static LogColumnService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new LogColumnService();
                }

                return m_instance;
            }
        }

        //private List<string> m_columnDataTypes = null;

        //private void InitColumnDataTypes()
        //{
        //    try
        //    {
        //        // 默认有三种类型
        //        m_columnDataTypes.Add(CConstValue.DateSign);
        //        m_columnDataTypes.Add(CConstValue.DecimalSign);
        //        m_columnDataTypes.Add(CConstValue.StringSign);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LogColumnService:初始化日志列数据类型失败，错误消息为：" + ex.Message, ex);
        //    }
        //    finally
        //    { 
        //    }
        //}

        ///// <summary>
        ///// 当前可用的日志列数据类型
        ///// </summary>
        //public List<string> AvaliableColumnDataTypes
        //{
        //    get
        //    {
        //        return m_columnDataTypes;
        //    }
        //}

        private void InitLogColumns()
        {
            List<LogColumn> lstColumns = DBService.Instance.ExistingLogColumns;

            foreach (LogColumn col in lstColumns)
            {
                if (m_logColumns.ContainsKey(col.Index))
                {
                    continue;
                }

                m_logColumns.Add(col.Index, col);
            }

            m_maxItemIndex = DBService.Instance.MaxColumnItemColumn + 1;
        }

        private Int32 m_maxItemIndex;

        public LogColumn CreateNewLogColumn(string name, string type)
        {
            LogColumn lc = new LogColumn();
            lc.Name = name;
            lc.Type = type;
            lc.Index = m_maxItemIndex;

            m_maxItemIndex++;
            return lc;
        }

        private Dictionary<int,LogColumn> m_logColumns=null;

        public Dictionary<int, LogColumn> AvaliableColumns
        {
            get
            {
                if (m_logColumns == null)
                {
                    m_logColumns = new Dictionary<int, LogColumn>();

                    InitLogColumns();
                }

                return m_logColumns;
            }
        }

        public string GetColumnName(int colIndex)
        {
            if (!AvaliableColumns.ContainsKey(colIndex))
            {
                throw new Exception(string.Format("日志列编号{0}不存在", colIndex));
            }

            return AvaliableColumns[colIndex].Name;
        }
        
        public void AddLogColumn(LogColumn column)
        {
            if (m_logColumns.ContainsKey(column.Index))
            {
                return;
            }            

            DBService.Instance.AddLogColumn(column);

            m_logColumns.Add(column.Index, column);
        }

        public void AddLogColumns(List<LogColumn> columns)
        {
            if (columns==null ||columns.Count <= 0)
            {
                return;
            }            

            DBService.Instance.AddLogColumns(columns);

            foreach (LogColumn lc in columns)
            {
                AvaliableColumns.Add(lc.Index, lc);
            }            
        }

        public void RemoveLogColumn(int columnIndex)
        {
            DBService.Instance.RemoveColumn(columnIndex);

            m_logColumns.Remove(columnIndex);
        }

        public void RemoveLogColumns(List<int> columnIndexes)
        {
            DBService.Instance.RemoveColumns(columnIndexes);

            foreach (int index in columnIndexes)
            {
                m_logColumns.Remove(index);
            }
        }

        public bool IsColumnIndexExist(int columnIndex)
        {
            return DBService.Instance.ExistColumnIndex(columnIndex); ;
        }

        public LogColumn GetLogColumn(int index)
        {
            if (AvaliableColumns.ContainsKey(index))
            {
                return AvaliableColumns[index];
            }
            else
            {
                return LogColumn.NullColumn;
            }
        }
    }
}
