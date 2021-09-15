using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    public partial class EmAutonicsTK4S : DriverBase
    {
        public enum ExeCmd
        {

        }
        #region Fields

        private Dictionary<string, Tag> _data;

        #endregion Fields

        #region Constructors

        public EmAutonicsTK4S(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {

        }

        #endregion Constructors

        #region Methods

        public override int Close(Tag commandTag)
        {
            try
            {
                if (IsSimulation == true)
                {
                    return FunctionResult.True;
                }

                return FunctionResult.True;
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

        public override int Open(Tag commonTag)
        {
            try
            {
                if (IsOpened == false)
                {
                    //Open
                }
                else
                {
                    // 이미 Open되어 있음.
                    Logger.Infomation("It's already open.");
                }
                return FunctionResult.True;
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

            uint dv = 0; // digital value
            double av = 0; // analog value
            ushort av1 = 0; // analog value
            uint av2 = 0; // analog value
            string sv = string.Empty; // string value;
            uint result = 0;
            /*
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
            */
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

        private void RunCommand(Tag commandTag)
        {
            int axisno = commandTag.Identity.Index;

            string protocols = commandTag.Identity.ExeCmd;

            uint result = 0;
            /*
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
                    
                case ExeCmd.TriggerSetTimeLevel:
                    result = TriggerSetTimeLevel(commandTag);
                    break;

                default:
                    // 그 외는 Property 설정이다.
                    break;
            }
            */
        }

        private void SetCommandResult(TagIdentity tag, object value)
        {
            RelatedTag relatedTag = new RelatedTag();
            if (GetRelatedTag(tag.SimCmd, ref relatedTag) == true)
            {
                GetTag(relatedTag.Name).Is = value;
            } // else
        }

        #endregion Methods
    }
}