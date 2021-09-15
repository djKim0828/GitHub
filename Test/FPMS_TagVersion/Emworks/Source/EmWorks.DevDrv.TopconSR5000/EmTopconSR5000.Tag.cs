using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Linq;
using System.Threading;

namespace EmWorks.DevDrv.TopconSR5000
{
    public partial class EmTopconSR5000
    {
        #region Methods

        private bool GetRelatedTag(string configRelatedTag, ref RelatedTag relatedTags)
        {
            if (configRelatedTag == string.Empty)
            {
                return false;
            }

            relatedTags = new RelatedTag();

            try
            {
                string[] properties = configRelatedTag.Split('/');

                relatedTags.Name = properties[0];
                relatedTags.Type = properties[1];
                relatedTags.DefaultValue = properties[2];
                relatedTags.ReActValue = properties[3];
                relatedTags.DelayTime = properties[4];

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private Tag GetTag(string propertyName)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Name == propertyName).Value;
            return temp;
        }

        private Tag GetTagByAction(int ioType, string action)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == ioType &&
                                            x.Value.Identity.Action == action).Value;

            return temp;
        }

        private Tag GetTagByExeCmd(int ioType, string exeCmd)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Identity.IoType == ioType &&
                                            x.Value.Identity.ExeCmd == exeCmd).Value;

            return temp;
        }

        private TagIdentity GetTagIdentity(string propertyName)
        {
            Tag temp = _data.FirstOrDefault(x => x.Value.Name == propertyName).Value;
            return temp.Identity;
        }

        private void SetCommandResult(TagIdentity tag, object value)
        {
            RelatedTag relatedTag = new RelatedTag();
            if (GetRelatedTag(tag.SimCmd, ref relatedTag) == true)
            {
                GetTag(relatedTag.Name).Is = value;
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
                        Thread.Sleep(Delaytime);

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
                        t.Is = reActValue;
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void UpdateTagByExeCmd(int ioType, string exeCmd, object value)
        {
            Tag t = GetTagByExeCmd(ioType, exeCmd);

            t.Is = value;
        }

        private void UpdateTagByAction(int ioType, string action, object value)
        {
            Tag t = GetTagByAction(ioType, action);

            //if (value == null)
            //{
            //    t = null;
            //    return;
            //}

            t.Is = value;
        }

        #endregion Methods

    }
}