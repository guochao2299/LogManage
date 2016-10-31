using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    /// <summary>
    /// 只是更新列顺序命令，该命令的undo/redo不做任何操作
    /// </summary>
    class UpdateTableItemSequenceCommand:CommandBase
    {
        private List<int> m_seq;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public UpdateTableItemSequenceCommand(string appGuid, string tableGuid, List<int> newSequence)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;

            m_seq = new List<int>();

            foreach (int i in newSequence)
            {
                m_seq.Add(i);
            }
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.UpdateTableItemsSequence(m_appGuid, m_tableGuid, m_seq);
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列顺序失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {           
        }

        public override void Redo()
        {
        }

        #endregion
    }
}
