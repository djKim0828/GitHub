using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EmWorks.App.OpticInspection
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

        public string _DateString { get; set; } = "";
        public DateTime _DateTime { get; set; } = DateTime.Now;

        //
        public string _Description { get; set; } = "";

        public string _Id { get; set; } = "AlarmId";
        public string _ImgPath { get; set; } = "";
        public string _Name { get; set; } = "Init";
        public StatusType.Alarm _State { get; set; } = StatusType.Alarm.Default;
        public string _StateString { get; set; } = "";
        public AlarmType _Type { get; set; } = AlarmType.HeavyAlarm;
        public string _TypeString { get; set; } = ""; // Todo: private get set 으로  변경하여 TEST 필요

        #endregion Properties

        #region Constructors

        public AlarmModel()
        {
            _collectionHandler = new CollectionHandler<AlarmModel>();
        }

        public AlarmModel(string filePath)
        {
            _collectionHandler = new CollectionHandler<AlarmModel>(filePath);
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

        public List<AlarmModel> LoadLog(string filepath)
        {
            return _collectionHandler.LoadList(filepath);
        }

        public bool SaveData(AlarmModel data, StatusType.Alarm statusType)
        {
            data._DateTime = DateTime.Now;
            data._DateString = data._DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            data._State = statusType;
            data._TypeString = data._Type.ToString();
            data._StateString = data._State.ToString();

            return SaveLine(data);
        }

        public bool SaveData(ref AlarmModel data, StatusType.Alarm statusType)
        {
            data._DateTime = DateTime.Now;
            data._DateString = data._DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            data._State = statusType;
            data._TypeString = data._Type.ToString();
            data._StateString = data._State.ToString();

            return SaveLine(data);
        }

        public bool SaveData(string id, AlarmType type, string name, StatusType.Alarm state, string imgPath)
        {
            this._DateTime = DateTime.Now;
            this._DateString = _DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            this._Type = type;
            this._TypeString = _Type.ToString();
            this._Id = id;
            this._Name = name;
            this._State = state;
            this._StateString = _State.ToString();
            this._ImgPath = imgPath;

            return SaveLine();
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