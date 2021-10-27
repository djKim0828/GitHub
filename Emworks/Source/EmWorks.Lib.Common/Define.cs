using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.Lib.Common
{
    public class Define
    {
        #region Enums

        public enum RunMode
        {
            Real,
            Dry,
            Simulation
        }
        public enum InequalitySymbol
        {
            Less,
            LessOrEqual,
            Equal,
            GreaterOrEqual,
            Greater
        }

        public enum RunStop
        {
            Run = 1,
            Stop = 4
        }

        public enum Laser
        {
            On,
            Off,
        }

        public enum ReceiveResult
        {
            None,
            Complete,
            Error
        }

        #endregion Enums

    }
}
