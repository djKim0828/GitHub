using EmWorks.Lib.Common;

namespace EmWorks.Lib.DriverCore
{
    public abstract class MotionBase : DriverBase
    {
        #region Constructors

        public MotionBase(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Properties

        public abstract int AxisCount { get; protected set; }
        public abstract string ExtenalLibVersion { get; protected set; }
        public abstract string Version { get; protected set; }

        #endregion Properties

        #region Methods

        public abstract bool AlarmReset(int axisNo);

        public abstract bool Homming(int axisNo);

        public abstract bool MoveStart(int axisNo, string position);

        public abstract bool ServoOn(int axisNo, bool ServoOn = false);

        public abstract bool Stop(int axisNo, string stopMode);

        #endregion Methods
    }
}