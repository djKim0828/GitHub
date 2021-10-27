using EmWorks.Lib.Common;
using System;
using static EmWorks.Lib.DriverCore.DriverBase;

namespace EmWorks.DevDrv.SamwonSDR100
{
    [Serializable]
    public class EmSamwonSDR100Model
    {
        #region Fields

        private ConfigHandler<EmSamwonSDR100Model> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmSamwonSDR100Model()
        {
            _configHandler = new ConfigHandler<EmSamwonSDR100Model>();
        }

        public EmSamwonSDR100Model(string filePath)
        {
            _configHandler = new ConfigHandler<EmSamwonSDR100Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmSamwonSDR100Model()
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

        public bool Load(string filepath, ref EmSamwonSDR100Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmSamwonSDR100Model loadConfig)
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