using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class DateTimeBetweenRelation:BetweenRelationBase
    {
        public const string DateTimeBetweenRelationName = "日期介于";

        public override string Name
        {
            get
            {
                return DateTimeBetweenRelationName; 
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
                DateTime param1 = Convert.ToDateTime(lstParams[0].Params);
                DateTime param2 = Convert.ToDateTime(lstParams[1].Params);

                result = param1 <= param2;
            }
            catch (Exception ex)
            {
                throw new Exception("日期类型数据介于关系验证出错，错误消息为：" + ex.Message);
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
                DateTime param1 = Convert.ToDateTime(lstParams[0].Params);
                DateTime param2 = Convert.ToDateTime(lstParams[1].Params);
                DateTime param3 = Convert.ToDateTime(lstParams[1].Params);

                result = (param1 >= param2) && (param1 <= param3);
            }
            catch (Exception ex)
            {
                throw new Exception("日期类型数据介于判断出错，错误消息为：" + ex.Message);
            }

            return result;
        }

        public override string DefaultValue
        {
            get
            {
                return DateTime.Now.ToString("u");
            }
        }

        public override string GetPartOfSqlExpress(string tableColName, List<OperateParam> lstParams)
        {
            string result = string.Empty;

            try
            {
                if (lstParams != null && lstParams.Count >= 2)
                {
                    string firstDate = DateTime.Parse(lstParams[0].Params).ToString("u").Trim(new char[] { 'Z' });//("yyyy-MM-dd hh:mm:ss");
                    string secondDate = DateTime.Parse(lstParams[1].Params).ToString("u").Trim(new char[] { 'Z' });
                    result = "((" + tableColName + ">=\'" + firstDate + "\') and (" +
                        tableColName + "<=\'" + secondDate + "\'))";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成日期介于条件SQL语句失败，错误消息为：" + ex.Message);
            }

            return result;
        }
    }
}
