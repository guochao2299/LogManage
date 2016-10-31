using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 日期类型，与Sql中datetime类型对应
    /// </summary>
    public class DateTimeColumnType:IColumnType
    {
        /// <summary>
        /// 日期类型的标识符
        /// </summary>
        public const string DateSign = "日期";

        #region IColumnType Members

        public string Name
        {
            get 
            {
                return DateSign;
            }
        }

        public string DBType
        {
            get 
            {
                return "datetime";
            }
        }

        public string DefaultValue
        {
            get
            {
                return DateTime.Now.ToString("u");
            }
        }

        public bool Validate(string content)
        {
            DateTime dt;
            return DateTime.TryParse(content, out dt);
        }

        public string GetDBValueExpress(string initValue)
        {
            return "\'" + DateTime.Parse(initValue).ToString("u").Trim(new char[]{'Z'}) + "\'";
        }
        #endregion
    }
}
