using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules
{
    /// <summary>
    /// 用于定义管理规则数据的借口
    /// </summary>
    public interface IManageRuleData
    {
        #region 用户定义的行为后果
        /// <summary>
        /// 获取系统中定义的所有行为后果
        /// </summary>
        /// <returns></returns>
        List<SecurityActionResult> GetDefinedActionResults();

        void UpdateResults(List<SecurityActionResult> results);

        void InsertResults(List<SecurityActionResult> results);

        void DeleteResults(List<string> lstGuids);

        /// <summary>
        /// 获取使用了指定行为结果的规则集合，返回规则guid集合，用分号隔开
        /// </summary>
        /// <param name="resultGuid"></param>
        /// <returns></returns>
        string GetRulesOfUsingTheResult(string resultGuid);
        #endregion

        #region 安全事件

        /// <summary>
        /// 获取用户定义的所有安全事件
        /// </summary>
        List<SecurityEvent> GetDefinedSecurityEvents();

        /// <summary>
        /// 新建安全事件
        /// </summary>
        /// <param name="se"></param>
        void CreateNewSecurityEvent(SecurityEvent se);

        /// <summary>
        /// 更新安全事件属性
        /// </summary>
        /// <param name="se"></param>
        void UpdateSecurityEventProperties(SecurityEvent se);

        /// <summary>
        /// 删除指定的安全事件
        /// </summary>
        /// <param name="eventGuid"></param>
        void DeleteSecurityEvent(string eventGuid);
        #endregion

        #region 安全行为

        /// <summary>
        /// 新建安全行为
        /// </summary>
        /// <param name="se"></param>
        void CreateNewSecurityAction(string eventGuid,SecurityAction sa);

        /// <summary>
        /// 更新安全行为属性
        /// </summary>
        /// <param name="se"></param>
        void UpdateSecurityActionProperties(string eventGuid, SecurityAction sa);

        /// <summary>
        /// 删除指定的安全行为,包含条件
        /// </summary>
        /// <param name="eventGuid"></param>
        void DeleteSecurityAction(string eventGuid, string actionGuid);
        #endregion

        #region 安全行为包含的条件
        /// <summary>
        /// 新建安全行为条件
        /// </summary>
        /// <param name="se"></param>
        void CreateNewSecurityCondition(string actionGuid, SecurityCondition sc);

        /// <summary>
        /// 更新安全行为条件
        /// </summary>
        /// <param name="se"></param>
        void UpdateSecurityConditionProperties(string actionGuid, SecurityCondition sc);

        /// <summary>
        /// 删除指定的安全行为条件
        /// </summary>
        /// <param name="eventGuid"></param>
        void DeleteSecurityCondition(string actionGuid,string conditonGuid);

        /// <summary>
        /// 删除指定的安全行为条件集合
        /// </summary>
        /// <param name="eventGuid"></param>
        void DeleteSecurityConditions(string actionGuid,List<string> conditionGuids);
        #endregion

        /// <summary>
        /// 获取所有已经定义的日志列
        /// </summary>
        /// <returns></returns>
        List<LogColumn> GetDefinedColumns();
    }
}
