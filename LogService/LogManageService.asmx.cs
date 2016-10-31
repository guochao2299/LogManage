using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

using LogManage.DataType.Relations;

namespace LogService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LogManageService : System.Web.Services.WebService
    {
        private const string TableColumnPrefix = "col";
        private const string TablePrefix = "tbl";

        // 每个表的第一列
        private const string FirstColumnOfTableColumn = "colGUID";
             

        #region 参数操作
        
        [WebMethod(Description="获取指定参数的值，如果没有则返回默认值")]        
        public string GetParamValue(string paraName, string defaultValue)
        {
            try
            {
                LogDataContext logContext = new LogDataContext();

                if (!logContext.SystemParams.Any(r => string.Compare(r.Name, paraName, true) == 0))
                {
                    return defaultValue;
                }

                return logContext.SystemParams.First(r => string.Compare(r.Name, paraName, true) == 0).Value;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("获取参数{0}的值失败，错误消息为：" + ex.Message, paraName), ex);
            }            
        }

        [WebMethod(Description = "设置指定参数的值")]
        public void SetParamValue(string paraName, string value)
        {
            try
            {
                LogDataContext logContext = new LogDataContext();
                SetParamValue(logContext, paraName, value);
                logContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }        

        private void SetParamValue(LogDataContext logContext,string paraName, string value)
        {
            try
            {
                if (!logContext.SystemParams.Any(r => string.Compare(r.Name, paraName, true) == 0))
                {
                    SystemParam sp=new SystemParam();
                    sp.Value=value;
                    sp.Name=paraName;
                    logContext.SystemParams.InsertOnSubmit(sp);
                }
                else
                {

                    IEnumerable<SystemParam> lstDatas = from g in logContext.SystemParams
                                                        where string.Compare(g.Name, paraName, true) == 0
                                                        select g;

                    foreach (SystemParam sp in lstDatas)
                    {
                        sp.Value = value;
                    }        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("设置参数{0}的值失败，错误消息为：" + ex.Message, paraName), ex);
            }
        }        

        [WebMethod(Description="获取日志列的最大索引号")]
        public int GetMaxColumnItemIndex()
        {
            return Convert.ToInt32(GetParamValue(MaxColumnIndexName, "0"));
        }
    
        #endregion

        #region 日志列操作

        [WebMethod(Description = "获取所有已定义的日志列，不包括标记为删除的项")]
        public List<LogColumnPlus> GetExistingLogColumns()
        {
            try
            {
                LogDataContext context=new LogDataContext();

                IEnumerable<Column> result = from g in context.Columns
                                             where !g.IsRemoved
                                             select g;

                List<LogColumnPlus> lstDatas = new List<LogColumnPlus>();

                foreach (Column c in result)
                {
                    LogColumnPlus lc = new LogColumnPlus();
                    lc.Index = c.ColIndex;
                    lc.Name = c.Name;
                    lc.Type = c.Type;

                    lstDatas.Add(lc);
                }

                return lstDatas;
            }
            catch (Exception ex)
            {
                throw new Exception("获取全部已注册的日志列失败，错误消息为：" + ex.Message);
            }
        }

        private Column GetLogColumnByColIndex(int colIndex)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<Column> result = from g in context.Columns
                                             where !g.IsRemoved && g.ColIndex==colIndex
                                             select g;

                if (result.Count() <= 0)
                {
                    throw new Exception("找不到指定列信息");
                }

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("获取指定日志列"+colIndex.ToString()+"失败，错误消息为：" + ex.Message);
            }
        }

        private Column Convert2Column(LogColumnPlus lc)
        {
            Column c = new Column();
            c.IsRemoved = false;
            c.ColIndex = lc.Index;
            c.Name = lc.Name;
            c.Type = lc.Type;

            return c;
        }

        /// <summary>
        /// 在参数表中，日志列的最大索引号对应的名称
        /// </summary>
        private const string MaxColumnIndexName = "MaxColumnItemIndex";

        [WebMethod(Description = "添加日志列，出错抛异常")]
        public void AddLogColumn(LogColumnPlus lc)
        {
            try
            {
                LogDataContext context = new LogDataContext();
                context.Columns.InsertOnSubmit(Convert2Column(lc));

                int maxIndex = GetMaxColumnItemIndex();
                maxIndex++;
                SetParamValue(context,MaxColumnIndexName, maxIndex.ToString());
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("添加日志列出错，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "添加日志列集合，出错抛异常")]
        public void AddLogColumns(List<LogColumnPlus> lcs)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                List<Column> lstColumns = new List<Column>();

                int maxIndex = GetMaxColumnItemIndex();

                foreach (LogColumnPlus lc in lcs)
                {
                    lstColumns.Add(Convert2Column(lc));

                    maxIndex++;
                }

                context.Columns.InsertAllOnSubmit(lstColumns);

                SetParamValue(context, MaxColumnIndexName, maxIndex.ToString());
                
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("添加日志列出错，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除日志列，出错抛异常")]
        public void RemoveLogColumn(int colIndex)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<Column> lstResult = from g in context.Columns
                                                where g.ColIndex == colIndex
                                                select g;

                if (lstResult == null)
                {
                    return;
                }

                context.Columns.DeleteAllOnSubmit(lstResult);

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志列出错，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除日志列集合，出错抛异常")]
        public void RemoveLogColumns(List<int> lcs)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<Column> lstResult = from g in context.Columns
                                                where lcs.Contains(g.ColIndex )
                                                select g;

                if (lstResult == null)
                {
                    return;
                }

                // 这里是否需要检测有表格使用此列
                // 如果有，是否删除表格中的此列

                context.Columns.DeleteAllOnSubmit(lstResult);

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志列集合出错，错误消息为：" + ex.Message);
            }
        }
        
        [WebMethod(Description = "检测日志列号是否存在，出错抛异常")]
        public bool IsExistColumnIndex(int colIndex)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                return context.Columns.Any(r => r.ColIndex == colIndex);
            }
            catch (Exception ex)
            {
                throw new Exception("检测日志列号是否存在出错，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 日志表操作

        private bool IsExistTable(LogDataContext context,string tableName)
        {
            try
            {
                return context.pr_IsTableExist(tableName) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("检索表格{0}是否存在失败，错误消息为：{1}", tableName, ex.Message));
            }
        }

        private bool IsExistColumnOfTable(LogDataContext context,string tableName, string colName)
        {
            try
            {
                return context.pr_IsColumnOfTableExist(tableName,colName) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("检索表格{0}中列{1}是否存在失败，错误消息为：{2}", tableName,colName,ex.Message));
            }
        }

        [WebMethod(Description = "为指定的应用程序添加日志表,这时不会创建一张表，而是在表中添加列时创建表")]
        public bool AddTable(string appGuid, LogTablePuls table)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                Table t = new Table();
                t.GUID = table.GUID;
                t.IsRemoved = false;
                t.Name = table.Name;
                t.Owner = appGuid;

                context.Tables.InsertOnSubmit(t);
                context.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("添加添加日志表操作失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除指定的应用程的指定日志表,目前暂时没有删除日志表对应的真实记录")]
        public bool RemoveTable(string appGuid, string tableGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<Table> lstResult = from g in context.Tables
                                               where string.Compare(appGuid, g.Owner, true) == 0 &&
                                                     string.Compare(tableGuid, g.GUID, true) == 0
                                               select g;

                if (lstResult == null)
                {
                    return true;
                }

                StringBuilder sb=new StringBuilder();

                foreach(Table t in lstResult)
                {
                    if (IsExistTable(context,CreateTableName(t.GUID)))
                    {
                        context.ExecuteCommand("Drop table " +CreateTableName(t.GUID));
                    }
                 }

                context.Tables.DeleteAllOnSubmit(lstResult);
                context.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志表失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "重命名指定的应用程的指定日志表的名称")]
        public bool ReNameTable(string appGuid, string tableGuid, string newName)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<Table> lstResult = from g in context.Tables
                                               where string.Compare(appGuid, g.Owner, true) == 0 &&
                                                     string.Compare(tableGuid, g.GUID, true) == 0
                                               select g;

                if (lstResult == null)
                {
                    return false;
                }

                foreach (Table t in lstResult)
                {
                    t.Name = newName;
                }
                
                context.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("重命名日志表失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "获取指定的应用程序的所有日志表")]
        public List<LogTablePuls> GetAppTables(string appGuid)
        {
            try
            {
                List<LogTablePuls> lstTables = new List<LogTablePuls>();

                LogDataContext context = new LogDataContext();

                IEnumerable<Table> lstResult = from g in context.Tables
                                               where string.Compare(appGuid, g.Owner, true) == 0 &&
                                                        !g.IsRemoved
                                               select g;

                if (lstResult == null)
                {
                    return lstTables;
                }

                foreach (Table t in lstResult)
                {
                    LogTablePuls lt = new LogTablePuls();
                    lt.Name = t.Name;
                    lt.GUID = t.GUID;
                    lt.Columns = new List<LogTableItemPlus>();
                    lt.Columns.AddRange(GetAppTableItems(t.GUID));

                    lstTables.Add(lt);
                }

                return lstTables;
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志表失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 日志表列操作

        private string CreateTableClumnName(int colIndex)
        {
            return string.Format("{0}{1}", TableColumnPrefix, colIndex);
        }

        private string CreateTableName(string tableGuid)
        {
            return string.Format("{0}{1}", TablePrefix, tableGuid.Replace('-', '_'));
        }

        [WebMethod(Description = "向指定的应用程序的日志表中添加日志表列")]
        public bool AddTableItems(string appGuid, string tableGuid, List<LogTableItemPlus> items)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<TableStruct> lstTableStructs = from g in context.TableStructs
                                                      where string.Compare(g.TableGUID, tableGuid, true) == 0
                                                      orderby g.SeqIndex ascending
                                                      select g;

                int maxSeq = 0;

                if (lstTableStructs.Count() > 0)
                {
                    maxSeq = lstTableStructs.First().SeqIndex;

                    int lastIndex = lstTableStructs.LastOrDefault().SeqIndex;

                    if (maxSeq < lastIndex)
                    {
                        maxSeq = lastIndex;
                    }

                    maxSeq++;
                } 
                
                List<TableStruct> lstColumns = new List<TableStruct>();

                foreach (LogTableItemPlus item in items)
                {
                    TableStruct ts = new TableStruct();
                    ts.ColIndex = item.LogColumnIndex;
                    ts.IsFilterColumn = item.IsFilterColumn;
                    ts.SeqIndex = maxSeq;
                    ts.NickName = item.NickName;
                    ts.Visible = item.Visible;
                    ts.TableGUID = tableGuid;

                    lstColumns.Add(ts);

                    maxSeq++;
                }

                context.TableStructs.InsertAllOnSubmit(lstColumns);

                if (items.Count > 0)
                {
                    // 如果不存在表，则建立一个仅包含Guid列的日志表
                    if (!IsExistTable(context, CreateTableName(tableGuid)))
                    {
                        Column c =GetLogColumnByColIndex(items[0].LogColumnIndex);
                        context.ExecuteCommand(string.Format("CREATE TABLE {0} ({1} varchar(50) PRIMARY KEY NOT NULL)", CreateTableName(tableGuid), FirstColumnOfTableColumn));                        
                    }


                    for (int i = 0; i < items.Count; i++)
                    {
                        Column c = GetLogColumnByColIndex(items[i].LogColumnIndex);
                        context.ExecuteCommand(string.Format("ALTER TABLE {0} ADD {1} {2} NULL", CreateTableName(tableGuid), CreateTableClumnName(c.ColIndex), TableColumnTypeService.Instance.GetSqlTypeOfTableColumn(c.Type)));
                    }
                }

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("添加日志列失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除指定的应用程序的日志表中的日志表列")]
        public bool RemoveTableItems(string appGuid, string tableGuid, List<int> colIndexes)
        {
            try
            {
                if (colIndexes.Count <= 0)
                {
                    return true;
                }

                LogDataContext context = new LogDataContext();

                int totalColumns = (from g in context.TableStructs
                                    where string.Compare(tableGuid, g.TableGUID, true) == 0
                                    select g).Count();

                IEnumerable<TableStruct> lstResult = from g in context.TableStructs
                                                     where string.Compare(tableGuid, g.TableGUID, true) == 0
                                                            && colIndexes.Contains(g.ColIndex)
                                                     select g;

                // 检测是否还有列，如果没有，则删除表

                if (totalColumns == lstResult.Count())
                {
                    if (IsExistTable(context, CreateTableName(tableGuid)))
                    {
                        context.ExecuteCommand("Drop table " + CreateTableName(tableGuid));
                    }
                }
                else
                {
                    foreach (TableStruct column in lstResult)
                    {
                        context.ExecuteCommand(string.Format("alter table {0} DROP COLUMN {1}", CreateTableName(tableGuid), CreateTableClumnName(column.ColIndex)));
                    }
                }

                context.TableStructs.DeleteAllOnSubmit(lstResult);

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志列失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "更新指定的应用程序的日志表中的日志表列的显示顺序")]
        public bool UpdateTableItemsSequence(string tableGuid, List<int> lstColIndexes)
        {
            try
            {
                if (lstColIndexes.Count <= 0)
                {
                    return false;
                }

                Dictionary<int, int> dctIndexes = new Dictionary<int, int>();

                for (int i = 0; i < lstColIndexes.Count; i++)
                {
                    dctIndexes.Add(lstColIndexes[i], i);
                }

                LogDataContext context = new LogDataContext();
                IEnumerable<TableStruct> lstResult = from g in context.TableStructs
                                                     where string.Compare(tableGuid, g.TableGUID, true) == 0
                                                     select g;                               

                foreach (TableStruct ts in lstResult)
                {
                    ts.SeqIndex = dctIndexes[ts.ColIndex];
                }

                dctIndexes.Clear();

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列显示顺序失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "更新指定的应用程序的日志表中的日志表列的内容")]
        public bool UpdateTableItemValue(string tableGuid, LogTableItemPlus item)
        {
            try
            {
                LogDataContext context = new LogDataContext();
                IEnumerable<TableStruct> lstResult = from g in context.TableStructs
                                                     where string.Compare(tableGuid, g.TableGUID, true) == 0
                                                            && (g.ColIndex==item.LogColumnIndex)
                                                     select g;

                //目前只有一项可以更新，就是该列是否为过滤项
                foreach (TableStruct ts in lstResult)
                {
                    ts.IsFilterColumn = item.IsFilterColumn;
                    ts.Visible = item.Visible;
                    ts.NickName = item.NickName;
                }                

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列内容失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description="获取指定应用程序的指定日志的所有列")]
        public List<LogTableItemPlus> GetAppTableItems(string tableGuid)
        {
            try
            {
                LogDataContext context=new LogDataContext();

                IEnumerable<TableStruct> lstResult=from g in context.TableStructs
                                                   where string.Compare(tableGuid,g.TableGUID,true)==0                                                   
                                                   orderby g.SeqIndex ascending
                                                   select g;

                List<LogTableItemPlus> lstItems = new List<LogTableItemPlus>();

                if(lstResult==null)
                {
                    return lstItems;
                }

                foreach(TableStruct ts in lstResult)
                {
                    LogTableItemPlus lti = new LogTableItemPlus();
                    lti.IsFilterColumn=ts.IsFilterColumn;
                    lti.LogColumnIndex=ts.ColIndex;
                    lti.SeqIndex=ts.SeqIndex;
                    lti.NickName = ts.NickName;
                    lti.Visible = ts.Visible;
                    
                    lstItems.Add(lti);
                }

                return lstItems;
            }
            catch(Exception ex)
            {
                throw new Exception("获取日志列信息失败，错误消息为："+ex.Message);
            }
        }

        // 获取指定应用程序的指定日志的所有列
        private List<TableColumnInfo> GetAppTableColumnNames(string tableGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<TableStruct> lstResult = from g in context.TableStructs
                                                     where string.Compare(tableGuid, g.TableGUID, true) == 0
                                                     orderby g.SeqIndex ascending
                                                     select g;

                List<TableColumnInfo> lstItems = new List<TableColumnInfo>();

                if (lstResult == null)
                {
                    return lstItems;
                }

                foreach (TableStruct ts in lstResult)
                {
                    TableColumnInfo info = new TableColumnInfo();
                    info.ColumnName = CreateTableClumnName(ts.ColIndex);
                    info.ColIndex = ts.ColIndex;
                    

                    lstItems.Add(info);
                }

                return lstItems;
            }
            catch (Exception ex)
            {
                throw new Exception("获取日志列名称失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region "应用程序分组操作"

        [WebMethod(Description="检查应用程序分组名称是否存在")]
        public bool IsAppGroupNameExisting(LogAppGroupNamePlus groupName)
        {
            try
            {
                LogDataContext context = new LogDataContext();
                AppGroupName agn=new AppGroupName();
                agn.Name=groupName.GroupName;
                return context.AppGroupNames.Contains(agn);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("检测分组名\"{0}\"是否存在失败，错误消息为：{1}",groupName,ex.Message));
            }
        }

        [WebMethod(Description = "添加新的应用程序分组")]
        public void AddAppGroups(List<LogAppGroupNamePlus> groupnames)
        {
            try
            {
                LogDataContext context = new LogDataContext();
                List<AppGroupName> lstNames=new List<AppGroupName>();

                foreach(LogAppGroupNamePlus name in groupnames)
                {
                    AppGroupName agn=new AppGroupName();
                    agn.Name=name.GroupName;

                    lstNames.Add(agn);
                }

                context.AppGroupNames.InsertAllOnSubmit(lstNames);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("更新应用程序分组内容失败，错误消息为：" + ex.Message);
            }

        }

        [WebMethod(Description = "重命名应用程序分组")]
        public bool ReNameAppGroup(string oldGroupName, string newGroupName)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<AppGroupName> names = from g in context.AppGroupNames
                                                  where string.Compare(g.Name, oldGroupName, true) == 0
                                                  select g;

                foreach (AppGroupName agn in names)
                {
                    agn.Name = newGroupName;
                }


                IEnumerable<App> lstResult = from g in context.Apps
                                             where string.Compare(oldGroupName, g.AppGroup, true) == 0
                                             select g;

                foreach (App app in lstResult)
                {
                    app.AppGroup = newGroupName;
                }

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序分组失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "重命名应用程序分组")]
        public bool ReNameAppGroups(List<LogAppGroupNamePlus> oldGroupNames, List<LogAppGroupNamePlus> newGroupNames)
        {
            try
            {
                if (oldGroupNames.Count != newGroupNames.Count)
                {
                    throw new Exception("新老名称数量不一致");
                }
                LogDataContext context = new LogDataContext();

                for (int i = 0; i < oldGroupNames.Count; i++)
                {
                    IEnumerable<AppGroupName> names = from g in context.AppGroupNames
                                                      where string.Compare(g.Name, oldGroupNames[i].GroupName, true) == 0
                                                      select g;

                    foreach (AppGroupName agn in names)
                    {
                        agn.Name = newGroupNames[i].GroupName;
                    }


                    IEnumerable<App> lstResult = from g in context.Apps
                                                 where string.Compare(oldGroupNames[i].GroupName, g.AppGroup, true) == 0
                                                 select g;

                    foreach (App app in lstResult)
                    {
                        app.AppGroup = newGroupNames[i].GroupName;
                    }
                }                

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序分组集合失败，错误消息为：" + ex.Message);
            }
        }
        
        [WebMethod(Description = "删除应用程序分组，删除时如果该分组下有应用程序，暂时不予删除，应该在客户端删除时确认删除时分组内不能有应用程序")]
        public bool RemoveAppGroupNames(List<LogAppGroupNamePlus> groupNames)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                foreach (LogAppGroupNamePlus lagnp in groupNames)
                {
                    IEnumerable<AppGroupName> lstResults = from g in context.AppGroupNames
                                                    where string.Compare(lagnp.GroupName, g.Name, true) == 0
                                                    select g;

                    context.AppGroupNames.DeleteAllOnSubmit(lstResults);
                }
                
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("删除应用程序分组失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "获取所有定义的应用程序分组")]
        public List<LogAppGroupNamePlus> GetExistingAppGroups()
        {
            try
            {
                LogDataContext context = new LogDataContext();

                List<LogAppGroupNamePlus> lstGroupNames = new List<LogAppGroupNamePlus>();

                IEnumerable<AppGroupName> lstAppResult = from g in context.AppGroupNames
                                                select g;

                if (lstAppResult == null)
                {
                    return lstGroupNames;
                }

                foreach (AppGroupName a in lstAppResult)
                {
                    LogAppGroupNamePlus la = new LogAppGroupNamePlus();
                    la.GroupName = a.Name;

                    lstGroupNames.Add(la);
                }

                return lstGroupNames;
            }
            catch (Exception ex)
            {
                throw new Exception("获取所有定义的应用程序分组失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 应用程序操作

        [WebMethod(Description = "添加新的应用程序，假定应用程序不包括日志表")]
        public bool AddApplication(LogAppPlus app)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                App a = new App();
                a.GUID = app.AppGUID;
                a.IsRemoved = false;
                a.Name = app.Name;
                a.IsImportLogsFromFile = app.IsImportLogsFromFile;
                a.AppGroup = app.AppGroupName;

                context.Apps.InsertOnSubmit(a);
                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列内容失败，错误消息为：" + ex.Message);
            }

        }

        [WebMethod(Description = "重命名应用程序")]
        public bool ReNameApplication(string appGuid, string newName)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<App> lstResult = from g in context.Apps
                                             where string.Compare(appGuid, g.GUID, true) == 0
                                             select g;

                foreach (App app in lstResult)
                {
                    app.Name = newName;
                }

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "更新应用程序属性")]
        public bool UpdateApplicationPropertes(string appGuid, LogAppPlus la)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<App> lstResult = from g in context.Apps
                                             where string.Compare(appGuid, g.GUID, true) == 0
                                             select g;

                foreach (App app in lstResult)
                {
                    app.Name = la.Name;
                    app.IsImportLogsFromFile = la.IsImportLogsFromFile;
                    app.AppGroup = la.AppGroupName;
                }

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("重命名应用程序失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除应用程序，删除时包括删除与其对应的日志表及表结构,日志内容暂时没有删除")]
        public bool RemoveApplication(string appGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<App> lstAppResult = from g in context.Apps
                                             where string.Compare(appGuid, g.GUID, true) == 0
                                             select g;

                foreach (App app in lstAppResult)
                {
                    IEnumerable<Table> lstTableResult=from g in context.Tables
                                                      where string.Compare(app.GUID, g.Owner, true) == 0
                                                      select g;

                    foreach (Table t in lstTableResult)
                    {
                        // 删除表结构
                        IEnumerable<TableStruct> lstStructResult = from g in context.TableStructs
                                                                   where string.Compare(t.GUID, g.TableGUID, true) == 0
                                                                   select g;

                        context.TableStructs.DeleteAllOnSubmit(lstStructResult);

                        if (IsExistTable(context, CreateTableName(t.GUID)))
                        {
                            context.ExecuteCommand("Drop table " + CreateTableName(t.GUID));
                        }
                    }

                    context.Tables.DeleteAllOnSubmit(lstTableResult);
                }

                context.Apps.DeleteAllOnSubmit(lstAppResult);

                context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列内容失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "获取所有定义的应用程序，不过没有获取应用程序的日志信，需要在客户端自己获取")]
        public List<LogAppPlus> GetExistingApps()
        {
            try
            {
                LogDataContext context = new LogDataContext();

                List<LogAppPlus> lstApps = new List<LogAppPlus>();

                IEnumerable<App> lstAppResult = from g in context.Apps
                                                where !g.IsRemoved
                                                select g;

                if (lstAppResult == null)
                {
                    return lstApps;
                }

                foreach (App a in lstAppResult)
                {
                    LogAppPlus la = new LogAppPlus();
                    la.AppGUID = a.GUID;
                    la.Name = a.Name;
                    la.IsImportLogsFromFile = a.IsImportLogsFromFile;
                    la.AppGroupName = a.AppGroup;

                    lstApps.Add(la);
                }

                return lstApps;
            }
            catch (Exception ex)
            {
                throw new Exception("获取所有定义的应用程序失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 日志内容操作

        private const int ContentLength = 120;

        private bool IsExistRecord(LogDataContext context, string tableName, string recordGuid)
        {
            //string sqlExpress = string.Format("select count({0}) from {1} where {2} = \'{3}\'",
            //    new object[]{FirstColumnOfTableColumn,CreateTableName(tableGuid),FirstColumnOfTableColumn,
            //    recordGuid});

            string queryString = string.Format("select count({0}) from {1} where {2} = \'{3}\'",
                new object[]{FirstColumnOfTableColumn,tableName,
                    FirstColumnOfTableColumn,recordGuid});

            IEnumerable<int> result = context.ExecuteQuery<int>(queryString);

            int count = result.FirstOrDefault();

            return count > 0;
        }

        //private List<LogContent> Split2LogContents(string appGuid, string tableGuid,string recordGuid, LogRecordItemPlus recordItem)
        //{
        //    List<LogContent> results = new List<LogContent>();

        //    string content = recordItem.Content;
        //    int seqIndex=0;
        //    int startIndex=0;

        //    while (content.Length > ContentLength)
        //    {
        //        LogContent lc = new LogContent();
        //        lc.RecordGuid = recordGuid;
        //        lc.TableGuid = tableGuid;
        //        lc.Line = seqIndex;
        //        lc.AppGuid = appGuid;
        //        lc.ColIndex = recordItem.ColumnIndex;
        //        lc.Content = content.Substring(startIndex, ContentLength);
                
        //        seqIndex++;
        //        startIndex+=ContentLength;
        //        content = content.Substring(startIndex);

        //        results.Add(lc);
        //    }

        //    LogContent lcEnd = new LogContent();
        //    lcEnd.RecordGuid = recordGuid;
        //    lcEnd.TableGuid = tableGuid;
        //    lcEnd.Line = seqIndex;
        //    lcEnd.AppGuid = appGuid;
        //    lcEnd.ColIndex = recordItem.ColumnIndex;
        //    lcEnd.Content = content;

        //    results.Add(lcEnd);

        //    return results;            
        //}

        //private List<LogContent> Split2LogContents(string appGuid, string tableGuid, ref List<LogRecordPlus> lstRecords)
        //{
        //    List<LogContent> results = new List<LogContent>();

        //    foreach(LogRecordPlus record in lstRecords)
        //    {
        //        if(IsExistRecord(appGuid,tableGuid,record.RecordGuid))
        //        {
        //            continue;
        //        }

        //        foreach(LogRecordItemPlus item in record.RecordItems)
        //        {
        //            if(string.IsNullOrEmpty(item.Content))
        //            {
        //                continue;
        //            }

        //            results.AddRange(Split2LogContents(appGuid, tableGuid, record.RecordGuid, item));
        //        }
        //    }

        //    return results;
        //}        

        private string CreateInsertLogRecordSqlExpress(string tableName,LogRecordPlus record)
        {
            string result = string.Empty;

            try
            {
                StringBuilder sbColNames = new StringBuilder();
                StringBuilder sbValues = new StringBuilder();

                sbColNames.Append("insert into " + tableName + "(");
                sbValues.Append("values(");

                // 添加日志guid
                sbColNames.Append(FirstColumnOfTableColumn);
                sbValues.Append("\'"+record.RecordGuid+"\'");

                foreach (LogRecordItemPlus item in record.RecordItems)
                {
                    if (string.IsNullOrEmpty(item.Content))
                    {
                        continue;
                    }

                    IColumnType t=TableColumnTypeService.Instance.GetColumnType(GetLogColumnByColIndex(item.ColumnIndex).Type);
                    string sqlExpress=t.GetDBValueExpress(item.Content);

                    if(string.IsNullOrEmpty(sqlExpress))
                    {
                        continue;
                    }

                    sbColNames.Append("," + CreateTableClumnName(item.ColumnIndex));
                    sbValues.Append("," + sqlExpress);
                }

                sbColNames.Append(") ");
                sbValues.Append(")");

                result = sbColNames.ToString() + sbValues.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("生成日志插入Sql语句失败，错误消息为：" + ex.Message);
            }
        }

        private List<String> Split2SqlExpresses(LogDataContext context, string tableGuid, ref List<LogRecordPlus> lstRecords)
        {
            List<string> results = new List<string>();
            string tableName=CreateTableName(tableGuid);

            for (int i = 0; i < lstRecords.Count; i++)
            {
                if (IsExistRecord(context, tableName, lstRecords[i].RecordGuid))
                {
                    continue;
                }

                results.Add(CreateInsertLogRecordSqlExpress(tableName, lstRecords[i]));
            }

                return results;
        }

        [WebMethod(Description = "添加应用程序中某一类表的日志，日志记录Guid重复的不添加")]
        public bool SaveAppLogs(string appGuid, string tableGuid, List<LogRecordPlus> lstRecords)
        {
            LogDataContext context = new LogDataContext();

            try
            {
                using (SqlConnection connection = new SqlConnection(context.Connection.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;

                    transaction = connection.BeginTransaction("SampleTransaction");
                    command.Transaction = transaction;

                    try
                    {
                        List<string> results = Split2SqlExpresses(context, tableGuid, ref lstRecords);

                        foreach (string express in results)
                        {
                            command.CommandText = express;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            throw new Exception("插入日志记录事务回滚失败，错误消息为：" + ex2.Message);
                        }

                        throw new Exception("插入日志记录失败，错误消息为：" + ex.Message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("更新日志列内容失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "根据条件获取应用程序中某一类表的日志")]
        public List<LogRecordPlus> GetAppLogs(string appGuid, string tableGuid, List<LogFilterConditionPlus> lstConditions)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dataReader = null;
            
            List<LogRecordPlus> queryResult = new List<LogRecordPlus>();            

            try
            {
                // 如果没有表结构，则说明没有数据，则直接返回空集合
                List<TableColumnInfo> tableColNames=GetAppTableColumnNames(tableGuid);            
                if(tableColNames.Count<=0)
                {
                    return queryResult;
                }

                LogDataContext context = new LogDataContext();
                #region 拼接sql语句
                StringBuilder sbSqlResult = new StringBuilder();

                sbSqlResult.Append(string.Format("select * from {0} ", CreateTableName(tableGuid)));

                // 依次组合检索条件
                if (lstConditions.Count > 0)
                {
                    List<OperateParam> lstParams = new List<OperateParam>();
                    StringBuilder sbConditions = new StringBuilder();

                    foreach (LogFilterConditionPlus condition in lstConditions)
                    {
                        lstParams.Clear();
                        IRelation relation = RelationService.Instance.GetRelation(condition.Relation);

                        if (relation.ParamsCount == 1)
                        {
                            lstParams.Add(new OperateParam(condition.Content));
                        }
                        else if (relation.ParamsCount == 2)
                        {
                            lstParams.Add(new OperateParam(condition.LeftBound));
                            lstParams.Add(new OperateParam(condition.RightBound));
                        }
                        else
                        {
                            throw new Exception(string.Format("目前检索函数不支持参数数量为{0}的关系{1}", relation.ParamsCount, relation.Name));
                        }

                        sbConditions.Append("and" + relation.GetPartOfSqlExpress(CreateTableClumnName(condition.ColumnIndex), lstParams));
                    }

                    if (sbConditions.Length > 0)
                    {
                        sbSqlResult.Append(" where ");
                        sbSqlResult.Append(sbConditions.ToString().Substring(3));
                    }
                }
#endregion

                // 查询
                connection = new SqlConnection(context.Connection.ConnectionString);
                command = new SqlCommand(sbSqlResult.ToString(), connection);
                connection.Open();
                dataReader = command.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    return queryResult;
                }

                while (dataReader.Read())
                {
                    LogRecordPlus record = new LogRecordPlus();
                    record.RecordGuid = Convert.ToString(dataReader[FirstColumnOfTableColumn]);
                    record.RecordItems = new List<LogRecordItemPlus>();

                    foreach (TableColumnInfo colInfo in tableColNames)
                    {
                        object value = dataReader[colInfo.ColumnName];
                        if (value != null)
                        {
                            LogRecordItemPlus recordItem = new LogRecordItemPlus();
                            recordItem.Content=Convert.ToString(value);
                            recordItem.ColumnIndex = colInfo.ColIndex;

                            record.RecordItems.Add(recordItem);
                        }
                    }

                    queryResult.Add(record);
                }

                return queryResult;
            }
            catch (Exception ex)
            {
                throw new Exception("获取应用程序日志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader = null;
                }

                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }
        }

        [WebMethod(Description = "根据日志GUID获取应用程序中某一类表的日志")]
        public List<LogRecordPlus> GetAppLogsByLogGuid(string appGuid, string tableGuid, List<string> lstRecordGuids)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dataReader = null;

            List<LogRecordPlus> queryResult = new List<LogRecordPlus>();

            try
            {
                // 如果没有表结构，则说明没有数据，则直接返回空集合
                List<TableColumnInfo> tableColNames = GetAppTableColumnNames(tableGuid);
                if (tableColNames.Count <= 0)
                {
                    return queryResult;
                }

                LogDataContext context = new LogDataContext();
                #region 拼接sql语句
                StringBuilder sbSqlResult = new StringBuilder();

                sbSqlResult.Append(string.Format("select * from {0} ", CreateTableName(tableGuid)));

                // 依次组合检索条件
                if (lstRecordGuids.Count > 0)
                {
                    StringBuilder sbConditions = new StringBuilder();

                    foreach (string strGuid in lstRecordGuids)
                    {
                        sbConditions.Append(",\'"+strGuid+"\'");
                    }

                    if (sbConditions.Length > 0)
                    {
                        sbSqlResult.Append(" where ");
                        sbSqlResult.Append(FirstColumnOfTableColumn+" IN  (");                        
                        sbSqlResult.Append(sbConditions.ToString().Substring(1));
                        sbSqlResult.Append(")");
                    }
                }
                #endregion

                // 查询
                connection = new SqlConnection(context.Connection.ConnectionString);
                command = new SqlCommand(sbSqlResult.ToString(), connection);
                connection.Open();
                dataReader = command.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    return queryResult;
                }

                while (dataReader.Read())
                {
                    LogRecordPlus record = new LogRecordPlus();
                    record.RecordGuid = Convert.ToString(dataReader[FirstColumnOfTableColumn]);
                    record.RecordItems = new List<LogRecordItemPlus>();

                    foreach (TableColumnInfo colInfo in tableColNames)
                    {
                        object value = dataReader[colInfo.ColumnName];
                        if (value != null)
                        {
                            LogRecordItemPlus recordItem = new LogRecordItemPlus();
                            recordItem.Content = Convert.ToString(value);
                            recordItem.ColumnIndex = colInfo.ColIndex;

                            record.RecordItems.Add(recordItem);
                        }
                    }

                    queryResult.Add(record);
                }

                return queryResult;
            }
            catch (Exception ex)
            {
                throw new Exception("获取应用程序日志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader = null;
                }

                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }
        }

        //[WebMethod(Description = "根据条件获取应用程序中某一类表的日志")]
        //public List<LogRecordPlus> GetAppLogs(string appGuid, string tableGuid, List<LogFilterConditionPlus> lstConditions)
        //{
        //    try
        //    {
        //        LogDataContext context=new LogDataContext();

        //        List<LogRecordPlus> lstRecords = new List<LogRecordPlus>();

        //        IEnumerable<LogContent> lstResults = from g in context.LogContents
        //                                             where (tableGuid == g.TableGuid) && (appGuid == g.AppGuid)                                                     
        //                                             orderby g.RecordGuid,g.ColIndex,g.Line ascending
        //                                             select g;
        //        // 依次进行条件
        //        foreach (LogFilterConditionPlus condition in lstConditions)
        //        {
 
        //        }

        //        // 组合查询结果

        //        if (lstResults.Count() > 0)
        //        {
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                   
        //            LogRecordPlus record=new LogRecordPlus();
        //            record.RecordItems = new List<LogRecordItemPlus>();
        //            record.RecordGuid=lstResults.FirstOrDefault().RecordGuid;

        //            LogRecordItemPlus recordItem = new LogRecordItemPlus();
        //            recordItem.ColumnIndex = lstResults.FirstOrDefault().ColIndex;                   

        //            foreach (LogContent content in lstResults)
        //            {
        //                if (recordItem.ColumnIndex != content.ColIndex)
        //                {
        //                    recordItem.Content=sb.ToString();

        //                    if (!string.IsNullOrEmpty(recordItem.Content))
        //                    {
        //                        record.RecordItems.Add(recordItem);
        //                    }

        //                    sb.Remove(0, sb.Length);

        //                    recordItem = new LogRecordItemPlus();
        //                    recordItem.ColumnIndex = content.ColIndex;
        //                }

        //                if (record.RecordGuid != content.RecordGuid)
        //                {
        //                    lstRecords.Add(record);

        //                    record = new LogRecordPlus();
        //                    record.RecordItems = new List<LogRecordItemPlus>();
        //                    record.RecordGuid = content.RecordGuid;
        //                }

        //                sb.Append(content.Content);
        //            }

        //            if (sb.ToString() != null)
        //            {
        //                lstRecords.Add(record);
        //            }
        //        }                         

        //        return lstRecords;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("获取应用程序日志失败，错误消息为：" + ex.Message);
        //    }
        //}
        #endregion

        #region 规则操作

        [WebMethod(Description = "获取用户定义的所有用户行为结果,有问题则抛异常")]
        public List<RuleActionResultPlus> GetDefinedActionResults()
        {
            try
            {
                List<RuleActionResultPlus> lstResult = new List<RuleActionResultPlus>();

                LogDataContext context=new LogDataContext();

                IEnumerable<ActionResult> queryResult = from g in context.ActionResults
                                                        select g;

                foreach (ActionResult ar in queryResult)
                {
                    RuleActionResultPlus result = new RuleActionResultPlus();
                    result.BgColor = ar.BackGroundColor;
                    result.Desc = ar.Description;
                    result.Guid = ar.ResultGuid.ToString();

                    lstResult.Add(result);
                }

                return lstResult;
            }
            catch(Exception ex)
            {
                throw new Exception("检索用户定义行为结果失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "更新用户定义的所有用户行为结果,如果结果没有定义，则不予更新，有问题则抛异常")]
        public void UpdateResults(List<RuleActionResultPlus> results)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                foreach (RuleActionResultPlus result in results)
                {
                    IEnumerable<ActionResult> queryResult = from g in context.ActionResults
                                                            where string.Compare(g.ResultGuid, result.Guid, true) == 0
                                                            select g;

                    foreach (ActionResult ar in queryResult)
                    {
                        ar.BackGroundColor = result.BgColor;
                        ar.Description = result.Desc;                        
                    }
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("更新用户定义的所有用户行为结果失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "插入用户定义的用户行为结果,有问题则抛异常")]
        public void InsertResults(List<RuleActionResultPlus> results)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                List<ActionResult> lstDatas = new List<ActionResult>();

                foreach (RuleActionResultPlus result in results)
                {
                    ActionResult ar = new ActionResult();
                    ar.ResultGuid = result.Guid;
                    ar.Description = result.Desc;
                    ar.BackGroundColor = result.BgColor;

                    lstDatas.Add(ar);
                }

                if (lstDatas.Count > 0)
                {
                    context.ActionResults.InsertAllOnSubmit(lstDatas);
                    context.SubmitChanges();
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("插入用户定义的用户行为结果集合失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "删除用户定义的用户行为结果,有问题则抛异常")]
        public void DeleteResults(List<string> lstGuids)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                foreach (string guid in lstGuids)
                {
                    IEnumerable<ActionResult> queryResult = from g in context.ActionResults
                                                            where string.Compare(g.ResultGuid, guid, true) == 0
                                                            select g;

                    context.ActionResults.DeleteAllOnSubmit(queryResult);
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除用户定义的用户行为结果失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "获取使用用户定义的用户行为结果的规则名称，用分号隔开,有问题则抛异常")]
        public string GetRulesOfUsingTheResult(string resultGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesAction> queryResult = from g in context.RulesActions
                                                        where string.Compare(g.Result, resultGuid, true) == 0
                                                        select g;

                StringBuilder sb = new StringBuilder();

                foreach (RulesAction ra in queryResult)
                {
                    sb.Append(context.RulesEvents.FirstOrDefault(r => string.Compare(r.Guid, ra.RuleEventGuid, true) == 0).Name);
                    sb.Append(";");
                }

                return sb.Length > 0 ? sb.ToString().Substring(0, sb.Length - 1) : string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("获取使用用户定义的用户行为结果的规则名称失败，错误消息为："+ex.Message);
            }
        }
        #endregion

        #region 安全事件
        [WebMethod(Description="获取所有定义的安全事件，包含用户行为和条件")]
        public List<RuleEvenPlus> GetDefinedSecurityEvents()
        {
            try
            {
                List<RuleEvenPlus> definedEvents = new List<RuleEvenPlus>();

                LogDataContext context = new LogDataContext();

                IEnumerable<RulesEvent> lstEvents = from g in context.RulesEvents
                                             select g;
                if (lstEvents.Count() <= 0)
                {
                    return definedEvents;
                }

                foreach (RulesEvent re in lstEvents)
                {
                    RuleEvenPlus rule = new RuleEvenPlus();
                    rule.Desc = re.Description;
                    rule.Guid = re.Guid;
                    rule.Name = re.Name;

                    IEnumerable<RulesAction> lstActions = from g in context.RulesActions
                                                          where string.Compare(g.RuleEventGuid, re.Guid, true) == 0
                                                          select g;

                    if(lstActions.Count()>0)
                    {
                        rule.Actions=new List<RuleActionPlus>();

                        foreach (RulesAction action in lstActions)
                        {
                            RuleActionPlus ap = new RuleActionPlus();
                            ap.Desc = action.Description;
                            ap.Guid = action.RuleHeadGuid;
                            ap.ResultGuid = action.Result;
                            ap.Sequence = action.Sequence;
                            ap.Name = action.Name;


                            IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                                        where string.Compare(g.RuleHeadGuid, action.RuleHeadGuid, true) == 0
                                                                        select g;

                            if (lstConditions.Count() > 0)
                            {
                                ap.Conditions = new List<RuleConditionPlus>();

                                foreach (RulesCondition rc in lstConditions)
                                {
                                    RuleConditionPlus rcp = new RuleConditionPlus();
                                    rcp.ColIndexDest = Convert.ToInt32(rc.ColIndexDest);
                                    rcp.ColIndexSrc = rc.ColIndexSrc;
                                    rcp.Condition = rc.Condition;
                                    rcp.Desc = rc.Description;
                                    rcp.IsUsingDestCol = rc.IsUsingDestCol;
                                    rcp.Relation = rc.Relation;
                                    rcp.Guid = rc.ConditionGuid;

                                    ap.Conditions.Add(rcp);
                                }
                            }

                            rule.Actions.Add(ap);
                        }
                    }                    

                    definedEvents.Add(rule);
                }

                return definedEvents;
            }
            catch (Exception ex)
            {
                throw new Exception("获取所有定义的安全事件失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "创建新的安全事件，不包含用户行为和条件")]
        public void CreateNewSecurityEvent(RuleEvenPlus se)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                RulesEvent re = new RulesEvent();
                re.Description = se.Desc;
                re.Guid = se.Guid;
                re.Name = se.Name;

                context.RulesEvents.InsertOnSubmit(re);

                if (se.Actions != null)
                {
                    foreach (RuleActionPlus rap in se.Actions)
                    {
                        RulesAction ra = new RulesAction();
                        ra.Description = rap.Desc;
                        ra.Result = rap.ResultGuid;
                        ra.RuleEventGuid = se.Guid;
                        ra.RuleHeadGuid = rap.Guid;
                        ra.Sequence = rap.Sequence;
                        ra.Name = rap.Name;

                        context.RulesActions.InsertOnSubmit(ra);

                        if (rap.Conditions.Count > 0)
                        {
                            foreach (RuleConditionPlus rcp in rap.Conditions)
                            {
                                RulesCondition rc = new RulesCondition();
                                rc.ColIndexDest = rcp.ColIndexDest;
                                rc.ColIndexSrc = rcp.ColIndexSrc;
                                rc.Condition = rcp.Condition;
                                rc.ConditionGuid = rcp.Guid;
                                rc.Description = rcp.Desc;
                                rc.IsUsingDestCol = rcp.IsUsingDestCol;
                                rc.Relation = rcp.Relation;
                                rc.RuleHeadGuid = rap.Guid;

                                context.RulesConditions.InsertOnSubmit(rc);
                            }
                        }
                    }
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("创建新的安全事件失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "更新安全事件信息，不包含用户行为和条件")]
        public void UpdateSecurityEventProperties(RuleEvenPlus se)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesEvent> lstEvents = from g in context.RulesEvents
                                                    where string.Compare(g.Guid, se.Guid, true) == 0
                                                    select g;

                if (lstEvents.Count() < 0)
                {
                    throw new Exception(string.Format("安全事件{0}在数据库中不存在", se.Name));
                }

                foreach (RulesEvent re in lstEvents)
                {
                    re.Description = se.Desc;
                    re.Name = se.Name;
                }               

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全事件信息失败，错误消息为：" + ex.Message);
            }
        }

        [WebMethod(Description = "删除安全事件，包含用户行为和条件")]
        public void DeleteSecurityEvent(string eventGuid)
        {
            try
            {                
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesEvent> lstEvents = from g in context.RulesEvents
                                                    where string.Compare(g.Guid,eventGuid,true)==0
                                                    select g;
                if (lstEvents.Count() <= 0)
                {
                    throw new Exception("要删除的安全事件不存在");
                }

                foreach (RulesEvent re in lstEvents)
                {
                    IEnumerable<RulesAction> lstActions = from g in context.RulesActions
                                                          where string.Compare(g.RuleEventGuid, re.Guid, true) == 0
                                                          select g;

                    if (lstActions.Count() > 0)
                    {
                        foreach (RulesAction action in lstActions)
                        {

                            IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                                        where string.Compare(g.RuleHeadGuid, action.RuleHeadGuid, true) == 0
                                                                        select g;

                            if (lstConditions.Count() > 0)
                            {
                                context.RulesConditions.DeleteAllOnSubmit(lstConditions);
                            }                           
                        }

                        context.RulesActions.DeleteAllOnSubmit(lstActions);
                    }                    
                }

                context.RulesEvents.DeleteAllOnSubmit(lstEvents);

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除安全事件失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 安全行为
        [WebMethod(Description = "创建新的安全事件的安全行为，不包含条件")]
        public void CreateNewActionOfEvent(string eventGuid, RuleActionPlus ap)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                RulesAction ra = new RulesAction();
                ra.Description = ap.Desc;
                ra.Result = ap.ResultGuid;
                ra.RuleEventGuid = eventGuid;
                ra.RuleHeadGuid = ap.Guid;
                ra.Sequence = ap.Sequence;
                ra.Name = ap.Name;

                context.RulesActions.InsertOnSubmit(ra);

                if (ap.Conditions.Count > 0)
                {
                    foreach (RuleConditionPlus rcp in ap.Conditions)
                    {
                        RulesCondition rc = new RulesCondition();
                        rc.ColIndexDest = rcp.ColIndexDest;
                        rc.ColIndexSrc = rcp.ColIndexSrc;
                        rc.Condition = rcp.Condition;
                        rc.ConditionGuid = rcp.Guid;
                        rc.Description = rcp.Desc;
                        rc.IsUsingDestCol = rcp.IsUsingDestCol;
                        rc.Relation = rcp.Relation;
                        rc.RuleHeadGuid = ap.Guid;

                        context.RulesConditions.InsertOnSubmit(rc);
                    }
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("创建新的安全行为失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "更新安全行为信息，不包含条件")]
        public void UpdateActionOfEvent(string eventGuid, RuleActionPlus ap)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesAction> lstActions = from g in context.RulesActions
                                                      where string.Compare(ap.Guid, g.RuleHeadGuid, true) == 0 &&
                                                            string.Compare(eventGuid, g.RuleEventGuid, true) == 0
                                                      select g;

                if (lstActions.Count() <= 0)
                {
                    throw new Exception("要更新的安全行为不存在");
                }

                foreach (RulesAction ra in lstActions)
                {
                    ra.Sequence = ap.Sequence;
                    ra.Result = ap.ResultGuid;
                    ra.Description = ap.Desc;
                    ra.Name = ap.Name;
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全行为信息失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "删除安全行为，包含条件")]
        public void DeleteActionOfEvent(string eventGuid, string actionGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesAction> lstActions = from g in context.RulesActions
                                                      where string.Compare(actionGuid, g.RuleHeadGuid, true) == 0 &&
                                                            string.Compare(eventGuid, g.RuleEventGuid, true) == 0
                                                      select g;

                if (lstActions.Count() <= 0)
                {
                    throw new Exception("要删除的安全行为不存在");
                }

                foreach (RulesAction ra in lstActions)
                {
                    IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                                where string.Compare(g.RuleHeadGuid, actionGuid, true) == 0
                                                                select g;

                    if (lstConditions.Count() > 0)
                    {
                        context.RulesConditions.DeleteAllOnSubmit(lstConditions);
                    }
                }

                context.RulesActions.DeleteAllOnSubmit(lstActions);

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除安全行为失败，错误消息为："+ex.Message);
            }
        }
        #endregion

        #region 安全条件
        [WebMethod(Description = "创建新的安全条件")]
        public void CreateNewConditionOfAction(string actionGuid, RuleConditionPlus rcp)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                RulesCondition rc = new RulesCondition();
                rc.ColIndexDest = rcp.ColIndexDest;
                rc.ColIndexSrc = rcp.ColIndexSrc;
                rc.Condition = rcp.Condition;
                rc.ConditionGuid = rcp.Guid;
                rc.Description = rcp.Desc;
                rc.IsUsingDestCol = rcp.IsUsingDestCol;
                rc.Relation = rcp.Relation;
                rc.RuleHeadGuid = actionGuid;

                context.RulesConditions.InsertOnSubmit(rc);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("创建新的安全条件失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "更新安全条件")]
        public void UpdateConditionOfAction(string actionGuid, RuleConditionPlus rcp)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                            where string.Compare(rcp.Guid, g.ConditionGuid, true) == 0 &&
                                                                  string.Compare(actionGuid, g.RuleHeadGuid, true) == 0
                                                            select g;

                if (lstConditions.Count() <= 0)
                {
                    throw new Exception("要更新的安全条件不存在");
                }

                foreach (RulesCondition rc in lstConditions)
                {
                    rc.ColIndexDest = rcp.ColIndexDest;
                    rc.ColIndexSrc = rcp.ColIndexSrc;
                    rc.Condition = rcp.Condition;
                    rc.Description = rcp.Desc;
                    rc.IsUsingDestCol = rcp.IsUsingDestCol;
                    rc.Relation = rcp.Relation;
                }

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全条件失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "删除安全条件")]
        public void DeleteConditionOfAction(string actionGuid,string conditionGuid)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                            where string.Compare(conditionGuid, g.ConditionGuid, true) == 0 &&
                                                                  string.Compare(actionGuid, g.RuleHeadGuid, true) == 0
                                                            select g;

                if (lstConditions.Count() <= 0)
                {
                    throw new Exception("要删除的安全条件不存在");
                }

                context.RulesConditions.DeleteAllOnSubmit(lstConditions);

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除安全条件失败，错误消息为："+ex.Message);
            }
        }

        [WebMethod(Description = "删除安全条件集合")]
        public void DeleteConditionsOfAction(string actionGuid, List<string> conditionGuids)
        {
            try
            {
                LogDataContext context = new LogDataContext();

                foreach (string conditionGuid in conditionGuids)
                {
                    IEnumerable<RulesCondition> lstConditions = from g in context.RulesConditions
                                                                where string.Compare(conditionGuid, g.ConditionGuid, true) == 0 &&
                                                                      string.Compare(actionGuid, g.RuleHeadGuid, true) == 0
                                                                select g;

                    if (lstConditions.Count() >0)
                    {
                        context.RulesConditions.DeleteAllOnSubmit(lstConditions);
                    }                    
                }                

                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("删除安全条件集合失败，错误消息为：" + ex.Message);
            }
        }
        #endregion
    }
}