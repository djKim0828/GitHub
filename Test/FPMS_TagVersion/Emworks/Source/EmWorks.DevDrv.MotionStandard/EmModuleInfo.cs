using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.MotionStandard
{
    public class EmModuleInfo
    {
        #region Fields

        public int BoardNo, No, Index;
        public uint Id, ModuleStatus, FuncRt; // for ajin board.
        public int IoType, DataType, DiCount, DoCount, AiCount, AoCount;
        public bool IsReady;
        public string Name;

        #endregion Fields

        #region Constructors

        public EmModuleInfo()
        {
            Initialization();
        }

        #endregion Constructors

        #region Methods

        private void Initialization()
        {
            IsReady = false;
            Name = "default";
            Id = ModuleStatus = FuncRt = 0;
            BoardNo = No = Index = DiCount = DoCount = AiCount = AoCount = 0;

            IoType = Tag.Idx.Unknown;
            DataType = Tag.Idx.Unknown;
        }

        #endregion Methods
    }
}
