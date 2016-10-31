using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 表示一条过滤条件
    /// </summary>
    public class LogFilterCondition
    {
        public LogFilterCondition(int colIndex, string type)
        {
            m_colIndex = colIndex;
            m_type = type;
        }

        private int m_colIndex = ConstColumnValue.NullLogColumnIndex;
        private string m_type = string.Empty;
        private string m_content = string.Empty;
        private string m_leftBound = string.Empty;
        private string m_rightBound = string.Empty;
        private string m_relation = string.Empty;


        public string Relation
        {
            get
            {
                return m_relation;
            }
            set
            {
                m_relation = value;
            }
        }

        public int ColumnIndex
        {
            get            
            {
                return m_colIndex;
            }
        }

        public string Type
        {
            get
            {
                return m_type;
            }
        }

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

        public string LeftBound        
        {
            get
            {
                return m_leftBound;
            }
            set
            {
                m_leftBound = value;
            }
        }

        public string RightBound
        {
            get
            {
                return m_rightBound;
            }
            set
            {
                m_rightBound = value;
            }
        }

    }
}
