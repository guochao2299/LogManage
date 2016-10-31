using System;
using System.Collections.Generic;
using System.Text;
using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis.SPI
{
    internal class ActionStatistics:StatisticsBase
    {
        /// <summary>
        /// 按安全行为统计结果
        /// </summary>
        protected override string GenerateKey(DataType.Rules.Evaluation.EvaluateResult er)
        {
            return er.EventGuid + "@" + er.ActionGuid;
        }

        protected override string GenerateTitle(DataType.Rules.Evaluation.EvaluateResult er)
        {
            return er.ActionName;
        }

        protected override System.Drawing.Color GenerateColor(EvaluateResult er, int index)
        {
            return ColorsPool.Instance.GetColor(index);
        }
    }
}
