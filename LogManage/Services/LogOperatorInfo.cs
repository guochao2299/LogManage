using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.Services
{
    /// <summary>
    /// 用于描述各程序集对应的配置文件
    /// </summary>
    [Serializable]
    public class LogOperatorInfo
    {
        public LogOperatorInfo()
        { }

        public string AppGuid;
        public string Filter;
        public string FileName;

        public List<LogTableInfo> TableInfos = new List<LogTableInfo>();
    }
}
