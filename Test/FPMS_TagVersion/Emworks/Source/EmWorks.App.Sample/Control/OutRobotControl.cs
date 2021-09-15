using EmWorks.DevDrv.NachiRobot;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;

namespace EmWorks.App.Sample
{
    public class OutRobotControl
    {
        #region Fields

        private Nachi_Out _Nachi_Out;
        private bool _Simulation = false;

        #endregion Fields

        #region Constructors

        public OutRobotControl(Nachi_Out robot, bool isSimulation = false)
        {
            _Nachi_Out = robot;

            _Simulation = isSimulation;
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
            if (_Nachi_Out.SetsyDeviceOutRobot(true) == true)
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

            if (_Nachi_Out.SetyOutRobotFAILURE_RESET(true, 1000) == false)
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

                _Nachi_Out.yOutRobotSPEED_OVERRIDE_INPUT_1 = (double)100; // 이것과 같이 Digital은 Group (Bit)로 사용한 경우 1번에만 값을 내려야 함.                

                step = 4;

                //4
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "]")); step++;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P1 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P2 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P3 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P4 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P5 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P6 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P7 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P8 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P9 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P10 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P11 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P12 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P13 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P14 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P15 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P16 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P17 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P18 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P19 = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P20 = (double)Idx.DigitalValue.Disable;

