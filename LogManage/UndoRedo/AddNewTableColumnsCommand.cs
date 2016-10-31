using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    public class AddNewTableColumnsCommand:CommandBase
    {
        private List<LogTableItem> m_columns=null;
        private string m_appGuid = string.Empty;
        private string m_tableGuid = string.Empty;

        public AddNewTableColumnsCommand(string appGuid, string tableGuid,List<LogTableItem> columns)
        {
            m_appGuid = appGuid;
            m_tableGuid = tableGuid;

            m_columns = new List<LogTableItem>();

            foreach (LogTableItem lti in columns)
            {
                m_columns.Add((LogTableItem)lti.Clone());
            }
        }

        #region ICommand Members

        public override void Execute()
        {
            try
            {
                AppService.Instance.AddTableItems(m_appGuid, m_tableGuid, m_columns);
            }
            catch (Exception ex)
            {
                throw new Exception("添加日志列失败，错误消息为：" + ex.Message, ex);
            }            
        }

        public override void Undo()
        {
            List<int> lstIndexes=new List<int>();

            for(int i=0;i<m_columns.Count;i++)
            {
                lstIndexes.Add(m_columns[i].LogColumnIndex);
            }

            AppService.Instance.RemoveTableItems(m_appGuid, m_tableGuid,lstIndexes);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_columns;
            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            AppService.Instance.AddTableItems(m_appGuid, m_tableGuid, m_columns);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_appGuid;
            arg.SecondLevelGuid = m_tableGuid;
            arg.Tag = m_columns;

            RaiseRedoDoneEvent(arg);
        }

        #endregion
    }
}
