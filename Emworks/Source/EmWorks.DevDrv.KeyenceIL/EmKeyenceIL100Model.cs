using EmWorks.Lib.Common;
using System;
using static EmWorks.Lib.DriverCore.DriverBase;

namespace EmWorks.DevDrv.KeyenceIL100
{
    [Serializable]
    public class EmKeyenceIL100Model
    {
        #region Fields

        private ConfigHandler<EmKeyenceIL100Model> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmKeyenceIL100Model()
        {
            _configHandler = new ConfigHandler<EmKeyenceIL100Model>();
        }

        public EmKeyenceIL100Model(string filePath)
        {
            _configHandler = new ConfigHandler<EmKeyenceIL100Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmKeyenceIL100Model()
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

        public bool Load(string filepath, ref EmKeyenceIL100Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmKeyenceIL100Model loadConfig)
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