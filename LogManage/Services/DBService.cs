using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;
using LogManage.LogServiceReference;
using LogManage.DataType.Rules;
using LogManage.DataType.Relations;

namespace LogManage.Services
{
    // 提供与数据库相关的操作，所有的数据库操作都必须经过这个类
    public class DBService:IManageRuleData
    {
        private LogManage.LogServiceReference.LogManageServiceSoapClient m_dbClient = null;
        private DBService()
        {
            m_dbClient = new LogServiceReference.LogManageServiceSoapClient();            
        }

        private static DBService m_instance = null;

        public static DBService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new DBService();
                }

                return m_instance;
            }
        }

        #region 日志列操作
        public List<LogColumn> ExistingLogColumns
        {
            get
            {
                return LogTypeConvert.FromLogColumnPluss(m_dbClient.GetExistingLogColumns());
            }
        }

        public Int32 MaxColumnItemColumn
        {
            get
            {               
                return m_dbClient.GetMaxColumnItemIndex();
            }
        }

        public void AddLogColumn(LogColumn column)
        {
            m_dbClient.AddLogColumn(LogTypeConvert.ToLogColumnPlus(column));
        }

        public void AddLogColumns(List<LogColumn> columns)
        {
            m_dbClient.AddLogColumns(LogTypeConvert.ToLogColumnPluss(columns));
        }

        public void RemoveColumn(int columnIndex)
        {
            m_dbClient.RemoveLogColumn(columnIndex);
        }

        public void RemoveColumns(List<int> columnIndexes)
        {
            ArrayOfInt ai = new ArrayOfInt();
            ai.AddRange(columnIndexes);
            m_dbClient.RemoveLogColumns(ai);
        }

        public bool ExistColumnIndex(int colIndex)
        {
            return m_dbClient.IsExistColumnIndex(colIndex);
        }
        #endregion

        #region table操作
        public bool AddTable(string appGuid,LogTable table)
        {
            return m_dbClient.AddTable(appGuid,LogTypeConvert.ToLogTablePlus(table));
        }

        public bool RemoveTable(string appGuid, string tableGuid)
        {
            return m_dbClient.RemoveTable(appGuid, tableGuid);
        }

        public bool ReNameTable(string appGuid, string tableGuid, string newName)
        {
            return m_dbClient.ReNameTable(appGuid, tableGuid, newName);
        }

        public List<LogTable> GetAppTables(string appGuid)
        {
            return LogTypeConvert.FromLogTablePluss(m_dbClient.GetAppTables(appGuid));
        }
        #endregion

        #region tableitem操作
        public bool AddTableItems(string appGuid,string tableGuid, List<LogTableItem> items)
        {
            return m_dbClient.AddTableItems(appGuid, tableGuid, LogTypeConvert.ToLogTableItemPluss(items));
        }

        public bool RemoveTableItems(string appGuid, string tableGuid,List<int> colIndexes) 
        {
            ArrayOfInt ai=new ArrayOfInt();
            ai.AddRange(colIndexes);
            return m_dbClient.RemoveTableItems(appGuid, tableGuid, ai);
        }

        public bool UpdateTableItemsSequence(string tableGuid,List<int> lstColIndexes)
        {
            ArrayOfInt ai = new ArrayOfInt();
            ai.AddRange(lstColIndexes);

            return m_dbClient.UpdateTableItemsSequence(tableGuid, ai);
        }

        public bool UpdateTableItemValue(string appGuid, string tableGuid, LogTableItem item)
        {
            return m_dbClient.UpdateTableItemValue(tableGuid, LogTypeConvert.ToLogTableItemPlus(item));
        }

        public List<LogTableItem> GetAppTableItems(string tableGuid)
        {
            return LogTypeConvert.FromLogTableItemPluss(m_dbClient.GetAppTableItems(tableGuid));
        }
        #endregion

        #region appgroup操作

        /// <summary>
        /// 添加应用程序分组集合
        /// </summary>
        /// <param name="appGroup"></param>
        public void AddGroupName(List<LogAppGroup> appGroups)
        {
            m_dbClient.AddAppGroups(LogTypeConvert.ToLogAppGroupPluss(appGroups));
        }

        public void ReNameGroupName(string oldGroupName, string newGroupName)
        {
            m_dbClient.ReNameAppGroup(oldGroupName, newGroupName);
        }

        public void ReNameGroupNames(List<LogAppGroup> oldGroupNames, List<LogAppGroup> newGroupNames)
        {
            m_dbClient.ReNameAppGroups(LogTypeConvert.ToLogAppGroupPluss(oldGroupNames),
                LogTypeConvert.ToLogAppGroupPluss(newGroupNames));
        }

        public void RemoveAppGroupNames(List<LogAppGroup> appGroups)
        {
            m_dbClient.RemoveAppGroupNames(LogTypeConvert.ToLogAppGroupPluss(appGroups));
        }

        public List<LogAppGroup> ExistingAppGroups
        {
            get
            {
                return LogTypeConvert.FromLogAppGroupPluss(m_dbClient.GetExistingAppGroups());
            }
        }
        #endregion

        #region app操作
        public bool AddApplication(LogApp app)
        {
            return m_dbClient.AddApplication(LogTypeConvert.ToLogAppPlus(app));
        }

        public bool ReNameApplication(string appGuid, string newName)
        {
            return m_dbClient.ReNameApplication(appGuid, newName);
        }

        public bool UpdateApplicationProperties(string appGuid, LogApp la)
        {
            return m_dbClient.UpdateApplicationPropertes(appGuid, LogTypeConvert.ToLogAppPlus(la));
        }

        public bool RemoveApplication(LogApp app)
        {
            return m_dbClient.RemoveApplication(app.AppGUID);
        }

        public List<LogApp> ExistingApps
        {
            get
            {
                List<LogApp> lstApps=LogTypeConvert.FromLogAppPluss(m_dbClient.GetExistingApps());

                foreach (LogApp la in lstApps)
                {
                    la.Tables.AddRange(GetAppTables(la.AppGUID));
                }

                return lstApps;
            }
        }
        #endregion       

        #region 日志内容操作
        public bool SaveAppLogs(string appGuid, string tableGuid, List<LogRecord> lstRecords)
        {
            return m_dbClient.SaveAppLogs(appGuid, tableGuid, LogTypeConvert.ToLogRecordPlus(lstRecords));
        }

        public List<LogRecord> GetAppLogs(string appGuid, string tableGuid, List<LogFilterCondition> lstConditions)
        {
            return LogTypeConvert.FromLogRecordPlus(appGuid,tableGuid,m_dbClient.GetAppLogs(appGuid,tableGuid,
                LogTypeConvert.ToLogFilterConditionPlus(lstConditions)));
        }

        public List<LogRecord> GetAppLogs(string appGuid, string tableGuid, List<string> lstRecordGuids)
        {
            ArrayOfString aos = new ArrayOfString();
            aos.AddRange(lstRecordGuids);

            return LogTypeConvert.FromLogRecordPlus(appGuid, tableGuid, m_dbClient.GetAppLogsByLogGuid(appGuid, tableGuid,
                aos));
        }
        #endregion

        #region 类型转换函数
        private static class LogTypeConvert
        {
            public static LogAppGroupNamePlus ToLogAppGroupPlus(LogAppGroup appGroup)
            {
                LogAppGroupNamePlus p = new LogAppGroupNamePlus();
                p.GroupName = appGroup.Name;

                return p;
            }

            public static LogAppGroup FromLogAppGroupPlus(LogAppGroupNamePlus plus)
            {
                return new LogAppGroup(plus.GroupName);
            }

            public static List<LogAppGroupNamePlus> ToLogAppGroupPluss(List<LogAppGroup> appGroups)
            {
                List<LogAppGroupNamePlus> lstGroups = new List<LogAppGroupNamePlus>();

                foreach (LogAppGroup p in appGroups)
                {
                    lstGroups.Add(ToLogAppGroupPlus(p));
                }

                return lstGroups;
            }

            public static List<LogAppGroup> FromLogAppGroupPluss(List<LogAppGroupNamePlus> pluss)
            {
                List<LogAppGroup> lstApps = new List<LogAppGroup>();

                foreach (LogAppGroupNamePlus p in pluss)
                {
                    lstApps.Add(FromLogAppGroupPlus(p));
                }

                return lstApps;
            }

            public static LogAppPlus ToLogAppPlus(LogApp app)
            {
                LogAppPlus p = new LogAppPlus();
                p.AppGUID = app.AppGUID;
                p.Name = app.Name;
                p.IsImportLogsFromFile = app.IsImportLogsFromFiles;
                p.AppGroupName = app.Group.Name;

                return p;
            }

            public static LogApp FromLogAppPlus( LogAppPlus plus)
            {
                LogApp app = new LogApp(plus.AppGUID);
                app.Name = plus.Name;
                app.IsImportLogsFromFiles = plus.IsImportLogsFromFile;
                app.Group = new LogAppGroup(plus.AppGroupName);

                return app;
            }

            public static List<LogApp> FromLogAppPluss(List<LogAppPlus> pluss)
            {
                List<LogApp> lstApps = new List<LogApp>();

                foreach (LogAppPlus p in pluss)
                {
                    lstApps.Add(FromLogAppPlus(p));
                }

                return lstApps;
            }

            public static LogColumnPlus ToLogColumnPlus(LogColumn col)
            {
                LogColumnPlus p = new LogColumnPlus();
                p.Index = col.Index;
                p.Name = col.Name;
                p.Type = col.Type;

                return p;
            }

            public static LogColumn FromLogColumnPlus(LogColumnPlus pCol)
            {
                LogColumn col = new LogColumn();
                col.Type = pCol.Type;
                col.Name = pCol.Name;
                col.Index = pCol.Index;

                return col;
            }

            public static List<LogColumn> FromLogColumnPluss(List<LogColumnPlus> pluss)
            {
                List<LogColumn> lstCols = new List<LogColumn>();

                foreach (LogColumnPlus p in pluss)
                {
                    lstCols.Add(FromLogColumnPlus(p));
                }

                return lstCols;
            }

            public static List<LogColumnPlus> ToLogColumnPluss(List<LogColumn> pluss)
            {
                List<LogColumnPlus> lstCols = new List<LogColumnPlus>();

                foreach (LogColumn p in pluss)
                {
                    lstCols.Add(ToLogColumnPlus(p));
                }

                return lstCols;
            }

            public static LogTablePuls ToLogTablePlus(LogTable t)
            {
                LogTablePuls p = new LogTablePuls();
                p.GUID = t.GUID;
                p.Name = t.Name;
                
                return p;
            }

            public static LogTable FromLogTablePlus(LogTablePuls t)
            {
                LogTable p = new LogTable(t.GUID);
                p.Name = t.Name;

                if (t.Columns != null && t.Columns.Count > 0)
                {
                    foreach (LogTableItemPlus lt in t.Columns)
                    {
                        p.Columns.Add(FromLogTableItemPlus(lt));
                    }
                }
                
                return p;
            }

            public static List<LogTable> FromLogTablePluss(List<LogTablePuls> pluss)
            {
                List<LogTable> lstApps = new List<LogTable>();

                foreach (LogTablePuls p in pluss)
                {
                    lstApps.Add(FromLogTablePlus(p));
                }

                return lstApps;
            }

            public static LogTableItem FromLogTableItemPlus(LogTableItemPlus t)
            {
                LogTableItem p = new LogTableItem();
                p.SeqIndex = t.SeqIndex;
                p.LogColumnIndex = t.LogColumnIndex;
                p.IsFilterColumn = t.IsFilterColumn;
                p.Visible = t.Visible;
                p.NickName = t.NickName;
                
                return p;
            }

            public static LogTableItemPlus ToLogTableItemPlus(LogTableItem t)
            {
                LogTableItemPlus p = new LogTableItemPlus();
                p.SeqIndex = t.SeqIndex;
                p.LogColumnIndex = t.LogColumnIndex;
                p.IsFilterColumn = t.IsFilterColumn;
                p.NickName = t.NickName;
                p.Visible = t.Visible;

                return p;
            }

            public static List<LogTableItemPlus> ToLogTableItemPluss(List<LogTableItem> pluss)
            {
                List<LogTableItemPlus> lstCols = new List<LogTableItemPlus>();

                foreach (LogTableItem p in pluss)
                {
                    lstCols.Add(ToLogTableItemPlus(p));
                }

                return lstCols;
            }

            public static List<LogTableItem> FromLogTableItemPluss(List<LogTableItemPlus> pluss)
            {
                List<LogTableItem> lstCols = new List<LogTableItem>();

                foreach (LogTableItemPlus p in pluss)
                {
                    lstCols.Add(FromLogTableItemPlus(p));
                }

                return lstCols;
            }

            public static LogRecord FromLogRecordPlus(string appGuid,string tableGuid,LogRecordPlus p)
            {
                LogRecord record = LogRecord.CreateNewLogRecord(appGuid, tableGuid, p.RecordGuid);

                if (p.RecordItems != null)
                {
                    foreach (LogRecordItemPlus plus in p.RecordItems)
                    {
                        record.Items.Add(plus.ColumnIndex, new LogRecordItem(plus.ColumnIndex, plus.Content));
                    }
                }

                return record;
            }

            public static List<LogRecord> FromLogRecordPlus(string appGuid, string tableGuid, List<LogRecordPlus> lstRecords)
            {
                List<LogRecord> lstResults = new List<LogRecord>();

                foreach (LogRecordPlus plus in lstRecords)
                {
                    lstResults.Add(FromLogRecordPlus(appGuid, tableGuid, plus));
                }

                return lstResults;
            }

            public static LogRecordPlus ToLogRecordPlus(LogRecord record)
            {
                LogRecordPlus recordPlus = new LogRecordPlus();
                recordPlus.RecordGuid = record.RecordGuid;
                recordPlus.RecordItems = new List<LogRecordItemPlus>();

                foreach (LogRecordItem lri in record.Items.Values)
                {
                    LogRecordItemPlus itemPlus = new LogRecordItemPlus();
                    itemPlus.ColumnIndex = lri.ColumnIndex;
                    itemPlus.Content = lri.Conetent;

                    recordPlus.RecordItems.Add(itemPlus);
                }

                return recordPlus;
            }

            public static List<LogRecordPlus> ToLogRecordPlus(List<LogRecord> lstRecords)
            {
                List<LogRecordPlus> lstResults = new List<LogRecordPlus>();

                foreach (LogRecord lr in lstRecords)
                {
                    lstResults.Add(ToLogRecordPlus(lr));
                }

                return lstResults;
            }

            public static List<LogFilterConditionPlus> ToLogFilterConditionPlus(List<LogFilterCondition> lstConditions)
            {
                List<LogFilterConditionPlus> plusCondition = new List<LogFilterConditionPlus>();

                foreach (LogFilterCondition condition in lstConditions)
                {
                    LogFilterConditionPlus plus = new LogFilterConditionPlus();
                    plus.ColumnIndex = condition.ColumnIndex;
                    plus.Content = condition.Content;
                    plus.RightBound = condition.RightBound;
                    plus.Relation = condition.Relation;
                    plus.LeftBound = condition.LeftBound;
                    plus.Type = condition.Type;

                    plusCondition.Add(plus);
                }

                return plusCondition;
            }

            public static List<SecurityActionResult> ToSecurityActionResults(List<RuleActionResultPlus> lstInitResults)
            {
                List<SecurityActionResult> lstDestResults = new List<SecurityActionResult>();

                foreach (RuleActionResultPlus rp in lstInitResults)
                {
                    SecurityActionResult result = SecurityActionResult.CreateSecurityActionResult(
                        rp.Desc, rp.Guid, rp.BgColor);

                    lstDestResults.Add(result);
                }

                return lstDestResults;
            }

            public static List<RuleActionResultPlus> FromSecurityActionResults(List<SecurityActionResult> lstInitResults)
            {
                List<RuleActionResultPlus> lstDestResults = new List<RuleActionResultPlus>();

                foreach (SecurityActionResult rp in lstInitResults)
                {
                    RuleActionResultPlus result = new RuleActionResultPlus();
                    result.BgColor = rp.BackgroundColor;
                    result.Desc = rp.Description;
                    result.Guid = rp.ResultGuid;

                    lstDestResults.Add(result);
                }

                return lstDestResults;
            }

            public static SecurityEvent ToSecurityEvent(RuleEvenPlus rep)
            {
                SecurityEvent se = new SecurityEvent();
                se.Description = rep.Desc;
                se.EventGuid = rep.Guid;
                se.Name = rep.Name;

                if (rep.Actions != null && rep.Actions.Count > 0)
                {
                    foreach (RuleActionPlus rap in rep.Actions)
                    {
                        se.SecurityActions.Add(ToSecurityAction(rap));
                    }
                }

                return se;
            }

            public static List<SecurityEvent> ToSecurityEvents(List<RuleEvenPlus> reps)
            {
                List<SecurityEvent> ses = new List<SecurityEvent>();

                foreach (RuleEvenPlus rep in reps)
                {
                    ses.Add(ToSecurityEvent(rep));
                }

                return ses;
            }

            public static RuleEvenPlus FromSecurityEvent(SecurityEvent se)
            {
                RuleEvenPlus rep = new RuleEvenPlus();
                rep.Desc = se.Description;
                rep.Guid = se.EventGuid;
                rep.Name = se.Name;

                if (se.SecurityActions.Count > 0)
                {
                    rep.Actions = new List<RuleActionPlus>();

                    foreach(SecurityAction sa in se.SecurityActions)
                    {
                        rep.Actions.Add(FromSecurityAction(sa));
                    }
                }

                return rep;
            }

            public static SecurityAction ToSecurityAction(RuleActionPlus rap)
            {
                SecurityAction sa = new SecurityAction();
                sa.ActionGuid = rap.Guid;
                sa.Description = rap.Desc;
                sa.Name = rap.Name;
                sa.ResultGuid = rap.ResultGuid;

                if (rap.Conditions!=null && rap.Conditions.Count > 0)
                {
                    foreach (RuleConditionPlus rcp in rap.Conditions)
                    {
                        sa.Conditions.Add(ToSecurityCondition(rcp));
                    }
                }

                return sa;
            }

            public static RuleActionPlus FromSecurityAction(SecurityAction sa)
            {
                RuleActionPlus rap = new RuleActionPlus();
                rap.Desc = sa.Description;
                rap.Guid = sa.ActionGuid;
                rap.Name = sa.Name;
                rap.ResultGuid = sa.ResultGuid;
                rap.Sequence = 0;

                if (sa.Conditions.Count > 0)
                {
                    rap.Conditions = new List<RuleConditionPlus>();

                    foreach (SecurityCondition condition in sa.Conditions)
                    {
                        rap.Conditions.Add(FromSecurityCondition(condition));
                    }
                }

                return rap;
            }

            public static SecurityCondition ToSecurityCondition(RuleConditionPlus rcp)
            {
                SecurityCondition conditon = new SecurityCondition();                
                conditon.DestinationCol = rcp.ColIndexDest;
                conditon.IsUsingDestCol = rcp.IsUsingDestCol;
                conditon.RelationName = rcp.Relation;
                conditon.SourceCol = rcp.ColIndexSrc;
                conditon.ConditionGuid = rcp.Guid;

                IRelation relation = RelationService.Instance.GetRelation(conditon.RelationName);

                if (relation.ParamsCount == 1)
                {
                    conditon.SetContent(rcp.Condition);
                }
                else
                {
                    string[] datas = rcp.Condition.Split(";".ToCharArray());
                    conditon.SetMultiValues(new List<string>(datas));
                }              

                return conditon;
            }

            public static RuleConditionPlus FromSecurityCondition(SecurityCondition rc)
            {
                RuleConditionPlus rcp = new RuleConditionPlus();
                rcp.ColIndexDest = rc.DestinationCol;
                rcp.ColIndexSrc = rc.SourceCol;                
                rcp.Desc = string.Empty;
                rcp.Guid = rc.ConditionGuid;
                rcp.IsUsingDestCol = rc.IsUsingDestCol;
                rcp.Relation = rc.RelationName;

                IRelation relation = RelationService.Instance.GetRelation(rc.RelationName);

                if (relation.ParamsCount == 1)
                {
                    rcp.Condition = rc.GetContent();
                }
                else
                {
                    StringBuilder sb=new StringBuilder();
                    for (int i = 0; i < rc.MultiValues.Count; i++)
                    {
                        sb.Append(rc.MultiValues[i]);

                        if (i != rc.MultiValues.Count - 1)
                        {
                            sb.Append(";");
                        }
                    }

                    rcp.Condition = sb.ToString();
                }              
                
                
                return rcp;
            }
        }
        #endregion

        #region 规则操作函数

        /// <summary>
        /// 获取用户定义的用户行为结果集合
        /// </summary>
        public List<SecurityActionResult> GetDefinedActionResults()
        {
            return LogTypeConvert.ToSecurityActionResults(m_dbClient.GetDefinedActionResults());
        }

        public void UpdateResults(List<SecurityActionResult> results)
        {
            m_dbClient.UpdateResults(LogTypeConvert.FromSecurityActionResults(results));
        }

        public void InsertResults(List<SecurityActionResult> results)
        {
            m_dbClient.InsertResults(LogTypeConvert.FromSecurityActionResults(results));
        }

        public void DeleteResults(List<string> lstGuids)
        {
            ArrayOfString aos=new ArrayOfString();
            aos.AddRange(lstGuids);

            m_dbClient.DeleteResults(aos);
        }

        public string GetRulesOfUsingTheResult(string resultGuid)
        {
            return m_dbClient.GetRulesOfUsingTheResult(resultGuid);
        }

        #endregion

        #region IManageRuleData Members

        public List<SecurityEvent> GetDefinedSecurityEvents()
        {
            return LogTypeConvert.ToSecurityEvents(m_dbClient.GetDefinedSecurityEvents());
        }

        public void CreateNewSecurityEvent(SecurityEvent se)
        {
            m_dbClient.CreateNewSecurityEvent(LogTypeConvert.FromSecurityEvent(se));
        }

        public void UpdateSecurityEventProperties(SecurityEvent se)
        {
            m_dbClient.UpdateSecurityEventProperties(LogTypeConvert.FromSecurityEvent(se));
        }

        public void DeleteSecurityEvent(string eventGuid)
        {
            m_dbClient.DeleteSecurityEvent(eventGuid);
        }

        #endregion

        #region IManageRuleData Members


        public void CreateNewSecurityAction(string eventGuid, SecurityAction sa)
        {
            m_dbClient.CreateNewActionOfEvent(eventGuid, LogTypeConvert.FromSecurityAction(sa));
        }

        public void UpdateSecurityActionProperties(string eventGuid, SecurityAction sa)
        {
            m_dbClient.UpdateActionOfEvent(eventGuid, LogTypeConvert.FromSecurityAction(sa));
        }

        public void DeleteSecurityAction(string eventGuid, string actionGuid)
        {
            m_dbClient.DeleteActionOfEvent(eventGuid, actionGuid);
        }

        public void CreateNewSecurityCondition(string actionGuid, SecurityCondition sc)
        {
            m_dbClient.CreateNewConditionOfAction(actionGuid, LogTypeConvert.FromSecurityCondition(sc));
        }

        public void UpdateSecurityConditionProperties(string actionGuid, SecurityCondition sc)
        {
            m_dbClient.UpdateConditionOfAction(actionGuid, LogTypeConvert.FromSecurityCondition(sc));
        }

        public void DeleteSecurityCondition(string actionGuid, string conditionGuid)
        {
            m_dbClient.DeleteConditionOfAction(actionGuid, conditionGuid);
        }

        public void DeleteSecurityConditions(string actionGuid, List<string> conditionGuids)
        {
            ArrayOfString aos=new ArrayOfString();
            aos.AddRange(conditionGuids);

            m_dbClient.DeleteConditionsOfAction(actionGuid,aos);
        }

        #endregion

        #region IManageRuleData Members


        public List<LogColumn> GetDefinedColumns()
        {
            return ExistingLogColumns;
        }

        #endregion
    }    
}
