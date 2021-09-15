using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.NachiRobot;

namespace EmWorks.App.Sample
{
    public class Nachi_Out
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public Nachi_Out(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmNachiRobot(_tags, isSimulationMode); // Todo : 사용할 Device 로 변경 必

        }

        public Tag this[string name]
        {
            get
            {
                return _tags[name];
            }
        }

        #endregion Constructors

        #region Destructors

        ~Nachi_Out()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceOutRobot
        {
            get
            {
                return _tags[nameof(sxDeviceOutRobot)];
            }
        }
        public object syDeviceOutRobot
        {
            get
            {
                return _tags[nameof(syDeviceOutRobot)];
            }
            set
            {
                _tags[nameof(syDeviceOutRobot)].Is = value;
            }
        }
        public Tag xDeviceLibraryVersion
        {
            get
            {
                return _tags[nameof(xDeviceLibraryVersion)];
            }
        }
        public Tag xOutRobotClient1Connect
        {
            get
            {
                return _tags[nameof(xOutRobotClient1Connect)];
            }
        }
        public object yOutRobotSendOffset
        {
            get
            {
                return _tags[nameof(yOutRobotSendOffset)];
            }
            set
            {
                _tags[nameof(yOutRobotSendOffset)].Is = value;
            }
        }
        public object yOutRobotOffsetdataLOAD_POSITION_A
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataLOAD_POSITION_A)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataLOAD_POSITION_A)].Is = value;
            }
        }
        public object yOutRobotOffsetdataLOAD_POSITION_B
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataLOAD_POSITION_B)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataLOAD_POSITION_B)].Is = value;
            }
        }
        public object yOutRobotOffsetdataUNLOAD_POSITION_A
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataUNLOAD_POSITION_A)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataUNLOAD_POSITION_A)].Is = value;
            }
        }
        public object yOutRobotOffsetdataUNLOAD_POSITION_B
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataUNLOAD_POSITION_B)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataUNLOAD_POSITION_B)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_A
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_A)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_A)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_B
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_B)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_B)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_A
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_A)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_A)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_B
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_B)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_B)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_C
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_C)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_C)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_D
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_D)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_D)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_C
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_C)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_C)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_D
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_D)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_D)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_E
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_E)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_E)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPT_F
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPT_F)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPT_F)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_E
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_E)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_E)].Is = value;
            }
        }
        public object yOutRobotOffsetdataBPU_F
        {
            get
            {
                return _tags[nameof(yOutRobotOffsetdataBPU_F)];
            }
            set
            {
                _tags[nameof(yOutRobotOffsetdataBPU_F)].Is = value;
            }
        }
        public Tag xOutRobotTEACH_MODE
        {
            get
            {
                return _tags[nameof(xOutRobotTEACH_MODE)];
            }
        }
        public Tag xOutRobotPLAY_BACK_MODE
        {
            get
            {
                return _tags[nameof(xOutRobotPLAY_BACK_MODE)];
            }
        }
        public Tag xOutRobotRUNNING_U1
        {
            get
            {
                return _tags[nameof(xOutRobotRUNNING_U1)];
            }
        }
        public Tag xOutRobotUNDER_STOPPING
        {
            get
            {
                return _tags[nameof(xOutRobotUNDER_STOPPING)];
            }
        }
        public Tag xOutRobotINTERLOCK_ALARM
        {
            get
            {
                return _tags[nameof(xOutRobotINTERLOCK_ALARM)];
            }
        }
        public Tag xOutRobotMOTOR_ENERGIZED
        {
            get
            {
                return _tags[nameof(xOutRobotMOTOR_ENERGIZED)];
            }
        }
        public Tag xOutRobotPROGRAM_ENDED_U1
        {
            get
            {
                return _tags[nameof(xOutRobotPROGRAM_ENDED_U1)];
            }
        }
        public Tag xOutRobotFAILURE
        {
            get
            {
                return _tags[nameof(xOutRobotFAILURE)];
            }
        }
        public Tag xOutRobotBATTERY_WARNING_M1
        {
            get
            {
                return _tags[nameof(xOutRobotBATTERY_WARNING_M1)];
            }
        }
        public Tag xOutRobotHOME_POSITION_U1_1
        {
            get
            {
                return _tags[nameof(xOutRobotHOME_POSITION_U1_1)];
            }
        }
        public Tag xOutRobotHOME_POSITION_U1_2
        {
            get
            {
                return _tags[nameof(xOutRobotHOME_POSITION_U1_2)];
            }
        }
        public Tag xOutRobotPROGRAM_SELECTED
        {
            get
            {
                return _tags[nameof(xOutRobotPROGRAM_SELECTED)];
            }
        }
        public Tag xOutRobotEXTERNAL_REST_ACK_NO
        {
            get
            {
                return _tags[nameof(xOutRobotEXTERNAL_REST_ACK_NO)];
            }
        }
        public Tag xOutRobotSTATE_OUTPUT_1
        {
            get
            {
                return _tags[nameof(xOutRobotSTATE_OUTPUT_1)];
            }
        }
        public Tag xOutRobotEMERGENCY_STOP
        {
            get
            {
                return _tags[nameof(xOutRobotEMERGENCY_STOP)];
            }
        }
        public Tag xOutRobotUNIT_READY
        {
            get
            {
                return _tags[nameof(xOutRobotUNIT_READY)];
            }
        }
        public Tag xOutRobotBUSY_SIGNALS
        {
            get
            {
                return _tags[nameof(xOutRobotBUSY_SIGNALS)];
            }
        }
        public Tag xOutRobotSPARE_X018
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X018)];
            }
        }
        public Tag xOutRobotSPARE_X019
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X019)];
            }
        }
        public Tag xOutRobotSPARE_X020
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X020)];
            }
        }
        public Tag xOutRobotSPARE_X021
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X021)];
            }
        }
        public Tag xOutRobotSPARE_X022
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X022)];
            }
        }
        public Tag xOutRobotPENDANT_EMS
        {
            get
            {
                return _tags[nameof(xOutRobotPENDANT_EMS)];
            }
        }
        public Tag xOutRobotCONTROLLER_EMS
        {
            get
            {
                return _tags[nameof(xOutRobotCONTROLLER_EMS)];
            }
        }
        public Tag xOutRobotSPARE_X025
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X025)];
            }
        }
        public Tag xOutRobotSPARE_X026
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X026)];
            }
        }
        public Tag xOutRobotSPARE_X027
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X027)];
            }
        }
        public Tag xOutRobotSPARE_X028
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X028)];
            }
        }
        public Tag xOutRobotMANUAL_MODE
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_MODE)];
            }
        }
        public Tag xOutRobotMANUAL_MOVE_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_MOVE_COMPLETE)];
            }
        }
        public Tag xOutRobotMANUAL_MOVE_STAND_BY
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_MOVE_STAND_BY)];
            }
        }
        public Tag xOutRobotSPARE_X32
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X32)];
            }
        }
        public Tag xOutRobotTOOL_1_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_1_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotTOOL_1_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_1_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotTOOL_1_CYLINDER_UP_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_1_CYLINDER_UP_DI)];
            }
        }
        public Tag xOutRobotTOOL_1_CYLINDER_DOWN_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_1_CYLINDER_DOWN_DI)];
            }
        }
        public Tag xOutRobotTOOL_2_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_2_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotTOOL_2_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_2_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotTOOL_2_CYLINDER_UP_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_2_CYLINDER_UP_DI)];
            }
        }
        public Tag xOutRobotTOOL_2_CYLINDER_DOWN_DI
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL_2_CYLINDER_DOWN_DI)];
            }
        }
        public Tag xOutRobotGET_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotGET_SELECT_ACK)];
            }
        }
        public Tag xOutRobotPUT_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotPUT_SELECT_ACK)];
            }
        }
        public Tag xOutRobotTOOL1_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL1_SELECT_ACK)];
            }
        }
        public Tag xOutRobotTOOL2_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL2_SELECT_ACK)];
            }
        }
        public Tag xOutRobotTOOL12_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotTOOL12_SELECT_ACK)];
            }
        }
        public Tag xOutRobotSTAGE1_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotSTAGE1_SELECT_ACK)];
            }
        }
        public Tag xOutRobotSTAGE2_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotSTAGE2_SELECT_ACK)];
            }
        }
        public Tag xOutRobotSTAGE3_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotSTAGE3_SELECT_ACK)];
            }
        }
        public Tag xOutRobotSTAGE4_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotSTAGE4_SELECT_ACK)];
            }
        }
        public Tag xOutRobotT1_MCR_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotT1_MCR_REQ)];
            }
        }
        public Tag xOutRobotT2_MCR_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotT2_MCR_REQ)];
            }
        }
        public Tag xOutRobotSTG1_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG1_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSTG1_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG1_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSTG2_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG2_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSTG2_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG2_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSTG3_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG3_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSTG3_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG3_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSTG4_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG4_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSTG4_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSTG4_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSPARE_STG1_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_STG1_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSPARE_STG1_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_STG1_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSPARE_STG2_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_STG2_VAC_ON_REQ)];
            }
        }
        public Tag xOutRobotSPARE_STG2_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_STG2_VAC_OFF_REQ)];
            }
        }
        public Tag xOutRobotSPARE_X64
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X64)];
            }
        }
        public Tag xOutRobotSPARE_X65
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X65)];
            }
        }
        public Tag xOutRobotP1_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP1_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X67
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X67)];
            }
        }
        public Tag xOutRobotSPARE_X68
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X68)];
            }
        }
        public Tag xOutRobotSPARE_X69
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X69)];
            }
        }
        public Tag xOutRobotP1_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP1_COMPLETE)];
            }
        }
        public Tag xOutRobotP2_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP2_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X72
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X72)];
            }
        }
        public Tag xOutRobotSPARE_X73
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X73)];
            }
        }
        public Tag xOutRobotSPARE_X74
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X74)];
            }
        }
        public Tag xOutRobotP2_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP2_COMPLETE)];
            }
        }
        public Tag xOutRobotP3_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP3_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X77
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X77)];
            }
        }
        public Tag xOutRobotSPARE_X78
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X78)];
            }
        }
        public Tag xOutRobotSPARE_X79
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X79)];
            }
        }
        public Tag xOutRobotP3_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP3_COMPLETE)];
            }
        }
        public Tag xOutRobotP4_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP4_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X82
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X82)];
            }
        }
        public Tag xOutRobotSPARE_X83
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X83)];
            }
        }
        public Tag xOutRobotSPARE_X84
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X84)];
            }
        }
        public Tag xOutRobotP4_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP4_COMPLETE)];
            }
        }
        public Tag xOutRobotP5_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP5_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X87
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X87)];
            }
        }
        public Tag xOutRobotSPARE_X88
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X88)];
            }
        }
        public Tag xOutRobotSPARE_X89
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X89)];
            }
        }
        public Tag xOutRobotP5_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP5_COMPLETE)];
            }
        }
        public Tag xOutRobotP6_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP6_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X92
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X92)];
            }
        }
        public Tag xOutRobotSPARE_X93
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X93)];
            }
        }
        public Tag xOutRobotSPARE_X94
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X94)];
            }
        }
        public Tag xOutRobotP6_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP6_COMPLETE)];
            }
        }
        public Tag xOutRobotP7_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP7_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X97
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X97)];
            }
        }
        public Tag xOutRobotSPARE_X98
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X98)];
            }
        }
        public Tag xOutRobotSPARE_X99
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X99)];
            }
        }
        public Tag xOutRobotP7_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP7_COMPLETE)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_START_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_START_ACK)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_MOVE_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_MOVE_ACK)];
            }
        }
        public Tag xOutRobotSPARE_X103
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X103)];
            }
        }
        public Tag xOutRobotSPARE_X104
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X104)];
            }
        }
        public Tag xOutRobotSPARE_X105
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X105)];
            }
        }
        public Tag xOutRobotSPARE_X106
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X106)];
            }
        }
        public Tag xOutRobotSPARE_X107
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X107)];
            }
        }
        public Tag xOutRobotSPARE_X108
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X108)];
            }
        }
        public Tag xOutRobotSPARE_X109
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X109)];
            }
        }
        public Tag xOutRobotSPARE_X110
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X110)];
            }
        }
        public Tag xOutRobotSPARE_X111
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X111)];
            }
        }
        public Tag xOutRobotSPARE_X112
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X112)];
            }
        }
        public Tag xOutRobotSPARE_X113
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X113)];
            }
        }
        public Tag xOutRobotSPARE_X114
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X114)];
            }
        }
        public Tag xOutRobotSPARE_X115
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X115)];
            }
        }
        public Tag xOutRobotSPARE_X116
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X116)];
            }
        }
        public Tag xOutRobotSPARE_X117
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X117)];
            }
        }
        public Tag xOutRobotSPARE_X118
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X118)];
            }
        }
        public Tag xOutRobotSPARE_X119
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X119)];
            }
        }
        public Tag xOutRobotSPARE_X120
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X120)];
            }
        }
        public Tag xOutRobotSPARE_X121
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X121)];
            }
        }
        public Tag xOutRobotSPARE_X122
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X122)];
            }
        }
        public Tag xOutRobotSPARE_X123
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X123)];
            }
        }
        public Tag xOutRobotSPARE_X124
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X124)];
            }
        }
        public Tag xOutRobotSPARE_X125
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X125)];
            }
        }
        public Tag xOutRobotSPARE_X126
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X126)];
            }
        }
        public Tag xOutRobotSPARE_X127
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X127)];
            }
        }
        public Tag xOutRobotSPARE_X128
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X128)];
            }
        }
        public object yOutRobotEXTERNAL_PLAY_START_U1
        {
            get
            {
                return _tags[nameof(yOutRobotEXTERNAL_PLAY_START_U1)];
            }
            set
            {
                _tags[nameof(yOutRobotEXTERNAL_PLAY_START_U1)].Is = value;
            }
        }
        public object yOutRobotPLAY_STOP_U1
        {
            get
            {
                return _tags[nameof(yOutRobotPLAY_STOP_U1)];
            }
            set
            {
                _tags[nameof(yOutRobotPLAY_STOP_U1)].Is = value;
            }
        }
        public object yOutRobotFAILURE_RESET
        {
            get
            {
                return _tags[nameof(yOutRobotFAILURE_RESET)];
            }
            set
            {
                _tags[nameof(yOutRobotFAILURE_RESET)].Is = value;
            }
        }
        public object yOutRobotMOTOR_ON_EXTERNAL
        {
            get
            {
                return _tags[nameof(yOutRobotMOTOR_ON_EXTERNAL)];
            }
            set
            {
                _tags[nameof(yOutRobotMOTOR_ON_EXTERNAL)].Is = value;
            }
        }
        public object yOutRobotMOTOR_OFF_EXTERNAL
        {
            get
            {
                return _tags[nameof(yOutRobotMOTOR_OFF_EXTERNAL)];
            }
            set
            {
                _tags[nameof(yOutRobotMOTOR_OFF_EXTERNAL)].Is = value;
            }
        }
        public object yOutRobotREDUCE_SPEED
        {
            get
            {
                return _tags[nameof(yOutRobotREDUCE_SPEED)];
            }
            set
            {
                _tags[nameof(yOutRobotREDUCE_SPEED)].Is = value;
            }
        }
        public object yOutRobotPAUSE
        {
            get
            {
                return _tags[nameof(yOutRobotPAUSE)];
            }
            set
            {
                _tags[nameof(yOutRobotPAUSE)].Is = value;
            }
        }
        public object yOutRobotEXTERNAL_RESET
        {
            get
            {
                return _tags[nameof(yOutRobotEXTERNAL_RESET)];
            }
            set
            {
                _tags[nameof(yOutRobotEXTERNAL_RESET)].Is = value;
            }
        }
        public object yOutRobotORIGIN_RETURN
        {
            get
            {
                return _tags[nameof(yOutRobotORIGIN_RETURN)];
            }
            set
            {
                _tags[nameof(yOutRobotORIGIN_RETURN)].Is = value;
            }
        }
        public object yOutRobotCYCLE_STOP
        {
            get
            {
                return _tags[nameof(yOutRobotCYCLE_STOP)];
            }
            set
            {
                _tags[nameof(yOutRobotCYCLE_STOP)].Is = value;
            }
        }
        public object yOutRobotRECIPE_OK
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_OK)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_OK)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_1
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_1)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_1)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_2
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_2)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_2)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_3
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_3)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_3)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_4
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_4)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_4)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_5
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_5)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_5)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_6
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_6)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_6)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_7
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_7)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_7)].Is = value;
            }
        }
        public object yOutRobotPROGRAM_SELECT_BIT_U1_8
        {
            get
            {
                return _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_8)];
            }
            set
            {
                _tags[nameof(yOutRobotPROGRAM_SELECT_BIT_U1_8)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_1
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_1)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_1)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_2
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_2)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_2)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_3
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_3)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_3)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_4
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_4)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_4)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_5
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_5)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_5)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_6
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_6)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_6)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_7
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_7)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_7)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_8
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_8)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_8)].Is = value;
            }
        }
        public object yOutRobotAUTO_RETRY_REQUEST
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_RETRY_REQUEST)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_RETRY_REQUEST)].Is = value;
            }
        }
        public object yOutRobotMANUAL_MODE_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_MODE_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_MODE_SELECT)].Is = value;
            }
        }
        public object yOutRobotMANUAL_MOVE_GO
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_MOVE_GO)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_MOVE_GO)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y031
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y031)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y031)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y032
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y032)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y032)].Is = value;
            }
        }
        public object yOutRobotTOOL_1_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_1_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_1_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_1_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_1_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_1_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_1_CYLINDER_UP_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_1_CYLINDER_UP_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_1_CYLINDER_UP_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_1_CYLINDER_DOWN_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_1_CYLINDER_DOWN_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_1_CYLINDER_DOWN_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_2_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_2_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_2_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_2_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_2_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_2_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_2_CYLINDER_UP_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_2_CYLINDER_UP_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_2_CYLINDER_UP_DO)].Is = value;
            }
        }
        public object yOutRobotTOOL_2_CYLINDER_DOWN_DO
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL_2_CYLINDER_DOWN_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL_2_CYLINDER_DOWN_DO)].Is = value;
            }
        }
        public object yOutRobotGET_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotGET_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotGET_SELECT)].Is = value;
            }
        }
        public object yOutRobotPUT_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotPUT_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotPUT_SELECT)].Is = value;
            }
        }
        public object yOutRobotTOOL1_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL1_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL1_SELECT)].Is = value;
            }
        }
        public object yOutRobotTOOL2_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL2_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL2_SELECT)].Is = value;
            }
        }
        public object yOutRobotTOOL12_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotTOOL12_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotTOOL12_SELECT)].Is = value;
            }
        }
        public object yOutRobotSTAGE1_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotSTAGE1_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotSTAGE1_SELECT)].Is = value;
            }
        }
        public object yOutRobotSTAGE2_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotSTAGE2_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotSTAGE2_SELECT)].Is = value;
            }
        }
        public object yOutRobotSTAGE3_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotSTAGE3_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotSTAGE3_SELECT)].Is = value;
            }
        }
        public object yOutRobotSTAGE4_SELECT
        {
            get
            {
                return _tags[nameof(yOutRobotSTAGE4_SELECT)];
            }
            set
            {
                _tags[nameof(yOutRobotSTAGE4_SELECT)].Is = value;
            }
        }
        public object yOutRobotT1_MCR_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotT1_MCR_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotT1_MCR_ACK)].Is = value;
            }
        }
        public object yOutRobotT2_MCR_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotT2_MCR_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotT2_MCR_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG1_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG1_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG1_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG1_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG1_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG1_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG2_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG2_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG2_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG2_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG2_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG2_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG3_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG3_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG3_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG3_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG3_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG3_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG4_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG4_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG4_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSTG4_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSTG4_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSTG4_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSPARE_STG1_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_STG1_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_STG1_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSPARE_STG1_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_STG1_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_STG1_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSPARE_STG2_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_STG2_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_STG2_VAC_ON_ACK)].Is = value;
            }
        }
        public object yOutRobotSPARE_STG2_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_STG2_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_STG2_VAC_OFF_ACK)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y64
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y64)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y64)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y65
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y65)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y65)].Is = value;
            }
        }
        public object yOutRobotP1_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP1_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP1_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP1_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y68
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y68)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y68)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y69
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y69)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y69)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y70
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y70)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y70)].Is = value;
            }
        }
        public object yOutRobotP2_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP2_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP2_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP2_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y73
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y73)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y73)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y74
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y74)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y74)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y75
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y75)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y75)].Is = value;
            }
        }
        public object yOutRobotP3_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP3_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP3_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP3_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP3_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP3_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y78
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y78)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y78)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y79
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y79)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y79)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y80
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y80)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y80)].Is = value;
            }
        }
        public object yOutRobotP4_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP4_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP4_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP4_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP4_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP4_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y83
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y83)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y83)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y84
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y84)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y84)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y85
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y85)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y85)].Is = value;
            }
        }
        public object yOutRobotP5_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP5_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP5_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP5_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP5_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP5_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y88
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y88)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y88)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y89
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y89)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y89)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y90
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y90)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y90)].Is = value;
            }
        }
        public object yOutRobotP6_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP6_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP6_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP6_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP6_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP6_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y93
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y93)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y93)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y94
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y94)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y94)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y95
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y95)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y95)].Is = value;
            }
        }
        public object yOutRobotP7_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP7_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP7_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP7_VISION_USE
        {
            get
            {
                return _tags[nameof(yOutRobotP7_VISION_USE)];
            }
            set
            {
                _tags[nameof(yOutRobotP7_VISION_USE)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y98
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y98)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y98)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y99
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y99)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y99)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y100
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y100)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y100)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_START_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_START_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_START_REQ)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_MOVE_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_MOVE_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_MOVE_REQ)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y103
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y103)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y103)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y104
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y104)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y104)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y105
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y105)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y105)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y106
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y106)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y106)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y107
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y107)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y107)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y108
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y108)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y108)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y109
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y109)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y109)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y110
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y110)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y110)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y111
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y111)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y111)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y112
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y112)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y112)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y113
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y113)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y113)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y114
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y114)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y114)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y115
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y115)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y115)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y116
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y116)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y116)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y117
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y117)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y117)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y118
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y118)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y118)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y119
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y119)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y119)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y120
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y120)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y120)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y121
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y121)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y121)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y122
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y122)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y122)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y123
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y123)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y123)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y124
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y124)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y124)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y125
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y125)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y125)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y126
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y126)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y126)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y127
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y127)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y127)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y128
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y128)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y128)].Is = value;
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_1
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_1)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_2
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_2)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_3
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_3)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_4
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_4)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_5
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_5)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_6
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_6)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_7
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_7)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_8
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_8)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_9
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_9)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_10
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_10)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_11
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_11)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_12
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_12)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_13
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_13)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_14
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_14)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_15
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_15)];
            }
        }
        public Tag xOutRobotWAIT_SIGNAL_NUM_U1_16
        {
            get
            {
                return _tags[nameof(xOutRobotWAIT_SIGNAL_NUM_U1_16)];
            }
        }
        public Tag xOutRobotCURRENT_POS_BITS_1
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_POS_BITS_1)];
            }
        }
        public Tag xOutRobotCURRENT_POS_BITS_2
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_POS_BITS_2)];
            }
        }
        public Tag xOutRobotCURRENT_POS_BITS_3
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_POS_BITS_3)];
            }
        }
        public Tag xOutRobotCURRENT_POS_BITS_4
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_POS_BITS_4)];
            }
        }
        public Tag xOutRobotCURRENT_POS_BITS_5
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_POS_BITS_5)];
            }
        }
        public Tag xOutRobotSPARE_WR0105
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0105)];
            }
        }
        public Tag xOutRobotSPARE_WR0106
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0106)];
            }
        }
        public Tag xOutRobotSPARE_WR0107
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0107)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_1
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_1)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_2
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_2)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_3
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_3)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_4
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_4)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_5
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_5)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BITS_6
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BITS_6)];
            }
        }
        public Tag xOutRobotSPARE_WR0114
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0114)];
            }
        }
        public Tag xOutRobotSPARE_WR0115
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0115)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_1
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_1)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_2
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_2)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_3
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_3)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_4
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_4)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_5
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_5)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_6
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_6)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_7
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_7)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_8
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_8)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_9
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_9)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_10
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_10)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_11
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_11)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_12
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_12)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_13
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_13)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_14
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_14)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_15
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_15)];
            }
        }
        public Tag xOutRobotMANUAL_ANSWER_BITS_16
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_ANSWER_BITS_16)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_ACK_1
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_ACK_1)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_ACK_2
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_ACK_2)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_ACK_3
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_ACK_3)];
            }
        }
        public Tag xOutRobotAUTO_CALIBRATION_ACK_4
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALIBRATION_ACK_4)];
            }
        }
        public Tag xOutRobotINTERFERENCE_REGION1_LEAVE
        {
            get
            {
                return _tags[nameof(xOutRobotINTERFERENCE_REGION1_LEAVE)];
            }
        }
        public Tag xOutRobotINTERFERENCE_REGION2_LEAVE
        {
            get
            {
                return _tags[nameof(xOutRobotINTERFERENCE_REGION2_LEAVE)];
            }
        }
        public Tag xOutRobotINTERFERENCE_REGION3_LEAVE
        {
            get
            {
                return _tags[nameof(xOutRobotINTERFERENCE_REGION3_LEAVE)];
            }
        }
        public Tag xOutRobotU5_RUNNING
        {
            get
            {
                return _tags[nameof(xOutRobotU5_RUNNING)];
            }
        }
        public Tag xOutRobotU5_RECEIVE_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotU5_RECEIVE_ACK)];
            }
        }
        public Tag xOutRobotU5_SEND_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotU5_SEND_ACK)];
            }
        }
        public Tag xOutRobotU4_RUNNING
        {
            get
            {
                return _tags[nameof(xOutRobotU4_RUNNING)];
            }
        }
        public Tag xOutRobotU4_RECEIVE_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotU4_RECEIVE_ACK)];
            }
        }
        public Tag xOutRobotU4_SEND_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotU4_SEND_ACK)];
            }
        }
        public Tag xOutRobotPROCESS_SKIP_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotPROCESS_SKIP_ACK)];
            }
        }
        public Tag xOutRobotPROCESS_EXIT_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotPROCESS_EXIT_ACK)];
            }
        }
        public Tag xOutRobotPROCESS_ERROR_ACK
        {
            get
            {
                return _tags[nameof(xOutRobotPROCESS_ERROR_ACK)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_1
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_1)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_2
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_2)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_3
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_3)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_4
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_4)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_5
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_5)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_6
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_6)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_7
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_7)];
            }
        }
        public Tag xOutRobotERROR_CODE_BITS_8
        {
            get
            {
                return _tags[nameof(xOutRobotERROR_CODE_BITS_8)];
            }
        }
        public Tag xOutRobotSPARE_WR0408
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0408)];
            }
        }
        public Tag xOutRobotSPARE_WR0409
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0409)];
            }
        }
        public Tag xOutRobotSPARE_WR0410
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0410)];
            }
        }
        public Tag xOutRobotSPARE_WR0411
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0411)];
            }
        }
        public Tag xOutRobotSPARE_WR0412
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0412)];
            }
        }
        public Tag xOutRobotSPARE_WR0413
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0413)];
            }
        }
        public Tag xOutRobotSPARE_WR0414
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0414)];
            }
        }
        public Tag xOutRobotSPARE_WR0415
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR0415)];
            }
        }
        public object yOutRobotMOVING_CONDITION_P1
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P1)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P1)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P2
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P2)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P2)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P3
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P3)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P3)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P4
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P4)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P4)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P5
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P5)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P5)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P6
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P6)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P6)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P7
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P7)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P7)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P8
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P8)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P8)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P9
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P9)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P9)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P10
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P10)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P10)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P11
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P11)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P11)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P12
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P12)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P12)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P13
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P13)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P13)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P14
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P14)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P14)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P15
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P15)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P15)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P16
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P16)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P16)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P17
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P17)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P17)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P18
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P18)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P18)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P19
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P19)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P19)].Is = value;
            }
        }
        public object yOutRobotMOVING_CONDITION_P20
        {
            get
            {
                return _tags[nameof(yOutRobotMOVING_CONDITION_P20)];
            }
            set
            {
                _tags[nameof(yOutRobotMOVING_CONDITION_P20)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0104
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0104)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0104)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0105
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0105)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0105)].Is = value;
            }
        }
        public object yOutRobotCOMPLETE_RETURN
        {
            get
            {
                return _tags[nameof(yOutRobotCOMPLETE_RETURN)];
            }
            set
            {
                _tags[nameof(yOutRobotCOMPLETE_RETURN)].Is = value;
            }
        }
        public object yOutRobotERROR_CODE_RETURN
        {
            get
            {
                return _tags[nameof(yOutRobotERROR_CODE_RETURN)];
            }
            set
            {
                _tags[nameof(yOutRobotERROR_CODE_RETURN)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_1
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_1)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_1)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_2
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_2)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_2)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_3
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_3)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_3)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_4
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_4)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_4)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_5
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_5)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_5)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_6
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_6)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_6)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0114
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0114)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0114)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0115
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0115)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0115)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_1
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_1)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_1)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_2
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_2)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_2)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_3
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_3)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_3)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_4
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_4)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_4)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_5
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_5)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_5)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_6
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_6)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_6)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_7
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_7)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_7)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_8
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_8)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_8)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_9
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_9)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_9)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_10
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_10)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_10)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_11
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_11)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_11)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_12
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_12)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_12)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_13
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_13)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_13)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_14
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_14)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_14)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_15
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_15)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_15)].Is = value;
            }
        }
        public object yOutRobotMANUAL_POS_BINARY_16
        {
            get
            {
                return _tags[nameof(yOutRobotMANUAL_POS_BINARY_16)];
            }
            set
            {
                _tags[nameof(yOutRobotMANUAL_POS_BINARY_16)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_REQ_1
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_1)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_1)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_REQ_2
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_2)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_2)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_REQ_3
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_3)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_3)].Is = value;
            }
        }
        public object yOutRobotAUTO_CALIBRATION_REQ_4
        {
            get
            {
                return _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_4)];
            }
            set
            {
                _tags[nameof(yOutRobotAUTO_CALIBRATION_REQ_4)].Is = value;
            }
        }
        public object yOutRobotINTERFERENCE_REGION1_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotINTERFERENCE_REGION1_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotINTERFERENCE_REGION1_PERMIT)].Is = value;
            }
        }
        public object yOutRobotINTERFERENCE_REGION2_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotINTERFERENCE_REGION2_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotINTERFERENCE_REGION2_PERMIT)].Is = value;
            }
        }
        public object yOutRobotINTERFERENCE_REGION3_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotINTERFERENCE_REGION3_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotINTERFERENCE_REGION3_PERMIT)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0307
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0307)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0307)].Is = value;
            }
        }
        public object yOutRobotU5_RECEIVE_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotU5_RECEIVE_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotU5_RECEIVE_REQ)].Is = value;
            }
        }
        public object yOutRobotU5_SEND_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotU5_SEND_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotU5_SEND_REQ)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW0310
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW0310)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW0310)].Is = value;
            }
        }
        public object yOutRobotU4_RECEIVE_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotU4_RECEIVE_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotU4_RECEIVE_REQ)].Is = value;
            }
        }
        public object yOutRobotU4_SEND_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotU4_SEND_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotU4_SEND_REQ)].Is = value;
            }
        }
        public object yOutRobotPROCESS_SKIP_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotPROCESS_SKIP_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotPROCESS_SKIP_REQ)].Is = value;
            }
        }
        public object yOutRobotPROCESS_EXIT_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotPROCESS_EXIT_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotPROCESS_EXIT_REQ)].Is = value;
            }
        }
        public object yOutRobotPROCESS_ERROR_REQ
        {
            get
            {
                return _tags[nameof(yOutRobotPROCESS_ERROR_REQ)];
            }
            set
            {
                _tags[nameof(yOutRobotPROCESS_ERROR_REQ)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_X_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_X_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_X_MINUS)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_Y_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_Y_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_Y_MINUS)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL1_T_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL1_T_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL1_T_MINUS)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_X_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_X_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_X_MINUS)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_Y_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_Y_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_Y_MINUS)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_LOW_16
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_16)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_LOW_16)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_1
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_1)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_1)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_2
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_2)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_2)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_3
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_3)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_3)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_4
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_4)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_4)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_5
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_5)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_5)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_6
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_6)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_6)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_7
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_7)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_7)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_8
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_8)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_8)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_9
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_9)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_9)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_10
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_10)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_10)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_11
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_11)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_11)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_12
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_12)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_12)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_13
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_13)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_13)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_14
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_14)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_14)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_HIGH_15
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_15)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_HIGH_15)].Is = value;
            }
        }
        public object yOutRobotALIGN_TOOL2_T_MINUS
        {
            get
            {
                return _tags[nameof(yOutRobotALIGN_TOOL2_T_MINUS)];
            }
            set
            {
                _tags[nameof(yOutRobotALIGN_TOOL2_T_MINUS)].Is = value;
            }
        }
        #endregion Properties

        #region Methods        

        public Dictionary<string, TagIdentity> GetAllData()
        {
            Dictionary<string, TagIdentity> temp = new Dictionary<string, TagIdentity>();
            foreach (Tag ti in _tags.Values)
            {
                temp.Add(ti.Name, ti.Identity);
            }

            return temp;
        }

        public Dictionary<string, Tag> GetTags()
        {
            return _tags;
        }

        public bool Load()
        {
            try
            {
                _jsonConvertor = new JavaScriptSerializer();

                string jsonString = File.ReadAllText(_filePath);

                Dictionary<string, TagIdentity> readData = _jsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

                _tags = new Dictionary<string, Tag>();

                foreach (TagIdentity ti in readData.Values)
                {
                    Tag temp = new Tag(ti);
                    _tags.Add(ti.Name, temp);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                string allText = _jsonConvertor.Serialize(GetAllData());

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(_filePath, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
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
                Logger.Error("Timeout");
                return false;
            }
        }

        private Tag GetpropertyName(string propertyName)
        {
            return _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
        }

        public bool SetsyDeviceOutRobot(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceOutRobot = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceOutRobot.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceOutRobot.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    Thread.Sleep(5);

                    if (isActionBreak == true)
                    {
                        break;
                    }

                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool SetyOutRobotFAILURE_RESET(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yOutRobotFAILURE_RESET = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xOutRobotFAILURE.Is == null)
                    {
                        continue;
                    }

                    if (xOutRobotFAILURE.Is.ToString() != Common.ToString())
                    {
                        break;
                    } // else

                    Thread.Sleep(5);

                    if (isActionBreak == true)
                    {
                        break;
                    }

                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion Methods
    }
}
