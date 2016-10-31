using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    public class DateTimeEqualRelation : EqualRelationBase
    {
        public const string DateTimeEqualRelationName = "日期等于";

        #region IRelation Members

        public override string Name
        {
            get
            {
                return DateTimeEqualRelationName;
            }
        }

        public override string DefaultValue
        {
            get
            {
                return DateTime.Now.ToString("u");
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
                DateTime param1 = Convert.ToDateTime(lstParams[0].Params);
                DateTime param2 = Convert.ToDateTime(lstParams[1].Params);

                result = param1.Equals(param2);
            }
            catch (Exception ex)
            {
                throw new Exception("日期类型数据相等判断出错，错误消息为：" + ex.Message);
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

                    result = "(" + tableColName + "=\'" + DateTime.Parse(value).ToString("u").Trim(new char[]{'Z'}) + "\')";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成日期相等条件SQL语句失败，错误消息为：" + ex.Message);
            }

            return result;
        }

        #endregion
    }
}
