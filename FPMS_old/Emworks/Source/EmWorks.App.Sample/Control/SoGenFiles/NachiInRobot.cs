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
    public class Nachi_In
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public Nachi_In(string filePath, bool isSimulationMode)
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

        ~Nachi_In()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceInRobot
        {
            get
            {
                return _tags[nameof(sxDeviceInRobot)];
            }
        }
        public object syDeviceInRobot
        {
            get
            {
                return _tags[nameof(syDeviceInRobot)];
            }
            set
            {
                _tags[nameof(syDeviceInRobot)].Is = Convert.ToDouble(value);
            }
        }
        public Tag xDeviceLibraryVersion
        {
            get
            {
                return _tags[nameof(xDeviceLibraryVersion)];
            }
        }
        public Tag xInRobotClient1Connect
        {
            get
            {
                return _tags[nameof(xInRobotClient1Connect)];
            }
        }
        public object yInRobotSendOffset
        {
            get
            {
                return _tags[nameof(yInRobotSendOffset)];
            }
            set
            {
                _tags[nameof(yInRobotSendOffset)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotOffsetdataLOAD_POSITION_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataLOAD_POSITION_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataLOAD_POSITION_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataLOAD_POSITION_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataLOAD_POSITION_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataLOAD_POSITION_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataMCR_POSITION_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataMCR_POSITION_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataMCR_POSITION_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataMCR_POSITION_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataMCR_POSITION_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataMCR_POSITION_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataUNLOAD_POSITION_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataUNLOAD_POSITION_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataUNLOAD_POSITION_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataUNLOAD_POSITION_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataUNLOAD_POSITION_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataUNLOAD_POSITION_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_A
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_A)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_A)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_B
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_B)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_B)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_C
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_C)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_C)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_D
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_D)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_D)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_C
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_C)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_C)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_D
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_D)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_D)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_C
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_C)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_C)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_D
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_D)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_D)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_E
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_E)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_E)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPN_F
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPN_F)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPN_F)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_E
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_E)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_E)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPO_F
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPO_F)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPO_F)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_E
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_E)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_E)].Is = value.ToString();
            }
        }
        public object yInRobotOffsetdataBPP_F
        {
            get
            {
                return _tags[nameof(yInRobotOffsetdataBPP_F)];
            }
            set
            {
                _tags[nameof(yInRobotOffsetdataBPP_F)].Is = value.ToString();
            }
        }
        public Tag xInRobotTEACH_MODE
        {
            get
            {
                return _tags[nameof(xInRobotTEACH_MODE)];
            }
        }
        public Tag xInRobotPLAY_BACK_MODE
        {
            get
            {
                return _tags[nameof(xInRobotPLAY_BACK_MODE)];
            }
        }
        public Tag xInRobotRUNNING_U1
        {
            get
            {
                return _tags[nameof(xInRobotRUNNING_U1)];
            }
        }
        public Tag xInRobotUNDER_STOPPING
        {
            get
            {
                return _tags[nameof(xInRobotUNDER_STOPPING)];
            }
        }
        public Tag xInRobotINTERLOCK_ALARM
        {
            get
            {
                return _tags[nameof(xInRobotINTERLOCK_ALARM)];
            }
        }
        public Tag xInRobotMOTOR_ENERGIZED
        {
            get
            {
                return _tags[nameof(xInRobotMOTOR_ENERGIZED)];
            }
        }
        public Tag xInRobotPROGRAM_ENDED_U1
        {
            get
            {
                return _tags[nameof(xInRobotPROGRAM_ENDED_U1)];
            }
        }
        public Tag xInRobotFAILURE
        {
            get
            {
                return _tags[nameof(xInRobotFAILURE)];
            }
        }
        public Tag xInRobotBATTERY_WARNING_M1
        {
            get
            {
                return _tags[nameof(xInRobotBATTERY_WARNING_M1)];
            }
        }
        public Tag xInRobotHOME_POSITION_U1_1
        {
            get
            {
                return _tags[nameof(xInRobotHOME_POSITION_U1_1)];
            }
        }
        public Tag xInRobotHOME_POSITION_U1_2
        {
            get
            {
                return _tags[nameof(xInRobotHOME_POSITION_U1_2)];
            }
        }
        public Tag xInRobotPROGRAM_SELECTED
        {
            get
            {
                return _tags[nameof(xInRobotPROGRAM_SELECTED)];
            }
        }
        public Tag xInRobotEXTERNAL_REST_ACK_NO
        {
            get
            {
                return _tags[nameof(xInRobotEXTERNAL_REST_ACK_NO)];
            }
        }
        public Tag xInRobotSTATE_OUTPUT_1
        {
            get
            {
                return _tags[nameof(xInRobotSTATE_OUTPUT_1)];
            }
        }
        public Tag xInRobotEMERGENCY_STOP
        {
            get
            {
                return _tags[nameof(xInRobotEMERGENCY_STOP)];
            }
        }
        public Tag xInRobotUNIT_READY
        {
            get
            {
                return _tags[nameof(xInRobotUNIT_READY)];
            }
        }
        public Tag xInRobotBUSY_SIGNALS
        {
            get
            {
                return _tags[nameof(xInRobotBUSY_SIGNALS)];
            }
        }
        public Tag xInRobotSPARE_X018
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X018)];
            }
        }
        public Tag xInRobotSPARE_X019
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X019)];
            }
        }
        public Tag xInRobotSPARE_X020
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X020)];
            }
        }
        public Tag xInRobotSPARE_X021
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X021)];
            }
        }
        public Tag xInRobotSPARE_X022
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X022)];
            }
        }
        public Tag xInRobotPENDANT_EMS
        {
            get
            {
                return _tags[nameof(xInRobotPENDANT_EMS)];
            }
        }
        public Tag xInRobotCONTROLLER_EMS
        {
            get
            {
                return _tags[nameof(xInRobotCONTROLLER_EMS)];
            }
        }
        public Tag xInRobotSPARE_X025
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X025)];
            }
        }
        public Tag xInRobotSPARE_X026
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X026)];
            }
        }
        public Tag xInRobotSPARE_X027
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X027)];
            }
        }
        public Tag xInRobotSPARE_X028
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X028)];
            }
        }
        public Tag xInRobotMANUAL_MODE
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_MODE)];
            }
        }
        public Tag xInRobotMANUAL_MOVE_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_MOVE_COMPLETE)];
            }
        }
        public Tag xInRobotMANUAL_MOVE_STAND_BY
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_MOVE_STAND_BY)];
            }
        }
        public Tag xInRobotSPARE_X32
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X32)];
            }
        }
        public Tag xInRobotTOOL_1_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_1_VACUUM_ON_DI)];
            }
        }
        public Tag xInRobotTOOL_1_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_1_VACUUM_OFF_DI)];
            }
        }
        public Tag xInRobotTOOL_1_CYLINDER_UP_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_1_CYLINDER_UP_DI)];
            }
        }
        public Tag xInRobotTOOL_1_CYLINDER_DOWN_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_1_CYLINDER_DOWN_DI)];
            }
        }
        public Tag xInRobotTOOL_2_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_2_VACUUM_ON_DI)];
            }
        }
        public Tag xInRobotTOOL_2_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_2_VACUUM_OFF_DI)];
            }
        }
        public Tag xInRobotTOOL_2_CYLINDER_UP_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_2_CYLINDER_UP_DI)];
            }
        }
        public Tag xInRobotTOOL_2_CYLINDER_DOWN_DI
        {
            get
            {
                return _tags[nameof(xInRobotTOOL_2_CYLINDER_DOWN_DI)];
            }
        }
        public Tag xInRobotGET_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotGET_SELECT_ACK)];
            }
        }
        public Tag xInRobotPUT_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotPUT_SELECT_ACK)];
            }
        }
        public Tag xInRobotTOOL1_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotTOOL1_SELECT_ACK)];
            }
        }
        public Tag xInRobotTOOL2_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotTOOL2_SELECT_ACK)];
            }
        }
        public Tag xInRobotTOOL12_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotTOOL12_SELECT_ACK)];
            }
        }
        public Tag xInRobotSTAGE1_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotSTAGE1_SELECT_ACK)];
            }
        }
        public Tag xInRobotSTAGE2_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotSTAGE2_SELECT_ACK)];
            }
        }
        public Tag xInRobotSTAGE3_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotSTAGE3_SELECT_ACK)];
            }
        }
        public Tag xInRobotSTAGE4_SELECT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotSTAGE4_SELECT_ACK)];
            }
        }
        public Tag xInRobotT1_MCR_REQ
        {
            get
            {
                return _tags[nameof(xInRobotT1_MCR_REQ)];
            }
        }
        public Tag xInRobotT2_MCR_REQ
        {
            get
            {
                return _tags[nameof(xInRobotT2_MCR_REQ)];
            }
        }
        public Tag xInRobotSTG1_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG1_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSTG1_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG1_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSTG2_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG2_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSTG2_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG2_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSTG3_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG3_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSTG3_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG3_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSTG4_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG4_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSTG4_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSTG4_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSPARE_STG1_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_STG1_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSPARE_STG1_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_STG1_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSPARE_STG2_VAC_ON_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_STG2_VAC_ON_REQ)];
            }
        }
        public Tag xInRobotSPARE_STG2_VAC_OFF_REQ
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_STG2_VAC_OFF_REQ)];
            }
        }
        public Tag xInRobotSPARE_X64
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X64)];
            }
        }
        public Tag xInRobotSPARE_X65
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X65)];
            }
        }
        public Tag xInRobotP1_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP1_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X67
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X67)];
            }
        }
        public Tag xInRobotSPARE_X68
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X68)];
            }
        }
        public Tag xInRobotSPARE_X69
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X69)];
            }
        }
        public Tag xInRobotP1_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP1_COMPLETE)];
            }
        }
        public Tag xInRobotP2_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP2_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X72
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X72)];
            }
        }
        public Tag xInRobotSPARE_X73
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X73)];
            }
        }
        public Tag xInRobotSPARE_X74
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X74)];
            }
        }
        public Tag xInRobotP2_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP2_COMPLETE)];
            }
        }
        public Tag xInRobotP3_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP3_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X77
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X77)];
            }
        }
        public Tag xInRobotSPARE_X78
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X78)];
            }
        }
        public Tag xInRobotSPARE_X79
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X79)];
            }
        }
        public Tag xInRobotP3_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP3_COMPLETE)];
            }
        }
        public Tag xInRobotP4_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP4_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X82
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X82)];
            }
        }
        public Tag xInRobotSPARE_X83
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X83)];
            }
        }
        public Tag xInRobotSPARE_X84
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X84)];
            }
        }
        public Tag xInRobotP4_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP4_COMPLETE)];
            }
        }
        public Tag xInRobotP5_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP5_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X87
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X87)];
            }
        }
        public Tag xInRobotSPARE_X88
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X88)];
            }
        }
        public Tag xInRobotSPARE_X89
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X89)];
            }
        }
        public Tag xInRobotP5_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP5_COMPLETE)];
            }
        }
        public Tag xInRobotP6_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP6_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X92
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X92)];
            }
        }
        public Tag xInRobotSPARE_X93
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X93)];
            }
        }
        public Tag xInRobotSPARE_X94
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X94)];
            }
        }
        public Tag xInRobotP6_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP6_COMPLETE)];
            }
        }
        public Tag xInRobotP7_INTERLOCK
        {
            get
            {
                return _tags[nameof(xInRobotP7_INTERLOCK)];
            }
        }
        public Tag xInRobotSPARE_X97
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X97)];
            }
        }
        public Tag xInRobotSPARE_X98
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X98)];
            }
        }
        public Tag xInRobotSPARE_X99
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X99)];
            }
        }
        public Tag xInRobotP7_COMPLETE
        {
            get
            {
                return _tags[nameof(xInRobotP7_COMPLETE)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_START_ACK
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_START_ACK)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_MOVE_ACK
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_MOVE_ACK)];
            }
        }
        public Tag xInRobotSPARE_X103
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X103)];
            }
        }
        public Tag xInRobotSPARE_X104
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X104)];
            }
        }
        public Tag xInRobotSPARE_X105
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X105)];
            }
        }
        public Tag xInRobotSPARE_X106
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X106)];
            }
        }
        public Tag xInRobotSPARE_X107
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X107)];
            }
        }
        public Tag xInRobotSPARE_X108
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X108)];
            }
        }
        public Tag xInRobotSPARE_X109
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X109)];
            }
        }
        public Tag xInRobotSPARE_X110
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X110)];
            }
        }
        public Tag xInRobotSPARE_X111
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X111)];
            }
        }
        public Tag xInRobotSPARE_X112
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X112)];
            }
        }
        public Tag xInRobotSPARE_X113
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X113)];
            }
        }
        public Tag xInRobotSPARE_X114
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X114)];
            }
        }
        public Tag xInRobotSPARE_X115
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X115)];
            }
        }
        public Tag xInRobotSPARE_X116
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X116)];
            }
        }
        public Tag xInRobotSPARE_X117
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X117)];
            }
        }
        public Tag xInRobotSPARE_X118
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X118)];
            }
        }
        public Tag xInRobotSPARE_X119
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X119)];
            }
        }
        public Tag xInRobotSPARE_X120
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X120)];
            }
        }
        public Tag xInRobotSPARE_X121
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X121)];
            }
        }
        public Tag xInRobotSPARE_X122
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X122)];
            }
        }
        public Tag xInRobotSPARE_X123
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X123)];
            }
        }
        public Tag xInRobotSPARE_X124
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X124)];
            }
        }
        public Tag xInRobotSPARE_X125
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X125)];
            }
        }
        public Tag xInRobotSPARE_X126
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X126)];
            }
        }
        public Tag xInRobotSPARE_X127
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X127)];
            }
        }
        public Tag xInRobotSPARE_X128
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_X128)];
            }
        }
        public object yInRobotEXTERNAL_PLAY_START_U1
        {
            get
            {
                return _tags[nameof(yInRobotEXTERNAL_PLAY_START_U1)];
            }
            set
            {
                _tags[nameof(yInRobotEXTERNAL_PLAY_START_U1)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPLAY_STOP_U1
        {
            get
            {
                return _tags[nameof(yInRobotPLAY_STOP_U1)];
            }
            set
            {
                _tags[nameof(yInRobotPLAY_STOP_U1)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotFAILURE_RESET
        {
            get
            {
                return _tags[nameof(yInRobotFAILURE_RESET)];
            }
            set
            {
                _tags[nameof(yInRobotFAILURE_RESET)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotMOTOR_ON_EXTERNAL
        {
            get
            {
                return _tags[nameof(yInRobotMOTOR_ON_EXTERNAL)];
            }
            set
            {
                _tags[nameof(yInRobotMOTOR_ON_EXTERNAL)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotMOTOR_OFF_EXTERNAL
        {
            get
            {
                return _tags[nameof(yInRobotMOTOR_OFF_EXTERNAL)];
            }
            set
            {
                _tags[nameof(yInRobotMOTOR_OFF_EXTERNAL)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotREDUCE_SPEED
        {
            get
            {
                return _tags[nameof(yInRobotREDUCE_SPEED)];
            }
            set
            {
                _tags[nameof(yInRobotREDUCE_SPEED)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPAUSE
        {
            get
            {
                return _tags[nameof(yInRobotPAUSE)];
            }
            set
            {
                _tags[nameof(yInRobotPAUSE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotEXTERNAL_RESET
        {
            get
            {
                return _tags[nameof(yInRobotEXTERNAL_RESET)];
            }
            set
            {
                _tags[nameof(yInRobotEXTERNAL_RESET)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotORIGIN_RETURN
        {
            get
            {
                return _tags[nameof(yInRobotORIGIN_RETURN)];
            }
            set
            {
                _tags[nameof(yInRobotORIGIN_RETURN)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotCYCLE_STOP
        {
            get
            {
                return _tags[nameof(yInRobotCYCLE_STOP)];
            }
            set
            {
                _tags[nameof(yInRobotCYCLE_STOP)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotRECIPE_OK
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_OK)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_OK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_1
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_1)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_1)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_2
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_2)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_2)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_3
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_3)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_3)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_4
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_4)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_4)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_5
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_5)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_5)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_6
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_6)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_6)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_7
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_7)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_7)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPROGRAM_SELECT_BIT_U1_8
        {
            get
            {
                return _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_8)];
            }
            set
            {
                _tags[nameof(yInRobotPROGRAM_SELECT_BIT_U1_8)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_1
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_1)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_1)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_2
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_2)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_2)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_3
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_3)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_3)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_4
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_4)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_4)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_5
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_5)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_5)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_6
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_6)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_6)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_7
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_7)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_7)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPEED_OVERRIDE_INPUT_8
        {
            get
            {
                return _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_8)];
            }
            set
            {
                _tags[nameof(yInRobotSPEED_OVERRIDE_INPUT_8)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotAUTO_RETRY_REQUEST
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_RETRY_REQUEST)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_RETRY_REQUEST)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotMANUAL_MODE_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_MODE_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_MODE_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotMANUAL_MOVE_GO
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_MOVE_GO)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_MOVE_GO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y031
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y031)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y031)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y032
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y032)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y032)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_1_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_1_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_1_VACUUM_ON_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_1_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_1_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_1_VACUUM_OFF_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_1_CYLINDER_UP_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_1_CYLINDER_UP_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_1_CYLINDER_UP_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_1_CYLINDER_DOWN_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_1_CYLINDER_DOWN_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_1_CYLINDER_DOWN_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_2_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_2_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_2_VACUUM_ON_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_2_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_2_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_2_VACUUM_OFF_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_2_CYLINDER_UP_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_2_CYLINDER_UP_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_2_CYLINDER_UP_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL_2_CYLINDER_DOWN_DO
        {
            get
            {
                return _tags[nameof(yInRobotTOOL_2_CYLINDER_DOWN_DO)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL_2_CYLINDER_DOWN_DO)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotGET_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotGET_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotGET_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotPUT_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotPUT_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotPUT_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL1_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotTOOL1_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL1_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL2_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotTOOL2_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL2_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotTOOL12_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotTOOL12_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotTOOL12_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTAGE1_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotSTAGE1_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotSTAGE1_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTAGE2_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotSTAGE2_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotSTAGE2_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTAGE3_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotSTAGE3_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotSTAGE3_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTAGE4_SELECT
        {
            get
            {
                return _tags[nameof(yInRobotSTAGE4_SELECT)];
            }
            set
            {
                _tags[nameof(yInRobotSTAGE4_SELECT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotT1_MCR_ACK
        {
            get
            {
                return _tags[nameof(yInRobotT1_MCR_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotT1_MCR_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotT2_MCR_ACK
        {
            get
            {
                return _tags[nameof(yInRobotT2_MCR_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotT2_MCR_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG1_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG1_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG1_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG1_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG1_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG1_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG2_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG2_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG2_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG2_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG2_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG2_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG3_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG3_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG3_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG3_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG3_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG3_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG4_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG4_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG4_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSTG4_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSTG4_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSTG4_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_STG1_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_STG1_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_STG1_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_STG1_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_STG1_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_STG1_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_STG2_VAC_ON_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_STG2_VAC_ON_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_STG2_VAC_ON_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_STG2_VAC_OFF_ACK
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_STG2_VAC_OFF_ACK)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_STG2_VAC_OFF_ACK)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y64
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y64)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y64)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y65
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y65)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y65)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP1_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP1_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP1_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP1_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP1_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP1_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y68
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y68)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y68)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y69
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y69)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y69)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y70
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y70)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y70)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP2_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP2_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP2_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP2_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP2_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP2_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y73
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y73)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y73)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y74
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y74)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y74)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y75
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y75)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y75)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP3_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP3_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP3_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP3_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP3_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP3_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y78
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y78)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y78)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y79
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y79)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y79)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y80
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y80)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y80)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP4_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP4_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP4_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP4_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP4_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP4_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y83
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y83)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y83)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y84
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y84)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y84)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y85
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y85)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y85)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP5_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP5_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP5_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP5_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP5_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP5_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y88
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y88)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y88)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y89
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y89)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y89)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y90
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y90)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y90)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP6_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP6_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP6_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP6_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP6_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP6_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y93
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y93)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y93)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y94
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y94)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y94)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y95
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y95)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y95)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP7_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotP7_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotP7_PERMIT)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotP7_VISION_USE
        {
            get
            {
                return _tags[nameof(yInRobotP7_VISION_USE)];
            }
            set
            {
                _tags[nameof(yInRobotP7_VISION_USE)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y98
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y98)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y98)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y99
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y99)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y99)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y100
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y100)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y100)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_START_REQ
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_START_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_START_REQ)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_MOVE_REQ
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_MOVE_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_MOVE_REQ)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y103
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y103)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y103)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y104
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y104)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y104)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y105
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y105)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y105)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y106
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y106)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y106)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y107
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y107)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y107)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y108
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y108)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y108)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y109
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y109)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y109)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y110
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y110)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y110)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y111
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y111)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y111)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y112
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y112)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y112)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y113
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y113)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y113)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y114
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y114)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y114)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y115
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y115)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y115)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y116
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y116)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y116)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y117
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y117)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y117)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y118
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y118)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y118)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y119
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y119)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y119)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y120
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y120)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y120)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y121
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y121)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y121)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y122
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y122)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y122)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y123
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y123)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y123)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y124
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y124)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y124)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y125
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y125)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y125)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y126
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y126)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y126)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y127
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y127)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y127)].Is = Convert.ToInt32(value);
            }
        }
        public object yInRobotSPARE_Y128
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_Y128)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_Y128)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_1
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_1)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_2
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_2)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_3
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_3)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_4
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_4)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_5
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_5)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_6
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_6)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_7
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_7)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_8
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_8)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_9
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_9)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_10
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_10)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_11
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_11)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_12
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_12)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_13
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_13)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_14
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_14)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_15
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_15)];
            }
        }
        public Tag xInRobotWAIT_SIGNAL_NUM_U1_16
        {
            get
            {
                return _tags[nameof(xInRobotWAIT_SIGNAL_NUM_U1_16)];
            }
        }
        public Tag xInRobotCURRENT_POS_BITS_1
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_POS_BITS_1)];
            }
        }
        public Tag xInRobotCURRENT_POS_BITS_2
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_POS_BITS_2)];
            }
        }
        public Tag xInRobotCURRENT_POS_BITS_3
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_POS_BITS_3)];
            }
        }
        public Tag xInRobotCURRENT_POS_BITS_4
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_POS_BITS_4)];
            }
        }
        public Tag xInRobotCURRENT_POS_BITS_5
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_POS_BITS_5)];
            }
        }
        public Tag xInRobotSPARE_WR0105
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0105)];
            }
        }
        public Tag xInRobotSPARE_WR0106
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0106)];
            }
        }
        public Tag xInRobotSPARE_WR0107
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0107)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_1
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_1)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_2
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_2)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_3
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_3)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_4
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_4)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_5
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_5)];
            }
        }
        public Tag xInRobotCURRENT_RECIPE_BITS_6
        {
            get
            {
                return _tags[nameof(xInRobotCURRENT_RECIPE_BITS_6)];
            }
        }
        public Tag xInRobotSPARE_WR0114
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0114)];
            }
        }
        public Tag xInRobotSPARE_WR0115
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0115)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_1
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_1)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_2
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_2)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_3
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_3)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_4
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_4)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_5
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_5)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_6
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_6)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_7
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_7)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_8
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_8)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_9
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_9)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_10
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_10)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_11
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_11)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_12
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_12)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_13
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_13)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_14
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_14)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_15
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_15)];
            }
        }
        public Tag xInRobotMANUAL_ANSWER_BITS_16
        {
            get
            {
                return _tags[nameof(xInRobotMANUAL_ANSWER_BITS_16)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_ACK_1
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_ACK_1)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_ACK_2
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_ACK_2)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_ACK_3
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_ACK_3)];
            }
        }
        public Tag xInRobotAUTO_CALIBRATION_ACK_4
        {
            get
            {
                return _tags[nameof(xInRobotAUTO_CALIBRATION_ACK_4)];
            }
        }
        public Tag xInRobotINTERFERENCE_REGION1_LEAVE
        {
            get
            {
                return _tags[nameof(xInRobotINTERFERENCE_REGION1_LEAVE)];
            }
        }
        public Tag xInRobotINTERFERENCE_REGION2_LEAVE
        {
            get
            {
                return _tags[nameof(xInRobotINTERFERENCE_REGION2_LEAVE)];
            }
        }
        public Tag xInRobotINTERFERENCE_REGION3_LEAVE
        {
            get
            {
                return _tags[nameof(xInRobotINTERFERENCE_REGION3_LEAVE)];
            }
        }
        public Tag xInRobotU5_RUNNING
        {
            get
            {
                return _tags[nameof(xInRobotU5_RUNNING)];
            }
        }
        public Tag xInRobotU5_RECEIVE_ACK
        {
            get
            {
                return _tags[nameof(xInRobotU5_RECEIVE_ACK)];
            }
        }
        public Tag xInRobotU5_SEND_ACK
        {
            get
            {
                return _tags[nameof(xInRobotU5_SEND_ACK)];
            }
        }
        public Tag xInRobotU4_RUNNING
        {
            get
            {
                return _tags[nameof(xInRobotU4_RUNNING)];
            }
        }
        public Tag xInRobotU4_RECEIVE_ACK
        {
            get
            {
                return _tags[nameof(xInRobotU4_RECEIVE_ACK)];
            }
        }
        public Tag xInRobotU4_SEND_ACK
        {
            get
            {
                return _tags[nameof(xInRobotU4_SEND_ACK)];
            }
        }
        public Tag xInRobotPROCESS_SKIP_ACK
        {
            get
            {
                return _tags[nameof(xInRobotPROCESS_SKIP_ACK)];
            }
        }
        public Tag xInRobotPROCESS_EXIT_ACK
        {
            get
            {
                return _tags[nameof(xInRobotPROCESS_EXIT_ACK)];
            }
        }
        public Tag xInRobotPROCESS_ERROR_ACK
        {
            get
            {
                return _tags[nameof(xInRobotPROCESS_ERROR_ACK)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_1
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_1)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_2
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_2)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_3
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_3)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_4
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_4)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_5
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_5)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_6
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_6)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_7
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_7)];
            }
        }
        public Tag xInRobotERROR_CODE_BITS_8
        {
            get
            {
                return _tags[nameof(xInRobotERROR_CODE_BITS_8)];
            }
        }
        public Tag xInRobotSPARE_WR0408
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0408)];
            }
        }
        public Tag xInRobotSPARE_WR0409
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0409)];
            }
        }
        public Tag xInRobotSPARE_WR0410
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0410)];
            }
        }
        public Tag xInRobotSPARE_WR0411
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0411)];
            }
        }
        public Tag xInRobotSPARE_WR0412
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0412)];
            }
        }
        public Tag xInRobotSPARE_WR0413
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0413)];
            }
        }
        public Tag xInRobotSPARE_WR0414
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0414)];
            }
        }
        public Tag xInRobotSPARE_WR0415
        {
            get
            {
                return _tags[nameof(xInRobotSPARE_WR0415)];
            }
        }
        public object yInRobotMOVING_CONDITION_P1
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P1)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P2
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P2)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P3
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P3)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P4
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P4)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P5
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P5)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P6
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P6)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P7
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P7)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P8
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P8)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P9
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P9)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P10
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P10)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P11
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P11)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P12
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P12)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P13
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P13)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P14
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P14)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P15
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P15)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P16
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P16)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P17
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P17)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P17)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P18
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P18)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P18)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P19
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P19)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P19)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMOVING_CONDITION_P20
        {
            get
            {
                return _tags[nameof(yInRobotMOVING_CONDITION_P20)];
            }
            set
            {
                _tags[nameof(yInRobotMOVING_CONDITION_P20)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0104
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0104)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0104)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0105
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0105)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0105)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotCOMPLETE_RETURN
        {
            get
            {
                return _tags[nameof(yInRobotCOMPLETE_RETURN)];
            }
            set
            {
                _tags[nameof(yInRobotCOMPLETE_RETURN)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotERROR_CODE_RETURN
        {
            get
            {
                return _tags[nameof(yInRobotERROR_CODE_RETURN)];
            }
            set
            {
                _tags[nameof(yInRobotERROR_CODE_RETURN)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_1
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_1)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_2
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_2)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_3
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_3)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_4
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_4)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_5
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_5)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotRECIPE_NO_BITS_6
        {
            get
            {
                return _tags[nameof(yInRobotRECIPE_NO_BITS_6)];
            }
            set
            {
                _tags[nameof(yInRobotRECIPE_NO_BITS_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0114
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0114)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0114)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0115
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0115)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0115)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_1
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_1)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_2
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_2)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_3
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_3)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_4
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_4)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_5
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_5)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_6
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_6)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_7
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_7)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_8
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_8)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_9
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_9)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_10
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_10)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_11
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_11)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_12
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_12)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_13
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_13)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_14
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_14)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_15
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_15)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotMANUAL_POS_BINARY_16
        {
            get
            {
                return _tags[nameof(yInRobotMANUAL_POS_BINARY_16)];
            }
            set
            {
                _tags[nameof(yInRobotMANUAL_POS_BINARY_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_REQ_1
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_1)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_REQ_2
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_2)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_REQ_3
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_3)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotAUTO_CALIBRATION_REQ_4
        {
            get
            {
                return _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_4)];
            }
            set
            {
                _tags[nameof(yInRobotAUTO_CALIBRATION_REQ_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotINTERFERENCE_REGION1_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotINTERFERENCE_REGION1_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotINTERFERENCE_REGION1_PERMIT)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotINTERFERENCE_REGION2_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotINTERFERENCE_REGION2_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotINTERFERENCE_REGION2_PERMIT)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotINTERFERENCE_REGION3_PERMIT
        {
            get
            {
                return _tags[nameof(yInRobotINTERFERENCE_REGION3_PERMIT)];
            }
            set
            {
                _tags[nameof(yInRobotINTERFERENCE_REGION3_PERMIT)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0307
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0307)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0307)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotU5_RECEIVE_REQ
        {
            get
            {
                return _tags[nameof(yInRobotU5_RECEIVE_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotU5_RECEIVE_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotU5_SEND_REQ
        {
            get
            {
                return _tags[nameof(yInRobotU5_SEND_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotU5_SEND_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotSPARE_WW0310
        {
            get
            {
                return _tags[nameof(yInRobotSPARE_WW0310)];
            }
            set
            {
                _tags[nameof(yInRobotSPARE_WW0310)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotU4_RECEIVE_REQ
        {
            get
            {
                return _tags[nameof(yInRobotU4_RECEIVE_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotU4_RECEIVE_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotU4_SEND_REQ
        {
            get
            {
                return _tags[nameof(yInRobotU4_SEND_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotU4_SEND_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotPROCESS_SKIP_REQ
        {
            get
            {
                return _tags[nameof(yInRobotPROCESS_SKIP_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotPROCESS_SKIP_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotPROCESS_EXIT_REQ
        {
            get
            {
                return _tags[nameof(yInRobotPROCESS_EXIT_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotPROCESS_EXIT_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotPROCESS_ERROR_REQ
        {
            get
            {
                return _tags[nameof(yInRobotPROCESS_ERROR_REQ)];
            }
            set
            {
                _tags[nameof(yInRobotPROCESS_ERROR_REQ)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_X_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_X_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_X_MINUS)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_Y_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_Y_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_Y_MINUS)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL1_T_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL1_T_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL1_T_MINUS)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_X_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_X_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_X_MINUS)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_Y_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_Y_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_Y_MINUS)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_LOW_16
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_16)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_LOW_16)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_1
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_1)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_1)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_2
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_2)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_2)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_3
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_3)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_3)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_4
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_4)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_4)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_5
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_5)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_5)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_6
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_6)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_6)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_7
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_7)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_7)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_8
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_8)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_8)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_9
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_9)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_9)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_10
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_10)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_10)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_11
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_11)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_11)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_12
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_12)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_12)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_13
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_13)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_13)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_14
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_14)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_14)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_HIGH_15
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_15)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_HIGH_15)].Is = Convert.ToDouble(value);
            }
        }
        public object yInRobotALIGN_TOOL2_T_MINUS
        {
            get
            {
                return _tags[nameof(yInRobotALIGN_TOOL2_T_MINUS)];
            }
            set
            {
                _tags[nameof(yInRobotALIGN_TOOL2_T_MINUS)].Is = Convert.ToDouble(value);
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

        public bool SetsyDeviceInRobot(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceInRobot = Common;

            //bool isActionBreak = false;
            //Action action = () =>
            //{
            //    while (true)
            //    {
            //        if (sxDeviceInRobot.Is == null)
            //        {
            //            continue;
            //        }

            //        if (sxDeviceInRobot.Is.ToString() == Common.ToString())
            //        {
            //            break;
            //        } // else

            //        Thread.Sleep(5);

            //        if (isActionBreak == true)
            //        {
            //            break;
            //        }

            //    }
            //};

            //if (WaitResult(timeOut, action) == false)
            //{
            //    isActionBreak = true;
            //    return false;
            //}
            //else
            //{
            return true;
            //}
        }
        public bool SetyInRobotFAILURE_RESET(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            yInRobotFAILURE_RESET = Common;

            return true;
        }
        #endregion Methods
    }
}
