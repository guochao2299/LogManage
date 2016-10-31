using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 各种关系必须实现此接口，关系是指等于、不等于、大于、小于等等
    /// </summary>
    public interface IRelation
    {
        /// <summary>
        /// 关系的名称，是以唯一值，不能重复
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 关系的分组，一组中是不同类型的实现，比如“等于”分组，里面有字符串等于、日期等于，“大于”分组，里面有字符串大于、日期大于等
        /// </summary>
        string Group { get; }

        /// <summary>
        /// 处理该关系的时候，除了列值，还需要几个参数
        /// </summary>
        int ParamsCount { get; }

        /// <summary>
        /// 进行运算关系,生成条件执行结果,输入参数的格式是这样的，显示condition的ConstContent，然后是condition的ColValues，各个关系自行获取需要的参数
        /// </summary>
        /// <param name="lstParams"></param>
        /// <returns></returns>
        bool Implement(List<OperateParam> lstParams);

        /// <summary>
        /// 输入的参数集合必须符合参数类型要求,是否满足运算关系要求，比如介于关系中，左边的日期必须要比右边的日期早
        /// </summary>
        /// <param name="lstParams"></param>
        /// <returns></returns>
        bool Validate(List<OperateParam> lstParams);

        /// <summary>
        /// 默认值
        /// </summary>
        string DefaultValue { get; }
                
        string GetPartOfSqlExpress(string tableColName,List<OperateParam> lstParams);
    }

    public class NullRelation : IRelation
    {
        public const string NullRelationName = "空关系";

        public string Name
        {
            get
            {
                return NullRelationName;
            }
        }
        public bool Implement(List<OperateParam> lstParams)
        {
            return false;
        }

        public string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams)
        {
            return string.Empty;
        }

        #region IRelation Members


        public string DefaultValue
        {
            get 
            {
                return string.Empty;
            }
        }

        #endregion

        #region IRelation Members


        public string Group
        {
            get 
            {
                return "空组";
            }
        }

        #endregion

        #region IRelation Members


        public int ParamsCount
        {
            get 
            {
                return 0;
            }
        }

        #endregion

        #region IRelation Members


        public bool Validate(List<OperateParam> lstParams)
        {
            return false;
        }

        #endregion
    }
}
