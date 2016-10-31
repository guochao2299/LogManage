using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LogManage.LogAnalysis
{
    /// <summary>
    /// 表示图标中的一个组成部分
    /// </summary>
    internal sealed class GraphPart
    {
        private object m_tag = null;

        public object Tag
        {
            get
            {
                return m_tag;
            }
            set
            {
                m_tag = value;
            }
        }

        private GraphicsPath m_region=new GraphicsPath();

        public GraphicsPath PartRegion
        {
            get
            {
                return m_region;
            }
        }

        private Color m_bgColor = Color.White;
        public Color BGColor
        {
            get
            {
                return m_bgColor;
            }
            set
            {
                m_bgColor = value;
            }
        }
    }
}
