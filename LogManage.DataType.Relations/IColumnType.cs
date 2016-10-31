using System;
using System.Text;
using System.Collections.Generic;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 表示日志列的类型接口
    /// </summary>
    public interface IColumnType
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 当前类型对应的数据库类型表示
        /// </summary>
        string DBType { get; }

        /// <summary>
        /// 输入的字符串内容是否符合当前类型格式
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        bool Validate(string content);

        /// <summary>
        /// 传入具体类型的字符串表示，输出在数据库语句中值的表示
        /// </summary>
        /// <param name="initValue"></param>
        /// <returns></returns>
        string GetDBValueExpress(string initValue);
    }

    public class NullColumnType:IColumnType
    {
        private static NullColumnType m_instance = null;
        public static NullColumnType Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new NullColumnType();
                }

                return m_instance;
            }
        }
        public const string NullColumnTypeName = "空类型";
        #region IColumnType Members

        public string Name
        {
            get
            {
                return NullColumnTypeName; 
            }
        }

        public string DBType
        {
            get 
            {
                return "varchar(255)";  
            }
        }

        public bool Validate(string content)
        {
            return false;
        }

        #endregion

        #region IColumnType Members


        public string GetDBValueExpress(string initValue)
        {
            return string.Empty;
        }

        #endregion
    }
}
