using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.Lib.DriverCore
{
    [Serializable]
    public abstract class MotionAxisPropertyModelBase
    {
        #region Fields

        private double _accel = double.NaN;
        private double _actualPosition;
        private bool _alarm = true;
        private List<string> _alarmCode = new List<string>();
        private Dictionary<string, PositionModel> _axisPosition = new Dictionary<string, PositionModel>();
        private bool _busy;
        private double _commandPosisiton;

        private double _decel = double.NaN;
        private bool _home;
        private bool _inPosition;
        private bool _negativeLimit;
        private bool _orign;
        private bool _positiveLimit;
        private bool _servoOn;
        private double _velocity = double.NaN;

        #endregion Fields

        #region Delegates

        public delegate void EventValueChanged(object sender, EventArgs Args);

        #endregion Delegates

        #region Events

        public event EventValueChanged OnAccel;

        public event EventValueChanged OnActualPosition;

        public event EventValueChanged OnAlarm;

        public event EventValueChanged OnAlarmCode;

        public event EventValueChanged OnBusy;

        public event EventValueChanged OnCommandPosisiton;

        public event EventValueChanged OnDecel;

        public event EventValueChanged OnHome;

        public event EventValueChanged OnInPosition;

        public event EventValueChanged OnNegativeLimit;

        public event EventValueChanged OnOrign;

        public event EventValueChanged OnPositiveLimit;

        public event EventValueChanged OnServoOn;

        public event EventValueChanged OnVelocity;

        #endregion Events

        #region Properties

        public virtual double Accel
        {
            get
            {
                return _accel;
            }
            set
            {
                if (_accel != value)
                {
                    _accel = value;
                    OnAccel?.Invoke(this, new EventVelueChangeArgs(this.AxisName, doubleValue: value));
                } // else
            }
        }

        public virtual double ActualPosition
        {
            get
            {
                return _actualPosition;
            }
            set
            {
                double temp = Math.Round(value, 3);
                if (_actualPosition != temp)
                {
                    _actualPosition = temp;
                    OnActualPosition?.Invoke(this, new EventVelueChangeArgs(this.AxisName, doubleValue: value));
                } // else
            }
        }

        public virtual bool Alarm
        {
            get
            {
                return _alarm;
            }
            set
            {
                if (_alarm != value)
                {
                    _alarm = value;
                    OnAlarm?.Invoke(this, new EventVelueChangeArgs(name: this.AxisName, boolenValue: value));
                } // else
            }
        }

        public virtual List<string> AlarmCode
        {
            get
            {
                return _alarmCode;
            }
            set
            {
                if (_alarmCode != value)
                {
                    _alarmCode = value;
                    OnAlarmCode?.Invoke(this, new EventVelueChangeArgs(this.AxisName));
                } // else
            }
        }

        public virtual string AxisName { get; set; } = string.Empty;

        public virtual int AxisNumber { get; set; } = int.MinValue;

        public virtual Dictionary<string, PositionModel> AxisPosition
        {
            get
            {
                return _axisPosition;
            }
            set
            {
                _axisPosition = value;
            }
        }

        public virtual bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                if (_busy != value)
                {
                    _busy = value;
                    OnBusy?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual double CommandPosisiton
        {
            get
            {
                return _commandPosisiton;
            }
            set
            {
                double temp = Math.Round(value, 3);
                if (_commandPosisiton != temp)
                {
                    _commandPosisiton = temp;
                    OnCommandPosisiton?.Invoke(this, new EventVelueChangeArgs(this.AxisName, doubleValue: temp));
                } // else
            }
        }

        public virtual double Decel
        {
            get
            {
                return _decel;
            }
            set
            {
                if (_decel != value)
                {
                    _decel = value;
                    OnDecel?.Invoke(this, new EventVelueChangeArgs(this.AxisName, doubleValue: value));
                } // else
            }
        }

        public virtual bool Home
        {
            get
            {
                return _home;
            }
            set
            {
                if (_home != value)
                {
                    _home = value;
                    OnHome?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual bool InPosition
        {
            get
            {
                return _inPosition;
            }
            set
            {
                if (_inPosition != value)
                {
                    _inPosition = value;
                    OnInPosition?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual bool NegativeLimit
        {
            get
            {
                return _negativeLimit;
            }
            set
            {
                if (_negativeLimit != value)
                {
                    _negativeLimit = value;
                    OnNegativeLimit?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual bool Orign
        {
            get
            {
                return _orign;
            }
            set
            {
                if (_orign != value)
                {
                    _orign = value;
                    OnOrign?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual bool PositiveLimit
        {
            get
            {
                return _positiveLimit;
            }
            set
            {
                if (_positiveLimit != value)
                {
                    _positiveLimit = value;
                    OnPositiveLimit?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual bool ServoOn
        {
            get
            {
                return _servoOn;
            }
            set
            {
                if (_servoOn != value)
                {
                    _servoOn = value;
                    OnServoOn?.Invoke(this, new EventVelueChangeArgs(this.AxisName, value));
                } // else
            }
        }

        public virtual double Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                if (_velocity != value)
                {
                    _velocity = value;
                    OnVelocity?.Invoke(this, new EventVelueChangeArgs(this.AxisName, doubleValue: value));
                } // else
            }
        }

        #endregion Properties
    }
}