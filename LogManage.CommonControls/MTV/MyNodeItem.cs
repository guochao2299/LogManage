using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LogManage.CommonControls.MTV
{
    public class MyNodeItem
    {
        public MyNodeItem(string content)
        {
            m_text = content;
        }

        private string m_text = string.Empty;

        /// <summary>
        /// 节点项的内容
        /// </summary>
        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                m_text = value;
            }
        }

        private bool m_isSelected=false;

        /// <summary>
        /// 节点项是否被选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return m_isSelected;
            }
            set
            {
                m_isSelected = value;
            }
        }

        private RectangleF m_bound = new RectangleF();

        public RectangleF Bound
        {
            get
            {
                return m_bound;
            }
            set
            {
                m_bound.X=value.X;
                m_bound.Y = value.Y;
                m_bound.Width = value.Width;
                m_bound.Height = value.Height;
            }
        }

        public void UpdateSize(Font f,float minWidth)
        {
            m_bound.Height = f.Height * 1.5f;

            if (string.IsNullOrEmpty(this.Text))
            {
                m_bound.Width = minWidth;
            }
            else
            {
                m_bound.Width = TextRenderer.MeasureText(this.Text, f).Width;
                if (m_bound.Width < minWidth)
                {
                    m_bound.Width = minWidth;
                }
            }            
        }
     
        public void DrawItem(Graphics g,PointF pStart,Color bgColor,Font f,Color foreColor,StringFormat sf,bool hasBound)
        {
            if (g==null)
            {
                return;
            }

            m_bound.X=pStart.X;
            m_bound.Y=pStart.Y;

            if (hasBound)
            {
                g.DrawRectangle(Pens.Black,m_bound.X,m_bound.Y,m_bound.Width,m_bound.Height);
            }

            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }
            
            SolidBrush sb=new SolidBrush(bgColor);
            g.FillRectangle(sb,m_bound);
            sb.Dispose();
            sb=new SolidBrush(foreColor);

            if (sf == null)
            {
                g.DrawString(this.Text, f, sb, m_bound);
            }
            else
            {
                g.DrawString(this.Text, f, sb, m_bound, sf);
            }

            sb.Dispose();
        }
    }
}
