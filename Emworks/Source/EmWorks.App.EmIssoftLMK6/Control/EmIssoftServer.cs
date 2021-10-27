using EmWorks.Lib.Common;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EmWorks.App.EmIssoftLMK6
{
    public class EmIssoftServer
    {
        #region Fields

        private static TcpClient _tcpClient;
        private static TcpListener _tcpServer;

        #endregion Fields

        #region Constructors

        public EmIssoftServer()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~EmIssoftServer()
        {
        }

        #endregion Destructors

        #region Methods

        public void Close()
        {
            _tcpServer.Server.Close();
        }

        public virtual bool Initialize()
        {
            _tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 15000);
            _tcpServer.Start();

            Task.Factory.StartNew(AcceptClient);

            return true;
        }

        public bool SendCommend(string commend, object o = null)
        {
            if (_tcpClient == null)
            {
                return false;
            }//else

            if (commend.IndexOf("<") > 0)
            {
                commend = commend.Substring(1);
            }//else

            if (o == null)
            {
                NetworkStream ns;

                try
                {
                    ns = _tcpClient.GetStream();
                    StreamWriter sw = new StreamWriter(ns);

                    sw.WriteLine(commend);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    try
                    {
                        _tcpClient.Dispose();
                    }
                    catch (Exception exp)
                    {
                        Logger.Exception(exp);
                    }
                }
            }
            else if (o != null)
            {
                TcpClient tc = (TcpClient)o;
                NetworkStream ns;

                try
                {
                    ns = tc.GetStream();
                    StreamWriter sw = new StreamWriter(ns);

                    sw.WriteLine(commend);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    try
                    {
                        _tcpClient.Dispose();
                    }
                    catch (Exception exp)
                    {
                        Logger.Exception(exp);
                    }
                }
            }//else

            return true;
        }

        private async Task AcceptClient()
        {
            while (true)
            {
                _tcpClient = await _tcpServer.AcceptTcpClientAsync().ConfigureAwait(false);
                new Thread(() => { BroadcastData(); }) { IsBackground = true }.Start();

                Thread.Sleep(10);//CPU 점유율 안정화
            }
        }

        private void BroadcastData()
        {
            NetworkStream ns = _tcpClient.GetStream();
            StreamReader sr = new StreamReader(ns);
            string commend;

            while (true)
            {
                commend = string.Empty;
                try
                {
                    commend = sr.ReadLine();
                    sr.DiscardBufferedData();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    break;
                }

                if (String.IsNullOrEmpty(commend) == false)
                {
                    //SendCommend(commend, _tcpClient);

                    ProcessMain.ReciveMainQueue.Enqueue(commend);
                }//else

                Thread.Sleep(10); //CPU 점유율 안정화
            }
        }

        #endregion Methods
    }
}