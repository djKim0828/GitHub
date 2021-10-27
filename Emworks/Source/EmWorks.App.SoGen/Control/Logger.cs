using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Logger - Version 1.0.0
    /// 로그 파일을 생성하고 로그를 기록하는 모듈이다.
    /// 기본적으로 실행폴더 아래의 log 폴더를 생성하여 날짜별 폴더를 만들어서 log를 남긴다.
    /// 또한, 최초 실행(선언 또는 함수호출)시에 정해진 보관기간(기본30일) 이전의 파일은 삭제한다.
    /// </summary>
    public class Logger
    {
        #region Fields

        private static Logger _instance;

        private bool _isLoggerStop = false;

        private bool _isWriteCustomerLog = false;

        private JavaScriptSerializer _jsonConvertor;

        private ConcurrentQueue<LogData> _logDataQueue = null;

        private string _logDirPath = null;

        private Thread _logFileWriteThread = null;

        private int _storagePeriod = 30;

        #endregion Fields

        #region Constructors

        private Logger()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            _jsonConvertor = new JavaScriptSerializer();
            StartLogger(currentPath + @"\Log");
        }

        private Logger(string logDir)
        {
            _jsonConvertor = new JavaScriptSerializer();
            StartLogger(logDir);
        }

        #endregion Constructors

        #region Destructors

        ~Logger()
        {
            // N/A
        }

        #endregion Destructors

        #region Enums

        private enum _LOGLEVEL
        {
            INFO = 0,
            TACTTIME = 1,
            WARNING = 2,
            ALARM = 3,
            ERROR = 4,
            FETAL = 5,
            DEBUG = 6
        }

        #endregion Enums

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
            }

            _instance.Write(_LOGLEVEL.ALARM, _instance.GetLogString(log, objects));
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
            }

            _instance.Write(_LOGLEVEL.DEBUG, _instance.GetLogString(log, objects));
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
            }

            _instance.Write(_LOGLEVEL.ERROR, _instance.GetLogString(log, objects));
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
            }

            _instance.Write(_LOGLEVEL.ERROR, exception.ToString());
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
            }

            _instance.Write(_LOGLEVEL.FETAL, _instance.GetLogString(log, objects));
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
            }

            _instance.Write(_LOGLEVEL.INFO, _instance.GetLogString(log, objects));
        }

        /// <summary>
        /// Change the instance and log file path
        /// </summary>
        /// <param name="dirPath">Changed log file directory path</param>
        public static void InitInstance(string dirPath = "")
        {
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
        /// 로그파일 보관기간을 설정한다. ( Default 는 30일 )
        /// Setting the storage period of log files.
        /// </summary>
        /// <param name="storagePeriod">Setting value</param>
        public static void SetStoragePeriod(int storagePeriod)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            _instance._storagePeriod = storagePeriod;
        }

        /// <summary>
        /// Setting the customer log write function
        /// </summary>
        /// <param name="isEnable">set enable</param>
        public static void SetWriteCustomerLog(bool isEnable)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            _instance._isWriteCustomerLog = isEnable;
        }

        /// <summary>
        /// Stop logger
        /// </summary>
        public static void StopLogger()
        {
            if (_instance == null)
            {
                return;
            }

            _instance._isLoggerStop = true;

            if (_instance._logFileWriteThread != null)
            {
                _instance._logFileWriteThread = null;
            }
        }

        /// <summary>
        /// static common Trace
        /// </summary>
        /// <param name="log">log message</param>
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
            }

            _instance.Write(_LOGLEVEL.WARNING, _instance.GetLogString(log, objects));
        }

        private bool CreateDirectorySafely(string directory)
        {
            bool success = false;

            try
            {
                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return success;
        }

        private bool DeleteDirectorySafely(string strDir, bool bRecursive)
        {
            bool success = false;

            try
            {
                if (Directory.Exists(strDir) == true)
                    Directory.Delete(strDir, bRecursive);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return success;
        }

        private void DeleteOldLogFiles()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(_logDirPath);

                foreach (string dir in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if (IsValidDate(dirInfo.Name))
                    {
                        string dateData = dirInfo.Name.Substring(0, 8);
                        if (GetDayLength(dateData) > _storagePeriod)
                        {
                            DeleteDirectorySafely(dir, true);
                            Trace("Delete : " + dir);
                        }// else
                    }// else
                }
            }
            catch
            {
            }
        }

        private int GetDayLength(string dateValue)
        {
            DateTime dt;
            DateTime.TryParseExact(dateValue, "yyyyMMdd", null, DateTimeStyles.None, out dt);

            TimeSpan sp = DateTime.Now - dt;
            return sp.Days;
        }

        private string GetLogFilePath(LogData logdata)
        {
            string logDir = string.Format("{0}\\{1}",
                _logDirPath,
                logdata.GetDateString());

            CreateDirectorySafely(logDir);

            return string.Format("{0}\\{1}.log",
                                logDir,
                                logdata.GetDateString());
        }

        private string GetLogFilePathforCustomer(LogData logdata)
        {
            string logDir = string.Format("{0}\\{1}",
                _logDirPath,
                logdata.GetDateString());

            CreateDirectorySafely(logDir);

            return string.Format("{0}\\{1}.clog",
                                logDir,
                                logdata.GetDateString());
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

        private bool IsValidDate(string dateValue)
        {
            try
            {
                string dateData = dateValue.Substring(0, 8);
                DateTime dt;
                return DateTime.TryParseExact(dateData, "yyyyMMdd", null, DateTimeStyles.None, out dt);
            }
            catch (System.Exception ex)
            {
                Trace(ex.ToString());
                return false;
            }
        }

        private void LogFileWriteThreadProc()
        {
            MakeInvariantCultureThread();

            while (true)
            {
                Thread.Sleep(10);

                if (_isLoggerStop)
                {
                    break;
                }

                try
                {
                    if (_logDataQueue.Count == 0)
                    {
                        continue;
                    }

                    LogData logdata = new LogData();

                    if (_logDataQueue.TryDequeue(out logdata))
                    {
                        WriteLogDataToFile(logdata);
                    }
                    else
                    {
                        Trace("Dequeue Fail !!");
                    }
                }
                catch (Exception ex)
                {
                    Trace(ex.ToString());
                }
            }
        }

        private void MakeInvariantCultureThread()
        {
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

                culture.NumberFormat = System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
                culture.DateTimeFormat = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;

                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                {
                    Logger.Exception(ex);
                }
            }
        }

        private void StartLogger(string logDir)
        {
            _logDirPath = logDir;

            if (Directory.Exists(_logDirPath) == false)
            {
                Directory.CreateDirectory(_logDirPath);
            }
            else
            {
                DeleteOldLogFiles();
            }

            _logDataQueue = new ConcurrentQueue<LogData>();

            if (_logFileWriteThread == null)
            {
                _logFileWriteThread = new Thread(LogFileWriteThreadProc);
                _logFileWriteThread.IsBackground = true;
            }

            _logFileWriteThread.Start();
        }

        private void Write(_LOGLEVEL level, string message)
        {
#if DEBUG
            if (level != _LOGLEVEL.DEBUG)
            {
                Trace(string.Format("[{0}] : {1}", level, message));
            }
#endif

            try
            {
                StackTrace stackTrace = new StackTrace();           // get call stack

                LogData logdata = new LogData();
                logdata.LogLevel = level;
                logdata.LogTime = DateTime.Now;
                logdata.ThreadID = Thread.CurrentThread.ManagedThreadId;
                logdata.StackFrams = stackTrace.GetFrames();
                logdata.Message = message;

                _logDataQueue.Enqueue(logdata);
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
            }
        }

        private void WriteLogDataToFile(LogData logdata)
        {
            try
            {
                string logFilePath = GetLogFilePath(logdata);

                string logText = _jsonConvertor.Serialize(new LogDataForFile(logdata));

                FileStream LogFileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter StreamWriter = new StreamWriter(LogFileStream);

                StreamWriter.WriteLine(logText);

                StreamWriter.Close();
                LogFileStream.Close();

                if (_isWriteCustomerLog == true)
                {
                    WriteLogDataToFileforCustomer(logdata);
                }
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
            }
        }

        private void WriteLogDataToFileforCustomer(LogData logdata)
        {
            string logFilePath = GetLogFilePathforCustomer(logdata);

            FileStream LogFileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter StreamWriter = new StreamWriter(LogFileStream);

            StreamWriter.WriteLine(logdata.Message);

            StreamWriter.Close();
            LogFileStream.Close();
        }

        #endregion Methods

        #region Structs

        private struct LogData
        {
            #region Properties

            public _LOGLEVEL LogLevel { get; set; }
            public DateTime LogTime { get; set; }
            public string Message { get; set; }
            public StackFrame[] StackFrams { get; set; }
            public int ThreadID { get; set; }
            public string UserId { get; set; }

            #endregion Properties

            #region Methods

            /// <summary>
            /// Date format change by region
            /// </summary>
            /// <returns></returns>
            public string GetDateString()
            {
                return LogTime.ToString("yyyyMMdd_HH", CultureInfo.InvariantCulture);
            }

            #endregion Methods
        }

        #endregion Structs

        #region Classes

        private class LogDataForFile
        {
            #region Constructors

            /// <summary>
            /// Definition class for writing log data
            /// </summary>
            /// <param name="logdata">input log data</param>
            public LogDataForFile(LogData logdata)
            {
                LogLevel = logdata.LogLevel;
                LogTime = logdata.LogTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
                StackFrams = GetCaller(logdata.StackFrams);
                ThreadID = logdata.ThreadID;
                Message = logdata.Message;
            }

            #endregion Constructors

            #region Properties

            public _LOGLEVEL LogLevel { get; set; }
            public string LogTime { get; set; }
            public string Message { get; set; }
            public string StackFrams { get; set; }
            public int ThreadID { get; set; }
            public string UserId { get; set; }

            #endregion Properties

            #region Methods

            private string GetCaller(StackFrame[] stackFrams)
            {
                string caller = "";
                for (int i = 0; i < stackFrams.Length; i++)
                {
                    if (stackFrams[i].GetMethod().DeclaringType == GetType())
                    {
                        continue;
                    }

                    if (stackFrams[i].GetMethod().ReflectedType.FullName.Contains("System."))
                    {
                        return caller;
                    }

                    if (caller.Length > 0)
                    {
                        caller = "-" + caller;
                    }

                    string callerInfo = string.Format("{0}:{1}",
                        stackFrams[i].GetMethod().DeclaringType.Name,
                        stackFrams[i].GetMethod().Name);

                    caller = callerInfo + caller;
                }

                return "Unknown";
            }

            #endregion Methods
        }

        #endregion Classes
    }
}
