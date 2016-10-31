using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogTableOfAppAttribute:Attribute
    {
        public LogTableOfAppAttribute(string appGuid, string tableGuid)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;
        }

        private string m_appGuid;
        private string m_tableGuid;

        /// <summary>
        /// 应用程序GUID
        /// </summary>
        public string AppGuid
        {
            get
            {
                return m_appGuid;
            }
            set
            {
                m_appGuid = value;
            }
        }

        /// <summary>
        /// 应用程序中其中一个表的GUID
        /// </summary>
        public string TableGuid
        {
            get
            {
                return m_tableGuid;
            }
            set
            {
                m_tableGuid = value;
            }
        }
    }
}
