using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using LogManage.DataType.Rules;
using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis.SPI
{
    internal class ResultStatistics:StatisticsBase
    {
        protected override string GenerateKey(DataType.Rules.Evaluation.EvaluateResult er)
        {
            return er.ResultGuid;
        }

        protected override string GenerateTitle(DataType.Rules.Evaluation.EvaluateResult er)
        {
            string title = string.Empty;

            if (SecurityEventService.Instance.IsSecurityActionResultExisting(er.ResultGuid))
            {
                title = SecurityEventService.Instance.GetSecurityActionResult(er.ResultGuid).Description;
            }
            else
            {
                title = er.ResultName;
            }

            return title;
        }

        protected override System.Drawing.Color GenerateColor(EvaluateResult er, int index)
        {
            Color c = Color.White;

            if (SecurityEventService.Instance.IsSecurityActionResultExisting(er.ResultGuid))
            {
                c = Color.FromArgb(SecurityEventService.Instance.GetSecurityActionResult(er.ResultGuid).BackgroundColor);
            }
            else
            {
                c = er.BackColor;
            }

            return c;
        }
    }
}
