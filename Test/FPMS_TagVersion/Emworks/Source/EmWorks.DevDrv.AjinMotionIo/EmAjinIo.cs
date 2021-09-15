using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public class test
    {
    }
    public class EmAjinIo : DriverBase
    {
        #region Fields

        public uint FuncRt;
        private Dictionary<string, Tag> _IoTags;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="isSimulation"></param>
        public EmAjinIo(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {
            _IoTags = tags;

            Initialization();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Module Information obtained from Ajin Lib
        /// </summary>
        public Dictionary<int, EmModuleInfo> _Modules { get; protected set; }

        /// <summary>
        /// analog channel modules count
        /// </summary>
        public int AioModuleCount
        {
            get
            {
                int count = 0;

                //lock (_isKey)

                FuncRt = CAXA.AxaInfoGetModuleCount(ref count);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    return count;
                else
                    return 0;
            }
        }

        /// <summary>
        /// digital I/O moduls count.
        /// </summary>
        public int DioModuleCount
        {
            get
            {
                int count = 0;
                //lock (_isKey)
                FuncRt = CAXD.AxdInfoGetModuleCount(ref count);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    return count;
                else
                    return 0;
            }
        }

        /// <summary>
        /// analog channel modules 유무 확인.
        /// </summary>
        public bool IsExsitAioModule
        {
            get
            {
                uint exsit = 0;

                //lock (_isKey)
                FuncRt = CAXA.AxaInfoIsAIOModule(ref exsit);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    return exsit == (uint)AXT_EXISTENCE.STATUS_EXIST ? true : false;
                else
                    return false;
            }
        }

        /// <summary>
        /// digital I/O moduls 유무 확인.
        /// </summary>
        public bool IsExsitDioModule
        {
            get
            {
                uint exsit = 0;

                //lock (_isKey)
                FuncRt = CAXD.AxdInfoIsDIOModule(ref exsit);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    return exsit == (uint)AXT_EXISTENCE.STATUS_EXIST ? true : false;
                else
                    return false;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 상태 업데이트 전 Module 확인
        /// </summary>
        /// <param name="IsAi">Analog 이면 True, Digital 이면 False </param>
        /// <returns></returns>
        public bool CheckStatus(bool IsAi)
        {
            if (IsSimulation == true)
            {
                Logger.Infomation("Simulator mode");
                return false;
            }

            if (IsAi == true)
            {
                if (!IsOpened || !IsExsitAioModule)
                {
                    Logger.Infomation("Return - IsOpen : " + IsOpened.ToString() + " Exist AIO : " + IsExsitAioModule);
                    return false;
                }
            }
            else
            {
                if (!IsOpened || !IsExsitDioModule)
                {
                    Logger.Infomation("Return - IsOpen : " + IsOpened.ToString() + " Exist DIO : " + IsExsitDioModule);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 종료
        /// </summary>
        /// <returns></returns>
        public override int Close(Tag commandTag)
        {
            try
            {
                if (IsSimulation == true)
                {
                    return FunctionResult.True;
                }

                uint funcRt = FunctionResult.True;

                bool isOpened = CAXL.AxlIsOpened() == 1 ? true : false; // Open되어 있을 경우 1, 아니면 0 리턴.
                if (isOpened)
                {
                    funcRt = (uint)CAXL.AxlClose();   // success = 1, failure = 0 ?
                }

                Logger.Debug("close result=" + funcRt);

                if (funcRt == FunctionResult.True)
                {
                    SetCommandResult(commandTag.Identity, OpenClose.Close);
                }

                return (int)funcRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

        /// <summary>
        /// 실행 명령
        /// </summary>
        public void Command(Tag commandTag)
        {
            try
            {
                if (IsSimulation == true)
                {
                    Logger.Infomation("Simulation Mode");
                    return;
                } // else

                if (commandTag.Identity.IoType == Tag.Idx.IoType.StatusOutput)
                {
                    if (commandTag.Is.ToString() == OpenClose.Open.ToString())
                    {
                        Open(commandTag);
                    }
                    else
                    {
                        Close(commandTag);
                    }
                }
                else if (commandTag.Identity.IoType == Tag.Idx.IoType.Output)
                {
                    int offset = Convert.ToInt32(commandTag.Identity.Offset);
                    if (commandTag.Identity.DataType == Tag.Idx.DataType.Int)
                    {
                        if (offset >= 0)
                        {
                            CAXD.AxdoWriteOutport(offset, (uint)commandTag.n);
                        }
                    }
                    else if (commandTag.Identity.DataType == Tag.Idx.DataType.Int)
                    {
                        if (offset >= 0)
                        {
                            CAXA.AxaoWriteVoltage(offset, (double)commandTag.n);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public double GetkPa(double slope, double voltage, double yIntercept)
        {
            /*
                [ANALOG_CALIBRATION_0]
                ModuleIndex=0
                Slope=-27.927776476508
                Y-Intercept=48.5
             */
            return slope * voltage + yIntercept;
        }

        /// <summary>
        /// Open
        /// </summary>
        /// <returns></returns>
        public override int Open(Tag commandTag)
        {
            IsOpened = CAXL.AxlIsOpened() == 1 ? true : false; // Open되어 있을 경우 1, 아니면 0 리턴.

            if (IsOpened == true)
            {
                SetCommandResult(commandTag.Identity, OpenClose.Open);

                // 이미 Open되어 있음.
                Logger.Infomation("It's already open.");
                return FunctionResult.True;
            }
            else
            {
                // Library open 상태 확인, 초기화.
                FuncRt = CAXL.AxlOpen(7);

                string commandTagName = commandTag.Identity.SimCmd.Split('/')[0];

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    FuncRt = FunctionResult.True;// Ajin의 성공은 0이지만 Code 통일을 위해 1로 변경

                    SetCommandResult(commandTag.Identity, OpenClose.Open);

                    IsOpened = true;

                    initModuleInfo();

                    StartUpdateProc();

                    Logger.Infomation("Ajin Open");
                }
                else
                {
                    SetCommandResult(commandTag.Identity, OpenClose.Close);

                    IsOpened = false;

                    Logger.Error("Ajin Open Failure [Error Code : " + FuncRt + "]");
                }
            }

            return (int)FuncRt;
        }

        public int[] ToIntArray(object data, int size)
        {
            string binary = Convert.ToString(Convert.ToInt32(data.ToString(), 10), 2);
            binary = binary.PadLeft(size, '0');

            char[] array = binary.ToCharArray();

            int[] ret = new int[array.Length];

            int index = 0;
            for (int i = ret.Length - 1; i >= 0; i--)
            {
                ret[index] = Convert.ToInt32(array[i].ToString());
                index++;
            }

            return ret;
        }

        public bool UpdateAi(EmModuleInfo em)
        {
            if (CheckStatus(true) == false)
            {
                return false;
            }

            int size = em.AiCount;
            int[] channel = new int[em.AiCount];
            uint[] upDigits = new uint[em.AiCount];

            FuncRt = CAXA.AxaiSwReadMultiDigit(size, channel, upDigits);

            if (FuncRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                var tag = (from item in _xTags
                           where item.Value.Identity.Offset == channel[i]
                           && item.Value.Identity.DataType == Tag.Idx.DataType.Double
                           && item.Value.Identity.IoType == Tag.Idx.IoType.Input
                           select item).FirstOrDefault();

                if (tag.Value != null)
                {
                    short upDigit = (short)upDigits[i]; //부호있는 2byte로 변환.
                    double ai;

                    int ai_unit_type = GetAiUnitType(tag.Value.Identity.ExeCmd);
                    if (ai_unit_type == AiUnitType.Digital) // real(ADC raw) data
                    {
                        ai = upDigit;
                    }
                    else if (ai_unit_type == AiUnitType.Voltage) // convert to voltage
                    {
                        ai = GetVoltage(upDigit);
                    }
                    else if (ai_unit_type == AiUnitType.kPa) // convert to kpa
                    {
                        ai = GetkPa(tag.Value.Identity.ExeCmd, GetVoltage(upDigit)); // service value 변환.
                    }
                    else
                    {
                        ai = upDigit;
                    }

                    if (tag.Value.d != ai)
                    {
                        tag.Value.Is = ai;
                    }
                }
            }

            return true;
        }

        public void UpdateStatus() // input update (loop)
        {
            if (CheckStatus(false) == false)
            {
                return;
            }

            foreach (EmModuleInfo em in _Modules.Values)
            {
                if (em.IsReady == false)
                {
                    return;
                }

                if (em.DataType == Tag.Idx.DataType.Int && (em.IoType == Tag.Idx.IoType.Input || em.IoType == Tag.Idx.IoType.Both))
                {
                    if (UpdateDi(em) == false)
                    {
                        break;
                    }
                }
                else if (em.DataType == Tag.Idx.DataType.Double && (em.IoType == Tag.Idx.IoType.Input || em.IoType == Tag.Idx.IoType.Both))
                {
                    if (UpdateAi(em) == false)
                    {
                        break;
                    }
                }
            }
        }

        protected override void DoSimulation(Tag tag)
        {
        }

        protected override void DoUpdate()
        {
            UpdateStatus();
        }

        protected override void Initialize()
        {
            InitInstance();

            if (IsSimulation == true)
            {
                //InitControl();
            }

            RegistEvent();
        }

        protected override void OnCommand(object sendor, EventTagChangedArgs e)
        {            
            Action action = () =>
            {
                Command(e.Tag);
            };

            action.BeginInvoke(null, null);
        }

        protected override void OnSimulation(object sendor, EventTagChangedArgs e)
        {
            Action action = () =>
            {
                RunSimCommand(e.Tag);
            };

            action.BeginInvoke(null, null);
        }

        private EmModuleInfo GetAioModuleInfo(int moduleNo)
        {
            EmModuleInfo module = new EmModuleInfo();

            module.No = moduleNo;
            module.Name = moduleNo.ToString();
            module.DataType = Tag.Idx.DataType.Double;

            FuncRt = CAXA.AxaInfoGetModule(module.No, ref module.BoardNo, ref module.Index, ref module.Id);
            if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                FuncRt = CAXA.AxaInfoGetInputCount(module.No, ref module.AiCount);
                FuncRt = CAXA.AxaInfoGetOutputCount(module.No, ref module.AoCount);

                if (module.AiCount > 0 && module.AoCount > 0)
                    module.IoType = Tag.Idx.IoType.Both;
                else if (module.AiCount > 0 && module.AoCount == 0)
                    module.IoType = Tag.Idx.IoType.Input;
                else if (module.AiCount == 0 && module.AoCount > 0)
                    module.IoType = Tag.Idx.IoType.Output;

                module.IsReady = true;
            }
            else
            {
                module.IsReady = false;
            }

            // if count get function error, return null.
            if (FuncRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                module.IsReady = false;

            return module;
        }

        private int GetAiUnitType(string exeCommand)
        {
            string[] list = exeCommand.Split(new char[] { '/' });

            if (list == null)
                return AiUnitType.Voltage;

            string unit_name = list[0].Replace(" ", "").ToUpper();

            if (unit_name == nameof(AiUnitType.Digital).ToUpper())
                return AiUnitType.Digital;
            else if (unit_name == nameof(AiUnitType.Voltage).ToUpper())
                return AiUnitType.Voltage;
            else if (unit_name == nameof(AiUnitType.kPa).ToUpper())
                return AiUnitType.kPa;
            else if (unit_name == nameof(AiUnitType.mPa).ToUpper())
                return AiUnitType.mPa;

            return AiUnitType.Voltage;
        }

        private EmModuleInfo GetDioModuleInfo(int moduleNo)
        {
            EmModuleInfo module = new EmModuleInfo();

            module.No = moduleNo;
            module.Name = moduleNo.ToString();
            module.DataType = Tag.Idx.DataType.Int;

            FuncRt = CAXD.AxdInfoGetModule(moduleNo, ref module.BoardNo, ref module.Index, ref module.Id);
            if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                FuncRt = CAXD.AxdInfoGetInputCount(module.No, ref module.DiCount);
                FuncRt = CAXD.AxdInfoGetOutputCount(module.No, ref module.DoCount);

                if (module.DiCount > 0 && module.DoCount > 0)
                    module.IoType = Tag.Idx.IoType.Both;
                else if (module.DiCount > 0 && module.DoCount == 0)
                    module.IoType = Tag.Idx.IoType.Input;
                else if (module.DiCount == 0 && module.DoCount > 0)
                    module.IoType = Tag.Idx.IoType.Output;

                module.IsReady = true;
            }
            else
            {
                module.IsReady = false;
            }

            if (FuncRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                module.IsReady = false;
            }

            return module;
        }

        private double GetkPa(string exeCommand, double voltage)
        {
            string[] ps = exeCommand.Split(new char[] { '/' });

            if (ps == null)
                return voltage;
            else if (ps.Length != 3)
                return voltage;

            string unit = string.Empty;
            double slope, y_intercept;

            try
            {
                unit = ps[0].Replace(" ", "");
                slope = Convert.ToDouble(ps[1].Replace(" ", ""));
                y_intercept = Convert.ToDouble(ps[2].Replace(" ", ""));
            }
            catch
            {
                // 잘못 입력했을 경우 default로 사용. 반드시 튜닝값 적용되어야 함.
                slope = 27.927776476508;
                y_intercept = 48.5;
            }

            if (unit.ToUpper() != "KPA" || ps.Length != 3)
                return voltage;

            return GetkPa(slope, voltage, y_intercept);
        }

        private bool GetRelatedTag(string configRelatedTag, ref RelatedTag relatedTags)
        {
            if (configRelatedTag == string.Empty)
            {
                return false;
            }

            relatedTags = new RelatedTag();

            try
            {
                string[] properties = configRelatedTag.Split('/');

                relatedTags.Name = properties[0];
                relatedTags.Type = properties[1];
                relatedTags.DefaultValue = properties[2];
                relatedTags.ReActValue = properties[3];
                relatedTags.DelayTime = properties[4];

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private int GetStartBitOffset(int moduleIndex)
        {
            int offset = 0;

            for (int i = 0; i < moduleIndex; i++)
            {
                if (_Modules[i].DataType == Tag.Idx.DataType.Int && _Modules[i].IoType == Tag.Idx.IoType.Input)
                {
                    offset += _Modules[i].DiCount;
                }
            }

            return offset;
        }

        private Tag GetTag(string propertyName)
        {
            Tag temp = _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
            return temp;
        }

        private double GetVoltage(short signedDigit)
        {
            return (((double)signedDigit) / (double)(short.MaxValue)) * 10.0f;
        }

        private void InitControl()
        {
            FrmEmulator fm = new FrmEmulator(_xTags, _yTags);
            fm.Show();
        }

        private void Initialization()
        {
        }

        private void InitInstance()
        {
            if (base._tags != null)
            {
                if (base._tags.Count > 0)
                {
                    _xTags = (from item in base._tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Input
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusInput
                              select item).ToDictionary((KeyValuePair<string, Tag> x) => x.Key, (KeyValuePair<string, Tag> y) => y.Value);

                    _yTags = (from item in base._tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Output
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusOutput
                              select item).ToDictionary((KeyValuePair<string, Tag> x) => x.Key, (KeyValuePair<string, Tag> y) => y.Value);
                }
            }
        }

        private void initModuleInfo()
        {
            _Modules = new Dictionary<int, EmModuleInfo>();

            int module_count = 0;

            // digital i/o modules
            if (IsExsitDioModule)
            {
                module_count = DioModuleCount;
                for (int no = 0; no < module_count; no++)
                {
                    if (_Modules.ContainsKey(no) == false)
                    {
                        _Modules.Add(no, GetDioModuleInfo(no));
                    }
                }
            }

            // analog channel modules
            if (IsExsitAioModule)
            {
                module_count = AioModuleCount;
                for (int no = 0; no < module_count; no++)
                {
                    if (_Modules.ContainsKey(no) == false)
                    {
                        _Modules.Add(no, GetAioModuleInfo(no));
                    }
                }
            }
        }

        private void RegistEvent()
        {
            if (_xTags != null)
            {
                foreach (var tag in _xTags)
                {
                    tag.Value.IsSimulation = IsSimulation;

                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    }
                }
            }

            if (_yTags != null)
            {
                foreach (var tag in _yTags)
                {
                    tag.Value.IsSimulation = IsSimulation;

                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    }

                    tag.Value.OnCommand += new EventTagChanged(OnCommand);
                }
            }
        }

        private void RunSimCommand(Tag commandTag)
        {
            if (commandTag.Identity.IoType == Tag.Idx.IoType.StatusOutput)
            {
                SetCommandResult(commandTag.Identity, commandTag.Is);
            }
            else if (commandTag.Identity.IoType == Tag.Idx.IoType.Output)
            {
                string reActValue = string.Empty;

                if (commandTag.Identity.SimCmd == null)
                {
                    return;
                }

                string[] commands = commandTag.Identity.SimCmd.Split(';');

                foreach (string command in commands)
                {
                    if (SetRelatedTagCommand(command, commandTag.Is, commandTag) == false)
                    {
                        break;
                    }
                }
            }
        }

        private void SetCommandResult(TagIdentity tag, object value)
        {
            RelatedTag relatedTag = new RelatedTag();
            if (GetRelatedTag(tag.SimCmd, ref relatedTag) == true)
            {
                GetTag(relatedTag.Name).Is = value;
            } // else
        }

        private bool SetRelatedTagCommand(string command, object value, Tag tag)
        {
            try
            {
                if (command != null && command.ToString() != string.Empty)
                {
                    object reActValue;
                    RelatedTag relatedTag = new RelatedTag();
                    if (GetRelatedTag(command, ref relatedTag) == true)
                    {
                        int Delaytime = Convert.ToInt32(relatedTag.DelayTime);
                        Thread.Sleep(Delaytime);

                        string type = relatedTag.Type;

                        if (type == CommandTagType.Invert)
                        {
                            reActValue = (value.ToString() == "1") ? "0" : "1";
                        }
                        else if (type == CommandTagType.ReActValueOut)
                        {
                            // SimCmd에서 읽은 값을 내린다.
                            reActValue = relatedTag.ReActValue;
                        }
                        else if (type == CommandTagType.AlwaysZero)
                        {
                            reActValue = "0"; // 무조건
                        }
                        else if (type == CommandTagType.AlwaysOne)
                        {
                            reActValue = "1"; // 무조건
                        }
                        else
                        {
                            // type : Defalult
                            reActValue = value;
                        }

                        Tag t = GetTag(relatedTag.Name);

                        if (t.Is != null)
                        {
                            Type temp = t.Is.GetType();
                            reActValue = Convert.ChangeType(reActValue, temp);
                        }

                        t.Is = reActValue;
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }

        private bool UpdateDi(EmModuleInfo em)
        {
            uint up_bits = 0;

            if (em.DiCount == 16)
            {
                FuncRt = CAXD.AxdiReadInportWord(em.Index, 0, ref up_bits);
            }
            else if (em.DiCount == 32)
            {
                FuncRt = CAXD.AxdiReadInportDword(em.Index, 0, ref up_bits);
            }

            if (FuncRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                Logger.Error("Failed the UpdateDi");
                return false;
            }

            int start_bit_offset = GetStartBitOffset(em.Index);

            int[] temp = ToIntArray(up_bits, em.DiCount);

            for (int n = 0; n < em.DiCount; n++)
            {
                var tag = (from item in _xTags
                           where item.Value.Identity.Offset == (start_bit_offset + n)
                           && item.Value.Identity.DataType == Tag.Idx.DataType.Int
                           && item.Value.Identity.IoType == Tag.Idx.IoType.Input
                           select item).FirstOrDefault();

                if (tag.Value != null)
                {
                    if (tag.Value.n != temp[n])
                    {
                        tag.Value.Is = temp[n];
                    }
                }
            }

            return true;
        }

        #endregion Methods
    }
}