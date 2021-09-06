using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public partial class EmAjinMotion : DriverBase
    {
        public uint FuncRt;       
        private Dictionary<string, Tag> _data;

        public EmAjinMotion(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {
            _data = tags;

            Initialization();
        }

        /// <summary>
        /// Module Information obtained from Ajin Lib
        /// </summary>
        public Dictionary<int, EmModuleInfo> _Modules { get; protected set; }

        public Dictionary<int, EmMotionProperty> _MotionPropertys { get; protected set; }

        public int AxisCount
        {
            get
            {
                int count = 0;
                
                FuncRt = CAXM.AxmInfoGetAxisCount(ref count);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    return count;
                else
                    return 0;
            }
        }

        /// <summary>
        /// motion moduls 유무 확인.
        /// </summary>
        public bool IsExsitMotionModule
        {
            get
            {
                uint exsit = 0;

                FuncRt = CAXM.AxmInfoIsMotionModule(ref exsit);

                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    return exsit == (uint)AXT_EXISTENCE.STATUS_EXIST ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AddModule(int index, EmModuleInfo moduleInfo)
        {
            if (_Modules == null)
            {
                _Modules = new Dictionary<int, EmModuleInfo>();
            }

            if (_Modules.ContainsKey(index) == false)
            {
                _Modules.Add(index, moduleInfo);
            }
        }

        public override int Close(Tag commandTag)
        {
            try
            {
                if (IsSimulation == true)
                {
                    return FunctionResult.True;
                }

                FuncRt = FunctionResult.True;

                bool isOpened = CAXL.AxlIsOpened() == 1 ? true : false; // Open되어 있을 경우 1, 아니면 0 리턴.
                if (isOpened)
                {
                    FuncRt = (uint)CAXL.AxlClose();
                }

                Logger.Debug("close result=" + FuncRt);

                return (int)FuncRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

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
                    RunCommand(commandTag);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public override int Open(Tag commandTag)
        {
            try
            {
                IsOpened = CAXL.AxlIsOpened() == 1 ? true : false; // Open되어 있을 경우 1, 아니면 0 리턴.

                if (IsOpened == false)
                {
                    // Library open 상태 확인, 초기화.
                    FuncRt = CAXL.AxlOpen(7);
                    
                    if (FuncRt != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        SetCommandResult(commandTag.Identity, OpenClose.Close);

                        IsOpened = false;

                        Logger.Error("Ajin Open Failure [Error Code : " + FuncRt + "]");

                        return (int)FuncRt;
                    }
                }
                else
                {
                    // 이미 Open되어 있음.
                    Logger.Infomation("It's already open.");
                }

                // Opne 성공
                initModuleInfo();

                StartUpdateProc();

                SetCommandResult(commandTag.Identity, OpenClose.Open);

                IsOpened = true;                
                FuncRt = FunctionResult.True;// Ajin의 성공은 0이지만 Code 통일을 위해 1로 변경

                Logger.Infomation("Ajin Open and Start updateProc");

                return (int)FuncRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return FunctionResult.False;
            }
        }

        public void UpdateStatus(Tag tag)
        {
            if (tag.Identity.IoType != EmWorks.Lib.Core.Tag.Idx.IoType.Input || !_xTags.ContainsKey(tag.Name) || !IsOpened)
                return;

            int axisno = tag.Identity.Index;
            
            string protocols = tag.Identity.ExeCmd;
            if (!_Modules.ContainsKey(axisno))
                return;

            uint dv = 0; // digital value
            double av = 0; // analog value
            ushort av1 = 0; // analog value
            uint av2 = 0; // analog value
            string sv = string.Empty; // string value;
            uint result = 0;
            
            if (protocols == nameof(CAXM.AxmSignalIsServoOn)) // servo on status
                result = CAXM.AxmSignalIsServoOn(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmSignalReadServoAlarm)) // slamr status
                result = CAXM.AxmSignalReadServoAlarm(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmSignalReadServoAlarmCode)) // alarm code
            {
                result = CAXM.AxmSignalReadServoAlarmCode(axisno, ref av1);
                av = (double)av1;
            }
            else if (protocols == nameof(CAXM.AxmHomeReadSignal)) // home signal
                result = CAXM.AxmHomeReadSignal(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmSignalReadLimitP)) // positive limit signal
                result = CAXM.AxmSignalReadLimitP(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmSignalReadLimitN)) // negative limit signal
                result = CAXM.AxmSignalReadLimitN(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmStatusReadInMotion)) // buzy signal
                result = CAXM.AxmStatusReadInMotion(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmStatusGetCmdPos)) // command position
                result = CAXM.AxmStatusGetCmdPos(axisno, ref av);
            else if (protocols == nameof(CAXM.AxmStatusGetActPos)) // actual position
                result = CAXM.AxmStatusGetActPos(axisno, ref av);
            else if (protocols == nameof(CAXM.AxmSignalReadInpos)) // inposition
                result = CAXM.AxmSignalReadInpos(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmSignalGetInposRange))
            {
                result = CAXM.AxmSignalGetInposRange(axisno, ref av);
                if (result == (uint)AXT_FUNC_RESULT.AXT_RT_NOT_SUPPORT_VERSION)
                    return; // evi 설비에 사용되는 motion board 는 지원하기 않음.
            }
            else if (protocols == nameof(CAXM.AxmSignalGetInpos))
            {
                result = CAXM.AxmSignalGetInpos(axisno, ref av2);
                av = (double)av2;
            }
            else if (protocols == nameof(CAXM.AxmHomeGetResult))
            {
                result = CAXM.AxmHomeGetResult(axisno, ref av2);
                av = (double)av2;
            }
            else if (protocols == nameof(CAXM.AxmMotGetAbsRelMode)) // read move mode
                result = CAXM.AxmMotGetAbsRelMode(axisno, ref dv);
            else if (protocols == nameof(CAXM.AxmMotGetMaxVel)) // 
                result = CAXM.AxmMotGetMaxVel(axisno, ref av);
            else if (protocols == nameof(CAXM.AxmMotGetMinVel)) // 
                result = CAXM.AxmMotGetMinVel(axisno, ref av);
            else if (protocols == nameof(CAXM.AxmHomeGetVel)) // 
                result = HomeGetVel(tag);
            else
            {
                av = GetMotionProperty(protocols, axisno);
            } // end if protocols item select.

            if (result == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                if (tag.Identity.DataType == EmWorks.Lib.Core.Tag.Idx.DataType.Int)
                {
                    if (tag.n != (int)dv)
                    {
                        tag.Is = (int)dv;
                    }
                }
                else if (tag.Identity.DataType == EmWorks.Lib.Core.Tag.Idx.DataType.Double)
                {
                    if (tag.d != av)
                    {
                        tag.Is = av;
                    }
                }
                else if (tag.Identity.DataType == EmWorks.Lib.Core.Tag.Idx.DataType.String)
                {
                    if (tag.s != sv)
                        tag.Is = sv;
                }
            }

        }

        protected override void DoSimulation(Tag tag)
        {
            // N/A
        }

        protected override void DoUpdate()
        {
            foreach (var item in _xTags)
                UpdateStatus(item.Value);
        }

        protected override void Initialize()
        {
            InitInstance();
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

        private void AddMotionProferty(int index)
        {
            if (_MotionPropertys == null)
            {
                _MotionPropertys = new Dictionary<int, EmMotionProperty>();
            }

            if (_MotionPropertys.ContainsKey(index) == false)
            {
                EmMotionProperty emp = new EmMotionProperty();
                emp.AxisIndex = index;

                _MotionPropertys.Add(index, emp);
            }
        }

        private EmModuleInfo GetAxisInfo(int moduleNo)
        {
            EmModuleInfo module = new EmModuleInfo();
            module.No = moduleNo;
            int remove_length = 8;

            FuncRt = CAXM.AxmInfoGetAxis(module.No, ref module.BoardNo, ref module.Index, ref module.Id);

            if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                module.Name = Enum.GetName(typeof(AXT_MODULE), module.Id).Remove(0, remove_length);
                module.IsReady = true;
            }
            else
            {
                module.IsReady = false;
            }

            return module;
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

        private Tag GetTag(string propertyName)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Name == propertyName).Value;
            return temp;
        }

        private Tag GetTag(string unit, string exeCmd)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.Unit == unit &&
                                            x.Value.Identity.ExeCmd == exeCmd).Value;
            return temp;
        }

        private void Initialization()
        {
        }

        private void InitInstance()
        {
            if (_tags != null)
            {
                if (_tags.Count > 0)
                {
                    _xTags = (from item in _tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Input
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusInput
                              select item).ToDictionary(x => x.Key, y => y.Value);

                    _yTags = (from item in _tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Output
                              || item.Value.Identity.IoType == (int)Tag.Idx.IoType.StatusOutput
                              select item).ToDictionary(x => x.Key, y => y.Value);
                }
            }
        }

        private void initModuleInfo()
        {
            _Modules = new Dictionary<int, EmModuleInfo>();

            int module_count = 0;

            if (_Modules.Count > 0)
            {
                _Modules.Clear();
            }

            if (IsExsitMotionModule == true)
            {
                module_count = AxisCount;
                for (int no = 0; no < module_count; no++)
                {
                    AddModule(no, GetAxisInfo(no));
                    AddMotionProferty(no);
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

        private string GetCurrentDirectory()
        {            
            return Environment.CurrentDirectory;           
        }

        private void RunCommand(Tag commandTag)
        {
            int axisno = commandTag.Identity.Index;
            
            string protocols = commandTag.Identity.ExeCmd;

            uint result = 0;

            if (protocols == nameof(CAXM.AxmSignalServoOn)) // servo on command
            {
                // Servo ON시에 Mot 파일 로드                
                string filePath = GetCurrentDirectory() + @"\MotionDefault.mot";

                result = CAXM.AxmMotLoadParaAll(filePath);

                if (result == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    result = CAXM.AxmSignalServoOn(axisno, (uint)commandTag.n);
                }
            }
            else if (protocols == nameof(CAXM.AxmSignalServoAlarmReset))
                result = CAXM.AxmSignalServoAlarmReset(axisno, 1);
            else if (protocols == nameof(CAXM.AxmHomeSetStart))
                result = HomeSetStart(commandTag);
            else if (protocols == nameof(CAXM.AxmStatusSetPosMatch))
                result = CAXM.AxmStatusSetPosMatch(axisno, 0.0); // library version 최신꺼 사용해야 함.
            else if (protocols == nameof(CAXM.AxmMoveStop))
                result = CAXM.AxmMoveStop(axisno, commandTag.d); // delcel 값이 너무 낮을 경우 처리 필요할 것 같음!
            else if (protocols == nameof(CAXM.AxmMoveEStop))
                result = CAXM.AxmMoveEStop(axisno);
            else if (protocols == nameof(CAXM.AxmMoveSStop))
                result = CAXM.AxmMoveSStop(axisno);
            else if (protocols == nameof(CAXM.AxmSignalSetInposRange))
            {
                result = CAXM.AxmSignalSetInposRange(axisno, commandTag.d);
                if (result == (uint)AXT_FUNC_RESULT.AXT_RT_NOT_SUPPORT_VERSION)
                {
                    Logger.Error(" AXT_FUNC_RESULT : " + result.ToString());
                    return; // evi 설비에 사용되는 motion board 는 지원하기 않음.
                }
            }
            else if (protocols == nameof(CAXM.AxmSignalSetInpos))
                result = CAXM.AxmSignalSetInpos(axisno, (uint)commandTag.n);
            else if (protocols == nameof(CAXM.AxmMotSetAbsRelMode))
                result = CAXM.AxmMotSetAbsRelMode(axisno, (uint)commandTag.n);
            else if (protocols == nameof(CAXM.AxmStatusSetCmdPos))
                result = CAXM.AxmStatusSetCmdPos(axisno, commandTag.d);
            else if (protocols == nameof(CAXM.AxmMotSetMinVel))
                result = CAXM.AxmMotSetMinVel(axisno, commandTag.d);
            else if (protocols == nameof(CAXM.AxmMotSetMaxVel))
                result = CAXM.AxmMotSetMaxVel(axisno, commandTag.d);
            else if (protocols == nameof(CAXM.AxmMoveStartPos))
            {
                result = MoveStartPos(commandTag);
            }
            else if (protocols == nameof(CAXM.AxmMoveVel))
            {
                result = MoveVel(commandTag);
            }
            else if (protocols == nameof(CAXM.AxmHomeSetVel)) //
            {
                result = HomeSetVel(commandTag);
            }
            else if (protocols == nameof(CAXM.AxmTriggerSetTimeLevel))
            {
                result = TriggerSetTimeLevel(commandTag);
            }
            else if (protocols == nameof(CAXM.AxmTriggerSetBlock))
            {
                result = TriggerSetBlock(commandTag);
            }
            else if (protocols == nameof(CAXM.AxmTriggerSetReset))
            {
                result = CAXM.AxmTriggerSetReset(axisno);
            }
            else
            {
                // 그 외는 Property 설정이다.
                SetMotionProperty(protocols, axisno, commandTag);
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

        private double GetMotionProperty(string protocols, int axisno)
        {
            double result = 0;
            switch (protocols)
            {
                case "CmdPos":
                    result = _MotionPropertys[axisno].CmdPos;
                    break;
                case "Velocity":
                    result = _MotionPropertys[axisno].Velocity;
                    break;
                case "Accel":
                    result = _MotionPropertys[axisno].Accel;
                    break;
                case "Decel":
                    result = _MotionPropertys[axisno].Decel;
                    break;
                case "HomeVel1st":
                    result = _MotionPropertys[axisno].yHomeVelocity1;
                    break;
                case "HomeVel2nd":
                    result = _MotionPropertys[axisno].yHomeVelocity2;
                    break;
                case "HomeVel3th":
                    result = _MotionPropertys[axisno].yHomeVelocity3;
                    break;
                case "HomeVel4th":
                    result = _MotionPropertys[axisno].yHomeVelocity4;
                    break;
                case "HomeAcc1st":
                    result = _MotionPropertys[axisno].yHomeAccel1;
                    break;
                case "HomeAcc2nd":
                    result = _MotionPropertys[axisno].yHomeAccel2;
                    break;
                case "JogAccel":
                    result = _MotionPropertys[axisno].JogAccel;
                    break;
                case "JogDecel":
                    result = _MotionPropertys[axisno].JogDecel;

                    break;
                case "TriggerTime":
                    result = _MotionPropertys[axisno].TriggerTime;
                    break;

                case "TriggerStartPos":
                    result = _MotionPropertys[axisno].TriggerStartPos;
                    break;

                case "TriggerEndPos":
                    result = _MotionPropertys[axisno].TriggerEndPos;
                    break;

                case "TriggerPeriodPos":
                    result = _MotionPropertys[axisno].TriggerPeriodPos;
                    break;

                default:
                    // Todo : 실제 사용시에 다시 추가 해야함.
                    //Logger.Error("Check a protocol. [" + protocols + "]");
                    break;
            }

            return result;
        }

        private void SetMotionProperty(string protocols, int axisno, Tag _tag)
        {
            switch (protocols)
            {
                case "CmdPos":
                    _MotionPropertys[axisno].CmdPos = _tag.d;
                    break;

                case "Velocity":
                    _MotionPropertys[axisno].Velocity = _tag.d;
                    break;

                case "Accel":
                    _MotionPropertys[axisno].Accel = _tag.d;
                    break;

                case "Decel":
                    _MotionPropertys[axisno].Decel = _tag.d;
                    break;

                case "HomeVel1st":
                    _MotionPropertys[axisno].yHomeVelocity1 = _tag.d;
                    break;

                case "HomeVel2nd":
                    _MotionPropertys[axisno].yHomeVelocity2 = _tag.d;
                    break;

                case "HomeVel3rd":
                    _MotionPropertys[axisno].yHomeVelocity3 = _tag.d;
                    break;

                case "HomeVel4th":
                    _MotionPropertys[axisno].yHomeVelocity4 = _tag.d;
                    break;

                case "HomeAcc1st":
                    _MotionPropertys[axisno].yHomeAccel1 = _tag.d;
                    break;

                case "HomeAcc2nd":
                    _MotionPropertys[axisno].yHomeAccel2 = _tag.d;
                    break;

                case "JogAccel":
                    _MotionPropertys[axisno].JogAccel = _tag.d;
                    break;

                case "JogDecel":
                    _MotionPropertys[axisno].JogDecel = _tag.d;
                    break;

                case "TriggerTime":
                    _MotionPropertys[axisno].TriggerTime = _tag.d;
                    break;

                case "TriggerStartPos":
                    _MotionPropertys[axisno].TriggerStartPos = _tag.d;
                    break;

                case "TriggerEndPos":
                    _MotionPropertys[axisno].TriggerEndPos = _tag.d;
                    break;

                case "TriggerPeriodPos":
                    _MotionPropertys[axisno].TriggerPeriodPos = _tag.d;
                    break;

                default:
                    Logger.Error("Check a protocol. [" + protocols + "]");
                    break;
            }
        }

        private void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }

    }
}