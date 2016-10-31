using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using LogManage.DataType.Rules.Evaluation;

namespace LogManage.LogAnalysis
{
    internal interface IGraph
    {
        void InitData(Dictionary<string, frmChartObserver.StatisticsResult> results);

        void SetClientSize(Size s);

        Size GetGraphMiniSize();

        void DrawGraph(Graphics g);

        frmChartObserver.StatisticsResult HitTest(Point p);

        string SelectedRegionKey { get; set; }

        string GraphName { get; }
    }

    internal abstract class GraphBase:IGraph
    {
        protected RectangleF m_bound = new RectangleF();
        protected Size m_graphMiniSize;
        protected Dictionary<string, frmChartObserver.StatisticsResult> m_results = null;
        protected Dictionary<string, GraphPart> m_regions = new Dictionary<string, GraphPart>();
        protected string m_selectedRegionKey = string.Empty;
        
        public string SelectedRegionKey
        {
            get
            {
                return m_selectedRegionKey;
            }
            set
            {
                m_selectedRegionKey = value;
            }
        }

        public virtual String GraphName
        {
            get
            {
                return "统计图表";
            }
        }

        #region IGraph Members

        protected abstract void RefreshRegions();

        public virtual void InitData(Dictionary<string, frmChartObserver.StatisticsResult> results)
        {
            m_results=results;
            RefreshRegions();
        }

        public virtual void SetClientSize(Size s)
        {
            m_graphMiniSize = s;

            int lMin = s.Width > s.Height ? s.Height : s.Width;

            m_bound.Width = lMin * 0.6f;
            m_bound.Height = m_bound.Width;

            m_bound.X = (s.Width - m_bound.Width) / 2;
            m_bound.Y = (s.Height - m_bound.Height) / 2;

            RefreshRegions();
        }

        public virtual Size GetGraphMiniSize()
        {
            return m_graphMiniSize;
        }             

        public abstract void DrawGraph(Graphics g);

        public virtual frmChartObserver.StatisticsResult HitTest(Point p)
        {
            if (m_results == null || m_results.Count <= 0)
            {
                return null;
            }

            foreach (string key in m_results.Keys)
            {
                if (m_regions[key].PartRegion.IsVisible(p))
                {
                    m_selectedRegionKey = key;
                    return m_results[key];
                }
            }

            m_selectedRegionKey = string.Empty;

            return null;
        }

        #endregion
    }

    internal sealed class NullGraph : GraphBase
    {

        protected override void RefreshRegions()
        {

        }

        public override void DrawGraph(Graphics g)
        {
            
        }
    }
}
