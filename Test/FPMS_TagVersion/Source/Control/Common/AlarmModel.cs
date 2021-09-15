using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FPMS.E2105_FS111_121
{
    public enum AlarmType
    {
        HeavyAlarm,
        LightAlarm
    }

    [Serializable]
    public class AlarmModel
    {
        #region Fields

        private CollectionHandler<AlarmModel> _collectionHandler;
        private string _datePathFormat = "yyyyMMdd";//저장 경로 날자 형식
        private string _fileNameExtension = ".enc";//저장 확장자

        #endregion Fields

        #region Properties

        public DateTime _DateTime { get; set; } = DateTime.Now;
        public string _DateString { get { return _DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture); } }

        //
        public string _Description { get; set; } = "";
        public string _Id { get; set; } = "AlarmId";
        public string _ImgPath { get; set; } = "";
        public string _Name { get; set; } = "Init";
        public StatusType.Alarm _State { get; set; } = StatusType.Alarm.Default;
        public string _StateString { get { return _State.ToString(); }}
        public AlarmType _Type { get; set; } = AlarmType.LightAlarm;
        public string _TypeString { get { return _Type.ToString(); }}
        #endregion Properties

        #region Constructors

        public AlarmModel()
        {
            _collectionHandler = new CollectionHandler<AlarmModel>();
        }

        public AlarmModel(string filePath, int storagePeriod = 180)
        {
            _collectionHandler = new CollectionHandler<AlarmModel>(filePath, storagePeriod);
        }

        /// <summary>
        /// 알람 정의시 사용.
        /// </summary>
        /// <param name="id">알람 ID</param>
        /// <param name="type">알람 타입 경알람 중알람</param>
        /// <param name="name">알람 명칭</param>
        /// <param name="imgPath">알람 이미지 경로</param>
        public AlarmModel(string id, string name, AlarmType type = AlarmType.LightAlarm, string imgPath = "")
        {
            this._Type = type;
            this._Id = id;
            this._Name = name;
            this._ImgPath = imgPath;
        }

        #endregion Constructors

        #region Methods

        public string GetDatePathFormat()
        {
            return _datePathFormat;
        }

        public string GetFileExtension()
        {
            return _fileNameExtension;
        }

        public List<AlarmModel> LoadList(string filepath)
        {
            return _collectionHandler.LoadList(filepath);
        }

        public bool SaveData(AlarmModel data, StatusType.Alarm statusType)
        {
            data._DateTime = DateTime.Now;

            data._State = statusType;

            return SaveLine(data);
        }

        public bool SaveData(ref AlarmModel data, StatusType.Alarm statusType)
        {
            data._DateTime = DateTime.Now;

            data._State = statusType;

            return SaveLine(data);
        }

        public bool SaveData(string id, AlarmType type, string name, StatusType.Alarm state, string imgPath)
        {
            this._DateTime = DateTime.Now;

            this._Type = type;
            this._Id = id;
            this._Name = name;
            this._State = state;
            this._ImgPath = imgPath;

            return SaveLine();
        }

        public void StartWrite()
        {
            _collectionHandler.StartWrite();
        }

        public void StopWrite()
        {
            _collectionHandler.StopWrite();
        }

        private string GetSubPath()
        {
            return GetSubPath(this);
        }

        private string GetSubPath(AlarmModel data)
        {
            //상황별로 Path 자유롭게 수정.
            string datePath = _DateTime.ToString(_datePathFormat, CultureInfo.InvariantCulture);

            string subPath = "\\" + datePath + "\\" + datePath + _fileNameExtension; //Use Date
            //string subPath = data._Id + "\\" + data._Id + _fileNameExtension; //Use ID

            return subPath;
        }

        private bool SaveLine(AlarmModel data)
        {
            string subPath = GetSubPath(data);
            return _collectionHandler.SaveLine(subPath, data);
        }

        private bool SaveLine()
        {
            return SaveLine(this);
        }

        #endregion Methods
    }
}