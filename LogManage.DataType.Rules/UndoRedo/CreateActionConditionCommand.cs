using System;
using System.Collections.Generic;
using System.Text;

//using LogManage.DataType;
//using LogManage.DataType.Rules;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class CreateActionConditionCommand:CommandBase
    {
        private string m_eventGuid = string.Empty;
        private string m_actionGuid = string.Empty;
        private SecurityCondition m_condition = null;

        public CreateActionConditionCommand(string eventGuid, string actionGuid, SecurityCondition condition)
        {
            m_actionGuid = actionGuid;
            m_eventGuid = eventGuid;
            m_condition = (SecurityCondition)condition.Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.AddSecurityCondition(m_eventGuid, m_actionGuid,
                (SecurityCondition)m_condition.Clone());
        }

        public override void Undo()
        {
            SecurityEventService.Instance.DeleteSecurityCondition(m_eventGuid, m_actionGuid,
                m_condition.ConditionGuid);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = m_condition.ConditionGuid;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.AddSecurityCondition(m_eventGuid, m_actionGuid,
               (SecurityCondition)m_condition.Clone());

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = m_condition;

            RaiseRedoDoneEvent(arg);
        }
    }
}
