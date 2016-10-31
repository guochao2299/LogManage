using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.Services;

namespace LogManage.UndoRedo
{
    internal class ImportAppStructsCommand : CommandBase
    {
        private List<LogApp> m_apps = new List<LogApp>();

        public ImportAppStructsCommand(List<LogApp> lstApps)
        {
            foreach (LogApp app in lstApps)
            {
                m_apps.Add((LogApp)app.Clone());
            }
        }

        private bool InsertAppStructs()
        {
            try
            {
                foreach (LogApp app in m_apps)
                {
                    if (AppService.Instance.AddApp(LogApp.CreateApp(app.Name,app.AppGUID,app.IsImportLogsFromFiles)))
                    {
                        foreach (LogTable table in app.Tables)
                        {
                            if (AppService.Instance.AddTable(app.AppGUID, LogTable.CreateLogTable(table.Name,table.GUID)))
                            {
                                if (!AppService.Instance.AddTableItems(app.AppGUID, table.GUID, table.Columns))
                                {
                                    throw new Exception(string.Format("应用程序{0}的表{1}插入列出错",
                                        new object[] { app.Name, table.Name }));
                                }
                            }
                            else
                            {
                                throw new Exception(string.Format("在应用程序{0}中插入表{1}出错",
                                                                       new object[] { app.Name, table.Name }));
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("插入应用程序{0}记录出错", app.Name));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("插入日志表结构失败，错误消息为：" + ex.Message);
            }
        }

        private bool RemoveAppStructs()
        {
            try
            {
                foreach (LogApp app in m_apps)
                {
                    if(!AppService.Instance.RemoveApp(app))
                    {
                        throw new Exception(string.Format("删除应用程序{0}结构出错", app.Name));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("插入日志表结构失败，错误消息为：" + ex.Message);
            }
        }

        public override void Execute()
        {
            InsertAppStructs();
        }

        public override void Undo()
        {
            RemoveAppStructs();
            RaiseUndoDoneEvent(new UndoRedoEventArg());
        }

        public override void Redo()
        {
            InsertAppStructs();
            RaiseRedoDoneEvent(new UndoRedoEventArg());
        }
    }
}
