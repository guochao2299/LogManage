using System;
using System.Collections.Generic;
using System.Text;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.UndoRedo
{
    public class ReNameTableCommand:CommandBase
    {
        private string m_oldName = string.Empty;
        private string m_newName = string.Empty;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public ReNameTableCommand(string appGuid, string tableGuid,string newName)
        {
            m_newName = newName;
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;
            m_oldName = AppService.Instance.GetAppTable(appGuid,tableGuid).Name;
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                if (string.Equals(m_oldName, m_newName, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                AppService.Instance.ReNameTable(m_appGuid,m_tableGuid,m_newName);                
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.ReNameTable(m_appGuid, m_tableGuid, m_oldName);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_oldName;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.ReNameTable(m_appGuid, m_tableGuid, m_newName);
            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_newName;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
