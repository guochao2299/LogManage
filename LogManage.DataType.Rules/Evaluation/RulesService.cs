using System;
using System.Collections.Generic;
using System.Text;

using LogManage.DataType.Rules.Evaluation.SPI;

namespace LogManage.DataType.Rules.Evaluation
{
    public class RulesService
    {
        private RulesService()
        {
            InitProcessors();
        }

        private static RulesService m_instance = null;

        public static RulesService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new RulesService();
                }

                return m_instance;
            }
        }

        private List<IRuleProcessor> m_processors = new List<IRuleProcessor>();

        public void InitProcessors()
        {
            m_processors.Clear();

            m_processors.Add(new ImmediatelyReturnRuleProcessor());

            // 从指定文件夹中加载可用的策略处理程序

        }

        private List<IRuleProcessor> m_immediatelyReturnProcessor = null;
        private List<IRuleProcessor> m_finalReturnProcessor = null;

        public List<IRuleProcessor> ImmediatelyReturnProcessors
        {
            get
            {
                if (m_immediatelyReturnProcessor == null)
                {
                    m_immediatelyReturnProcessor = new List<IRuleProcessor>();

                    foreach (IRuleProcessor p in m_processors)
                    {
                        if (p.IsImmediatelyReturn)
                        {
                            m_immediatelyReturnProcessor.Add(p);
                        }
                    }
                }

                return m_immediatelyReturnProcessor;
            }
        }

        public List<IRuleProcessor> FinalReturnProcessors
        {
            get
            {
                if (m_finalReturnProcessor == null)
                {
                    m_finalReturnProcessor = new List<IRuleProcessor>();

                    foreach (IRuleProcessor p in m_processors)
                    {
                        if (!p.IsImmediatelyReturn)
                        {
                            m_finalReturnProcessor.Add(p);
                        }
                    }
                }

                return m_finalReturnProcessor;
            }
        }
    }
}
