using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

using LogManage.DataType;
using System.Windows.Forms;

namespace LogManage.Services
{
    /// <summary>
    /// 在获取日志内容时，提供一些辅助的服务
    /// </summary>
    public class LogContentService
    {
        private const string LogOperatorPath = "LogOperators";
        private const string LogOperatorFileExtension = ".lgopt";

        private LogContentService()
        {
            m_operators = new Dictionary<string, ILogOperator>();

            LoadLogOperators();
        }

        private void LoadLogOperators()
        {
            try
            {
                // 动态加载日志处理函数
                string dirPath = Path.Combine(Application.StartupPath, LogOperatorPath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                DirectoryInfo di = new DirectoryInfo(dirPath);
                FileInfo[] files = di.GetFiles("*" + LogOperatorFileExtension);

                if (files != null && files.Length > 0)
                {
                    StringBuilder sbErrors = new StringBuilder();
                    XmlSerializer dataSerializer = new XmlSerializer(typeof(LogOperatorInfo));
                    XmlTextReader xmlReader=null;

                    foreach (FileInfo info in files)
                    {
                        try
                        {
                            xmlReader=new XmlTextReader(info.FullName);
                            LogOperatorInfo optInfo = (LogOperatorInfo)dataSerializer.Deserialize(xmlReader);

                            if (string.IsNullOrWhiteSpace(optInfo.AppGuid))
                            {
                                throw new Exception("应用程序Guid不能为空");
                            }

                            string assemblyFile=Path.Combine(dirPath,optInfo.FileName);

                            if (m_operators.ContainsKey(optInfo.AppGuid))
                            {
                                throw new Exception(string.Format("应用程序Guid：{0}已经存在", optInfo.AppGuid));
                            }

                            if(!File.Exists(assemblyFile))
                            {
                                throw new Exception(string.Format("文件{0}不存在或者没有权限访问",assemblyFile));
                            }

                            Assembly optor = Assembly.LoadFile(assemblyFile);

                            foreach (Module m in optor.GetModules())
                            {
                                foreach (Type t in m.GetTypes())
                                {
                                    Type interfaceT=t.GetInterface("ILogOperator",true);

                                    if (interfaceT == null)
                                    {
                                        continue;
                                    }

                                    ILogOperator optInstance=(ILogOperator)System.Activator.CreateInstance(t);
                                    optInstance.AppGuid = optInfo.AppGuid;
                                    optInstance.Filter=optInfo.Filter;

                                    m_operators.Add(optInfo.AppGuid, optInstance);
                                }
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            sbErrors.AppendLine(string.Format("文件{0}解析错误，错误消息为:{1}",
                                info.FullName, ex.Message));
                        }
                        finally
                        {
                            if(xmlReader!=null)
                            {
                                xmlReader.Close();
                                xmlReader=null;
                            }
                        }
                    }

                    if (sbErrors.Length > 0)
                    {
                        throw new Exception(sbErrors.ToString());
                    }
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("加载日志操作类失败，错误消息为：" + ex.Message);
            }
        }

        private static LogContentService m_instance = null;

        public static LogContentService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new LogContentService();
                }

                return m_instance;
            }
        }

        private Dictionary<string, ILogOperator> m_operators = null;

        public ILogOperator GetLogOperator(string appGuid)
        {
            if (m_operators.ContainsKey(appGuid))
            {
                return m_operators[appGuid];
            }

            return new NullLogOperator();
        }

        public void AddLogOperator(string appGuid,ILogOperator opt)
        {
            if (m_operators.ContainsKey(appGuid))
            {
                throw new Exception("缓冲区中已经存在该应用程序的操作类！");
            }

            m_operators.Add(appGuid, opt);
        }

        public bool SaveAppLogs(string appGuid, string tableGuid, List<LogRecord> lstRecords)
        {
            return DBService.Instance.SaveAppLogs(appGuid, tableGuid, lstRecords);
        }

        public List<LogRecord> GetAppLogs(string appGuid, string tableGuid, List<LogFilterCondition> lstFilters)
        {
            return DBService.Instance.GetAppLogs(appGuid, tableGuid, lstFilters);
        }

        public List<LogRecord> GetAppLogs(string appGuid, string tableGuid, List<string> lstRecordGuids)
        {
            return DBService.Instance.GetAppLogs(appGuid, tableGuid, lstRecordGuids);
        }
    } 


}
