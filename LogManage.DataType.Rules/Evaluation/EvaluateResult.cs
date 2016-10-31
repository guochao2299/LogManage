using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LogManage.DataType.Rules.Evaluation
{
    /// <summary>
    /// 保存策略评估结果
    /// </summary>
    public class EvaluateResult
    {
        public EvaluateResult(string logGuid)
        {
            if (string.IsNullOrWhiteSpace(logGuid))
            {
                throw new Exception("策略评估结果中的日志Guid不能为空!");
            }

            m_logGuid = logGuid;
        }

        // 下面几项用于追溯日志出处
        private string m_logGuid = string.Empty;
        private string m_tableGuid = string.Empty;
        private string m_appGuid = string.Empty;

        // 下面这几项是用于追溯安全事件出处
        private string m_resultGuid = string.Empty;
        private Color m_bgColor = Color.White;
        private string m_name = string.Empty;
        private string m_eventName = string.Empty;
        private string m_eventGuid = string.Empty;
        private string m_actionName = string.Empty;
        private string m_actionGuid = string.Empty;

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

        /// <summary>
        /// 保存该评估结果属于哪条日志
        /// </summary>
        public string LogGuid
        {
            get
            {
                return m_logGuid;
            }
        }

        /// <summary>
        /// 保存评估结果对应的guid，如果在日志管理系统中已经定义了需要的结果，可以直接保存结果对应的guid
        /// </summary>
        public string ResultGuid
        {
            get
            {
                return m_resultGuid;
            }
            set
            {
                m_resultGuid = value;
            }
        }

        /// <summary>
        /// 如果ResultGuid为空，或者日志管理系统中没有定义对应的ResultGuid，可以在这里保存系统中显示评估结果时的名称
        /// </summary>
        public string ResultName
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

        /// <summary>
        /// 评估结果所属的事件名称
        /// </summary>
        public string EventName
        {
            get
            {
                return m_eventName;
            }
            set
            {
                m_eventName = value;
            }
        }

        /// <summary>
        /// 评估结果所属的事件的Guid，便于查询信息
        /// </summary>
        public string EventGuid
        {
            get
            {
                return m_eventGuid;
            }
            set
            {
                m_eventGuid = value;
            }
        }

        /// <summary>
        /// 评估结果所属的行为名称
        /// </summary>
        public string ActionName
        {
            get
            {
                return m_actionName;
            }
            set
            {
                m_actionName = value;
            }
        }

        /// <summary>
        /// 评估结果所属的行为的guid
        /// </summary>
        public string ActionGuid
        {
            get
            {
                return m_actionGuid;
            }
            set
            {
                m_actionGuid = value;
            }
        }

        /// <summary>
        /// 如果ResultGuid为空，或者日志管理系统中没有定义对应的ResultGuid，可以在这里保存系统中显示评估结果时的背景颜色
        /// </summary>
        public Color BackColor
        {
            get
            {
                return m_bgColor;
            }
            set
            {
                m_bgColor = value;
            }

        }
    }
}
