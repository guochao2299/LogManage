using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 和应用程序相关的一些固定值
    /// </summary>
    public static class ConstAppValue
    {
        /// <summary>
        /// 创建应用程序时的默认应用程序名称
        /// </summary>
        public const string DefaultAppName = "新应用程序";

        /// <summary>
        /// 应用程序的日志是否从外部文件导入的默认值
        /// </summary>
        public const bool DefaultIsLogsFromFile = true;

        /// <summary>
        /// 当应用系统为Null时的程序名称
        /// </summary>
        public const string NullAppName = "空应用程序";
    }
}
