using EmWorks.Lib.DriverCore;
using System;

namespace EmWorks.DevDrv.AjinMotionIo
{
    [Serializable]
    public class DioModel : DIoModelBase
    {
        #region Fields

        private int _moduleNum;
        private int _offsetNum;
        private bool _checkDioLoop = false;

        #endregion Fields

        #region Constructors

        public DioModel(int dIoNumber) : base(dIoNumber)
        {

        }

        #endregion Constructors

        ~DioModel()
        {
            _checkDioLoop = false;
        }

        #region Properties

        public int ModuleNum
        {
            get { return _moduleNum; }
            set { _moduleNum = value; }
        }

        public int OffsetNum
        {
            get { return _offsetNum; }
            set { _offsetNum = value; }
        }
        public bool CheckDioLoop
        {
            get { return _checkDioLoop; }
            set { _checkDioLoop = value; }
        }

        #endregion Properties
    }
}