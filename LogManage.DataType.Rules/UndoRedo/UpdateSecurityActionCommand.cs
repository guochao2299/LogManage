using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class UpdateSecurityActionCommand:CommandBase
    {
        private SecurityAction m_oldValue = null;
        private SecurityAction m_newValue = null;
        private string m_eventGuid = string.Empty;

        public UpdateSecurityActionCommand(string eventGuid,SecurityAction sa)
        {
            m_eventGuid = eventGuid;
            m_newValue = sa.LowerClone();
            m_oldValue = SecurityEventService.Instance.GetSecurityAction(eventGuid, sa.ActionGuid).LowerClone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.UpdateSecurityActionProperties(m_eventGuid, m_newValue);           
        }

        public override void Undo()
        {
            SecurityEventService.Instance.UpdateSecurityActionProperties(m_eventGuid, m_oldValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_oldValue.ActionGuid;
            arg.Tag = m_oldValue.Name;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.UpdateSecurityActionProperties(m_eventGuid, m_newValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_newValue.ActionGuid;
            arg.Tag = m_newValue.Name;

            RaiseRedoDoneEvent(arg);
        }
    }
}
