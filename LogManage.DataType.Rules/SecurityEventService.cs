using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules
{
    public class SecurityEventService
    {
        private SecurityEventService()
        { }

        private IManageRuleData m_manager = null;

        public IManageRuleData DBManager
        {
            get
            {
                return m_manager;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("安全事件管理对象引用不能为空");
                }

                if (m_manager == null)
                {
                    m_manager = value;
                }
            }
        }

        private static SecurityEventService m_instance = null;

        public static SecurityEventService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SecurityEventService();
                }

                return m_instance;
            }
        }

        private Dictionary<string, SecurityEvent> m_rules = null;

        /// <summary>
        /// 获取可用的规则，尽量不用此属性操作规则集合，最好调用AddEvent、UpdateEvent和DeleteEvent
        /// </summary>
        public Dictionary<string, SecurityEvent> AvaliableEvents
        {
            get
            {
                if (m_rules == null)
                {
                    m_rules = new Dictionary<string, SecurityEvent>();

                    foreach(SecurityEvent se in m_manager.GetDefinedSecurityEvents())
                    {
                        m_rules.Add(se.EventGuid,se);
                    }
                }

                return m_rules;
            }
        }

        /// <summary>
        /// 添加安全事件，如果已经存在事件Guid，则不予添加
        /// </summary>
        /// <param name="sEvent"></param>
        /// <returns></returns>
        public void AddSecurityEvent(SecurityEvent sEvent)
        {
            try
            {
                if (sEvent == null || string.IsNullOrWhiteSpace(sEvent.EventGuid))
                {
                    throw new Exception("安全事件或者安全事件的guid不能为空");
                }

                if (m_rules.ContainsKey(sEvent.EventGuid))
                {
                    throw new Exception(string.Format("安全事件{1}的guid已经存在", sEvent.Name));
                }

                m_manager.CreateNewSecurityEvent(sEvent);

                m_rules.Add(sEvent.EventGuid, sEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("添加安全事件失败，错误消息为：" + ex.Message);
            }            
        }

        public SecurityEvent GetSecurityEvent(string eventGuid)
        {
            try
            {
                if (!m_rules.ContainsKey(eventGuid))
                {
                    throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
                }

                return m_rules[eventGuid];
            }
            catch (Exception ex)
            {
                throw new Exception("获取安全事件失败，错误消息为：" + ex.Message);
            }            
        }

        /// <summary>
        /// 更新安全事件
        /// </summary>
        /// <param name="sEvent"></param>
        /// <returns></returns>
        public void UpdateSecurityEvent(SecurityEvent sEvent)
        {
            try
            {
                if (!m_rules.ContainsKey(sEvent.EventGuid))
                {
                    throw new Exception(string.Format("要更新的安全事件{1}的不存在", sEvent.Name));
                }

                m_manager.UpdateSecurityEventProperties(sEvent);

                m_rules[sEvent.EventGuid].CopyFrom(sEvent);
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全事件属性失败，错误消息为：" + ex.Message);
            }            
        }
        
        /// <summary>
        /// 删除安全事件
        /// </summary>
        /// <param name="eventGuid"></param>
        public void DeleteSecurityEvent(string eventGuid)
        {
            try
            {
                m_manager.DeleteSecurityEvent(eventGuid);

                if (m_rules.ContainsKey(eventGuid))
                {
                    m_rules.Remove(eventGuid);
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("删除安全事件失败，错误消息为：" + ex.Message);
            } 
        }

        public SecurityAction GetSecurityAction(string eventGuid,string actionGuid)
        {
            try
            {
                if (!m_rules.ContainsKey(eventGuid))
                {
                    throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
                }

                SecurityAction result = m_rules[eventGuid].GetSecurityAction(actionGuid);

                if (result == null)
                {
                    throw new Exception(string.Format("安全事件{0}中不存在安全行为{1}", eventGuid,actionGuid));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("获取安全事件失败，错误消息为：" + ex.Message);
            }
        }

        #region 用户定义的用户行为结果管理

        private Dictionary<string, SecurityActionResult> m_actionResults = null;

        /// <summary>
        /// 获取系统中定义的所有行为后果
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, SecurityActionResult> DefinedActionResults
        {
            get
            {
                if (m_actionResults == null)
                {
                    m_actionResults = new Dictionary<string, SecurityActionResult>();

                    foreach (SecurityActionResult ar in m_manager.GetDefinedActionResults())
                    {
                        m_actionResults.Add(ar.ResultGuid, ar);
                    }
                }

                return m_actionResults;
            }
        }

        public SecurityActionResult GetSecurityActionResult(string resultGuid)
        {
            if (!DefinedActionResults.ContainsKey(resultGuid))
            {
                throw new Exception("用户行为相关的后果guid不存在");
            }

            return DefinedActionResults[resultGuid];
        }

        public bool IsSecurityActionResultExisting(string resultGuid)
        {
            return DefinedActionResults.ContainsKey(resultGuid);
        }

        public void UpdateResults(List<SecurityActionResult> results)
        {
            m_manager.UpdateResults(results);

            foreach (SecurityActionResult ar in results)
            {
                DefinedActionResults[ar.ResultGuid].CopyFrom(ar);
            }
        }

        public void InsertResults(List<SecurityActionResult> results)
        {
            m_manager.InsertResults(results);

            foreach (SecurityActionResult ar in results)
            {
                DefinedActionResults.Add(ar.ResultGuid, (SecurityActionResult)ar.Clone());
            }
        }

        public void DeleteResults(List<string> lstGuids)
        {
            m_manager.DeleteResults(lstGuids);

            foreach (string s in lstGuids)
            {
                DefinedActionResults.Remove(s);
            }
        }

        /// <summary>
        /// 获取使用了指定行为结果的规则集合，返回规则guid集合，用分号隔开
        /// </summary>
        /// <param name="resultGuid"></param>
        /// <returns></returns>
        public string GetRulesOfUsingTheResult(string resultGuid)
        {
            return m_manager.GetRulesOfUsingTheResult(resultGuid);
        }
        #endregion

        #region 安全行为

        /// <summary>
        /// 新建安全行为
        /// </summary>
        /// <param name="se"></param>
        public void AddSecurityAction(string eventGuid, SecurityAction sa)
        {
            try
            {
                if (!m_rules.ContainsKey(eventGuid))
                {
                    throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
                }

                SecurityEvent se = m_rules[eventGuid];                

                if (se.ContainsAction(sa.ActionGuid))
                {
                    throw new Exception(string.Format("安全事件{0}中已经存在安全行为{1}", eventGuid, sa.Name));
                }

                m_manager.CreateNewSecurityAction(eventGuid, sa);
                se.AddSecurityAction(sa);
            }
            catch (Exception ex)
            {
                throw new Exception("新建安全行为失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 更新安全行为属性
        /// </summary>
        /// <param name="se"></param>
        public void UpdateSecurityActionProperties(string eventGuid, SecurityAction sa)
        {
            try
            {
                if (!m_rules.ContainsKey(eventGuid))
                {
                    throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
                }

                SecurityEvent se = m_rules[eventGuid];

                if (!se.ContainsAction(sa.ActionGuid))
                {
                    throw new Exception(string.Format("安全事件{0}中不存在安全行为{1}", eventGuid, sa.Name));
                }

                m_manager.UpdateSecurityActionProperties(eventGuid, sa);

                se.GetSecurityAction(sa.ActionGuid).CopyFrom(sa);
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全行为属性失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除指定的安全行为,包含条件
        /// </summary>
        /// <param name="eventGuid"></param>
        public void DeleteSecurityAction(string eventGuid, string actionGuid)
        {
            try
            {
                if (!m_rules.ContainsKey(eventGuid))
                {
                    throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
                }

                SecurityEvent se = m_rules[eventGuid];

                if (!se.ContainsAction(actionGuid))
                {
                    throw new Exception(string.Format("安全事件{0}中不存在安全行为{1}", eventGuid, actionGuid));
                }

                m_manager.DeleteSecurityAction(eventGuid, actionGuid);
                se.DeleteAction(actionGuid);
            }
            catch (Exception ex)
            {
                throw new Exception("删除指定的安全行为失败，错误消息为：" + ex.Message);
            }
        }
        #endregion

        #region 安全行为包含的条件

        /// <summary>
        /// 检查安全事件和安全行为是否存在，不存在抛异常，存在则返回安全行为的引用
        /// </summary>
        /// <param name="eventGuid"></param>
        /// <param name="actionGuid"></param>
        /// <returns></returns>
        private SecurityAction BasicCheck(string eventGuid, string actionGuid)
        {
            if (!m_rules.ContainsKey(eventGuid))
            {
                throw new Exception(string.Format("安全事件的guid{0}不存在", eventGuid));
            }

            SecurityAction sa = m_rules[eventGuid].GetSecurityAction(actionGuid);

            if (sa == null)
            {
                throw new Exception(string.Format("安全事件{0}中不存在安全行为{1}", eventGuid, actionGuid));
            }

            return sa;
        }

        /// <summary>
        /// 获取安全事件中某一行为的条件集合
        /// </summary>
        public SecurityCondition GetSecurityCondition(string eventGuid, string actionGuid, string conditionGuid)
        {
            try
            {
                SecurityAction sa = BasicCheck(eventGuid, actionGuid);
                SecurityCondition sc = sa.GetCondition(conditionGuid);

                if (sc == null)
                {
                    throw new Exception(string.Format("安全事件{0}的安全行为{1}中不存在条件{2}",
                        new object[]{eventGuid,actionGuid,conditionGuid}));
                }

                return sc;
            }
            catch (Exception ex)
            {
                throw new Exception("获取安全条件失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 新建安全行为条件
        /// </summary>
        /// <param name="se"></param>
        public void AddSecurityCondition(string eventGuid,string actionGuid, SecurityCondition sc)
        {
            try
            {
                SecurityAction sa = BasicCheck(eventGuid, actionGuid);               

                if (sa.ContainsCondition(sc.ConditionGuid))
                {
                    throw new Exception(string.Format("安全事件{0}的安全行为{1}中已经存在条件{2}",
                        new object[] { eventGuid, actionGuid, sc.ConditionGuid }));
                }

                m_manager.CreateNewSecurityCondition(actionGuid, sc);
                sa.AddCondition(sc);
            }
            catch (Exception ex)
            {
                throw new Exception("新建安全行为条件失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 更新安全行为条件
        /// </summary>
        /// <param name="se"></param>
        public void UpdateSecurityConditionProperties(string eventGuid,string actionGuid, SecurityCondition sc)
        {
            try
            {
                SecurityAction sa = BasicCheck(eventGuid, actionGuid);

                if (!sa.ContainsCondition(sc.ConditionGuid))
                {
                    throw new Exception(string.Format("安全事件{0}的安全行为{1}中不存在条件{2}",
                        new object[] { eventGuid, actionGuid, sc.ConditionGuid }));
                }

                m_manager.UpdateSecurityConditionProperties(actionGuid,sc);
                sa.UpdateConditionProperties(sc);
            }
            catch (Exception ex)
            {
                throw new Exception("更新安全行为条件失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除指定的安全行为条件
        /// </summary>
        /// <param name="eventGuid"></param>
        public void DeleteSecurityCondition(string eventGuid,string actionGuid, string conditonGuid)
        {
            try
            {
                SecurityAction sa = BasicCheck(eventGuid, actionGuid);

                if (!sa.ContainsCondition(conditonGuid))
                {
                    throw new Exception(string.Format("安全事件{0}的安全行为{1}中不存在条件{2}",
                        new object[] { eventGuid, actionGuid, conditonGuid }));
                }

                m_manager.DeleteSecurityCondition(actionGuid, conditonGuid);

                sa.DeleteCondition(conditonGuid);
            }
            catch (Exception ex)
            {
                throw new Exception("删除指定的安全行为条件失败，错误消息为：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除指定的安全行为条件集合
        /// </summary>
        /// <param name="eventGuid"></param>
        public void DeleteSecurityConditions(string eventGuid,string actionGuid, List<string> conditionGuids)
        {
            try
            {
                SecurityAction sa = BasicCheck(eventGuid, actionGuid);

                m_manager.DeleteSecurityConditions(actionGuid, conditionGuids);

                sa.DeleteConditions(conditionGuids);
            }
            catch (Exception ex)
            {
                throw new Exception("删除指定的安全行为条件集合失败，错误消息为：" + ex.Message);
            }
        }
        #endregion
    }
}
