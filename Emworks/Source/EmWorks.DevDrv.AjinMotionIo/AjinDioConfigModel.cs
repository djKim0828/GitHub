using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.AjinMotionIo
{
    [Serializable]
    public class AjinDioConfigModel
    {
        #region Fields

        private ConfigHandler<AjinDioConfigModel> _configHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public AjinDioConfigModel()
        {
            _configHandler = new ConfigHandler<AjinDioConfigModel>();
        }

        public AjinDioConfigModel(string filePath)
        {
            _configHandler = new ConfigHandler<AjinDioConfigModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~AjinDioConfigModel()
        {
            //
        }

        #endregion Destructors

        #region Properties

        public Dictionary<string, DioModel> DigitalInput { get; set; }
        public Dictionary<string, DioModel> DigitalOutput { get; set; }

        #endregion Properties

        #region Methods

        public bool Load(string filepath, ref AjinDioConfigModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref AjinDioConfigModel loadConfig)
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