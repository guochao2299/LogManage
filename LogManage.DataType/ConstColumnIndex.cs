using System;
using System.Text;

namespace LogManage.DataType
{
    /// <summary>
    /// 保存常用列的列号
    /// </summary>
    public static class ConstColumnIndex
    {
        /// <summary>
        /// 备注           14
        /// </summary>
        public const int RemarkColIndex = 14;

        /// <summary>
        /// 用户账号 8
        /// </summary>
        public const int UserIDColIndex = 8;

        /// <summary>
        /// 用户名   9
        /// </summary>
        public const int UserNameColIndex = 9;

        /// <summary>       
        /// 计算机名 10
        /// </summary>
        public const int ComputerNameColIndex = 10;

        /// <summary>
        /// 密级     11
        /// </summary>
        public const int SecretGradeColIndex = 11;

        /// <summary>
        /// 文件名称 23
        /// </summary>
        public const int FileNameGradeColIndex = 23;

        /// <summary>
        /// 创建日期 13
        /// </summary>
        public const int CreateTimeColIndex = 13;

        /// <summary>
        /// 文件密级，字符串，35
        /// </summary>
        public const int FileSecretColIndex = 35;

        /// <summary>
        /// 文件密级值，数字，36
        /// </summary>
        public const int FileSecretValueColIndex = 36;

        /// <summary>
        /// usb设备密级
        /// </summary>
        public const int USBDeviceSecretColIndex = 42;

        /// <summary>
        /// usb设备密级值
        /// </summary>
        public const int USBDeviceSecretLevelColIndex = 43;

    }
}
