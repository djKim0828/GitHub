using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.AjinMotionIo
{
    [Serializable]
    public class AxisPropertyModel : MotionAxisPropertyModelBase
    {
        #region Constructors

        public AxisPropertyModel(string axisName, int axisNum)
        {
            this.AxisName = axisName;
            this.AxisNumber = axisNum;
        }

        #endregion Constructors
        private bool _absoluteType = false;
        private EmAjinMotion.AbsRelMode _moveMode = default(EmAjinMotion.AbsRelMode);

        #region Properties

        public event EventValueChanged OnChangeMoveMode;

        public bool AbsoluteType
        {
            get
            {
                return _absoluteType;
            }
            set
            {
                _absoluteType = value;
            }
        }
        public EmAjinMotion.AbsRelMode MoveMode
        {
            get
            {
                return _moveMode;
            }
            set
            {
                if (_moveMode != value)
                {
                    _moveMode = value;
                    OnChangeMoveMode?.Invoke(this, new EventVelueChangeArgs(this.AxisName));
                }//else
            }
        }

        #endregion Properties
    }
}