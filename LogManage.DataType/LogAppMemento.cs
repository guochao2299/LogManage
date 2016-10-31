using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType
{
    public struct LogAppMemento
    {
        public LogAppMemento(bool isImportFromFiles,string appName,string groupName)
        {
            IsImportLogsFromFile = isImportFromFiles;
            LogAppName = appName;
            GroupName = groupName;
        }
        public bool IsImportLogsFromFile;
        public string LogAppName;
        public string GroupName;
    }
}
