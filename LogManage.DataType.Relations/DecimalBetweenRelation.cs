using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class DecimalBetweenRelation:BetweenRelationBase
    {
        public const string DecimalEqualRelationName = "数值介于";

        public override string Name
        {
            get 
            {
                return DecimalEqualRelationName;
            }
        }

        /// <summary>
        /// 这里输入的参数集合是两个边界值，第一个是左边界值，第二个是右边界值
        /// </summary>
        /// <param name="lstParams"></param>
        /// <returns></returns>
        public override bool Validate(List<OperateParam> lstParams)
        {
            if (lstParams == null || lstParams.Count != 2)
            {
                return false;
            }

            bool result = false;

            try
            {
                float param1 = Convert.ToSingle(lstParams[0].Params);
                float param2 = Convert.ToSingle(lstParams[1].Params);                

                result = param1<=param2;
            }
            catch (Exception ex)
            {
                throw new Exception("数值类型数据介于关系验证出错，错误消息为：" + ex.Message);
            }

            return result;
        }

        public override bool Implement(List<OperateParam> lstParams)
        {
            if (lstParams == null || lstParams.Count != 3)
            {
                return false;
            }

            bool result = false;

            try
            {
                float param1 = Convert.ToSingle(lstParams[0].Params);
                float param2 = Convert.ToSingle(lstParams[1].Params);
                float param3 = Convert.ToSingle(lstParams[1].Params);

                result = (param1 >= param2)&& (param1<=param3);
            }
            catch (Exception ex)
            {
                throw new Exception("数值类型数据介于判断出错，错误消息为：" + ex.Message);
            }

            return result;
        }

        public override string DefaultValue
        {
            get 
            {
                return "0";
            }
        }

        public override string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams)
        {
            string result = string.Empty;

            try
            {
                if (lstParams != null && lstParams.Count >= 2)
                {
                    float value1 = Convert.ToSingle(lstParams[0].Params);
                    float value2 = Convert.ToSingle(lstParams[1].Params);

                    result = "((" + tableColName + ">=" + value1 + ") and ("+
                        tableColName + "<=" + value2 + "))";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成数值介于条件SQL语句失败，错误消息为：" + ex.Message);
            }

            return result;
        }
    }
}
