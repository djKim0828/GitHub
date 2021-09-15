using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public class ExeCmd
    {
        public const string Version = "Version";
        public const string ExtenalLibVersion = "ExtenalLibVersion";
        public const string SignalPositiveLimit = "SignalPositiveLimit";
        public const string SignalHome = "SignalHome";
        public const string SignalNegativeLimit = "SignalNegativeLimit";
        public const string SignalInpos = "SignalInpos";
        public const string SignalBusy = "SignalBusy";
        public const string ServoOn = "ServoOn";
        public const string AlarmReset = "AlarmReset";
        public const string Alarm = "Alarm";
        public const string AlarmCode = "AlarmCode";
        public const string Homing = "Homing";
        public const string PositionMatch = "PositionMatch";
        public const string Stop = "Stop";
        public const string EStop = "EStop";
        public const string SStop = "SStop";
        public const string InpositionEnable = "InpositionEnable";
        public const string InposRange = "InposRange";
        public const string MoveModeSelect = "MoveModeSelect";
        public const string MoveMode = "MoveMode";
        public const string MoveStart = "MoveStart";
        public const string CommandPosisiton = "CommandPosisiton";
        public const string ActualPosition = "ActualPosition";
        public const string MinVel = "MinVel";
        public const string MaxVel = "MaxVel";
        public const string Velocity = "Velocity";
        public const string Accel = "Accel";
        public const string Decel = "Decel";
        public const string HomingVel = "HomingVel";
        public const string HomingVel1st = "HomingVel1st";
        public const string HomingVel2nd = "HomingVel2nd";
        public const string HomingVel3rd = "HomingVel3rd";
        public const string HomingVel4th = "HomingVel4th";
        public const string HomingAcc1st = "HomingAcc1st";
        public const string HomingAcc2nd = "HomingAcc2nd";
        public const string JogVel = "JogVel";
        public const string JogAccel = "JogAccel";
        public const string JogDecel = "JogDecel";
        public const string JogVelocity = "JogVelocity";
        public const string TriggerTime = "TriggerTime";
        public const string TriggerStartPos = "TriggerStartPos";
        public const string TriggerEndPos = "TriggerEndPos";
        public const string TriggerPeriodPos = "TriggerPeriodPos";
        public const string TriggerSetTimeLevel = "TriggerSetTimeLevel";
        public const string TriggerSetBlock = "TriggerSetBlock";
        public const string TriggerSetReset = "TriggerSetReset";
    }

    public class OpenClose
    {
        #region Fields

        public const int Error = -1;
        public const int Close= 0;
        public const int Open = 1;

        #endregion Fields
    }        

    public class AiUnitType
    {
        public const int Digital = 0;
        public const int kPa = 2;
        public const int mPa = 3;
        public const int Voltage = 1;
    }

    public class FunctionResult
    {
        public const int False = 0;
        public const int True = 1;
    }

    public class RelatedTag
    {        
        public string Name = "0";
        public string Type = "1";
        public string DefaultValue = "2";
        public string ReActValue = "3";
        public string DelayTime = "4";
    }

    public class CommandTagType
    {
        #region Fields

        public const string Defualt = "0";
        public const string Invert = "1";
        public const string ReActValueOut = "2";
        public const string AlwaysZero = "3";
        public const string AlwaysOne = "4";

        #endregion Fields
    }

    public class MotionMoveType
    {
        public const string AbsoluteType = "0";
        public const string RelativeType = "1";
    }
}
