using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage.SelfDefineControl
{
    public partial class DrawBoard : ScrollableControl
    {
        public DrawBoard()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }
    }
}
