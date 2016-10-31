using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    /// <summary>
    /// 删除应用程序命令。
    /// </summary>
    public class DeleteAppCommand:CommandBase
    {
        private LogApp m_newData;
        public DeleteAppCommand(LogApp la)
        {
            m_newData = (LogApp)la.Clone();
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.RemoveApp(m_newData);
            }
            catch (Exception ex)
            {
                throw new Exception("删除新的应用程序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.AddApp(m_newData);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_newData.AppGUID;
            arg.Tag = m_newData;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.RemoveApp(m_newData);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_newData.AppGUID;
            arg.Tag = m_newData;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
