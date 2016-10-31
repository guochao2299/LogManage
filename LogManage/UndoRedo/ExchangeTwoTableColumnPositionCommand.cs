using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    /// <summary>
    /// 交换日志表中两列的顺序命令，这个新顺序不会更改数据库里面的顺序，需要在命令对数据库更新,undoredo事件参数中tag保存的是List,顺序保存firstIndex和secondIndex
    /// </summary>
    public class ExchangeTwoTableColumnPositionCommand:CommandBase
    {
        private int m_firstIndex = -1;
        private int m_secondIndex = -1;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public ExchangeTwoTableColumnPositionCommand(string appGuid, string tableGuid, int firstIndex,int secondIndex)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;

            m_firstIndex = firstIndex;
            m_secondIndex = secondIndex;
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.ExchangeTableItemsSequence(m_appGuid, m_tableGuid,m_firstIndex,m_secondIndex);
            }
            catch (Exception ex)
            {
                throw new Exception("交换日志列顺序内容失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.ExchangeTableItemsSequence(m_appGuid, m_tableGuid, m_secondIndex, m_firstIndex);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;

            List<int> lstSeq = new List<int>();
            lstSeq.Add(m_secondIndex);
            lstSeq.Add(m_firstIndex);
            arg.Tag = lstSeq;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.ExchangeTableItemsSequence(m_appGuid, m_tableGuid, m_firstIndex, m_secondIndex);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;

            List<int> lstSeq = new List<int>();
            lstSeq.Add(m_firstIndex);
            lstSeq.Add(m_secondIndex);            
            arg.Tag = lstSeq;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
