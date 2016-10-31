using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using LogManage.DataType;

namespace LogManage.LogAnalysis
{
    internal sealed class LogRecordsPool
    {
        private LogRecordsPool()
        { }

        ~LogRecordsPool()
        {
            m_mutex.Dispose();
        }

        private static Mutex m_mutex = new Mutex();

        public static void Suspend()
        {
            WaitMutex();
        }

        private  static void WaitMutex()
        {
            m_mutex.WaitOne();
        }

        public static void Resume()
        {
            ReleaseMutex();
        }

        private static void ReleaseMutex()
        {
            m_mutex.ReleaseMutex();
        }

        private static LogRecordsPool m_instance = null;

        public static LogRecordsPool Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new LogRecordsPool();

                    Init();
                }

                return m_instance;
            }
        }

        private static void Init()
        { }

        private Queue<LogRecord> m_logRecordsPool = new Queue<LogRecord>();        

        public void AddLogRecord(LogRecord record)
        {
            WaitMutex();

            try
            {
                m_logRecordsPool.Enqueue(record);

            }
            catch (Exception ex)
            {
                throw new Exception("添加待分析的日志记录失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        public void ClearPool()
        {
            WaitMutex();

            try
            {
                m_logRecordsPool.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("清空日志池失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        public LogRecord GetOneLogRecord()
        {
            WaitMutex();

            try
            {
                if (m_logRecordsPool.Count <= 0)
                {
                    return null;
                }

                return m_logRecordsPool.Dequeue();
            }
            catch (Exception ex)
            {
                throw new Exception("获取日志记录失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        private bool m_finishSign=false;

        /// <summary>
        /// 标记producer线程已经结束
        /// </summary>
        public void SetFinishSign()
        {
            WaitMutex();

            try
            {
                m_finishSign = true;
            }
            catch (Exception ex)
            {
                throw new Exception("设置Producer线程已经结束标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        /// <summary>
        /// 标记producer线程重置为开始
        /// </summary>
        public void ResetFinishSign()
        {
            WaitMutex();

            try
            {
                m_finishSign = false;
            }
            catch (Exception ex)
            {
                throw new Exception("重置Producer线程结束标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        /// <summary>
        /// 获取producer线程已经结束
        /// </summary>
        public bool GetFinishSign()
        {
            WaitMutex();

            try
            {
                return m_finishSign;
            }
            catch (Exception ex)
            {
                throw new Exception("获取Producer线程已经结束标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        /// <summary>
        /// 日志池是否为空
        /// </summary>
        public bool IsPoolEmpty()
        {
            Console.WriteLine("IsPoolEmpty start");

            WaitMutex();

            try
            {
                return m_logRecordsPool.Count<=0;
            }
            catch (Exception ex)
            {
                throw new Exception("获取日志池是否为空失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
                Console.WriteLine("IsPoolEmpty end");
            }
        }

        private bool m_breakSign = false;

        /// <summary>
        /// 标记Consumer线程是否已经结束
        /// </summary>
        public void SetBreakSign()
        {
            WaitMutex();

            try
            {
                m_breakSign = true;
            }
            catch (Exception ex)
            {
                throw new Exception("设置Consumer线程被中止标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        /// <summary>
        /// 标记Consumer线程重置为开始
        /// </summary>
        public void ResetBreakSign()
        {
            WaitMutex();

            try
            {
                m_breakSign = false;
            }
            catch (Exception ex)
            {
                throw new Exception("重置Consumer线程被中止标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }

        /// <summary>
        /// 获取Consumer线程被中止标志
        /// </summary>
        public bool GetBreakSign()
        {
            WaitMutex();

            try
            {
                return m_breakSign;
            }
            catch (Exception ex)
            {
                throw new Exception("获取Consumer线程被中止标志失败，错误消息为：" + ex.Message);
            }
            finally
            {
                ReleaseMutex();
            }
        }
    }
}
