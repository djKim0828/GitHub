using System;
using System.IO.Ports;
using System.Text;

namespace StEm
{
    public class SerialControl : Device
    {
        #region Fields

        public string _logMessage;
        private SerialPort _SerialPort;

        #endregion Fields

        #region Constructors

        public SerialControl()
        {
            _SerialPort = new SerialPort();
        }

        #endregion Constructors

        #region Destructors

        ~SerialControl()
        {
            // Todo : 종료시 Close 강제 실행
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventLogMessageHandler(string message);

        public delegate void EventReceiveMessageHandler(string message);

        #endregion Delegates

        #region Events

        public event EventLogMessageHandler LogMessage;

        public event EventReceiveMessageHandler ReceiveMessage;

        #endregion Events

        #region Methods

        public override int Close()
        {
            _SerialPort.Close();
            return result.True;
        }

        public override int Open(string port, string baudRate, string newLine)
        {
            try
            {
                _SerialPort.PortName = port;
                _SerialPort.BaudRate = Convert.ToInt16(baudRate);

                if (newLine != string.Empty)
                {
                    _SerialPort.NewLine = newLine;
                } // else

                
                _SerialPort.Open();

                _SerialPort.DataReceived += _SerialPort_DataReceived;

                LogMessage("Open completed");
                _logMessage = "Open completed3";

                return result.True;
            }
            catch (System.Exception ex)
            {
                LogMessage(ex.Message.ToString());
                return result.False;
            }
        }

        public override bool Send(string data, bool isHex)
        {                        
            if (isHex == false)
            {
                _SerialPort.Write(data);

                // 로그 출력
                LogMessage("Send Data : " + data);
            }
            else
            {
                if (data.Length % 2 > 0 )
                {
                    LogMessage("HEX로 전송할 데이터를 확인하세요.(2배수 길이) - " + data);
                    return false;
                } // else


                int length = data.Length / 2;
                byte[] sendData = new byte[length];
                
                for (int i=0;i<length;i++)
                {
                    sendData[i] = Convert.ToByte(data.Substring(i*2, 2), 16);
                }

                _SerialPort.Write(sendData, 0, sendData.Length);

                // 로그 출력
                string debugData = ConvertByteToStringByte(sendData);
                LogMessage("Send Data : " + debugData);

            }
            

            return true;
        }

        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sendSerial = (SerialPort)sender;                        

            //retrieve number of bytes in the buffer
            int bytes = sendSerial.BytesToRead;
            //create a byte array to hold the awaiting data
            byte[] comBuffer = new byte[bytes];
            //read the data and store it
            sendSerial.Read(comBuffer, 0, bytes);

            // Byte를 문자로 변경
            ASCIIEncoding ascii = new ASCIIEncoding();
            String ReceivedData = ascii.GetString(comBuffer).Replace("\0", "");

            string debugData = ConvertByteToStringByte(comBuffer);

            LogMessage("Receive Data : " + ReceivedData + " [ Hex : " + debugData + "]");            

            // 이벤트 호출
            ReceiveMessage(ReceivedData + "/" + debugData);
        }

        private string ConvertByteToStringByte(byte[] comBuffer)
        {
            string returnData = string.Empty;

            try
            {
                for (int i = 0; i < comBuffer.Length; i++)
                {
                    returnData = returnData + string.Format("{0:X2} ", comBuffer[i]);
                }

                return returnData;
            }
            catch (System.Exception ex)
            {
                LogMessage(ex.ToString());
                return string.Empty;
            }
            
        }

        #endregion Methods

        #region Classes

        public class result
        {
            #region Fields

            public const int False = 0;
            public const int True = 1;

            #endregion Fields
        }

        #endregion Classes
    }
}