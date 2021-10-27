using EmWorks.Lib.Common;
using EmWorks.Lib.DriverCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EmWorks.DevDrv.IssoftLMK6
{
    public class EmIssoftLMK6 : DriverBase
    {
        #region Fields

        public const string LMKCameraPath = "C:\\TechnoTeam\\LabSoft\\Camera";
        public const int LMKDelay = 300;
        public const string LMKServerPath = "D:\\ENC\\Source\\LMKserver";
        public static EmIssoftClient lmkclient = new EmIssoftClient();
        public static ConcurrentQueue<MethodData> ReciveLMKQueue = new ConcurrentQueue<MethodData>();
        private EmIssoftLMK6Model _configIssoftLMK6;
        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();
        private Array _lensFactor;
        private Process[] _lmkProcess = Process.GetProcessesByName("lmk4");
        private MethodData _reciveMethod;
        private Dictionary<string, object> _refData;
        private object[] _returnParam;
        private MethodData _sendMethod = new MethodData();
        private TimeSpan _timeout = new TimeSpan(0, 0, 60);

        #endregion Fields

        #region Constructors

        public EmIssoftLMK6(Define.RunMode isSimulation) : base(isSimulation)
        {
            if (isSimulation != Define.RunMode.Real)
            {
                Initial(); // test 용
                return;
            }
            else
            {
                Initial();
            }
        }

        #endregion Constructors

        #region Events

        public event EventValueChanged LensFactor;

        #endregion Events

        #region Enums

        public enum RegionType
        {
            Rectangle,
            Line,
            Circle,
            Polygon,
            Polyline,
        }

        public enum ShowStatus
        {
            Hide,
            Show,
            ShowMinimized,
            ShowNormal,
            ShowMaximized,
            ShowFullScreen
        };

        #endregion Enums

        #region Properties

        public EmIssoftLMK6Model ConfigIssoftLMK6
        {
            get
            {
                return _configIssoftLMK6;
            }
        }

        public bool IsOpenLMK { get; private set; }

        public Array LensFactors
        {
            get
            {
                return _lensFactor;
            }
            set
            {
                if (_lensFactor != value)
                {
                    _lensFactor = value;
                    LensFactor?.Invoke();
                }
            }
        }

        #endregion Properties

        #region Methods

        public static bool Initial()
        {
            Process[] lmkserver = Process.GetProcessesByName("EmWorks.App.EmIssoftLMK6");

            try
            {
                if (lmkserver.Length <= 0)
                {
                    Process.Start(LMKServerPath + "\\EmWorks.App.EmIssoftLMK6.exe");
                }//else
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            return true;
        }

        public bool AutoScanTime()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMulaMeasureProcess(string projectFilePath)
        {
            bool ret = true;

            Thread.Sleep(1000);

            ret = BlackMuraOpenDialog();
            if (ret != true)
            {
                return false;
            }//else

            ret = BlackMuraLoadAdjFile(projectFilePath);
            if (ret != true)
            {
                return false;
            }//else

            ret = BlackMuraBrightCapture();
            if (ret != true)
            {
                return false;
            }//else

            Thread.Sleep(1000);

            ret = BlackMuraDarkCapture();
            if (ret != true)
            {
                return false;
            }//else

            ret = BlackMuraCloseDialog();
            if (ret != true)
            {
                return false;
            }//else

            return true;
        }

        public bool BlackMuraAdjImageCopy()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraAutomaticMeasure(int iBrightImage, int iDarkImage, double dTimeDelay)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iBrightImage, iDarkImage, dTimeDelay };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraBrightCapture()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraBrightLoad(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraCloseDialog()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraDarkCapture()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraDarkLoad(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public Dictionary<string, object> BlackMuraGetDstDirectory(string qrDstDirectory)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { qrDstDirectory };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> BlackMuraGetFlagsProject(int irShowCoord, int irShowMeasurementRegion, int irShowAdjustmentRegions, int irShowBrightClassification)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { irShowCoord, irShowMeasurementRegion, irShowAdjustmentRegions, irShowBrightClassification };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> BlackMuraGetMeasureFlags(int irShowDarkClassification, int irSaveAutomatically)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { irShowDarkClassification, irSaveAutomatically };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> BlackMuraGetProjectFileName(string qrFileName)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { qrFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> BlackMuraGetSerialNumber(string qrSerialNumber)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { qrSerialNumber };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool BlackMuraLoadAdjFile(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraLoadProject(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraOpenDialog()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraSaveProject(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraSetDstDirectory(string qDstDirectory)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qDstDirectory };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraSetFlagsProject(int iShowCoord, int iShowMeasurementRegion, int iShowAdjustmentRegions, int iShowBrightClassification)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iShowCoord, iShowMeasurementRegion, iShowAdjustmentRegions, iShowBrightClassification };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraSetMeasureFlags(int iShowDarkClassification, int iSaveAutomatically)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iShowDarkClassification, iSaveAutomatically };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool BlackMuraSetSerialNumber(string qSerialNumber)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qSerialNumber };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool CameraSettingProcess(string projectFilePath)
        {
            bool ret = true;

            ret = CameraSetupDialogOpen();
            if (ret != true)
            {
                return false;
            }//else

            ret = CameraSetupLoadProject(projectFilePath);
            if (ret != true)
            {
                return false;
            }//else

            ret = CameraSetupDialogClose();
            if (ret != true)
            {
                return false;
            }//else

            return ret;
        }

        public bool CameraSetupDialogClose()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            Thread.Sleep(LMKDelay);

            return ret.Result;
        }

        public bool CameraSetupDialogOpen()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            Thread.Sleep(LMKDelay);

            return ret.Result;
        }

        public bool CameraSetupLoadProject(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool CameraSetupMeasurementStart(int iLuminanceImage)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iLuminanceImage };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool CameraSetupMeasurementStop()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool CameraSetupSaveResult(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool Check_LMKisRun()
        {
            _lmkProcess = Process.GetProcessesByName("lmk4");

            if (_lmkProcess.Length > 0)
            {
                return true;
            }

            return false;
        }

        public bool CheckIsOpen()
        {
            if (Check_LMKisRun() != true)
            {
                this.IsOpenLMK = false;

                return false;
            }//else

            this.IsOpenLMK = true;

            return true;
        }

        public override bool Close()
        {
            if (this.ProgramClose() != true)
            {
                return false;
            }//else

            return true;
        }

        public Dictionary<string, object> ColorAutoScanTime(Array qslExposureTimes)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { qslExposureTimes };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool ColorAutoScanTime2()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            Thread.Sleep(LMKDelay);

            return ret.Result;
        }

        public bool ColorCaptureHighDyn(int iCountPic = 10, double dStartRatio = 10.0, double dFactor = 3.0)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iCountPic, dStartRatio, dFactor };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ColorCaptureMultiPic(int iCountPic = 10)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iCountPic };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ColorHighDynPic(double dMaxTime = 15.0, double dMinTime = 0.0, double dFactor = 3.0, int iSmear = 0, double dModFrequency = 0.0)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { dMaxTime, dMinTime, dFactor, iSmear, dModFrequency };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ColorHighDynPic2(double dMaxTime = 15.0, double dMinTime = 0.0, double dFactor = 3.0, int iPicCount = 10)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { dMaxTime, dMinTime, dFactor, iPicCount };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelClose()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelCopyItem(int iIndex)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iIndex };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelCopySelected()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public Dictionary<string, object> ExcelGetItemProperties(int iIndex, int irType, string qrName, string qrSheet, int irClear, string qrRange, int irSelected)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iIndex, irType, qrName, qrSheet, irClear, qrRange, irSelected };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> ExcelGetNumberItems(int irNumber)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { irNumber };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool ExcelOpenFile(string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelSave()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelSelectAll()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelSetItemProperties(int iIndex, string qSheet, int iClear, string qRange, int iSelect)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iIndex, qSheet, iClear, qRange, iSelect };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelSetVisible(int iVisible)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iVisible };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExcelUnselectAll()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ExecMenuPoint(string qMenuPoint)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qMenuPoint };

            var thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public Dictionary<string, object> ExecTclCommand(string qCommand, int irReturn, string qResult)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { qCommand, irReturn, qResult };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetColor(double iInputR, double iInputG, double iInputB, double iReferenceR, double iReferenceG, double iReferenceB, int iColorSpace, double iOutputColor1, double iOutputColor2, double iOutputColor3)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iInputR, iInputG, iInputB, iReferenceR, iReferenceG, iReferenceB, iColorSpace, iOutputColor1, iOutputColor2, iOutputColor3 };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetFlagsStatistic(int iTyp, int iObject, int irGeo, int irPhoto, int irMinMax, int irTime, int irHorseshoe)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iTyp, iObject, irGeo, irPhoto, irMinMax, irTime, irHorseshoe };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetFlagsStatistic2(int iTyp, int iObject, int irGeo, int irPhoto, int irMinMax, int irTime, int irMaxCountTime, int irHorseshoe)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iTyp, iObject, irGeo, irPhoto, irMinMax, irTime, irMaxCountTime, irHorseshoe };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool GetFocusFactorList(Array getArray, int index)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { getArray, index };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            if (ret.Result == null)
            {
                return false;
            }

            LensFactors = (Array)_refData["getArray"];

            return true;
        }

        public Dictionary<string, object> GetIndexOfImage(string Imagetype, int returnindex)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { Imagetype, returnindex };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetPropertiesRegion(int iList, int iIndex, int irType, int irPoints, Array slX, Array slY)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iList, iIndex, irType, irPoints, slX, slY };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public Dictionary<string, object> GetRegionListSize(int iList, int irSize)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iList, irSize };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool HighDynPic(double dMaxTime, double dMinTime, double dFactor, int iSmear, double dModFrequency)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { dMaxTime, dMinTime, dFactor, iSmear, dModFrequency };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool HighDynPic2(double dMaxTime, double dMinTime, double dFactor, int iPicCount)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { dMaxTime, dMinTime, dFactor, iPicCount };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool HighDynPic3(int iCountPic = 10, double dStartRatio = 10.0, double dFactor = 3.0)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iCountPic, dStartRatio, dFactor };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public Dictionary<string, object> ImageGetSize(int iImage, int irFirstLine, int irLastLine, int irFirstColumn, int irLastColumn, int irDimensions)
        {
            if (CheckIsOpen() != true)
            {
                return null;
            }//else

            var param = new { iImage, irFirstLine, irLastLine, irFirstColumn, irLastColumn, irDimensions };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<Dictionary<string, object>>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return _refData; });

            return ret.Result;
        }

        public bool ImageSetRefColorInRGB(int iImage, double dRed, double dGreen, double dBlue)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iImage, dRed, dGreen, dBlue };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ImageSetSize(int iIndex, int iFirstLine, int iLastLine, int iFirstColumn, int iLastColumn)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iIndex, iFirstLine, iLastLine, iFirstColumn, iLastColumn };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public void InitialConfig(string filePath)
        {
            _configIssoftLMK6 = new EmIssoftLMK6Model(filePath);
            _configIssoftLMK6.Load(ref _configIssoftLMK6);
            _configIssoftLMK6.Save();

            if ((int)IsSimulation < (int)_configIssoftLMK6.RunMode)
            {
                IsSimulation = _configIssoftLMK6.RunMode;
            } // else
        }

        public bool IssoftServerOpen()
        {
            Process[] lmkserver = Process.GetProcessesByName("EmWorks.App.EmIssoftLMK6");
            bool result = true;
            if (lmkserver.Length > 0)
            {
                if (lmkclient.IsConnect != true)
                {
                    var ret = Task<bool>.Factory.StartNew(() => { var taskresult = lmkclient.Open(); return taskresult; });

                    result = ret.Result;
                    IsOpen = lmkclient.IsConnect;
                }//else
            }//else

            return result;
        }

        public bool LoadCamera()
        {
            try
            {
                DirectoryInfo lmkDirectory = new DirectoryInfo(LMKCameraPath);

                if (_configIssoftLMK6.CameraInfo == null || _configIssoftLMK6.CameraInfo.Count != lmkDirectory.GetDirectories().Length)
                {
                    if (lmkDirectory.Exists == true)
                    {
                        _configIssoftLMK6.CameraInfo = new List<EmIssoftLMKCameraModel>();

                        foreach (DirectoryInfo item in lmkDirectory.GetDirectories())
                        {
                            var cameraInfo = new EmIssoftLMKCameraModel();
                            cameraInfo.CameraName = item.Name;

                            foreach (DirectoryInfo cameraItem in item.GetDirectories())
                            {
                                cameraInfo.LensNameList.Add(cameraItem.Name);
                            }

                            _configIssoftLMK6.CameraInfo.Add(cameraInfo);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }//else
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            return true;
        }

        public bool MergeColorImage(int iSrcGreyImage1, int iSrcGreyImage2, int iSrcGreyImage3, int iDstColorImage, int iColorSpace)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iSrcGreyImage1, iSrcGreyImage2, iSrcGreyImage3, iDstColorImage, iColorSpace };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool MultiPic(int iCountPic = 10, int iSmear = 0, double dModFrequency = 0)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iCountPic, iSmear, dModFrequency };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool MultiPic2(int iCountPic = 10)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iCountPic };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public override bool Open()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
            return true;
        }

        public bool ProgramClose()
        {
            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool ProgramOpen()
        {
            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SaveImage(int iNumber, string qFileName)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iNumber, qFileName };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SelectImage(int iIndex)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iIndex };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SendCommend(MethodData sendMethod)
        {
            try
            {
                string jsonString = _jsonConvertor.Serialize(sendMethod);

                EmIssoftClient.SendMainQueue.Enqueue(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            return true;
        }

        public bool SetFocusFactor(int iIndex)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iIndex };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetImageSize(string sImageName, int Lines, int Columns, int FirstLine, int FirstColumn)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { sImageName, Lines, Columns, FirstLine, FirstColumn };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetNewCamera(string qPathToLens)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qPathToLens };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SetNewCamera2(string qNameCamera, string qNameLens)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { qNameCamera, qNameLens };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool Show(ShowStatus iShowStatus)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iShowStatus };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SinglePic(int iSmear, double dModFrequency)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iSmear, dModFrequency };

            var thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SinglePic2()
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        public bool SplitColorImage(int iSrcColorImage, int iDstGreyImage1, int iDstGreyImage2, int iDstGreyImage3, int iColorSpace)
        {
            if (CheckIsOpen() != true)
            {
                return false;
            }//else

            var param = new { iSrcColorImage, iDstGreyImage1, iDstGreyImage2, iDstGreyImage3, iColorSpace };

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            var ret = Task<bool>.Factory.StartNew(() => { var result = Startasync(thisMethod, param); return result; });

            return ret.Result;
        }

        private dynamic Convert(dynamic source, Type dest)
        {
            dynamic returnValue;

            try
            {
                if (dest == typeof(bool))
                {
                    int intValue = System.Convert.ToInt32(source);
                    returnValue = System.Convert.ToBoolean(intValue);

                    return returnValue;
                }//else
                else if (dest == typeof(Array))
                {
                    Array Value = source;

                    return Value;
                }

                returnValue = System.Convert.ChangeType(source, dest);
            }
            catch
            {
                if (dest.IsEnum == true && Enum.IsDefined(dest, source))
                {
                    returnValue = Enum.Parse(dest, source, true);
                }
                else
                {
                    returnValue = null;
                }
            }

            return returnValue;
        }

        private bool GetThisMethod(MethodBase thisMethod, object param)
        {
            Type t = this.GetType();
            MethodInfo method = t.GetMethod(thisMethod.Name);

            try
            {
                _sendMethod._Classname = this.GetType().Name;
                _sendMethod._Funcname = thisMethod.Name.ToString();
                _sendMethod._Parameter = new List<MethodParameter>();

                for (int i = 0; i < thisMethod.GetParameters().Length; i++)
                {
                    _sendMethod._Parameter.Add(new MethodParameter()
                    {
                        _Name = thisMethod.GetParameters()[i].Name,
                        _Value = param.GetType().GetProperties()[i].GetValue(param),
                        _ValueType = method.GetParameters()[i].ParameterType.ToString(),
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private bool ReciveCheck()
        {
            MethodData commands;
            _reciveMethod = new MethodData();
            _refData = new Dictionary<string, object>();

            DateTime now = DateTime.Now;

            while (true)
            {
                if (DateTime.Now.Subtract(now) > _timeout || lmkclient.IsConnect != true)
                {
                    Logger.Error("LMK Server Timeout");

                    return false;
                }//else

                Thread.Sleep(1);

                if (ReciveLMKQueue.IsEmpty == true)
                {
                    continue;
                }//else

                if (ReciveLMKQueue.TryDequeue(out commands) == true)
                {
                    _reciveMethod = commands;
                    break;
                }//else
            }

            if (_reciveMethod._Parameter.Count > 0)
            {
                _returnParam = new object[_reciveMethod._Parameter.Count];

                for (int i = 0; i < _reciveMethod._Parameter.Count; i++)
                {
                    _returnParam[i] = Convert(_reciveMethod._Parameter[i]._Value, Type.GetType(_reciveMethod._Parameter[i]._ValueType));

                    _refData.Add(_reciveMethod._Parameter[i]._Name, _returnParam[i]);
                }
            }//else

            if (_reciveMethod == null || _reciveMethod._FunctionResult != true)
            {
                return false;
            }//else

            return true;
        }

        private bool Startasync(MethodBase thisMethod, object param)
        {
            bool ret = true;

            ret = GetThisMethod(thisMethod, param);
            if (ret != true)
            {
                return false;
            }//else

            ret = SendCommend(_sendMethod);
            if (ret != true)
            {
                return false;
            }//else

            ret = ReciveCheck();
            if (ret != true)
            {
                return false;
            }//else

            return true;
        }

        #endregion Methods
    }
}