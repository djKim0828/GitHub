using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;

namespace EmWorks.DevDrv.SamwonTemp1500
{
    public partial class EmSamwonTemp1500
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
                    default:
                        SimRelatedTagCommnad(commandTag);
                        break;
                }
            } // else
        }

        private bool SetRelatedTagCommand(string command, object value, Tag tag)
        {
            try
            {
                if (command != null && command.ToString() != string.Empty)
                {
                    object reActValue;
                    RelatedTag relatedTag = new RelatedTag();
                    if (GetRelatedTag(command, ref relatedTag) == true)
                    {
                        int Delaytime = Convert.ToInt32(relatedTag.DelayTime);
                        System.Threading.Thread.Sleep(Delaytime);

                        string type = relatedTag.Type;

                        if (type == CommandTagType.Invert)
                        {
                            reActValue = (value.ToString() == "1") ? "0" : "1";
                        }
                        else if (type == CommandTagType.ReActValueOut)
                        {
                            // SimCmd에서 읽은 값을 내린다.
                            reActValue = relatedTag.ReActValue;
                        }
                        else if (type == CommandTagType.AlwaysZero)
                        {
                            reActValue = "0"; // 무조건
                        }
                        else if (type == CommandTagType.AlwaysOne)
                        {
                            reActValue = "1"; // 무조건
                        }
                        else
                        {
                            // type : Defalult
                            reActValue = value;
                        }

                        Tag t = GetTag(relatedTag.Name);

                        if (t.Is != null)
                        {
                            Type temp = t.Is.GetType();
                            reActValue = Convert.ChangeType(reActValue, temp);
                        }

                        t.Is = reActValue;
                    }
                } // else

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void SimRelatedTagCommnad(Tag commandTag)
        {
            string reActValue = string.Empty;

            if (commandTag.Identity.SimCmd == null)
            {
                return;
            } // else

            string[] commands = commandTag.Identity.SimCmd.Split(';');

            foreach (string command in commands)
            {
                if (SetRelatedTagCommand(command, commandTag.Is, commandTag) == false)
                {
                    break;
                } // else
            }
        }

        #endregion Methods
    }
}