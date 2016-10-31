using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis
{
    internal class AnalysisResultService
    {
        private List<IObserver> m_observers = new List<IObserver>();

        private AnalysisResultService()
        { 
        }

        private static AnalysisResultService m_instance = null;

        public static AnalysisResultService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new AnalysisResultService();
                }

                return m_instance;
            }
        }

        /// <summary>
        ///  注册完成后，会立即调用一次观察者的Notify函数，方便观察者获取现有的数据
        /// </summary>
        /// <param name="observer"></param>
        public void RegisterObserver(IObserver observer)
        {
            try
            {
                m_observers.Add(observer);

                observer.Notify(m_analysisResults);
            }
            catch (Exception ex)
            {
                throw new Exception("注册观察者失败，错误消息为：" + ex.Message);
            }
        }

        public void UnregisterObserver(IObserver observer)
        {
            try
            {
                if (m_observers.Contains(observer))
                {
                    m_observers.Remove(observer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("注销观察者失败，错误消息为：" + ex.Message);
            }
        }

        private List<EvaluateResult> m_analysisResults = new List<EvaluateResult>();

        private void NotifyObserver()
        {
            foreach (IObserver ob in m_observers)
            {
                ob.Notify(m_analysisResults);
            }
        }

        /// <summary>
        /// 重置分析结果，如果输入的结果集合为空，则将现有结果集合清空
        /// </summary>
        /// <param name="newResult"></param>
        public void ResetAnalysisResults(List<EvaluateResult> newResult)
        {
            // 这里有可能清空时，其他窗口还在有这个数据

            try
            {
                m_analysisResults.Clear();
                m_analysisResults.AddRange(newResult);
                NotifyObserver();
            }
            catch (Exception ex)
            {
                throw new Exception("重置日志分析结果失败，错误消息为：" + ex.Message);
            }
        }
    }
}
