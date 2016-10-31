using System;
using System.Collections.Generic;
using System.Text;

using LogManage.Services;
using LogManage.DataType;

namespace LogManage.UndoRedo
{   
    /// <summary>
    /// 创建新应用程序命令。
    /// </summary>
    internal class CreateNewAppCommand:CommandBase
    {
        private LogApp m_newData;
        public CreateNewAppCommand(LogApp la)
        {
            m_newData = (LogApp)la.Clone();
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.AddApp(m_newData);
            }
            catch (Exception ex)
            {
                throw new Exception("创建新的应用程序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.RemoveApp(m_newData);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_newData.AppGUID;
            arg.Tag = m_newData;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.AddApp(m_newData);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.Tag = m_newData;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
