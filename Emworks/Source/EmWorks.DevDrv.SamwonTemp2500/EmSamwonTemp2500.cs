using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace EmWorks.DevDrv.SamwonTemp2500
{
    public partial class EmSamwonTemp2500 : DriverBase
    {
        #region Fields

        public const int DICount = 16;
        public const int UnitAddress = 1;
        private bool _busyThread;
        private Dictionary<string, DIModel> _chamberDI = new Dictionary<string, DIModel>();
        private EmSamwonTemp2500Model _configSamwonTemp2500;
        private int _defaultTime;
        private List<DIModel> _dIModelList = new List<DIModel>();
        private int _errorCount;
        private ushort _errorValue;
        private bool _isPauseThread;
        private double _pv;
        private string _receiveData;
        private Define.ReceiveResult _receiveResult;
        private SerialPort _serialPort;
        private string _stx;
        private double _sv;

        #endregion Fields

        #region Constructors

        public EmSamwonTemp2500(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmSamwonTemp2500()
        {
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventDIChanged(object sender, EventArgs Args);

        #endregion Delegates

        #region Events

        public event EventValueChanged ChangedPV;

        public event EventValueChanged ChangedSV;

        public event EventDIChanged DigitalInputOnChange;

        #endregion Events

        #region Properties

        public Dictionary<string, DIModel> ChamberDI
        {
            get
            {
                return _chamberDI;
            }

            protected set
            {
                _chamberDI = value;
            }
        }

        public EmSamwonTemp2500Model ConfigSamwonTemp2500
        {
            get
            {
                return _configSamwonTemp2500;
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

        public double SV
        {
            get
            {
                return _sv;
            }
            protected set
            {
                if (_sv != value)
                {
                    _sv = value;
                    ChangedSV?.Invoke();
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

            Logger.Debug("Temp2500 - Try, Close");

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

            Logger.Debug("Temp2500 - Ok, Close");

            return true;
        }

        public bool DefineInputList(Dictionary<string, int> ioList)
        {
            bool result = true;

            foreach (var item in ioList)
            {
                if (_dIModelList.Count <= item.Value)
                {
                    Logger.Error("SetInputList Out of Count - " + item.Key);
                    result = false;

                    continue;
                }//else

                if (_dIModelList[item.Value].DIoName != item.Key)
                {
                    _dIModelList[item.Value].DIoName = item.Key;

                    if (_chamberDI.ContainsKey(item.Key) == true)
                    {
                        _chamberDI[item.Key] = _dIModelList[item.Value];
                    }
                    else
                    {
                        _chamberDI.Add(item.Key, _dIModelList[item.Value]);
                    }
                }//else
            }

            return result;
        }

        public void InitialConfig(string filePath)
        {
            _configSamwonTemp2500 = new EmSamwonTemp2500Model(filePath);
            _configSamwonTemp2500.Load(ref _configSamwonTemp2500);
            _configSamwonTemp2500.Save();

            if ((int)IsSimulation < (int)_configSamwonTemp2500.RunMode)
            {
                IsSimulation = _configSamwonTemp2500.RunMode;
            } // else
        }

        public override bool Open()
        {
            if (IsSimulation == Define.RunMode.Simulation)
            {
                IsOpen = true;

                StartUpdateProc();

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

        internal void DI_OnChange(object sender, EventArgs Args)
        {
            DigitalInputOnChange?.Invoke(this, Args);
        }

        protected override void Initialize()
        {
            InitInstance();
            RegistEvents();
        }

        protected override void InitInstance()
        {
            _stx = "\u0002";
            _defaultTime = 5000;
            _errorCount = 3;
            _isPauseThread = false;
            _busyThread = false;
            _pv = 0.0;
            _sv = 0.0;

            for (int i = 0; i < DICount; i++)
            {
                DIModel temp = new DIModel(i);
                _dIModelList.Add(temp);
            }
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
                        if (GetPVSV() == false)
                        {
                            errorCount++;
                        }
                        else
                        {
                            errorCount = 0;
                        }

                        Thread.Sleep(sleep);

                        if (GetDI() == false)
                        {
                            errorCount++;
                        }
                        else
                        {
                            errorCount = 0;

                            DIUpdate();
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

        private void DIUpdate()
        {
            foreach (var item in _chamberDI)
            {
                if (string.IsNullOrEmpty(item.Value.DIoName) != true)
                {
                    int temp = (_errorValue >> item.Value.DIoNumber) & 0x01;

                    item.Value.Signal = Convert.ToBoolean(temp);
                }//else
            }
        }

        private bool OpenSerialPort()
        {
            try
            {
                Logger.Debug("Temp2500 - Try, Open");

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

                ComportModel tempComport = _configSamwonTemp2500.Comport;

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

                Logger.Debug("Temp2500 - Ok, Open");

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

        private void RegistEvents()
        {
            foreach (var item in _dIModelList)
            {
                item.OnChange += DI_OnChange;
            }
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