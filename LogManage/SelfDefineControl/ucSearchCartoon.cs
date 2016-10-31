using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LogManage.SelfDefineControl
{
    internal partial class ucSearchCartoon : UserControl
    {
        private int m_index = 0;
        private RectangleF m_bound = new RectangleF(0, 0,1,1);
        private RectangleF m_searcherBound = new RectangleF(0, 0, 1, 1);
        private readonly float[,] MoveOrbit = new float[,]{{0.8F,0},{0.8F,-0.2f},{0.8F,-0.4f},{0.8F,-0.6f},{0.8F,-0.8f},
                                                          {0.6F,-0.8f},{0.4F,-0.8f},{0.2F,-0.8f},{0,-0.8f},
                                                          {-0.2F,-0.8f},{-0.4F,-0.8f},{-0.6F,-0.8f},{-0.8F,-0.8f},
                                                          {-0.8F,-0.6f},{-0.8F,-0.4f},{-0.8F,-0.2f},{-0.8F,0},
                                                          {-0.8F,0.2f},{-0.8F,0.4f},{-0.8F,0.6f},{-0.8F,0.8f},
                                                          {-0.6F,0.8f},{-0.4F,0.8f},{-0.2F,0.8f},{0,0.8f},
                                                          {0.2F,0.8f},{0.4F,0.8f},{0.6F,0.8f},{0.8F,0.8f},
                                                          {0.8F,0.6f},{0.8F,0.4f},{0.8F,0.2f}};

        public ucSearchCartoon()
        {
            InitializeComponent();

            CalclBound();

            CalclSearcherPosition();        

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        private void CalclSearcherPosition()
        {
            m_searcherBound.X = Convert.ToSingle(m_bound.Width * 0.5F * MoveOrbit[m_index,1]);
            m_searcherBound.Y = Convert.ToSingle(m_bound.Width * 0.5F * MoveOrbit[m_index,0]);
        }

        private void CalclBound()
        {
            m_bound.X = Convert.ToSingle(this.Width / 4.0);
            m_bound.Y =  Convert.ToSingle(this.Height / 4.0);

            m_bound.Width = 2 * (m_bound.X > m_bound.Y ? m_bound.Y : m_bound.X);
            m_bound.Height = m_bound.Width;

            m_searcherBound.Width = m_bound.Width / 2;
            m_searcherBound.Height = m_searcherBound.Width;            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_index++;

            if (m_index >= MoveOrbit.GetLength(0))
            {
                m_index = 0;
            }

            CalclSearcherPosition();
        
            Invalidate(this.ClientRectangle);
        }

        private void ucSearchCartoon_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            e.Graphics.DrawImage(LogManage.Properties.Resources.board, m_bound);

            e.Graphics.TranslateTransform(m_bound.X + m_bound.Width * 0.5F, m_bound.Y + m_bound.Width * 0.5F);

            e.Graphics.DrawImage(LogManage.Properties.Resources.searcher, m_searcherBound);

            e.Graphics.ResetTransform();
        }

        private void ucSearchCartoon_Resize(object sender, EventArgs e)
        {
            CalclBound();
            CalclSearcherPosition();
            Invalidate(this.ClientRectangle);
        }

        public void StartCartoon()
        {
            this.timer1.Enabled = true;
        }

        public void PauseCartoon()
        {
            this.timer1.Enabled = false;
        }

        public void ResetCartoon()
        {
            this.timer1.Enabled = false;
            m_index = 0;
        }

        public void ContinueCartoon()
        {
            this.timer1.Enabled = true;
        }
    
    }
}
