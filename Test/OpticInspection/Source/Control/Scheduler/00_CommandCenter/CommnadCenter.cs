using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="CommandCenter"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-06-30
    /// Description : object detail description.
    /// </summary>
    public partial class CommandCenter
    {
        #region Fields

        public static InspectionCommander _InspectionCommander;
        public static object _IsKey = new object();
        public static ShuttleCommander _ShuttleCommander;
        private static StatusType.Operation _commandStatus;
        private static CommandCenter _instance;
        private static string[] _scheduleList;
        private Dictionary<string, Cell> _cells = null;
        private Dictionary<string, Cell> _insertCells = null;
        private bool _isLoop;
        private int _loopInterval;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description :  objcet constructor.
        /// </summary>
        public CommandCenter()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description :  object destructor.
        /// </summary>
        ~CommandCenter()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventCommnadMessage(object sendor, EventCommandMessageArgs e);

        #endregion Delegates

        #region Events

        public static event EventCommnadMessage CommandMessage;

        #endregion Events

        #region Properties

        public static string _CellId { get; set; }

        public static StatusType.Operation _CommandStatus
        {
            get
            {
                return _commandStatus;
            }
            set
            {
                _commandStatus = value;
            }
        }

        public static int _MaximumCapacity { get; } = Global.ConfigGeneral.MaximumCellCapacity;
        public static int _MotionCheckInterval { get; } = Global.ConfigGeneral.MotionCheckIntervalTime;
        public static ModelRecipe.RecipeItem _Recipe { get; set; }
        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public static void ChangeFov(int fovId)
        {
            CommandMessage?.Invoke(_instance, new EventCommandMessageArgs(CommandType.ChangeFov, fovId, string.Empty));
        }

        public static void SendSR5000(int commandType, object data)
        {
            CommandMessage?.Invoke(_instance, new EventCommandMessageArgs(commandType, data, string.Empty));
        }        

        public static void ChangeStatus(StatusType.Operation status)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            } // else

            if (status == StatusType.Operation.Start)
            {
                _beforeTime = DateTime.Now;

                string[] schedule = new string[Idx.StageIndex.StageCount];
                schedule[0] = Idx.StageIndex.Shuttle.ToString();
                schedule[1] = Idx.StageIndex.Inspection.ToString();

                _scheduleList = schedule;

                ThreadPool.QueueUserWorkItem(InsertEmptyCell);
            } // else

            _CommandStatus = status;
        }

        public static bool CheckCommmadCenterStatus(object eStopTag, object moveTag, double movePosition)
        {
            // Abort 이면 ( 별도 if문으로 Pause-Resume 후에도 Abort 동작 가능)
            if (_CommandStatus == StatusType.Operation.Abort)
            {
                // Abort시 바로 정지
                ((Tag)eStopTag).Is = Idx.DigitalValue.Enable;
                return false;
            }
            else if (_CommandStatus == StatusType.Operation.Pause)
            {
                // Pause 일시정지시에는 바로 정지
                ((Tag)eStopTag).Is = Idx.DigitalValue.Enable;

                // 다음 상태로 변경될 때까지 대기
                bool isResult = false;
                Action action = () =>
                {
                    while (true)
                    {
                        // 단순히 해제될 때까지만 기다린다.
                        if (_CommandStatus != StatusType.Operation.Pause)
                        {
                            isResult = true;

                            ((Tag)moveTag).Is = movePosition;

                            break;
                        } // else

                        Thread.Sleep(5);
                    }
                };

                //if (_instance.WaitResult(100000, action) == false)
                if (_instance.WaitResult(action) == false)
                {
                    return false;
                } // else
            }  // else

            return true;
        }

        public static void InsertEmptyCell(object state)
        {
            try
            {
                object c1 = new object();

                CellChangeInfo ccif = new CellChangeInfo();
                ccif.TempObject = c1;
                ccif.ChangeType = CommandCenter.ChangeType.Insert;

                CommandCenter.ChangeCell(ccif);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public static int ReInitialize()
        {
            try
            {
                _ShuttleCommander.InitInstance();
                _InspectionCommander.InitInstance();

                _instance.InitDictionary();

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        public static void RequestAlarm(string className, string functionName)
        {
            string[] ErrorInfo = new string[Idx.AlarmEventIndex.MaxLength];
            ErrorInfo[Idx.AlarmEventIndex.Id] = className + "_" + functionName;
            ErrorInfo[Idx.AlarmEventIndex.Name] = className + "_" + functionName;
            ErrorInfo[Idx.AlarmEventIndex.Description] = "New Alarm";

            StatusMachine.Request(StatusType.Alarm.Occurred, ErrorInfo);
        }

        public int SearchNextStage(int id)
        {
            try
            {
                int nextId = -1;

                for (int i = 0; i < _scheduleList.Length; i++)
                {
                    if (_scheduleList[i] == id.ToString())
                    {
                        if (i < _scheduleList.Length - 1)
                        {
                            nextId = Convert.ToInt16(_scheduleList[i + 1]);
                            break;
                        } // else
                    } // else
                }

                return nextId;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private void AllComplete(Cell cell)
        {
            CellChangeInfo ccif = new CellChangeInfo();
            ccif.Id = cell.Id;
            ccif.ChangeType = CommandCenter.ChangeType.Delete;

            CommandCenter.ChangeCell(ccif);
        }

        private void ChangeAllStageStatus()
        {
            // 각 Stage의 상태를 변경하는 함수 이다.
            // 이 함수에서 각 Stage의 상태를 읽어서 다른 Stage의 상태를 변경한다.
            // 해당 과제는 하나의 Wafer의 검색이 완료되면 Shttule 을 Laod/Unload 위치로 이동하고 검색 Stage를 Wait로 만든다.

            _ShuttleCommander.Move(Idx.MotionAxisNo.ShuttleY2, "min");
            _ShuttleCommander.Move(Idx.MotionAxisNo.ShuttleX1, "min");

            _ShuttleCommander.ChangeStatus(Idx.StageStatus.Wait);

            _InspectionCommander.ChangeStatus(Idx.StageStatus.Wait);

            StatusMachine.Request(StatusType.Operation.Stop);
        }

        private void ChangeCellStage()
        {
            try
            {
                if (_insertCells.Count == CommandCenter._MaximumCapacity)
                {
                    foreach (Cell cl in _insertCells.Values)
                    {
                        _cells.Add(cl.Id, cl);
                    }

                    _insertCells.Clear();

                }  // else

                CellChangeInfo ccif = new CellChangeInfo();
                ccif.ChangeType = CommandCenter.ChangeType.SearchComplateCell;

                Dictionary<string, Cell> resultCell = (Dictionary<string, Cell>)CommandCenter.ChangeCell(ccif);

                if (resultCell != null)
                {
                    int nextStage = -1;

                    foreach (Cell cell in resultCell.Values)
                    {
                        nextStage = SearchNextStage(cell.StageId);

                        if (nextStage < 0)
                        {
                            //Todo : 임시 Tact time 체크
                            TimeSpan ts = DateTime.Now - _beforeTime;
                            if (ts.Seconds > 2)
                            {
                                Global._tactTime = ts;
                                _beforeTime = DateTime.Now;
                            } // else

                            AllComplete(cell);

                            ChangeAllStageStatus();

                            continue;
                        }
                        else
                        {
                            ccif = new CellChangeInfo();
                            ccif.Id = cell.Id;
                            ccif.StageId = nextStage;
                            ccif.StatusId = Idx.StageStatus.Wait;
                            ccif.ChangeType = CommandCenter.ChangeType.UpdataeNextStage;

                            CommandCenter.ChangeCell(ccif);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                if (_CommandStatus == StatusType.Operation.Start ||
                    _CommandStatus == StatusType.Operation.Resume)
                {
                    //Todo : 장비 상태 변경?
                    ChangeCellStage();
                }
                else
                {
                    //Todo : 장비 상태 변경?
                    //CommandCenter.SetTowerLamp(Idx.TowerLampStatus.None);
                }

                Thread.Sleep(_loopInterval);
            }
        }

        private void InitDictionary()
        {
            _insertCells = new Dictionary<string, Cell>();
            _cells = new Dictionary<string, Cell>();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True || resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
                _loopInterval = 5;
                _isLoop = true;

                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                _IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                InitDictionary();

                _ShuttleCommander = new ShuttleCommander(Idx.StageIndex.Shuttle);
                _InspectionCommander = new InspectionCommander(Idx.StageIndex.Inspection);

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            return Idx.FunctionResult.True;
        }

        private bool WaitResult(Action action)
        {
            //int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne())
            {
                return true;
            }
            else // timeout
            {
                Logger.Debug("Timeout");
                return false;
            }
        }

        #endregion Methods

        #region Classes

        public class CommandType
        {
            #region Fields

            public const int ChangeFov = 0;
            public const int StartInspection = 1;
            public const int PseudoData = 2;
            public const int RGBData = 3;
            public const int ResultData = 4;
            public const int CompleteInspection = 5;

            #endregion Fields
        }

        public class EventCommandMessageArgs : EventArgs
        {
            #region Constructors

            public EventCommandMessageArgs(int commandType, object command, string message)
            {
                Initialize();

                _CommandType = commandType;
                _Command = command;
                _Message = message;
            }

            #endregion Constructors

            #region Properties

            public object _Command { get; protected set; }
            public int _CommandType { get; protected set; }
            public string _Message { get; protected set; }
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
                Logger.Infomation(_Message);
            }

            #endregion Methods
        }

        #endregion Classes
    }
}