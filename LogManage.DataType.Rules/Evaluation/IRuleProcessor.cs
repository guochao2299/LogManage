//
// 目前，由于系统中定义的策略只针对一条记录进行分析，
// 而实际使用时需要对多条记录进行分析，这就复杂了很多
// 于是想到了在系统中只针对一条记录进行策略配置
// 对于需要分析多条记录的策略，则采用外部扩展的方式进行定制
// 这样可以暂时解决一部分问题，
// 然后在系统中定义一个策略管理器，统一管理这两类策略处理方式
// 2014-9-18 郭超

using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType;

namespace LogManage.DataType.Rules.Evaluation
{
    /// <summary>
    /// 执行策略的接口定义
    /// </summary>
    public interface IRuleProcessor
    {
        /// <summary>
        /// 初始化函数，策略管理器调用此函数更改要执行的策略,如果处理器内部自带策略，不用理会此函数
        /// </summary>
        void InitialRules(List<SecurityEvent> lstRules);

        /// <summary>
        /// 调用Execute后是否可以立即获得策略分析结果，true为可以，false为输入全部策略后再获取结果
        /// </summary>
        bool IsImmediatelyReturn { get; }

        /// <summary>
        /// 输入一条记录，然后分析，如果是立即返回的策略分析，调用后可以直接获取Result，如果不是，则输入全部日志以后，需要调用analisis函数进行分析后再获取Result
        /// </summary>
        /// <param name="lstItems"></param>
        void Execute(LogRecord record,ILogColumnService colSrv);

        /// <summary>
        /// 调用Execute后，如果需要进一步分析，则调用此函数，注意！！！此函数是在所有日志记录都传递给Execute后才调用的
        /// </summary>
        void Analysis();

        List<EvaluateResult> Result { get; }
    }
}
