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

        public Dictionary<int, EmMotionProperty> _MotionProperty { get; protected set; }

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

            switch (protocols)
            {
                case ExeCmd.Version:
                    sv = "1.0.0.0";
                    break;
                case ExeCmd.ExtenalLibVersion:
                    byte bt = new byte();
                    result = CAXL.AxlGetLibVersion(ref bt);
                    sv = bt.ToString();
                    break;
                case ExeCmd.SignalPositiveLimit:
                    result = CAXM.AxmSignalReadLimitP(axisno, ref dv);
                    break;
                case ExeCmd.SignalHome:
                    result = CAXM.AxmHomeReadSignal(axisno, ref dv);
                    break;
                case ExeCmd.SignalNegativeLimit:
                    result = CAXM.AxmSignalReadLimitN(axisno, ref dv);
                    break;
                case ExeCmd.SignalInpos:
                    result = CAXM.AxmSignalReadInpos(axisno, ref dv);
                    break;
                case ExeCmd.SignalBusy:
                    result = CAXM.AxmStatusReadInMotion(axisno, ref dv);
                    break;
                case ExeCmd.ServoOn:
                    result = CAXM.AxmSignalIsServoOn(axisno, ref dv);
                    break;
                case ExeCmd.Alarm:
                    result = CAXM.AxmSignalReadServoAlarm(axisno, ref dv);
                    break;
                case ExeCmd.AlarmCode:
                    result = CAXM.AxmSignalReadServoAlarmCode(axisno, ref av1);
                    av = (double)av1;
                    break;
                case ExeCmd.Homing:
                    result = CAXM.AxmHomeGetResult(axisno, ref av2);
                    av = (double)av2;
                    break;                
                case ExeCmd.InpositionEnable:
                    result = CAXM.AxmSignalGetInpos(axisno, ref av2);
                    av = (double)av2;
                    break;
                case ExeCmd.InposRange:
                    result = CAXM.AxmSignalGetInposRange(axisno, ref av);
                    if (result == (uint)AXT_FUNC_RESULT.AXT_RT_NOT_SUPPORT_VERSION)
                        return; // evi 설비에 사용되는 motion board 는 지원하기 않음.
                    break;
                case ExeCmd.MoveMode:
                    result = CAXM.AxmMotGetAbsRelMode(axisno, ref dv);
                    break;
                case ExeCmd.CommandPosisiton:
                    result = CAXM.AxmStatusGetCmdPos(axisno, ref av);
                    break;
                case ExeCmd.ActualPosition:
                    result = CAXM.AxmStatusGetActPos(axisno, ref av);
                    break;
                case ExeCmd.MinVel:
                    result = CAXM.AxmMotGetMinVel(axisno, ref av);
                    break;
                case ExeCmd.MaxVel:
                    result = CAXM.AxmMotGetMaxVel(axisno, ref av);
                    break;                
                case ExeCmd.HomingVel1st:
                    result = HomeGetVel(tag);
                    break;                
                default:
                    av = GetMotionProperty(protocols, axisno);
                    break;
            }            

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
            if (_MotionProperty == null)
            {
                _MotionProperty = new Dictionary<int, EmMotionProperty>();
            }

            if (_MotionProperty.ContainsKey(index) == false)
            {
                EmMotionProperty emp = new EmMotionProperty();
                emp.AxisIndex = index;

                _MotionProperty.Add(index, emp);
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

        private Tag GetxTag(string unit, string exeCmd)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == Tag.Idx.IoType.Input &&
                                            x.Value.Identity.Unit == unit &&
                                            x.Value.Identity.ExeCmd == exeCmd).Value;
            return temp;
        }

        private Tag GetyTag(string unit, string exeCmd)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == Tag.Idx.IoType.Output &&
                                            x.Value.Identity.Unit == unit &&
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

            switch (protocols)
            {
                case ExeCmd.ServoOn:
                    // Servo ON시에 Mot 파일 로드                
                    string filePath = GetCurrentDirectory() + @"\MotionDefault.mot";

                    result = CAXM.AxmMotLoadParaAll(filePath);

                    if (result == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    {
                        result = CAXM.AxmSignalServoOn(axisno, (uint)commandTag.n);
                    } // else
                    break;
                case ExeCmd.AlarmReset:
                    result = CAXM.AxmSignalServoAlarmReset(axisno, 1);
                    break;                
                case ExeCmd.Homing:
                    result = HomeSetStart(commandTag);
                    break;
                case ExeCmd.PositionMatch:
                    result = CAXM.AxmStatusSetPosMatch(axisno, 0.0); // library version 최신꺼 사용해야 함.
                    break;
                case ExeCmd.Stop:
                    result = CAXM.AxmMoveStop(axisno, commandTag.d); // delcel 값이 너무 낮을 경우 처리 필요할 것 같음!
                    break;
                case ExeCmd.EStop:
                    result = CAXM.AxmMoveEStop(axisno);
                    break;
                case ExeCmd.SStop:
                    result = CAXM.AxmMoveSStop(axisno);
                    break;
                case ExeCmd.InpositionEnable:
                    result = CAXM.AxmSignalSetInpos(axisno, (uint)commandTag.n);
                    break;
                case ExeCmd.InposRange:
                    result = CAXM.AxmSignalSetInposRange(axisno, commandTag.d);
                    if (result == (uint)AXT_FUNC_RESULT.AXT_RT_NOT_SUPPORT_VERSION)
                    {
                        Logger.Error(" AXT_FUNC_RESULT : " + result.ToString());
                        return; // evi 설비에 사용되는 motion board 는 지원하기 않음.
                    }
                    break;
                case ExeCmd.MoveModeSelect:
                    result = CAXM.AxmMotSetAbsRelMode(axisno, (uint)commandTag.n);
                    break;
                case ExeCmd.MoveStart:
                    result = MoveStartPos(commandTag);
                    break;
                case ExeCmd.CommandPosisiton:
                    result = CAXM.AxmStatusSetCmdPos(axisno, commandTag.d);
                    break;
                case ExeCmd.MinVel:
                    result = CAXM.AxmMotSetMinVel(axisno, commandTag.d);
                    break;
                case ExeCmd.MaxVel:
                    result = CAXM.AxmMotSetMaxVel(axisno, commandTag.d);
                    break;
                case ExeCmd.HomingVel:
                    result = HomeSetVel(commandTag);
                    break;                
                case ExeCmd.JogVelocity:
                    result = MoveVel(commandTag);
                    break;
                case ExeCmd.TriggerSetBlock:
                    result = TriggerSetBlock(commandTag);
                    break;
                case ExeCmd.TriggerSetReset:
                    result = CAXM.AxmTriggerSetReset(axisno);
                    break;
                case ExeCmd.TriggerSetTimeLevel:
                    result = TriggerSetTimeLevel(commandTag);
                    break;
                default:
                    // 그 외는 Property 설정이다.
                    SetMotionProperty(protocols, axisno, commandTag);
                    break;
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
                    result = _MotionProperty[axisno].CmdPos;
                    break;
                case "Velocity":
                    result = _MotionProperty[axisno].Velocity;
                    break;
                case "Accel":
                    result = _MotionProperty[axisno].Accel;
                    break;
                case "Decel":
                    result = _MotionProperty[axisno].Decel;
                    break;
                case "HomeVel1st":
                    result = _MotionProperty[axisno].yHomeVelocity1;
                    break;
                case "HomeVel2nd":
                    result = _MotionProperty[axisno].yHomeVelocity2;
                    break;
                case "HomeVel3th":
                    result = _MotionProperty[axisno].yHomeVelocity3;
                    break;
                case "HomeVel4th":
                    result = _MotionProperty[axisno].yHomeVelocity4;
                    break;
                case "HomeAcc1st":
                    result = _MotionProperty[axisno].yHomeAccel1;
                    break;
                case "HomeAcc2nd":
                    result = _MotionProperty[axisno].yHomeAccel2;
                    break;
                case "JogAccel":
                    result = _MotionProperty[axisno].JogAccel;
                    break;
                case "JogDecel":
                    result = _MotionProperty[axisno].JogDecel;

                    break;
                case "TriggerTime":
                    result = _MotionProperty[axisno].TriggerTime;
                    break;

                case "TriggerStartPos":
                    result = _MotionProperty[axisno].TriggerStartPos;
                    break;

                case "TriggerEndPos":
                    result = _MotionProperty[axisno].TriggerEndPos;
                    break;

                case "TriggerPeriodPos":
                    result = _MotionProperty[axisno].TriggerPeriodPos;
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
                    _MotionProperty[axisno].CmdPos = _tag.d;
                    break;

                case "Velocity":
                    _MotionProperty[axisno].Velocity = _tag.d;
                    break;

                case "Accel":
                    _MotionProperty[axisno].Accel = _tag.d;
                    break;

                case "Decel":
                    _MotionProperty[axisno].Decel = _tag.d;
                    break;

                case "HomeVel1st":
                    _MotionProperty[axisno].yHomeVelocity1 = _tag.d;
                    break;

                case "HomeVel2nd":
                    _MotionProperty[axisno].yHomeVelocity2 = _tag.d;
                    break;

                case "HomeVel3rd":
                    _MotionProperty[axisno].yHomeVelocity3 = _tag.d;
                    break;

                case "HomeVel4th":
                    _MotionProperty[axisno].yHomeVelocity4 = _tag.d;
                    break;

                case "HomeAcc1st":
                    _MotionProperty[axisno].yHomeAccel1 = _tag.d;
                    break;

                case "HomeAcc2nd":
                    _MotionProperty[axisno].yHomeAccel2 = _tag.d;
                    break;

                case "JogAccel":
                    _MotionProperty[axisno].JogAccel = _tag.d;
                    break;

                case "JogDecel":
                    _MotionProperty[axisno].JogDecel = _tag.d;
                    break;

                case "TriggerTime":
                    _MotionProperty[axisno].TriggerTime = _tag.d;
                    break;

                case "TriggerStartPos":
                    _MotionProperty[axisno].TriggerStartPos = _tag.d;
                    break;

                case "TriggerEndPos":
                    _MotionProperty[axisno].TriggerEndPos = _tag.d;
                    break;

                case "TriggerPeriodPos":
                    _MotionProperty[axisno].TriggerPeriodPos = _tag.d;
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