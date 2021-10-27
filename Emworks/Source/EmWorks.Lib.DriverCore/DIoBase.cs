using System;
using System.Collections.Generic;
using EmWorks.Lib.Common;

namespace EmWorks.Lib.DriverCore
{
    [Serializable]
    public abstract class DIoBase : DriverBase
    {
        #region Constructors

        public DIoBase(Define.RunMode isSimulation) : base(isSimulation)
        {
        }

        #endregion Constructors

        #region Properties

        public abstract string Version { get; protected set; }
        public abstract int DICount { get; protected set; }
        public abstract int DOCount { get; protected set; }
        public abstract string ExtenalLibVersion { get; protected set; }

        #endregion Properties
    }
}