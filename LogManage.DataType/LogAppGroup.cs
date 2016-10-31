using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 应用程序分组
    /// </summary>
    public class LogAppGroup
    {
        public LogAppGroup(string groupName)
        {
            m_groupName = groupName;
        }

        public LogAppGroup()
        {
            
        }

        private string m_groupName = string.Empty;

        /// <summary>
        /// 应用程序分组名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_groupName;
            }
            set
            {
                m_groupName = value;
            }
        }

        public void CopyFrom(LogAppGroup group)
        {
            this.Name = group.Name;
        }
    }
}
