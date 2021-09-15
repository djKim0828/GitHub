using EmWorks.Lib.Core;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    public partial class EmAutonicsTK4S
    {
        #region Methods

        private void RunSimCommand(Tag commandTag)
        {
            if (commandTag.Identity.IoType == Tag.Idx.IoType.StatusOutput)
            {
                SetCommandResult(commandTag.Identity, commandTag.Is);
            }
            else if (commandTag.Identity.IoType == Tag.Idx.IoType.Output)
            {
                string protocols = commandTag.Identity.ExeCmd;

                switch (protocols)
                {
                    case null:
                    default:
                        //SimRelatedTagCommnad(commandTag);
                        break;
                }
            }
        }

        #endregion Methods
    }
}