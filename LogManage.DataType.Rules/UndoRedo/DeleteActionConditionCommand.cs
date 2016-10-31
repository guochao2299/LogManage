using System;
using System.Collections.Generic;
using System.Text;

namespace LogManage.DataType.Rules.UndoRedo
{
    internal class DeleteActionConditionCommand:CommandBase
    {
        private string m_eventGuid = string.Empty;
        private string m_actionGuid = string.Empty;
        private List<SecurityCondition> m_conditions = null;

        public DeleteActionConditionCommand(string eventGuid,string actionGuid,List<SecurityCondition> conditions)
        {
            m_actionGuid = actionGuid;
            m_eventGuid = eventGuid;

            m_conditions = new List<SecurityCondition>();

            foreach (SecurityCondition condition in conditions)
            {
                m_conditions.Add((SecurityCondition)condition.Clone());
            }
        }

        public override void Execute()
        {
            List<string> lstConditions=new List<string>();

            foreach(SecurityCondition sc in m_conditions)
            {
                lstConditions.Add(sc.ConditionGuid);
            }

            SecurityEventService.Instance.DeleteSecurityConditions(m_eventGuid, m_actionGuid, lstConditions);
        }

        public override void Undo()
        {
            List<string> lstGuids = new List<string>();

            foreach (SecurityCondition sc in m_conditions)
            {
                SecurityEventService.Instance.AddSecurityCondition(m_eventGuid,m_actionGuid,(SecurityCondition)sc.Clone());
                lstGuids.Add(sc.ConditionGuid);
            }

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = m_conditions;

            RaiseUndoDoneEvent(arg);
        }

        public override void Redo()
        {
            List<string> lstConditions = new List<string>();

            foreach (SecurityCondition sc in m_conditions)
            {
                lstConditions.Add(sc.ConditionGuid);
            }

            SecurityEventService.Instance.DeleteSecurityConditions(m_eventGuid, m_actionGuid, lstConditions);

            UndoRedoEventArg arg = new UndoRedoEventArg();
            arg.FirstLevelGuid = m_eventGuid;
            arg.SecondLevelGuid = m_actionGuid;
            arg.Tag = lstConditions;

            RaiseRedoDoneEvent(arg);
        }
    }
}
