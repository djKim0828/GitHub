using System;
using System.Collections.Generic;
using System.Globalization;

namespace EmWorks.Lib.Common
{
    [Serializable]
    public class LogData
    {
        #region Enums

        public enum Level
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

        #region Fields

        private CollectionHandler<LogData> _collectionHandler;
        private string _dateFileNameFormat = "yyyyMMdd_HH";//Todo: 저장 경로 날자 형식
        private string _dateFolderPathFormat = "yyyyMMdd";//Todo: 저장 경로 날자 형식
        private string _fileNameExtension = ".log";//Todo: 저장 확장자
        private string _dateString = string.Empty;

        #endregion Fields

        #region Properties

        public DateTime DateTime { get; set; } = DateTime.Now;
        public string DateString {
            get
            {
                if (string.IsNullOrEmpty(_dateString) == true)
                {
                    return DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                }//else

                return _dateString;
            }
            set
            {
                _dateString = value;
            }
        }
        //

        public Level LogLevel { get; set; } = Level.INFO;
        public string LogLevelString { get { return LogLevel.ToString(); } set { Enum.Parse(typeof(Level), value); } }
        public string Message { get; set; } = string.Empty;
        public string StackFrams { get; set; } = string.Empty;
        public int ThreadId { get; set; } = -1;
        public string UserId { get; set; } = string.Empty;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public LogData()
        {
            _collectionHandler = new CollectionHandler<LogData>();
        }

        public LogData(string filePath, int storagePeriod = 30)
        {
            _collectionHandler = new CollectionHandler<LogData>(filePath);
        }

        /// <summary>
        /// data 선언 시 사용
        /// </summary>
        /// <param name="name">명칭</param>
        public LogData(Level level, int threadId, string stackFrams, string message)//Todo: 파라메타로 선언 시 사용. ex name
        {
            this.LogLevel = level;
            this.ThreadId = threadId;
            this.StackFrams = stackFrams;
            this.Message = message;
        }

        #endregion Constructors

        #region Destructors

        ~LogData()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public string GetDatePathFormat()
        {
            return _dateFolderPathFormat;
        }

        public string GetFileExtension()
        {
            return _fileNameExtension;
        }

        public List<LogData> LoadList(string filepath)
        {
            return _collectionHandler.LoadList(filepath);
        }

        public bool SaveData(LogData data)
        {
            data.DateTime = DateTime.Now;

            return SaveLine(data);
        }

        public bool SaveData(ref LogData data)
        {
            data.DateTime = DateTime.Now;

            return SaveLine(data);
        }

        public bool SaveData(Level logLevel) //Todo: 저장할 파라메타 추가. ex name
        {
            this.DateTime = DateTime.Now;

            this.LogLevel = logLevel;

            return SaveLine();
        }

        public void StartWrite()
        {
            _collectionHandler.StartWrite();
        }

        public void StopWrite()
        {
            _collectionHandler.StopWrite();
        }

        private string GetSubPath()
        {
            return GetSubPath(this);
        }

        private string GetSubPath(LogData data)
        {
            //Todo: 상황별로 Path 자유롭게 수정.
            string dateFileName = DateTime.ToString(_dateFileNameFormat, CultureInfo.InvariantCulture);
            string dateFolderPath = DateTime.ToString(_dateFolderPathFormat, CultureInfo.InvariantCulture);
            string subPath = "\\" + dateFolderPath + "\\" + dateFileName + _fileNameExtension; //Use Date
            //string subPath = data._Id + "\\" + data._Id + _fileNameExtension; //Use ID

            return subPath;
        }

        private bool SaveLine(LogData data)
        {
            string subPath = GetSubPath(data);
            return _collectionHandler.SaveLine(subPath, data);
        }

        private bool SaveLine()
        {
            return SaveLine(this);
        }

        #endregion Methods
    }
}