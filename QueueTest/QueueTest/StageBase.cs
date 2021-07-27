using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace QueueTest
{
    public abstract class StageBase
    {
        #region Fields

        public Dictionary<string, Cell> _MyCells;

        // 상수
        private const int MaximumCapacity = 2;

        private const int WaitTimeSecond = 5; // 5초안에 다음 Cell이 들어오지 않으면 프로세스 시작

        private int _Interval = 500;
        private bool _isLoop = false;
        private int _StageID;
        private DateTime _timeStamp;

        private Thread _WorkProcThread;

        #endregion Fields

        #region Constructors

        public StageBase(int stageId)
        {
            _StageID = stageId;

            Initialize();
        }

        #endregion Constructors

        #region Properties

        public int _ProcessStep { get; protected set; } = 0;
        public int _Status { get; protected set; } = 0;

        #endregion Properties

        #region Methods

        public void ChangeCellStatus()
        {
            foreach (Cell cell in _MyCells.Values)
            {
                CommandCenter.UpdataeCellInfo(cell.Id, _StageID, _Status);
            }
        }

        public int GetStatus()
        {
            return _Status;
        }

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else // timeout
            {
                Logger.Debug("Timeout");
                return false;
            }
        }

        public bool CheckCommmadCenterStatus()
        {
            // Pause 이면,
            if (CommandCenter._CommandStatus == CommandCenter.Operation.Pause)
            {
                // 다음 상태로 변경될 때까지 대기
                bool _IsResult = false;
                Action action = () =>
                {
                    while (true)
                    {
                        // 단순히 해제될 때까지만 기다린다.
                        if (CommandCenter._CommandStatus != CommandCenter.Operation.Resume)
                        {
                            _IsResult = true;
                            break;
                        }                        

                        Thread.Sleep(5);
                    }
                };
                
                if (WaitResult(100000, action) == false)
                {
                    return false;
                } // else
               
            }

            // 시작, 정지이면 그대로 진행
            // Abort이면 바로 종료
            if (CommandCenter._CommandStatus == CommandCenter.Operation.Abort)
            {
                return false;
            }

            return true;
        }

        public virtual void ProcessStatus()
        {
            ChangeCellStatus();

            if (_ProcessStep == Idx.StageStap.Step1)
            {
                Thread.Sleep(1000);

                _ProcessStep++;
            }
            else if (_ProcessStep == Idx.StageStap.Step2)
            {
                Thread.Sleep(1000);

                _ProcessStep++;
            }
            else if (_ProcessStep == Idx.StageStap.Step3)
            {
                _Status = Idx.StageStatus.Complate;
            }
        }

        private bool CheckStartStage()
        {
            //Cell을 조회하여 Start 여부를 결정한다.

            _MyCells = CommandCenter.SearchCell(_StageID, MaximumCapacity);

            if (_MyCells.Count <= 0)
            {
                _timeStamp = DateTime.Now;
                return false;
            } //

            if (_MyCells.Count >= MaximumCapacity)
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

        private void CompleteStatus()
        {
            ChangeCellStatus();

            InitInstance();
        }

        private void Initialize()
        {
            _isLoop = true;

            InitInstance();

            _WorkProcThread = new Thread(WorksProc);
            _WorkProcThread.IsBackground = true;
            _WorkProcThread.Start();

            //ThreadPool.QueueUserWorkItem(WorksProc);
        }

        public void InitInstance()
        {
            _timeStamp = DateTime.Now;

            _ProcessStep = Idx.StageStap.Step1;

            _Status = Idx.StageStatus.Wait;
        }

        private void WorksProc(object state)
        {
            while (_isLoop)
            {
                if (CommandCenter._CommandStatus == CommandCenter.Operation.Start ||
                    CommandCenter._CommandStatus == CommandCenter.Operation.Resume)
                { 
                    RunProcess();
                }                

                Thread.Sleep(_Interval);
                Application.DoEvents();
            }
        }

        private void RunProcess()
        {                        
            if (_Status == Idx.StageStatus.Wait)
            {
                if (CheckStartStage() == true)
                {
                    // Start
                    _ProcessStep = Idx.StageStap.Step1;
                    _Status = Idx.StageStatus.Busy;
                }
            }
            else if (_Status == Idx.StageStatus.Busy)
            {
                ProcessStatus();
            }
            else if (_Status == Idx.StageStatus.Complate)
            {
                CompleteStatus();
            }
        }

        #endregion Methods

    }
}