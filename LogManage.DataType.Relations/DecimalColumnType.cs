using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 数值类型，与Sql中Decimal类型对应
    /// </summary>
    public class DecimalColumnType:IColumnType
    {
        /// <summary>
        /// 数字类型的标识符
        /// </summary>
        public const string DecimalSign = "数值";

        #region IColumnType Members

        public string Name
        {
            get 
            {
                return DecimalSign;
            }
        }

        public string DBType
        {
            get
            {
                return "decimal(10,4)";    
            }
        }

        public string DefaultValue
        {
            get
            {
                return "0";
            }
        }

        public bool Validate(string content)
        {
            decimal tmp=0;
            return decimal.TryParse(content, out tmp);
        }

        public string GetDBValueExpress(string initValue)
        {
            return initValue;
        }

        #endregion
    }
}
