using EmWorks.Lib.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.App.EmIssoftLMK6
{
    public class MethodParameter
    {
        #region Fields

        public string _Name;
        public object _Value;
        public string _ValueType;

        #endregion Fields
    }

    public class ProcessMain
    {
        #region Fields

        public static ConcurrentQueue<string> ReciveMainQueue = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> ReturnMainQueue = new ConcurrentQueue<string>();

        public COMCommend _Command;
        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();
        private EmIssoftLMK6 _lmk6 = new EmIssoftLMK6();
        private EmIssoftMicroshake _microshake = new EmIssoftMicroshake();
        private Thread _processThread;
        private EmIssoftServer _server = new EmIssoftServer();
        private bool _threadExit = false;

        #endregion Fields

        #region Constructors

        public ProcessMain()
        {
            _processThread = new Thread(ExcuteThread) { IsBackground = true };
            _processThread.Start();
        }

        #endregion Constructors

        #region Destructors

        ~ProcessMain()
        {
            _microshake.ProgramClose(0);
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

        #region Methods

        public void Close()
        {
            _server.Close();
            _threadExit = true;
        }

        private void ExcuteThread()
        {
            string command = string.Empty;
            while (_threadExit == false)
            {
                if (ReciveMainQueue.IsEmpty == true && ReturnMainQueue.IsEmpty == true)
                {
                    continue;
                }//else

                command = string.Empty;
                if (ReciveMainQueue.IsEmpty == false)
                {
                    if (ReciveMainQueue.TryDequeue(out command))
                    {
                        SplitCommend(command);
                    }//else
                }//else

                command = string.Empty;
                if (ReturnMainQueue.IsEmpty == false)
                {
                    if (ReturnMainQueue.TryDequeue(out command))
                    {
                        _server.SendCommend(command);
                    }//else
                }//else

                Thread.Sleep(1);
            }
        }

        private void SplitCommend(string Command)
        {
            var command = _jsonConvertor.Deserialize<ReciveMethod>(Command);

            try
            {
                _Command = (COMCommend)Enum.Parse(typeof(COMCommend), command._Classname);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return;
            }

            switch (_Command)
            {
                case COMCommend.None:
                    break;

                case COMCommend.EmIssoftLMK6:
                    _lmk6.StartFunc(command._Funcname, command._Parameter);
                    break;

                case COMCommend.EmIssoftMicroshake:
                    _microshake.StartFunc(command._Funcname, command._Parameter);
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }

    public class ReciveMethod
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
            var returnValue = new ReciveMethod();
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
}