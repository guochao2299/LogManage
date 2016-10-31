using System;
using System.Collections.Generic;
using System.Text;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.UndoRedo
{
    public class UpdateAppPropertiesCommand:CommandBase
    {
        private LogAppMemento m_oldApp ;
        private LogAppMemento m_newApp ;
        private string m_appGuid = string.Empty;

        public UpdateAppPropertiesCommand(string appGuid, LogAppMemento newData)
        {
            m_newApp = newData;
            m_appGuid = appGuid;
            m_oldApp = AppService.Instance.GetApp(appGuid).CreateMemento();
        }

        private bool IsAppMementoEqualed()
        {
            return (m_oldApp.IsImportLogsFromFile == m_newApp.IsImportLogsFromFile) &&
                (string.Equals(m_oldApp.LogAppName, m_newApp.LogAppName, StringComparison.OrdinalIgnoreCase));
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                if (IsAppMementoEqualed())
                {
                    return;
                }

                AppService.Instance.UpdateAppProperties(m_appGuid,m_newApp);                
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.UpdateAppProperties(m_appGuid, m_oldApp);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.Tag = m_oldApp;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.UpdateAppProperties(m_appGuid, m_newApp);
            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.Tag = m_newApp;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
