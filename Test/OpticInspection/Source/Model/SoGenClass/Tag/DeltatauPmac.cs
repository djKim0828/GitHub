using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.MotionStandard;

namespace EmWorks.App.OpticInspection
{
    public class DeltatauPmac
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private EmMotionStandard _device;

        #endregion Fields

        #region Constructors

        public DeltatauPmac(string filePath, bool isSimulationMode)
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

        ~DeltatauPmac()
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
        public Tag xMotionSignalPositiveLimit
        {
            get
            {
                return _tags[nameof(xMotionSignalPositiveLimit)];
            }
        }
        public Tag xMotionSignalHome
        {
            get
            {
                return _tags[nameof(xMotionSignalHome)];
            }
        }
        public Tag xMotionSignalNegativeLimit
        {
            get
            {
                return _tags[nameof(xMotionSignalNegativeLimit)];
            }
        }
        public Tag xMotionSignalInpos
        {
            get
            {
                return _tags[nameof(xMotionSignalInpos)];
            }
        }
        public Tag xMotionSignalBusy
        {
            get
            {
                return _tags[nameof(xMotionSignalBusy)];
            }
        }
        public object yMotionServoOn
        {
            get
            {
                return _tags[nameof(yMotionServoOn)];
            }
            set
            {
                _tags[nameof(yMotionServoOn)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xMotionServoOn
        {
            get
            {
                return _tags[nameof(xMotionServoOn)];
            }
        }
        public object yMotionAlarmReset
        {
            get
            {
                return _tags[nameof(yMotionAlarmReset)];
            }
            set
            {
                _tags[nameof(yMotionAlarmReset)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xMotionAlarmStatus
        {
            get
            {
                return _tags[nameof(xMotionAlarmStatus)];
            }
        }
        public Tag xMotionAlarmCode
        {
            get
            {
                return _tags[nameof(xMotionAlarmCode)];
            }
        }
        public object yMotionHoming
        {
            get
            {
                return _tags[nameof(yMotionHoming)];
            }
            set
            {
                _tags[nameof(yMotionHoming)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xMotionHomingStatus
        {
            get
            {
                return _tags[nameof(xMotionHomingStatus)];
            }
        }
        public object yMotionPosMatch
        {
            get
            {
                return _tags[nameof(yMotionPosMatch)];
            }
            set
            {
                _tags[nameof(yMotionPosMatch)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionStop
        {
            get
            {
                return _tags[nameof(yMotionStop)];
            }
            set
            {
                _tags[nameof(yMotionStop)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionStopStatus
        {
            get
            {
                return _tags[nameof(xMotionStopStatus)];
            }
        }
        public object yMotionEStop
        {
            get
            {
                return _tags[nameof(yMotionEStop)];
            }
            set
            {
                _tags[nameof(yMotionEStop)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionSStop
        {
            get
            {
                return _tags[nameof(yMotionSStop)];
            }
            set
            {
                _tags[nameof(yMotionSStop)].Is = Convert.ToInt32(value);
            }
        }
        public object yMotionInposEnable
        {
            get
            {
                return _tags[nameof(yMotionInposEnable)];
            }
            set
            {
                _tags[nameof(yMotionInposEnable)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionInposEnable
        {
            get
            {
                return _tags[nameof(xMotionInposEnable)];
            }
        }
        public object yMotionInposRange
        {
            get
            {
                return _tags[nameof(yMotionInposRange)];
            }
            set
            {
                _tags[nameof(yMotionInposRange)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionInposRange
        {
            get
            {
                return _tags[nameof(xMotionInposRange)];
            }
        }
        public object yMotionMoveMode
        {
            get
            {
                return _tags[nameof(yMotionMoveMode)];
            }
            set
            {
                _tags[nameof(yMotionMoveMode)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xMotionMoveMode
        {
            get
            {
                return _tags[nameof(xMotionMoveMode)];
            }
        }
        public object yMotionMove
        {
            get
            {
                return _tags[nameof(yMotionMove)];
            }
            set
            {
                _tags[nameof(yMotionMove)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionCommand
        {
            get
            {
                return _tags[nameof(xMotionCommand)];
            }
        }
        public Tag xMotionActual
        {
            get
            {
                return _tags[nameof(xMotionActual)];
            }
        }
        public object yMotionVelocityMin
        {
            get
            {
                return _tags[nameof(yMotionVelocityMin)];
            }
            set
            {
                _tags[nameof(yMotionVelocityMin)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionVelocityMax
        {
            get
            {
                return _tags[nameof(yMotionVelocityMax)];
            }
            set
            {
                _tags[nameof(yMotionVelocityMax)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionVelocityMin
        {
            get
            {
                return _tags[nameof(xMotionVelocityMin)];
            }
        }
        public Tag xMotionVelocityMax
        {
            get
            {
                return _tags[nameof(xMotionVelocityMax)];
            }
        }
        public object yMotionVelocity
        {
            get
            {
                return _tags[nameof(yMotionVelocity)];
            }
            set
            {
                _tags[nameof(yMotionVelocity)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionVelocity
        {
            get
            {
                return _tags[nameof(xMotionVelocity)];
            }
        }
        public object yMotionAccel
        {
            get
            {
                return _tags[nameof(yMotionAccel)];
            }
            set
            {
                _tags[nameof(yMotionAccel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionAccel
        {
            get
            {
                return _tags[nameof(xMotionAccel)];
            }
        }
        public object yMotionDecel
        {
            get
            {
                return _tags[nameof(yMotionDecel)];
            }
            set
            {
                _tags[nameof(yMotionDecel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionDecel
        {
            get
            {
                return _tags[nameof(xMotionDecel)];
            }
        }
        public object yMotionHomeVelocity
        {
            get
            {
                return _tags[nameof(yMotionHomeVelocity)];
            }
            set
            {
                _tags[nameof(yMotionHomeVelocity)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeVel1st
        {
            get
            {
                return _tags[nameof(yMotionHomeVel1st)];
            }
            set
            {
                _tags[nameof(yMotionHomeVel1st)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeVel2nd
        {
            get
            {
                return _tags[nameof(yMotionHomeVel2nd)];
            }
            set
            {
                _tags[nameof(yMotionHomeVel2nd)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeVel3rd
        {
            get
            {
                return _tags[nameof(yMotionHomeVel3rd)];
            }
            set
            {
                _tags[nameof(yMotionHomeVel3rd)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeVel4th
        {
            get
            {
                return _tags[nameof(yMotionHomeVel4th)];
            }
            set
            {
                _tags[nameof(yMotionHomeVel4th)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeAcc1st
        {
            get
            {
                return _tags[nameof(yMotionHomeAcc1st)];
            }
            set
            {
                _tags[nameof(yMotionHomeAcc1st)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionHomeAcc2nd
        {
            get
            {
                return _tags[nameof(yMotionHomeAcc2nd)];
            }
            set
            {
                _tags[nameof(yMotionHomeAcc2nd)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionHomeVelocity
        {
            get
            {
                return _tags[nameof(xMotionHomeVelocity)];
            }
        }
        public Tag xMotionHomeVel1st
        {
            get
            {
                return _tags[nameof(xMotionHomeVel1st)];
            }
        }
        public Tag xMotionHomeVel2nd
        {
            get
            {
                return _tags[nameof(xMotionHomeVel2nd)];
            }
        }
        public Tag xMotionHomeVel3rd
        {
            get
            {
                return _tags[nameof(xMotionHomeVel3rd)];
            }
        }
        public Tag xMotionHomeVel4th
        {
            get
            {
                return _tags[nameof(xMotionHomeVel4th)];
            }
        }
        public Tag xMotionHomeAcc1st
        {
            get
            {
                return _tags[nameof(xMotionHomeAcc1st)];
            }
        }
        public Tag xMotionHomeAcc2nd
        {
            get
            {
                return _tags[nameof(xMotionHomeAcc2nd)];
            }
        }
        public object yMotionJogVelocity
        {
            get
            {
                return _tags[nameof(yMotionJogVelocity)];
            }
            set
            {
                _tags[nameof(yMotionJogVelocity)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionJogAccel
        {
            get
            {
                return _tags[nameof(yMotionJogAccel)];
            }
            set
            {
                _tags[nameof(yMotionJogAccel)].Is = Convert.ToDouble(value);
            }
        }
        public object yMotionJogDecel
        {
            get
            {
                return _tags[nameof(yMotionJogDecel)];
            }
            set
            {
                _tags[nameof(yMotionJogDecel)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xMotionJogVelocity
        {
            get
            {
                return _tags[nameof(xMotionJogVelocity)];
            }
        }
        public Tag xMotionJogAccel
        {
            get
            {
                return _tags[nameof(xMotionJogAccel)];
            }
        }
        public Tag xMotionJogDecel
        {
            get
            {
                return _tags[nameof(xMotionJogDecel)];
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
        public bool SetyMotionServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yMotionServoOn = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xMotionServoOn.Is == null)
                    {
                        continue;
                    }

                    if (xMotionServoOn.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xMotionServoOn.Is.ToString() == OpenClose.Error.ToString())
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
