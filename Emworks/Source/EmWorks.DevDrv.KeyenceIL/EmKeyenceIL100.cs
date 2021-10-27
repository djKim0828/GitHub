using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.IO.Ports;
using System.Threading;

namespace EmWorks.DevDrv.KeyenceIL100
{
    public partial class EmKeyenceIL100 : DriverBase
    {
        #region Fields

        public const int UnitAddress = 0;
        private bool _busyThread;
        private EmKeyenceIL100Model _configKeyenceIL100;
        private int _defaultTime;
        private double _distance;
        private int _errorCount;
        private bool _isPauseThread;
        private Define.Laser _onOffLaser;
        private string _receiveData;
        private Define.ReceiveResult _receiveResult;
        private SerialPort _serialPort;

        #endregion Fields

        #region Constructors

        public EmKeyenceIL100(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmKeyenceIL100()
        {
        }

        #endregion Destructors

        #region Events

        public event EventValueChanged ChangedDistance;

        public event EventValueChanged ChangedOnOffLaser;

        #endregion Events

        #region Properties

        public EmKeyenceIL100Model ConfigKeyenceIL100
        {
            get
            {
                return _configKeyenceIL100;
            }
        }

        public double Distance
        {
            get
            {
                return _distance;
            }
            protected set
            {
                if (_distance != value)
                {
                    _distance = value;
                    ChangedDistance?.Invoke();
                } // else
            }
        }

        public Define.Laser OnOffLaser
        {
            get
            {
                return _onOffLaser;
            }
            protected set
            {
                if (_onOffLaser != value)
                {
                    _onOffLaser = value;
                    ChangedOnOffLaser?.Invoke();
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

            Logger.Debug("IL100 - Try, Close");

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

            Logger.Debug("IL100 - Ok, Close");

            return true;
        }

        public void InitialConfig(string filePath)
        {
            _configKeyenceIL100 = new EmKeyenceIL100Model(filePath);
            _configKeyenceIL100.Load(ref _configKeyenceIL100);
            _configKeyenceIL100.Save();

            if ((int)IsSimulation < (int)_configKeyenceIL100.RunMode)
            {
                IsSimulation = _configKeyenceIL100.RunMode;
            } // else
        }

        public override bool Open()
        {
            if (IsSimulation == Define.RunMode.Simulation)
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
            _distance = 0.0;
        }

        protected override void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc);
        }

        protected override void ThreadUpdateProc(object threadContext)
        {
            int sleep = 500;
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
                        if (GetDistance() == false)
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
                Logger.Debug("IL100 - Try, Open");

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

                ComportModel tempComport = _configKeyenceIL100.Comport;

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

                Logger.Debug("IL100 - Ok, Open");

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

        #region Classes

        public class SerialCommand
        {
            #region Fields

            public const string SR = "SR";
            public const string SW = "SW";

            #endregion Fields
        }

        #endregion Classes
    }
}