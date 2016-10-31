using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules;
using LogManage.DataType;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class CreateNewUserActionCommand:CommandBase
    {
        private string m_eventGuid = string.Empty;
        private SecurityAction m_action = null;

        public CreateNewUserActionCommand(string eventGuid, SecurityAction sa)
        {
            m_eventGuid = eventGuid;
            m_action =(SecurityAction)sa.Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.AddSecurityAction(m_eventGuid,(SecurityAction) m_action.Clone());
        }

        public override void Undo()
        {
            SecurityEventService.Instance.DeleteSecurityAction(m_eventGuid, m_action.ActionGuid);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_action.ActionGuid;
            arg.Tag = m_action.Name;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.AddSecurityAction(m_eventGuid, (SecurityAction)m_action.Clone());

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_action.ActionGuid;
            arg.Tag = m_action.Name;

            RaiseUndoDoneEvent(arg);
        }
    }
}
