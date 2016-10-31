using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis
{
    internal interface IStatistics
    {
        Dictionary<string, frmChartObserver.StatisticsResult> Statistics(List<DataType.Rules.Evaluation.EvaluateResult> newAnalysises);
    }

    internal class NullStatistics:IStatistics
    {
        #region IStatistics Members

        public Dictionary<string, frmChartObserver.StatisticsResult> Statistics(List<EvaluateResult> newAnalysises)
        {
            return new Dictionary<string, frmChartObserver.StatisticsResult>();
        }

        #endregion
    }

    internal abstract class StatisticsBase:IStatistics
    {
        protected abstract string GenerateKey(EvaluateResult er);
        protected abstract string GenerateTitle(EvaluateResult er);
        protected abstract Color GenerateColor(EvaluateResult er,int index);

        #region IStatistics Members

        public Dictionary<string, frmChartObserver.StatisticsResult> Statistics(List<DataType.Rules.Evaluation.EvaluateResult> data)
        {
            try
            {
                Dictionary<string, frmChartObserver.StatisticsResult> dctAnalysis = new Dictionary<string, frmChartObserver.StatisticsResult>();

                foreach (EvaluateResult er in data)
                {
                    string key = GenerateKey(er);
                    string title = GenerateTitle(er);                    

                    if (dctAnalysis.ContainsKey(key))
                    {
                        dctAnalysis[key].Count++;
                    }
                    else
                    {
                        frmChartObserver.StatisticsResult sr = new frmChartObserver.StatisticsResult();
                        sr.Count = 1;
                        sr.Title = title;
                        sr.BackColor = GenerateColor(er,dctAnalysis.Count);
                        dctAnalysis.Add(key, sr);
                    }

                    dctAnalysis[key].Results.Add(er);
                }

                foreach (frmChartObserver.StatisticsResult sr in dctAnalysis.Values)
                {
                    sr.Percentage = sr.Count * 1.0f / data.Count;
                }

                return dctAnalysis;
            }
            catch (Exception ex)
            {
                throw new Exception("统计日志分析结果失败，错误消息为：" + ex.Message);
            }
        }

        #endregion
    }
}
