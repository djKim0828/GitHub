using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    public abstract class StageBase
    {
        #region Fields

        public Dictionary<string, Cell> _MyCells;

        // 상수        
        private const int WaitTimeSecond = 10; // 5초안에 다음 Cell이 들어오지 않으면 프로세스 시작

        private int _interval = 50;
        private bool _isLoop = false;

        private int _stageID;
        private DateTime _timeStamp;

        private Thread _workProcThread;

        #endregion Fields

        #region Constructors

        public StageBase(int stageId)
        {
            _stageID = stageId;

            Initialize();
        }

        ~StageBase()
        {
            _isLoop = false;
        }

        #endregion Constructors

        #region Properties

        public int _ProcessStep { get; protected set; } = 0;
        public int _Status { get; protected set; } = 0;

        #endregion Properties

        #region Methods

        public void ChangeCellStatus(string className)
        {
            foreach (Cell cell in _MyCells.Values)
            {
                CellChangeInfo ccif = new CellChangeInfo();
                ccif.Id = cell.Id;
                ccif.StatusId = _Status;
                ccif.ClassName = className;
                ccif.ChangeType = CommandCenter.ChangeType.UpdataeCellStatusId;

                CommandCenter.ChangeCell(ccif);
            }
        }

        public void CompleteStatus()
        {
            ChangeCellStatus(this.GetType().Name);
        }

        public MethodInfo[] GetFunctions(object classObject)
        {
            try
            {
                MethodInfo[] functions = classObject.GetType().
                    GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                return functions;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return null;
            }
        }

        public object GetSequenceClass(string sequenceClassName)
        {
            string nameSpace = GetType().Namespace;

            Assembly assembly = Assembly.Load(nameSpace);

            Type type = assembly.GetType(nameSpace + "." + sequenceClassName);// 클래스 이름에 해당하는 Type을 가져옮

            object classObject = Activator.CreateInstance(type);

            return classObject;
        }

        public int GetStatus()
        {
            return _Status;
        }

        public void InitInstance()
        {
            // 변수 초기화
            _timeStamp = DateTime.Now;
            _Status = Idx.StageStatus.Wait;
        }

        public abstract void RunProcessStep();        

        private bool CheckStartStage()
        {
            //Cell을 조회하여 Start 여부를 결정한다.
            CellChangeInfo ccif = new CellChangeInfo();
            ccif.StageId = _stageID;
            ccif.ChangeType = CommandCenter.ChangeType.SearchWaitCell;

            _MyCells = (Dictionary<string, Cell>)CommandCenter.ChangeCell(ccif);

            if (_MyCells.Count <= 0)
            {
                _timeStamp = DateTime.Now;
                return false;
            } //

            if (_MyCells.Count >= CommandCenter._MaximumCapacity)
            {
                return true;
            }
            else
            {
                TimeSpan span = DateTime.Now.Subtract(_timeStamp);

                if (span.Seconds > WaitTimeSecond)
                {
                    return true;
                } //else

                return false;
            }
        }

        private void Initialize()
        {
            _isLoop = true;

            InitInstance();

            _workProcThread = new Thread(WorksProc);
            _workProcThread.IsBackground = true;
            _workProcThread.Start();

            //ThreadPool.QueueUserWorkItem(WorksProc);
        }

        private void RunProcess()
        {
            if (_stageID == Idx.StageIndex.Shuttle)
            {
                int aaa = 0;
            }

            if (_Status == Idx.StageStatus.Idle)
            {
                // doing nothing
            }
            else if (_Status == Idx.StageStatus.Wait)
            {
                if (CheckStartStage() == true)
                {
                    // Start
                    _ProcessStep = Idx.StageStap.None;
                    _Status = Idx.StageStatus.Busy;
                }
            }
            else if (_Status == Idx.StageStatus.Busy)
            {
                RunProcessStep();
            }
            else if (_Status == Idx.StageStatus.Complate)
            {                
                CompleteStatus();

                _Status = Idx.StageStatus.Idle;           
            }
        }

        private void WorksProc(object state)
        {
            while (_isLoop)
            {
                if (CommandCenter._CommandStatus == StatusType.Operation.Start ||
                    CommandCenter._CommandStatus == StatusType.Operation.Resume)
                {
                    RunProcess();
                }

                Thread.Sleep(_interval);
                //Func.DoEvents();
            }
        }

        #endregion Methods
    }
}