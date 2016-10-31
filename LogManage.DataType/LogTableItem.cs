using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LogManage.DataType
{
    [Serializable()]
    [XmlRoot("LogTableItem")]
    public class LogTableItem:ICloneable
    {        
        private Int32 m_logColumnIndex;

        /// <summary>
        /// 
        /// </summary>
        public Int32 LogColumnIndex
        {
            get
            {
                return m_logColumnIndex;
            }
            set
            {
                m_logColumnIndex = value;
            }
        }


        private string m_colName = string.Empty;
        public string ColumnName
        {
            get
            {
                return m_colName;
            }
            set
            {
                m_colName = value;
            }
        }

        private bool m_isFilterColumn = ConstTableValue.DefaultLogTableItemIsFilterValue;

        /// <summary>
        /// 该列是否作为条件检索列，如果是，则显示检索条件时可以显示该检索条件
        /// </summary>
        public bool IsFilterColumn
        {
            get
            {
                return m_isFilterColumn;
            }
            set
            {
                m_isFilterColumn = value;
            }
        }

        private int m_seqIndex;
        public int SeqIndex
        {
            get
            {
                return m_seqIndex;
            }
            set
            {
                m_seqIndex = value;
            }
        }

        private bool m_visible = true;

        /// <summary>
        /// 日志列是否为隐藏，true为显示，false为显示
        /// </summary>
        public bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
            }
        }

        private string m_nickName=string.Empty;

        /// <summary>
        /// 日志列在当前表中的别名
        /// </summary>
        public string NickName
        {
            get
            {
                return m_nickName;
            }
            set
            {
                m_nickName = value;
            }
        }

        /// <summary>
        /// 从另外一个对象中复制全部内容
        /// </summary>
        /// <param name="item"></param>
        public void CopyFrom(LogTableItem item)
        {
            this.IsFilterColumn = item.IsFilterColumn;
            this.LogColumnIndex = item.LogColumnIndex;
            this.ColumnName = item.ColumnName;
            this.SeqIndex = item.SeqIndex;
            this.NickName = item.NickName;
            this.Visible = item.Visible;
        }

        public static LogTableItem CreateNewLogTableItem(Int32 colIndex)
        {
            return CreateNewLogTableItem(colIndex, false);
        }

        public static LogTableItem CreateNewLogTableItem(Int32 colIndex,bool isFilter)
        {
            LogTableItem lti = new LogTableItem();
            lti.LogColumnIndex = colIndex;
            lti.IsFilterColumn = isFilter;

            return lti;
        }

        public object Clone()
        {
            LogTableItem item = new LogTableItem();
            item.IsFilterColumn = this.IsFilterColumn;
            item.LogColumnIndex = this.LogColumnIndex;
            item.ColumnName = this.ColumnName;
            item.Visible = this.Visible;
            item.NickName = this.NickName;
            return item;
        }
    }
}
