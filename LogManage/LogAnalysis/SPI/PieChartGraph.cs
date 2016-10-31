using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LogManage.LogAnalysis.SPI
{
    internal class PieChartGraph : GraphBase
    {
        public override string GraphName
        {
            get
            {
                return "饼图";
            }
        }

        public override void DrawGraph(Graphics g)
        {
            SolidBrush sb=new SolidBrush(Color.White);

            try
            {
                foreach (GraphPart gp in m_regions.Values)
                {
                    sb.Color=gp.BGColor;

                    g.FillPath(sb, gp.PartRegion);
                }

                if(!string.IsNullOrEmpty(SelectedRegionKey) && m_regions.ContainsKey(SelectedRegionKey) )
                {
                    sb.Color = SystemColors.ButtonHighlight;

                    g.FillPath(sb, m_regions[SelectedRegionKey].PartRegion);
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("绘制饼图失败，错误消息为：" + ex.Message);
            }
            finally
            {
                sb.Dispose();
            }
        }

        protected override void RefreshRegions()
        {
            try
            {
                foreach (GraphPart gp in m_regions.Values)
                {
                    gp.PartRegion.Dispose();
                }

                m_regions.Clear();
                GC.Collect();

                float sumAngles = 0;

                if (m_results == null || m_results.Count <= 0)
                {
                    return;
                }

                foreach (string s in m_results.Keys)
                {
                    GraphPart gp = new GraphPart();
                    gp.Tag = s;
                    gp.BGColor = m_results[s].BackColor;

                    float angle=Convert.ToSingle(m_results[s].Percentage * 360);
                    gp.PartRegion.AddPie(m_bound.X,m_bound.Y,m_bound.Width,m_bound.Height, sumAngles, angle);
                    sumAngles += angle;

                    m_regions.Add(s, gp);
                }
            }
            catch (Exception ex)            
            {
                throw new Exception("计算图标绘图区域失败，错误消息为：" + ex.Message);
            }
        }
    }
}
