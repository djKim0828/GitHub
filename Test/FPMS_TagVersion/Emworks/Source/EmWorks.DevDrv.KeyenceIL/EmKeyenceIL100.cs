using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.KeyenceIL100
{
    public class EmKeyenceIL100 : DriverBase
    {
        #region Fields

        private Dictionary<string, Tag> _data;

        private bool _isUpdateState = false;
        private SerialPort _serialPort;

        #endregion Fields

        #region Constructors

        public EmKeyenceIL100(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {
            _data = tags;

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~EmKeyenceIL100()
        {
            Close(null);
        }

        #endregion Destructors

        #region Methods

        public override int Close(Tag commandTag)
        {
            _serialPort.Close();
            _serialPort = null;

            // sx Tag 값을 수정해주어야 한다.
            UpdateStateInput(Idx.OpenClose.Close);

            return 0;
        }

        public override int Open(Tag commonTag)
        {
            try
            {
                if (_serialPort != null)
                {
                    return Idx.FunctionResult.True;
                }//else

                //_SerialPort = new SerialPort(commonTag.Identity.Options[0].ToString(),
                //        commonTag.Identity.Options[1]);

                //_SerialPort.DataReceived += _SerialPort_DataReceived;

                // sx Tag 값을 수정해주어야 한다.
                UpdateStateInput(Idx.OpenClose.Open);

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return Idx.FunctionResult.False;
            }
        }

        protected override void DoSimulation(Tag tag)
        {
        }

        protected override void DoUpdate()
        {
            GetDistance();
        }

        protected override void OnCommand(object sendor, EventTagChangedArgs e)
        {
            Action action = () =>
            {
                Command(e.Tag);
            };

            action.BeginInvoke(null, null);
        }

        protected override void ThreadUpdateProc(object threadContext)
        {
            while (IsUpdateLoop)
            {
                if (_isUpdateState == false)
                {
                    continue;
                }//else

                if (IsOpened)
                {
                    DoUpdate();
                    Thread.Sleep(LoopInterval);
                }
                else
                    Thread.Sleep(5000);
            }
        }

        private void Command(Tag tag)
        {
            try
            {
                if (tag.Identity.IoType == Tag.Idx.IoType.StatusOutput)
                {
                    if (tag.Is.ToString() == Idx.OpenClose.Open.ToString())
                    {
                        Open(tag);
                    }
                    else
                    {
                        Close(tag);
                    }
                }
                else if (tag.Identity.IoType == Tag.Idx.IoType.Output)
                {
                    RunCommand(tag);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void GetDistance()
        {
            string buffer = string.Empty;
            int dataAddress = 37; //NPV

            Tag t = GetTagByExeCmd(Idx.IoType.input, "GetDistance");

            // GetDistance SR,unitAddress,dataAddress
            string writeData = string.Format("SR,{0:00},{1:000}", 0, dataAddress);
            try
            {
                SendCommand(writeData);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }

            if (false == ReadData(ref buffer))
            {
                Logger.Infomation("Read Fail");
                return;
            }//else

            var readDataArray = buffer.Split(',');
            if (readDataArray.Length == 4)
            {
                for (int i = 0; i < readDataArray.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (readDataArray[i].IndexOf("SR") == -1)
                            {
                                Logger.Infomation("\'SR\'Read Fail");
                                return;
                            }//else
                            break;

                        case 3:
                            t.Is = Convert.ToDouble(readDataArray[i]);
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                Logger.Infomation("Read Fail");
                return;
            }

            return;
        }

        private Tag GetTagByExeCmd(int ioType, string exeCmd)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == ioType &&
                                            x.Value.Identity.ExeCmd == exeCmd).Value;

            return temp;
        }

        private Tag GetTagByState(int IoType)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == IoType).Value;

            return temp;
        }

        private void Initialize()
        {
            InitInstance();
            RegistEvent();

            StartUpdateProc();
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
                }//else
            }//else
        }

        private void OnOffLazer(Tag tag)
        {
            string buffer = string.Empty;
            int dataAddress = 100; //NPV

            //if (IsConnect == false) return;

            // Lazer OnOff SW,unitAddress,dataAddress, OnOff
            string writeData = string.Format("SW,{0:00},{1:000},{2}", 0, dataAddress, (int)tag.Is == 1 ? "0" : "1");

            try
            {
                SendCommand(writeData);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            if (false == ReadData(ref buffer))
            {
                Logger.Infomation("Read Fail");
                return;
            }//else

            var readDataArray = buffer.Split(',');
            if (readDataArray.Length == 3)
            {
                for (int i = 0; i < readDataArray.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (readDataArray[i].IndexOf("SW") == -1)
                            {
                                Logger.Infomation("\'SW\'Read Fail");
                                return;
                            }//else
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                Logger.Infomation("Read Fail");
                return;
            }

            if ((int)tag.Is == 1)
            {
                _isUpdateState = true;
            }
            else
            {
                _isUpdateState = false;
            }

            Logger.Debug(string.Format("SetLaser Complete : {0}", (int)tag.Is == 1 ? "On" : "Off"));
            return;
        }

        private bool ReadData(ref string readData, string checkText = "", int waitTime = 5000, bool readLine = true)
        {
            byte[] buffer = new byte[_serialPort.ReadBufferSize];
            try
            {
                _serialPort.ReadTimeout = waitTime;
                if (readLine)
                {
                    readData = _serialPort.ReadLine();
                }
                else
                {
                    _serialPort.Read(buffer, 0, _serialPort.ReadBufferSize);
                    readData = _serialPort.Encoding.GetString(buffer);
                }

                if (string.IsNullOrEmpty(checkText) == false)
                {
                    if (readData.IndexOf(checkText) == -1)
                    {
                        return false;
                    }//else
                }//else
                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
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
                    }//else
                }
            }//else

            if (_yTags != null)
            {
                foreach (var tag in _yTags)
                {
                    tag.Value.IsSimulation = IsSimulation;

                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    }//else

                    tag.Value.OnCommand += new EventTagChanged(OnCommand);
                }
            }//else
        }

        private void RunCommand(Tag tag)
        {
            OnOffLazer(tag);
        }

        private void SendCommand(string command)
        {
            _serialPort.WriteLine(command);
        }

        private void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }

        private void UpdateStateInput(object isOpen)
        {
            Tag tag = GetTagByState(Idx.IoType.StateInput);

            tag.Is = isOpen;
        }

        #endregion Methods
    }
}