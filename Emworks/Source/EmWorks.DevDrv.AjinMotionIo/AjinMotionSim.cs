using EmWorks.Lib.Common;
using System;
using System.IO;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public class AjinMotionSim
    {
        #region Fields

        private EmAjinMotion _motion;

        #endregion Fields

        #region Constructors

        internal AjinMotionSim(EmAjinMotion motion)
        {
            _motion = motion;
        }

        #endregion Constructors

        #region Methods

        internal bool AlarmReset(AxisPropertyModel axis)
        {
            axis.AlarmCode.Clear();
            axis.Alarm = false;

            return true;
        }

        internal bool CheckAxisDefaultVelue(AxisConfigModel axis)
        {
            return true;
        }

        internal bool CheckAxisHommingVelue(AxisConfigModel axis)
        {
            return true;
        }

        internal bool GetActualPosition(AxisPropertyModel axis)
        {
            return true;
        }

        internal bool GetAlarmCode(int axisNum)
        {
            _motion.AxisNumber[axisNum].AlarmCode.Add("Motion Simulation Alarm Code");

            return true;
        }

        internal bool GetAxisCount(ref int count)
        {
            count = _motion.AxisConfigName.Count;

            return true;
        }

        internal EmModuleInfo GetAxisInfo(int moduleNo)
        {
            EmModuleInfo module = new EmModuleInfo();
            module.Number = moduleNo;
            module.BoardNumber = moduleNo;
            module.Index = moduleNo;
            module.Id = (uint)moduleNo;
            module.IsReady = true;
            return module;
        }

        internal bool GetBusySignal(AxisPropertyModel axis)
        {
            if (axis.ActualPosition != axis.CommandPosisiton)
            {
                axis.Busy = false;
            }
            else
            {
                axis.Busy = true;
            }

            return true;
        }

        internal bool GetCmdPosition(AxisPropertyModel axis)
        {
            return true;
        }

        internal string GetCurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }

        internal bool GetExsitMotionModule()
        {
            return true;
        }

        internal bool GetExtenalLibVersion()
        {
            _motion._version = "Simulation";

            return true;
        }

        internal bool GetHomeSignal(AxisPropertyModel axis)
        {
            if (axis.ActualPosition == 0)
            {
                axis.Home = true;
            }
            else
            {
                axis.Home = false;
            }

            return true;
        }

        internal bool GetHommingResult(int axisNo, ref uint homeResult)
        {
            return true;
        }

        internal bool GetInPositionSignal(AxisPropertyModel axis)
        {
            if (axis.ActualPosition == axis.CommandPosisiton)
            {
                axis.InPosition = false;
            }
            else
            {
                axis.InPosition = true;
            }
            return true;
        }

        internal bool GetIsOpen()
        {
            return _motion.IsOpen;
        }

        internal bool GetLimitSignal(AxisPropertyModel axis)
        {
            if (axis.ActualPosition == _motion.AxisConfigName[axis.AxisName].SwLimitMax)
            {
                axis.PositiveLimit = true;
                axis.NegativeLimit = false;
            }
            else if (axis.ActualPosition == _motion.AxisConfigName[axis.AxisName].SwLimitMin)
            {
                axis.NegativeLimit = true;
                axis.PositiveLimit = false;
            }
            else
            {
                axis.NegativeLimit = false;
                axis.PositiveLimit = false;
            }

            return true;
        }

        internal bool GetMoveMode(int axisNo)
        {
            return true;
        }

        internal bool GetServoAlarmSignal(AxisPropertyModel axis)
        {
            return true;
        }

        internal bool GetServoOn(AxisPropertyModel axis)
        {
            return true;
        }

        internal bool Homming(AxisPropertyModel axis)
        {
            axis.CommandPosisiton = _motion.AxisConfigName[axis.AxisName].SwLimitMin;
            double drection;
            if (axis.ActualPosition > 0)
            {
                drection = -1.0;
            }//else

            if (axis.ActualPosition < 0)
            {
                drection = -1.0;
            }
            else
            {
                axis.CommandPosisiton = 0;
                axis.ActualPosition = 0;
                axis.Orign = true;

                return true;
            }

            while (true)
            {
                axis.ActualPosition = axis.ActualPosition - 0.001 * drection;
                if (axis.ActualPosition < 0)
                {
                    axis.ActualPosition = 0;
                    break;
                }//else
            }

            axis.CommandPosisiton = 0;
            axis.ActualPosition = 0;
            axis.Orign = true;

            return true;
        }

        internal bool LoadMot()
        {
            string filePath = GetCurrentDirectory() + @"\MotionDefault.mot";

            if (File.Exists(filePath) != true)
            {
                Logger.Error("LoadMot - Wrong FilePath", filePath);
            }//else

            for (int axisno = 0; axisno < _motion.AxisCount; axisno++)
            {
                _motion.AxisNumber[axisno].CommandPosisiton = axisno * 10 + 1;
                _motion.AxisNumber[axisno].Velocity = axisno * 10 + 2;
                _motion.AxisNumber[axisno].Accel = axisno * 10 + 3;
                _motion.AxisNumber[axisno].Decel = axisno * 10 + 4;
            }

            return true;
        }

        internal bool MoveModeSelect(AxisPropertyModel axis, EmAjinMotion.AbsRelMode mode)
        {
            axis.MoveMode = mode;

            return true;
        }

        internal bool MoveStart(AxisPropertyModel axis, string positionString, bool skipChangeMoveMode)
        {
            axis.MoveMode = (EmAjinMotion.AbsRelMode)Enum.Parse(typeof(EmAjinMotion.AbsRelMode), _motion.AxisConfigName[axis.AxisName].PositionDic[positionString].MoveMode);

            if (axis.MoveMode == EmAjinMotion.AbsRelMode.Absolute)
            {
                axis.CommandPosisiton = _motion.AxisConfigName[axis.AxisName].PositionDic[positionString].Position;
            }
            else
            {
                axis.CommandPosisiton = axis.CommandPosisiton + _motion.AxisConfigName[axis.AxisName].PositionDic[positionString].Position;
            }

            while (true)
            {
                if (axis.ActualPosition == axis.CommandPosisiton)
                {
                    break;
                }//else

                if (axis.ActualPosition > axis.CommandPosisiton)
                {
                    axis.ActualPosition = axis.ActualPosition - 0.001;
                }
                else
                {
                    axis.ActualPosition = axis.ActualPosition + 0.001;
                }
            }
            return true;
        }

        internal bool OpenLib()
        {
            return true;
        }

        internal bool ServoOn(AxisPropertyModel axis, bool servoOn)
        {
            axis.ServoOn = servoOn;

            return true;
        }

        internal bool SetAxisHommingVelue(AxisConfigModel axis)
        {
            return true;
        }

        internal bool Stop(AxisPropertyModel Axis, string stopMode)
        {
            Axis.CommandPosisiton = Axis.ActualPosition;

            return true;
        }

        #endregion Methods
    }
}