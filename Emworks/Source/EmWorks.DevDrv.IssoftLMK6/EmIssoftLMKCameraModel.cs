using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.IssoftLMK6
{
    [Serializable]
    public class EmIssoftLMKCameraModel
    {
        #region Fields

        private ConfigHandler<EmIssoftLMKCameraModel> _configHandler;

        #endregion Fields

        #region Properties
        
        public string CameraName { get; set; } = "0";

        public List<string> LensNameList { get; set; } = new List<string>();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmIssoftLMKCameraModel()
        {
            _configHandler = new ConfigHandler<EmIssoftLMKCameraModel>();
        }

        public EmIssoftLMKCameraModel(string filePath)
        {
            _configHandler = new ConfigHandler<EmIssoftLMKCameraModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmIssoftLMKCameraModel()
        {
            //
        }

        #endregion Destructors

        #region Methods

        public bool Load(string filepath, ref EmIssoftLMKCameraModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmIssoftLMKCameraModel loadConfig)
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