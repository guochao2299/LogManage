using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType
{
    public static class ConstColumnValue
    {
        /// <summary>
        /// 如果列是一个空列，本项指定其列名
        /// </summary>
        public const string NullLogColumnName = "空列名称";

        /// <summary>
        /// 如果列是一个空列，本项指定其列序号
        /// </summary>
        public const int NullLogColumnIndex = -1;

        /// <summary>
        /// 默认日志列的名称
        /// </summary>
        public const string DefaultLogColumnName = "新日志列";

        /// <summary>
        /// 默认日志列的类型
        /// </summary>
        public const string DefaultLogColumnType = "字符串";
    }

    
}
