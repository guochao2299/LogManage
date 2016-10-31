using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules.Evaluation
{
    /// <summary>
    /// 输入一条日志记录就可以马上获取日志分析结果的类的基类
    /// </summary>
    public abstract class ImmediatelyReturnRuleProcessorBase:IRuleProcessor
    {
        protected class ConditionInfo
        {
            public string EventGuid = string.Empty;
            public string EventName = string.Empty;
            public string ActionGuid = string.Empty;
            public string ActionName = string.Empty;

            public SecurityCondition Condition=null;
        }

        protected List<SecurityEvent> m_events = new List<SecurityEvent>();
        protected List<EvaluateResult> m_result = new List<EvaluateResult>();

        #region IRuleProcessor Members

        public virtual void InitialRules(List<SecurityEvent> lstRules)
        {
            m_events.Clear();
            m_events.AddRange(lstRules);
        }

        public bool IsImmediatelyReturn
        {
            get 
            {
                return true;    
            }
        }

        public abstract void Execute(LogRecord record, ILogColumnService colSrv);

        /// <summary>
        /// 不需要调用此函数
        /// </summary>
        public void Analysis()
        {            
        }

        /// <summary>
        /// 返回日志分析结果
        /// </summary>
        public List<EvaluateResult> Result
        {
            get 
            {
                return m_result;
            }
        }

        #endregion
    }
}
