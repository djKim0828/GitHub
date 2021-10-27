using EmWorks.Lib.Common;
using System;
using static EmWorks.Lib.DriverCore.DriverBase;

namespace EmWorks.DevDrv.SamwonTemp2500
{
    [Serializable]
    public class EmSamwonTemp2500Model
    {
        #region Fields

        private ConfigHandler<EmSamwonTemp2500Model> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmSamwonTemp2500Model()
        {
            _configHandler = new ConfigHandler<EmSamwonTemp2500Model>();
        }

        public EmSamwonTemp2500Model(string filePath)
        {
            _configHandler = new ConfigHandler<EmSamwonTemp2500Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmSamwonTemp2500Model()
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

        public bool Load(string filepath, ref EmSamwonTemp2500Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmSamwonTemp2500Model loadConfig)
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