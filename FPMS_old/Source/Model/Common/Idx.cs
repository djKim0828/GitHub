using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMS.E2105_FS111_121
{
    public class Idx
    {
        public const string DefaultPositionName = "DefaultPosition";

        public class ChennelID
        {
            public const string Channel1 = "A";
            public const string Channel2 = "B";
        }

        public class AlarmEventIndex
        {

            public const int Id = 0;
            public const int Name = 1;
            public const int Description = 2;
            public const int MaxLength = 3;

        }

        public class AlarmTagIndex
        {

            public const string Name = "Name";
            public const string Type = "Type";
            public const string Description = "Description";
            public const string ImagePath = "ImagePath";
            public const string OccurrenceTime = "OccurrenceTime";

        }

        public class AlarmType
        {

            public const string Heavy = "2";
            public const string Light = "1";
            public const string Normal = "0";

        }

        public class IsVibibleChange
        {
            public const int NoChange = 0;
            public const int Change = 1;
        }

        public class InitResult
        {
            public const int False = -1;
            public const int True = 1;
        }

        public class UserLevel
        {
            public const string Master = "0";
            public const string Engineer = "1";
            public const string Operator = "2";
            public const string Unknown = "3";
        }

        public class RunMode
        {
            public const string Real = "0";
            public const string Dry = "1";
            public const string Simulation = "2";
        }

        public class FunctionResult
        {
            public const int False = 0;
            public const int True = 1;
        }

        public class McrStatusOptionIndex
        {
            public const int McrName = 0;
            public const int McrIp = 1;
            public const int McrPort = 2;
        }

        public class McrIndex
        {
            public const int Mcr1 = 0;
            public const int Mcr2 = 1;
        }


        public class MotionInfo
        {
            public const string Name = "Name";
            public const string MoveType = "MoveType";
            public const string AxisName = "AxisName";
            public const string Position = "Position";
            public const string Speed = "Speed";
            public const string Accel = "Accel";
            public const string Decel = "Decel";
            public const string TagCategoryFirstName = "Position";
            public const string TagAxisFirstName = "Axis";
        }

        public class MotionAxisNo
        {
            public const int X = 0;
            public const int Y = 1;
            public const int Z = 2;
            public const int V = 3;
            public const int H = 4;
            public const int R = 5;            
        }

        public class MotionAxisName
        {
            public const string X1 = "SHUTTLE_X1";
            public const string Y2 = "SHUTTLE_Y2";
            public const string X3 = "INSPECTOR_X3";
            public const string Z4 = "CAMERA_Z4";
            public const string Z5 = "SR5000_Z5";
            public const string X6 = "";
            public const string Y7 = "";
            public const string C8 = "";
            public const string Z9 = "";
        }

        public class MotionMoveType
        {
            public const string AbsoluteType = "0";
            public const string RelativeType = "1";
        }

        public class MotionAction
        {
            public const string sxDeviceMotion = nameof(sxDeviceMotion);
            public const string SignalPositiveLimit = nameof(SignalPositiveLimit);
            public const string SignalHome = nameof(SignalHome);
            public const string SignalNegativeLimit = nameof(SignalNegativeLimit);
            public const string SignalInpos = nameof(SignalInpos);
            public const string SignalBusy = nameof(SignalBusy);
            public const string ServoOn = nameof(ServoOn);
            public const string AlarmReset = nameof(AlarmReset);
            public const string AlarmStatus = nameof(AlarmStatus);
            public const string AlarmCode = nameof(AlarmCode);
            public const string Homing = nameof(Homing);
            public const string HomingStatus = nameof(HomingStatus);
            public const string PosMatch = nameof(PosMatch);
            public const string Stop = nameof(Stop);
            public const string StopStatus = nameof(StopStatus);
            public const string EStop = nameof(EStop);
            public const string SStop = nameof(SStop);
            public const string InposEnable = nameof(InposEnable);
            public const string InposRange = nameof(InposRange);
            public const string MoveMode = nameof(MoveMode);
            public const string Move = nameof(Move);
            public const string Command = nameof(Command);
            public const string Actual = nameof(Actual);
            public const string VelocityMin = nameof(VelocityMin);
            public const string VelocityMax = nameof(VelocityMax);
            public const string Velocity = nameof(Velocity);
            public const string Accel = nameof(Accel);
            public const string Decel = nameof(Decel);
            public const string HomeVelocity = nameof(HomeVelocity);
            public const string HomeVel1st = nameof(HomeVel1st);
            public const string HomeVel2nd = nameof(HomeVel2nd);
            public const string HomeVel3rd = nameof(HomeVel3rd);
            public const string HomeVel4th = nameof(HomeVel4th);
            public const string HomeAcc1st = nameof(HomeAcc1st);
            public const string HomeAcc2nd = nameof(HomeAcc2nd);            
            public const string JogVelocity = nameof(JogVelocity);
            public const string JogAccel = nameof(JogAccel);
            public const string JogDecel = nameof(JogDecel);
            public const string JogVelocity2nd = nameof(JogVelocity2nd);
            public const string JogAccel2nd = nameof(JogAccel2nd);
            public const string JogDecel2nd = nameof(JogDecel2nd);
            public const string TriggerTime = nameof(TriggerTime);
            public const string TriggerStartPos = nameof(TriggerStartPos);
            public const string TriggerEndPos = nameof(TriggerEndPos);
            public const string TriggerPeriodPos = nameof(TriggerPeriodPos);
            public const string TriggerSetTimeLevel = nameof(TriggerSetTimeLevel);
            public const string TriggerSetBlock = nameof(TriggerSetBlock);
            public const string TriggerSetReset = nameof(TriggerSetReset);
            public const string HomeOffset = nameof(HomeOffset);
        }

        public class MotionProperfy
        {
            public const string HomeOffset = nameof(HomeOffset);
            public const string HomeVel1st = nameof(HomeVel1st);
            public const string HomeVel2nd = nameof(HomeVel2nd);
            public const string HomeVel3rd = nameof(HomeVel3rd);
            public const string HomeVel4th = nameof(HomeVel4th);
            public const string HomeAcc1st = nameof(HomeAcc1st);
            public const string HomeAcc2nd = nameof(HomeAcc2nd);
            public const string JogAcc = nameof(JogAcc);
            public const string JogDec = nameof(JogDec);
            public const string JogFastVel = nameof(JogFastVel);
            public const string JogSlowVel = nameof(JogSlowVel);
            public const string NegativeLimit = nameof(NegativeLimit);
            public const string PositiveLimit = nameof(PositiveLimit);
            public const string ValocityMax = nameof(ValocityMax);
            public const string ValocityMin = nameof(ValocityMin);

            public const string RelativeDistance = nameof(RelativeDistance);

            public const string CommandPosisiton = nameof(CommandPosisiton);
            public const string Velocity = nameof(Velocity);
            public const string Accel = nameof(Accel);
            public const string Decel = nameof(Decel);

        }
        public class DeviceOpen
        {
            public const string Close = "0";
            public const string Open = "1";
        }

        public class DigitalValue
        {
            public const int UnKnow = -1;
            public const int Disable = 0;
            public const int Enable = 1;
        }

        public class TowerLampStatus
        {
            public const int None = 0;
            public const int Normal = 1;
            public const int Alarm = 2;
        }

        public class TimeOut
        {
            public const int HomeSearch = 60000;
        }

        public class MotionHomeStatus
        {
            public const string Searching = "0";
            public const string Complate = "1";
            public const string None = "255";
        }

        public class NachiRobotStatusOptionIndex
        {
            public const int Ip = 0;
            public const int Port = 1;
            public const int CCLinkChannel = 2;
        }

        public class OpenClose
        {

            public const int Error = -1;
            public const int Close = 0;
            public const int Open = 1;

        }

        public class StageIndex
        {
            public const int Shuttle = 0;
            public const int Inspection = 1;
            public const int StageCount = 2;
        }

        public class StageStatus
        {
            public const int Idle = 0;
            public const int Wait = 1;
            public const int Busy = 2;
            public const int Complate = 3;
        }

        public class StageStap
        {
            public const int None = 0; // 최초
            //public const int Step2 = 1;
            //public const int Step3 = 2;
            //public const int Step4 = 3;
        }


    }
}
