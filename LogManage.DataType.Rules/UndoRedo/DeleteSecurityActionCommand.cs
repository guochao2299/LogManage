using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.DataType.Rules;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class DeleteSecurityActionCommand:CommandBase
    {
        private string m_eventGuid = string.Empty;
        private SecurityAction m_action = null;

        public DeleteSecurityActionCommand(string eventGuid,string actionGuid)
        {
            m_eventGuid = eventGuid;
            m_action = (SecurityAction)SecurityEventService.Instance.GetSecurityAction(eventGuid, actionGuid).Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.DeleteSecurityAction(m_eventGuid, m_action.ActionGuid);
        }

        public override void Undo()
        {
            SecurityEventService.Instance.AddSecurityAction(m_eventGuid, (SecurityAction)m_action.Clone());

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_action.ActionGuid;
            arg.Tag = m_action.Name;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.DeleteSecurityAction(m_eventGuid, m_action.ActionGuid);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_action.ActionGuid;
            arg.Tag = m_action.Name;

            RaiseUndoDoneEvent(arg);
        }
    }
}
