using System;
using System.Diagnostics;
using System.Threading;

namespace EmWorks.Lib.Common
{
    /// <summary>
    /// Logger - Version 1.1.0
    /// 로그 파일을 생성하고 로그를 기록하는 모듈이다.
    /// 기본적으로 실행폴더 아래의 log 폴더를 생성하여 날짜별 폴더를 만들어서 log를 남긴다.
    /// 또한, 최초 실행(선언 또는 함수호출)시에 정해진 보관기간(기본30일) 이전의 파일은 삭제한다.
    /// </summary>
    public class Logger
    {
        #region Fields

        private static Logger _instance;

        private static string _logDirPath = string.Empty;
        private static int _storagePeriod;
        private LogData _logData;

        #endregion Fields

        #region Constructors

        private Logger()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            if (_logDirPath == string.Empty)
            {
                currentPath = currentPath.TrimEnd(new char[] { '\\' });
                _logDirPath = currentPath + @"\Log";
            }//else

            _logData = new LogData(_logDirPath, _storagePeriod);
        }

        private Logger(string logDir)
        {
            _logDirPath = logDir;
            _logData = new LogData(logDir, _storagePeriod);
        }

        #endregion Constructors

        #region Destructors

        ~Logger()
        {
            // N/A
        }

        #endregion Destructors

        #region Methods

        /// <summary>
        /// Write a <see cref="Alarm"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">other message</param>
        public static void Alarm(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            _instance.Write(LogData.Level.ALARM, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Write a <see cref="Debug"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">other message</param>
        public static void Debug(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            _instance.Write(LogData.Level.DEBUG, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Write a <see cref="Error"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">other message</param>
        public static void Error(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            // Todo : 개발시에 확인을 위하여
            System.Windows.Forms.MessageBox.Show(new System.Windows.Forms.Form() { TopMost = true }, _instance.GetLogString(log, objects));

            _instance.Write(LogData.Level.ERROR, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Write a <see cref="Exception"/> type log.
        /// </summary>
        /// <param name="exception">Exception data</param>
        public static void Exception(Exception exception)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            // Todo : 개발시에 확인을 위하여
            System.Windows.Forms.MessageBox.Show(exception.ToString());

            _instance.Write(LogData.Level.ERROR, exception.ToString());
        }

        /// <summary>
        /// Write a <see cref="Fatal"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">log message</param>
        public static void Fatal(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            _instance.Write(LogData.Level.FETAL, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Write a <see cref="Infomation"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">other message</param>
        public static void Infomation(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            _instance.Write(LogData.Level.INFO, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Change the instance and log file path
        /// </summary>
        /// <param name="dirPath">Changed log file directory path</param>
        public static void InitInstance(string dirPath = "", int storagePeriod = 30)
        {
            _storagePeriod = storagePeriod;
            _logDirPath = dirPath;
            StopLogger();

            if (dirPath == string.Empty)
            {
                _instance = new Logger();
            }
            else
            {
                _instance = new Logger(dirPath);
            }
        }

        /// <summary>
        /// Stop logger
        /// </summary>
        public static void StopLogger()
        {
            if (_instance == null)
            {
                return;
            }//else
            _instance._logData.StopWrite();
        }

        /// <summary>
        /// static common Trace
        /// </summary>
        /// <param name="log">log message </param>
        /// <param name="objects">other message</param>
        public static void Trace(string log, params object[] objects)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(_instance.GetLogString(log, objects));
#endif
        }

        /// <summary>
        /// Write a <see cref="Warning"/> type log.
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="objects">other message</param>
        public static void Warning(string log, params object[] objects)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }//else

            _instance.Write(LogData.Level.WARNING, _instance.GetLogString(log, objects));
        }

        private string GetCaller(StackFrame[] stackFrams)
        {
            string caller = "";
            for (int i = 0; i < stackFrams.Length; i++)
            {
                if (stackFrams[i].GetMethod().DeclaringType == GetType())
                {
                    continue;
                }//else

                if (stackFrams[i].GetMethod().ReflectedType.FullName.Contains("System."))
                {
                    return caller;
                }//else

                if (caller.Length > 0)
                {
                    caller = "-" + caller;
                }//else

                string callerInfo = string.Format("{0}:{1}",
                    stackFrams[i].GetMethod().DeclaringType.Name,
                    stackFrams[i].GetMethod().Name);

                caller = callerInfo + caller;
            }

            return "Unknown";
        }

        private string GetLogString(string log, object[] objects)
        {
            string logValue = string.Empty;

            try
            {
                if (objects != null && objects.Length > 0)
                {
                    if (log != string.Empty)
                    {
                        logValue += log + ",";
                    } //else

                    for (int i = 0; i < objects.Length; i++)
                    {
                        logValue += objects[i].ToString() + ",";
                        //logValue = string.Format(log, objects); // 기존 Code 주석 처리 - string.format를 쓴 구체적인 이유를 모르겠음.
                    }
                }
                else
                {
                    logValue = log;
                }
            }
            catch
            {
                logValue = log;
            }

            return logValue;
        }

        private void Write(LogData.Level level, string message)
        {
#if DEBUG
            if (level != LogData.Level.DEBUG)
            {
                Trace(string.Format("[{0}] : {1}", level, message));
            }//else
#endif
            try
            {
                StackTrace stackTrace = new StackTrace();           // get call stack

                LogData tempdata = new LogData(level, Thread.CurrentThread.ManagedThreadId, GetCaller(stackTrace.GetFrames()), message);
                _logData.SaveData(tempdata);
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
            }
        }

        #endregion Methods
    }
}