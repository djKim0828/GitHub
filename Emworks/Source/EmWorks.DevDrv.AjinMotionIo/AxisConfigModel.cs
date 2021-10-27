using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.AjinMotionIo
{
    [Serializable]
    public class AxisConfigModel : MotionAxisConfigModelBase
    {
        #region Constructors

        public AxisConfigModel()
        {
        }

        public AxisConfigModel(string axisName, int axisNum)
        {
            this.AxisName = axisName;
            this.AxisNumber = axisNum;
        }

        #endregion Constructors

        #region Properties

        public override string AxisName { get; set; } = string.Empty;
        public override int AxisNumber { get; set; } = int.MinValue;
        public override double Velocity { get; set; } = double.MaxValue;
        public override double Accel { get; set; } = double.MaxValue;
        public override double Decel { get; set; } = double.MaxValue;
        public override double HomeOffset { get; set; } = double.MaxValue;
        public override double CmdPos { get; set; } = double.MaxValue;
        public override double MinVel { get; set; } = double.MaxValue;
        public override double MaxVel { get; set; } = double.MaxValue;

        public double HomeVelocity1 { get; set; } = double.MaxValue;
        public double HomeVelocity2 { get; set; } = double.MaxValue;
        public double HomeVelocity3 { get; set; } = double.MaxValue;
        public double HomeVelocity4 { get; set; } = double.MaxValue;
        public double HomeAccel1 { get; set; } = double.MaxValue;
        public double HomeAccel2 { get; set; } = double.MaxValue;

        public double SwLimitMin { get; set; } = double.MinValue;
        public double SwLimitMax { get; set; } = double.MaxValue;

        public bool? UseInPositionSignal { get; set; }
        public bool? UseAlarmSignal { get; set; }

        public uint HomeSignal { get; set; } = uint.MaxValue;
        public double HomeClearTime { get; set; } = double.MaxValue;
        public int HomeDir { get; set; } = int.MinValue;
        public uint Zphas { get; set; } = uint.MaxValue;

        public Dictionary<string, PositionModel> PositionDic { get; set; }

        #endregion Properties
    }
}