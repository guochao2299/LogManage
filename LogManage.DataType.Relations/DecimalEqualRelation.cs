using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class DecimalEqualRelation:EqualRelationBase
    {
        public const string DecimalEqualRelationName = "数值等于";

        #region IRelation Members

        public override string Name
        {
            get
            {
                return DecimalEqualRelationName;
            }
        }

        public override string DefaultValue
        {
            get
            {
                return "0";
            }
        }

        public override bool Implement(List<OperateParam> lstParams)
        {
            if (lstParams == null || lstParams.Count != 2)
            {
                return false;
            }
            
            bool result=false;

            try
            {
                float param1 = Convert.ToSingle(lstParams[0].Params);
                float param2 = Convert.ToSingle(lstParams[1].Params);

                result = (param1 == param2);
            }
            catch (Exception ex)
            {
                throw new Exception("数值类型数据相等判断出错，错误消息为：" + ex.Message);
            }

            return result;
        }

        public override string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams)
        {
            string result = string.Empty;

            try
            {
                if (lstParams != null && lstParams.Count >= 1)
                {
                    float value = Convert.ToSingle(lstParams[0].Params);

                    result = "(" + tableColName + "=" + value + ")";                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成数值相等条件SQL语句失败，错误消息为：" + ex.Message);
            }            

            return result;
        }

        #endregion
    }
}
