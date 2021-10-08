using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="LoadStage"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-02-15
    /// Description : object detail description.
    /// </summary>
    public class ShuttleCommander : StageBase
    {
        #region Fields

        public Dictionary<string, MotionPosition> _X1PostionList;
        public Dictionary<string, MotionPosition> _Y2PostionList;

        private string _sequenceName = "ShuttleSequence1";

        #endregion Fields

        #region Constructors

        public ShuttleCommander(int stageId) : base(stageId)
        {
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public void ChangeStatus(int status)
        {
            _Status = status;
        }        

        public bool Move(int axisIndex, string position, int timeOut=10000, bool isCheckSkip = false)
        {
            switch(axisIndex)
            {
                case Idx.MotionAxisNo.ShuttleX1:
                    if (CommandCenter.Move(Global._PmacX1, _X1PostionList, position, timeOut) == false)
                    {
                        return false;
                    } // else

                    break;
                case Idx.MotionAxisNo.ShuttleY2:
                    if (CommandCenter.Move(Global._PmacY2, _Y2PostionList, position, timeOut) == false)
                    {
                        return false;
                    } // else

                    break;
                default:
                    // Nothing
                    break;
            }                       
         
            return true;
        }        
        
        public override void RunProcessStep()
        {
            ChangeCellStatus(this.GetType().Name);

            string sequenceClassName = _sequenceName;
            object classObject = GetSequenceClass(sequenceClassName);

            MethodInfo[] functions = GetFunctions(classObject);

            bool isComplete = true;
            foreach (MethodInfo mi in functions)
            {
                Logger.Debug("Start Function [ " + mi.Name + "]");

                //object[] parametersArray = new object[] { "" }; // 파라미터가 필요한 경우 사용
                object result = mi.Invoke(classObject, new object[] { });

                if ((bool)result == false)
                {
                    Logger.Debug("***** Failed [ " + mi.Name + "]");

                    isComplete = false;

                    if (CommandCenter._CommandStatus != StatusType.Operation.Abort)
                    {
                        StatusMachine.Request(StatusType.Operation.Abort);
                        CommandCenter.RequestAlarm(GetType().Name, mi.Name);
                    } // else

                    break;
                }
                else
                {
                    Logger.Debug("Success [ " + mi.Name + "]");
                }

                if (CommandCenter._CommandStatus != StatusType.Operation.Start &&
                    CommandCenter._CommandStatus != StatusType.Operation.Resume)
                {
                    isComplete = false;
                    break;
                } // else
            } // else

            if (isComplete == true)
            {
                _Status = Idx.StageStatus.Complate;
            } // else
        }

        internal void UpdateCellId()
        {
            foreach (Cell cl in _MyCells.Values)
            {
                CellChangeInfo ccif = new CellChangeInfo();
                ccif.Id = cl.Id;
                ccif.CellId = CommandCenter._CellId;
                ccif.ClassName = this.GetType().Name;
                ccif.ChangeType = CommandCenter.ChangeType.UpdateCellId;

                CommandCenter.ChangeCell(ccif);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-15
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

            if (resultInstance == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
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
                _IsInitialled = false;

                CommandCenter.LoadMotionPosition(Idx.MotionAxisNo.ShuttleX1, ref _X1PostionList);
                CommandCenter.LoadMotionPosition(Idx.MotionAxisNo.ShuttleY2, ref _Y2PostionList);

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }        

        #endregion Methods
    }
}