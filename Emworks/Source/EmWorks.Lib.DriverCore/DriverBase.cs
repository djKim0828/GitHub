using EmWorks.Lib.Common;
using System.Threading;

namespace EmWorks.Lib.DriverCore
{
    public abstract class DriverBase
    {
        #region Fields

        protected bool _isOpen = false;

        #endregion Fields

        #region Constructors

        public DriverBase(Define.RunMode isSimulation)
        {
            IsSimulation = isSimulation;
            Logger.Debug("RunMode = ", IsSimulation);
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~DriverBase()
        {
            Close();
            IsUpdateLoop = false;
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventValueChanged();

        #endregion Delegates

        #region Events

        public event EventValueChanged ChangedOpen;

        #endregion Events

        #region Properties

        public virtual bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    ChangedOpen?.Invoke();
                } // else
            }
        }

        public virtual Define.RunMode IsSimulation { get; set; } = Define.RunMode.Simulation;
        public virtual bool IsUpdateLoop { get; set; } = false;
        public virtual int LoopInterval { get; set; } = 5;

        #endregion Properties

        #region Methods

        public abstract bool Close();

        public abstract bool Open();

        protected virtual void Initialize()
        {
            InitInstance();
            RegistEvent();

            if (IsSimulation != Define.RunMode.Simulation)
            {
                return;
            } // else
        }

        protected virtual void InitInstance()
        {
        }

        protected virtual void RegistEvent()
        {
        }

        protected virtual void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }

        protected virtual void ThreadUpdateProc(object threadContext)
        {
            while (IsUpdateLoop)
            {
                if (IsOpen)
                {
                    Thread.Sleep(LoopInterval);
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        #endregion Methods
    }
}
