using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using static EmWorks.Lib.DriverCore.DriverBase;

namespace EmWorks.DevDrv.AjinMotionIo
{
    [Serializable]
    public class AjinMotionConfigModel
    {
        #region Fields

        private Dictionary<string, AxisConfigModel> _allAxis = null;

        private ConfigHandler<AjinMotionConfigModel> _configHandler;
        
        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        public AjinMotionConfigModel()
        {
            _configHandler = new ConfigHandler<AjinMotionConfigModel>();
        }

        public AjinMotionConfigModel(string filePath)
        {
            _configHandler = new ConfigHandler<AjinMotionConfigModel>(filePath);
        }

        #endregion Constructors

        #region Destructors

        ~AjinMotionConfigModel()
        {
            //
        }

        #endregion Destructors

        #region Properties
        
        public Define.RunMode RunMode { get; set; } = Define.RunMode.Simulation;
        public string RunModeString { get { return RunMode.ToString(); } }
        public int SpareAxisCount { get; set; } = 0;

        public Dictionary<string, AxisConfigModel> AllAxis
        {
            get
            {
                return _allAxis;
            }
            set
            {
                _allAxis = value;
            }
        }

        #endregion Properties

        #region Methods

        public bool Load()
        {
            AjinMotionConfigModel temp = new AjinMotionConfigModel();

            bool result = _configHandler.LoadConfig(ref temp);
            if (result == false)
            {
                return result;
            }

            var properties = typeof(AjinMotionConfigModel).GetProperties();
            foreach (var item in properties)
            {
                var property = typeof(AjinMotionConfigModel).GetProperty(item.Name);
                var value = property.GetValue(temp);
                object[] obj = new object[1];
                obj[0] = value;
                property.GetSetMethod(true).Invoke(this, obj);
            }
            return result;
        }

        public bool Load(string filepath, ref AjinMotionConfigModel loadConfig)
        {
            return _configHandler.LoadConfig(filepath, ref loadConfig);
        }

        public bool Load(ref AjinMotionConfigModel loadConfig)
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