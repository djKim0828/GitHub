using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.MotionStandard
{
    public partial class EmMotionStandard : DriverBase
    {
        public uint FuncRt;
        private Dictionary<string, Tag> _data;

        public EmMotionStandard(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
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

                // Add your code

                if (FuncRt == FunctionResult.True)
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
                // Add your code

                return true;
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

                // Add your code

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
                // Add your Code
                //IsOpened = CAXL.AxlIsOpened() == 1 ? true : false; // Open되어 있을 경우 1, 아니면 0 리턴.

                if (IsOpened == false)
                {
                    // Library open 상태 확인, 초기화.
                    //Add your Code

                    if (FuncRt != FunctionResult.True)
                    {
                        SetCommandResult(commandTag.Identity, OpenClose.Close);

                        IsOpened = false;

                        Logger.Error("Open Failure [Error Code : " + FuncRt + "]");

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

                Logger.Infomation("Open and Start updateProc");

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

            // Add your Code

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

            //FuncRt = CAXM.AxmInfoGetAxis(module.No, ref module.BoardNo, ref module.Index, ref module.Id);

            if (FuncRt == FunctionResult.True)
            {
                //module.Name = Enum.GetName(typeof(AXT_MODULE), module.Id).Remove(0, remove_length);
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

            switch (protocols)
            {
                case ExeCmd.Version:

                    break;
                case ExeCmd.ExtenalLibVersion:

                    break;
                case ExeCmd.SignalPositiveLimit:

                    break;
                case ExeCmd.SignalHome:

                    break;
                case ExeCmd.SignalNegativeLimit:

                    break;
                case ExeCmd.SignalInpos:

                    break;
                case ExeCmd.SignalBusy:

                    break;
                case ExeCmd.ServoOn:

                    break;
                case ExeCmd.AlarmReset:

                    break;
                case ExeCmd.Alarm:

                    break;
                case ExeCmd.AlarmCode:

                    break;
                case ExeCmd.Homing:

                    break;
                case ExeCmd.PositionMatch:

                    break;
                case ExeCmd.Stop:

                    break;
                case ExeCmd.EStop:

                    break;
                case ExeCmd.SStop:

                    break;
                case ExeCmd.InpositionEnable:

                    break;
                case ExeCmd.InposRange:

                    break;
                case ExeCmd.MoveModeSelect:

                    break;
                case ExeCmd.MoveMode:

                    break;
                case ExeCmd.MoveStart:

                    break;
                case ExeCmd.CommandPosisiton:

                    break;
                case ExeCmd.ActualPosition:

                    break;
                case ExeCmd.MinVel:

                    break;
                case ExeCmd.MaxVel:

                    break;
                case ExeCmd.Velocity:

                    break;
                case ExeCmd.Accel:

                    break;
                case ExeCmd.Decel:

                    break;
                case ExeCmd.HomingVel:

                    break;
                case ExeCmd.HomingVel1st:

                    break;
                case ExeCmd.HomingVel2nd:

                    break;
                case ExeCmd.HomingVel3rd:

                    break;
                case ExeCmd.HomingVel4th:

                    break;
                case ExeCmd.HomingAcc1st:

                    break;
                case ExeCmd.HomingAcc2nd:

                    break;
                case ExeCmd.JogVel:

                    break;
                case ExeCmd.JogAccel:

                    break;
                case ExeCmd.JogDecel:

                    break;
                case ExeCmd.JogVelocity:

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
                case ExeCmd.CommandPosisiton:
                    _MotionPropertys[axisno].CmdPos = _tag.d;
                    break;

                case ExeCmd.Velocity:
                    _MotionPropertys[axisno].Velocity = _tag.d;
                    break;

                case ExeCmd.Accel:
                    _MotionPropertys[axisno].Accel = _tag.d;
                    break;

                case ExeCmd.Decel:
                    _MotionPropertys[axisno].Decel = _tag.d;
                    break;

                case ExeCmd.HomingVel1st:
                    _MotionPropertys[axisno].yHomeVelocity1 = _tag.d;
                    break;

                case ExeCmd.HomingVel2nd:
                    _MotionPropertys[axisno].yHomeVelocity2 = _tag.d;
                    break;

                case ExeCmd.HomingVel3rd:
                    _MotionPropertys[axisno].yHomeVelocity3 = _tag.d;
                    break;

                case ExeCmd.HomingVel4th:
                    _MotionPropertys[axisno].yHomeVelocity4 = _tag.d;
                    break;

                case ExeCmd.HomingAcc1st:
                    _MotionPropertys[axisno].yHomeAccel1 = _tag.d;
                    break;

                case ExeCmd.HomingAcc2nd:
                    _MotionPropertys[axisno].yHomeAccel2 = _tag.d;
                    break;

                case ExeCmd.JogAccel:
                    _MotionPropertys[axisno].JogAccel = _tag.d;
                    break;

                case ExeCmd.JogDecel:
                    _MotionPropertys[axisno].JogDecel = _tag.d;
                    break;

                case ExeCmd.TriggerTime:
                    _MotionPropertys[axisno].TriggerTime = _tag.d;
                    break;

                case ExeCmd.TriggerStartPos:
                    _MotionPropertys[axisno].TriggerStartPos = _tag.d;
                    break;

                case ExeCmd.TriggerEndPos:
                    _MotionPropertys[axisno].TriggerEndPos = _tag.d;
                    break;

                case ExeCmd.TriggerPeriodPos:
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