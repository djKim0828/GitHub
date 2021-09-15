using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    [Serializable]
    public class ConfigGeneralModel
    {
        #region Fields

        private ConfigHandler<ConfigGeneralModel> _configHandler;

        #endregion Fields

        #region Properties

        public string Language { get; set; } = "EN";
        public string LanguageFilePath { get; set; } = "\\Language\\";
        public string LogFilePath { get; set; } = "\\Data\\Log\\";
        public int LogStoragePeriodDate { get; set; } = 180;

        public string AjinIoTagFilePath { get; set; } = "\\Config\\Tag\\AjinIo.json";
        public string AjinMotionXTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionX.json";
        public string AjinMotionYTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionY.json";
        public string AjinMotionZTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionZ.json";
        public string AjinMotionVTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionV.json";
        public string AjinMotionHTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionH.json";
        public string AjinMotionRTagFilePath { get; set; } = "\\Config\\Tag\\AjinMotionR.json";
        public string AlarmDataFilePath { get; set; } = "\\Data\\Alarm\\";
        public string AlarmListFilePath { get; set; } = "\\Config\\Alarm\\Alarm.json";

        public string RootPath { get; set; } = string.Empty;
        public string RunMode { get; set; } = "2";
        public string StartPage { get; set; } = "1";

        public string ModelFilePath { get; set; } = "\\Data\\Model";
        public string PatternFilePath { get; set; } = "\\Data\\Pattern";
        public string RecipeFilePath { get; set; } = "\\Data\\Recipe";
        public string SequenceFilePath { get; set; } = "\\Data\\Sequence";

        public string SamwonTemp1500TagFilePath { get; set; } = "\\Config\\Tag\\SamwonTemp1500.json";
        public string SamwonSDR100TagFilePath { get; set; } = "\\Config\\Tag\\SamwonSDR100.json";

        public string CenterPosX { get; set; } = "0";
        public string CenterPosY { get; set; } = "0";
        public string CenterPosZ { get; set; } = "0";
        public string CenterPosR { get; set; } = "0";
        public string CenterPosV { get; set; } = "0";
        public string CenterPosH { get; set; } = "0";
               

        // 모션 위치 검색 인터발

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

        #region Methods

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

        #endregion Methods
    }
}
