using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LogManage.CommonControls.MTV
{
    public class MyTreeView : ScrollableControl
    {
        private MyTreeNode m_topNode = new MyTreeNode();

        public MyTreeView()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserPaint
                | ControlStyles.UserPaint , true);
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                UpdateNodeBound(m_topNode,value);
                this.Refresh();
            }
        }

        private void UpdateNodeBound(MyTreeNode node,Font f)
        {
            node.UpdateSize(f);

            if (node.Nodes == null || node.Nodes.Count <= 0)
            {
                return;
            }

            foreach (MyTreeNode subNode in node.Nodes)
            {
                UpdateNodeBound(subNode, f);
            }
        }
    }    
}
