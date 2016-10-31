using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    public class RemoveTableColumnsCommand  
        :CommandBase
    {
        private List<LogTableItem> m_columns=null;
        private List<int> m_indexes=null;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public RemoveTableColumnsCommand(string appGuid, string tableGuid,List<LogTableItem> columns)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;

            m_columns = new List<LogTableItem>();
            m_indexes=new List<int>();

            foreach (LogTableItem lti in columns)
            {
                m_columns.Add((LogTableItem)lti.Clone());
                m_indexes.Add(lti.LogColumnIndex);
            }
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.RemoveTableItems(m_appGuid, m_tableGuid, m_indexes);
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志列失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            AppService.Instance.AddTableItems(m_appGuid, m_tableGuid,m_columns);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_columns;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.RemoveTableItems(m_appGuid, m_tableGuid, m_indexes);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_columns;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
