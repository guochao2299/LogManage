using System;
using System.Text;

namespace LogManage.DataType
{
    public interface ILogColumnService
    {
        LogColumn GetLogColumn(int colIndex);
    }
}
