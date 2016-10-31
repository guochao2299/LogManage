using System;
namespace LogManage.DataType
{
    public static class ConstTableValue
    {
        /// <summary>
        /// 如果表是一个空表，本项指定其表名
        /// </summary>
        public const string NullLogTableName = "空表名称";

        /// <summary>
        /// 默认日志表的名称
        /// </summary>
        public const string DefaultLogTableName = "新日志表";

        /// <summary>
        /// 日志表项中是否为过滤项的默认值
        /// </summary>
        public const bool DefaultLogTableItemIsFilterValue = false;
    }
}
