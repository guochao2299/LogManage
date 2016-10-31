using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LogManage.LogAnalysis.SPI
{
    internal sealed class BarChartGraph:GraphBase
    {
        public override string GraphName
        {
            get
            {
                return "柱状图";
            }
        }

        public override void SetClientSize(Size s)
        {
            m_graphMiniSize = s;

            m_bound.Size = s;

            RefreshRegions();
        }

        protected override void RefreshRegions()
        {
            FontFamily ff=new FontFamily(FontName);

            StringFormat sf=new StringFormat();

            Font f=new Font(FontName,FontSize);

            try
            {
                foreach (GraphPart gp in m_regions.Values)
                {
                    gp.PartRegion.Dispose();
                }

                m_regions.Clear();
                GC.Collect();

                float sumLength = 0;
                m_graphMiniSize.Height = Convert.ToInt32(m_bound.X);
                m_graphMiniSize.Width = 0;

                if (m_results == null || m_results.Count <= 0)
                {
                    return;
                }

                sf.Alignment= StringAlignment.Center;
                sf.LineAlignment= StringAlignment.Far;

                int maxCount = 0;
                foreach (string s in m_results.Keys)
                {
                    if (m_results[s].Count > maxCount)
                    {
                        maxCount = m_results[s].Count;
                    }
                }

                maxCount+=maxCount/2;// 这里只是为了柱状图的柱不到顶

                float perHeight = (m_bound.Height - 2 * DistanceFromSide) * 1.0f / maxCount;

                sumLength += DistanceFromSide + AxisWidth + DistanceFromAxis;

                foreach (string s in m_results.Keys)
                {
                    frmChartObserver.StatisticsResult sr=m_results[s];
                    GraphPart gp = new GraphPart();
                    gp.Tag = s;
                    gp.BGColor = sr.BackColor;
                    gp.PartRegion.AddRectangle(new RectangleF(sumLength, DistanceFromSide + (maxCount - sr.Count) * perHeight-DistanceFromAxis,
                        BarWidth, sr.Count * perHeight));
                    gp.PartRegion.AddString(sr.Count.ToString(), ff, BarFontStyle, FontSize,
                        new RectangleF(sumLength, DistanceFromSide + (maxCount - sr.Count) * perHeight - DistanceFromAxis - f.Height * 2,
                            BarWidth, f.Height * 2), sf);

                    sumLength += DistanceBetweenBars + BarWidth;
                    m_regions.Add(s, gp);
                }

                m_graphMiniSize.Width = Convert.ToInt32(sumLength);

                if (m_graphMiniSize.Width < m_bound.Width)
                {
                    m_graphMiniSize.Width = Convert.ToInt32(m_bound.Width);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("计算柱状图绘图区域失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if(ff!=null)
                {
                    ff.Dispose();
                }

                if(f!=null)
                {
                    f.Dispose();
                }

                if(sf!=null)
                {
                    sf.Dispose();
                }
            }
        }

        private const int DistanceBetweenBars = 10;
        private const int BarWidth = 40;
        private const int DistanceFromSide = 40;
        private const int DistanceFromAxis = 5;
        private const int AxisWidth = 5;
        private readonly Color AxisColor = Color.DarkBlue;
        private const string FontName = "宋体";
        private const int FontSize = 25;
        private const int BarFontStyle = 2;

        public override void DrawGraph(Graphics g)
        {
            Pen pAxis = new Pen(AxisColor, AxisWidth);
            SolidBrush sb = new SolidBrush(Color.White);

            try
            {
                // 先绘制坐标轴
                pAxis.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(pAxis, m_bound.X + DistanceFromSide,
                    m_bound.Y + m_bound.Height - DistanceFromSide,
                    m_bound.X + DistanceFromSide, m_bound.Y + DistanceFromSide);
                g.DrawLine(pAxis, m_bound.X + DistanceFromSide,
                    m_bound.Y + m_bound.Height - DistanceFromSide,
                    m_bound.X + m_graphMiniSize.Width,
                    m_bound.Y + m_bound.Height - DistanceFromSide);

                foreach (GraphPart gp in m_regions.Values)
                {
                    sb.Color = gp.BGColor;

                    g.FillPath(sb, gp.PartRegion);
                }

                if (!string.IsNullOrEmpty(SelectedRegionKey) && m_regions.ContainsKey(SelectedRegionKey))
                {
                    sb.Color = SystemColors.ButtonHighlight;

                    g.FillPath(sb, m_regions[SelectedRegionKey].PartRegion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("绘制柱状图失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (sb != null)
                {
                    sb.Dispose();
                }

                if (pAxis != null)
                {
                    pAxis.Dispose();
                }
            }
        }
    }
}
