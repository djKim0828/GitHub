using EmWorks.Lib.Common;
using System;
using static EmWorks.Lib.DriverCore.DriverBase;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    [Serializable]
    public class EmAutonicsTK4SModel
    {
        #region Fields

        private ConfigHandler<EmAutonicsTK4SModel> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmAutonicsTK4SModel()
        {
            _configHandler = new ConfigHandler<EmAutonicsTK4SModel>();
        }

        public EmAutonicsTK4SModel(string filePath)
        {
            _configHandler = new ConfigHandler<EmAutonicsTK4SModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmAutonicsTK4SModel()
        {
        }

        #endregion Destructors

        #region Properties

        public ComportModel Comport { get; set; } = new ComportModel();
        public ComportModel DefaultComport { get; set; } = new ComportModel();
        public Define.RunMode RunMode { get; set; } = Define.RunMode.Simulation;
        public string RunModeString { get { return RunMode.ToString(); } }

        #endregion Properties

        #region Methods

        public bool Load(string filepath, ref EmAutonicsTK4SModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmAutonicsTK4SModel loadConfig)
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