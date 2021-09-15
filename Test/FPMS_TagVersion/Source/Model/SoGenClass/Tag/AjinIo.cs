using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.AjinMotionIo;

namespace FPMS.E2105_FS111_121
{
    public class AjinIo
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _JsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public AjinIo(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmAjinIo(_tags, isSimulationMode);

        }

        public Tag this[string name]
        {
            get
            {
                return _tags[name];
            }
        }

        #endregion Constructors

        #region Destructors

        ~AjinIo()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceIo
        {
            get
            {
                return _tags[nameof(sxDeviceIo)];
            }
        }
        public object syDeviceIo
        {
            get
            {
                return _tags[nameof(syDeviceIo)];
            }
            set
            {
                _tags[nameof(syDeviceIo)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xDeviceLibraryVersion
        {
            get
            {
                return _tags[nameof(xDeviceLibraryVersion)];
            }
        }
        public Tag xSafetyEmoNormal
        {
            get
            {
                return _tags[nameof(xSafetyEmoNormal)];
            }
        }
        public Tag xSafetyDoorNormal
        {
            get
            {
                return _tags[nameof(xSafetyDoorNormal)];
            }
        }
        public Tag xSafetyTeachOn
        {
            get
            {
                return _tags[nameof(xSafetyTeachOn)];
            }
        }
        public Tag xSafetyAutoModeOn
        {
            get
            {
                return _tags[nameof(xSafetyAutoModeOn)];
            }
        }
        public Tag xSystemOpPanelEmo
        {
            get
            {
                return _tags[nameof(xSystemOpPanelEmo)];
            }
        }
        public Tag xSystemDeskEmo
        {
            get
            {
                return _tags[nameof(xSystemDeskEmo)];
            }
        }
        public Tag xSystemRoomEmo
        {
            get
            {
                return _tags[nameof(xSystemRoomEmo)];
            }
        }
        public Tag xSafetyDoorOpen
        {
            get
            {
                return _tags[nameof(xSafetyDoorOpen)];
            }
        }
        public Tag xSafetyDoorKeyOut
        {
            get
            {
                return _tags[nameof(xSafetyDoorKeyOut)];
            }
        }
        public Tag xSystemAlarmClearOn
        {
            get
            {
                return _tags[nameof(xSystemAlarmClearOn)];
            }
        }
        public Tag xSystemBuzzerStopOn
        {
            get
            {
                return _tags[nameof(xSystemBuzzerStopOn)];
            }
        }
        public Tag xChamberGlassDoorOpen
        {
            get
            {
                return _tags[nameof(xChamberGlassDoorOpen)];
            }
        }
        public Tag xChamberGlassDoorClose
        {
            get
            {
                return _tags[nameof(xChamberGlassDoorClose)];
            }
        }
        public Tag xChamberDoorLockOn
        {
            get
            {
                return _tags[nameof(xChamberDoorLockOn)];
            }
        }
        public Tag xUtilRoomOverTempAlarm
        {
            get
            {
                return _tags[nameof(xUtilRoomOverTempAlarm)];
            }
        }
        public Tag xUtilRoomFan1Alarm
        {
            get
            {
                return _tags[nameof(xUtilRoomFan1Alarm)];
            }
        }
        public Tag xUtilRoomFan2Alarm
        {
            get
            {
                return _tags[nameof(xUtilRoomFan2Alarm)];
            }
        }
        public Tag xUtilRoomFan3Alarm
        {
            get
            {
                return _tags[nameof(xUtilRoomFan3Alarm)];
            }
        }
        public Tag xUtilRoomFan4Alarm
        {
            get
            {
                return _tags[nameof(xUtilRoomFan4Alarm)];
            }
        }
        public Tag xRoomFan5Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan5Alarm)];
            }
        }
        public Tag xRoomFan6Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan6Alarm)];
            }
        }
        public Tag xRoomFan7Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan7Alarm)];
            }
        }
        public Tag xRoomFan8Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan8Alarm)];
            }
        }
        public Tag xRoomFan9Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan9Alarm)];
            }
        }
        public Tag xRoomFan10Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan10Alarm)];
            }
        }
        public Tag xRoomFan11Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan11Alarm)];
            }
        }
        public Tag xRoomFan12Alarm
        {
            get
            {
                return _tags[nameof(xRoomFan12Alarm)];
            }
        }
        public Tag xDetectorSafetyStopAlarm
        {
            get
            {
                return _tags[nameof(xDetectorSafetyStopAlarm)];
            }
        }
        public Tag xZaxisSafetyPositionOn
        {
            get
            {
                return _tags[nameof(xZaxisSafetyPositionOn)];
            }
        }
        public Tag xDutJigPlateFoldingOn
        {
            get
            {
                return _tags[nameof(xDutJigPlateFoldingOn)];
            }
        }
        public Tag xChamberOpenSensor1On
        {
            get
            {
                return _tags[nameof(xChamberOpenSensor1On)];
            }
        }
        public Tag xChamberOpenSensor1on
        {
            get
            {
                return _tags[nameof(xChamberOpenSensor1on)];
            }
        }
        public Tag xLedSourceLampOn
        {
            get
            {
                return _tags[nameof(xLedSourceLampOn)];
            }
        }
        public Tag xLedSourceConstOn
        {
            get
            {
                return _tags[nameof(xLedSourceConstOn)];
            }
        }
        public Tag xLedSourceOverOn
        {
            get
            {
                return _tags[nameof(xLedSourceOverOn)];
            }
        }
        public Tag xLedSourceBusyOn
        {
            get
            {
                return _tags[nameof(xLedSourceBusyOn)];
            }
        }
        public Tag xLedSourceError1Alarm
        {
            get
            {
                return _tags[nameof(xLedSourceError1Alarm)];
            }
        }
        public Tag xLedSourceError2Alarm
        {
            get
            {
                return _tags[nameof(xLedSourceError2Alarm)];
            }
        }
        public Tag xLedSourceError3Alarm
        {
            get
            {
                return _tags[nameof(xLedSourceError3Alarm)];
            }
        }
        public Tag xLedSourceCoverOpenAlarm
        {
            get
            {
                return _tags[nameof(xLedSourceCoverOpenAlarm)];
            }
        }
        public object yTowerLampRedOn
        {
            get
            {
                return _tags[nameof(yTowerLampRedOn)];
            }
            set
            {
                _tags[nameof(yTowerLampRedOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yTowerLampYellowOn
        {
            get
            {
                return _tags[nameof(yTowerLampYellowOn)];
            }
            set
            {
                _tags[nameof(yTowerLampYellowOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yTowerLampGreenOn
        {
            get
            {
                return _tags[nameof(yTowerLampGreenOn)];
            }
            set
            {
                _tags[nameof(yTowerLampGreenOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yTowerLampBuzzerOn
        {
            get
            {
                return _tags[nameof(yTowerLampBuzzerOn)];
            }
            set
            {
                _tags[nameof(yTowerLampBuzzerOn)].Is = Convert.ToInt32(value);
            }
        }
        public object ySafetyDoorLockRelease
        {
            get
            {
                return _tags[nameof(ySafetyDoorLockRelease)];
            }
            set
            {
                _tags[nameof(ySafetyDoorLockRelease)].Is = Convert.ToInt32(value);
            }
        }
        public object ySafetyModeKeyUnlock
        {
            get
            {
                return _tags[nameof(ySafetyModeKeyUnlock)];
            }
            set
            {
                _tags[nameof(ySafetyModeKeyUnlock)].Is = Convert.ToInt32(value);
            }
        }
        public object yChamberDoorOpen
        {
            get
            {
                return _tags[nameof(yChamberDoorOpen)];
            }
            set
            {
                _tags[nameof(yChamberDoorOpen)].Is = Convert.ToInt32(value);
            }
        }
        public object yChamberDoorClose
        {
            get
            {
                return _tags[nameof(yChamberDoorClose)];
            }
            set
            {
                _tags[nameof(yChamberDoorClose)].Is = Convert.ToInt32(value);
            }
        }
        public object ySystemAlarmClearOn
        {
            get
            {
                return _tags[nameof(ySystemAlarmClearOn)];
            }
            set
            {
                _tags[nameof(ySystemAlarmClearOn)].Is = Convert.ToInt32(value);
            }
        }
        public object ySystemBuzzerStopOn
        {
            get
            {
                return _tags[nameof(ySystemBuzzerStopOn)];
            }
            set
            {
                _tags[nameof(ySystemBuzzerStopOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceRemoteOn
        {
            get
            {
                return _tags[nameof(yLedSourceRemoteOn)];
            }
            set
            {
                _tags[nameof(yLedSourceRemoteOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD0On
        {
            get
            {
                return _tags[nameof(yLedSourceD0On)];
            }
            set
            {
                _tags[nameof(yLedSourceD0On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD1On
        {
            get
            {
                return _tags[nameof(yLedSourceD1On)];
            }
            set
            {
                _tags[nameof(yLedSourceD1On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD2On
        {
            get
            {
                return _tags[nameof(yLedSourceD2On)];
            }
            set
            {
                _tags[nameof(yLedSourceD2On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD3On
        {
            get
            {
                return _tags[nameof(yLedSourceD3On)];
            }
            set
            {
                _tags[nameof(yLedSourceD3On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD4On
        {
            get
            {
                return _tags[nameof(yLedSourceD4On)];
            }
            set
            {
                _tags[nameof(yLedSourceD4On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceD5On
        {
            get
            {
                return _tags[nameof(yLedSourceD5On)];
            }
            set
            {
                _tags[nameof(yLedSourceD5On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceF1On
        {
            get
            {
                return _tags[nameof(yLedSourceF1On)];
            }
            set
            {
                _tags[nameof(yLedSourceF1On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceF2On
        {
            get
            {
                return _tags[nameof(yLedSourceF2On)];
            }
            set
            {
                _tags[nameof(yLedSourceF2On)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceTimerResetOn
        {
            get
            {
                return _tags[nameof(yLedSourceTimerResetOn)];
            }
            set
            {
                _tags[nameof(yLedSourceTimerResetOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceRemotePowerOn
        {
            get
            {
                return _tags[nameof(yLedSourceRemotePowerOn)];
            }
            set
            {
                _tags[nameof(yLedSourceRemotePowerOn)].Is = Convert.ToInt32(value);
            }
        }
        public object yLedSourceFSW1On
        {
            get
            {
                return _tags[nameof(yLedSourceFSW1On)];
            }
            set
            {
                _tags[nameof(yLedSourceFSW1On)].Is = Convert.ToInt32(value);
            }
        }
        #endregion Properties

        #region Methods        

        public Dictionary<string, TagIdentity> GetAllData()
        {
            Dictionary<string, TagIdentity> temp = new Dictionary<string, TagIdentity>();
            foreach (Tag ti in _tags.Values)
            {
                temp.Add(ti.Name, ti.Identity);
            }

            return temp;
        }

        public Dictionary<string, Tag> GetTags()
        {
            return _tags;
        }

        public bool Load()
        {
            try
            {
                _JsonConvertor = new JavaScriptSerializer();

                string jsonString = File.ReadAllText(_filePath);

                Dictionary<string, TagIdentity> readData = _JsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

                _tags = new Dictionary<string, Tag>();

                foreach (TagIdentity ti in readData.Values)
                {
                    Tag temp = new Tag(ti);
                    _tags.Add(ti.Name, temp);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                string allText = _JsonConvertor.Serialize(GetAllData());

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(_filePath, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
        }

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else // timeout
            {
                Logger.Error("Timeout");
                return false;
            }
        }

        private Tag GetpropertyName(string propertyName)
        {
            return _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
        }

        public bool SetsyDeviceIo(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceIo = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (sxDeviceIo.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceIo.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceIo.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    if (isActionBreak == true)
                    {
                        break;
                    }

                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                if (isError == true)
                {
                    return false;
                }

                return true;
            }
        }
        #endregion Methods
    }
}
