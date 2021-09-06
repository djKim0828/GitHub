using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using HsApiNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace EmWorks.DevDrv.TopconSR5000
{
    public partial class EmTopconSR5000 : DriverBase
    {
        #region Fields

        public int _ActiveDeviceNo = 0;

        public Configuration _config;

        public HsApiNet.DeviceInfo _DeviceInfo = null;

        public Recipe _Recipe;

        private Dictionary<string, string> _commandTargetObject = new Dictionary<string, string>();

        private Dictionary<string, Tag> _data;

        private bool _isAutoIntegrationTime = false;

        private bool _isSimulation = false;

        private RunningStatus _lastStatus = RunningStatus.NONE;

        private FocusResult _liveViewFocusResult = null;

        private FocusSetting _liveViewFocusSetting = new FocusSetting();

        // LiveView
        private LiveSetting _liveViewSetting = new LiveSetting();

        private int _measurementDataId = 0;

        private int _wavelength = 0;

        //Auto Spot
        private AutoSporProperties _autoSporProperties;

        #endregion Fields

        #region Constructors

        public EmTopconSR5000(Dictionary<string, Tag> tags, bool isSimulation) : base(tags, isSimulation)
        {
            _data = tags;

            _isSimulation = isSimulation;

            _autoSporProperties = new AutoSporProperties();
        }

        #endregion Constructors

        #region Destructors

        // ||<
        // \r\n
        // Client 별 명령 타켓 저장하여 Receive 데이터 처리
        ~EmTopconSR5000()
        {
            DeviceClose();
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventDriverMessage(object sendor, EventDriverMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventDriverMessage DriverMessage;

        #endregion Events

        #region Methods

        public override int Close(Tag commandTag)
        {
            try
            {
                if (commandTag == null)
                {
                    return FunctionResult.True;
                }

                // Open 되지 않은 경우는,
                if (_DeviceInfo == null)
                {
                    return FunctionResult.True;
                }

                DeviceClose();

                _commandTargetObject.Remove(commandTag.Identity.Index.ToString());

                RelatedTag relatedTag = new RelatedTag();
                if (GetRelatedTag(commandTag.Identity.SimCmd, ref relatedTag) == true)
                {
                    GetTag(relatedTag.Name).Is = OnOff.Off;
                } // else

                Logger.Infomation("Close SR5000 [" + commandTag.Name + "]");

                return FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return FunctionResult.False;
            }
        }

        public void Command(Tag commandTag)
        {
            try
            {
                if (commandTag.Identity.IoType == EmWorks.Lib.Core.Tag.Idx.IoType.StatusOutput)
                {
                    if (commandTag.Is.ToString() == OnOff.On.ToString())
                    {
                        Open(commandTag);
                    }
                    else
                    {
                        Close(commandTag);
                    }
                }
                else if (commandTag.Identity.IoType == EmWorks.Lib.Core.Tag.Idx.IoType.Output)
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
                HsApiNet.Error ret = HsApiNet.Error.NONE;

                string calibrationPath = commandTag.Identity.Options;

                ret = HsApi.hsInitialize(calibrationPath, out _config);
                if (ret != HsApiNet.Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    return FunctionResult.False;
                }

                // Update Open Tag
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.StatusInput,
                    commandTag.Identity.Action, OpenClose.Open);

                Logger.Infomation("Open SR5000 [" + commandTag.Name + "]");

                return FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return FunctionResult.False;
            }
        }

        public void ShowError(HsApiNet.Error errorCode, Tag commandTag, object value)
        {
            // This is the procedure to get the error message.
            string errorMessage;
            HsApi.hsGetErrorString(errorCode, out errorMessage);

            //Logger.Error("[" + errorCode + "] " + errorMessage);

            if (commandTag != null)
            {
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, value);
            }
        }

        public void SuccessCommand(Tag commandTag, object value)
        {
            Logger.Infomation("Success [" + commandTag.Name + "] ");

            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, value);
        }

        public void SuccessCommand(Tag commandTag)
        {
            Logger.Infomation("Success [" + commandTag.Name + "] ");

            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, FunctionResult.True);
        }

        protected override void DoSimulation(Tag tag)
        {
            // N/A
        }

        protected override void DoUpdate()
        {
            // N/A
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
            string action = commandTag.Identity.Action;

            switch (action)
            {
                case "Connect":
                    Connect(commandTag);
                    break;

                case "LoadRecipe":
                    LoadRecipe(commandTag);
                    break;

                case "LoadMeasurement":
                    LoadMeasurement(commandTag);
                    break;

                case "GetPseudoImage":
                    GetPseudoImage(commandTag);
                    break;

                case "GetRGBImage":
                    GetRGBImage(commandTag);
                    break;

                case "SaveRecipe":
                    SaveRecipe(commandTag);
                    break;

                case "SaveMeasurement":
                    SaveMeasurement(commandTag);
                    break;

                case "DestroyMeasurement":
                    DestroyMeasurement(commandTag);
                    break;

                case "StartMeasurement":
                    StartMeasurement(commandTag);
                    break;

                case "CreatMeasurementData":
                    CreatMeasurementData(commandTag);
                    break;

                case "StopMeasurement":
                    StopMeasurement(commandTag);
                    break;

                case "StartLiveView":
                    StartLiveView(commandTag);
                    break;

                case "StopLiveView":
                    StopLiveView(commandTag);
                    break;

                case "GetLiveViewImage":
                    GetLiveViewImage(commandTag);
                    break;
                case "SearchROI":
                    SearchROI(commandTag);
                    break;
                default:
                    SetMotionProperty(commandTag);
                    break;
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

        #endregion Methods

        #region Classes

        public class EventDriverMessageArgs : EventArgs
        {
            #region Constructors

            public EventDriverMessageArgs(string message)
            {
                Initialize();

                Message = message;
            }

            #endregion Constructors

            #region Properties

            public string Message { get; protected set; }
            public DateTime OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }

        // Auto Spot Property
        public class AutoSporProperties
        {
            public int shapeType = 0;
            public double width = 0;
            public double height = 0;
            public int min = 0;
            public int max = 0;
            public int yTh = 0;

            public AutoSporProperties()
            {
                width = height = 0;
                min = max = yTh = 0;
            }
        }

        
        #endregion Classes
    }
}