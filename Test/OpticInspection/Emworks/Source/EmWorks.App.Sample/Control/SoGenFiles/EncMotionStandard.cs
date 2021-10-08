using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.MotionStandard;

namespace EmWorks.App.Sample
{
    public class EncMotionStandard
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private EmMotionStandard _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public EncMotionStandard(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmMotionStandard(_tags, isSimulationMode);

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

        ~EncMotionStandard()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceMotion
        {
            get
            {
                return _tags[nameof(sxDeviceMotion)];
            }
        }
        public object syDeviceMotion
        {
            get
            {
                return _tags[nameof(syDeviceMotion)];
            }
            set
            {
                _tags[nameof(syDeviceMotion)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xDeviceDriverVersion
        {
            get
            {
                return _tags[nameof(xDeviceDriverVersion)];
            }
        }
        public Tag xDeviceExternalLibVersion
        {
            get
            {
                return _tags[nameof(xDeviceExternalLibVersion)];
            }
        }
        public Tag xInShuttleX1SignalPositiveLimit
        {
            get
            {
                return _tags[nameof(xInShuttleX1SignalPositiveLimit)];
            }
        }
        public Tag xInShuttleX1SignalHome
        {
            get
            {
                return _tags[nameof(xInShuttleX1SignalHome)];
            }
        }
        public Tag xInShuttleX1SignalNegativeLimit
        {
            get
            {
                return _tags[nameof(xInShuttleX1SignalNegativeLimit)];
            }
        }
        public Tag xInShuttleX1SignalInpos
        {
            get
            {
                return _tags[nameof(xInShuttleX1SignalInpos)];
            }
        }
        public Tag xInShuttleX1SignalBusy
        {
            get
            {
                return _tags[nameof(xInShuttleX1SignalBusy)];
            }
        }
        public object yInShuttleX1ServoOn
        {
            get
            {
                return _tags[nameof(yInShuttleX1ServoOn)];
            }
            set
            {
                _tags[nameof(yInShuttleX1ServoOn)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xInShuttleX1ServoOn
        {
            get
            {
                return _tags[nameof(xInShuttleX1ServoOn)];
            }
        }
        public object yInShuttleX1AlarmReset
        {
            get
            {
                return _tags[nameof(yInShuttleX1AlarmReset)];
            }
            set
            {
                _tags[nameof(yInShuttleX1AlarmReset)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xInShuttleX1AlarmStatus
        {
            get
            {
                return _tags[nameof(xInShuttleX1AlarmStatus)];
            }
        }
        public Tag xInShuttleX1AlarmCode
        {
            get
            {
                return _tags[nameof(xInShuttleX1AlarmCode)];
            }
        }
        public object yInShuttleX1Homing
        {
            get
            {
                return _tags[nameof(yInShuttleX1Homing)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Homing)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xInShuttleX1HomingStatus
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomingStatus)];
            }
        }
        public object yInShuttleX1PosMatch
        {
            get
            {
                return _tags[nameof(yInShuttleX1PosMatch)];
            }
            set
            {
                _tags[nameof(yInShuttleX1PosMatch)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1Stop
        {
            get
            {
                return _tags[nameof(yInShuttleX1Stop)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Stop)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1StopStatus
        {
            get
            {
                return _tags[nameof(xInShuttleX1StopStatus)];
            }
        }
        public object yInShuttleX1EStop
        {
            get
            {
                return _tags[nameof(yInShuttleX1EStop)];
            }
            set
            {
                _tags[nameof(yInShuttleX1EStop)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1SStop
        {
            get
            {
                return _tags[nameof(yInShuttleX1SStop)];
            }
            set
            {
                _tags[nameof(yInShuttleX1SStop)].Is = Convert.ToInt32(value);
            }
        }
        public object yInShuttleX1InposEnable
        {
            get
            {
                return _tags[nameof(yInShuttleX1InposEnable)];
            }
            set
            {
                _tags[nameof(yInShuttleX1InposEnable)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1InposEnable
        {
            get
            {
                return _tags[nameof(xInShuttleX1InposEnable)];
            }
        }
        public object yInShuttleX1InposRange
        {
            get
            {
                return _tags[nameof(yInShuttleX1InposRange)];
            }
            set
            {
                _tags[nameof(yInShuttleX1InposRange)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1InposRange
        {
            get
            {
                return _tags[nameof(xInShuttleX1InposRange)];
            }
        }
        public object yInShuttleX1MoveMode
        {
            get
            {
                return _tags[nameof(yInShuttleX1MoveMode)];
            }
            set
            {
                _tags[nameof(yInShuttleX1MoveMode)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xInShuttleX1MoveMode
        {
            get
            {
                return _tags[nameof(xInShuttleX1MoveMode)];
            }
        }
        public object yInShuttleX1Move
        {
            get
            {
                return _tags[nameof(yInShuttleX1Move)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Move)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1Command
        {
            get
            {
                return _tags[nameof(xInShuttleX1Command)];
            }
        }
        public Tag xInShuttleX1Actual
        {
            get
            {
                return _tags[nameof(xInShuttleX1Actual)];
            }
        }
        public object yInShuttleX1VelocityMin
        {
            get
            {
                return _tags[nameof(yInShuttleX1VelocityMin)];
            }
            set
            {
                _tags[nameof(yInShuttleX1VelocityMin)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1VelocityMax
        {
            get
            {
                return _tags[nameof(yInShuttleX1VelocityMax)];
            }
            set
            {
                _tags[nameof(yInShuttleX1VelocityMax)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1VelocityMin
        {
            get
            {
                return _tags[nameof(xInShuttleX1VelocityMin)];
            }
        }
        public Tag xInShuttleX1VelocityMax
        {
            get
            {
                return _tags[nameof(xInShuttleX1VelocityMax)];
            }
        }
        public object yInShuttleX1Velocity
        {
            get
            {
                return _tags[nameof(yInShuttleX1Velocity)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Velocity)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1Velocity
        {
            get
            {
                return _tags[nameof(xInShuttleX1Velocity)];
            }
        }
        public object yInShuttleX1Accel
        {
            get
            {
                return _tags[nameof(yInShuttleX1Accel)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Accel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1Accel
        {
            get
            {
                return _tags[nameof(xInShuttleX1Accel)];
            }
        }
        public object yInShuttleX1Decel
        {
            get
            {
                return _tags[nameof(yInShuttleX1Decel)];
            }
            set
            {
                _tags[nameof(yInShuttleX1Decel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1Decel
        {
            get
            {
                return _tags[nameof(xInShuttleX1Decel)];
            }
        }
        public object yInShuttleX1HomeVelocity
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeVelocity)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeVelocity)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeVel1st
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeVel1st)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeVel1st)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeVel2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeVel2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeVel2nd)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeVel3rd
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeVel3rd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeVel3rd)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeVel4th
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeVel4th)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeVel4th)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeAcc1st
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeAcc1st)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeAcc1st)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1HomeAcc2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeAcc2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeAcc2nd)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1HomeVelocity
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeVelocity)];
            }
        }
        public Tag xInShuttleX1HomeVel1st
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeVel1st)];
            }
        }
        public Tag xInShuttleX1HomeVel2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeVel2nd)];
            }
        }
        public Tag xInShuttleX1HomeVel3rd
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeVel3rd)];
            }
        }
        public Tag xInShuttleX1HomeVel4th
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeVel4th)];
            }
        }
        public Tag xInShuttleX1HomeAcc1st
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeAcc1st)];
            }
        }
        public Tag xInShuttleX1HomeAcc2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeAcc2nd)];
            }
        }
        public object yInShuttleX1JogVelocity
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogVelocity)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogVelocity)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1JogAccel
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogAccel)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogAccel)].Is = Convert.ToDouble(value);
            }
        }
        public object yInShuttleX1JogDecel
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogDecel)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogDecel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xInShuttleX1JogVelocity
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogVelocity)];
            }
        }
        public Tag xInShuttleX1JogAccel
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogAccel)];
            }
        }
        public Tag xInShuttleX1JogDecel
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogDecel)];
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
                _jsonConvertor = new JavaScriptSerializer();

                string jsonString = File.ReadAllText(_filePath);

                Dictionary<string, TagIdentity> readData = _jsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

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
                string allText = _jsonConvertor.Serialize(GetAllData());

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

        public bool SetsyDeviceMotion(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceMotion = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceMotion.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceMotion.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceMotion.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetyInShuttleX1ServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yInShuttleX1ServoOn = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xInShuttleX1ServoOn.Is == null)
                    {
                        continue;
                    }

                    if (xInShuttleX1ServoOn.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xInShuttleX1ServoOn.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
