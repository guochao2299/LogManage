using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;

namespace LogManage.Services
{
    /// <summary>
    /// 提供关于应用程序的一些服务，这里的应用程序是指存在日志的应用程序
    /// </summary>
    public class AppService
    {
        private static AppService m_instance = null;

        public static AppService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new AppService();
                }

                return m_instance;
            }
        }

        private Dictionary<string, LogApp> m_existingApps = null;        

        private void InitApps()
        {
 
        }

        public LogApp GetApp(string guid)
        {
            if (!m_existingApps.ContainsKey(guid))
            {
                throw new Exception("要获取的应用程序不存在");
            }

            return m_existingApps[guid];
        }

        public Dictionary<string, LogApp> ExistingApps
        {
            get
            {
                if (m_existingApps == null)
                {
                    m_existingApps = new Dictionary<string, LogApp>();

                    foreach (LogApp la in DBService.Instance.ExistingApps)
                    {
                        m_existingApps.Add(la.AppGUID, la);
                    }
                }                

                return m_existingApps;
            }
        }

        public bool IsAppExist(string appGuid)
        {
            return ExistingApps.ContainsKey(appGuid);
        }

        public bool AddApp(LogApp app)
        {
            if(m_existingApps.ContainsKey(app.AppGUID))
            {
                throw new Exception("该应用程序已经存在，不能重复新建!");
            }
                       

            if (DBService.Instance.AddApplication(app))
            {
                m_existingApps.Add(app.AppGUID, (LogApp)app.Clone());
                return true;
            }

            return false;
        }

        public bool RemoveApp(LogApp app)
        {
            if (!m_existingApps.ContainsKey(app.AppGUID))
            {
                throw new Exception(string.Format("要删除的应用程序{0}不存在", app.Name));
            }

            if(DBService.Instance.RemoveApplication(app))
            {
                m_existingApps.Remove(app.AppGUID);
                return true;
            }

            return false;
        }

        public bool ReNameApp(string appGuid, string newName)
        {
            if (DBService.Instance.ReNameApplication(appGuid, newName))
            {
                m_existingApps[appGuid].Name = newName;
                return true;
            }

            return false;
        }

        public bool UpdateAppProperties(string appGuid, LogAppMemento app)
        {
            LogApp la = LogApp.NullApplication;
            la.Name = app.LogAppName;
            la.IsImportLogsFromFiles = app.IsImportLogsFromFile;

            if (DBService.Instance.UpdateApplicationProperties(appGuid, la))
            {
                m_existingApps[appGuid].Name = app.LogAppName;
                m_existingApps[appGuid].IsImportLogsFromFiles = app.IsImportLogsFromFile;
                return true;
            }

            return false;
        }

        public bool UpdateAppProperties(string appGuid, LogApp app)
        {
            if (DBService.Instance.UpdateApplicationProperties(appGuid, app))
            {
                m_existingApps[appGuid].Name = app.Name;
                m_existingApps[appGuid].IsImportLogsFromFiles = app.IsImportLogsFromFiles;
                return true;
            }

            return false;
        }

        public LogTable GetAppTable(string appGuid,string tableGuid)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要获取的日志结构所属的应用程序不存在");
            }

            return m_existingApps[appGuid].GetTable(tableGuid);
        }

        #region appgroup
        private Dictionary<string, LogAppGroup> m_appGroupNames = null;

        public Dictionary<string, LogAppGroup> ExistingAppGroups
        {
            get
            {
                if (m_appGroupNames == null)
                {
                    m_appGroupNames = new Dictionary<string, LogAppGroup>();

                    foreach (LogAppGroup la in DBService.Instance.ExistingAppGroups)
                    {
                        m_appGroupNames.Add(la.Name, la);
                    }
                }

                return m_appGroupNames;
            }
        }

        /// <summary>
        /// 是否寻在应用程序分组名称
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool IsAppGroupNameExist(string groupName)
        {
            return m_appGroupNames.ContainsKey(groupName);
        }

        public void ReNameGroupNames(List<LogAppGroup> oldGroupNames, List<LogAppGroup> newGroupNames)
        {
            DBService.Instance.ReNameGroupNames(oldGroupNames,
                newGroupNames);

            for(int i=0;i<oldGroupNames.Count;i++)
            {
                if (m_appGroupNames.ContainsKey(oldGroupNames[i].Name))
                {
                    m_appGroupNames.Remove(oldGroupNames[i].Name);
                }

                m_appGroupNames.Add(newGroupNames[i].Name, oldGroupNames[i]);

                foreach (LogApp la in m_existingApps.Values)
                {
                    if(string.Equals(la.Group.Name,oldGroupNames[i].Name))
                    {
                        la.Group=newGroupNames[i];
                    }
                }
            }
        }

        public void RemoveAppGroupNames(List<LogAppGroup> appGroups)
        {
            DBService.Instance.RemoveAppGroupNames(appGroups);

            foreach (LogAppGroup lag in appGroups)
            {
                if (m_appGroupNames.ContainsKey(lag.Name))
                {
                    m_appGroupNames.Remove(lag.Name);
                }
            }
        }

        public void AddGroupName(List<LogAppGroup> appGroups)
        {
            DBService.Instance.AddGroupName(appGroups);

            foreach (LogAppGroup lag in appGroups)
            {
                m_appGroupNames.Add(lag.Name,lag);
            }
        }

        /// <summary>
        /// /应用程序分组名称中是否存在应用程序
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool IsAppGroupHasApps(string groupName)
        {
            return true;
        }
        #endregion

        public List<LogTable> GetAppTables(string appGuid)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要获取的日志结构所属的应用程序不存在");
            }

            return m_existingApps[appGuid].Tables;
        }

        /// <summary>
        /// 遍历应用程序，检查应用程序的Guid是否存在，如果存在，返回table所在应用程序名称+表名称，不存在返回empty
        /// </summary>
        /// <param name="tableGuid"></param>
        /// <returns></returns>
        public string IsTableExist(string tableGuid)
        {
            foreach (LogApp app in ExistingApps.Values)
            {
                if(app.IsExistingTable(tableGuid))
                {
                    return app.Name+"："+app.GetTable(tableGuid).Name;
                }
            }

            return string.Empty;
        }

        public bool AddTable(string appGuid,LogTable table)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要添加的日志结构所属的应用程序不存在");
            }

            if (DBService.Instance.AddTable(appGuid,table))
            {
                m_existingApps[appGuid].AddTable((LogTable)table.Clone());
                return true;
            }

            return false;
        }

        public bool RemoveTable(string appGuid,string tableGuid)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要删除的日志结构所属的应用程序不存在");
            }

            if (DBService.Instance.RemoveTable(appGuid,tableGuid))
            {
                m_existingApps[appGuid].RemoveTable(tableGuid);
                return true;
            }

            return false;
        }

        public bool ReNameTable(string appGuid, string tableGuid, string newName)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要重命名的日志结构所属的应用程序不存在");
            } 
            if (DBService.Instance.ReNameTable(appGuid,tableGuid, newName))
            {
                m_existingApps[appGuid].ReNameTable(tableGuid, newName);
                return true;
            }

            return false;
        }

        public List<LogTableItem> GetAppTableItems(string appGuid, string tableGuid)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要获取的日志结构所属的应用程序不存在");
            }

            return m_existingApps[appGuid].GetTable(tableGuid).Columns;
        }

        public LogTableItem GetAppTableItem(string appGuid, string tableGuid,int colIndex)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要获取的日志结构列所属的应用程序不存在");
            }

            return m_existingApps[appGuid].GetTable(tableGuid).GetColumn(colIndex);
        }

        public bool UpdateTableItemValue(string appGuid, string tableGuid,LogTableItem item)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要更新的日志结构所属的应用程序不存在");
            }

            if (DBService.Instance.UpdateTableItemValue(appGuid, tableGuid, item))
            {
                m_existingApps[appGuid].GetTable(tableGuid).GetColumn(item.LogColumnIndex).CopyFrom(item);
                return true;
            }

            return false;
        }

        public bool AddTableItems(string appGuid, string tableGuid, List<LogTableItem> items)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要添加的日志结构列所属的应用程序不存在");
            }

            if (DBService.Instance.AddTableItems(appGuid, tableGuid, items))
            {
                m_existingApps[appGuid].GetTable(tableGuid).AddColumns(items);
                return true;
            }

            return false;
        }

        public bool RemoveTableItems(string appGuid, string tableGuid, List<int> colIndexes)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要删除的日志结构列所属的应用程序不存在");
            }

            if (DBService.Instance.RemoveTableItems(appGuid, tableGuid, colIndexes))
            {
                m_existingApps[appGuid].GetTable(tableGuid).RemoveColumns(colIndexes);
                return true;
            }

            return false;
        }

        public bool UpdateTableItemsSequence(string appGuid, string tableGuid, List<int> lstColIndexes)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要更新的日志结构列顺序所属的应用程序不存在");
            }

            if (DBService.Instance.UpdateTableItemsSequence(tableGuid,lstColIndexes))
            {
                m_existingApps[appGuid].GetTable(tableGuid).UpdateSequence(lstColIndexes);
                return true;
            }

            return false;
        }

        private void ExchageTableItem(LogTable table, int firstIndex, int secondIndex)
        {
            LogTableItem lti = (LogTableItem)table.Columns[firstIndex].Clone();

            table.Columns[firstIndex].CopyFrom(table.Columns[secondIndex]);
            table.Columns[secondIndex].CopyFrom(lti);
        }

        public bool ExchangeTableItemsSequence(string appGuid, string tableGuid, int firstIndex, int secondIndex)
        {
            if (!m_existingApps.ContainsKey(appGuid))
            {
                throw new Exception("要更新的日志结构列顺序所属的应用程序不存在");
            }

            LogTable table = GetAppTable(appGuid, tableGuid);
            ExchageTableItem(table, firstIndex, secondIndex);

            try
            {
                return UpdateTableItemsSequence(appGuid, tableGuid, table.ColIndexesASC);
            }
            catch (Exception ex)
            {
                ExchageTableItem(table, secondIndex, firstIndex);

                throw new Exception("交换日志列顺序失败，错误消息为：" + ex.Message);
            }
        }
    }
}
