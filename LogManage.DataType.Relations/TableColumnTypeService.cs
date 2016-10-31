using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogManage.DataType.Relations;

namespace LogManage.DataType.Relations
{
    public class TableColumnTypeService
    {
        private TableColumnTypeService()
        {
            InitAvaliableTypes();
        }

        private Dictionary<string, IColumnType> m_avaliableTypes = new Dictionary<string, IColumnType>();

        public Dictionary<string, IColumnType> AvaliableTypes
        {
            get
            {
                return m_avaliableTypes;
            }
        }

        public IColumnType GetColumnType(string colType)
        {
            IColumnType t = NullColumnType.Instance;

            if (m_avaliableTypes.ContainsKey(colType))
            {
                t = m_avaliableTypes[colType];
            }

            return t;
        }

        public string DefaultColumnType
        {
            get
            {
                return StringColumnType.StringSign;
            }
        }

        private void InitAvaliableTypes()
        {
            m_avaliableTypes.Add(StringColumnType.StringSign,new StringColumnType());
            m_avaliableTypes.Add(DecimalColumnType.DecimalSign,new DecimalColumnType());
            m_avaliableTypes.Add(DateTimeColumnType.DateSign, new DateTimeColumnType());
        }

        private static TableColumnTypeService m_instance = null;

        public static TableColumnTypeService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new TableColumnTypeService();
                }

                return m_instance;
            }
        }

        public string GetSqlTypeOfTableColumn(string originType)
        {
            string t = StringColumnType.StringSign;

            if (m_avaliableTypes.ContainsKey(originType))
            {
                return m_avaliableTypes[originType].DBType;
            }

            return t;
        }
    }
}
