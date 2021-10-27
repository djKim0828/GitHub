using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EmWorks.DevDrv.IssoftLMK6
{
    public class EmIssoftMicroshake : DriverBase
    {
        #region Fields

        public static ConcurrentQueue<MethodData> ReciveMicroshakeQueue = new ConcurrentQueue<MethodData>();
        private bool _isOpenMicroshake;
        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();
        private Process[] _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");
        private MethodData _reciveMethod;
        private Dictionary<string, object> _refData;
        private object[] _returnParam;
        private MethodData _sendMethod = new MethodData();
        private TimeSpan _timeout = new TimeSpan(0, 0, 10);

        #endregion Fields

        #region Constructors

        public EmIssoftMicroshake(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Events

        public event EventValueChanged _ChangedConnect;

        #endregion Events

        #region Enums

        public enum ShakeMode
        {
            FixedPath = 0,
            Random = 1,
        }

        #endregion Enums

        #region Properties

        public bool _IsRun { get; private set; }

        public bool IsOpenMicroshake
        {
            get
            {
                return this._isOpenMicroshake;
            }
            set
            {
                if (_isOpenMicroshake != value)
                {
                    _isOpenMicroshake = value;
                    _ChangedConnect?.Invoke();
                } // else
            }
        }

        #endregion Properties

        #region Methods

        public override bool Close()
        {
            if (ProgramClose() != true)
            {
                return false;
            }//else

            return true;
        }

        public bool DriveIsReady()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool DriveReference()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool FixedPathIsReady()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool GetLastError()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public Dictionary<string, object> GetLastErrorText(string errorstring)
        {
            if (IsOpen() != true)
            {
                errorstring = "Open Error";
                return null;
            }//else

            var param = new { errorstring };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetPosition(double zpos, double xpos)
        {
            if (IsOpen() != true)
            {
                return null;
            }//else

            var param = new { zpos, xpos };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetShakeFactors(double zfact, double xfact)
        {
            if (IsOpen() != true)
            {
                return null;
            }//else

            var param = new { zfact, xfact };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetStartPosition(double zpos, double xpos)
        {
            if (IsOpen() != true)
            {
                return null;
            }//else

            var param = new { zpos, xpos };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool HideDialog()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool InitMicroshake()
        {
            if (IsOpen() == true)
            {
                return true;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool IsOpen()
        {
            _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");

            if (_microshakeProcess.Length <= 0)
            {
                this.IsOpenMicroshake = false;

                return false;
            }//else

            this.IsOpenMicroshake = true;

            return true;
        }

        public bool LoadCapFile(string filename)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { filename };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool LoadConfig(string filename)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { filename };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool LoadDriveConfig(string filename)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { filename };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool LoadFixedPathFile(string filename)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { filename };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool MoveTo(double zpos, double xpos)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { zpos, xpos };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public override bool Open()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
            return true;
        }

        public bool PixelPitchIsReady()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ProgramClose()
        {
            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ProgramOpen()
        {
            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetShakeFactors(double zfact, double xfact)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { zfact, xfact };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetShakeMode(ShakeMode mode)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { mode };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetStartPosition(double zpos, double xpos)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { zpos, xpos };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ShowDialog()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool StartMoving()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            if (ret.Result == true)
            {
                _IsRun = true;
            }
            else
            {
                _IsRun = false;
            }

            return ret.Result;
        }

        public bool StopMoving()
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            if (ret.Result == true)
            {
                _IsRun = false;
            }
            else
            {
                _IsRun = true;
            }

            return ret.Result;
        }

        private dynamic Convert(dynamic source, Type dest)
        {
            dynamic returnValue;

            try
            {
                if (dest == typeof(bool))
                {
                    int intValue = System.Convert.ToInt32(source);
                    returnValue = System.Convert.ToBoolean(intValue);

                    return returnValue;
                }//else

                returnValue = System.Convert.ChangeType(source, dest);
            }
            catch
            {
                if (dest.IsEnum == true && Enum.IsDefined(dest, source))
                {
                    returnValue = Enum.Parse(dest, source, true);
                }
                else
                {
                    returnValue = null;
                }
            }

            return returnValue;
        }

        private bool GetThisMethod(MethodBase thisMethod, object param)
        {
            try
            {
                _sendMethod._Classname = this.GetType().Name;
                _sendMethod._Funcname = thisMethod.Name.ToString();
                _sendMethod._Parameter = new List<MethodParameter>();

                for (int i = 0; i < thisMethod.GetParameters().Count(); i++)
                {
                    _sendMethod._Parameter.Add(new MethodParameter()
                    {
                        _Name = thisMethod.GetParameters()[i].Name,
                        _Value = param.GetType().GetProperties()[i].GetValue(param),
                        _ValueType = thisMethod.GetParameters()[i].GetType().FullName
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private bool ReciveCheck()
        {
            MethodData commands;
            _reciveMethod = new MethodData();
            _refData = new Dictionary<string, object>();

            DateTime now = DateTime.Now;

            while (true) //Todo 시간 받아 올수 있도록 수정...
            {
                if (DateTime.Now.Subtract(now) > _timeout || EmIssoftLMK6.lmkclient.IsConnect != true)
                {
                    Logger.Error("LMK Server Timeout");

                    return false;
                }//else

                Thread.Sleep(1);
                if (ReciveMicroshakeQueue.IsEmpty)
                {
                    continue;
                }//else

                if (ReciveMicroshakeQueue.TryDequeue(out commands))
                {
                    _reciveMethod = commands;

                    break;
                }//else
            }

            if (_reciveMethod._Parameter.Count > 0)
            {
                _returnParam = new object[_reciveMethod._Parameter.Count];

                for (int i = 0; i < _reciveMethod._Parameter.Count; i++)
                {
                    _returnParam[i] = Convert(_reciveMethod._Parameter[i]._Value, Type.GetType(_reciveMethod._Parameter[i]._ValueType));

                    _refData.Add(_reciveMethod._Parameter[i]._Name, _returnParam[i]);
                }
            }//else

            if (_reciveMethod == null || _reciveMethod._FunctionResult != true)
            {
                return false;
            }//else

            return true;
        }

        private bool SendCommend(MethodData sendMethod)
        {
            try
            {
                string jsonString = _jsonConvertor.Serialize(sendMethod);

                EmIssoftClient.SendMainQueue.Enqueue(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            return true;
        }

        private bool Startasync(MethodBase thisMethod, object param)
        {
            bool ret = true;

            ret = GetThisMethod(thisMethod, param);
            if (ret != true)
            {
                return false;
            }//else

            ret = SendCommend(_sendMethod);
            if (ret != true)
            {
                return false;
            }//else

            ret = ReciveCheck();
            if (ret != true)
            {
                return false;
            }//else

            return true;
        }

        #endregion Methods
    }
}