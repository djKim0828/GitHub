using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace TCPClitentTest
{
    public class EmTCPClient
    {
        TcpClient _cl;

        public bool _isConnent;

        private Thread _objThreadListening;
        private bool _threadExit = false;

        public delegate void EventReceiveMessage(object sendor, EventReceiveMessageArgs e);
        public event EventReceiveMessage ReceiveMessage;

        public EmTCPClient()
        {
            _isConnent = false;
        }

        public bool Open(string ip, int port)
        {
            try
            {
                _cl = new TcpClient();

                _cl.Connect(ip, 20000);

                if (null == _objThreadListening)
                {
                    _threadExit = false;
                    _objThreadListening = new Thread(ListenThread);
                    _objThreadListening.Start();
                }

                _isConnent = true;

                return true;
            }
            catch (System.Exception ex)
            {
                _isConnent = false;
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                _threadExit = true;

                _cl.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void ListenThread()
        {
            try
            {
                while (true)
                {

                    if (_threadExit == true)
                    {
                        return;
                    }

                    NetworkStream nwStream = _cl.GetStream();

                    byte[] buffer = new byte[_cl.ReceiveBufferSize];

                    int bytesRead = nwStream.Read(buffer, 0, _cl.ReceiveBufferSize);

                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    ReceiveMessage(this, new EventReceiveMessageArgs("", 0, 0, dataReceived.Trim()));


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void Send(string meg)
        {
            byte[] byteData = Encoding.Default.GetBytes(meg);

            if (_isConnent == true)
            {
                _cl.Client.Send(byteData);
            }            
        }

        public class EventReceiveMessageArgs : EventArgs
        {
            #region Constructors

            public EventReceiveMessageArgs(string ipAddress, int port, int clientId, string message)
            {
                Initialize();

                IpAddress = ipAddress;
                Port = port;

                ClientId = clientId;
                Message = message;
            }

            #endregion Constructors

            #region Properties

            public bool IsConnect;
            public int ClientId { get; protected set; }
            public string Message { get; protected set; }
            public DateTime OccurredTime { get; protected set; }
            public string IpAddress { get; protected set; }
            public int Port { get; protected set; }


            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }
    }
}