                //5
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "]")); step++;
                _Nachi_Out.yOutRobotP1_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP2_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP3_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP4_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP5_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP6_PERMIT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP7_PERMIT = Idx.DigitalValue.Disable;

                //6
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotP1_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP2_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP3_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP4_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP5_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP6_VISION_USE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotP7_VISION_USE = Idx.DigitalValue.Disable;

                //7
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotTOOL_1_CYLINDER_DOWN_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_1_CYLINDER_UP_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_1_VACUUM_ON_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_1_VACUUM_OFF_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_2_CYLINDER_DOWN_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_2_CYLINDER_UP_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_2_VACUUM_ON_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_2_VACUUM_OFF_DO = Idx.DigitalValue.Disable;

                //8
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotGET_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPUT_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL1_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL2_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL12_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTAGE1_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTAGE2_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTAGE3_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTAGE4_SELECT = Idx.DigitalValue.Disable;

                //9
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotT1_MCR_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotT2_MCR_ACK = Idx.DigitalValue.Disable;

                //10
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotSTG1_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG1_VAC_OFF_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG2_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG2_VAC_OFF_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG3_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG3_VAC_OFF_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG4_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSTG4_VAC_OFF_ACK = Idx.DigitalValue.Disable;

                //11
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotSPARE_STG1_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSPARE_STG1_VAC_OFF_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSPARE_STG2_VAC_ON_ACK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotSPARE_STG2_VAC_OFF_ACK = Idx.DigitalValue.Disable;

                // Control Signals Clear
                //12
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotEXTERNAL_PLAY_START_U1 = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPLAY_STOP_U1 = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotFAILURE_RESET = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMOTOR_ON_EXTERNAL = Idx.DigitalValue.Disable;


                _Nachi_Out.yOutRobotMOTOR_OFF_EXTERNAL = Idx.DigitalValue.Enable;
                Sleep(100);
                _Nachi_Out.yOutRobotMOTOR_OFF_EXTERNAL = Idx.DigitalValue.Disable;


                _Nachi_Out.yOutRobotREDUCE_SPEED = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPAUSE = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotEXTERNAL_RESET = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotORIGIN_RETURN = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotCYCLE_STOP = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMANUAL_MODE_SELECT = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotMANUAL_MOVE_GO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotAUTO_RETRY_REQUEST = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotAUTO_CALIBRATION_START_REQ = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotAUTO_CALIBRATION_MOVE_REQ = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotRECIPE_OK = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPROCESS_EXIT_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPROCESS_SKIP_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotPROCESS_ERROR_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotERROR_CODE_RETURN = (double)Idx.DigitalValue.Disable;

                //13
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotRECIPE_NO_BITS_1 = (double)Idx.DigitalValue.Disable;//anlog
                _Nachi_Out.yOutRobotPROGRAM_SELECT_BIT_U1_1 = (double)Idx.DigitalValue.Disable;//anlog
                _Nachi_Out.yOutRobotMANUAL_POS_BINARY_1 = (double)Idx.DigitalValue.Disable;//anlog
                _Nachi_Out.yOutRobotU4_SEND_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotU4_RECEIVE_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotU5_SEND_REQ = (double)Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotU5_RECEIVE_REQ = (double)Idx.DigitalValue.Disable;

                //14
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotINTERFERENCE_REGION1_PERMIT = (double)Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotINTERFERENCE_REGION2_PERMIT = (double)Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotINTERFERENCE_REGION3_PERMIT = (double)Idx.DigitalValue.Enable;

                Sleep(100);

                //15
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotERROR_CODE_RETURN = (double)Idx.DigitalValue.Enable;

                //16
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotUNDER_STOPPING, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotUNDER_STOPPING:True]"));
                    return false;
                } // else

                //17
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotPLAY_STOP_U1 = Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotPAUSE = Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotU4_RECEIVE_REQ = (double)Idx.DigitalValue.Enable;
                //_Nachi_Out.yOutRobotU5_SEND_REQ = (double)Idx.DigitalValue.Enable; // RMS 사용시에 
                int recipeNo = 1;
                _Nachi_Out.yOutRobotRECIPE_NO_BITS_1 = (double)recipeNo;//1
                _Nachi_Out.yOutRobotPROGRAM_SELECT_BIT_U1_1 = (double)Idx.DigitalValue.Enable;//1

                //18
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotEXTERNAL_RESET = Idx.DigitalValue.Enable;

                //19
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotEXTERNAL_REST_ACK_NO, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotEXTERNAL_REST_ACK_NO:True]"));
                    return false;
                } // else

                //20
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotEXTERNAL_REST_ACK_NO, Idx.DigitalValue.Disable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotEXTERNAL_REST_ACK_NO:True]"));
                    return false;
                } // else


                //21
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotMOTOR_ON_EXTERNAL = Idx.DigitalValue.Enable;

                //22
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotMOTOR_ENERGIZED, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotMOTOR_ENERGIZED:True]"));
                    return false;
                } // else

                //23
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotUNIT_READY, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotMOTOR_ENERGIZED:True]"));
                    return false;
                } // else

                //24
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotSTATE_OUTPUT_1, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotSTATE_OUTPUT_1:True]"));
                    return false;
                } // else

                //25
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotEXTERNAL_PLAY_START_U1 = Idx.DigitalValue.Enable;

                //26
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotRUNNING_U1, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotRUNNING_U1:True]"));
                    return false;
                } // else

                //25 ****************************
                //LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                //_Nachi_Out.yOutRobotEXTERNAL_PLAY_START_U1 = Idx.DigitalValue.Disable;

                //Sleep(500);

                //27
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotCURRENT_RECIPE_BITS_1, recipeNo, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotCURRENT_RECIPE_BITS_1:True]"));
                    return false;
                } // else

                //28
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotRECIPE_OK = Idx.DigitalValue.Enable;

                //29
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotHOME_POSITION_U1_1, Idx.DigitalValue.Enable, 10000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotHOME_POSITION_U1_1:True]"));
                    return false;
                } // else


                //30
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotU4_RECEIVE_REQ = (double)Idx.DigitalValue.Enable;

                //31
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;

                //Sleep(500);

                if (_Simulation == true)
                {                    
                    // 테스트 시에 활용하고 여기서 종료한다.
                    LoggerMessage(this, new EventLoggerMessageArgs("Initialization completed")); step++;

                    return true;
                }

                //32
                if (WaitforClientConnect(_Nachi_Out.xOutRobotClient1Connect, 10000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Connect Client Failed"));
                    return false;
                }

                //33
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotU4_RECEIVE_ACK, Idx.DigitalValue.Enable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotHOME_POSITION_U1_1:True]"));
                    return false;
                } // else

                // 옵셋 데이터 전송
                //34
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotSendOffset = Idx.DigitalValue.Enable;

                //35
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotU4_RECEIVE_REQ = (double)Idx.DigitalValue.Disable;

                //36
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotU4_RECEIVE_ACK, Idx.DigitalValue.Disable, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotHOME_POSITION_U1_1:True]"));
                    return false;
                } // else

                //37
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotERROR_CODE_RETURN = (double)Idx.DigitalValue.Enable;

                //38
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] "));
                if (WaitforSignal(_Nachi_Out.xOutRobotCURRENT_POS_BITS_1, 1, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotCURRENT_POS_BITS_1:True]"));
                    return false;
                } // else

                //38
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] "));
                if (WaitforSignal(_Nachi_Out.xOutRobotCURRENT_POS_BITS_3, 1, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotCURRENT_POS_BITS_1:True]"));
                    return false;
                } // else

                //38
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] "));step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotCURRENT_POS_BITS_5, 1, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotCURRENT_POS_BITS_1:True]"));
                    return false;
                } // else


                //39
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Enable;

                //40
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (WaitforSignal(_Nachi_Out.xOutRobotCURRENT_POS_BITS_1, 0, 5000) == Idx.FunctionResult.False)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Set init Faild  [xOutRobotCURRENT_POS_BITS_1:True]"));
                    return false;
                } // else

                //41
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Disable;


                LoggerMessage(this, new EventLoggerMessageArgs("Initialization completed")); step++;

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }


        private bool checkIO(Tag tag, int isOn, int count = 80)
        {
            // Complete 확인
            for (int i = 0; i < count; i++)
            {
                if (tag.Is != null)
                {
                    if (tag.Is.ToString() == isOn.ToString())
                    {
                        return true;
                    }
                }

                Thread.Sleep(50);
            }

            return false;
        }

        private void SignalsClear()
        {
            _Nachi_Out.yOutRobotGET_SELECT = Idx.DigitalValue.Enable;

            _Nachi_Out.yOutRobotPUT_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotTOOL1_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotTOOL2_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotTOOL12_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotSTAGE1_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotSTAGE2_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotSTAGE3_SELECT = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotSTAGE4_SELECT = Idx.DigitalValue.Enable;
        }

        public bool RunExitP1()
        {
            LoggerMessage(this, new EventLoggerMessageArgs("Start RunExitP1"));

            // Exit
            /////////////////////////////////////////////////////////////////////////////////////////////////
            int step = 1;

            //1
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotMOVING_CONDITION_P1 = (double)Idx.DigitalValue.Disable;

            //2
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotP1_PERMIT = Idx.DigitalValue.Disable;

            //3
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotPROCESS_EXIT_REQ = (double)Idx.DigitalValue.Enable; // 102 로

            //4
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotPROCESS_EXIT_ACK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //5
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotPROCESS_EXIT_REQ = (double)Idx.DigitalValue.Disable; // 102 로

            //6
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotPROCESS_EXIT_ACK, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //7
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            internalSelectionSignalsClear();

            //8
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotCURRENT_POS_BITS_1, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //9
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotP1_INTERLOCK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //10
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            Sleep(100);

            LoggerMessage(this, new EventLoggerMessageArgs("End RunExitP1"));

            return true;
        }

        public bool RunCompleteP1()
        {
            LoggerMessage(this, new EventLoggerMessageArgs("Start RunHandShakeP1"));

            int step = 1;

            //1
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotP1_COMPLETE, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //2
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotP1_PERMIT = Idx.DigitalValue.Disable;

            //3
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            internalSelectionSignalsClear();

            //4
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Enable;

            //5
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotP1_COMPLETE, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //6
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Disable;

            //7
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            LoggerMessage(this, new EventLoggerMessageArgs("End RunHandShakeP1"));

            return true;
        }

        public bool RunGetP1()
        {
            LoggerMessage(this, new EventLoggerMessageArgs("End RunGetP1"));

            int step = 1;

            // move to wait position.
            ///////////////////////////////////////////////////////////////////////////////////////////////
            // Todo : Load/Inload될 Motion 확인
            // Todo : Align 확인 - 아직은 Skip

            //1
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotTOOL_1_VACUUM_ON_DO = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotTOOL_2_VACUUM_ON_DO = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotTOOL_1_VACUUM_OFF_DO = Idx.DigitalValue.Enable;
            _Nachi_Out.yOutRobotTOOL_2_VACUUM_OFF_DO = Idx.DigitalValue.Enable;

            //2
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotP1_VISION_USE = Idx.DigitalValue.Disable; // Vision을 사용하는 경우

            //3
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotGET_SELECT = Idx.DigitalValue.Enable;
            if (checkIO(_Nachi_Out.xOutRobotGET_SELECT_ACK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //4
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotTOOL1_SELECT = Idx.DigitalValue.Enable;
            if (checkIO(_Nachi_Out.xOutRobotTOOL1_SELECT_ACK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //5
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotSTAGE1_SELECT = Idx.DigitalValue.Enable;
            if (checkIO(_Nachi_Out.xOutRobotSTAGE1_SELECT_ACK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //6
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotP1_PERMIT = Idx.DigitalValue.Enable; //111 -> 110

            Sleep(2000);

            // Cylinder up
            //LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            //Global._AjinIo.yOutRobotPad1Up = Idx.DigitalValue.Enable;
            //Global._AjinIo.yOutRobotPad1Down = Idx.DigitalValue.Disable;
            //Global._AjinIo.yOutRobotPadVac1On = Idx.DigitalValue.Enable; // Todo : 확인 필요

            // Handshake
            /////////////////////////////////////////////////////////////////////////////////////////////////
            //7
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotTOOL_1_VACUUM_ON_DO = Idx.DigitalValue.Enable;  // 112
            
            LoggerMessage(this, new EventLoggerMessageArgs("End RunGetP1"));

            return true;
        }

        public bool RunConditionP1()
        {
            LoggerMessage(this, new EventLoggerMessageArgs("Start RunConditionP1"));

            //1
            int step = 1;
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (((Tag)_Nachi_Out.yOutRobotMOVING_CONDITION_P1).Is.ToString() == Idx.DigitalValue.Enable.ToString())
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //2
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            _Nachi_Out.yOutRobotMOVING_CONDITION_P1 = (double)Idx.DigitalValue.Enable;   // 101 로 이동

            //3
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotCURRENT_POS_BITS_1, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //4
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotP1_INTERLOCK, Idx.DigitalValue.Enable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            //5
            LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
            if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
            {
                LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                return false;
            }

            LoggerMessage(this, new EventLoggerMessageArgs("End RunCondition1"));

            return true;
        }

        public bool RunLoad()
        {
            try
            {
                if (RunConditionP1() == false)
                {
                    return false;
                } // else

                if (RunGetP1() == false)
                {
                    return false;
                }

                if (RunCompleteP1() == false)
                {
                    return false;
                }

                if (RunExitP1() == false)
                {
                    return false;
                }

                LoggerMessage(this, new EventLoggerMessageArgs("Success - Move Position1 & Exit"));

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public bool RunUnload()
        {
            try
            {
                //1
                int step = 1;
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (((Tag)_Nachi_Out.yOutRobotMOVING_CONDITION_P3).Is.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //2
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P3 = (double)Idx.DigitalValue.Enable; // 301

                //3
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] "));
                if (checkIO(_Nachi_Out.xOutRobotCURRENT_POS_BITS_1, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step).ToString() + "] "));
                    return false;
                }

                //4
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotCURRENT_POS_BITS_2, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //5
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotP3_INTERLOCK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //6
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }
                
                //7
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotTOOL_1_VACUUM_ON_DO = Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotTOOL_2_VACUUM_ON_DO = Idx.DigitalValue.Enable;
                _Nachi_Out.yOutRobotTOOL_1_VACUUM_OFF_DO = Idx.DigitalValue.Disable;
                _Nachi_Out.yOutRobotTOOL_2_VACUUM_OFF_DO = Idx.DigitalValue.Disable;

                //8
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotP3_VISION_USE = Idx.DigitalValue.Disable; // Vision을 사용하는 경우

                //9
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotPUT_SELECT = Idx.DigitalValue.Enable;
                if (checkIO(_Nachi_Out.xOutRobotPUT_SELECT_ACK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //10
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotTOOL1_SELECT = Idx.DigitalValue.Enable;
                if (checkIO(_Nachi_Out.xOutRobotTOOL1_SELECT_ACK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //11
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotSTAGE1_SELECT = Idx.DigitalValue.Enable;
                if (checkIO(_Nachi_Out.xOutRobotSTAGE1_SELECT_ACK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //12
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotP3_PERMIT = Idx.DigitalValue.Enable; //201 -> 211 -> 210

                Sleep(1000);

                // Handshake
                /////////////////////////////////////////////////////////////////////////////////////////////////
                //13
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotSTG1_VAC_ON_ACK = Idx.DigitalValue.Enable;  // 112            
                Sleep(200);

                //14
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotTOOL_1_VACUUM_OFF_DO = Idx.DigitalValue.Enable;  // 112   
                Sleep(200);
 

                // Cycle End ↓↓↓↓↓↓↓↓
                /////////////////////////////////////////////////////////////////////////////////////////////////

                //14
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotP3_COMPLETE, Idx.DigitalValue.Enable, 100) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //15
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotP3_PERMIT = Idx.DigitalValue.Disable;

                //16
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                internalSelectionSignalsClear();

                //17
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Enable;

                //18
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotP3_COMPLETE, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //19
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotCOMPLETE_RETURN = (double)Idx.DigitalValue.Disable;

                //20
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                // Exit
                /////////////////////////////////////////////////////////////////////////////////////////////////

                //21
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotMOVING_CONDITION_P3 = (double)Idx.DigitalValue.Disable;

                //22
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotP3_PERMIT = Idx.DigitalValue.Disable;

                //23
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotPROCESS_EXIT_REQ = (double)Idx.DigitalValue.Enable; // 102 로

                //24
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotPROCESS_EXIT_ACK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //25
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                _Nachi_Out.yOutRobotPROCESS_EXIT_REQ = (double)Idx.DigitalValue.Disable; // 102 로

                //26
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotPROCESS_EXIT_ACK, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                internalSelectionSignalsClear();

                //27
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotCURRENT_POS_BITS_3, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //28
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotP3_INTERLOCK, Idx.DigitalValue.Enable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                //29
                LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] ")); step++;
                if (checkIO(_Nachi_Out.xOutRobotBUSY_SIGNALS, Idx.DigitalValue.Disable) == false)
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("Error [" + (step - 1).ToString() + "] "));
                    return false;
                }

                Sleep(100);

                LoggerMessage(this, new EventLoggerMessageArgs("Success - Move Unload & Exit")); step++;

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void internalSelectionSignalsClear()
        {
            _Nachi_Out.yOutRobotGET_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotPUT_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotTOOL1_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotTOOL2_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotTOOL12_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotSTAGE1_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotSTAGE2_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotSTAGE3_SELECT = Idx.DigitalValue.Disable;
            _Nachi_Out.yOutRobotSTAGE4_SELECT = Idx.DigitalValue.Disable;
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
                _Nachi_Out.yOutRobotMANUAL_MODE_SELECT = isEnable;

                if (isEnable == OnOff.On)
                {
                    if (WaitforSignal(_Nachi_Out.xOutRobotMANUAL_MODE, OnOff.On, 10000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [yOutRobotMANUAL_MODE_SELECT:True]"));
                        return FunctionResult.False;
                    } // else

                    if (WaitforSignal(_Nachi_Out.xOutRobotMANUAL_MOVE_STAND_BY, OnOff.On, 10000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [yOutRobotMANUAL_MODE_SELECT:True]"));
                        return FunctionResult.False;
                    } // else

                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMANUAL_MOVE_GO - On")); step++;
                    _Nachi_Out.yOutRobotMANUAL_MOVE_GO = OnOff.On;

                }
                else
                {
                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotMANUAL_MOVE_GO - On")); step++;
                    _Nachi_Out.yOutRobotMANUAL_MOVE_GO = OnOff.Off;

                    if (WaitforSignal(_Nachi_Out.xOutRobotHOME_POSITION_U1_1, OnOff.On, 10000) == FunctionResult.False)
                    {
                        LoggerMessage(this, new EventLoggerMessageArgs("Set Manual Mode  [xOutRobotHOME_POSITION_U1_1:False]"));
                        return FunctionResult.False;
                    } // else

                    LoggerMessage(this, new EventLoggerMessageArgs("[" + step.ToString() + "] yOutRobotEXTERNAL_PLAY_START_U1 - On")); step++;
                    _Nachi_Out.yOutRobotEXTERNAL_PLAY_START_U1 = OnOff.On;

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
                _Nachi_Out.yOutRobotPAUSE = isPause;

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