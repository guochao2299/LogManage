using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LogManage.DataType.Rules
{
    /// <summary>
    /// 安全行为造成的后果
    /// </summary>
    [Serializable]
    public class SecurityActionResult:ICloneable
    {
        public const string DefaultResultName = "默认行为后果";
        public readonly Color DefaultResultBGColor = Color.White;

        public SecurityActionResult()
        {
            m_color = DefaultResultBGColor.ToArgb();
        }


        private string m_guid = string.Empty;

        public string ResultGuid
        {
            get
            {
                return m_guid;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("规则Guid不能为空");
                }

                m_guid = value;
            }
        }

        private string m_description = string.Empty;

        /// <summary>
        /// 安全行为的后果描述
        /// </summary>
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("安全事件结果不能为空或者空白，请换成有意义的文字描述");
                }

                m_description = value;
            }
        }

        //private int m_sequence=0;

        ///// <summary>
        ///// 安全事件之间的重要程序顺序，数值越大越重要
        ///// </summary>
        //public int Sequence
        //{
        //    get
        //    {
        //        return m_sequence;
        //    }
        //    set
        //    {
        //        m_sequence = value;
        //    }
        //}

        private int m_color = 0;

        /// <summary>
        /// 安全行为后果对应的颜色
        /// </summary>
        public int BackgroundColor
        {
            get
            {
                return m_color;
            }
            set
            {
                m_color = value;
            }
        }

        public void CopyFrom(SecurityActionResult result)
        {
            this.Description = result.Description;
            this.BackgroundColor = result.BackgroundColor;
        }

        public object Clone()
        {
            SecurityActionResult result = new SecurityActionResult();
            result.ResultGuid = this.ResultGuid;
            result.Description = this.Description;
            result.BackgroundColor = this.BackgroundColor;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (!(obj is SecurityActionResult)))
            {
                return false;
            }

            SecurityActionResult result = (SecurityActionResult)obj;

            if (this.BackgroundColor != result.BackgroundColor)
            {
                return false;
            }

            if (!string.Equals(this.Description, result.Description, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.Description.GetHashCode() ^ this.BackgroundColor.GetHashCode();
        }

        public static SecurityActionResult CreateNewSecurityActionResult()
        {
            SecurityActionResult result = new SecurityActionResult();
            result.Description = DefaultResultName;
            result.ResultGuid = Guid.NewGuid().ToString();

            return result;
        }

        public static SecurityActionResult CreateSecurityActionResult(string desc,string guid,int bgColor)
        {
            SecurityActionResult result = new SecurityActionResult();
            result.Description = desc;
            result.ResultGuid = guid;
            result.BackgroundColor = bgColor;

            return result;
        }
    }
}
