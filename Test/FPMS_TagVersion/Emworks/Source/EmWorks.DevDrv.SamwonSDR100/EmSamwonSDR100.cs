using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace EmWorks.DevDrv.SamwonSDR100
{
    public partial class EmSamwonSDR100 : DriverBase
    {
        #region Fields

        private bool _busyThread;
        private ComportModel _comportInformation;
        private int _defaultTime;
        private int _errorCount;
        private bool _isPauseThread;
        private string _receiveData;
        private ReceiveResult _receiveResult;
        private Dictionary<string, Tag> _sdr100Tags;
        private SerialPort _serialPort;
        private string _stx;

        #endregion Fields

        #region Constructors

        public EmSamwonSDR100(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {
            _sdr100Tags = tags;
        }

        #endregion Constructors

        #region Destructors

        ~EmSamwonSDR100()
        {
        }

        #endregion Destructors

        #region Methods

        public override int Close(Tag commandTag)
        {
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

            // sx Tag 값을 수정해주어야 한다.
            UpdateStateInput(OpenCloseStatus.Close);

            return FunctionResult.True;
        }

        public override int Open(Tag commonTag)
        {
            try
            {
                SetCommandResult(commonTag.Identity, null);

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

                _serialPort.PortName = _comportInformation.PortNum;
                _serialPort.BaudRate = int.Parse(_comportInformation.BaudRate);
                _serialPort.DataBits = int.Parse(_comportInformation.DataBits);
                _serialPort.Parity = _comportInformation.ParityBit;
                _serialPort.StopBits = _comportInformation.StopBit;
                _serialPort.DtrEnable = _comportInformation.DtrEnable;
                _serialPort.RtsEnable = _comportInformation.RtsEnable;
                _serialPort.DataReceived += _SerialPort_DataReceived;

                if (_comportInformation.SendTerminateString != "" &&
                    _comportInformation.RecvTerminateString != "" &&
                    _comportInformation.SendTerminateString == _comportInformation.RecvTerminateString)
                {
                    _serialPort.NewLine = _comportInformation.SendTerminateString;
                } // else

                _serialPort.Open();

                if (Connect() == FunctionResult.False)
                {
                    UpdateStateInput(OpenCloseStatus.Error);

                    Logger.Error("Error> SDR100 Connect");

                    return FunctionResult.False;
                } // else

                // sx Tag 값을 수정해주어야 한다.
                UpdateStateInput(OpenCloseStatus.Open);

                IsOpened = true;
                StartUpdateProc();

                return FunctionResult.True;
            }
            catch (Exception ex)
            {
                UpdateStateInput(OpenCloseStatus.Error);

                IsOpened = false;
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

        protected override void DoSimulation(Tag tag)
        {
        }

        protected override void DoUpdate()
        {
        }

        protected override void Initialize()
        {
            InitInstance();
            RegistEvent();
        }

        protected override void OnCommand(object sendor, EventTagChangedArgs e)
        {
            Action action = () =>
            {
                Command(e.Tag);
            };

            action.BeginInvoke(null, null);
        }

        protected override void OnSimulation(object sendor, EventTagChangedArgs e)
        {
            Action action = () =>
            {
                RunSimCommand(e.Tag);
            };

            action.BeginInvoke(null, null);
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

                    if (IsOpened == true)
                    {
                        if (GetPV() == FunctionResult.False)
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

                        Tag tag = GetTagByState(IoType.StateInput);

                        Close(tag);
                    } // else
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = _serialPort.BytesToRead;

            try
            {
                if (size != 0)
                {
                    string tempReceiveData = _serialPort.ReadLine();

                    byte[] readData = Encoding.Default.GetBytes(tempReceiveData);

                    int checksumLocation = readData.Length - 2; // Checksum 제외한 Data 길이 & Checksum 시작 Index

                    byte[] checksum = CheckSum(readData, checksumLocation);
                    if (readData[checksumLocation] != checksum[0] ||
                        readData[checksumLocation + 1] != checksum[1])
                    {
                        _receiveResult = ReceiveResult.Error;

                        return;
                    } // else

                    _receiveData = tempReceiveData.Substring(0, tempReceiveData.Length - 2);

                    if (_receiveData.IndexOf("OK") == -1)
                    {
                        _receiveResult = ReceiveResult.Error;

                        return;
                    } // else

                    _receiveResult = ReceiveResult.Complete;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                _receiveResult = ReceiveResult.Error;
            }
        }

        private byte[] CheckSum(byte[] value, int length)
        {
            byte[] outvalue = new byte[2];
            int sum = 0;
            string temp = "";

            for (int i = 1; i < length; i++)
            {
                sum += value[i];
            }

            temp = BitConverter.GetBytes(sum)[0].ToString("X2");
            outvalue = ASCIIEncoding.ASCII.GetBytes(temp);

            return outvalue;
        }

        private void Command(Tag commandTag)
        {
            try
            {
                if (commandTag.Identity.IoType == Tag.Idx.IoType.StatusOutput)
                {
                    if (commandTag.Is.ToString() == OpenCloseStatus.Open.ToString())
                    {
                        Open(commandTag);
                    }
                    else
                    {
                        Close(commandTag);
                    }
                }
                else if (commandTag.Identity.IoType == Tag.Idx.IoType.Output)
                {
                    RunCommand(commandTag);
                } // else
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private int Connect()
        {
            byte[] sendCommand;

            try
            {
                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("Error> SDR100 Port is not open");

                    return FunctionResult.False;
                } // else

                _receiveData = null;
                _receiveResult = ReceiveResult.None;

                Pause();
                WaitPause();

                sendCommand = MakeCommand(GetTagByState(IoType.StateInput).Identity.Index, SerialCommand.AMI);

                if (SendCommand(sendCommand) == FunctionResult.False)
                {
                    Logger.Error("Error> Connect SendCommand");

                    Resume();

                    return FunctionResult.False;
                } // else

                int timeOut = Environment.TickCount + _defaultTime;
                while (true)
                {
                    if (_receiveResult == ReceiveResult.Complete)
                    {
                        break;
                    }
                    else if (_receiveResult == ReceiveResult.Error ||
                             timeOut < Environment.TickCount)
                    {
                        Logger.Error("Error> Connect Receive Result");

                        Resume();

                        return FunctionResult.False;
                    } // else

                    Thread.Sleep(100);
                }

                Resume();

                return FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                Resume();

                return FunctionResult.False;
            }
        }

        private int GetPV()
        {
            byte[] sendCommand;
            double pvValue = 0.0;
            int dataAddress = 1; //GetPV

            try
            {
                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("Error> SDR100 Port is not open");

                    return FunctionResult.False;
                } // else

                _receiveData = null;
                _receiveResult = ReceiveResult.None;

                sendCommand = MakeCommand(GetTagByExeCmd(IoType.input, "GetPV1").Identity.Index, SerialCommand.RSD, 6, dataAddress);

                if (SendCommand(sendCommand) == FunctionResult.False)
                {
                    Logger.Error("Error> GetPV SendCommand");

                    return FunctionResult.False;
                } // else

                int timeOut = Environment.TickCount + _defaultTime;
                while (true)
                {
                    if (_receiveResult == ReceiveResult.Complete)
                    {
                        break;
                    }
                    else if (_receiveResult == ReceiveResult.Error ||
                             timeOut < Environment.TickCount)
                    {
                        Logger.Error("Error> GetPV Receive Result");

                        return FunctionResult.False;
                    } // else

                    Thread.Sleep(100);
                }

                var readDataArray = _receiveData.Split(',');
                if (readDataArray.Length != 8)
                {
                    Logger.Error("Error> GetPV Receive Data Length");

                    return FunctionResult.False;
                } // else

                Tag tag = GetTagByExeCmd(IoType.input, "GetPV1");
                pvValue = Convert.ToInt32(readDataArray[2], 16);
                tag.Is = pvValue * 0.1;

                tag = GetTagByExeCmd(IoType.input, "GetPV2");
                pvValue = Convert.ToInt32(readDataArray[3], 16);
                tag.Is = pvValue * 0.1;

                tag = GetTagByExeCmd(IoType.input, "GetPV3");
                pvValue = Convert.ToInt32(readDataArray[4], 16);
                tag.Is = pvValue * 0.1;

                tag = GetTagByExeCmd(IoType.input, "GetPV4");
                pvValue = Convert.ToInt32(readDataArray[5], 16);
                tag.Is = pvValue * 0.1;

                tag = GetTagByExeCmd(IoType.input, "GetPV5");
                pvValue = Convert.ToInt32(readDataArray[6], 16);
                tag.Is = pvValue * 0.1;

                tag = GetTagByExeCmd(IoType.input, "GetPV6");
                pvValue = Convert.ToInt32(readDataArray[7], 16);
                tag.Is = pvValue * 0.1;

                return FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

        private bool GetRelatedTag(string configRelatedTag, ref RelatedTag relatedTags)
        {
            if (configRelatedTag == string.Empty)
            {
                return false;
            } // else

            relatedTags = new RelatedTag();

            try
            {
                string[] properties = configRelatedTag.Split('/');

                relatedTags.Name = properties[0];
                relatedTags.Type = properties[1];
                relatedTags.DefaultValue = properties[2];
                relatedTags.ReActValue = properties[3];
                relatedTags.DelayTime = properties[4];

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private Tag GetTag(string propertyName)
        {
            Tag temp = _sdr100Tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
            return temp;
        }

        private Tag GetTagByExeCmd(int ioType, string exeCmd)
        {
            Tag temp = _sdr100Tags.FirstOrDefault(x => x.Value.Identity.IoType == ioType && x.Value.Identity.ExeCmd == exeCmd).Value;

            return temp;
        }

        private Tag GetTagByState(int ioType)
        {
            Tag temp = _sdr100Tags.FirstOrDefault(x => x.Value.Identity.IoType == ioType).Value;

            return temp;
        }

        private void InitInstance()
        {
            if (base._tags != null)
            {
                if (base._tags.Count > 0)
                {
                    _xTags = (from item in base._tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Input
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusInput
                              select item).ToDictionary((KeyValuePair<string, Tag> x) => x.Key, (KeyValuePair<string, Tag> y) => y.Value);

                    _yTags = (from item in base._tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Output
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusOutput
                              select item).ToDictionary((KeyValuePair<string, Tag> x) => x.Key, (KeyValuePair<string, Tag> y) => y.Value);
                } // else
            } // else

            _stx = "\u0002";
            _defaultTime = 5000;
            _errorCount = 3;
        }

        private void LoadComport(Tag tag)
        {
            _comportInformation = (ComportModel)tag.Is;
        }

        private byte[] MakeCommand(int unitAddress, string command, int dataCount = 0, int dataAddress = 0, int setValue = 0)
        {
            string writeData = "";
            byte[] sendCommand;
            byte[] checksum = new byte[2];

            switch (command)
            {
                case SerialCommand.AMI:
                    writeData = _stx + string.Format("{0:00}{1}", unitAddress, command);
                    break;

                case SerialCommand.RSD:
                    writeData = _stx + string.Format("{0:00}{1},{2:00},{3:0000}", unitAddress, command, dataCount, dataAddress);
                    break;

                case SerialCommand.WSD:
                    writeData = _stx + string.Format("{0:00}{1},{2:00},{3:0000},{4:x4}", unitAddress, command, dataCount, dataAddress, setValue);
                    break;

                default:
                    break;
            }

            sendCommand = Encoding.Default.GetBytes(writeData);
            checksum = CheckSum(sendCommand, sendCommand.Length);
            sendCommand = sendCommand.Concat(checksum).ToArray();

            return sendCommand;
        }

        private void Pause()
        {
            _isPauseThread = true;
        }

        private void RegistEvent()
        {
            if (_xTags != null)
            {
                foreach (var tag in _xTags)
                {
                    tag.Value.IsSimulation = IsSimulation;

                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    } // else
                }
            } // else

            if (_yTags != null)
            {
                foreach (var tag in _yTags)
                {
                    tag.Value.IsSimulation = IsSimulation;

                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    } // else

                    tag.Value.OnCommand += new EventTagChanged(OnCommand);
                }
            } // else
        }

        private void Resume()
        {
            _isPauseThread = false;
        }

        private void RunCommand(Tag tag)
        {
            switch (tag.Identity.ExeCmd)
            {
                case ExeCmd.Comport:
                    LoadComport(tag);
                    break;

                default:
                    break;
            }
        }

        private int SendCommand(byte[] command, int waitTime = 5000)
        {
            try
            {
                _serialPort.WriteTimeout = waitTime;

                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                Thread.Sleep(30);

                _serialPort.Write(command, 0, command.Length);

                return FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

        private void SetCommandResult(TagIdentity tag, object value)
        {
            RelatedTag relatedTag = new RelatedTag();
            if (GetRelatedTag(tag.SimCmd, ref relatedTag) == true)
            {
                GetTag(relatedTag.Name).Is = value;
            } // else
        }

        private void StartUpdateProc()
        {
            IsUpdateLoop = true;
            _isPauseThread = false;
            _busyThread = false;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc);
        }

        private void UpdateStateInput(int isOpen)
        {
            Tag tag = GetTagByState(IoType.StateInput);

            tag.Is = isOpen;
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