using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    public interface ILogOperator
    {
        /// <summary>
        /// 系统的唯一标识符，从日志管理系统中可以获取
        /// </summary>
        string AppGuid { get; set; }

        /// <summary>
        /// 如果从文件中读取日志，则需设置可以导入哪些文件后缀名，值示例为："Text files (*.txt)|*.txt|All files (*.*)|*.*"
        /// </summary>
        string Filter { get; set; }

        Dictionary<string, List<LogRecord>> ReadLogFromFiles(string appGuid, string[] filePaths);
    }

    public class NullLogOperator : ILogOperator
    {
        public string Filter
        {
            get
            {
                return "All files (*.*)|*.*";
            }
            set
            { 
            }
        }

        public string AppGuid
        {
            get
            {
                return string.Empty;
            }
            set
            { }
        }

        public Dictionary<string, List<LogRecord>> ReadLogFromFiles(string appGuid, string[] fileName)
        {
            return new Dictionary<string, List<LogRecord>>();
        }
    }
}
