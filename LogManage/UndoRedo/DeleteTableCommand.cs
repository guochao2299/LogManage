using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    public class DeleteLogStructCommand:CommandBase
    {
        private LogTable m_data=LogTable.NullLogTable;
        private string m_appGuid=string.Empty;
        public DeleteLogStructCommand(string appGuid,LogTable lt)
        {
            m_data = (LogTable)lt.Clone();
            m_appGuid = appGuid;
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.RemoveTable(m_appGuid,m_data.GUID);
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志表结构失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.AddTable(m_appGuid,m_data);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.SecondLevelGuid = m_data.GUID;
            arg.FirstLevelGuid = m_appGuid;
            arg.Tag = m_data.Name;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.RemoveTable(m_appGuid,m_data.GUID);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.SecondLevelGuid = m_data.GUID;
            arg.FirstLevelGuid = m_appGuid;
            arg.Tag = m_data.Name; ;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
