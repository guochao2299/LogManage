using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public abstract class ContainRelationBase : IRelation
    {
        public const string ContainRelationGroupName = "包含";

        #region IRelation Members

        public abstract string Name { get; }

        public string Group
        {
            get
            {
                return ContainRelationGroupName;
            }

        }

        public int ParamsCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// 这里输入的参数集合是两个边界值，第一个是左边界值，第二个是右边界值
        /// </summary>
        /// <param name="lstParams"></param>
        /// <returns></returns>
        public abstract bool Implement(List<OperateParam> lstParams);

        public abstract bool Validate(List<OperateParam> lstParams);

        public abstract string DefaultValue
        {
            get;
        }

        public abstract string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams);
        #endregion
    }
}
