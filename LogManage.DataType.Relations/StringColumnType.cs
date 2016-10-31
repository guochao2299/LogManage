using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 字符串类型，与SQL中varchar类型对应
    /// </summary>
    public class StringColumnType:IColumnType
    {
        /// <summary>
        /// 字符串类型的标识符
        /// </summary>
        public const string StringSign = "字符串";
 
        #region IColumnType Members

        public string Name
        {
            get 
            {
                return StringSign;
            }
        }

        public string DBType
        {
            get
            {
                return "varchar(250)"; 
            }
        }

        public string DefaultValue
        {
            get
            {
                return string.Empty;
            }
        }

        public bool Validate(string content)
        {
            return true;
        }

        public string GetDBValueExpress(string initValue)
        {
            return "\'" + initValue + "\'";
        }

        #endregion
    }
}
