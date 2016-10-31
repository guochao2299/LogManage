using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using LogManage.DataType.Relations;

namespace LogManage.DataType.Rules.Evaluation.SPI
{
    public class ImmediatelyReturnRuleProcessor:ImmediatelyReturnRuleProcessorBase
    {
        public override void Execute(LogRecord record, ILogColumnService colSrv)
        {
            try
            {
                m_result.Clear();

                List<OperateParam> lstParams=new List<OperateParam>();

                foreach (SecurityEvent se in m_events)
                {
                    foreach (SecurityAction sa in se.SecurityActions)
                    {
                        if (sa.Conditions.Count <= 0)
                        {
                            continue;
                        }

                        bool result=true;

                        // 先检查是否满足关系要求
                        foreach (SecurityCondition condition in sa.Conditions)
                        {
                            if (!record.Items.ContainsKey(condition.SourceCol))
                            {
                                result = false;
                                break;
                            }

                            lstParams.Clear();

                            IRelation relation = RelationService.Instance.GetRelation(colSrv.GetLogColumn(condition.SourceCol).Type,condition.RelationName);

                            if (condition.IsUsingDestCol)
                            {
                                foreach (string s in condition.MultiValues)
                                {
                                    int colIndex=Convert.ToInt32(s);

                                    if (!record.Items.ContainsKey(colIndex))
                                    {
                                        result = false;
                                        goto NextAction;
                                    }

                                    lstParams.Add(new OperateParam(record.Items[colIndex].Conetent));
                                }
                            }
                            else
                            {
                                foreach (string s in condition.MultiValues)
                                {
                                    lstParams.Add(new OperateParam(s));
                                }
                            }                            
                            
                            // 检测是否满足关系
                            if (!relation.Validate(lstParams))
                            {
                                result = false;
                            }
                            else
                            {
                                lstParams.Insert(0, new OperateParam(record.Items[condition.SourceCol].Conetent));
                                result = result && relation.Implement(lstParams);
                            }

                            if (!result)
                            {
                                break;
                            }
                        }

                    NextAction:

                        // 如果符合结果
                        if(result)
                        {
                            EvaluateResult er = new EvaluateResult(record.RecordGuid);
                            er.AppGuid = record.AppGuid;
                            er.TableGuid = record.TableGuid;
                            er.ActionName = sa.Name;
                            er.ActionGuid = sa.ActionGuid;
                            er.EventName = se.Name;
                            er.EventGuid = se.EventGuid;
                            er.ResultGuid = sa.ResultGuid;

                            m_result.Add(er);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("根据策略分析日志记录失败，错误消息为：" + ex.Message);
            }
        }
    }
}
