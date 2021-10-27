using EmWorks.Lib.Common;
using System;

namespace EmWorks.Lib.DriverCore
{
    [Serializable]
    public abstract class DIoModelBase
    {
        #region Fields

        private string _dIoName;

        private int _dIoNumber;

        private bool _siganl;

        #endregion Fields

        #region Constructors

        public DIoModelBase()
        {

        }
        public DIoModelBase(int dIoNumber)
        {
            this._dIoNumber = dIoNumber;
        }

        #endregion Constructors

        #region Delegates

        public delegate void EventValueChanged(object sender, EventArgs Args);

        #endregion Delegates

        #region Events

        public event EventValueChanged OnChange;

        #endregion Events

        #region Properties

        public virtual string DIoName
        {
            get { return _dIoName; }
            set { _dIoName = value; }
        }

        public virtual int DIoNumber
        {
            get { return _dIoNumber; }
            set { _dIoNumber = value; }
        }

        public virtual bool Signal
        {
            get
            {
                return _siganl;
            }
            set
            {
                if (_siganl != value)
                {
                    _siganl = value;
                    OnChange?.Invoke(this, new EventVelueChangeArgs(this._dIoName, boolenValue: this._siganl, intValue: _dIoNumber));
                    Logger.Debug(this._dIoName, this);
                }//else
            }
        }
        public virtual bool CommendSignal
        {
            get
            {
                return _siganl;
            }
            set
            {
                if (_siganl != value)
                {
                    _siganl = value;
                    Logger.Debug(this._dIoName, this);
                }//else
            }
        }

        #endregion Properties
    }
}