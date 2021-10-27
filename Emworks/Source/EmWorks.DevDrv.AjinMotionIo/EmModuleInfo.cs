namespace EmWorks.DevDrv.AjinMotionIo
{
    public class EmModuleInfo
    {
        #region Fields

        public int BoardNumber, Number, Index;
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
            BoardNumber = Number = Index = DiCount = DoCount = AiCount = AoCount = 0;
        }

        #endregion Methods
    }
}