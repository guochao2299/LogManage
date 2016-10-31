using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    ///  命令箱，命令集合
    /// </summary>
    public class CommandBox
    {
        private Stack<ICommand> m_undoBuffer = null;
        private Stack<ICommand> m_redoBuffer = null;

        public CommandBox()
        {
            m_redoBuffer = new Stack<ICommand>();
            m_undoBuffer = new Stack<ICommand>();
        }


        /// <summary>
        /// 是否可以做undo操作
        /// </summary>
        public bool CanUndo
        {
            get
            {
                return m_undoBuffer.Count > 0;
            }
        }

        /// <summary>
        /// 是否可以做redo操作
        /// </summary>
        public bool CanRedo
        {
            get
            {
                return m_redoBuffer.Count > 0;
            }
        }

        /// <summary>
        /// 添加操作命令
        /// </summary>
        /// <param name="cmd"></param>
        public void AddCommand(ICommand cmd)
        {
            // 是不是应该有个最大条数，要不内存受不了

            m_undoBuffer.Push(cmd);

            // 这里是不是应该把m_pointer之后的内容都删除掉?否则redo的时候就会有问题
            // 应该是要清除m_pointer后面的
            if (m_redoBuffer.Count > 0)
            {
                m_redoBuffer.Clear();
            }
        }

        public void Undo()
        {
            if (m_undoBuffer.Count <= 0)
            {
                return;
            }

            ICommand ic = m_undoBuffer.Pop();

            ic.Undo();
            m_redoBuffer.Push(ic);
        }

        public void Redo()
        {
            if (m_redoBuffer.Count <= 0)
            {
                return;
            }

            ICommand ic = m_redoBuffer.Pop();

            ic.Redo();
            m_undoBuffer.Push(ic);
        }

        public void Clear()
        {
            m_undoBuffer.Clear();
            m_redoBuffer.Clear();
        }
    }
}
