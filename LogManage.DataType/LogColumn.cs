//
// 代表
using System;
using System.Text;
using System.Xml.Serialization;

namespace LogManage.DataType
{
    /// <summary>
    /// 表示应用系统日志内容的一列
    /// </summary>
    [Serializable()]
    public class LogColumn:ICloneable
    {
        private string m_name;
        
        /// <summary>
        /// 该列对应的列名，可以为空
        /// </summary>
        public String Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        private Int32 m_index;

        /// <summary>
        /// 列对应的序号，该序号为唯一值
        /// </summary>
        public Int32 Index
        {
            get
            {
                return m_index;
            }
            set
            {
                m_index = value;
            }
        }

        private string m_type;

        /// <summary>
        /// 列对应的类型，主要用于条件检索时使用
        /// </summary>
        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public object Clone()
        {
            LogColumn lc = new LogColumn();
            lc.Type = this.Type;
            lc.Name = this.Name;
            lc.Index = this.Index;

            return lc;
        }

        /// <summary>
        /// 返回一个空列
        /// </summary>
        [XmlIgnore]
        public static LogColumn NullColumn
        {
            get
            {
                LogColumn lc = new LogColumn();
                lc.Index = -1;
                lc.m_type = string.Empty;
                lc.Name = ConstColumnValue.NullLogColumnName;

                return lc;
            }
        }
    }
}
