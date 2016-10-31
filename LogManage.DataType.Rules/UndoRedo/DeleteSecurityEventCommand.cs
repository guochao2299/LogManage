using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules;
using LogManage.DataType;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class DeleteSecurityEventCommand:CommandBase
    {
        private SecurityEvent m_initData = null;

        public DeleteSecurityEventCommand(string eventGuid)
        {
            m_initData = (SecurityEvent) SecurityEventService.Instance.GetSecurityEvent(eventGuid).Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.DeleteSecurityEvent(m_initData.EventGuid);
        }

        public override void Undo()
        {
            SecurityEventService.Instance.AddSecurityEvent((SecurityEvent)m_initData.Clone());

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_initData.EventGuid;
            arg.Tag = m_initData.Name;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.DeleteSecurityEvent(m_initData.EventGuid);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_initData.EventGuid;
            arg.Tag = m_initData.Name;
            RaiseRedoDoneEvent(arg);
        }
    }
}
