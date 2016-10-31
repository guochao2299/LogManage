using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules;
using LogManage.DataType;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class UpdateSecurityEventCommand:CommandBase
    {
        private SecurityEvent m_oldValue = null;
        private SecurityEvent m_newValue = null;

        public UpdateSecurityEventCommand(SecurityEvent se)
        {
            m_newValue = se.LowerClone();
            m_oldValue = SecurityEventService.Instance.GetSecurityEvent(se.EventGuid).LowerClone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.UpdateSecurityEvent(m_newValue);
        }

        public override void Undo()
        {
            SecurityEventService.Instance.UpdateSecurityEvent(m_oldValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_oldValue.EventGuid;
            arg.Tag = m_oldValue.Name;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.UpdateSecurityEvent(m_newValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_newValue.EventGuid;
            arg.Tag = m_newValue.Name;
            RaiseRedoDoneEvent(arg);
        }
    }
}
