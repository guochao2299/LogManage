using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 表示一条日志过滤项,
    /// </summary>
    public class LogFilterRecord
    {
        public LogFilterRecord()
        { }

        private int m_colIndex;

        /// <summary>
        /// 过滤项对应的列
        /// </summary>
        public int ColumnIndex
        {
            get 
            {
                return m_colIndex;
            }
            set
            {
                m_colIndex = value;
            }
        }

        private string m_colType=ConstColumnValue.DefaultLogColumnType;

        /// <summary>
        /// 列的类型，目前固定为字符串、数字和日期三种
        /// </summary>
        public string ColumnType        
        {
            get
            {
                return m_colType;
            }
            set
            {
                m_colType = value;
            }
        }

        private string m_content = string.Empty;
        
        /// <summary>
        /// 如果类型是字符串和数字，则本项保存其值
        /// </summary>
        public string Content
        {
            get
            {
                return m_content;
            }
            set
            {
                m_content = value;
            }
        }

        private string m_startDate = DateTime.Now.ToString();

        /// <summary>
        /// 如果过滤项为日期，则本项保存查询起始日期
        /// </summary>
        public string StartDate
        {
            get
            {
                return m_startDate;
            }
            set
            {
                m_startDate = value;
            }
        }

        private string m_endDate = DateTime.Now.ToString();

        /// <summary>
        /// 如果过滤项为日期，则本项保存查询结束日期
        /// </summary>
        public string EndDate
        {
            get
            {
                return m_endDate;
            }
            set
            {
                m_endDate = value;
            }
        }
    }
}
