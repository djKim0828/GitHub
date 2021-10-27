using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.IO.Ports;
using System.Threading;

namespace EmWorks.DevDrv.SamwonSDR100
{
    public partial class EmSamwonSDR100 : DriverBase
    {
        #region Fields

        public const int UnitAddress = 1;
        private bool _busyThread;
        private EmSamwonSDR100Model _configSamwonSDR100;
        private int _defaultTime;
        private int _errorCount;
        private bool _isPauseThread;
        private double _pv1;
        private double _pv2;
        private double _pv3;
        private double _pv4;
        private double _pv5;
        private double _pv6;
        private string _receiveData;
        private Define.ReceiveResult _receiveResult;
        private SerialPort _serialPort;
        private string _stx;

        #endregion Fields

        #region Constructors

        public EmSamwonSDR100(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmSamwonSDR100()
        {
        }

        #endregion Destructors

        #region Events

        public event EventValueChanged ChangedPV1;

        public event EventValueChanged ChangedPV2;

        public event EventValueChanged ChangedPV3;

        public event EventValueChanged ChangedPV4;

        public event EventValueChanged ChangedPV5;

        public event EventValueChanged ChangedPV6;

        #endregion Events        

        #region Properties

        public EmSamwonSDR100Model ConfigSamwonSDR100
        {
            get
            {
                return _configSamwonSDR100;
            }
        }

        public double PV1
        {
            get
            {
                return _pv1;
            }
            set
            {
                if (_pv1 != value)
                {
                    _pv1 = value;
                    ChangedPV1?.Invoke();
                } // else
            }
        }

        public double PV2
        {
            get
            {
                return _pv2;
            }
            set
            {
                if (_pv2 != value)
                {
                    _pv2 = value;
                    ChangedPV2?.Invoke();
                } // else
            }
        }

        public double PV3
        {
            get
            {
                return _pv3;
            }
            set
            {
                if (_pv3 != value)
                {
                    _pv3 = value;
                    ChangedPV3?.Invoke();
                } // else
            }
        }

        public double PV4
        {
            get
            {
                return _pv4;
            }
            set
            {
                if (_pv4 != value)
                {
                    _pv4 = value;
                    ChangedPV4?.Invoke();
                } // else
            }
        }

        public double PV5
        {
            get
            {
                return _pv5;
            }
            set
            {
                if (_pv5 != value)
                {
                    _pv5 = value;
                    ChangedPV5?.Invoke();
                } // else
            }
        }

        public double PV6
        {
            get
            {
                return _pv6;
            }
            set
            {
                if (_pv6 != value)
                {
                    _pv6 = value;
                    ChangedPV6?.Invoke();
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

            Logger.Debug("SDR100 - Try, Close");

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

            Logger.Debug("SDR100 - Ok, Close");

            return true;
        }

        public void InitialConfig(string filePath)
        {
            _configSamwonSDR100 = new EmSamwonSDR100Model(filePath);
            _configSamwonSDR100.Load(ref _configSamwonSDR100);
            _configSamwonSDR100.Save();

            if ((int)IsSimulation < (int)_configSamwonSDR100.RunMode)
            {
                IsSimulation = _configSamwonSDR100.RunMode;
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
            _stx = "\u0002";
            _defaultTime = 5000;
            _errorCount = 3;
            _isPauseThread = false;
            _busyThread = false;
            _pv1 = 0.0;
            _pv2 = 0.0;
            _pv3 = 0.0;
            _pv4 = 0.0;
            _pv5 = 0.0;
            _pv6 = 0.0;
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
                Logger.Debug("SDR100 - Try, Open");

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

                ComportModel tempComport = _configSamwonSDR100.Comport;

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

                Logger.Debug("SDR100 - Ok, Open");

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

            public const string AMI = "AMI";
            public const string RSD = "RSD";
            public const string WSD = "WSD";

            #endregion Fields
        }

        #endregion Classes
    }
}