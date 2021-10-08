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
    public class Robot
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;  // Todo : 사용할 Device 로 변경 必

        #endregion Fields

        #region Constructors

        public Robot(string filePath, bool isSimulationMode)
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

        ~Robot()
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
        public Tag xOutRobotCYCLE_STOP_END
        {
            get
            {
                return _tags[nameof(xOutRobotCYCLE_STOP_END)];
            }
        }
        public Tag xOutRobotORIGIN_RETURN_ERROR
        {
            get
            {
                return _tags[nameof(xOutRobotORIGIN_RETURN_ERROR)];
            }
        }
        public Tag xOutRobotCOURSE_DATA_ERROR
        {
            get
            {
                return _tags[nameof(xOutRobotCOURSE_DATA_ERROR)];
            }
        }
        public Tag xOutRobotMANUAL_MODE_ERROR
        {
            get
            {
                return _tags[nameof(xOutRobotMANUAL_MODE_ERROR)];
            }
        }
        public Tag xOutRobotSOCKET_COMMUNICATION_NG
        {
            get
            {
                return _tags[nameof(xOutRobotSOCKET_COMMUNICATION_NG)];
            }
        }
        public Tag xOutRobotSPARE_X150
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X150)];
            }
        }
        public Tag xOutRobotSPARE_X151
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X151)];
            }
        }
        public Tag xOutRobotSPARE_X152
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X152)];
            }
        }
        public Tag xOutRobotSPARE_X153
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X153)];
            }
        }
        public Tag xOutRobotUI_TASK_RUNNING
        {
            get
            {
                return _tags[nameof(xOutRobotUI_TASK_RUNNING)];
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
        public Tag xOutRobotUI_DATA_RECEIVE_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotUI_DATA_RECEIVE_COMPLETE)];
            }
        }
        public Tag xOutRobotUI_DATA_VERIFY_REQUEST
        {
            get
            {
                return _tags[nameof(xOutRobotUI_DATA_VERIFY_REQUEST)];
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
        public Tag xOutRobotSPARE_X162
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X162)];
            }
        }
        public Tag xOutRobotSPARE_X163
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X163)];
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
        public Tag xOutRobotSPARE_X166
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X166)];
            }
        }
        public Tag xOutRobotSPARE_X167
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X167)];
            }
        }
        public Tag xOutRobotSPARE_X168
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X168)];
            }
        }
        public Tag xOutRobotSPARE_X169
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X169)];
            }
        }
        public Tag xOutRobotSPARE_X170
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X170)];
            }
        }
        public Tag xOutRobotSPARE_X171
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X171)];
            }
        }
        public Tag xOutRobotSPARE_X172
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X172)];
            }
        }

        public Tag xOutRobotSPARE_X173
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X173)];
            }
        }
        public Tag xOutRobotSPARE_X174
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X174)];
            }
        }
        public Tag xOutRobotSPARE_X175
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X175)];
            }
        }
        public Tag xOutRobotSPARE_X176
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X176)];
            }
        }
        public Tag xOutRobotP1_STAGE_GET_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE_GET_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X178
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X178)];
            }
        }
        public Tag xOutRobotSPARE_X179
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X179)];
            }
        }
        public Tag xOutRobotP1_STAGE1_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE1_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotP1_STAGE1_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE1_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotP1_STAGE2_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE2_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotP1_STAGE2_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE2_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotSPARE_X184
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X184)];
            }
        }
        public Tag xOutRobotP1_ERROR
        {
            get
            {
                return _tags[nameof(xOutRobotP1_ERROR)];
            }
        }
        public Tag xOutRobotP1_STAGE_GET_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP1_STAGE_GET_COMPLETE)];
            }
        }
        public Tag xOutRobotP2_STAGE_PUT_INTERLOCK
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE_PUT_INTERLOCK)];
            }
        }
        public Tag xOutRobotSPARE_X188
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X188)];
            }
        }
        public Tag xOutRobotSPARE_X189
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X189)];
            }
        }
        public Tag xOutRobotSPARE_X190
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X190)];
            }
        }
        public Tag xOutRobotP2_STAGE1_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE1_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotP2_STAGE1_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE1_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotP2_STAGE2_VACUUM_ON_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE2_VACUUM_ON_DI)];
            }
        }
        public Tag xOutRobotP2_STAGE2_VACUUM_OFF_DI
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE2_VACUUM_OFF_DI)];
            }
        }
        public Tag xOutRobotP2_ERROR
        {
            get
            {
                return _tags[nameof(xOutRobotP2_ERROR)];
            }
        }
        public Tag xOutRobotP2_STAGE_PUT_COMPLETE
        {
            get
            {
                return _tags[nameof(xOutRobotP2_STAGE_PUT_COMPLETE)];
            }
        }
        public Tag xOutRobotSPARE_X197
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X197)];
            }
        }
        public Tag xOutRobotSPARE_X198
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X198)];
            }
        }
        public Tag xOutRobotSPARE_X199
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X199)];
            }
        }
        public Tag xOutRobotSPARE_X200
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X200)];
            }
        }
        public Tag xOutRobotSPARE_X201
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X201)];
            }
        }
        public Tag xOutRobotSPARE_X202
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X202)];
            }
        }
        public Tag xOutRobotSPARE_X203
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X203)];
            }
        }
        public Tag xOutRobotSPARE_X204
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X204)];
            }
        }
        public Tag xOutRobotSPARE_X205
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X205)];
            }
        }
        public Tag xOutRobotSPARE_X206
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X206)];
            }
        }
        public Tag xOutRobotSPARE_X207
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X207)];
            }
        }
        public Tag xOutRobotSPARE_X208
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X208)];
            }
        }
        public Tag xOutRobotSPARE_X209
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X209)];
            }
        }
        public Tag xOutRobotSPARE_X210
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X210)];
            }
        }
        public Tag xOutRobotSPARE_X211
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X211)];
            }
        }
        public Tag xOutRobotSPARE_X212
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X212)];
            }
        }
        public Tag xOutRobotSPARE_X213
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X213)];
            }
        }
        public Tag xOutRobotSPARE_X214
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X214)];
            }
        }
        public Tag xOutRobotSPARE_X215
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X215)];
            }
        }
        public Tag xOutRobotSPARE_X216
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X216)];
            }
        }
        public Tag xOutRobotSPARE_X217
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X217)];
            }
        }
        public Tag xOutRobotSPARE_X218
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X218)];
            }
        }
        public Tag xOutRobotSPARE_X219
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X219)];
            }
        }
        public Tag xOutRobotSPARE_X220
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X220)];
            }
        }
        public Tag xOutRobotSPARE_X221
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X221)];
            }
        }
        public Tag xOutRobotSPARE_X222
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X222)];
            }
        }
        public Tag xOutRobotSPARE_X223
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X223)];
            }
        }
        public Tag xOutRobotSPARE_X224
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X224)];
            }
        }
        public Tag xOutRobotSPARE_X225
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X225)];
            }
        }
        public Tag xOutRobotSPARE_X226
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X226)];
            }
        }
        public Tag xOutRobotSPARE_X227
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X227)];
            }
        }
        public Tag xOutRobotSPARE_X228
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X228)];
            }
        }
        public Tag xOutRobotSPARE_X229
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X229)];
            }
        }
        public Tag xOutRobotSPARE_X230
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X230)];
            }
        }
        public Tag xOutRobotSPARE_X231
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X231)];
            }
        }
        public Tag xOutRobotSPARE_X232
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X232)];
            }
        }
        public Tag xOutRobotSPARE_X233
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X233)];
            }
        }
        public Tag xOutRobotSPARE_X234
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X234)];
            }
        }
        public Tag xOutRobotSPARE_X235
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X235)];
            }
        }
        public Tag xOutRobotSPARE_X236
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X236)];
            }
        }
        public Tag xOutRobotSPARE_X237
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X237)];
            }
        }
        public Tag xOutRobotSPARE_X238
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X238)];
            }
        }
        public Tag xOutRobotSPARE_X239
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X239)];
            }
        }
        public Tag xOutRobotSPARE_X240
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X240)];
            }
        }
        public Tag xOutRobotSPARE_X241
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X241)];
            }
        }
        public Tag xOutRobotSPARE_X242
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X242)];
            }
        }
        public Tag xOutRobotSPARE_X243
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X243)];
            }
        }
        public Tag xOutRobotSPARE_X244
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X244)];
            }
        }
        public Tag xOutRobotSPARE_X245
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X245)];
            }
        }
        public Tag xOutRobotSPARE_X246
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X246)];
            }
        }
        public Tag xOutRobotSPARE_X247
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X247)];
            }
        }
        public Tag xOutRobotSPARE_X248
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X248)];
            }
        }
        public Tag xOutRobotSPARE_X249
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X249)];
            }
        }
        public Tag xOutRobotSPARE_X250
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X250)];
            }
        }
        public Tag xOutRobotSPARE_X251
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X251)];
            }
        }
        public Tag xOutRobotSPARE_X252
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X252)];
            }
        }
        public Tag xOutRobotSPARE_X253
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X253)];
            }
        }
        public Tag xOutRobotSPARE_X254
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X254)];
            }
        }
        public Tag xOutRobotSPARE_X255
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_X255)];
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
        public object yOutRobotSPARE_Y138
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y138)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y138)].Is = value;
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
        public object yOutRobotSPEED_OVERRIDE_INPUT_16
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_16)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_16)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_32
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_32)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_32)].Is = value;
            }
        }
        public object yOutRobotSPEED_OVERRIDE_INPUT_64
        {
            get
            {
                return _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_64)];
            }
            set
            {
                _tags[nameof(yOutRobotSPEED_OVERRIDE_INPUT_64)].Is = value;
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
        public object yOutRobotSPARE_Y157
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y157)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y157)].Is = value;
            }
        }
        public object yOutRobotUI_DATA_RECEIVE_REQUEST
        {
            get
            {
                return _tags[nameof(yOutRobotUI_DATA_RECEIVE_REQUEST)];
            }
            set
            {
                _tags[nameof(yOutRobotUI_DATA_RECEIVE_REQUEST)].Is = value;
            }
        }
        public object yOutRobotUI_DATA_VERIFY_COMPLETE
        {
            get
            {
                return _tags[nameof(yOutRobotUI_DATA_VERIFY_COMPLETE)];
            }
            set
            {
                _tags[nameof(yOutRobotUI_DATA_VERIFY_COMPLETE)].Is = value;
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
        public object yOutRobotSPARE_Y162
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y162)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y162)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y163
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y163)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y163)].Is = value;
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
        public object yOutRobotSPARE_Y166
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y166)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y166)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y167
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y167)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y167)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y168
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y168)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y168)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y169
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y169)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y169)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y170
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y170)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y170)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y171
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y171)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y171)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y172
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y172)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y172)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y173
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y173)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y173)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y174
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y174)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y174)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y175
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y175)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y175)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y176
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y176)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y176)].Is = value;
            }
        }
        public object yOutRobotP1_TOOL1_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP1_TOOL1_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_TOOL1_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP1_TOOL2_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP1_TOOL2_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_TOOL2_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP1_TOOL12_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP1_TOOL12_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_TOOL12_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP1_STAGE1_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP1_STAGE1_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_STAGE1_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotP1_STAGE1_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP1_STAGE1_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_STAGE1_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotP1_STAGE2_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP1_STAGE2_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_STAGE2_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotP1_STAGE2_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP1_STAGE2_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP1_STAGE2_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y184
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y184)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y184)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y185
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y185)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y185)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y186
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y186)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y186)].Is = value;
            }
        }
        public object yOutRobotP2_TOOL1_1_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP2_TOOL1_1_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_TOOL1_1_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP2_TOOL2_2_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP2_TOOL2_2_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_TOOL2_2_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP2_TOOL1_2_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP2_TOOL1_2_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_TOOL1_2_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP2_TOOL2_1_PERMIT
        {
            get
            {
                return _tags[nameof(yOutRobotP2_TOOL2_1_PERMIT)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_TOOL2_1_PERMIT)].Is = value;
            }
        }
        public object yOutRobotP2_STAGE1_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP2_STAGE1_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_STAGE1_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotP2_STAGE1_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP2_STAGE1_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_STAGE1_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotP2_STAGE2_VACUUM_ON_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP2_STAGE2_VACUUM_ON_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_STAGE2_VACUUM_ON_DO)].Is = value;
            }
        }
        public object yOutRobotP2_STAGE2_VACUUM_OFF_DO
        {
            get
            {
                return _tags[nameof(yOutRobotP2_STAGE2_VACUUM_OFF_DO)];
            }
            set
            {
                _tags[nameof(yOutRobotP2_STAGE2_VACUUM_OFF_DO)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y195
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y195)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y195)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y196
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y196)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y196)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y197
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y197)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y197)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y198
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y198)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y198)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y199
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y199)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y199)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y200
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y200)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y200)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y201
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y201)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y201)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y202
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y202)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y202)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y203
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y203)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y203)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y204
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y204)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y204)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y205
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y205)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y205)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y206
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y206)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y206)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y207
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y207)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y207)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y208
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y208)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y208)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y209
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y209)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y209)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y210
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y210)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y210)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y211
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y211)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y211)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y212
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y212)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y212)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y213
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y213)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y213)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y214
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y214)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y214)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y215
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y215)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y215)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y216
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y216)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y216)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y217
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y217)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y217)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y218
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y218)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y218)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y219
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y219)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y219)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y220
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y220)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y220)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y221
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y221)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y221)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y222
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y222)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y222)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y223
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y223)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y223)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y224
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y224)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y224)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y225
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y225)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y225)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y226
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y226)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y226)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y227
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y227)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y227)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y228
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y228)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y228)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y229
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y229)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y229)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y230
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y230)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y230)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y231
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y231)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y231)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y232
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y232)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y232)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y233
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y233)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y233)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y234
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y234)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y234)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y235
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y235)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y235)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y236
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y236)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y236)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y237
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y237)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y237)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y238
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y238)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y238)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y239
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y239)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y239)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y240
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y240)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y240)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y241
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y241)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y241)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y242
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y242)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y242)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y243
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y243)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y243)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y244
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y244)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y244)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y245
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y245)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y245)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y246
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y246)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y246)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y247
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y247)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y247)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y248
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y248)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y248)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y249
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y249)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y249)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y250
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y250)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y250)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y251
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y251)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y251)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y252
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y252)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y252)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y253
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y253)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y253)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y254
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y254)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y254)].Is = value;
            }
        }
        public object yOutRobotSPARE_Y255
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_Y255)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_Y255)].Is = value;
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
        public Tag xOutRobotSPARE_WR69
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR69)];
            }
        }
        public Tag xOutRobotSPARE_WR70
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR70)];
            }
        }
        public Tag xOutRobotSPARE_WR71
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR71)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BIT_1
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BIT_1)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BIT_2
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BIT_2)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BIT_3
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BIT_3)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BIT_4
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BIT_4)];
            }
        }
        public Tag xOutRobotCURRENT_RECIPE_BIT_5
        {
            get
            {
                return _tags[nameof(xOutRobotCURRENT_RECIPE_BIT_5)];
            }
        }
        public Tag xOutRobotSPARE_WR77
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR77)];
            }
        }
        public Tag xOutRobotSPARE_WR78
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR78)];
            }
        }
        public Tag xOutRobotSPARE_WR79
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR79)];
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
        public Tag xOutRobotAUTO_CALLIBRATION_END1
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALLIBRATION_END1)];
            }
        }
        public Tag xOutRobotAUTO_CALLIBRATION_END2
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALLIBRATION_END2)];
            }
        }
        public Tag xOutRobotAUTO_CALLIBRATION_END3
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALLIBRATION_END3)];
            }
        }
        public Tag xOutRobotAUTO_CALLIBRATION_END4
        {
            get
            {
                return _tags[nameof(xOutRobotAUTO_CALLIBRATION_END4)];
            }
        }
        public Tag xOutRobotSPARE_WR304
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR304)];
            }
        }
        public Tag xOutRobotSPARE_WR305
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR305)];
            }
        }
        public Tag xOutRobotSPARE_WR306
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR306)];
            }
        }
        public Tag xOutRobotSPARE_WR307
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR307)];
            }
        }
        public Tag xOutRobotSPARE_WR308
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR308)];
            }
        }
        public Tag xOutRobotSPARE_WR309
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR309)];
            }
        }
        public Tag xOutRobotSPARE_WR310
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR310)];
            }
        }
        public Tag xOutRobotSPARE_WR311
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR311)];
            }
        }
        public Tag xOutRobotSPARE_WR312
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR312)];
            }
        }
        public Tag xOutRobotSPARE_WR313
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR313)];
            }
        }
        public Tag xOutRobotSPARE_WR314
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR314)];
            }
        }
        public Tag xOutRobotSPARE_WR315
        {
            get
            {
                return _tags[nameof(xOutRobotSPARE_WR315)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_1
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_1)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_2
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_2)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_4
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_4)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_8
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_8)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_16
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_16)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_32
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_32)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_64
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_64)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_128
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_128)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_256
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_256)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_512
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_512)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_1024
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_1024)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_2048
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_2048)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_4096
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_4096)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_8192
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_8192)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_16384
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_16384)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_32768
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_32768)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_65536
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_65536)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_131072
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_131072)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_262144
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_262144)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_524288
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_524288)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_1048576
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_1048576)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_2097152
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_2097152)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_4194304
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_4194304)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_8388608
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_8388608)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_16777216
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_16777216)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_33554432
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_33554432)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_67108864
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_67108864)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_134217728
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_134217728)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_268435456
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_268435456)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_536870912
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_536870912)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_1073741824
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_1073741824)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_X_2147483648
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_X_2147483648)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_1
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_1)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_2
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_2)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_4
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_4)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_8
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_8)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_16
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_16)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_32
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_32)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_64
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_64)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_128
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_128)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_256
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_256)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_512
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_512)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_1024
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_1024)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_2048
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_2048)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_4096
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_4096)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_8192
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_8192)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_16384
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_16384)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_32768
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_32768)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_65536
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_65536)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_131072
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_131072)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_262144
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_262144)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_524288
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_524288)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_1048576
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_1048576)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_2097152
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_2097152)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_4194304
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_4194304)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_8388608
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_8388608)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_16777216
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_16777216)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_33554432
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_33554432)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_67108864
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_67108864)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_134217728
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_134217728)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_268435456
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_268435456)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_536870912
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_536870912)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_1073741824
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_1073741824)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Y_2147483648
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Y_2147483648)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_1
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_1)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_2
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_2)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_4
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_4)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_8
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_8)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_16
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_16)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_32
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_32)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_64
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_64)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_128
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_128)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_256
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_256)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_512
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_512)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_1024
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_1024)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_2048
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_2048)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_4096
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_4096)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_8192
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_8192)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_16384
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_16384)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_32768
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_32768)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_65536
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_65536)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_131072
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_131072)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_262144
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_262144)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_524288
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_524288)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_1048576
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_1048576)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_2097152
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_2097152)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_4194304
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_4194304)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_8388608
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_8388608)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_16777216
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_16777216)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_33554432
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_33554432)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_67108864
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_67108864)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_134217728
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_134217728)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_268435456
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_268435456)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_536870912
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_536870912)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_1073741824
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_1073741824)];
            }
        }
        public Tag xOutRobotROBOT_CURRENT_POSITION_Z_2147483648
        {
            get
            {
                return _tags[nameof(xOutRobotROBOT_CURRENT_POSITION_Z_2147483648)];
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
        public object yOutRobotSPARE_WW104
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW104)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW104)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW105
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW105)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW105)].Is = value;
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
        public object yOutRobotRECIPE_NO_BITS_8
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_8)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_8)].Is = value;
            }
        }
        public object yOutRobotRECIPE_NO_BITS_16
        {
            get
            {
                return _tags[nameof(yOutRobotRECIPE_NO_BITS_16)];
            }
            set
            {
                _tags[nameof(yOutRobotRECIPE_NO_BITS_16)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW113
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW113)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW113)].Is = value;
            }
        }
        public object yOutRobotSPARE_WW114
        {
            get
            {
                return _tags[nameof(yOutRobotSPARE_WW114)];
            }
            set
            {
                _tags[nameof(yOutRobotSPARE_WW114)].Is = value;
            }
        }
        public object yOutRobotPOSITION_SKIP_REQUEST
        {
            get
            {
                return _tags[nameof(yOutRobotPOSITION_SKIP_REQUEST)];
            }
            set
            {
                _tags[nameof(yOutRobotPOSITION_SKIP_REQUEST)].Is = value;
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
        public object yOutRobotLoadManualPosApproach
        {
            get
            {
                return _tags[nameof(yOutRobotLoadManualPosApproach)];
            }
            set
            {
                _tags[nameof(yOutRobotLoadManualPosApproach)].Is = value;
            }
        }
        public object yOutRobotLoadManualPosEndWait
        {
            get
            {
                return _tags[nameof(yOutRobotLoadManualPosEndWait)];
            }
            set
            {
                _tags[nameof(yOutRobotLoadManualPosEndWait)].Is = value;
            }
        }
        public object yOutRobotUnloadManualPosApproach
        {
            get
            {
                return _tags[nameof(yOutRobotUnloadManualPosApproach)];
            }
            set
            {
                _tags[nameof(yOutRobotUnloadManualPosApproach)].Is = value;
            }
        }
        public object yOutRobotUnloadManualPosEndWait
        {
            get
            {
                return _tags[nameof(yOutRobotUnloadManualPosEndWait)];
            }
            set
            {
                _tags[nameof(yOutRobotUnloadManualPosEndWait)].Is = value;
            }
        }
        public object yOutRobotInitManualPos
        {
            get
            {
                return _tags[nameof(yOutRobotInitManualPos)];
            }
            set
            {
                _tags[nameof(yOutRobotInitManualPos)].Is = value;
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
            try
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
            catch (System.Exception ex)
            {
                return false;
            }
            
        }

        private Tag GetpropertyName(string propertyName)
        {
            return _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
        }

        public bool SetsyDeviceIO(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceOutRobot = Common;

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
                }
            };

            return WaitResult(timeOut, action);
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

                    if (xOutRobotFAILURE.Is.ToString() == Common.ToString())
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
                isActionBreak = true; // action 정지
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
