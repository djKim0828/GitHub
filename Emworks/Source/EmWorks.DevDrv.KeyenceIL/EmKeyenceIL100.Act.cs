using EmWorks.Lib.Common;
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace EmWorks.DevDrv.KeyenceIL100
{
    public partial class EmKeyenceIL100
    {
        #region Methods

        public bool GetDistance()
        {
            byte[] sendCommand;
            int dataAddress = 37; //GetDistance

            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("IL100 - Error, Port is not open");

                    return false;
                } // else

                _receiveData = null;
                _receiveResult = Define.ReceiveResult.None;

                sendCommand = MakeCommand(SerialCommand.SR, UnitAddress, dataAddress);

                if (SendCommand(sendCommand) == false)
                {
                    string data = BitConverter.ToString(sendCommand);
                    data = data.Replace("-", " ");

                    Logger.Error($"IL100 - Error, GetDistance, SendCommand, Data : {data}");

                    return false;
                } // else

                int timeOut = Environment.TickCount + _defaultTime;
                while (true)
                {
                    if (_receiveResult == Define.ReceiveResult.Complete)
                    {
                        break;
                    }
                    else if (_receiveResult == Define.ReceiveResult.Error ||
                             timeOut < Environment.TickCount)
                    {
                        Logger.Error($"IL100 - Error, GetDistance, Receive Result, Data : {_receiveData}");

                        return false;
                    } // else

                    Thread.Sleep(100);
                }

                var readDataArray = _receiveData.Split(',');
                Distance = Convert.ToDouble(readDataArray[3]);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool SetOnOffLaser(Define.Laser onOff)
        {
            byte[] sendCommand;
            int dataAddress = 100; //On,Off

            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                Logger.Debug($"IL100 - try, SetOnOffLaser, {onOff}");

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("IL100 - Error, Port is not open");

                    return false;
                } // else

                Pause();
                WaitPause();

                _receiveData = null;
                _receiveResult = Define.ReceiveResult.None;

                sendCommand = MakeCommand(SerialCommand.SW, UnitAddress, dataAddress, (int)onOff);

                if (SendCommand(sendCommand) == false)
                {
                    Resume();

                    string data = BitConverter.ToString(sendCommand);
                    data = data.Replace("-", " ");

                    Logger.Error($"IL100 - Error, SetOnOffLaser, SendCommand, Data : {data}");

                    return false;
                } // else

                int timeOut = Environment.TickCount + _defaultTime;
                while (true)
                {
                    if (_receiveResult == Define.ReceiveResult.Complete)
                    {
                        break;
                    }
                    else if (_receiveResult == Define.ReceiveResult.Error ||
                             timeOut < Environment.TickCount)
                    {
                        Resume();

                        Logger.Error($"IL100 - Error, SetOnOffLaser, Receive Result, Data : {_receiveData}");

                        return false;
                    } // else

                    Thread.Sleep(100);
                }

                OnOffLaser = onOff;

                Resume();

                Logger.Debug($"IL100 - Ok, SetOnOffLaser, {onOff}");

                return true;
            }
            catch (Exception ex)
            {
                Resume();

                Logger.Exception(ex);

                return false;
            }
        }

        private bool Connect()
        {
            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                Logger.Debug("IL100 - Try, Connect");

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("IL100 - Error, Port is not open");

                    return false;
                } // else

                if (SetOnOffLaser(Define.Laser.Off) != true)
                {
                    Logger.Error("IL100 - Error, Connect");

                    return false;
                } // else

                StartUpdateProc();

                Logger.Debug("IL100 - Ok, Connect");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private byte[] MakeCommand(string command, int unitAddress, int dataAddress, int setValue = 0)
        {
            string writeData = string.Empty;
            byte[] sendCommand;

            switch (command)
            {
                case SerialCommand.SR:
                    writeData = string.Format("{0},{1:00},{2:000}", command, unitAddress, dataAddress);
                    break;

                case SerialCommand.SW:
                    writeData = string.Format("{0},{1:00},{2:000},{3}", command, unitAddress, dataAddress, setValue);
                    break;

                default:
                    break;
            }

            sendCommand = Encoding.Default.GetBytes(writeData);

            return sendCommand;
        }

        private bool SendCommand(byte[] command, int waitTime = 5000)
        {
            try
            {
                _serialPort.WriteTimeout = waitTime;

                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                Thread.Sleep(30);

                string data = Encoding.Default.GetString(command);
                _serialPort.WriteLine(data);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = _serialPort.BytesToRead;

            try
            {
                if (size != 0)
                {
                    _receiveData = _serialPort.ReadLine();

                    var readDataArray = _receiveData.Split(',');
                    if (readDataArray[0].Contains(SerialCommand.SR) == true)
                    {
                        if (readDataArray.Length != 4)
                        {
                            _receiveResult = Define.ReceiveResult.Error;

                            return;
                        } // else
                    }
                    else if (readDataArray[0].Contains(SerialCommand.SW) == true)
                    {
                        if (readDataArray.Length != 3)
                        {
                            _receiveResult = Define.ReceiveResult.Error;

                            return;
                        } // else
                    }
                    else
                    {
                        _receiveResult = Define.ReceiveResult.Error;

                        return;
                    }

                    _receiveResult = Define.ReceiveResult.Complete;
                } // else
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                _receiveResult = Define.ReceiveResult.Error;
            }
        }

        #endregion Methods
    }
}