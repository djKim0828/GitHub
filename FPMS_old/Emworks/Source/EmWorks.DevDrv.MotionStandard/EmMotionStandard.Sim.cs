using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;

namespace EmWorks.DevDrv.MotionStandard
{
    public partial class EmMotionStandard
    {
        #region Fields

        private bool _isSimMoveStop = false;

        #endregion Fields

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
                    case ExeCmd.Stop:
                    case ExeCmd.SStop:
                    case ExeCmd.EStop:
                        // 정지
                        SimStopMotion();
                        break;

                    case ExeCmd.MoveStart:
                        // 이동
                        SimMoveStartPos(commandTag);
                        break;

                    default:
                        SimRelatedTagCommnad(commandTag);
                        break;
                }
            }
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

                        if (t.Is != null)
                        {
                            Type temp = t.Is.GetType();
                            reActValue = Convert.ChangeType(reActValue, temp);
                        }

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

        private void SimMoveStartPos(Tag commandTag)
        {
            _isSimMoveStop = false;

            Thread SimStartThread = new Thread(SimMoveStartPosProc);
            SimStartThread.Start(commandTag);
        }

        private void SimMoveStartPosProc(object tag)
        {
            try
            {
                Tag commandTag = (Tag)tag;

                // 현재 위치와 Target 위치 가져오기
                double targetPos = commandTag.d;

                // Busy Tag
                Tag xBusyTag = GetTag(commandTag.Identity.Unit, ExeCmd.SignalBusy);
                xBusyTag.Is = 1;

                // 명령 Tag
                Tag xCmdPosTag = GetTag(commandTag.Identity.Unit, ExeCmd.CommandPosisiton);
                xCmdPosTag.Is = Convert.ToDouble(targetPos);

                // 정지 Tag
                Tag yEStopTag = GetTag(commandTag.Identity.Unit, ExeCmd.EStop);
                yEStopTag.Is = 0;

                // 실제 Tag
                Tag xActPosTag = GetTag(commandTag.Identity.Unit, ExeCmd.ActualPosition);
                if (xActPosTag.Is == null)
                {
                    xActPosTag.Is = Convert.ToDouble(0);
                } // else

                double currentPos = Convert.ToDouble(xActPosTag.Is);
                double diff = Math.Abs(currentPos) - Math.Abs(targetPos);

                if (currentPos > targetPos)
                {
                    for (double i = currentPos; i >= targetPos; i--)
                    {
                        if (yEStopTag != null && yEStopTag.Is != null)
                        {
                            if (yEStopTag.Is.ToString() == "1")
                            {
                                _isSimMoveStop = true;

                                xBusyTag.Is = 0;
                                break;
                            } // else
                        }

                        xActPosTag.Is = i;
                        System.Windows.Forms.Application.DoEvents();

                        double diff2 = Math.Abs(i) - Math.Abs(targetPos);

                        if (diff2 > 5)
                        {
                            i = i - 2;
                        }

                        Thread.Sleep(10);
                    }
                }
                else
                {
                    for (double i = currentPos; i <= targetPos; i++)
                    {
                        if (yEStopTag != null && yEStopTag.Is != null)
                        {
                            if (yEStopTag.Is.ToString() == "1")
                            {
                                _isSimMoveStop = true;

                                xBusyTag.Is = 0;
                                break;
                            } // else
                        }

                        xActPosTag.Is = i;
                        System.Windows.Forms.Application.DoEvents();

                        double diff2 = Math.Abs(targetPos) - Math.Abs(i);

                        if (diff2 > 5)
                        {
                            i = i + 2;
                        }

                        Thread.Sleep(10);
                    }
                }

                if (_isSimMoveStop != true)
                {
                    xActPosTag.Is = targetPos; // 마지막엔 무조건 맞게
                }

                xBusyTag.Is = 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SimRelatedTagCommnad(Tag commandTag)
        {
            string reActValue = string.Empty;

            if (commandTag.Identity.SimCmd == null)
            {
                return;
            }

            string[] commands = commandTag.Identity.SimCmd.Split(';');

            foreach (string command in commands)
            {
                if (SetRelatedTagCommand(command, commandTag.Is, commandTag) == false)
                {
                    break;
                }
            }
        }

        private void SimStopMotion()
        {
            // N/A
        }

        #endregion Methods
    }
}