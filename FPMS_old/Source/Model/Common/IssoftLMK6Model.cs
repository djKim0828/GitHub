using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    [Serializable]
    public class IssoftLMK6Model
    {
        #region Fields

        private ConfigHandler<IssoftLMK6Model> _configHandler;

        #endregion Fields

        #region Properties

        //Todo default 값 수정
        public string Camera { get; set; } = "2";
        public string ConorscopeLens { get; set; } = "2";
        public string NomalFactor { get; set; } = "2";
        public string NomalFactorIndex { get; set; } = "2";
        public string NomalLens { get; set; } = "2";
        public string RunMode { get; set; } = "2";

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public IssoftLMK6Model()
        {
            _configHandler = new ConfigHandler<IssoftLMK6Model>();
        }

        public IssoftLMK6Model(string filePath)
        {
            _configHandler = new ConfigHandler<IssoftLMK6Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~IssoftLMK6Model()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref IssoftLMK6Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref IssoftLMK6Model loadConfig)
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