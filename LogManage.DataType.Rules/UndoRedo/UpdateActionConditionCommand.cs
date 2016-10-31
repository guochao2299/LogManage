using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class UpdateActionConditionCommand:CommandBase
    {
        private string m_eventGuid = string.Empty;
        private string m_actionGuid = string.Empty;
        public SecurityCondition m_oldValue = null;
        public SecurityCondition m_newValue = null;

        public UpdateActionConditionCommand(string eventGuid, string actionGuid, SecurityCondition sc)
        {
            m_eventGuid = eventGuid;
            m_actionGuid = actionGuid;
            m_newValue = (SecurityCondition)sc.Clone();
            m_oldValue = (SecurityCondition)SecurityEventService.Instance.GetSecurityCondition(m_eventGuid, m_actionGuid, sc.ConditionGuid).Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.UpdateSecurityConditionProperties(m_eventGuid,
                m_actionGuid, m_newValue);
        }

        public override void Undo()
        {
            SecurityEventService.Instance.UpdateSecurityConditionProperties(m_eventGuid,
               m_actionGuid, m_oldValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = m_oldValue;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.UpdateSecurityConditionProperties(m_eventGuid,
               m_actionGuid, m_newValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = m_newValue;

            RaiseRedoDoneEvent(arg);
        }
    }
}
