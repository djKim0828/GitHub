using EmWorks.Lib.Common;
using System;
using System.IO;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="ConfigGeneral"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class ConfigGeneral
    {
        #region locale variable

        private static ConfigGeneralModel _configGeneralModel;
        private static ConfigHandler<ConfigGeneralModel> _configHandler;

        private static string _filePath;
        private static ConfigGeneral _instance;

        #endregion locale variable

        #region member properties

        public static string AccountFilePath
        {
            get
            {
                return _configGeneralModel.AccountFilePath;
            }
            set
            {
                _configGeneralModel.AccountFilePath = value;
            }
        }

        public static string AlarmDataFilePath
        {
            get
            {
                return _configGeneralModel.AlarmDataFilePath;
            }
            set
            {
                _configGeneralModel.AlarmDataFilePath = value;
            }
        }

        public static string AlarmListFilePath
        {
            get
            {
                return _configGeneralModel.AlarmListFilePath;
            }
            set
            {
                _configGeneralModel.AlarmListFilePath = value;
            }
        }

        public static string CellDataFilePath
        {
            get
            {
                return _configGeneralModel.CellDataFilePath;
            }
            set
            {
                _configGeneralModel.CellDataFilePath = value;
            }
        }

        public static string DeltatauMotionC8TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionC8TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionC8TagFilePath = value;
            }
        }

        public static string DeltatauMotionX1TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionX1TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionX1TagFilePath = value;
            }
        }

        public static string DeltatauMotionX3TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionX3TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionX3TagFilePath = value;
            }
        }

        public static string DeltatauMotionX6TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionX6TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionX6TagFilePath = value;
            }
        }

        public static string DeltatauMotionY2TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionY2TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionY2TagFilePath = value;
            }
        }

        public static string DeltatauMotionY7TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionY7TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionY7TagFilePath = value;
            }
        }

        public static string DeltatauMotionZ4TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionZ4TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionZ4TagFilePath = value;
            }
        }

        public static string DeltatauMotionZ5TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionZ5TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionZ5TagFilePath = value;
            }
        }

        public static string DeltatauMotionZ9TagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatauMotionZ9TagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatauMotionZ9TagFilePath = value;
            }
        }

        public static string DeltatusIoTagFilePath
        {
            get
            {
                return _configGeneralModel.DeltatusIoTagFilePath;
            }
            set
            {
                _configGeneralModel.DeltatusIoTagFilePath = value;
            }
        }

        public static string Language
        {
            get
            {
                return _configGeneralModel.Language;
            }
            set
            {
                _configGeneralModel.Language = value;
            }
        }

        public static string LanguageFilePath
        {
            get
            {
                return _configGeneralModel.LanguageFilePath;
            }
            set
            {
                _configGeneralModel.LanguageFilePath = value;
            }
        }

        public static string LogFilePath
        {
            get
            {
                return _configGeneralModel.LogFilePath;
            }
            set
            {
                _configGeneralModel.LogFilePath = value;
            }
        }

        public static string LogInAccount
        {
            get
            {
                return _configGeneralModel.LogInAccount;
            }
            set
            {
                _configGeneralModel.LogInAccount = value;
            }
        }

        public static string RootPath
        {
            get
            {
                return _configGeneralModel.RootPath;
            }
            set
            {
                _configGeneralModel.RootPath = value;
            }
        }

        public static string RunMode
        {
            get
            {
                return _configGeneralModel.RunMode;
            }
            set
            {
                _configGeneralModel.RunMode = value;
            }
        }

        public static string StartPage
        {
            get
            {
                return _configGeneralModel.StartPage;
            }
            set
            {
                _configGeneralModel.StartPage = value;
            }
        }

        public bool IsInitialled { get; protected set; }

        #endregion member properties

        #region EmWorks convention functions

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        /// <summary>
        /// 생성자, 필요시 FilePath를 변경합니다.
        /// </summary>
        /// <param name="filePath"></param>
        public ConfigGeneral(string filePath)
        {
            //_configGeneralModel = new ConfigGeneralModel();
            _configHandler = new ConfigHandler<ConfigGeneralModel>();
            LoadConfig(filePath);
        }

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        private ConfigGeneral()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더
            //_configGeneralModel = new ConfigGeneralModel();
            _configHandler = new ConfigHandler<ConfigGeneralModel>();

            LoadConfig(newPath + @"\Config\ConfigGeneral.json");
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~ConfigGeneral()
        {
            // add your code here
        }

        #region Methods

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : 초기화 또는 File Path를 지정할 때 사용
        /// </summary>
        /// <param name="dirPath"></param>
        public static void InitInstance(string filePath = "")
        {
            if (filePath == string.Empty)
            {
                _instance = new ConfigGeneral();
            }
            else
            {
                _instance = new ConfigGeneral(filePath);
            }

            #endregion Methods
        }

        /// <summary>
        /// Json 파일로 저장
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            return _configHandler.SaveFile(_filePath, _configGeneralModel);
        }

        /// <summary>
        /// Json 파일 로드
        /// </summary>
        /// <param name="filePath"></param>
        private static void LoadConfig(string filePath)
        {
            _configGeneralModel = new ConfigGeneralModel();

            _filePath = filePath;
            var temp = _configHandler.loadConfig(_filePath);
            if (temp != null)
            {
                _configGeneralModel = temp;
            }
        }

        #endregion EmWorks convention functions
    }
}