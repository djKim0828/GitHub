using EmWorks.Lib.Common;
using MicroShakeConfiguratorLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Script.Serialization;

namespace EmWorks.App.EmIssoftLMK6
{
    public class EmIssoftMicroshake
    {
        #region Fields

        public const string MicroShakePath = "C:\\TechnoTeam\\MicroShakeConfigurator\\bin";
        private static EmIssoftMicroshake thisMicroshakeItem = new EmIssoftMicroshake();

        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();

        private MicroShakeConfigurator _microshake;

        private Process[] _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");

        private ReciveMethod _returnData = new ReciveMethod();

        #endregion Fields

        #region Enums

        public enum ShakeMode
        {
            FixedPath = 0,
            Random = 1,
        }

        #endregion Enums

        #region Methods

        public bool DriveIsReady()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.DriveIsReady();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool DriveReference()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.DriveReference();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool FixedPathIsReady()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.FixedPathIsReady();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetLastError()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.GetLastError();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetLastErrorText(string result)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            result = _microshake.GetLastErrorText();

            if (String.IsNullOrEmpty(result) == true)
            {
                ret = -1;
            }//else

            var param = new { result };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetPosition(double zpos, double xpos)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.GetPos(ref zpos, ref xpos);

            var param = new { zpos, xpos };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetShakeFactors(double zfact, double xfact)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.GetShakeFactors(ref zfact, ref xfact);

            var param = new { zfact, xfact };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetStartPosition(double zpos, double xpos)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.GetStartPosition(ref zpos, ref xpos);

            var param = new { zpos, xpos };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool HideDialog()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.HideDialog();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool InitMicroshake()
        {
            int ret = 0;
            
            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            _microshake = new MicroShakeConfigurator();

            _microshake.Init();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool LoadCapFile(string filename)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.LoadCapFile(filename);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool LoadConfig(string filename)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.LoadConfig(filename);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool LoadDriveConfig(string filename)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.LoadDriveConfig(filename);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool LoadFixedPathFile(string filename)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.LoadFixedPathFile(filename);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool MoveTo(double zpos, double xpos)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.MoveTo(zpos, xpos);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool PixelPitchIsReady()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.PixelPitchIsReady();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ProgramClose(int Question)
        {
            _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");

            for (int i = 0; i < _microshakeProcess.Length; i++)
            {
                try
                {
                    _microshakeProcess[i].Kill();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    return false;
                }
            }

            return true;
        }

        public bool ProgramOpen()
        {
            _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");

            if (_microshakeProcess.Length > 0)
            {
                for (int i = 0; i < _microshakeProcess.Length; i++)
                {
                    try
                    {
                        _microshakeProcess[i].Kill();
                    }
                    catch (Exception ex)
                    {
                        Logger.Exception(ex);

                        return false;
                    }
                }
            }//else

            Process.Start(MicroShakePath + "\\MicroShakeConfigurator.exe");

            _microshake = new MicroShakeConfigurator();

            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SendCommend(ReciveMethod sendMethod)
        {
            try
            {
                string jsonString = _jsonConvertor.Serialize(sendMethod);

                ProcessMain.ReturnMainQueue.Enqueue(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            return true;
        }

        public bool SetShakeFactors(double zfact, double xfact)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.SetShakeFactors(zfact, xfact);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SetShakeMode(ShakeMode mode)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.SetShakeMode((int)mode);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SetStartPosition(double zpos, double xpos)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.SetStartPosition(zpos, xpos);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ShowDialog()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.ShowDialog();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool StartFunc(string funcname, List<MethodParameter> parameter)
        {
            object returnValue;

            Type t = this.GetType();
            MethodInfo method = t.GetMethod(funcname);

            if (parameter.Count > 0)
            {
                object[] parameters = new object[parameter.Count];

                for (int i = 0; i < parameter.Count; i++)
                {
                    parameters[i] = Convert(parameter[i]._Value, method.GetParameters()[i].ParameterType);
                }

                returnValue = method.Invoke(thisMicroshakeItem, parameters);
            }
            else
            {
                returnValue = method.Invoke(thisMicroshakeItem, null);
            }

            if (returnValue.GetType() == typeof(bool))
            {
                return (bool)returnValue;
            }//else

            return true;
        }

        public bool StartMoving()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.StartMoving();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool StopMoving()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _microshake.StopMoving();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
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

        private bool IsOpen()
        {
            _microshakeProcess = Process.GetProcessesByName("MicroShakeConfigurator");

            if (_microshakeProcess.Length < 0)
            {
                return false;
            }//else

            return true;
        }

        private bool ParsingData(MethodBase thisMethod, int ret, object param)
        {
            Type t = this.GetType();
            MethodInfo method = t.GetMethod(thisMethod.Name);

            try
            {
                _returnData._Classname = this.GetType().Name;
                _returnData._Funcname = thisMethod.Name.ToString();
                _returnData._Parameter = new List<MethodParameter>();

                for (int i = 0; i < param.GetType().GetProperties().Length; i++)
                {
                    _returnData._Parameter.Add(new MethodParameter()
                    {
                        _Name = thisMethod.GetParameters()[i].Name,
                        _Value = param.GetType().GetProperties()[i].GetValue(param),
                        _ValueType = method.GetParameters()[i].ParameterType.ToString(),
                    });
                }

                if (ret == 0)
                {
                    _returnData._FunctionResult = true;
                }
                else
                {
                    _returnData._FunctionResult = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            if (SendCommend(_returnData) != true)
            {
                Logger.Debug("Error> Server Return Data");

                return false;
            }

            return true;
        }

        #endregion Methods
    }
}