using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    [Serializable]
    public class SamwonSDR100Model
    {
        #region Fields

        private ConfigHandler<SamwonSDR100Model> _configHandler;

        #endregion Fields

        #region Properties

        public string RunMode { get; set; } = "2";
        public bool Enable { get; set; } = false;
        public ComportModel Comport { get; set; } = new ComportModel();
        public ComportModel DefaultComport { get; set; } = new ComportModel();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public SamwonSDR100Model()
        {
            _configHandler = new ConfigHandler<SamwonSDR100Model>();
        }

        public SamwonSDR100Model(string filePath)
        {
            _configHandler = new ConfigHandler<SamwonSDR100Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~SamwonSDR100Model()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref SamwonSDR100Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref SamwonSDR100Model loadConfig)
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