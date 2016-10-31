using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules.Evaluation
{
    public abstract class FinalReturnRuleProcessorBase:IRuleProcessor
    {
        protected List<SecurityEvent> m_events = new List<SecurityEvent>();
        protected List<EvaluateResult> m_result = new List<EvaluateResult>();

        #region IRuleProcessor Members

        public virtual void InitialRules(List<SecurityEvent> lstRules)
        {
            
        }

        public bool IsImmediatelyReturn
        {
            get 
            {
                return false;
            }
        }

        public abstract void Execute(LogRecord record, ILogColumnService colSrv);

        public abstract void Analysis();

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
