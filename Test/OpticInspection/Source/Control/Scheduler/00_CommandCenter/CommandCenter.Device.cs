using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    public partial class CommandCenter
    {
        #region Methods

        public static void LoadMotionPosition(int motionIndex, ref Dictionary<string, MotionPosition> _PostionList)
        {
            Global._MotionControl.LoadMotionPosition(motionIndex, ref _PostionList);
        }

        public static bool Move(DeltatauPmac tagClass, Dictionary<string, MotionPosition> positionList, string PositionName, int timeOut, bool isCheckSkip = false)
        {
            try
            {
                if (PositionName == string.Empty)
                {
                    return true;
                } // else

                Thread.Sleep(100);

                tagClass.yMotionMoveMode = Convert.ToInt32(positionList[PositionName].MoveType);
                Thread.Sleep(1);

                tagClass.yMotionVelocity = positionList[PositionName].Speed;
                Thread.Sleep(1);

                tagClass.yMotionAccel = positionList[PositionName].Accel;
                Thread.Sleep(1);

                tagClass.yMotionDecel = positionList[PositionName].Decel;
                Thread.Sleep(1);

                tagClass.yMotionMove = positionList[PositionName].Position;
                Thread.Sleep(10);

                if (isCheckSkip == true)
                {
                    return true;
                }

                bool isActionBreak = false;

                double setCmdPos = positionList[PositionName].Position;
                double getActPos = tagClass.xMotionActual.d;

                Action action = () =>
                {
                    // Todo :장비 TEST 시 보강 필요
                    while (true)
                    {
                        setCmdPos = positionList[PositionName].Position;
                        getActPos = tagClass.xMotionActual.d;

                        double diff = Math.Abs(setCmdPos - getActPos);
                        if (diff < 2)
                        {
                            break;
                        } // else

                        if (CommandCenter.CheckCommmadCenterStatus(tagClass.yMotionEStop,
                                                                   tagClass.yMotionMove,
                                                                   positionList[PositionName].Position) == false)
                        {
                            Logger.Debug("Stop Function");
                            isActionBreak = true;
                            break;
                        } // else

                        Thread.Sleep(CommandCenter._MotionCheckInterval);
                    }
                };

                if (_instance.WaitResult(timeOut, action) == false)
                {
                    isActionBreak = true;
                    return false;
                }
                else
                {
                    if (isActionBreak == true)
                    {
                        return false;
                    }
                    else
                    {
                        // 성공
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else // timeout
            {
                Logger.Debug("Timeout");
                return false;
            }
        }

        #endregion Methods
    }
}