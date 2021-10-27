using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.IssoftLMK6
{
    [Serializable]
    public class EmIssoftLMK6Model
    {
        #region Fields

        private ConfigHandler<EmIssoftLMK6Model> _configHandler;

        #endregion Fields

        #region Properties

        //Todo default 값 수정
        public Define.RunMode RunMode { get; set; } = Define.RunMode.Simulation;
        public string RunModeString { get { return RunMode.ToString(); } }

        public string Camera { get; set; } = "2";
        public string LensFactor { get; set; } = "2";
        public int LensFactorIndex { get; set; } = 0;
        public string Lens { get; set; } = "2";
        public List<EmIssoftLMKCameraModel> CameraInfo { get; set; } = null;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmIssoftLMK6Model()
        {
            _configHandler = new ConfigHandler<EmIssoftLMK6Model>();
        }

        public EmIssoftLMK6Model(string filePath)
        {
            _configHandler = new ConfigHandler<EmIssoftLMK6Model>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmIssoftLMK6Model()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref EmIssoftLMK6Model loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmIssoftLMK6Model loadConfig)
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
