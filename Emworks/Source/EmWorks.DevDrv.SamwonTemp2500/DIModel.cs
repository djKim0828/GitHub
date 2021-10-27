using EmWorks.Lib.DriverCore;
using System;

namespace EmWorks.DevDrv.SamwonTemp2500
{
    [Serializable]
    public class DIModel : DIoModelBase
    {
        #region Constructors

        public DIModel(int dIoNumber) : base(dIoNumber)
        {
        }

        #endregion Constructors

        #region Destructors

        ~DIModel()
        {
        }

        #endregion Destructors
    }
}