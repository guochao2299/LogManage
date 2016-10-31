using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LogManage.LogAnalysis
{
    public sealed class ColorsPool
    {
        private Random m_random = null;

        private ColorsPool()
        {
            InitColorsPool();

            m_random = new Random();
        }

        private static ColorsPool m_instance = null;

        public static ColorsPool Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ColorsPool();                    
                }

                return m_instance;
            }
        }

        private void InitColorsPool()
        {
            m_pools.Add(Color.Orange);
            m_pools.Add(Color.Red);
            m_pools.Add(Color.Yellow);
            m_pools.Add(Color.Green);
            m_pools.Add(Color.Blue);
            m_pools.Add(Color.Brown);
            m_pools.Add(Color.Purple);
            m_pools.Add(Color.Pink);
            m_pools.Add(Color.Gold);
            m_pools.Add(Color.Silver);
        }

        private List<Color> m_pools = new List<Color>();

        public Color GetColor(int index)
        {
            if (index >= 0 && index < m_pools.Count)
            {
                return m_pools[index];
            }

            return Color.FromArgb(m_random.Next(255), m_random.Next(255), m_random.Next(255));
        }    
    }
}
