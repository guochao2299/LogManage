using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis
{
    internal interface IObserver
    {
        void Notify(List<EvaluateResult> newAnalysises);
    }
}
