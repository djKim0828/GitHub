using EmWorks.Lib.Common;
using System;

namespace EmWorks.App.OpticInspection
{
    public enum WriteLevel
    {
        INFO = 0,
        DEBUG = 1
    }

    public class Cell
    {
        //private ConcurrentQueue<Cell> _CellDataQueue = null;

        #region Fields

        private CollectionHandler<CellInfo> _collectionHandler;
        private int _stageId;
        private int _statusId;
        private int _stepId;

        #endregion Fields

        #region Properties

        public string CellId { get; set; }
        public string ChangeTime { get; set; }
        public string Id { get; set; }
        public string insertTime { get; set; }

        public int StageId
        {
            get
            {
                return _stageId;
            }
            set
            {
                _stageId = value;
                SaveCellInfo();
                WriteLog();
            }
        }

        public int StatusId
        {
            get
            {
                return _statusId;
            }

            set
            {
                _statusId = value;
                SaveCellInfo();
                WriteLog();
            }
        }

        public int StepId
        {
            get
            {
                return _stepId;
            }

            set
            {
                _stepId = value;
                SaveCellInfo();
                WriteLog();
            }
        }

        public object Temp { get; set; }

        #endregion Properties

        // Test 용도

        #region Methods

        public void SaveCellInfo()
        {
            return;

            // Todo : 윤대리 확인 必
            //CellInfo cellInfo = new CellInfo();
            //cellInfo.Id = Id;
            //cellInfo.CellId = CellId;
            //cellInfo.StageId = StageId;
            //cellInfo.StatusId = StatusId;
            //cellInfo.StepId = StepId;
            //cellInfo.insertTime = insertTime;
            //ChangeTime = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
            //cellInfo.ChangeTime = ChangeTime;

            //_listHandler = new ListHandler<CellInfo>();

            //_listHandler.SaveLine("", cellInfo);
        }

        public void WriteLog()
        {
            string msg = "ID = " + Id + " Cell ID = " + CellId + " Stage ID =" + StageId + " StepId = " + StepId + " StatusID = " + StatusId;
            Logger.Debug(msg);
        }

        #endregion Methods
    }

    public class CellChangeInfo
    {
        #region Fields

        private string _cellId;
        private int _changeType;
        private string _channelId;
        private string _className;
        private string _id;
        private int _stageId;
        private int _statusId;
        private int _stepId;
        private object _tempObject;

        #endregion Fields

        #region Properties

        public string CellId
        {
            get
            {
                return _cellId;
            }

            set
            {
                _cellId = value;
            }
        }

        public int ChangeType
        {
            get
            {
                return _changeType;
            }

            set
            {
                _changeType = value;
            }
        }

        public string ChannelId
        {
            get
            {
                return _channelId;
            }

            set
            {
                _channelId = value;
            }
        }

        public string ClassName
        {
            get
            {
                return _className;
            }

            set
            {
                _className = value;
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public int StageId
        {
            get
            {
                return _stageId;
            }
            set
            {
                _stageId = value;
            }
        }

        public int StatusId
        {
            get
            {
                return _statusId;
            }

            set
            {
                _statusId = value;
            }
        }

        public int StepId
        {
            get
            {
                return _stepId;
            }

            set
            {
                _stepId = value;
            }
        }

        public object TempObject
        {
            get
            {
                return _tempObject;
            }

            set
            {
                _tempObject = value;
            }
        }

        #endregion Properties
    }

    [Serializable]
    public class CellInfo
    {
        #region Fields

        private string _cellId;
        private string _id;
        private int _stageId;
        private int _statusId;
        private int _stepId;

        #endregion Fields

        #region Properties

        public string CellId
        {
            get
            {
                return _cellId;
            }

            set
            {
                _cellId = value;
            }
        }

        public string ChangeTime { get; set; }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string insertTime { get; set; }

        public int StageId
        {
            get
            {
                return _stageId;
            }
            set
            {
                _stageId = value;
            }
        }

        public int StatusId
        {
            get
            {
                return _statusId;
            }

            set
            {
                _statusId = value;
            }
        }

        public int StepId
        {
            get
            {
                return _stepId;
            }

            set
            {
                _stepId = value;
            }
        }

        #endregion Properties
    }
}
