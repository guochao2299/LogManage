using System;
using System.Collections.Generic;
using System.Text;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.UndoRedo
{
    class CreateNewLogStructCommand:CommandBase
    {
        private LogTable m_newData=LogTable.NullLogTable;
        private string m_appGuid = string.Empty;

        public CreateNewLogStructCommand(string appGuid, LogTable lt)
        {
            m_newData = (LogTable)lt.Clone();
            m_appGuid = appGuid;
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.AddTable(m_appGuid,m_newData);
            }
            catch (Exception ex)
            {
                throw new Exception("创建新的应用程序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.RemoveTable(m_appGuid,m_newData.GUID);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_newData.GUID;
            arg.Tag = m_newData.Name;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.AddTable(m_appGuid,m_newData);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.Tag = m_newData.Name;
            arg.SecondLevelGuid = m_newData.GUID;
            arg.FirstLevelGuid = m_appGuid;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
