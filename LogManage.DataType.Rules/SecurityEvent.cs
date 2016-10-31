using System;
using System.Collections.Generic;
using System.Text;

// 先给出一个安全事件的示例
// 事件名称：用户非法登录
// 事件等级：严重事件
// 事件行为：
//      1）用户登录非法计算机
//          条件1）：用户名 = gchao
//          条件2）：计算机名 = file
//      2）低密级用户登录高密级计算机
//          条件1）：用户密级=非密公开
//          条件2）：计算机密级=机密

namespace LogManage.DataType.Rules
{
    /// <summary>
    /// 一个安全事件，安全事件包含几种行为，行为之间为or的关系，每个事件都包含事件的结果
    /// </summary>
    [Serializable]
    public class SecurityEvent:ICloneable
    {
        public const string DefaultEventName = "默认安全事件";

        public SecurityEvent(string name,string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                throw new NullReferenceException("安全事件唯一标示符不能为空");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = DefaultEventName;
            }

            m_guid = guid;
            m_name = name;
        }

        public SecurityEvent()
        { }

        private string m_name = string.Empty;

        /// <summary>
        /// 安全事件的名称
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
                    throw new NullReferenceException("安全事件名称不能为空");
                }

                m_name = value;
            }
        }

        private string m_guid = string.Empty;

        /// <summary>
        /// 安全事件的唯一标示符
        /// </summary>
        public string EventGuid
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

        private string m_description=string.Empty;

        /// <summary>
        /// 对安全事件的描述
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

        private List<SecurityAction> m_actions = new List<SecurityAction>();

        /// <summary>
        /// 组成安全事件的用户行为集合，行为与行为之间是or的关系
        /// </summary>
        public List<SecurityAction> SecurityActions
        {
            get
            {
                return m_actions;
            }
        }

        public SecurityAction GetSecurityAction(string actionGuid)
        {
            foreach (SecurityAction sa in this.SecurityActions)
            {
                if (string.Equals(sa.ActionGuid, actionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return sa;
                }
            }

            return null;
        }

        /// <summary>
        /// 添加安全行为(深度复制)，如果安全事件中已经存在安全行为，则直接返回，不添加
        /// </summary>
        /// <param name="sa"></param>
        public void AddSecurityAction(SecurityAction sa)
        {
            if (ContainsAction(sa.ActionGuid))
            {
                return;
            }

            m_actions.Add((SecurityAction)sa.Clone());
        }

        public bool ContainsAction(string actionGuid)
        {
            foreach (SecurityAction sa in this.SecurityActions)
            {
                if (string.Equals(sa.ActionGuid, actionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 更新安全行为属性(不包含条件集合)，如果安全事件中不存在安全行为，则直接返回
        /// </summary>
        /// <param name="sa"></param>
        public void UpdateActionProperties(SecurityAction sa)
        {
            SecurityAction initData = GetSecurityAction(sa.ActionGuid);

            if (initData != null)
            {
                initData.CopyFrom(sa);
            }            
        }

        /// <summary>
        /// 删除安全行为，如果不存在则直接返回
        /// </summary>
        /// <param name="actionGuid"></param>
        public void DeleteAction(string actionGuid)
        {
            for (int i = m_actions.Count - 1; i >= 0; i--)
            {
                if (string.Equals(actionGuid, m_actions[i].ActionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    m_actions.RemoveAt(i);
                    return;
                }
            }
        }

        public static SecurityEvent CreateNewSecurityEvent(string name, string desc)
        {
            SecurityEvent se = new SecurityEvent();
            se.Name = name;
            se.Description = desc;
            se.EventGuid = Guid.NewGuid().ToString();

            return se;
        }

        public static SecurityEvent CreateSecurityEvent(string name,string guid, string desc)
        {
            SecurityEvent se = new SecurityEvent();
            se.Name = name;
            se.Description = desc;
            se.EventGuid = guid;

            return se;
        }

        public object Clone()
        {
            SecurityEvent se = this.LowerClone();
            //se.EventGuid = this.EventGuid;
            //se.Description = this.Description;
            //se.Name = this.Name;

            foreach (SecurityAction sa in this.SecurityActions)
            {
                se.SecurityActions.Add((SecurityAction)sa.Clone());
            }

            return se;
        }

        /// <summary>
        /// 没有复制用户行为信息
        /// </summary>
        /// <returns></returns>
        public SecurityEvent LowerClone()
        {
            SecurityEvent se = new SecurityEvent();
            se.EventGuid = this.EventGuid;
            se.Description = this.Description;
            se.Name = this.Name;

            return se;
        }

        public void CopyFrom(SecurityEvent se)
        {
            this.Name = se.Name;
            this.Description = se.Description;            
        }
    }
}
