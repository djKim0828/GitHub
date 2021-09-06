using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.AjinMotionIo;

namespace EmWorks.App.Sample
{
    public class AjinMotion
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public AjinMotion(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmAjinMotion(_tags, isSimulationMode); // Todo : 사용할 Device 로 변경 必

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

        ~AjinMotion()
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
                _tags[nameof(syDeviceMotion)].Is = value;
            }
        }
        public Tag xDeviceLibraryVersion
        {
            get
            {
                return _tags[nameof(xDeviceLibraryVersion)];
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
                _tags[nameof(yInShuttleX1ServoOn)].Is = value;
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
                _tags[nameof(yInShuttleX1AlarmReset)].Is = value;
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
                _tags[nameof(yInShuttleX1Homing)].Is = value;
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
                _tags[nameof(yInShuttleX1PosMatch)].Is = value;
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
                _tags[nameof(yInShuttleX1Stop)].Is = value;
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
                _tags[nameof(yInShuttleX1EStop)].Is = value;
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
                _tags[nameof(yInShuttleX1SStop)].Is = value;
            }
        }
        public Tag xInShuttleX1PositiveLimit
        {
            get
            {
                return _tags[nameof(xInShuttleX1PositiveLimit)];
            }
        }
        public Tag xInShuttleX1Home
        {
            get
            {
                return _tags[nameof(xInShuttleX1Home)];
            }
        }
        public Tag xInShuttleX1NegativeLimit
        {
            get
            {
                return _tags[nameof(xInShuttleX1NegativeLimit)];
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
                _tags[nameof(yInShuttleX1InposEnable)].Is = value;
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
                _tags[nameof(yInShuttleX1InposRange)].Is = value;
            }
        }
        public Tag xInShuttleX1InposRange
        {
            get
            {
                return _tags[nameof(xInShuttleX1InposRange)];
            }
        }
        public Tag xInShuttleX1Inpos
        {
            get
            {
                return _tags[nameof(xInShuttleX1Inpos)];
            }
        }
        public Tag xInShuttleX1Busy
        {
            get
            {
                return _tags[nameof(xInShuttleX1Busy)];
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
                _tags[nameof(yInShuttleX1MoveMode)].Is = value;
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
                _tags[nameof(yInShuttleX1Move)].Is = value;
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
                _tags[nameof(yInShuttleX1VelocityMin)].Is = value;
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
                _tags[nameof(yInShuttleX1VelocityMax)].Is = value;
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
                _tags[nameof(yInShuttleX1Velocity)].Is = value;
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
                _tags[nameof(yInShuttleX1Accel)].Is = value;
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
                _tags[nameof(yInShuttleX1Decel)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeVelocity)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeVel1st)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeVel2nd)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeVel3rd)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeVel4th)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeAcc1st)].Is = value;
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
                _tags[nameof(yInShuttleX1HomeAcc2nd)].Is = value;
            }
        }
        public object yInShuttleX1HomeDec1st
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeDec1st)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeDec1st)].Is = value;
            }
        }
        public object yInShuttleX1HomeDec2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1HomeDec2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1HomeDec2nd)].Is = value;
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
        public Tag xInShuttleX1HomeDec1st
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeDec1st)];
            }
        }
        public Tag xInShuttleX1HomeDec2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1HomeDec2nd)];
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
                _tags[nameof(yInShuttleX1JogVelocity)].Is = value;
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
                _tags[nameof(yInShuttleX1JogAccel)].Is = value;
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
                _tags[nameof(yInShuttleX1JogDecel)].Is = value;
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
        public object yInShuttleX1JogVelocity2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogVelocity2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogVelocity2nd)].Is = value;
            }
        }
        public object yInShuttleX1JogAccel2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogAccel2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogAccel2nd)].Is = value;
            }
        }
        public object yInShuttleX1JogDecel2nd
        {
            get
            {
                return _tags[nameof(yInShuttleX1JogDecel2nd)];
            }
            set
            {
                _tags[nameof(yInShuttleX1JogDecel2nd)].Is = value;
            }
        }
        public Tag xInShuttleX1JogVelocity2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogVelocity2nd)];
            }
        }
        public Tag xInShuttleX1JogAccel2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogAccel2nd)];
            }
        }
        public Tag xInShuttleX1JogDecel2nd
        {
            get
            {
                return _tags[nameof(xInShuttleX1JogDecel2nd)];
            }
        }
        public object yMCRX2ServoOn
        {
            get
            {
                return _tags[nameof(yMCRX2ServoOn)];
            }
            set
            {
                _tags[nameof(yMCRX2ServoOn)].Is = value;
            }
        }
        public Tag xMCRX2ServoOn
        {
            get
            {
                return _tags[nameof(xMCRX2ServoOn)];
            }
        }
        public object yMCRX2AlarmReset
        {
            get
            {
                return _tags[nameof(yMCRX2AlarmReset)];
            }
            set
            {
                _tags[nameof(yMCRX2AlarmReset)].Is = value;
            }
        }
        public Tag xMCRX2AlarmStatus
        {
            get
            {
                return _tags[nameof(xMCRX2AlarmStatus)];
            }
        }
        public Tag xMCRX2AlarmCode
        {
            get
            {
                return _tags[nameof(xMCRX2AlarmCode)];
            }
        }
        public object yMCRX2Homing
        {
            get
            {
                return _tags[nameof(yMCRX2Homing)];
            }
            set
            {
                _tags[nameof(yMCRX2Homing)].Is = value;
            }
        }
        public Tag xMCRX2HomingStatus
        {
            get
            {
                return _tags[nameof(xMCRX2HomingStatus)];
            }
        }
        public object yMCRX2PosMatch
        {
            get
            {
                return _tags[nameof(yMCRX2PosMatch)];
            }
            set
            {
                _tags[nameof(yMCRX2PosMatch)].Is = value;
            }
        }
        public object yMCRX2Stop
        {
            get
            {
                return _tags[nameof(yMCRX2Stop)];
            }
            set
            {
                _tags[nameof(yMCRX2Stop)].Is = value;
            }
        }
        public Tag xMCRX2StopStatus
        {
            get
            {
                return _tags[nameof(xMCRX2StopStatus)];
            }
        }
        public object yMCRX2EStop
        {
            get
            {
                return _tags[nameof(yMCRX2EStop)];
            }
            set
            {
                _tags[nameof(yMCRX2EStop)].Is = value;
            }
        }
        public object yMCRX2SStop
        {
            get
            {
                return _tags[nameof(yMCRX2SStop)];
            }
            set
            {
                _tags[nameof(yMCRX2SStop)].Is = value;
            }
        }
        public Tag xMCRX2PositiveLimit
        {
            get
            {
                return _tags[nameof(xMCRX2PositiveLimit)];
            }
        }
        public Tag xMCRX2Home
        {
            get
            {
                return _tags[nameof(xMCRX2Home)];
            }
        }
        public Tag xMCRX2NegativeLimit
        {
            get
            {
                return _tags[nameof(xMCRX2NegativeLimit)];
            }
        }
        public object yMCRX2InposEnable
        {
            get
            {
                return _tags[nameof(yMCRX2InposEnable)];
            }
            set
            {
                _tags[nameof(yMCRX2InposEnable)].Is = value;
            }
        }
        public Tag xMCRX2InposEnable
        {
            get
            {
                return _tags[nameof(xMCRX2InposEnable)];
            }
        }
        public object yMCRX2InposRange
        {
            get
            {
                return _tags[nameof(yMCRX2InposRange)];
            }
            set
            {
                _tags[nameof(yMCRX2InposRange)].Is = value;
            }
        }
        public Tag xMCRX2InposRange
        {
            get
            {
                return _tags[nameof(xMCRX2InposRange)];
            }
        }
        public Tag xMCRX2Inpos
        {
            get
            {
                return _tags[nameof(xMCRX2Inpos)];
            }
        }
        public Tag xMCRX2Busy
        {
            get
            {
                return _tags[nameof(xMCRX2Busy)];
            }
        }
        public object yMCRX2MoveMode
        {
            get
            {
                return _tags[nameof(yMCRX2MoveMode)];
            }
            set
            {
                _tags[nameof(yMCRX2MoveMode)].Is = value;
            }
        }
        public Tag xMCRX2MoveMode
        {
            get
            {
                return _tags[nameof(xMCRX2MoveMode)];
            }
        }
        public object yMCRX2Move
        {
            get
            {
                return _tags[nameof(yMCRX2Move)];
            }
            set
            {
                _tags[nameof(yMCRX2Move)].Is = value;
            }
        }
        public Tag xMCRX2Command
        {
            get
            {
                return _tags[nameof(xMCRX2Command)];
            }
        }
        public Tag xMCRX2Actual
        {
            get
            {
                return _tags[nameof(xMCRX2Actual)];
            }
        }
        public object yMCRX2VelocityMin
        {
            get
            {
                return _tags[nameof(yMCRX2VelocityMin)];
            }
            set
            {
                _tags[nameof(yMCRX2VelocityMin)].Is = value;
            }
        }
        public object yMCRX2VelocityMax
        {
            get
            {
                return _tags[nameof(yMCRX2VelocityMax)];
            }
            set
            {
                _tags[nameof(yMCRX2VelocityMax)].Is = value;
            }
        }
        public Tag xMCRX2VelocityMin
        {
            get
            {
                return _tags[nameof(xMCRX2VelocityMin)];
            }
        }
        public Tag xMCRX2VelocityMax
        {
            get
            {
                return _tags[nameof(xMCRX2VelocityMax)];
            }
        }
        public object yMCRX2Velocity
        {
            get
            {
                return _tags[nameof(yMCRX2Velocity)];
            }
            set
            {
                _tags[nameof(yMCRX2Velocity)].Is = value;
            }
        }
        public Tag xMCRX2Velocity
        {
            get
            {
                return _tags[nameof(xMCRX2Velocity)];
            }
        }
        public object yMCRX2Accel
        {
            get
            {
                return _tags[nameof(yMCRX2Accel)];
            }
            set
            {
                _tags[nameof(yMCRX2Accel)].Is = value;
            }
        }
        public Tag xMCRX2Accel
        {
            get
            {
                return _tags[nameof(xMCRX2Accel)];
            }
        }
        public object yMCRX2Decel
        {
            get
            {
                return _tags[nameof(yMCRX2Decel)];
            }
            set
            {
                _tags[nameof(yMCRX2Decel)].Is = value;
            }
        }
        public Tag xMCRX2Decel
        {
            get
            {
                return _tags[nameof(xMCRX2Decel)];
            }
        }
        public object yMCRX2HomeVelocity
        {
            get
            {
                return _tags[nameof(yMCRX2HomeVelocity)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeVelocity)].Is = value;
            }
        }
        public object yMCRX2HomeVel1st
        {
            get
            {
                return _tags[nameof(yMCRX2HomeVel1st)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeVel1st)].Is = value;
            }
        }
        public object yMCRX2HomeVel2nd
        {
            get
            {
                return _tags[nameof(yMCRX2HomeVel2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeVel2nd)].Is = value;
            }
        }
        public object yMCRX2HomeVel3rd
        {
            get
            {
                return _tags[nameof(yMCRX2HomeVel3rd)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeVel3rd)].Is = value;
            }
        }
        public object yMCRX2HomeVel4th
        {
            get
            {
                return _tags[nameof(yMCRX2HomeVel4th)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeVel4th)].Is = value;
            }
        }
        public object yMCRX2HomeAcc1st
        {
            get
            {
                return _tags[nameof(yMCRX2HomeAcc1st)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeAcc1st)].Is = value;
            }
        }
        public object yMCRX2HomeAcc2nd
        {
            get
            {
                return _tags[nameof(yMCRX2HomeAcc2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeAcc2nd)].Is = value;
            }
        }
        public object yMCRX2HomeDec1st
        {
            get
            {
                return _tags[nameof(yMCRX2HomeDec1st)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeDec1st)].Is = value;
            }
        }
        public object yMCRX2HomeDec2nd
        {
            get
            {
                return _tags[nameof(yMCRX2HomeDec2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2HomeDec2nd)].Is = value;
            }
        }
        public Tag xMCRX2HomeVelocity
        {
            get
            {
                return _tags[nameof(xMCRX2HomeVelocity)];
            }
        }
        public Tag xMCRX2HomeVel1st
        {
            get
            {
                return _tags[nameof(xMCRX2HomeVel1st)];
            }
        }
        public Tag xMCRX2HomeVel2nd
        {
            get
            {
                return _tags[nameof(xMCRX2HomeVel2nd)];
            }
        }
        public Tag xMCRX2HomeVel3rd
        {
            get
            {
                return _tags[nameof(xMCRX2HomeVel3rd)];
            }
        }
        public Tag xMCRX2HomeVel4th
        {
            get
            {
                return _tags[nameof(xMCRX2HomeVel4th)];
            }
        }
        public Tag xMCRX2HomeAcc1st
        {
            get
            {
                return _tags[nameof(xMCRX2HomeAcc1st)];
            }
        }
        public Tag xMCRX2HomeAcc2nd
        {
            get
            {
                return _tags[nameof(xMCRX2HomeAcc2nd)];
            }
        }
        public Tag xMCRX2HomeDec1st
        {
            get
            {
                return _tags[nameof(xMCRX2HomeDec1st)];
            }
        }
        public Tag xMCRX2HomeDec2nd
        {
            get
            {
                return _tags[nameof(xMCRX2HomeDec2nd)];
            }
        }
        public object yMCRX2JogVelocity
        {
            get
            {
                return _tags[nameof(yMCRX2JogVelocity)];
            }
            set
            {
                _tags[nameof(yMCRX2JogVelocity)].Is = value;
            }
        }
        public object yMCRX2JogAccel
        {
            get
            {
                return _tags[nameof(yMCRX2JogAccel)];
            }
            set
            {
                _tags[nameof(yMCRX2JogAccel)].Is = value;
            }
        }
        public object yMCRX2JogDecel
        {
            get
            {
                return _tags[nameof(yMCRX2JogDecel)];
            }
            set
            {
                _tags[nameof(yMCRX2JogDecel)].Is = value;
            }
        }
        public Tag xMCRX2JogVelocity
        {
            get
            {
                return _tags[nameof(xMCRX2JogVelocity)];
            }
        }
        public Tag xMCRX2JogAccel
        {
            get
            {
                return _tags[nameof(xMCRX2JogAccel)];
            }
        }
        public Tag xMCRX2JogDecel
        {
            get
            {
                return _tags[nameof(xMCRX2JogDecel)];
            }
        }
        public object yMCRX2JogVelocity2nd
        {
            get
            {
                return _tags[nameof(yMCRX2JogVelocity2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2JogVelocity2nd)].Is = value;
            }
        }
        public object yMCRX2JogAccel2nd
        {
            get
            {
                return _tags[nameof(yMCRX2JogAccel2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2JogAccel2nd)].Is = value;
            }
        }
        public object yMCRX2JogDecel2nd
        {
            get
            {
                return _tags[nameof(yMCRX2JogDecel2nd)];
            }
            set
            {
                _tags[nameof(yMCRX2JogDecel2nd)].Is = value;
            }
        }
        public Tag xMCRX2JogVelocity2nd
        {
            get
            {
                return _tags[nameof(xMCRX2JogVelocity2nd)];
            }
        }
        public Tag xMCRX2JogAccel2nd
        {
            get
            {
                return _tags[nameof(xMCRX2JogAccel2nd)];
            }
        }
        public Tag xMCRX2JogDecel2nd
        {
            get
            {
                return _tags[nameof(xMCRX2JogDecel2nd)];
            }
        }
        public object yINSPECTIONX3ServoOn
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3ServoOn)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3ServoOn)].Is = value;
            }
        }
        public Tag xINSPECTIONX3ServoOn
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3ServoOn)];
            }
        }
        public object yINSPECTIONX3AlarmReset
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3AlarmReset)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3AlarmReset)].Is = value;
            }
        }
        public Tag xINSPECTIONX3AlarmStatus
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3AlarmStatus)];
            }
        }
        public Tag xINSPECTIONX3AlarmCode
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3AlarmCode)];
            }
        }
        public object yINSPECTIONX3Homing
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Homing)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Homing)].Is = value;
            }
        }
        public Tag xINSPECTIONX3HomingStatus
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomingStatus)];
            }
        }
        public object yINSPECTIONX3PosMatch
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3PosMatch)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3PosMatch)].Is = value;
            }
        }
        public object yINSPECTIONX3Stop
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Stop)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Stop)].Is = value;
            }
        }
        public Tag xINSPECTIONX3StopStatus
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3StopStatus)];
            }
        }
        public object yINSPECTIONX3EStop
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3EStop)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3EStop)].Is = value;
            }
        }
        public object yINSPECTIONX3SStop
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3SStop)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3SStop)].Is = value;
            }
        }
        public Tag xINSPECTIONX3PositiveLimit
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3PositiveLimit)];
            }
        }
        public Tag xINSPECTIONX3Home
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Home)];
            }
        }
        public Tag xINSPECTIONX3NegativeLimit
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3NegativeLimit)];
            }
        }
        public object yINSPECTIONX3InposEnable
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3InposEnable)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3InposEnable)].Is = value;
            }
        }
        public Tag xINSPECTIONX3InposEnable
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3InposEnable)];
            }
        }
        public object yINSPECTIONX3InposRange
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3InposRange)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3InposRange)].Is = value;
            }
        }
        public Tag xINSPECTIONX3InposRange
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3InposRange)];
            }
        }
        public Tag xINSPECTIONX3Inpos
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Inpos)];
            }
        }
        public Tag xINSPECTIONX3Busy
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Busy)];
            }
        }
        public object yINSPECTIONX3MoveMode
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3MoveMode)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3MoveMode)].Is = value;
            }
        }
        public Tag xINSPECTIONX3MoveMode
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3MoveMode)];
            }
        }
        public object yINSPECTIONX3Move
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Move)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Move)].Is = value;
            }
        }
        public Tag xINSPECTIONX3Command
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Command)];
            }
        }
        public Tag xINSPECTIONX3Actual
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Actual)];
            }
        }
        public object yINSPECTIONX3VelocityMin
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3VelocityMin)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3VelocityMin)].Is = value;
            }
        }
        public object yINSPECTIONX3VelocityMax
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3VelocityMax)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3VelocityMax)].Is = value;
            }
        }
        public Tag xINSPECTIONX3VelocityMin
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3VelocityMin)];
            }
        }
        public Tag xINSPECTIONX3VelocityMax
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3VelocityMax)];
            }
        }
        public object yINSPECTIONX3Velocity
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Velocity)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Velocity)].Is = value;
            }
        }
        public Tag xINSPECTIONX3Velocity
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Velocity)];
            }
        }
        public object yINSPECTIONX3Accel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Accel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Accel)].Is = value;
            }
        }
        public Tag xINSPECTIONX3Accel
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Accel)];
            }
        }
        public object yINSPECTIONX3Decel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3Decel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3Decel)].Is = value;
            }
        }
        public Tag xINSPECTIONX3Decel
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3Decel)];
            }
        }
        public object yINSPECTIONX3HomeVelocity
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeVelocity)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeVelocity)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeVel1st
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeVel1st)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeVel1st)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeVel2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeVel2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeVel2nd)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeVel3rd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeVel3rd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeVel3rd)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeVel4th
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeVel4th)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeVel4th)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeAcc1st
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeAcc1st)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeAcc1st)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeAcc2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeAcc2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeAcc2nd)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeDec1st
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeDec1st)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeDec1st)].Is = value;
            }
        }
        public object yINSPECTIONX3HomeDec2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3HomeDec2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3HomeDec2nd)].Is = value;
            }
        }
        public Tag xINSPECTIONX3HomeVelocity
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeVelocity)];
            }
        }
        public Tag xINSPECTIONX3HomeVel1st
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeVel1st)];
            }
        }
        public Tag xINSPECTIONX3HomeVel2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeVel2nd)];
            }
        }
        public Tag xINSPECTIONX3HomeVel3rd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeVel3rd)];
            }
        }
        public Tag xINSPECTIONX3HomeVel4th
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeVel4th)];
            }
        }
        public Tag xINSPECTIONX3HomeAcc1st
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeAcc1st)];
            }
        }
        public Tag xINSPECTIONX3HomeAcc2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeAcc2nd)];
            }
        }
        public Tag xINSPECTIONX3HomeDec1st
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeDec1st)];
            }
        }
        public Tag xINSPECTIONX3HomeDec2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3HomeDec2nd)];
            }
        }
        public object yINSPECTIONX3JogVelocity
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogVelocity)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogVelocity)].Is = value;
            }
        }
        public object yINSPECTIONX3JogAccel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogAccel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogAccel)].Is = value;
            }
        }
        public object yINSPECTIONX3JogDecel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogDecel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogDecel)].Is = value;
            }
        }
        public Tag xINSPECTIONX3JogVelocity
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogVelocity)];
            }
        }
        public Tag xINSPECTIONX3JogAccel
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogAccel)];
            }
        }
        public Tag xINSPECTIONX3JogDecel
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogDecel)];
            }
        }
        public object yINSPECTIONX3JogVelocity2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogVelocity2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogVelocity2nd)].Is = value;
            }
        }
        public object yINSPECTIONX3JogAccel2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogAccel2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogAccel2nd)].Is = value;
            }
        }
        public object yINSPECTIONX3JogDecel2nd
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3JogDecel2nd)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3JogDecel2nd)].Is = value;
            }
        }
        public Tag xINSPECTIONX3JogVelocity2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogVelocity2nd)];
            }
        }
        public Tag xINSPECTIONX3JogAccel2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogAccel2nd)];
            }
        }
        public Tag xINSPECTIONX3JogDecel2nd
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3JogDecel2nd)];
            }
        }
        public object yOutShuttleX10ServoOn
        {
            get
            {
                return _tags[nameof(yOutShuttleX10ServoOn)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10ServoOn)].Is = value;
            }
        }
        public Tag xOutShuttleX10ServoOn
        {
            get
            {
                return _tags[nameof(xOutShuttleX10ServoOn)];
            }
        }
        public object yOutShuttleX10AlarmReset
        {
            get
            {
                return _tags[nameof(yOutShuttleX10AlarmReset)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10AlarmReset)].Is = value;
            }
        }
        public Tag xOutShuttleX10AlarmStatus
        {
            get
            {
                return _tags[nameof(xOutShuttleX10AlarmStatus)];
            }
        }
        public Tag xOutShuttleX10AlarmCode
        {
            get
            {
                return _tags[nameof(xOutShuttleX10AlarmCode)];
            }
        }
        public object yOutShuttleX10Homing
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Homing)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Homing)].Is = value;
            }
        }
        public Tag xOutShuttleX10HomingStatus
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomingStatus)];
            }
        }
        public object yOutShuttleX10PosMatch
        {
            get
            {
                return _tags[nameof(yOutShuttleX10PosMatch)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10PosMatch)].Is = value;
            }
        }
        public object yOutShuttleX10Stop
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Stop)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Stop)].Is = value;
            }
        }
        public Tag xOutShuttleX10StopStatus
        {
            get
            {
                return _tags[nameof(xOutShuttleX10StopStatus)];
            }
        }
        public object yOutShuttleX10EStop
        {
            get
            {
                return _tags[nameof(yOutShuttleX10EStop)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10EStop)].Is = value;
            }
        }
        public object yOutShuttleX10SStop
        {
            get
            {
                return _tags[nameof(yOutShuttleX10SStop)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10SStop)].Is = value;
            }
        }
        public Tag xOutShuttleX10PositiveLimit
        {
            get
            {
                return _tags[nameof(xOutShuttleX10PositiveLimit)];
            }
        }
        public Tag xOutShuttleX10Home
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Home)];
            }
        }
        public Tag xOutShuttleX10NegativeLimit
        {
            get
            {
                return _tags[nameof(xOutShuttleX10NegativeLimit)];
            }
        }
        public object yOutShuttleX10InposEnable
        {
            get
            {
                return _tags[nameof(yOutShuttleX10InposEnable)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10InposEnable)].Is = value;
            }
        }
        public Tag xOutShuttleX10InposEnable
        {
            get
            {
                return _tags[nameof(xOutShuttleX10InposEnable)];
            }
        }
        public object yOutShuttleX10InposRange
        {
            get
            {
                return _tags[nameof(yOutShuttleX10InposRange)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10InposRange)].Is = value;
            }
        }
        public Tag xOutShuttleX10InposRange
        {
            get
            {
                return _tags[nameof(xOutShuttleX10InposRange)];
            }
        }
        public Tag xOutShuttleX10Inpos
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Inpos)];
            }
        }
        public Tag xOutShuttleX10Busy
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Busy)];
            }
        }
        public object yOutShuttleX10MoveMode
        {
            get
            {
                return _tags[nameof(yOutShuttleX10MoveMode)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10MoveMode)].Is = value;
            }
        }
        public Tag xOutShuttleX10MoveMode
        {
            get
            {
                return _tags[nameof(xOutShuttleX10MoveMode)];
            }
        }
        public object yOutShuttleX10Move
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Move)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Move)].Is = value;
            }
        }
        public Tag xOutShuttleX10Command
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Command)];
            }
        }
        public Tag xOutShuttleX10Actual
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Actual)];
            }
        }
        public object yOutShuttleX10VelocityMin
        {
            get
            {
                return _tags[nameof(yOutShuttleX10VelocityMin)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10VelocityMin)].Is = value;
            }
        }
        public object yOutShuttleX10VelocityMax
        {
            get
            {
                return _tags[nameof(yOutShuttleX10VelocityMax)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10VelocityMax)].Is = value;
            }
        }
        public Tag xOutShuttleX10VelocityMin
        {
            get
            {
                return _tags[nameof(xOutShuttleX10VelocityMin)];
            }
        }
        public Tag xOutShuttleX10VelocityMax
        {
            get
            {
                return _tags[nameof(xOutShuttleX10VelocityMax)];
            }
        }
        public object yOutShuttleX10Velocity
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Velocity)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Velocity)].Is = value;
            }
        }
        public Tag xOutShuttleX10Velocity
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Velocity)];
            }
        }
        public object yOutShuttleX10Accel
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Accel)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Accel)].Is = value;
            }
        }
        public Tag xOutShuttleX10Accel
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Accel)];
            }
        }
        public object yOutShuttleX10Decel
        {
            get
            {
                return _tags[nameof(yOutShuttleX10Decel)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10Decel)].Is = value;
            }
        }
        public Tag xOutShuttleX10Decel
        {
            get
            {
                return _tags[nameof(xOutShuttleX10Decel)];
            }
        }
        public object yOutShuttleX10HomeVelocity
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeVelocity)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeVelocity)].Is = value;
            }
        }
        public object yOutShuttleX10HomeVel1st
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeVel1st)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeVel1st)].Is = value;
            }
        }
        public object yOutShuttleX10HomeVel2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeVel2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeVel2nd)].Is = value;
            }
        }
        public object yOutShuttleX10HomeVel3rd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeVel3rd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeVel3rd)].Is = value;
            }
        }
        public object yOutShuttleX10HomeVel4th
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeVel4th)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeVel4th)].Is = value;
            }
        }
        public object yOutShuttleX10HomeAcc1st
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeAcc1st)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeAcc1st)].Is = value;
            }
        }
        public object yOutShuttleX10HomeAcc2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeAcc2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeAcc2nd)].Is = value;
            }
        }
        public object yOutShuttleX10HomeDec1st
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeDec1st)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeDec1st)].Is = value;
            }
        }
        public object yOutShuttleX10HomeDec2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10HomeDec2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10HomeDec2nd)].Is = value;
            }
        }
        public Tag xOutShuttleX10HomeVelocity
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeVelocity)];
            }
        }
        public Tag xOutShuttleX10HomeVel1st
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeVel1st)];
            }
        }
        public Tag xOutShuttleX10HomeVel2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeVel2nd)];
            }
        }
        public Tag xOutShuttleX10HomeVel3rd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeVel3rd)];
            }
        }
        public Tag xOutShuttleX10HomeVel4th
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeVel4th)];
            }
        }
        public Tag xOutShuttleX10HomeAcc1st
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeAcc1st)];
            }
        }
        public Tag xOutShuttleX10HomeAcc2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeAcc2nd)];
            }
        }
        public Tag xOutShuttleX10HomeDec1st
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeDec1st)];
            }
        }
        public Tag xOutShuttleX10HomeDec2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10HomeDec2nd)];
            }
        }
        public object yOutShuttleX10JogVelocity
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogVelocity)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogVelocity)].Is = value;
            }
        }
        public object yOutShuttleX10JogAccel
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogAccel)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogAccel)].Is = value;
            }
        }
        public object yOutShuttleX10JogDecel
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogDecel)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogDecel)].Is = value;
            }
        }
        public Tag xOutShuttleX10JogVelocity
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogVelocity)];
            }
        }
        public Tag xOutShuttleX10JogAccel
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogAccel)];
            }
        }
        public Tag xOutShuttleX10JogDecel
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogDecel)];
            }
        }
        public object yOutShuttleX10JogVelocity2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogVelocity2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogVelocity2nd)].Is = value;
            }
        }
        public object yOutShuttleX10JogAccel2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogAccel2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogAccel2nd)].Is = value;
            }
        }
        public object yOutShuttleX10JogDecel2nd
        {
            get
            {
                return _tags[nameof(yOutShuttleX10JogDecel2nd)];
            }
            set
            {
                _tags[nameof(yOutShuttleX10JogDecel2nd)].Is = value;
            }
        }
        public Tag xOutShuttleX10JogVelocity2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogVelocity2nd)];
            }
        }
        public Tag xOutShuttleX10JogAccel2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogAccel2nd)];
            }
        }
        public Tag xOutShuttleX10JogDecel2nd
        {
            get
            {
                return _tags[nameof(xOutShuttleX10JogDecel2nd)];
            }
        }
        public Tag xINSPECTIONX3TriggerTime
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerTime)];
            }
        }
        public Tag xINSPECTIONX3TriggerLevel
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerLevel)];
            }
        }
        public Tag xINSPECTIONX3TriggerSelect
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerSelect)];
            }
        }
        public Tag xINSPECTIONX3TriggerInterrupt
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerInterrupt)];
            }
        }
        public Tag xINSPECTIONX3TriggerStartPos
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerStartPos)];
            }
        }
        public Tag xINSPECTIONX3TriggerEndPos
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerEndPos)];
            }
        }
        public Tag xINSPECTIONX3TriggerPeriodPos
        {
            get
            {
                return _tags[nameof(xINSPECTIONX3TriggerPeriodPos)];
            }
        }
        public object yINSPECTIONX3TriggerTime
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerTime)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerTime)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerLevel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerLevel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerLevel)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerSelect
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerSelect)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerSelect)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerInterrupt
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerInterrupt)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerInterrupt)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerStartPos
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerStartPos)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerStartPos)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerEndPos
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerEndPos)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerEndPos)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerPeriodPos
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerPeriodPos)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerPeriodPos)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerSetTimeLevel
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerSetTimeLevel)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerSetTimeLevel)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerSetBlock
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerSetBlock)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerSetBlock)].Is = value;
            }
        }
        public object yINSPECTIONX3TriggerSetReset
        {
            get
            {
                return _tags[nameof(yINSPECTIONX3TriggerSetReset)];
            }
            set
            {
                _tags[nameof(yINSPECTIONX3TriggerSetReset)].Is = value;
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
                return true;
            }
        }
        public bool SetyInShuttleX1ServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yInShuttleX1ServoOn = Common;

            bool isActionBreak = false;
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
                return true;
            }
        }
        public bool SetyMCRX2ServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yMCRX2ServoOn = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xMCRX2ServoOn.Is == null)
                    {
                        continue;
                    }

                    if (xMCRX2ServoOn.Is.ToString() == Common.ToString())
                    {
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
                return true;
            }
        }
        public bool SetyINSPECTIONX3ServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yINSPECTIONX3ServoOn = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xINSPECTIONX3ServoOn.Is == null)
                    {
                        continue;
                    }

                    if (xINSPECTIONX3ServoOn.Is.ToString() == Common.ToString())
                    {
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
                return true;
            }
        }
        public bool SetyOutShuttleX10ServoOn(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yOutShuttleX10ServoOn = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xOutShuttleX10ServoOn.Is == null)
                    {
                        continue;
                    }

                    if (xOutShuttleX10ServoOn.Is.ToString() == Common.ToString())
                    {
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
                return true;
            }
        }
        #endregion Methods
    }
}
