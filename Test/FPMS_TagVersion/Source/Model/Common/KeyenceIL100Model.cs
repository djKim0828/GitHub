using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    [Serializable]
    public class KeyenceIL100Model
    {
        #region Fields

        private ConfigHandler<KeyenceIL100Model> _configHandler;

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
        public KeyenceIL100Model()
        {
            _configHandler = new ConfigHandler<KeyenceIL100Model>();
        }

        public KeyenceIL100Model(string filePath)
        {
            _configHandler = new ConfigHandler<KeyenceIL100Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~KeyenceIL100Model()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref KeyenceIL100Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref KeyenceIL100Model loadConfig)
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