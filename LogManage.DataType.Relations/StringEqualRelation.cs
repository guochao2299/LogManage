
using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class StringEqualRelation : EqualRelationBase
    {
        public const string EqualRalationName="字符串等于";

        #region IRelation Members

        public override string Name
        {
            get 
            {
                return EqualRalationName;
            }
        }

        public override string DefaultValue
        {
            get 
            {
                return string.Empty;
            }
        }
        
        public override bool Implement(List<OperateParam> lstParams)
        {
            if (lstParams == null || lstParams.Count != 2)
            {
                return false;
            }

            bool result = false;

            try
            {
                string param1 = (string)lstParams[0].Params;
                string param2 = (string)lstParams[1].Params;

                if (string.IsNullOrEmpty(param1) &&
                    string.IsNullOrEmpty(param2))
                {
                    result = true;
                }
                else
                {
                    result=string.Compare(param1, param2) == 0;
                }                 
            }
            catch (Exception ex)
            {
                throw new Exception("字符串类型数据相等判断出错，错误消息为：" + ex.Message);
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
                    string value = Convert.ToString(lstParams[0].Params);

                    if (!string.IsNullOrEmpty(value))
                    {
                        result = "(" + tableColName + "=\'" + value + "\')";
                    }
                    else 
                    {
                        result = "(" + tableColName + " is NULL)";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成字符串相等条件SQL语句失败，错误消息为：" + ex.Message);
            }            

            return result;
        }
        #endregion
    }
}
