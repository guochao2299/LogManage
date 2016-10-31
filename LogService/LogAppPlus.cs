using System;
using System.Collections.Generic;
using System.Web;

// 创建这几个类是应用客户端和服务器端同时应用了DataType程序集
// 然后在服务空间中就出现了和DataType空间中相同的几个类
// 造成类型调用不明确，所以专门创建这几个类专供服务器端使用

namespace LogService
{
    public struct LogAppGroupNamePlus
    {
        public string GroupName;
    }
    /// <summary>
    /// 代表一个存在日志的应用程序
    /// </summary>
    public struct LogAppPlus
    {
        /// <summary>
        /// 系统所属分组
        /// </summary>
        public string AppGroupName;

        /// <summary>
        /// 系统唯一标识符
        /// </summary>
        public string AppGUID;        

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 该应用程序的日志是否从外部文件中导入
        /// </summary>
        public bool IsImportLogsFromFile;
    }

    public struct LogColumnPlus
    {
        /// <summary>
        /// 该列对应的列名，可以为空
        /// </summary>
        public String Name;

        /// <summary>
        /// 列对应的序号，该序号为唯一值
        /// </summary>
        public Int32 Index;

        /// <summary>
        /// 列对应的类型，主要用于条件检索时使用
        /// </summary>
        public string Type;
    }

    public struct LogTablePuls
    {
        /// <summary>
        /// 表的唯一标示符
        /// </summary>
        public string GUID;

        public string Name;

        public List<LogTableItemPlus> Columns;
    }

    public struct LogTableItemPlus
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 LogColumnIndex;

        /// <summary>
        /// 该列是否作为条件检索列，如果是，则显示检索条件时可以显示该检索条件
        /// </summary>
        public bool IsFilterColumn;

        public int SeqIndex;

        public bool Visible;

        public string NickName;
    }

    public struct LogRecordItemPlus
    {      
        public int ColumnIndex;
        public string Content;
    }

    public struct LogRecordPlus
    {
        public string RecordGuid;

        public List<LogRecordItemPlus> RecordItems;
    }

    internal struct TableColumnInfo
    {
        public string ColumnName;
        public int ColIndex;
    }

    // <summary>
    /// 表示一条过滤条件
    /// </summary>
    public struct LogFilterConditionPlus
    {
        public int ColumnIndex;

        public string Type;

        public string Content;

        public string LeftBound;

        public string RightBound;

        public string Relation;
    }

    public struct RuleEvenPlus
    {
        public string Name;
        public string Guid;
        public string Desc;

        public List<RuleActionPlus> Actions;
    }

    public struct RuleActionPlus
    {
        public string Guid;
        public string Desc;
        public string ResultGuid;
        public int Sequence;
        public string Name;

        public List<RuleConditionPlus> Conditions;
    }

    public struct RuleConditionPlus
    {
        public string Guid;
        public int ColIndexSrc;
        public int ColIndexDest;
        public bool IsUsingDestCol;
        public string Desc;
        public string Condition;
        public string Relation;
    }

    public struct RuleActionResultPlus
    {
        public string Guid;
        public string Desc;
        public int BgColor;
    }
}