using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LogColumnAttribute:Attribute
    {
        public LogColumnAttribute()
        { }

        private int m_colIndex;
        
        /// <summary>
        /// 表示具有该属性的Property对应的日志管理系统中定义的日志列的列号
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
    }
}
