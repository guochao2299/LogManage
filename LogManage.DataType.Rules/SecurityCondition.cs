using System;
using System.Collections.Generic;
using System.Text;

// 表示

namespace LogManage.DataType.Rules
{
    /// <summary>
    /// 表示描述用户行为的一个组成条件，多个条件的合集构成一个用户行为
    /// </summary>
    [Serializable]
    public class SecurityCondition:ICloneable
    {
        /// <summary>
        /// content中的分隔符
        /// </summary>
        public const string SplitString = ";";

        public SecurityCondition()
        {}

        private int m_colSource=-1;
        
        /// <summary>
        /// 条件关联的列，这个必须存在，不能为负值
        /// </summary>
        public int SourceCol
        {
            get
            {
                return m_colSource;
            }
            set
            {
                //if(value<0)
                //{
                //    throw new Exception("条件相关的列号不能为负值");
                //}

                m_colSource=value;
            }
        }

        private int m_colDestination=-1;

        /// <summary>
        /// 给此项赋值之前必须先将IsUsingDestCol设置为true，此属性为可选项，当条件为日志的两个列之间比较关系时，本属性保存第二个列的列号
        /// </summary>
        public int DestinationCol
        {
            get
            {
                return m_colDestination;
            }
            set
            {
                //if(m_isUsingDestCol &&  value<0)
                //{
                //    throw new Exception("条件相关的第二个列号不能为负值");
                //}

                m_colDestination=value;
            }
        }
        private bool m_isUsingDestCol=false;
        
        /// <summary>
        /// 为true指明当前条件是两个列之间做比较
        /// </summary>
        public bool IsUsingDestCol
        {
            get
            {
                return m_isUsingDestCol;
            }
            set
            {
                m_isUsingDestCol=value;
            }
        }

        private string m_relationName = string.Empty;

        /// <summary>
        /// 条件所用到的关系，比如等于、大于、小于等，如果为空则不执行此条件
        /// </summary>
        public string RelationName
        {
            get
            {
                return m_relationName;
            }
            set
            {
                m_relationName = value;
            }
        }

        private List<string> m_colValues = new List<string>();

        /// <summary>
        /// 如果条件只用到一个值，则本属性安全值先后顺序，从左到右，从上到下进行存储，保存到数据库时，在统一处理，多个值之间用“;”号连接
        /// </summary>
        public List<string> MultiValues
        {
            get
            {
                return m_colValues;
            }
            set
            {
                m_colValues = value;
            }
        }

        public string GetContent()
        {
            return m_colValues.Count > 0 ? m_colValues[0] : string.Empty;
        }

        public void SetContent(string content)
        {
            m_colValues.Clear();
            m_colValues.Add(content);
        }

        public void SetMultiValues(List<string> lstNewValues)
        {
            MultiValues.Clear();

            foreach (String s in lstNewValues)
            {
                MultiValues.Add(s);
            }
        }

        private string m_guid = string.Empty;
        public string ConditionGuid
        {
            get
            {
                return m_guid;
            }
            set
            {
                m_guid = value;
            }
        }

        public void CopyFrom(SecurityCondition sc)
        {
            this.IsUsingDestCol = sc.IsUsingDestCol;
            this.DestinationCol = sc.DestinationCol;            
            this.RelationName = sc.RelationName;
            this.SourceCol = sc.SourceCol;

            this.MultiValues.Clear();
            foreach (string s in sc.MultiValues)
            {
                this.MultiValues.Add(s);
            }
        }

        public object Clone()
        {
            SecurityCondition sc = new SecurityCondition();
            sc.DestinationCol = this.DestinationCol;
            sc.IsUsingDestCol = this.IsUsingDestCol;
            sc.RelationName = this.RelationName;
            sc.m_colSource = this.SourceCol;
            sc.ConditionGuid = this.ConditionGuid;
                        
            foreach (string s in this.MultiValues)
            {
                sc.MultiValues.Add(s);
            }
           
            return sc;
        }

        public static SecurityCondition CreateNewSecurityCondition()
        {
            SecurityCondition condition = new SecurityCondition();
            condition.ConditionGuid = Guid.NewGuid().ToString();

            return condition;
        }

        public static SecurityCondition CreateSecurityCondition(string guid)
        {
            SecurityCondition condition = new SecurityCondition();
            condition.ConditionGuid = guid;

            return condition;
        }
    }

    public class SaveConditionEventArgs : EventArgs
    {
        public SaveConditionEventArgs()
        { }

        public SecurityCondition Condition;
    }
}
