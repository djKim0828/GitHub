using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    [Serializable]
    public class AutonicsTK4SModel
    {
        #region Fields

        private ConfigHandler<AutonicsTK4SModel> _configHandler;

        #endregion Fields

        #region Properties

        public string RunMode { get; set; } = "2";
        public ComportModel Comport { get; set; } = new ComportModel();
        public ComportModel DefaultComport { get; set; } = new ComportModel();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public AutonicsTK4SModel()
        {
            _configHandler = new ConfigHandler<AutonicsTK4SModel>();
        }

        public AutonicsTK4SModel(string filePath)
        {
            _configHandler = new ConfigHandler<AutonicsTK4SModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~AutonicsTK4SModel()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref AutonicsTK4SModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref AutonicsTK4SModel loadConfig)
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