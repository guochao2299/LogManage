using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules
{
    /// <summary>
    /// 代表用户的一个行为，每个安全事件的要包含一个或者多个行为，目前认为这些行为都造成相同的后果
    /// </summary>
    [Serializable]
    public class SecurityAction:ICloneable
    {
        public SecurityAction(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                throw new NullReferenceException("用户行为唯一标示符不能为空");
            }

            m_guid = guid;
        }

        public SecurityAction()
        { }

        private string m_guid = string.Empty;

        /// <summary>
        /// 安全事件的唯一标示符
        /// </summary>
        public string ActionGuid
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

        private string m_resultRuid = string.Empty;

        /// <summary>
        /// 用户行为对应的结果的guid
        /// </summary>
        public string ResultGuid
        {
            get
            {
                return m_resultRuid;
            }
            set
            {
                m_resultRuid = value;
            }
        }

        private string m_name = string.Empty;

        /// <summary>
        /// 用户行为名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("用户行为名称必须为有意义的名称");
                }

                m_name = value;
            }
        }

        private string m_description = string.Empty;

        /// <summary>
        /// 用户行为描述
        /// </summary>
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }

        private List<SecurityCondition> m_conditions = new List<SecurityCondition>();

        /// <summary>
        /// 描述用户行为的条件集合，条件与条件之间是and的关系
        /// </summary>
        public List<SecurityCondition> Conditions
        {
            get
            {
                return m_conditions;
            }
        }

        public object Clone()
        {
            SecurityAction sa = this.LowerClone();            

            foreach (SecurityCondition sc in this.Conditions)
            {
                sa.Conditions.Add((SecurityCondition)sc.Clone());
            }

            return sa;
        }

        public SecurityAction LowerClone()
        {
            SecurityAction sa = new SecurityAction();            
            sa.ResultGuid = this.ResultGuid;
            sa.Description = this.Description;
            sa.Name = this.Name;
            sa.ActionGuid = this.ActionGuid;

            return sa;
        }

        /// <summary>
        /// 仅复制行为的属性，不包含条件集合
        /// </summary>
        /// <param name="sa"></param>
        public void CopyFrom(SecurityAction sa)
        {
            this.Description = sa.Description;
            this.Name = sa.Name;
            this.ResultGuid = sa.ResultGuid;            
        }

        public SecurityCondition GetCondition(string conditionGuid)
        {
            foreach (SecurityCondition sc in this.Conditions)
            {
                if (string.Equals(sc.ConditionGuid,conditionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return sc;                         
                }
            }

            return null;
        }

        public void DeleteCondition(string conditinGuid)
        {
            for (int i = m_conditions.Count - 1; i >= 0; i--)
            {
                if (string.Equals(conditinGuid, m_conditions[i].ConditionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    m_conditions.RemoveAt(i);
                    return;
                }
            }
        }

        public void DeleteConditions(List<string> conditinGuids)
        {
            foreach (string conditionGuid in conditinGuids)
            {
                DeleteCondition(conditionGuid);
            }
        }

        /// <summary>
        /// 添加新的条件，深度复制，如果条件已经存在，则直接返回，不添加
        /// </summary>
        /// <param name="conditionGuid"></param>
        public void AddCondition(SecurityCondition sc)
        {
            if (!ContainsCondition(sc.ConditionGuid))
            {
                this.Conditions.Add((SecurityCondition)sc.Clone());
            }
        }

        /// <summary>
        /// 更新条件属性，如果条件不存在则直接返回
        /// </summary>
        /// <param name="sc"></param>
        public void UpdateConditionProperties(SecurityCondition sc)
        {
            SecurityCondition initData = GetCondition(sc.ConditionGuid);
            if (initData != null)
            {
                initData.CopyFrom(sc);
            }
        }

        public bool ContainsCondition(string conditionGuid)
        {
            foreach (SecurityCondition sc in this.Conditions)
            {
                if (string.Equals(sc.ConditionGuid, conditionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static SecurityAction CreateNewSecurityAction(string name, string desc, string resultGuid)
        {
            SecurityAction sa = new SecurityAction();
            sa.ActionGuid = Guid.NewGuid().ToString();
            sa.Description = desc;
            sa.Name = name;
            sa.ResultGuid = resultGuid;

            return sa;
        }

        public static SecurityAction CreateSecurityAction(string name, string guid,string desc, string resultGuid)
        {
            SecurityAction sa = new SecurityAction();
            sa.ActionGuid = guid;
            sa.Description = desc;
            sa.Name = name;
            sa.ResultGuid = resultGuid;            

            return sa;
        }

    }
}
