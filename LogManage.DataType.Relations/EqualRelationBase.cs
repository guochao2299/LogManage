using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType.Relations
{
    public abstract class EqualRelationBase:IRelation
    {
        public const string EquleRelationGroupName = "等于";

        #region IRelation Members

        public abstract string Name{ get; }


        public string Group
        {
            get
            {
                return EquleRelationGroupName;
            }

        }

        public int ParamsCount
        {
            get
            {
                return 1;
            }
        }

        public virtual bool Validate(List<OperateParam> lstParams)
        {
            return true;
        }

        public abstract bool Implement(List<OperateParam> lstParams);

        public abstract string DefaultValue
        {
            get ; 
        }

        public abstract string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams);        

        #endregion
    }
}
