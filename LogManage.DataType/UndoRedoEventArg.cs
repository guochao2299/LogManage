using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogManage.DataType
{
    public class UndoRedoEventArg:EventArgs
    {
        public UndoRedoEventArg():base()
        {
        }

        public string FirstLevelGuid;

        public string SecondLevelGuid;

        public object Tag;        
    }
}
