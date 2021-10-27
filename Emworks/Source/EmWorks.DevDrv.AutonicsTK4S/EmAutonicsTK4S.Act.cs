using EmWorks.Lib.Common;
using System;
using System.IO.Ports;
using System.Threading;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    public partial class EmAutonicsTK4S
    {
        #region Methods

        private bool Connect()
        {
            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                Logger.Debug("TK4S - Try, Connect");

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("TK4S - Error, Port is not open");

                    return false;
                } // else

                if (GetPV() != true)
                {
                    Logger.Error("TK4S - Error, Connect");

                    return false;
                } // else

                StartUpdateProc();

                Logger.Debug("TK4S - Ok, Connect");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private bool GetPV()
        {
            byte[] sendCommand;
            int function = 4;
            int dataAddress = 1000; //PV
            
            int dataLength = 1;
            int crc16 = 45498; //B1BA

            try
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    return true;
                } // else

                if (_serialPort.IsOpen == false)
                {
                    Logger.Error("TK4S - Error, Port is not open");

                    return false;
                } // else

                _receiveData = null;
                _receiveResult = Define.ReceiveResult.None;

                sendCommand = MakeCommand(UnitAddress, function, dataAddress, dataLength, crc16);

                if (SendCommand(sendCommand) == false)
                {
                    string data = BitConverter.ToString(sendCommand);
                    data = data.Replace("-", " ");

                    Logger.Error($"TK4S - Error, GetPV, SendCommand, Data : {data}");

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
                        string data = BitConverter.ToString(_receiveData);
                        data = data.Replace("-", " ");

                        Logger.Error($"TK4S - Error, GetPV, Receive Result, Data : {data}");

                        return false;
                    } // else

                    Thread.Sleep(100);
                }

                PV = _receiveData[4];

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private byte[] MakeCommand(int unitAddress, int function, int dataAddress, int dataCount, int crc16)
        {
            string writeData = string.Empty;
            byte[] sendCommand = new byte[8];

            sendCommand[0] = (byte)unitAddress;
            sendCommand[1] = (byte)function;
            sendCommand[2] = BitConverter.GetBytes(dataAddress)[1];
            sendCommand[3] = BitConverter.GetBytes(dataAddress)[0];
            sendCommand[4] = BitConverter.GetBytes(dataCount)[1];
            sendCommand[5] = BitConverter.GetBytes(dataCount)[0];
            sendCommand[6] = BitConverter.GetBytes(crc16)[1];
            sendCommand[7] = BitConverter.GetBytes(crc16)[0];

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

                _serialPort.Write(command, 0, command.Length);

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
                    _receiveData = new byte[size];

                    _serialPort.Read(_receiveData, 0, size);

                    if (_receiveData[0] != UnitAddress || _receiveData.Length < 4)
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