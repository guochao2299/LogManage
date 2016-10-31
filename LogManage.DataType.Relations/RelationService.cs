using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Relations
{
    /// <summary>
    /// 该服务为其他对象提供运算关系服务
    /// </summary>
    public class RelationService
    {
        private RelationService()
        {
            InitRelations();
        }

        private static RelationService m_instance = null;

        public static RelationService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new RelationService();
                }

                return m_instance;
            }
        }


        private Dictionary<string, IRelation> m_relations = new Dictionary<string, IRelation>();

        private void InitRelations()
        {            
            AddRelation(NullRelation.NullRelationName, new NullRelation(),false);
            AddRelation(DateTimeBetweenRelation.DateTimeBetweenRelationName, new DateTimeBetweenRelation(),false);
            AddRelation(DecimalBetweenRelation.DecimalEqualRelationName, new DecimalBetweenRelation(), false);
            AddRelation(DateTimeEqualRelation.DateTimeEqualRelationName, new DateTimeEqualRelation(), false);
            AddRelation(DecimalEqualRelation.DecimalEqualRelationName, new DecimalEqualRelation(), false);
            AddRelation(StringEqualRelation.EqualRalationName, new StringEqualRelation(), false);
            AddRelation(StringContainRelation.ContainRalationName, new StringContainRelation(), false);
        }

        /// <summary>
        /// 添加关系
        /// </summary>
        /// <param name="relationName">关系名称</param>
        /// <param name="relation">关系对象</param>
        /// <param name="isOverride">如果已经存在相同名称的关系，是否覆盖原有对象</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddRelation(string relationName, IRelation relation,bool isOverride)
        {
            bool result = false;

            if (!m_relations.ContainsKey(relationName))
            {
                m_relations.Add(relationName, relation);
                result = true;
            }
            else if (isOverride)
            {
                m_relations.Remove(relationName);
                m_relations.Add(relationName, relation);
                result = true;
            }

            return result;
        }

        
        /// <summary>
        /// 获取关系实例对象，如果不存在，则返回一个NullRelation对象,relationName为组合值，是类型+关系分组
        /// </summary>
        /// <param name="relationName"></param>
        /// <returns></returns>
        public IRelation GetRelation(string relationName)
        {
            string tmp = relationName;

            if (!m_relations.ContainsKey(relationName))
            {
                tmp = NullRelation.NullRelationName;
            }

            return m_relations[tmp];
        }

        public IRelation GetRelation(string colType, string colRelation)
        {
            return GetRelation(GetRelationName(colType,colRelation));                
        }

        public string GetRelationName(string colType, string colRelation)
        {
            return colType + colRelation;                 
        }
       
        /// <summary>
        /// 默认的关系，就是什么都没有
        /// </summary>
        public string DefaultRelationName
        {
            get
            {
                return EqualRelationBase.EquleRelationGroupName;
            }
        }

        /// <summary>
        /// 当前可用的关系名称集合
        /// </summary>
        public List<string> GetAvaliableRelations(string colType)
        {
            List<string> result = new List<string>();

            foreach (IRelation relation in m_relations.Values)
            {
                if (relation.Name.StartsWith(colType) &&  !result.Contains(relation.Group))
                {
                    result.Add(relation.Group);
                }
            }

            return result;
        }
    }
}
