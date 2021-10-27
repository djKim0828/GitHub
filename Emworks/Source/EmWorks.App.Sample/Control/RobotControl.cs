using EmWorks.DevDrv.NachiRobot;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;

namespace EmWorks.App.Sample
{
    public class RobotControl
    {
        #region Fields

        private Robot _robot;

        #endregion Fields

        #region Constructors

        public RobotControl(Robot robot)
        {
            _robot = robot;
        }

        #endregion Constructors

        #region Delegates

        public delegate void EventLoggerMessage(object sendor, EventLoggerMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventLoggerMessage LoggerMessage;

        #endregion Events

        #region Methods

        public bool Open()
        {
            if (_robot.SetsyDeviceIO(true) == true)
            {
                return true;
            }
            else
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Failed"));
                return false;
            }
        }

        public bool SetAlarmReset()
        {
            LoggerMessage(this, new EventLoggerMessageArgs("SetAlarmReset"));

            _robot.yOutRobotFAILURE_RESET = OnOff.On;

            if (_robot.SetyOutRobotFAILURE_RESET(false, 1000) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("SetAlarmReset Off - fail"));
                return false;
            }

            LoggerMessage(this, new EventLoggerMessageArgs("SetAlarmReset Complete"));

            return true;
        }

        public bool SetRunInitialize()
        {
            try
            {
                int step = 0;

                LoggerMessage(this, new EventLoggerMessageArgs("start Initialize"));

                if (SetAlarmReset() == false)
                {
                    return false;
                }

                step = 4;

                //4
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotPOSITION_SKIP_REQUEST - On")); step++;
                _robot.yOutRobotPOSITION_SKIP_REQUEST = (double)OnOff.On;

                Sleep(200);

                //5
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotPOSITION_SKIP_REQUEST - Off")); step++;
                _robot.yOutRobotPOSITION_SKIP_REQUEST = (double)OnOff.Off;
                //6
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMOVING_CONDITION_P1 - Off")); step++;
                _robot.yOutRobotMOVING_CONDITION_P1 = (double)OnOff.Off;
                //7
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotP1_TOOL1_PERMIT - Off")); step++;
                _robot.yOutRobotP1_TOOL1_PERMIT = OnOff.Off;
                //8
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotP1_TOOL2_PERMIT - Off")); step++;
                _robot.yOutRobotP1_TOOL2_PERMIT = OnOff.Off;
                //9
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotP1_TOOL12_PERMIT - Off")); step++;
                _robot.yOutRobotP1_TOOL12_PERMIT = OnOff.Off;
                //10
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMOVING_CONDITION_P2 - Off")); step++;
                _robot.yOutRobotMOVING_CONDITION_P2 = (double)OnOff.Off;
                //11
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotP2_TOOL1_1_PERMIT = OnOff.Off;
                //12
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotP2_TOOL2_2_PERMIT = OnOff.Off;
                //13
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotERROR_CODE_RETURN = (double)OnOff.On; // index : 7

                Sleep(200);

                //14
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotERROR_CODE_RETURN = (double)OnOff.Off; // index : 7

                //15
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_PLAY_START_U1 = OnOff.Off;
                //16
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPLAY_STOP_U1 = OnOff.Off;
                //17
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotFAILURE_RESET = OnOff.Off;
                //18
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMOTOR_ON_EXTERNAL = OnOff.Off;
                //19
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMOTOR_OFF_EXTERNAL = OnOff.Off;
                //20
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotREDUCE_SPEED = OnOff.Off;
                //21
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPAUSE = OnOff.Off;
                //22
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_RESET = OnOff.Off;
                //23
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotORIGIN_RETURN = OnOff.Off;
                //24
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotCYCLE_STOP = OnOff.Off;
                //25
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPROGRAM_SELECT_BIT_U1_1 = OnOff.Off;
                //26
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMANUAL_MODE_SELECT = OnOff.Off;
                //27
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMANUAL_MOVE_GO = OnOff.Off;
                //28
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_1_VACUUM_OFF_DO = OnOff.Off;
                //29
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_2_VACUUM_OFF_DO = OnOff.Off;
                //30
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPLAY_STOP_U1 = OnOff.On;
                //31
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPAUSE = OnOff.On;
                //32
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotPROGRAM_SELECT_BIT_U1_1 = OnOff.On;
                //33
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_RESET = OnOff.On;

                //Sleep(200);

                //34
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotEXTERNAL_REST_ACK_NO, OnOff.On, 1000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [EXTERNAL_REST_ACK_NO:True]"));
                    return false;
                } // else

                //35
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotEXTERNAL_REST_ACK_NO, OnOff.Off, 1000) == FunctionResult.False)  // Real Run시에는 10초
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild [EXTERNAL_REST_ACK_NO:False]"));
                    return false;
                } // else

                //36
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_RESET = OnOff.Off;

                // Sleep
                Sleep(1000);

                //37
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMOTOR_ON_EXTERNAL = OnOff.On;
                //38
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotMOTOR_ENERGIZED, OnOff.On, 5000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [MOTOR_ENERGIZED : true]"));
                    return false;
                } // else
                //39
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotMOTOR_ON_EXTERNAL = OnOff.Off;

                // Sleep
                Sleep(1000);

                //40
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_PLAY_START_U1 = OnOff.On;

                // Sleep
                Sleep(500);

                //41
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotEXTERNAL_PLAY_START_U1 = OnOff.Off;
                //42
                if (WaitforSignal(_robot.xOutRobotRUNNING_U1, OnOff.On, 5000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild [RUNNING_U1 : true]"));
                    return false;
                } // else

                //43
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotHOME_POSITION_U1_1, OnOff.On, 5000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild [HOME_POSITION_U1_1 : true]"));
                    return false;
                } // else

                //44
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotUI_TASK_RUNNING, OnOff.On, 5000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild [UI_TASK_RUNNING : true]"));
                    return false;
                } // else

                //45
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforClientConnect(_robot.xOutRobotClient1Connect, 10000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Connect Client Failed"));
                    return false;
                }
                //46
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotSendOffset = OnOff.On;
                //47
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotUI_DATA_RECEIVE_REQUEST = OnOff.On;

                Sleep(100);

                //48
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_robot.xOutRobotUI_DATA_RECEIVE_COMPLETE, OnOff.On, 20000) == FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [UI_DATA_RECEIVE_COMPLETE : true]"));
                    return false;
                } // else

                //49
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotUI_DATA_RECEIVE_REQUEST = OnOff.Off;

                Sleep(500);


                //50
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_1_VACUUM_ON_DO = OnOff.Off;
                //51
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_1_VACUUM_OFF_DO = OnOff.Off;
                //52
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_2_VACUUM_ON_DO = OnOff.Off;
                //53
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotTOOL_2_VACUUM_OFF_DO = OnOff.Off;
                //54
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotCOMPLETE_RETURN = (double)OnOff.Off;
                //55
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotERROR_CODE_RETURN = (double)OnOff.Off;
                //56
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotP1_STAGE1_VACUUM_ON_DO = OnOff.On;
                //57
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _robot.yOutRobotP2_STAGE1_VACUUM_ON_DO = OnOff.On;

                LoggerMessage(this, new EventLoggerMessageArgs("Initialization completed")); step++;

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private int CheckClinetConnect(Tag tag, int timeout)
        {
            int returnValue = 0;

            int count = 0;
            while (true)
            {
                if (tag.Is != null)
                {
                    if (tag.Is.ToString() == OpenClose.Open.ToString())
                    {
                        returnValue = 1;
                        break;
                    } // else
                }// else

                Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
                count++;

                if (count * 100 > timeout)
                {
                    // Timeout
                    break;
                }
            }

            return returnValue;
        }

        internal int SetManualMode(int isEnable)
        {
            try
            {
                int step = 1;

                LoggerMessage(this, new EventLoggerMessageArgs("start SetManualMode"));

                //1 
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMANUAL_MODE_SELECT - On")); step++;
                _robot.yOutRobotMANUAL_MODE_SELECT = isEnable;

                if (isEnable == OnOff.On)
                {
                    if (WaitforSignal(_robot.xOutRobotMANUAL_MODE, OnOff.On, 10000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [yOutRobotMANUAL_MODE_SELECT:True]"));
                        return FunctionResult.False;
                    } // else

                    if (WaitforSignal(_robot.xOutRobotMANUAL_MOVE_STAND_BY, OnOff.On, 10000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [yOutRobotMANUAL_MODE_SELECT:True]"));
                        return FunctionResult.False;
                    } // else

                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMANUAL_MOVE_GO - On")); step++;
                    _robot.yOutRobotMANUAL_MOVE_GO = OnOff.On;

                }
                else
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMANUAL_MOVE_GO - On")); step++;
                    _robot.yOutRobotMANUAL_MOVE_GO = OnOff.Off;

                    if (WaitforSignal(_robot.xOutRobotHOME_POSITION_U1_1, OnOff.On, 1000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [xOutRobotHOME_POSITION_U1_1:False]"));
                        return FunctionResult.False;
                    } // else

                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotEXTERNAL_PLAY_START_U1 - On")); step++;
                    _robot.yOutRobotEXTERNAL_PLAY_START_U1 = OnOff.On;

                    // 초기화
                    //SetRunInitialize();
                }

                return FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                LoggerMessage(this, new EventLoggerMessageArgs(ex.Message.ToString()));
                return FunctionResult.False;
            }
        }

        internal int SetPause(int isPause)
        {
            try
            {
                int step = 1;

                LoggerMessage(this, new EventLoggerMessageArgs("start SetPause"));

                //1 
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotPAUSE - On")); step++;
                _robot.yOutRobotPAUSE = isPause;               

                return FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                LoggerMessage(this, new EventLoggerMessageArgs(ex.Message.ToString()));
                return FunctionResult.False;
            }
        }

        private int CheckTagStatus(Tag tag,
                            int tagetValue,
                            int timeout,
                            bool isOutput = false)
        {
            int returnValue = 0;

            int count = 0;
            while (true)
            {
                if (tag.Is != null)
                {
                    if (tag.Is.ToString() == tagetValue.ToString())
                    {
                        returnValue = 1;
                        break;
                    } // else
                }

                Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
                count++;

                if (count * 100 > timeout)
                {
                    // Timeout
                    returnValue = 0;
                    break;
                }
            }

            return returnValue;
        }

        private void Sleep(int milliseconds)
        {
            LoggerMessage(this, new EventLoggerMessageArgs("Sleep - " + milliseconds.ToString()));
            System.Threading.Thread.Sleep(milliseconds);
            System.Windows.Forms.Application.DoEvents();
        }

        private int WaitforClientConnect(Tag tag, int timeout)
        {
            int result = 0;
            try
            {
                Action action = () =>
                {
                    result = CheckClinetConnect(tag, timeout);
                };

                return WaitResult(timeout, action);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return result;
            }
        }

        private int WaitforSignal(Tag tag,
                            int tagetValue,
                            int timeout,
                            bool isOutput = false)
        {
            int result = 0;
            try
            {
                Action action = () =>
                {
                    result = CheckTagStatus(tag, tagetValue, timeout, isOutput);
                };

                return WaitResult(timeout, action);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return result;
            }
        }

        private int WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return FunctionResult.True;
            }
            else // timeout
            {
                Logger.Error("Timeout");
                return FunctionResult.False;
            }
        }

        #endregion Methods

        #region Classes

        public class EventLoggerMessageArgs : EventArgs
        {
            #region Constructors

            public EventLoggerMessageArgs(string message)
            {
                Initialize();

                Message = message;
            }

            #endregion Constructors

            #region Properties

            public string Message { get; protected set; }
            public DateTime OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}