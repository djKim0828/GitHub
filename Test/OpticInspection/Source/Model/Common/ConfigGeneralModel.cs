using EmWorks.Lib.Common;
using System;

namespace EmWorks.App.OpticInspection
{
    [Serializable]
    public class ConfigGeneralModel
    {
        #region Fields

        private ConfigHandler<ConfigGeneralModel> _configHandler;

        #endregion Fields

        #region Properties

        public string AccountFilePath { get; set; } = "\\Config\\configAccount.json";
        public string AlarmDataFilePath { get; set; } = "\\Data\\Alarm\\";
        public string AlarmListFilePath { get; set; } = "\\Config\\Alarm\\Alarm.json";
        public string CellDataFilePath { get; set; } = "\\Data\\Cell\\";
        public string DeltatauMotionC8TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacC8.json";
        public string DeltatauMotionX1TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacX1.json";
        public string DeltatauMotionX3TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacX3.json";
        public string DeltatauMotionX6TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacX6.json";
        public string DeltatauMotionY2TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacY2.json";
        public string DeltatauMotionY7TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacY7.json";
        public string DeltatauMotionZ4TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacZ4.json";
        public string DeltatauMotionZ5TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacZ5.json";
        public string DeltatauMotionZ9TagFilePath { get; set; } = "\\Config\\Tag\\DeltatauPmacZ9.json";
        public string TopconSR5000TagFilePath { get; set; } = "\\Config\\Tag\\TopconSR5000.json";
        public string DeltatusIoTagFilePath { get; set; } = "\\Config\\Tag\\AjinIo.json";
        public string Language { get; set; } = "EN";
        public string LanguageFilePath { get; set; } = "\\Language\\";
        public string LogFilePath { get; set; } = "\\Log\\Log\\";
        public string LogInAccount { get; set; } = "0";
        public string RecipeFilePath { get; set; } = "\\Config\\Recipe.json";
        public string RootPath { get; set; } = string.Empty;
        public string RunMode { get; set; } = "2";
        public string StartPage { get; set; } = "1";

        public int MaximumCellCapacity { get; set; } = 1; // 검사할 수 있는 최대 Cell(Wafer) 갯수
        public int MotionCheckIntervalTime { get; set; } = 50; // 모션 위치 검색 인터발

        public string ResultFilePath { get; set; } = "Result\\";
        public string ResultPsudoFilePath { get; set; } = "Pseudo\\";
        public string ResultRGBFilePath { get; set; } = "RGB\\";       

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public ConfigGeneralModel()
        {
            _configHandler = new ConfigHandler<ConfigGeneralModel>();
        }

        public ConfigGeneralModel(string filePath)
        {
            _configHandler = new ConfigHandler<ConfigGeneralModel>(filePath);
        }
        #endregion Constructors

        #region Destructors

        ~ConfigGeneralModel()
        {
            //
        }

        #endregion Destructors

        public bool Load(string filepath, ref ConfigGeneralModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref ConfigGeneralModel loadConfig)
        {
            return _configHandler.LoadConfig(ref loadConfig);
        }

        public bool Save()
        {
            return _configHandler.SaveFile(this);
        }

        public bool Save(string filepath)
        {
            return _configHandler.SaveFile(filepath, this);
        }

    }
}
