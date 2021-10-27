using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.IO.Ports;
using System.Threading;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    public partial class EmAutonicsTK4S : DriverBase
    {
        #region Fields

        public const int UnitAddress = 1;
        private bool _busyThread;
        private EmAutonicsTK4SModel _configAutonicsTK4S;
        private int _defaultTime;
        private int _errorCount;
        private bool _isPauseThread;
        private double _pv;
        private byte[] _receiveData;
        private Define.ReceiveResult _receiveResult;
        private SerialPort _serialPort;

        #endregion Fields

        #region Constructors

        public EmAutonicsTK4S(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmAutonicsTK4S()
        {
        }

        #endregion Destructors

        #region Events

        public event EventValueChanged ChangedPV;

        #endregion Events

        #region Properties

        public EmAutonicsTK4SModel ConfigAutonicsTK4S
        {
            get
            {
                return _configAutonicsTK4S;
            }
        }

        public double PV
        {
            get
            {
                return _pv;
            }
            protected set
            {
                if (_pv != value)
                {
                    _pv = value;
                    ChangedPV?.Invoke();
                } // else
            }
        }

        #endregion Properties

        #region Methods

        public override bool Close()
        {
            if (IsSimulation == Define.RunMode.Simulation)
            {
                IsOpen = false;

                return true;
            } // else

            Logger.Debug("TK4S - Try, Close");

            if (_serialPort != null)
            {
                IsUpdateLoop = false;

                while (true)
                {
                    if (_busyThread == false)
                    {
                        break;
                    } // else

                    Thread.Sleep(50);
                }

                _serialPort.Close();
                _serialPort = null;
            } // else

            IsOpen = false;

            Logger.Debug("TK4S - Ok, Close");

            return true;
        }

        public void InitialConfig(string filePath)
        {
            _configAutonicsTK4S = new EmAutonicsTK4SModel(filePath);
            _configAutonicsTK4S.Load(ref _configAutonicsTK4S);
            _configAutonicsTK4S.Save();

            if ((int)IsSimulation < (int)_configAutonicsTK4S.RunMode)
            {
                IsSimulation = _configAutonicsTK4S.RunMode;
            } // else
        }

        public override bool Open()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                IsOpen = true;

                return true;
            } // else

            if (OpenSerialPort() == false)
            {
                IsOpen = false;

                return false;
            } // else

            if (Connect() == false)
            {
                IsOpen = false;

                return false;
            } // else

            IsOpen = true;

            return true;
        }

        protected override void Initialize()
        {
            InitInstance();
        }

        protected override void InitInstance()
        {
            _defaultTime = 5000;
            _errorCount = 3;
            _isPauseThread = false;
            _busyThread = false;
            _pv = 0.0;
        }

        protected override void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc);
        }

        protected override void ThreadUpdateProc(object threadContext)
        {
            int sleep = 1000;
            int errorCount = 0;

            try
            {
                while (IsUpdateLoop == true)
                {
                    if (_isPauseThread == true)
                    {
                        Thread.Sleep(sleep);
                        continue;
                    } // else

                    _busyThread = true;

                    if (IsOpen == true)
                    {
                        if (GetPV() == false)
                        {
                            errorCount++;
                        }
                        else
                        {
                            errorCount = 0;
                        }

                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        Thread.Sleep(sleep);
                    }

                    _busyThread = false;

                    if (_errorCount <= errorCount)
                    {
                        IsUpdateLoop = false;

                        Close();
                    } // else
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private bool OpenSerialPort()
        {
            try
            {
                Logger.Debug("TK4S - Try, Open");

                if (_serialPort == null)
                {
                    _serialPort = new SerialPort();
                }
                else
                {
                    if (_serialPort.IsOpen == true)
                    {
                        _serialPort.Close();
                    } // else
                }

                ComportModel tempComport = _configAutonicsTK4S.Comport;

                _serialPort.PortName = tempComport.PortNum;
                _serialPort.BaudRate = int.Parse(tempComport.BaudRate);
                _serialPort.DataBits = int.Parse(tempComport.DataBits);
                _serialPort.Parity = tempComport.ParityBit;
                _serialPort.StopBits = tempComport.StopBit;
                _serialPort.DtrEnable = tempComport.DtrEnable;
                _serialPort.RtsEnable = tempComport.RtsEnable;
                _serialPort.DataReceived += SerialPort_DataReceived;

                if (tempComport.SendTerminateString != string.Empty &&
                    tempComport.RecvTerminateString != string.Empty &&
                    tempComport.SendTerminateString == tempComport.RecvTerminateString)
                {
                    _serialPort.NewLine = tempComport.SendTerminateString;
                } // else

                _serialPort.Open();

                Logger.Debug("TK4S - Ok, Open");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void Pause()
        {
            _isPauseThread = true;
        }

        private void Resume()
        {
            _isPauseThread = false;
        }

        private void WaitPause(int defaultTime = 5000)
        {
            int timeOut = Environment.TickCount + defaultTime;

            while (true)
            {
                if (_busyThread == false)
                {
                    break;
                } // else

                if (timeOut < Environment.TickCount)
                {
                    return;
                } // else

                Thread.Sleep(50);
            }
            return;
        }

        #endregion Methods
    }
}