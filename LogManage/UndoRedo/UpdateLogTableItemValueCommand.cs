using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    public class UpdateLogTableItemValueCommand:CommandBase
    {
        private LogTableItem  m_newValue=null;
        private LogTableItem m_oldValue=null;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public UpdateLogTableItemValueCommand(string appGuid, string tableGuid, LogTableItem newValue)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;

            m_newValue = (LogTableItem)newValue.Clone();
            m_oldValue = (LogTableItem)AppService.Instance.GetAppTableItem(appGuid, tableGuid, newValue.LogColumnIndex).Clone();
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.UpdateTableItemValue(m_appGuid, m_tableGuid, m_newValue);
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列内容失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.UpdateTableItemValue(m_appGuid, m_tableGuid, m_oldValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_oldValue;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.UpdateTableItemValue(m_appGuid, m_tableGuid, m_newValue);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_newValue;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
