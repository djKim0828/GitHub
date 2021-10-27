using EmWorks.Lib.Common;
using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace EmWorks.DevDrv.SamwonSDR100
{
    public partial class EmSamwonSDR100
    {
        #region Methods

        private byte[] CheckSum(byte[] value, int length)
        {
            byte[] outvalue = new byte[2];
            int sum = 0;
            string temp = string.Empty;

            for (int i = 1; i < length; i++)
            {
                sum += value[i];
            }

            temp = BitConverter.GetBytes(sum)[0].ToString("X2");
            outvalue = Encoding.ASCII.GetBytes(temp);

            return outvalue;
        }

        private bool Connect()
        {
            byte[] sendCommand;

            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                Logger.Debug("SDR100 - Try, Connect");

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("SDR100 - Error, Port is not open");

                    return false;
                } // else

                Pause();
                WaitPause();

                _receiveData = null;
                _receiveResult = Define.ReceiveResult.None;

                sendCommand = MakeCommand(UnitAddress, SerialCommand.AMI);

                if (SendCommand(sendCommand) == false)
                {
                    Resume();

                    string data = BitConverter.ToString(sendCommand);
                    data = data.Replace("-", " ");

                    Logger.Error($"SDR100 - Error, Connect, SendCommand, Data : {data}");

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

                        Logger.Error($"SDR100 - Error, Connect, Receive Result, Data : {_receiveData}");

                        return false;
                    } // else

                    Thread.Sleep(100);
                }

                StartUpdateProc();

                Resume();

                Logger.Debug("SDR100 - Ok, Connect");

                return true;
            }
            catch (Exception ex)
            {
                Resume();

                Logger.Exception(ex);

                return false;
            }
        }

        private bool GetPV()
        {
            byte[] sendCommand;
            double pvValue = 0.0;
            int dataLength = 6;
            int dataAddress = 1; //GetPV

            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("SDR100 - Error, Port is not open");

                    return false;
                } // else

                _receiveData = null;
                _receiveResult = Define.ReceiveResult.None;

                sendCommand = MakeCommand(UnitAddress, SerialCommand.RSD, dataLength, dataAddress);

                if (SendCommand(sendCommand) == false)
                {
                    string data = BitConverter.ToString(sendCommand);
                    data = data.Replace("-", " ");

                    Logger.Error($"SDR100 - Error, GetPV, SendCommand, Data : {data}");

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
                        Logger.Error($"SDR100 - Error, GetPV, Receive Result, Data : {_receiveData}");

                        return false;
                    } // else

                    Thread.Sleep(100);
                }

                var readDataArray = _receiveData.Split(',');
                if (readDataArray.Length != 8)
                {
                    Logger.Error($"SDR100 - Error, GetPV, Receive Data Length, Data : {_receiveData}");

                    return false;
                } // else

                pvValue = Convert.ToInt32(readDataArray[2], 16);
                PV1 = pvValue * 0.1;

                pvValue = Convert.ToInt32(readDataArray[3], 16);
                PV2 = pvValue * 0.1;

                pvValue = Convert.ToInt32(readDataArray[4], 16);
                PV3 = pvValue * 0.1;

                pvValue = Convert.ToInt32(readDataArray[5], 16);
                PV4 = pvValue * 0.1;

                pvValue = Convert.ToInt32(readDataArray[6], 16);
                PV5 = pvValue * 0.1;

                pvValue = Convert.ToInt32(readDataArray[7], 16);
                PV6 = pvValue * 0.1;

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private byte[] MakeCommand(int unitAddress, string command, int dataCount = 0, int dataAddress = 0, int setValue = 0)
        {
            string writeData = string.Empty;
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
            const int checksumLength = 2;// Checksum 제외한 Data 길이 & Checksum 시작 Index
            int size = _serialPort.BytesToRead;

            try
            {
                if (size != 0)
                {
                    _receiveData = _serialPort.ReadLine();

                    byte[] readData = Encoding.Default.GetBytes(_receiveData);

                    int checksumLocation = readData.Length - checksumLength;

                    byte[] checksum = CheckSum(readData, checksumLocation);
                    if (readData[checksumLocation] != checksum[0] ||
                        readData[checksumLocation + 1] != checksum[1])
                    {
                        _receiveResult = Define.ReceiveResult.Error;

                        return;
                    } // else

                    _receiveData = _receiveData.Substring(0, _receiveData.Length - checksumLength);

                    if (_receiveData.Contains("OK") == false)
                    {
                        _receiveResult = Define.ReceiveResult.Error;

                        return;
                    } // else

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