using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class StringContainRelation:ContainRelationBase
    {
        public const string ContainRalationName = "字符串包含";

        public override string Name
        {
            get 
            { 
                return ContainRalationName;
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
                if (lstParams[0].Params == null || string.IsNullOrEmpty(Convert.ToString(lstParams[0].Params)))
                {
                    return false;
                }

                if (lstParams[1].Params == null || string.IsNullOrEmpty(Convert.ToString(lstParams[1].Params)))
                {
                    return false;
                }

                string param1 = Convert.ToString(lstParams[0].Params);
                string param2 = Convert.ToString(lstParams[1].Params);

                result = param1.Contains(param2);
            }
            catch (Exception ex)
            {
                throw new Exception("字符串类型数据包含关系验证出错，错误消息为：" + ex.Message);
            }

            return result;
        }

        public override bool Validate(List<OperateParam> lstParams)
        {
            if (lstParams == null || lstParams.Count != 1)
            {
                return false;
            }

            return lstParams[0].Params != null && (!string.IsNullOrEmpty(Convert.ToString(lstParams[0].Params)));            
        }

        public override string DefaultValue
        {
            get 
            {
                return string.Empty; 
            }
        }

        public override string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams)
        {
            string result = string.Empty;

            try
            {
                if (lstParams != null && lstParams.Count >= 1)
                {
                    string subString = Convert.ToString(lstParams[0].Params);

                    result = "(" + tableColName + " like \'%" + subString + "%\')";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成字符串包含条件SQL语句失败，错误消息为：" + ex.Message);
            }

            return result;
        }
    }
}
