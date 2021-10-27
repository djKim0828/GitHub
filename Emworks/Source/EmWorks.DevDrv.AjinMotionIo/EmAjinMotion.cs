using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public partial class EmAjinMotion : MotionBase
    {
        #region Fields

        public const double DoubleMinValue = double.MinValue;
        public const string PositionDefaultName = "Default";
        internal string _extenalLibVersion = string.Empty;
        internal string _version = string.Empty;
        private Dictionary<string, AxisConfigModel> _axisConfigName = new Dictionary<string, AxisConfigModel>();

        private int _axisCount = int.MinValue;

        private Dictionary<string, AxisPropertyModel> _axisName = new Dictionary<string, AxisPropertyModel>();

        private Dictionary<int, AxisPropertyModel> _axisNumber = new Dictionary<int, AxisPropertyModel>();
        private AjinMotionConfigModel _configMotion;
        private bool _isExsitMotionModule;
        private AjinMotionSim _motionSim;
        private Dictionary<int, EmModuleInfo> Modules;

        #endregion Fields

        #region Constructors

        public EmAjinMotion(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmAjinMotion()
        {
            IsUpdateLoop = false;
            Close();
        }

        #endregion Destructors

        #region Enums

        public enum AbsRelMode
        {
            Absolute,
            Relative
        }

        public enum HomeResult
        {
            HOME_SUCCESS = 0x01,
            HOME_SEARCHING = 0x02,
            HOME_ERR_GNT_RANGE = 0x10,
            HOME_ERR_USER_BREAK = 0x11,
            HOME_ERR_VELOCITY = 0x12,
            HOME_ERR_AMP_FAULT = 0x13,
            HOME_ERR_NEG_LIMIT = 0x14,
            HOME_ERR_POS_LIMIT = 0x15,
            HOME_ERR_NOT_DETECT = 0x16,
            HOME_ERR_UNKNOWN = 0xFF
        }

        public enum StopMode
        {
            EStop,
            SStop,
            StopEx
        }

        private enum AjinSignal
        {
            AjinFalse,
            AjinTrue
        }

        private enum AlarmReturnMode
        {
            Immediate,
            Blocking,
            NonBlocking
        }

        #endregion Enums

        #region Properties

        public Dictionary<string, AxisConfigModel> AxisConfigName
        {
            get
            {
                return _axisConfigName;
            }

            protected set
            {
                _axisConfigName = value;
            }
        }

        public override int AxisCount
        {
            get
            {
                if (_axisCount == int.MinValue)
                {
                    if (GetAxisCount(ref _axisCount) == false)
                    {
                        _axisCount = int.MinValue;
                    }//else
                }//else

                return _axisCount;
            }
            protected set
            {
                _axisCount = value;
            }
        }

        public Dictionary<string, AxisPropertyModel> AxisName
        {
            get
            {
                return _axisName;
            }

            protected set
            {
                _axisName = value;
            }
        }

        public Dictionary<int, AxisPropertyModel> AxisNumber
        {
            get
            {
                return _axisNumber;
            }

            protected set
            {
                _axisNumber = value;
            }
        }

        public AjinMotionConfigModel ConfigMotion
        {
            get
            {
                return _configMotion;
            }
        }

        public override string ExtenalLibVersion
        {
            get
            {
                return _extenalLibVersion;
            }
            protected set
            {
                _version = value;
            }
        }

        public override string Version
        {
            get
            {
                return _version;
            }
            protected set
            {
                _version = value;
            }
        }

        private bool IsExsitMotionModule
        {
            get
            {
                _isExsitMotionModule = GetExsitMotionModule();

                return _isExsitMotionModule;
            }
        }

        #endregion Properties

        #region Methods

        public override bool AlarmReset(int axisNo)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("AlarmReset - " + axisNo + "is not Contain AxisNumber", axisNo);
                return false;
            }//else

            return AlarmReset(_axisNumber[axisNo]);
        }

        public bool AlarmReset(string axisName)
        {
            if (AxisName.ContainsKey(axisName) != true)
            {
                Logger.Error("AlarmReset - " + axisName + "is not Contain AxisName", axisName);
                return false;
            }//else

            return AlarmReset(AxisName[axisName]);
        }

        public override bool Close()
        {
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            GetIsOpen();

            if (IsOpen == true)
            {
                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXL.AxlClose();   // success = 1, failure = 0 ?
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("Close Exception");
                    return false;
                }
            }//else

            GetIsOpen();

            bool result = !(IsOpen);

            Logger.Debug("close result = " + result);

            return result;
        }

        public bool GetMoveMode(int axisNo)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("GetMoveMode - " + axisNo + "is not Contain AxisNumber", axisNo);
                return false;
            }//else

            return GetMoveMode(_axisNumber[axisNo]);
        }

        public override bool Homming(int axisNo)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("Homming - " + axisNo + "is not Contain AxisNumber", axisNo);
                return false;
            }//else

            return Homming(_axisNumber[axisNo]);
        }

        public bool Homming(string axisName)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("Homming - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return Homming(_axisName[axisName]);
        }

        public void InitialConfig(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) == true)
            {
                _configMotion = new AjinMotionConfigModel();
            }
            else
            {
                _configMotion = new AjinMotionConfigModel(filePath);
            }
            _configMotion.Load(ref _configMotion);
            _configMotion.Save();

            if ((int)IsSimulation < (int)_configMotion.RunMode)
            {
                IsSimulation = _configMotion.RunMode;
            }//else
        }

        public bool MoveAbsolutePosition(string axisName, double position)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("MoveAbsolutePosition - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return MoveAbsolutePosition(_axisName[axisName], position);
        }

        public bool MoveModeSelect(int axisNo, AbsRelMode movePosition)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("MoveModeSelect - " + axisNo + "is not Contain AxisNumber", axisNo);

                return false;
            }//else

            return MoveModeSelect(_axisNumber[axisNo], movePosition);
        }

        public bool MoveModeSelect(string axisName, AbsRelMode movePosition)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("MoveModeSelect - " + axisName + "is not Contain AxisNumber", axisName);

                return false;
            }//else

            return MoveModeSelect(_axisName[axisName], movePosition);
        }

        public bool MoveRelativeValue(string axisName, double position)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("MoveRelativePosition - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return MoveRelativeValue(_axisName[axisName], position);
        }

        public override bool MoveStart(int axisNo, string position)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("MoveStart - " + axisNo + "is not Contain AxisNumber", axisNo);

                return false;
            }//else

            return MoveStart(_axisNumber[axisNo], position);
        }

        public bool MoveStart(int axisNo, string position, bool skipChangeMoveMode = false)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("MoveStart - " + axisNo + "is not Contain AxisNumber", axisNo);

                return false;
            }//else

            return MoveStart(_axisNumber[axisNo], position, skipChangeMoveMode);
        }

        public bool MoveStart(string axisName, string position, bool skipChangeMoveMode = false)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("MoveStart - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return MoveStart(_axisName[axisName], position, skipChangeMoveMode);
        }

        public override bool Open()
        {
            try
            {
                GetIsOpen();
                if (IsOpen == true)
                {
                    // 이미 Open되어 있음.
                    Logger.Infomation("It's already open.");
                }
                else
                {
                    if (OpenLib() != true)
                    {
                        IsOpen = false;
                        return false;
                    }//else
                }

                if (_axisName.Count > 0)
                {
                    foreach (var item in _axisName)
                    {
                        RegistAxisEvent(item.Value, false);
                    }
                    _axisName.Clear();
                    _axisNumber.Clear();
                }//else

                IsOpen = true;

                if (_configMotion.AllAxis == null)
                {
                    _configMotion.AllAxis = _axisConfigName;
                }
                else
                {
                    _axisConfigName = _configMotion.AllAxis;
                }

                // Opne 성공
                if (initModuleInfo() != true)
                {
                    IsOpen = false;
                    Logger.Error("Ajin Fail to initModuleInfo");

                    return IsOpen;
                }//else

                // Todo : 실제 테스트시 확인 필요
                if (LoadMot() != true)
                {
                    IsOpen = false;
                    Logger.Error("Ajin Fail to LoadMot");

                    return IsOpen;
                }//else

                if (SetMotionDefaultValue() != true)
                {
                    IsOpen = false;
                    Logger.Error("Ajin Fail to SetAxisDefaultValue");
                    return IsOpen;
                }//else

                StartUpdateProc();

                IsOpen = true;

                Logger.Infomation("Ajin Open and Start updateProc");
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            return IsOpen;
        }

        public override bool ServoOn(int axisNo, bool servoOn)
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("ServoOn - " + axisNo + "is not Contain AxisNumber", axisNo);

                return false;
            }//else

            return ServoOn(_axisNumber[axisNo], servoOn);
        }

        public bool ServoOn(string axisName, bool servoOn)
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("ServoOn - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return ServoOn(_axisName[axisName], servoOn);
        }

        public bool SetMotionDefaultValue()
        {
            if (IsOpen != true)
            {
                Logger.Error("Lib is Not Open");

                return false;
            }//else

            if (_axisConfigName.Count != AxisCount)
            {
                Logger.Error("AxisCount and MotionMouduleCount are Different");

                return false;
            }//else

            bool result = true;

            foreach (var item in _axisConfigName)
            {
                if (CheckAxisHommingVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (SetAxisHommingVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (CheckAxisSignalVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (SetAxisSignalVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (CheckAxisDefaultVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (SetAxisDefaultVelue(item.Value) != true)
                {
                    result = false;
                }//else

                if (SetAxisDefaultPosition(item.Value) != true)
                {
                    result = false;
                }//else
            }

            ConfigMotion.Save();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="stopMode">기본값 = "" = StopMode.SStop</param>
        /// <returns></returns>
        public override bool Stop(int axisNo, string stopMode = "")
        {
            if (_axisNumber.ContainsKey(axisNo) != true)
            {
                Logger.Error("Stop - " + axisNo + "is not Contain AxisNumber", axisNo);

                return false;
            }//else

            return Stop(_axisNumber[axisNo], stopMode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="stopMode">기본값 = "" = StopMode.SStop</param>
        /// <returns></returns>
        public bool Stop(string axisName, string stopMode = "")
        {
            if (_axisName.ContainsKey(axisName) != true)
            {
                Logger.Error("Stop - " + axisName + "is not Contain AxisName", axisName);

                return false;
            }//else

            return Stop(_axisName[axisName], stopMode);
        }

        protected void DoUpdate()
        {
            foreach (var item in _axisName)
            {
                GetServoAlarmSignal(item.Value);

                GetServoOn(item.Value);

                GetMoveMode(item.Value);

                GetBusySignal(item.Value);

                GetActualPosition(item.Value);

                GetCommandPosisiton(item.Value);

                GetInPositionSignal(item.Value);

                GetLimitSignal(item.Value);

                GetHomeSignal(item.Value);
            }
        }

        protected override void Initialize()
        {
            _motionSim = new AjinMotionSim(this);

            InitInstance();
            RegistEvent();

            Logger.Debug("ExtenalLibVersion", ExtenalLibVersion);
            Logger.Debug("Version", Version);
        }

        protected override void InitInstance()
        {
            GetExtenalLibVersion();
            LoopInterval = 1;
        }

        protected override void RegistEvent()
        {
            //
        }

        protected override void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }

        protected override void ThreadUpdateProc(object threadContext)
        {
            while (IsUpdateLoop)
            {
                if (IsSimulation != Define.RunMode.Real)
                {
                    break;
                }//else

                if (IsOpen)
                {
                    DoUpdate();
                    Thread.Sleep(LoopInterval);
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private void AddModule(int index, EmModuleInfo moduleInfo)
        {
            if (Modules == null)
            {
                Modules = new Dictionary<int, EmModuleInfo>();
            }// else

            if (Modules.ContainsKey(index) == false)
            {
                Modules.Add(index, moduleInfo);
            }// else
        }

        private void AddMotionProperty(int index)
        {
            if (_axisNumber == null)
            {
                _axisNumber = new Dictionary<int, AxisPropertyModel>();
            } //else

            if (_axisNumber.ContainsKey(index) == false)
            {
                string Name = AxisConfigName.FirstOrDefault(x => x.Value.AxisNumber == index).Key;

                AxisPropertyModel temp = new AxisPropertyModel(Name, index);

                RegistAxisEvent(temp, true);

                _axisName.Add(Name, temp);
                _axisNumber.Add(index, temp);
            }// else
        }

        private bool AlarmReset(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.AlarmReset(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalServoAlarmReset(axis.AxisNumber, (uint)AjinSignal.AjinTrue);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AlarmReset Exception - ", axis.AxisNumber);
                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("AlarmReset - ", funcRt);

                return false;
            }//else

            return true;
        }

        private void Axis_OnAlarm(object sender, EventArgs Args)
        {
            string axisName = ((EventVelueChangeArgs)Args).Name;
            bool? axisState = ((EventVelueChangeArgs)Args).BoolenValue;

            if (axisState == true)
            {
                GetAlarmCode(_axisName[axisName].AxisNumber);
            }
            else if (axisState == false)
            {
                _axisName[axisName].AlarmCode.Clear();
            }
            else
            {
                Logger.Error("Axis_OnAlarm value null");
            }
        }

        private void Axis_OnServoOn(object sender, EventArgs Args)
        {
            string axisName = ((EventVelueChangeArgs)Args).Name;
            if ((_axisName[axisName].ServoOn == false) && (_axisName[axisName].AbsoluteType == false))
            {
                _axisName[axisName].Orign = false;
            }//else
        }

        private bool CheckAbsRelMode(int axisNumber, AbsRelMode mode, int timeout = 2000)
        {
            if (_axisNumber[axisNumber].MoveMode == mode)
            {
                return true;
            }//else

            DateTime startTime = DateTime.Now;
            DateTime limiteTime = startTime.AddMilliseconds(timeout);
            bool result = false;

            while (DateTime.Now < limiteTime)
            {
                if (_axisNumber[axisNumber].MoveMode == mode)
                {
                    result = true;
                    break;
                }//else

                Thread.Sleep(LoopInterval);
            }
            return result;
        }

        private bool CheckAxisDefaultVelue(AxisConfigModel axis)
        {
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.CheckAxisDefaultVelue(axis);
            } // else

            if (axis.CmdPos == double.MaxValue || axis.Velocity == double.MaxValue || axis.Accel == double.MaxValue || axis.Decel == double.MaxValue)
            {
                double cmdPos = 0;
                double Velocity = 0;
                double Accel = 0;
                double Decel = 0;

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotGetParaLoad(axis.AxisNumber, ref cmdPos, ref Velocity, ref Accel, ref Decel);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmMotGetParaLoad Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmMotGetParaLoad Fail", axis);
                    return false;
                }//else

                axis.CmdPos = cmdPos;
                axis.Velocity = Velocity;
                axis.Accel = Accel;
                axis.Decel = Decel;
            }//else

            if (axis.MinVel == double.MaxValue || axis.MaxVel == double.MaxValue)
            {
                double MinVel = 0;
                double MaxVel = 0;

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotGetMinVel(axis.AxisNumber, ref MinVel);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmMotGetMinVel Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmMotGetMinVel Fail", axis);
                    return false;
                }//else

                axis.MinVel = MinVel;

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotGetMaxVel(axis.AxisNumber, ref MaxVel);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmMotGetMaxVel Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmMotGetMaxVel Fail", axis);
                    return false;
                }//else

                axis.MaxVel = MaxVel;

                return true;
            }//else

            return true;
        }

        private bool CheckAxisHommingVelue(AxisConfigModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.CheckAxisHommingVelue(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (axis.HomeVelocity1 == double.MaxValue || axis.HomeVelocity2 == double.MaxValue || axis.HomeVelocity3 == double.MaxValue ||
                axis.HomeVelocity4 == double.MaxValue || axis.HomeAccel1 == double.MaxValue || axis.HomeAccel2 == double.MaxValue)
            {
                double homeVel1 = default(double);
                double homeVel2 = default(double);
                double homeVel3 = default(double);
                double homeVel4 = default(double);
                double homeAcc1 = default(double);
                double homeAcc2 = default(double);

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeGetVel(axis.AxisNumber, ref homeVel1, ref homeVel2, ref homeVel3, ref homeVel4, ref homeAcc1, ref homeAcc2);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmHomeGetVel Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Debug("AxmHomeGetVel", funcRt);
                    return false;
                }//else

                axis.HomeVelocity1 = homeVel1;
                axis.HomeVelocity2 = homeVel2;
                axis.HomeVelocity3 = homeVel3;
                axis.HomeVelocity4 = homeVel4;
                axis.HomeAccel1 = homeAcc1;
                axis.HomeAccel2 = homeAcc2;
            }//else

            if (axis.HomeDir == int.MaxValue || axis.HomeSignal == uint.MaxValue || axis.Zphas == uint.MaxValue ||
                axis.HomeClearTime == double.MaxValue || axis.HomeOffset == double.MaxValue)
            {
                int homeDir = default(int);
                uint homeSignal = default(uint);
                uint homeZPhas = default(uint);
                double homeClearTime = default(double);
                double homeOffset = default(double);
                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeGetMethod(axis.AxisNumber, ref homeDir, ref homeSignal, ref homeZPhas, ref homeClearTime, ref homeOffset);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmHomeGetMethod Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Debug("AxmHomeGetMethod", funcRt);
                    return false;
                }//else

                axis.HomeDir = homeDir;
                axis.HomeSignal = homeSignal;
                axis.Zphas = homeZPhas;
                axis.HomeClearTime = homeClearTime;
                axis.HomeOffset = homeOffset;
            }//else

            return true;
        }

        private bool CheckAxisSignalVelue(AxisConfigModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.CheckAxisHommingVelue(axis);
            } // else

            uint ajinSignalUnuse = 2;
            uint ajinSignalUse = 3;

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (axis.UseAlarmSignal == null || axis.UseInPositionSignal == null)
            {
                uint useInPosition = uint.MaxValue;

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalGetInpos(axis.AxisNumber, ref useInPosition);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmSignalGetInpos Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Debug("AxmSignalGetInpos", funcRt);
                    return false;
                }//else

                axis.UseInPositionSignal = useInPosition != ajinSignalUnuse ? true : false;

                uint useInAlarm = uint.MaxValue;

                try
                {
                    funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalGetServoAlarm(axis.AxisNumber, ref useInAlarm);
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                    Logger.Error("AxmSignalGetServoAlarm Exception", axis);
                    return false;
                }

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Debug("AxmSignalGetServoAlarm", funcRt);
                    return false;
                }//else

                axis.UseAlarmSignal = useInAlarm != ajinSignalUnuse ? true : false;
            }//else

            return true;
        }

        private bool CheckMovePosition(int axisNumber, int timeout = 2000)
        {
            if (_axisNumber[axisNumber].ActualPosition == _axisNumber[axisNumber].CommandPosisiton)
            {
                return true;
            }//else

            DateTime startTime = DateTime.Now;
            DateTime limiteTime = startTime.AddMilliseconds(timeout);
            bool result = false;

            while (DateTime.Now < limiteTime)
            {
                if (_axisNumber[axisNumber].ActualPosition == _axisNumber[axisNumber].CommandPosisiton)
                {
                    return true;
                }//else
                Thread.Sleep(LoopInterval);
            }
            return result;
        }

        private bool CheckServoOn(int axisNumber, bool servoOn, int timeout = 5000)
        {
            DateTime startTime = DateTime.Now;
            DateTime limiteTime = startTime.AddMilliseconds(timeout);
            bool result = false;

            while (DateTime.Now < limiteTime)
            {
                if (_axisNumber[axisNumber].ServoOn == servoOn)
                {
                    result = true;
                    break;
                }//else
                Thread.Sleep(LoopInterval);
            }
            return result;
        }

        private bool GetActualPosition(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetActualPosition(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            double actualPosition = default(double);
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmStatusGetCmdPos(axis.AxisNumber, ref actualPosition);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmStatusGetActPos", axis.AxisName);

                    return false;
                }//else

                axis.ActualPosition = actualPosition;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmStatusGetActPos Exception - ", axis.AxisName);

                return false;
            }

            return true;
        }

        private bool GetAlarmCode(int axisNum)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetAlarmCode(axisNum);
            } // else

            //Todo:필요한지 확인필요.
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmStatusRequestServoAlarmHistory(axisNum);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug("AxmStatusRequestServoAlarmHistory", funcRt);
                return false;
            }//else

            //Todo:필요한지 확인필요.

            int AlarmCount = int.MinValue;
            uint[] AlarmCode = new uint[15];

            for (int i = 0; i < AlarmCode.Length; i++)
            {
                AlarmCode[i] = uint.MaxValue;
            }

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmStatusReadServoAlarmHistory(axisNum, (uint)AlarmReturnMode.NonBlocking, ref AlarmCount, ref AlarmCode[0]);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug(funcRt.ToString());

                return false;
            }//else

            for (int i = 0; i < AlarmCode.Length; i++)
            {
                if (AlarmCode[i] == uint.MaxValue)
                {
                    break;
                }//else

                _axisNumber[axisNum].AlarmCode.Add(AlarmCode[i].ToString());
            }

            return true;
        }

        private bool GetAxisCount(ref int count)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetAxisCount(ref count);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            int temp = 0;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmInfoGetAxisCount(ref temp);

                if (funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    count = temp - _configMotion.SpareAxisCount;

                    return true;
                }
                else
                {
                    Logger.Error("GetAxisCount Error");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private EmModuleInfo GetAxisInfo(int moduleNo)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetAxisInfo(moduleNo);
            } // else

            EmModuleInfo module = new EmModuleInfo();
            module.Number = moduleNo;
            try
            {
                AXT_FUNC_RESULT funcRt = (AXT_FUNC_RESULT)CAXM.AxmInfoGetAxis(module.Number, ref module.BoardNumber, ref module.Index, ref module.Id);

                if (funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    module.Name = Enum.GetName(typeof(AXT_MODULE), module.Id);
                    module.IsReady = true;
                }
                else
                {
                    module.IsReady = false;
                    Logger.Debug("GetAxisInfo : module", module);
                }
            }
            catch (Exception e)
            {
                module.IsReady = false;
                Logger.Exception(e);
            }

            return module;
        }

        private bool GetBusySignal(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetBusySignal(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            uint busy = default(uint);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmStatusReadInMotion(axis.AxisNumber, ref busy);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmStatusReadInMotion", axis.AxisName);
                    return false;
                }//else

                axis.Busy = busy == (int)AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmStatusReadInMotion Exception - ", axis.AxisName);
                return false;
            }

            return true;
        }

        private bool GetCommandPosisiton(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetCmdPosition(axis);
            } // else

            //AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            //double commandPosisiton = default(double);
            //try
            //{
            //    funcRt = (AXT_FUNC_RESULT)CAXM.AxmStatusGetCmdPos(axis.AxisNumber, ref commandPosisiton);

            //    if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            //    {
            //        Logger.Error("AxmStatusGetActPos", axis.AxisName);

            //        return false;
            //    }//else

            //    axis.CommandPosisiton = commandPosisiton;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Exception(ex);
            //    Logger.Error("AxmStatusGetActPos Exception - ", axis.AxisName);

            //    return false;
            //}

            return true;
        }

        private string GetCurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }

        private bool GetExsitMotionModule()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetExsitMotionModule();
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            uint exsit = uint.MaxValue;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmInfoIsMotionModule(ref exsit);
            }
            catch (Exception e)
            {
                Logger.Exception(e);

                return false;
            }

            if (funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                if (exsit == (uint)AXT_EXISTENCE.STATUS_EXIST)
                {
                    return true;
                }
                else
                {
                    Logger.Debug("GetExsitMotionModule : Data - " + ((AXT_EXISTENCE)exsit).ToString());

                    return false;
                }
            }//else

            return false;
        }

        private void GetExtenalLibVersion()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                _motionSim.GetExtenalLibVersion();
                return;
            } // else

            AXT_FUNC_RESULT result = default(AXT_FUNC_RESULT);
            byte bt = new byte();

            try
            {
                result = (AXT_FUNC_RESULT)CAXL.AxlGetLibVersion(ref bt);
                if (result != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("Ajin - ExtenalLibiVersion Get Fail");

                    return;
                }//else

                _extenalLibVersion = bt.ToString();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
        }

        private bool GetHomeResult(int axisNo)
        {
            uint homeResult = uint.MaxValue;

            if (GetHommingResult(axisNo, ref homeResult) != true)
            {
                return false;
            }//else

            while (homeResult == (uint)HomeResult.HOME_SEARCHING)
            {
                if (GetHommingResult(axisNo, ref homeResult) != true)
                {
                    return false;
                }//else

                Thread.Sleep(10);
            }

            if (homeResult != (uint)HomeResult.HOME_SUCCESS)
            {
                HomeResult temp = (HomeResult)homeResult;

                Logger.Error("GetHomeResult Homming Error -", temp);

                return false;
            }//else

            return true;
        }

        private bool GetHomeSignal(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetHomeSignal(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint homeSignal = default(uint);
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeReadSignal(axis.AxisNumber, ref homeSignal);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmHomeReadSignal", axis.AxisName);

                    return false;
                }//else

                axis.Home = homeSignal == (int)AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmHomeReadSignal Exception - ", axis.AxisName);

                return false;
            }

            return true;
        }

        private bool GetHommingResult(int axisNo, ref uint homeResult)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetHommingResult(axisNo, ref homeResult);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeGetResult(axisNo, ref homeResult);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("GetHommingResult AxmHomeGetResult- Exception", axisNo);

                return false;
            }

            return true;
        }

        private bool GetInPositionSignal(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetInPositionSignal(axis);
            } // else

            if (_axisConfigName[axis.AxisName].UseInPositionSignal != true)
            {
                return true;
            }//else

            //if (axis.AxisName == "XAxis")
            //{
            //    Logger.Debug("TestCode");
            //}

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint inPositionSignal = default(uint);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalReadInpos(axis.AxisNumber, ref inPositionSignal);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmSignalReadInpos", axis.AxisName);

                    return false;
                }//else

                axis.InPosition = inPositionSignal == (int)AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalReadInpos Exception - ", axis.AxisName);

                return false;
            }

            return true;
        }

        private bool GetIsOpen()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetIsOpen();
            } // else

            bool isOpen;

            try
            {
                isOpen = (AjinSignal)CAXL.AxlIsOpened() == AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }

            IsOpen = isOpen;

            return true;
        }

        private bool GetLimitSignal(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetLimitSignal(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            uint positiveLimit = default(uint);
            uint negativeLimit = default(uint);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalReadLimit(axis.AxisNumber, ref positiveLimit, ref negativeLimit);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmSignalReadLimit", axis.AxisName);
                    return false;
                }//else

                axis.PositiveLimit = positiveLimit == (int)AjinSignal.AjinTrue ? true : false;
                axis.NegativeLimit = negativeLimit == (int)AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalReadLimit Exception - ", axis.AxisName);

                return false;
            }

            return true;
        }

        private bool GetMoveMode(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetMoveMode(axis.AxisNumber);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint getMode = uint.MaxValue;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotGetAbsRelMode(axis.AxisNumber, ref getMode);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            _axisNumber[axis.AxisNumber].MoveMode = (AbsRelMode)getMode;

            return true;
        }

        private bool GetServoAlarmSignal(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetServoAlarmSignal(axis);
            } // else

            if (_axisConfigName[axis.AxisName].UseAlarmSignal != true)
            {
                return true;
            }//else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint servoAlarm = default(uint);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalReadServoAlarm(axis.AxisNumber, ref servoAlarm);

                if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    Logger.Error("AxmSignalReadServoAlarm " + axis.AxisName, funcRt);
                    return false;
                }//else

                axis.Alarm = servoAlarm == (int)AjinSignal.AjinTrue ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalReadServoAlarm Exception - ", axis.AxisName);

                return false;
            }

            return true;
        }

        private bool GetServoOn(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.GetServoOn(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint servoOn = uint.MaxValue;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalIsServoOn(axis.AxisNumber, ref servoOn);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalIsServoOn Exception - ", axis.AxisNumber);
                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("GetServoOn - ", funcRt);

                return false;
            }//else

            axis.ServoOn = servoOn == (uint)AjinSignal.AjinTrue ? true : false;

            return true;
        }

        private bool Homming(AxisPropertyModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.Homming(axis);
            } // else

            if (axis.ServoOn != true)
            {
                Logger.Error("Homming Servo is Not On. - ", axis.AxisName);

                return false;
            } // else

            if (SetHomming(axis) != true)
            {
                return false;
            }

            //Todo:함수 확인필요

            if (GetHomeResult(axis.AxisNumber) != true)
            {
                Stop(axis.AxisNumber);
                Logger.Error("Homming Fail GetHomeResult");

                return false;
            }//else

            axis.Orign = true;

            axis.CommandPosisiton = 0.0;

            return true;
        }

        private bool initModuleInfo()
        {
            Modules = new Dictionary<int, EmModuleInfo>();

            int module_count = 0;

            if (Modules.Count > 0)
            {
                Modules.Clear();
            }// else

            if (IsExsitMotionModule == true)
            {
                module_count = AxisCount;
                for (int number = 0; number < module_count; number++)
                {
                    EmModuleInfo module = GetAxisInfo(number);
                    AddModule(number, module);
                    AddMotionProperty(number);
                }
            }// else

            return true;
        }

        private bool LoadMot()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.LoadMot();
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            // Servo ON시에 Mot 파일 로드

            string filePath = GetCurrentDirectory() + @"\MotionDefault.mot";

            if (File.Exists(filePath) != true)
            {
                Logger.Error("LoadMot - Wrong FilePath", filePath);

                return false;
            }//else

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotLoadParaAll(filePath);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("LoadMot - AxmMotLoadParaAll", filePath);
                return false;
            }

            if (funcRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveAbsolutePosition(AxisPropertyModel axis, double position)
        {
            axis.AxisPosition[PositionDefaultName].Position = position;
            axis.AxisPosition[PositionDefaultName].MoveMode = AbsRelMode.Absolute.ToString();

            return MoveStart(axis, PositionDefaultName);
        }

        private bool MoveModeSelect(AxisPropertyModel axis, AbsRelMode mode)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.MoveModeSelect(axis, mode);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotSetAbsRelMode(axis.AxisNumber, (uint)mode);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmMotSetAbsRelMode Exception - ", axis.AxisName);

                return false;
            }

            bool result = CheckAbsRelMode(axis.AxisNumber, mode);

            return result;
        }

        private bool MoveRelativeValue(AxisPropertyModel axis, double position)
        {
            axis.AxisPosition[PositionDefaultName].Position = position;
            axis.AxisPosition[PositionDefaultName].MoveMode = AbsRelMode.Relative.ToString();

            return MoveStart(axis, PositionDefaultName);
        }

        private bool MoveStart(AxisPropertyModel axis, string positionString, bool skipChangeMoveMode = false)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.MoveStart(axis, positionString, skipChangeMoveMode);
            } // else

            if (axis.ServoOn != true)
            {
                Logger.Error("Homming Servo is Not On. - ", axis.AxisName);

                return false;
            } // else

            if (axis.Orign == false)
            {
                Logger.Error("MoveStart Orign is Not Set. - ", axis.AxisName);

                return false;
            }// else

            if (axis.Busy == true)
            {
                Logger.Error("MoveStart Axis is Busy. - ", axis.AxisName);

                return false;
            }// else

            if (axis.AxisPosition.ContainsKey(positionString) != true)
            {
                Logger.Error("MoveStart PositionName not found. - ", positionString);

                return false;
            }//else

            AbsRelMode mode = axis.MoveMode;

            if (skipChangeMoveMode != true)
            {
                if (Enum.TryParse<AbsRelMode>(axis.AxisPosition[positionString].MoveMode, out mode) != true)
                {
                    Logger.Error("MoveStart - TryParse ", axis.AxisPosition[positionString].MoveMode);

                    return false;
                }//else
            }//else

            if (MoveModeSelect(axis, mode) != true)
            {
                Logger.Error("MoveModeSelect fail ", mode);

                return false;
            }// else

            double position = axis.AxisPosition[positionString].Position;
            double velocity = axis.AxisPosition[positionString].Velocity;
            double accel = axis.AxisPosition[positionString].Accel;
            double decel = axis.AxisPosition[positionString].Decel;

            if (mode == AbsRelMode.Absolute)
            {
                if (position > _configMotion.AllAxis[axis.AxisName].SwLimitMax)
                {
                    Logger.Error("MoveStart - Out of MaxSwLimit Absolute Velue", position);
                    position = _configMotion.AllAxis[axis.AxisName].SwLimitMax;
                }
                else if (position < _configMotion.AllAxis[axis.AxisName].SwLimitMin)
                {
                    Logger.Error("MoveStart - Out of MinSwLimit Absolute Velue", position);
                    position = _configMotion.AllAxis[axis.AxisName].SwLimitMin;
                }//else
                axis.CommandPosisiton = position;
            }
            else
            {
                double temp = axis.CommandPosisiton + position;
                if (temp > _configMotion.AllAxis[axis.AxisName].SwLimitMax)
                {
                    Logger.Error("MoveStart - Out of MaxSwLimit Relative Velue", temp);
                    position = _configMotion.AllAxis[axis.AxisName].SwLimitMax - axis.CommandPosisiton;
                }
                else if (temp < _configMotion.AllAxis[axis.AxisName].SwLimitMin)
                {
                    Logger.Error("MoveStart - Out of MinSwLimit Relative Velue", position);
                    position = _configMotion.AllAxis[axis.AxisName].SwLimitMin - axis.CommandPosisiton;
                }//else
                axis.CommandPosisiton = axis.CommandPosisiton + position;
            }

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            double currnetPos = axis.ActualPosition;
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMoveStartPos(axis.AxisNumber, position, velocity, accel, decel);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("MoveStart - ", funcRt);

                return false;
            }//else
            var calc = (Math.Abs(currnetPos - axis.CommandPosisiton) / velocity) * 1200;

            bool result = CheckMovePosition(axis.AxisNumber, Convert.ToInt32(calc));

            return result;
        }

        private bool OpenLib()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.OpenLib();
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            funcRt = (AXT_FUNC_RESULT)CAXL.AxlOpen(7);

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("Ajin Open Failure [Error Code : " + funcRt + "]");

                return false;
            }// else

            return true;
        }

        private void RegistAxisEvent(AxisPropertyModel axis, bool value = true)
        {
            if (value == true)
            {
                axis.OnAlarm += Axis_OnAlarm;
                axis.OnServoOn += Axis_OnServoOn;
            }
            else
            {
                axis.OnAlarm -= Axis_OnAlarm;
                axis.OnServoOn -= Axis_OnServoOn;
            }
        }

        private bool ServoOn(AxisPropertyModel axis, bool servoOn)
        {
            axis.AlarmCode.Clear();
            GetAlarmCode(axis.AxisNumber);

            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.ServoOn(axis, servoOn);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            uint cmdServoOn = (uint)(servoOn == true ? AjinSignal.AjinTrue : AjinSignal.AjinFalse);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalServoOn(axis.AxisNumber, cmdServoOn);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalServoOn Exception - ", axis);
                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("ServoOn Fail - ", funcRt);

                return false;
            }//else

            bool result = CheckServoOn(axis.AxisNumber, servoOn);

            return result;
        }

        private bool SetAxisDefaultPosition(AxisConfigModel axis)
        {
            if (axis.PositionDic == null)
            {
                axis.PositionDic = new Dictionary<string, PositionModel>();
            }//else

            if ((axis.PositionDic.Count > 0) != true || axis.PositionDic.ContainsKey(PositionDefaultName) != true)
            {
                PositionModel defaultPosition = new PositionModel(PositionDefaultName);
                defaultPosition.Accel = axis.Accel;
                defaultPosition.Decel = axis.Decel;
                defaultPosition.Velocity = axis.Velocity;
                defaultPosition.MoveMode = AbsRelMode.Absolute.ToString();
                defaultPosition.Position = 0;
                axis.PositionDic.Add(PositionDefaultName, defaultPosition);
            }//else
            if (axis.PositionDic[PositionDefaultName].Accel == double.MaxValue ||
                axis.PositionDic[PositionDefaultName].Decel == double.MaxValue ||
                axis.PositionDic[PositionDefaultName].Velocity == double.MaxValue)
            {
                axis.PositionDic[PositionDefaultName].Accel = axis.Accel;
                axis.PositionDic[PositionDefaultName].Decel = axis.Decel;
                axis.PositionDic[PositionDefaultName].Velocity = axis.Velocity;
            }//else
            _axisName[axis.AxisName].AxisPosition = axis.PositionDic;

            return true;
        }

        private bool SetAxisDefaultVelue(AxisConfigModel axis)
        {
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.SetAxisHommingVelue(axis);
            } // else

            if (axis.MinVel == double.MaxValue || axis.MaxVel == double.MaxValue)
            {
                Logger.Error("SetAxisDefaultVelue is Error", axis);

                return false;
            }// else

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotSetMinVel(axis.AxisNumber, axis.MinVel);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmMotSetMinVel Exception", axis);

                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("AxmMotSetMinVel Fail", axis);
                return false;
            }//else

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmMotSetMaxVel(axis.AxisNumber, axis.MaxVel);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmMotSetMaxVel Exception", axis);

                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("AxmMotSetMaxVel Fail", axis);
                return false;
            }//else

            return true;
        }

        private bool SetAxisHommingVelue(AxisConfigModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.SetAxisHommingVelue(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (axis.HomeVelocity1 == double.MaxValue || axis.HomeVelocity2 == double.MaxValue || axis.HomeVelocity3 == double.MaxValue ||
                axis.HomeVelocity4 == double.MaxValue || axis.HomeAccel1 == double.MaxValue || axis.HomeAccel2 == double.MaxValue)
            {
                Logger.Error("HomeVelue is Error", axis);

                return false;
            }// else

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeSetVel(axis.AxisNumber, axis.HomeVelocity1, axis.HomeVelocity2, axis.HomeVelocity3, axis.HomeVelocity4, axis.HomeAccel1, axis.HomeAccel2);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmHomeSetVel Exception", axis);

                return false;
            }
            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug("AxmHomeSetVel", funcRt);

                return false;
            }//else

            if (axis.HomeDir == int.MaxValue || axis.HomeSignal == uint.MaxValue || axis.Zphas == uint.MaxValue ||
                axis.HomeClearTime == double.MaxValue || axis.HomeOffset == double.MaxValue)
            {
                Logger.Error("HomeMethodVelue is Error", axis);

                return false;
            }//else

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeSetMethod(axis.AxisNumber, axis.HomeDir, axis.HomeSignal, axis.Zphas, axis.HomeClearTime, axis.HomeOffset);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmHomeGetMethod Exception", axis);

                return false;
            }
            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug("AxmHomeSetMethod", funcRt);

                return false;
            }//else

            return true;
        }

        private bool SetAxisSignalVelue(AxisConfigModel axis)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.SetAxisHommingVelue(axis);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            if (axis.UseAlarmSignal == null)
            {
                Logger.Error("UseAlarmSignal is Error", axis);

                return false;
            }// else

            uint ajinSignalUnuse = 2;
            uint ajinSignalUse = 3;

            uint useSignal = axis.UseInPositionSignal == true ? ajinSignalUse : ajinSignalUnuse;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalSetInpos(axis.AxisNumber, useSignal);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalSetInpos Exception", axis);

                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug("AxmSignalSetInpos", funcRt);

                return false;
            }//else

            if (axis.UseInPositionSignal == null)
            {
                Logger.Error("UseInPositionSignal is Error", axis);

                return false;
            }// else

            useSignal = axis.UseAlarmSignal == true ? ajinSignalUse : ajinSignalUnuse;
            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmSignalSetServoAlarm(axis.AxisNumber, useSignal);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("AxmSignalSetServoAlarm Exception", axis);

                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Debug("AxmSignalSetServoAlarm", funcRt);
                return false;
            }//else

            return true;
        }

        private bool SetHomming(AxisPropertyModel axis)
        {
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXM.AxmHomeSetStart(axis.AxisNumber);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("Homming Exception - ", axis.AxisName);

                return false;
            }

            if ((funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS || funcRt == AXT_FUNC_RESULT.AXT_RT_MOTION_HOME_SEARCHING) == false)
            {
                Logger.Error("Homming - ", funcRt);

                return false;
            }//else

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="stopMode">기본값 = "" = StopMode.SStop</param>
        /// <returns></returns>
        private bool Stop(AxisPropertyModel axis, string stopMode = "")
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _motionSim.Stop(axis, stopMode);
            } // else

            if (string.IsNullOrEmpty(stopMode))
            {
                stopMode = StopMode.SStop.ToString();
            }// else

            StopMode Mode = default(StopMode);

            bool result = Enum.TryParse<StopMode>(stopMode, out Mode);
            if (result != true)
            {
                Logger.Error("Stop TryParse - " + axis.AxisName, stopMode);
                return false;
            }//else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            try
            {
                switch (Mode)
                {
                    case StopMode.EStop:
                        funcRt = (AXT_FUNC_RESULT)CAXM.AxmMoveEStop(axis.AxisNumber);
                        break;

                    case StopMode.SStop:
                        funcRt = (AXT_FUNC_RESULT)CAXM.AxmMoveSStop(axis.AxisNumber);
                        break;

                    default:
                        Logger.Error("Stop TryParse - " + axis.AxisName, Mode);
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Logger.Error("Stop Exception - " + axis.AxisName, Mode);

                return false;
            }

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("Stop - " + axis.AxisName, funcRt);
                return false;
            }//else

            axis.CommandPosisiton = axis.ActualPosition;

            return true;
        }

        #endregion Methods
    }
}