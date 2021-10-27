using System;
using System.Collections.Generic;

namespace EmWorks.Lib.DriverCore
{
    [Serializable]
    public abstract class MotionAxisConfigModelBase
    {
        #region Properties

        public abstract string AxisName { get; set; }
        public abstract int AxisNumber { get; set; }
        public abstract double HomeOffset { get; set; }
        public abstract double MaxVel { get; set; }
        public abstract double MinVel { get; set; }
        public abstract double CmdPos { get; set; }
        public abstract double Accel { get; set; }
        public abstract double Decel { get; set; }
        public abstract double Velocity { get; set; }

        #endregion Properties
    }
}