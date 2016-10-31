using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LogManage.CommonControls.MTV
{
    public class MyTreeNode
    {
        public const float MIN_ITEM_SIZE = 20;
        private const float FOLD_INDECATOR_BOX_SIZE = 5;

        public MyTreeNode(string content)
        { 

        }

        public MyTreeNode()
        {

        }

        private List<MyNodeItem> m_items = new List<MyNodeItem>();
        public List<MyNodeItem> Items
        {
            get
            {
                return m_items;
            }
            set
            {
                m_items.Clear();

                if (value != null)
                {
                    m_items.AddRange(value);
                }
            }
        }

        private List<MyTreeNode> m_nodes = new List<MyTreeNode>();
        public List<MyTreeNode> Nodes
        {
            get
            {
                return m_nodes;
            }
            set
            {
                m_nodes.Clear();

                if (value != null)
                {
                    m_nodes.AddRange(m_nodes);
                }
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
                m_bound.X = value.X;
                m_bound.Y = value.Y;
                m_bound.Width = value.Width;
                m_bound.Height = value.Height;
            }
        }

        public void UpdateSize(Font f)
        {
            m_bound.Height = f.Height * 1.5f;
            m_bound.Width = 0;

            foreach (MyNodeItem item in this.Items)
            {
                item.UpdateSize(f, MIN_ITEM_SIZE);
                m_bound.Width += item.Bound.Width;
            }
        }

        public void DrawNode(PointF pStart, Color bgColor, Font f, Color foreColor, StringFormat sf, bool hasBound)
        {
            try
            {
                bool hasSubNode = (this.Nodes != null && this.Nodes.Count > 0);


            }
            catch (Exception ex)
            {
                throw new Exception("绘制treeview节点失败，错误消息为：" + ex.Message);
            }
        }
    }
}
