using EmWorks.Lib.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EmWorks.DevDrv.IssoftLMK6
{
    public class EmIssoftClient
    {
        #region Fields

        public static ConcurrentQueue<string> ReciveMainQueue = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> SendMainQueue = new ConcurrentQueue<string>();

        public COMCommend _Commend;
        private Thread _clientReciveThread;
        private Thread _clientSendThread;
        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();

        private TcpClient _tcpClient;

        #endregion Fields

        #region Constructors

        public EmIssoftClient()
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmIssoftClient()
        {
            if (_clientReciveThread != null)
            {
                _clientReciveThread.Abort();
            }
        }

        #endregion Destructors

        #region Enums

        public enum COMCommend
        {
            None,
            EmIssoftLMK6,
            EmIssoftMicroshake,
        }

        #endregion Enums

        #region Properties

        public bool IsConnect { get; protected set; }

        #endregion Properties

        #region Methods

        public void ClientData()
        {
            while (true)
            {
                if (SendMainQueue.IsEmpty && ReciveMainQueue.IsEmpty)
                {
                    continue;
                }//else

                string commend = string.Empty;

                if (SendMainQueue.IsEmpty != true)
                {
                    while (SendMainQueue.TryDequeue(out commend) == false)
                    {
                        Thread.Sleep(1); //코드 안정화
                    }

                    SendCommend(commend);
                }//else

                commend = string.Empty;

                if (ReciveMainQueue.IsEmpty != true)
                {
                    while (ReciveMainQueue.TryDequeue(out commend) == false)
                    {
                        Thread.Sleep(1); //코드 안정화
                    }

                    SplitCommend(commend);
                }//else
            }
        }

        public bool Open()
        {
            IsConnect = false;
            _clientReciveThread = new Thread(ClientReciveData) { IsBackground = true };
            _clientSendThread = new Thread(ClientData) { IsBackground = true };

            var ret = ClientConnect();
            if (ret.Result != true)
            {
                return false;
            }//else

            return true;
        }

        public void SendCommend(string commend)
        {
            if (IsConnect == false)
            {
                return;
            }//else

            NetworkStream ns;

            try
            {
                ns = _tcpClient.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(commend);

                Thread.Sleep(100); // Flush 전 안정화 시간

                sw.FlushAsync();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                IsConnect = false;
                try
                {
                    _tcpClient.Dispose();
                    _tcpClient = null;
                }
                catch (Exception exp)
                {
                    Logger.Exception(exp);
                }

                EmIssoftLMK6.Initial();

                return;
            }
        }

        public void SplitCommend(string reciveCommend)
        {
            var commend = _jsonConvertor.Deserialize<MethodData>(reciveCommend);

            try
            {
                _Commend = (COMCommend)Enum.Parse(typeof(COMCommend), commend._Classname);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return;
            }

            switch (_Commend)
            {
                case COMCommend.None:
                    break;

                case COMCommend.EmIssoftLMK6:
                    EmIssoftLMK6.ReciveLMKQueue.Enqueue(commend);
                    break;

                case COMCommend.EmIssoftMicroshake:
                    EmIssoftMicroshake.ReciveMicroshakeQueue.Enqueue(commend);
                    break;

                default:
                    break;
            }
        }

        private async Task<bool> ClientConnect()
        {
            var ConnectTask = Task.Run(() =>
            {
                string clientIp = "127.0.0.1";
                int clientPort = 15000;
                int clientTimeout = 1000;

                try
                {
                    _tcpClient = new TcpClient(clientIp, clientPort);
                    _tcpClient.SendTimeout = clientTimeout;
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                }
            });

            int taskDelay = 10000;

            var TimeoutTask = Task.Delay(taskDelay);
            var doneTask = await Task.WhenAny(ConnectTask, TimeoutTask).ConfigureAwait(false);

            if (doneTask == TimeoutTask)
            {
                try
                {
                    ConnectTask.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                }

                IsConnect = false;

                return false;
            }//else

            if (_tcpClient.Connected)
            {
                IsConnect = true;
                _clientReciveThread.Start();
                _clientSendThread.Start();
            }//else

            return true;
        }

        private void ClientReciveData()
        {
            NetworkStream ns = _tcpClient.GetStream();
            StreamReader sr = new StreamReader(ns);

            while (_tcpClient.Connected)
            {
                string commend = string.Empty;

                try
                {
                    commend = sr.ReadLine();
                }
                catch
                {
                    break;
                }

                if (String.IsNullOrEmpty(commend) == false)
                {
                    ReciveMainQueue.Enqueue(commend);
                }//else
            }
        }

        #endregion Methods
    }

    public class MethodData
    {
        #region Fields

        public string _Classname;
        public string _Funcname;
        public bool _FunctionResult;
        public List<MethodParameter> _Parameter;

        #endregion Fields

        #region Methods

        public object Clone()
        {
            var returnValue = new MethodData();
            returnValue._Classname = this._Classname;
            returnValue._Funcname = this._Funcname;
            returnValue._FunctionResult = this._FunctionResult;
            returnValue._Parameter = new List<MethodParameter>();

            foreach (var item in this._Parameter)
            {
                returnValue._Parameter.Add(new MethodParameter() { _Name = item._Name, _Value = item._Value, _ValueType = item._ValueType });
            }
            return returnValue;
        }

        #endregion Methods
    }

    public class MethodParameter
    {
        #region Fields

        public string _Name;
        public object _Value;
        public string _ValueType;

        #endregion Fields
    }
}