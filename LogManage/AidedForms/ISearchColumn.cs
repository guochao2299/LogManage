using System;
using System.Text;

namespace LogManage.AidedForms
{
    public interface ISearchColumn
    {
        string SearchColumnDown(ref int startRowIndex, int columnIndex, string searchContent);
        string SearchColumnUp(ref int startRowIndex, int columnIndex, string searchContent);

        int RowsCount { get;}
    }
}
