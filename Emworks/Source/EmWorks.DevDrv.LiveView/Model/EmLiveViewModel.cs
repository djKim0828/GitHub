using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.LiveView
{
    [Serializable]
    public class EmLiveViewModel
    {
        #region Fields

        private ConfigHandler<EmLiveViewModel> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public EmLiveViewModel()
        {
            _configHandler = new ConfigHandler<EmLiveViewModel>();
        }

        public EmLiveViewModel(string filePath)
        {
            _configHandler = new ConfigHandler<EmLiveViewModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~EmLiveViewModel()
        {
            //
        }

        #endregion Destructors

        #region Properties

        public List<string> CameraIP { get; set; } = new List<string>();

        #endregion Properties

        #region Methods

        public bool Load(string filepath, ref EmLiveViewModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref EmLiveViewModel loadConfig)
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