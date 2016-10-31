using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.DataType.Rules;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class CreateNewSecurityEventCommand : CommandBase
    {
        private SecurityEvent m_initData = null;

        public CreateNewSecurityEventCommand(SecurityEvent se)
        {
            m_initData =(SecurityEvent)se.Clone();
        }

        public override void Execute()
        {
            SecurityEventService.Instance.AddSecurityEvent((SecurityEvent)m_initData.Clone());
        }

        public override void Undo()
        {
            SecurityEventService.Instance.DeleteSecurityEvent(m_initData.EventGuid);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_initData.EventGuid;
            arg.Tag = m_initData.Name;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            SecurityEventService.Instance.AddSecurityEvent((SecurityEvent)m_initData.Clone());

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_initData.EventGuid;
            arg.Tag = m_initData.Name;
            RaiseRedoDoneEvent(arg);
        }
    }
}
