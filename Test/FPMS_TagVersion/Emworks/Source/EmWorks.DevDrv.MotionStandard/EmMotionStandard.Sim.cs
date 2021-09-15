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

        private void AlarmReset(Tag commandTag)
        {
            Tag xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.Alarm);
            xTag.Is = commandTag.Is;
        }

        private void ChangeMoveMode(Tag commandTag)
        {
            Tag xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.MoveMode);
            xTag.Is = commandTag.Is;
        }

        private void Homing(Tag commandTag)
        {
            if (commandTag.Is.ToString() == "0")
            {
                return;
            } // else

            Tag xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.SignalBusy);
            xTag.Is = 1;

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.SignalHome);
            xTag.Is = 0;

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.Homing);
            xTag.Is = 2;

            Thread.Sleep(500); // Stop 루틴은 구현하지 않았습니다.

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.Homing);
            xTag.Is = 1;

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.SignalHome);
            xTag.Is = 1;

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.CommandPosisiton);
            xTag.Is = (double)0;

            xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.ActualPosition);
            xTag.Is = (double)0;
        }

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
                    case ExeCmd.ServoOn:
                        ServerOn(commandTag);
                        break;

                    case ExeCmd.AlarmReset:
                        AlarmReset(commandTag);
                        break;

                    case ExeCmd.Homing:
                        Homing(commandTag);
                        break;

                    case ExeCmd.MoveModeSelect:
                        ChangeMoveMode(commandTag);
                        break;

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

                    case ExeCmd.JogVel:
                        SimJogMove(commandTag);
                        break;

                    default:
                        SimRelatedTagCommnad(commandTag);
                        break;
                }
            }
        }

        private void ServerOn(Tag commandTag)
        {
            Tag xTag = GetxTag(commandTag.Identity.Unit, ExeCmd.ServoOn);
            xTag.Is = commandTag.Is;
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

        private void SimJogMove(Tag commandTag)
        {
            if (_isSimMoveStop == true)
            {
                return;
            } // else

            Thread SimStartThread = new Thread(SimJogMoveProc);
            SimStartThread.Start(commandTag);
        }

        private void SimJogMoveProc(object tag)
        {
            try
            {
                Tag commandTag = (Tag)tag;

                // 현재 위치와 Target 위치 가져오기
                double targetPos = commandTag.d;

                // Busy Tag
                Tag xBusyTag = GetxTag(commandTag.Identity.Unit, ExeCmd.SignalBusy);
                xBusyTag.Is = 1;

                // 정지 Tag
                Tag yEStopTag = GetyTag(commandTag.Identity.Unit, ExeCmd.EStop);
                yEStopTag.Is = 0;

                // 실제 Tag
                Tag xActPosTag = GetxTag(commandTag.Identity.Unit, ExeCmd.ActualPosition);
                if (xActPosTag.Is == null)
                {
                    xActPosTag.Is = Convert.ToDouble(0);
                } // else

                double i = (double)xActPosTag.Is;

                while (true)
                {
                    if (yEStopTag != null && yEStopTag.Is != null)
                    {
                        if (yEStopTag.Is.ToString() == "1")
                        {
                            _isSimMoveStop = true;

                            xBusyTag.Is = 0;
                            break;
                        } // else
                    } // else

                    if (commandTag.d > 0)
                    {
                        xActPosTag.Is = i++;
                    }
                    else
                    {
                        xActPosTag.Is = i--;
                    }

                    Thread.Sleep(10);
                }

                _isSimMoveStop = false;

                xBusyTag.Is = 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SimMoveStartPos(Tag commandTag)
        {
            if (_isSimMoveStop == true)
            {
                return;
            } // else

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
                Tag xBusyTag = GetxTag(commandTag.Identity.Unit, ExeCmd.SignalBusy);
                xBusyTag.Is = 1;

                // 명령 Tag
                Tag xCmdPosTag = GetxTag(commandTag.Identity.Unit, ExeCmd.CommandPosisiton);
                xCmdPosTag.Is = Convert.ToDouble(targetPos);

                // 정지 Tag
                Tag yEStopTag = GetyTag(commandTag.Identity.Unit, ExeCmd.EStop);
                yEStopTag.Is = 0;

                // 실제 Tag
                Tag xActPosTag = GetxTag(commandTag.Identity.Unit, ExeCmd.ActualPosition);
                if (xActPosTag.Is == null)
                {
                    xActPosTag.Is = Convert.ToDouble(0);
                } // else

                Tag xMoveMode = GetxTag(commandTag.Identity.Unit, ExeCmd.MoveMode);

                if (xMoveMode.Is.ToString() == MotionMoveType.RelativeType.ToString())
                {
                    targetPos = (double)xActPosTag.Is + targetPos;
                }

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
                        } // else

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

                _isSimMoveStop = false;

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