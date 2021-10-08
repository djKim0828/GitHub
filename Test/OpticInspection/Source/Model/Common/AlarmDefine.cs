using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmWorks.App.OpticInspection
{
    public class AlarmDefine
    {
        #region Fields

        private static Dictionary<string, AlarmModel> _alarmConfig = new Dictionary<string, AlarmModel>();
        private static List<string> _alarmList = new List<string>();
        private static ConfigHandler<Dictionary<string, AlarmModel>> _configHandler;
        private static string _filePath;

        private static AlarmDefine _instance;

        #endregion Fields

        #region Constructors

        private AlarmDefine(string filePath = "")
        {
            if (filePath == "")
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
                currentPath = currentPath.TrimEnd(new char[] { '\\' });

                string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더
                _filePath = newPath + @"\Config\Alarm.json";
            }
            else
            {
                _filePath = filePath;
            }
            if (_configHandler == null)
            {
                _configHandler = new ConfigHandler<Dictionary<string, AlarmModel>>();
            }
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~AlarmDefine()
        {
        }

        #endregion Destructors

        #region Methods

        public static string GetAlarmRootPath()
        {
            return Path.GetDirectoryName(_filePath);
        }

        public static bool GetData(string id, ref AlarmModel alarm)
        {
            bool result = false;
            AlarmModel data = null;

            if (string.IsNullOrEmpty(id) == true)
            {
                return result;
            }// else

            try
            {
                result = _alarmConfig.TryGetValue(id, out data);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return result;
            }

            return result;
        }

        public static void InitInstance(string filePath = "")
        {
            if (filePath == string.Empty)
            {
                _instance = new AlarmDefine();
            }
            else
            {
                _instance = new AlarmDefine(filePath);
            }
        }

        /// <summary>
        /// 이 함수보다 GetData() 함수를 사용하도록 권장.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isExists(string id)
        {
            return _alarmList.Contains(id);
        }

        public static bool UpdateAlarm(AlarmModel alarmModel)
        {
            if (_instance == null)
            {
                _instance = new AlarmDefine();
            }// else

            if (GetData(alarmModel._Id, ref alarmModel) == false)
            {
                AddDictionary(alarmModel._Id, alarmModel);
            }// else

            return SaveDictionary();
        }

        public static bool UploadAlarm(string id, string name, AlarmType type, string imgPath)
        {
            AlarmModel newAlarm = new AlarmModel(id, name, type, imgPath);

            return UpdateAlarm(newAlarm);
        }

        private static void AddDictionary(string id, AlarmModel alarmModel)
        {
            _alarmConfig.Add(id, alarmModel);
        }

        private static void DefineAndLoadAlarm(AlarmModel alarmModel)
        {
            if (GetData(alarmModel._Id, ref alarmModel) == false)
            {
                _alarmConfig.Add(alarmModel._Id, alarmModel);
            }// else
        }

        private static bool LoadDictionary()
        {
            string directory = Path.GetDirectoryName(_filePath);

            try
            {
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                    return false;
                }// else

                if (File.Exists(_filePath) == false)
                {
                    return false;
                }// else
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            Dictionary<string, AlarmModel> data = new Dictionary<string, AlarmModel>();

            if (_configHandler.LoadConfig(_filePath, ref data) == false)
            {
                return false;
            }// else

            _alarmConfig = data;
            _alarmList = _alarmConfig.Keys.ToList();

            return true;
        }

        private static bool SaveDictionary()
        {
            if (_configHandler.SaveFile(_filePath, _alarmConfig) == false)
            {
                return false;
            }// else

            return LoadDictionary();
        }

        private void Initialize()
        {
            LoadDictionary();

            RegistAlarm();

            SaveDictionary();
        }

        private void RegistAlarm()
        {
            DefineAndLoadAlarm(new AlarmModel("aaa", "aaa", AlarmType.HeavyAlarm, ""));
            DefineAndLoadAlarm(new AlarmModel("bbb", "bbb", AlarmType.HeavyAlarm, ""));
        }

        #endregion Methods
    }
}