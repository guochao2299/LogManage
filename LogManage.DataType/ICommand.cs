using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType
{
    public delegate void UndoRedoEventHandler(UndoRedoEventArg e);

    public interface ICommand
    {
        void Execute();

        event UndoRedoEventHandler UndoDone;

        event UndoRedoEventHandler RedoDone;

        void Undo();

        void Redo();
    }

    public abstract class CommandBase : ICommand
    {
        public abstract void Execute();

        public abstract void Undo();

        public abstract void Redo();

        protected void RaiseUndoDoneEvent(UndoRedoEventArg e)
        {
            if (m_undoDone != null)
            {
                m_undoDone(e);
            }
        }

        protected void RaiseRedoDoneEvent(UndoRedoEventArg e)
        {
            if (m_redoDone != null)
            {
                m_redoDone(e);
            }
        }

        private UndoRedoEventHandler m_undoDone;

        public event UndoRedoEventHandler UndoDone
        {
            add
            {
                m_undoDone += value;
            }
            remove
            {
                m_undoDone -= value;
            }
        }

        private UndoRedoEventHandler m_redoDone;

        public event UndoRedoEventHandler RedoDone
        {
            add
            {
                m_redoDone += value;
            }
            remove
            {
                m_redoDone -= value;
            }
        }   
    }
}
