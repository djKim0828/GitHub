using System;

namespace EmWorks.Lib.DriverCore
{
    [Serializable]
    public class PositionModel
    {
        #region Constructors

        public PositionModel()
        {
        }

        public PositionModel(string positionName)
        {
            PositionName = positionName;
        }

        #endregion Constructors

        #region Properties

        public double Accel { get; set; } = double.MinValue;
        public double Decel { get; set; } = double.MinValue;
        public string MoveMode { get; set; } = string.Empty;
        public double Position { get; set; } = double.MinValue;
        public string PositionName { get; set; } = string.Empty;
        public double Velocity { get; set; } = double.MinValue;

        #endregion Properties
    }
}