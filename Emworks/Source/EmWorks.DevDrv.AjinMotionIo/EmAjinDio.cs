using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public partial class EmAjinDio : DIoBase
    {
        #region Fields

        internal string _extenalLibVersion = string.Empty;
        internal string _version = string.Empty;
        private AjinMotionConfigModel _configDIO;
        private int _dICount = int.MinValue;
        private Dictionary<string, DioModel> _digitalInput = new Dictionary<string, DioModel>();
        private Dictionary<string, DioModel> _digitalOutput = new Dictionary<string, DioModel>();
        private List<DioModel> _dIList = new List<DioModel>();
        private int _dioModuleCount = int.MinValue;
        private AjinDioSim _dioSim;
        private int _dOCount = int.MinValue;
        private List<DioModel> _dOList = new List<DioModel>();
        private bool _ioDefined = false;
        private bool _isExsitDioModule;
        private bool _isExsitIoModule;
        private Dictionary<int, EmModuleInfo> Modules;

        #endregion Fields

        #region Constructors

        public EmAjinDio(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Destructors

        ~EmAjinDio()
        {
            IsUpdateLoop = false;

            if (_dOList != null)
            {
                foreach (var item in _dOList)
                {
                    item.CheckDioLoop = false;
                }
            }//else

            if (_dIList != null)
            {
                foreach (var item in _dIList)
                {
                    item.CheckDioLoop = false;
                }
            }//else

            Close();
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventDioChanged(object sender, EventArgs Args);

        #endregion Delegates

        #region Events

        public event EventDioChanged DigitalInputOnChange;

        public event EventDioChanged DigitalOutputOnChange;

        #endregion Events

        #region Enums

        private enum AjinSignal : uint
        {
            AjinFalse,
            AjinTrue
        }

        private enum IoType
        {
            Both,
            Input,
            Output
        }

        #endregion Enums

        #region Properties

        public override int DICount
        {
            get
            {
                if (_dICount <= 0)
                {
                    if (GetDICount() == false)
                    {
                        _dICount = int.MinValue;
                    }//else
                }//else

                return _dICount;
            }
            protected set
            {
                _dICount = value;
            }
        }

        public Dictionary<string, DioModel> DigitalInput
        {
            get
            {
                return _digitalInput;
            }

            protected set
            {
                _digitalInput = value;
            }
        }

        public Dictionary<string, DioModel> DigitalOutput
        {
            get
            {
                return _digitalOutput;
            }

            protected set
            {
                _digitalOutput = value;
            }
        }

        public List<DioModel> DIList
        {
            get
            {
                return _dIList;
            }

            protected set
            {
                _dIList = value;
            }
        }

        public int DioModuleCount
        {
            get
            {
                if (_dioModuleCount <= 0)
                {
                    if (GetModuleCount(ref _dioModuleCount) != true)
                    {
                        _dioModuleCount = int.MinValue;
                    }//else
                }//else

                return _dioModuleCount;
            }
        }

        public override int DOCount
        {
            get
            {
                if (_dOCount <= 0)
                {
                    if (GetDOCount() == false)
                    {
                        _dOCount = int.MinValue;
                    }//else
                }//else
                return _dOCount;
            }

            protected set
            {
                _dOCount = value;
            }
        }

        public List<DioModel> DOList
        {
            get
            {
                return _dOList;
            }

            protected set
            {
                _dOList = value;
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

        private bool IsExsitDioModule
        {
            get
            {
                _isExsitDioModule = GetExsitDioModule();

                return _isExsitDioModule;
            }
        }

        #endregion Properties

        #region Methods

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

        public bool DefineInputList(Dictionary<string, int> ioList)
        {
            bool result = true;
            if (IsSimulation != Define.RunMode.Real)
            {
                result = _dioSim.DefineInputList(ioList);
                if (result == true)
                {
                    _ioDefined = true;
                }//else

                return result;
            } // else

            foreach (var item in ioList)
            {
                if (_dIList.Count <= item.Value)
                {
                    Logger.Error("SetInputList Out of Count - " + item.Key);
                    result = false;

                    continue;
                }//else

                if (_dIList[item.Value].DIoName != item.Key)
                {
                    _dIList[item.Value].DIoName = item.Key;

                    if (_digitalInput.ContainsKey(item.Key) == true)
                    {
                        _digitalInput[item.Key] = _dIList[item.Value];
                    }
                    else
                    {
                        _digitalInput.Add(item.Key, _dIList[item.Value]);
                    }
                }//else
            }
            _ioDefined = true;

            return result;
        }

        public bool DefineOutputList(Dictionary<string, int> ioList)
        {
            bool result = true;
            if (IsSimulation != Define.RunMode.Real)
            {
                result = _dioSim.DefineOutputList(ioList);
                if (result == true)
                {
                    _ioDefined = true;
                }//else

                return result;
            } // else

            foreach (var item in ioList)
            {
                if (_dOList.Count <= item.Value)
                {
                    Logger.Error("SetOutputList Out of Count - " + item.Key);
                    result = false;

                    continue;
                }//else

                if (_dOList[item.Value].DIoName != item.Key)
                {
                    _dOList[item.Value].DIoName = item.Key;

                    if (_digitalOutput.ContainsKey(item.Key) == true)
                    {
                        _digitalOutput[item.Key] = _dOList[item.Value];
                    }
                    else
                    {
                        _digitalOutput.Add(item.Key, _dOList[item.Value]);
                    }
                }//else
            }
            return result;
        }

        public void InitialConfig(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) == true)
            {
                _configDIO = new AjinMotionConfigModel();
            }
            else
            {
                _configDIO = new AjinMotionConfigModel(filePath);
            }
            _configDIO.Load(ref _configDIO);

            if ((int)IsSimulation < (int)_configDIO.RunMode)
            {
                IsSimulation = _configDIO.RunMode;
            }//else
        }

        public override bool Open()
        {
            if (IsSimulation == Define.RunMode.Simulation)
            {
                IsOpen = true;

                return IsOpen;
            } // else

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

                IsOpen = true;

                // Opne 성공
                if (initModuleInfo() != true)
                {
                    IsOpen = false;
                    Logger.Error("Ajin Fail to initModuleInfo");

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

        public bool SetDigitalInput(string dioName, bool signal)
        {
            return SetDigitalInput(_digitalInput[dioName], signal);
        }

        public bool SetDigitalOutput(string dioName, bool signal)
        {
            return SetDigitalOutput(_digitalOutput[dioName], signal);
        }

        internal void DI_OnChange(object sender, EventArgs Args)
        {
            DigitalInputOnChange?.Invoke(this, Args);
        }

        internal void DO_OnChange(object sender, EventArgs Args)
        {
            DigitalOutputOnChange?.Invoke(this, Args);
        }

        protected void DoUpdate()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return;
            } // else
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);
            if (_ioDefined == false)
            {
                return;
            }//else

            foreach (var item in _digitalInput)
            {
                if (string.IsNullOrEmpty(item.Value.DIoName) != true)
                {
                    uint up_bits = uint.MaxValue;
                    funcRt = (AXT_FUNC_RESULT)CAXD.AxdiReadInport(item.Value.DIoNumber, ref up_bits);
                    item.Value.Signal = up_bits == (uint)AjinSignal.AjinTrue ? true : false;
                }//else
                Thread.Sleep(1);
            }
            foreach (var item in _digitalOutput)
            {
                if (string.IsNullOrEmpty(item.Value.DIoName) != true)
                {
                    uint up_bits = uint.MaxValue;
                    funcRt = (AXT_FUNC_RESULT)CAXD.AxdoReadOutport(item.Value.DIoNumber, ref up_bits);
                    item.Value.Signal = up_bits == (uint)AjinSignal.AjinTrue ? true : false;
                }//else
                Thread.Sleep(1);
            }

            int start_bit_offset = 0;

            //foreach (EmModuleInfo em in Modules.Values)
            //{
            //    uint up_bits = 0;
            //    if (em.DiCount == 16)
            //    {
            //        funcRt = (AXT_FUNC_RESULT)CAXD.AxdiReadInportWord(em.Index, 0, ref up_bits);
            //    }
            //    else if (em.DiCount == 32)
            //    {
            //        funcRt = (AXT_FUNC_RESULT)CAXD.AxdiReadInportDword(em.Index, 0, ref up_bits);
            //    }//else

            //    if (funcRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            //    {
            //        Logger.Error("Failed the UpdateDi");
            //        continue;
            //    }//else

            //    int temp = Convert.ToInt32(up_bits);
            //    BitArray b = new BitArray(new int[] { temp });
            //    for (int n = 0; n < em.DiCount; n++)
            //    {
            //        for (int i = 0; i < b.Count; i++)
            //        {
            //            if (string.IsNullOrEmpty(_dIList[start_bit_offset + n].DIoName) != true)
            //            {
            //                _dIList[start_bit_offset + n].Signal = b[n];
            //            }//else
            //        }
            //    }
            //    start_bit_offset = start_bit_offset + em.DiCount;
            //}
        }

        protected override void Initialize()
        {
            _dioSim = new AjinDioSim(this);

            InitInstance();
            RegistEvent();

            Logger.Debug("ExtenalLibVersion", _extenalLibVersion);
            Logger.Debug("Version", _version);

            if (IsSimulation != Define.RunMode.Real)
            {
                return;
            } // else
        }

        protected override void InitInstance()
        {
            GetExtenalLibVersion();
        }

        protected override void RegistEvent()
        {
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
                int start_bit_offset = GetStartBitDiOffset(index);

                Modules.Add(index, moduleInfo);
                if (moduleInfo.DiCount > 0)
                {
                    for (int i = 0; i < moduleInfo.DiCount; i++)
                    {
                        DioModel temp = new DioModel(start_bit_offset + i);
                        temp.ModuleNum = index;
                        temp.OffsetNum = i;
                        _dIList.Add(temp);
                    }
                    _dICount = _dICount + moduleInfo.DiCount;
                }//else
                start_bit_offset = GetStartBitDoOffset(index);
                if (moduleInfo.DoCount > 0)
                {
                    for (int i = 0; i < moduleInfo.DoCount; i++)
                    {
                        DioModel temp = new DioModel(start_bit_offset + i);
                        temp.ModuleNum = index;
                        temp.OffsetNum = i;
                        _dOList.Add(temp);
                    }
                    _dOCount = _dOCount + moduleInfo.DoCount;
                }//else
            }// else
        }

        private bool CheckDigitalOutput(DioModel dio,int timeout = 1000)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timespan = new TimeSpan(0, 0, 1000);
            bool result = false;

            while (dio.CheckDioLoop)
            {
                if (dio.Signal == dio.CommendSignal)
                {
                    result = true;
                    break;
                }//else

                Thread.Sleep(LoopInterval);
            }
            return result;
        }

        private string GetCurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }

        private bool GetDICount()
        {
            int temp = 0;

            foreach (var item in Modules)
            {
                temp = temp + item.Value.DiCount;
            }

            _dICount = temp;

            return true;
        }

        private EmModuleInfo GetDioInfo(int moduleNo)
        {
            EmModuleInfo module = new EmModuleInfo();
            module.Number = moduleNo;
            try
            {
                AXT_FUNC_RESULT funcRt = (AXT_FUNC_RESULT)CAXD.AxdInfoGetModule(module.Number, ref module.BoardNumber, ref module.Index, ref module.Id);

                if (funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    module.Name = Enum.GetName(typeof(AXT_MODULE), module.Id);

                    funcRt = (AXT_FUNC_RESULT)CAXD.AxdInfoGetInputCount(module.Number, ref module.DiCount);

                    funcRt = (AXT_FUNC_RESULT)CAXD.AxdInfoGetOutputCount(module.Number, ref module.DoCount);

                    if (module.DiCount > 0 && module.DoCount > 0)
                    {
                        module.IoType = (int)IoType.Both;
                    }
                    else if (module.DiCount > 0 && module.DoCount == 0)
                    {
                        module.IoType = (int)IoType.Input;
                    }
                    else if (module.DiCount == 0 && module.DoCount > 0)
                    {
                        module.IoType = (int)IoType.Output;
                    }//else

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

        private bool GetDOCount()
        {
            int temp = 0;

            foreach (var item in Modules)
            {
                temp = temp + item.Value.DoCount;
            }

            _dOCount = temp;

            return true;
        }

        private bool GetExsitDioModule()
        {
            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            uint exsit = uint.MaxValue;

            try
            {
                funcRt = (AXT_FUNC_RESULT)CAXD.AxdInfoIsDIOModule(ref exsit);
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

        private bool GetExsitIoModule()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                //return _DioSim.GetExsitMotionModule();
            } // else

            AXT_FUNC_RESULT FuncRt = default(AXT_FUNC_RESULT);

            uint exsit = 0;

            try
            {
                FuncRt = (AXT_FUNC_RESULT)CAXD.AxdInfoIsDIOModule(ref exsit);
            }
            catch (Exception e)
            {
                Logger.Exception(e);

                return false;
            }

            if (FuncRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                if (exsit == (uint)AXT_EXISTENCE.STATUS_EXIST)
                {
                    return true;
                }
                else
                {
                    Logger.Debug("GetExsitIoModule : Data - " + ((AXT_EXISTENCE)exsit).ToString());

                    return false;
                }
            }//else

            return false;
        }

        private void GetExtenalLibVersion()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                _dioSim.GetExtenalLibVersion();
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

        private bool GetIsOpen()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _dioSim.GetIsOpen();
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

        private bool GetModuleCount(ref int count)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _dioSim.GetModuleCount(ref count);
            } // else

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            //lock (_isKey)
            funcRt = (AXT_FUNC_RESULT)CAXD.AxdInfoGetModuleCount(ref _dioModuleCount);

            if (funcRt == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                return true;
            }
            else
            {
                Logger.Error("GetModuleCount Error");
                return false;
            }
        }

        private int GetStartBitDiOffset(int moduleIndex)
        {
            int offset = 0;

            for (int i = 0; i < moduleIndex; i++)
            {
                if (Modules[i].IoType == (int)IoType.Input)
                {
                    offset += Modules[i].DiCount;
                }//else
            }

            return offset;
        }

        private int GetStartBitDoOffset(int moduleIndex)
        {
            int offset = 0;

            for (int i = 0; i < moduleIndex; i++)
            {
                if (Modules[i].IoType == (int)IoType.Input)
                {
                    offset += Modules[i].DoCount;
                }//else
            }

            return offset;
        }

        private bool initModuleInfo()
        {
            Modules = new Dictionary<int, EmModuleInfo>();

            int module_count = 0;

            if (Modules.Count > 0)
            {
                Modules.Clear();
            }// else

            if (IsExsitDioModule == true)
            {
                module_count = DioModuleCount;

                for (int number = 0; number < module_count; number++)
                {
                    EmModuleInfo module = GetDioInfo(number);
                    AddModule(number, module);
                }

                foreach (var item in _dIList)
                {
                    item.OnChange += DI_OnChange;
                }

                foreach (var item in _dOList)
                {
                    item.OnChange += DO_OnChange;
                }
            }// else

            return true;
        }

        private bool OpenLib()
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _dioSim.OpenLib();
            } // else

            AXT_FUNC_RESULT FuncRt = default(AXT_FUNC_RESULT);

            FuncRt = (AXT_FUNC_RESULT)CAXL.AxlOpen(7);

            if (FuncRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("Ajin Open Failure [Error Code : " + FuncRt + "]");

                return false;
            }// else

            return true;
        }

        private bool SetDigitalInput(DioModel dio, bool signal)
        {
            if (IsSimulation == Define.RunMode.Simulation)
            {
                return _dioSim.SetDigitalInput(dio, signal);
            } // else

            return true;
        }

        private bool SetDigitalOutput(DioModel dio, bool signal)
        {
            if (IsSimulation != Define.RunMode.Real)
            {
                return _dioSim.SetDigitalOutput(dio, signal);
            } // else

            if (dio.CommendSignal == signal)
            {
                return true;
            }

            dio.CommendSignal = signal;

            AjinSignal ajSignal = signal == true ? AjinSignal.AjinTrue : AjinSignal.AjinFalse;

            AXT_FUNC_RESULT funcRt = default(AXT_FUNC_RESULT);

            //lock (_isKey)
            funcRt = (AXT_FUNC_RESULT)CAXD.AxdoWriteOutport(dio.DIoNumber, (uint)ajSignal);

            if (funcRt != AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("SetDigitalOutput Fail - ", funcRt);

                return false;
            }//else

            bool result = CheckDigitalOutput(dio);

            return result;
        }

        #endregion Methods
    }
}