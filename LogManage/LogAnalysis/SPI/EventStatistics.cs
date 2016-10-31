using System;
using System.Collections.Generic;
using System.Text;
using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis.SPI
{
    /// <summary>
    /// 按安全事件统计结果
    /// </summary>
    internal class EventStatistics:StatisticsBase
    {
        protected override string GenerateKey(DataType.Rules.Evaluation.EvaluateResult er)
        {
            return er.EventGuid;
        }

        protected override string GenerateTitle(DataType.Rules.Evaluation.EvaluateResult er)
        {
            return er.EventName;
        }

        protected override System.Drawing.Color GenerateColor(EvaluateResult er, int index)
        {
            return ColorsPool.Instance.GetColor(index);
        }
    }
}
