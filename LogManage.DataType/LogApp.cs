using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LogManage.DataType
{
    /// <summary>
    /// 代表一个存在日志的应用程序
    /// </summary>
    [Serializable()]
    public class LogApp:ICloneable
    {
        public LogApp()
        {
            m_guid=string.Empty;
            m_tables = new List<LogTable>();
        }

        public LogApp(string guid):this()
        {
            m_guid = guid;
        }

        private string m_guid;
        
        /// <summary>
        /// 系统唯一标识符
        /// </summary>
        public string AppGUID
        {
            get
            {
                return m_guid;
            }
            set
            {
                m_guid = value;
            }
        }
        
        private string m_name;

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        private bool m_isImportLogsFromFiles=true;
        
        /// <summary>
        /// 该应用程序的日志是否从外部文件中导入
        /// </summary>
        public bool IsImportLogsFromFiles
        {
            get
            {
                return m_isImportLogsFromFiles;
            }
            set
            {
                m_isImportLogsFromFiles = value;
            }
        }

        private List<LogTable> m_tables = null;

        public List<LogTable> Tables
        {
            get
            {
                return m_tables;
            }
            set
            {
                m_tables = value;
            }
        }

        private LogAppGroup m_group = new LogAppGroup();

        public LogAppGroup Group
        {
            get
            {
                return m_group;
            }
            set
            {
                m_group.CopyFrom(value);
            }
        }

        public bool IsExistingTable(string tableGuid)
        {
            foreach (LogTable lt in Tables)
            {
                if (string.Equals(lt.GUID, tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public LogTable GetTable(string tableGuid)
        {
            foreach (LogTable lt in Tables)
            {
                if (string.Equals(lt.GUID, tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return lt;
                }
            }

            throw new Exception("要查找的日志表不存在");
        }

        public void AddTable(LogTable table)
        {
            Tables.Add((LogTable)table.Clone());
        }

        public void RemoveTable(string tableGuid)
        {
            for(int i=Tables.Count-1;i>=0;i--)
            {
                if (string.Equals(Tables[i].GUID, tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    Tables.RemoveAt(i);
                    return;
                }
            }
        }

        public void ReNameTable(string tableGuid,string newName)
        {
            LogTable lt = LogTable.NullLogTable;

            for (int i = Tables.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Tables[i].GUID, tableGuid, StringComparison.OrdinalIgnoreCase))
                {
                    lt = Tables[i];
                    break;
                }
            }

            lt.Name = newName;
        }

        public LogAppMemento CreateMemento()
        {
            return new LogAppMemento(this.IsImportLogsFromFiles, this.Name,this.Group.Name);
        }

        public object Clone()
        {
            LogApp la = new LogApp();
            la.m_guid = this.AppGUID;
            la.Name = this.Name;
            la.IsImportLogsFromFiles = this.IsImportLogsFromFiles;
            la.Group = this.Group;

            foreach (LogTable lt in this.Tables)
            {
                la.Tables.Add((LogTable)lt.Clone());
            }

            return la;
        }

        /// <summary>
        /// 创建一个空的应用程序
        /// </summary>
        [XmlIgnore]
        public static LogApp NullApplication
        {
            get
            {
                LogApp app = new LogApp();
                app.Name = ConstAppValue.NullAppName;
                app.m_guid = string.Empty;

                return app;
            }
        }

        /// <summary>
        /// 创建一个新的应用程序
        /// </summary>
        [XmlIgnore]
        public static LogApp NewApplication
        {
            get
            {
                LogApp app = new LogApp();
                app.Name = ConstAppValue.DefaultAppName;
                app.m_guid = Guid.NewGuid().ToString();

                return app;
            }
        }

        public static LogApp CreateAppFromMemento(LogAppMemento memento)
        {
            LogApp app = NullApplication;
            app.Name = memento.LogAppName;
            app.IsImportLogsFromFiles = memento.IsImportLogsFromFile;
            app.Group = new LogAppGroup(memento.GroupName);

            return app;
        }

        public static LogApp CreateApp(string appName,string appGuid,bool isImportLogsFromFiles)
        {
            LogApp app = NullApplication;
            app.Name = appName;
            app.AppGUID = appGuid;
            app.IsImportLogsFromFiles = isImportLogsFromFiles;

            return app;
        }
    }
}
