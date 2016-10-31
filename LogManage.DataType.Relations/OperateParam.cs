using System;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 表示一个关系运算的时候需要的参数，转换后的结果
    /// </summary>
    [Serializable()]    
    public class OperateParam
    {
        public OperateParam()
        { }

        public OperateParam(string content)
        {
            Params = content;
        }

        /// <summary>
        /// 本项保存从参数转换前的字符串，具体类型在使用时进行转换
        /// </summary>
        public string Params = string.Empty;
    }
}
