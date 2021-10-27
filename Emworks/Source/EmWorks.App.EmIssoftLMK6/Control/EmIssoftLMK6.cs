using EmWorks.Lib.Common;
using lmk4Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.App.EmIssoftLMK6
{
    public class EmIssoftLMK6
    {
        #region Fields

        private static EmIssoftLMK6 thisLMK6Item = new EmIssoftLMK6();
        private JavaScriptSerializer _jsonConvertor = new JavaScriptSerializer();
        private LMKAxServer _lmk6;
        private int _lmkDelay = 300;

        private Process[] _lmkProcess = Process.GetProcessesByName("lmk4");
        private Array _qslExposureTimes = new Array[5];
        private ReciveMethod _returnData = new ReciveMethod();

        #endregion Fields

        #region Enums

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

        #region Methods

        public bool AutoScanTime()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iAutoScanTime();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }
        
        public bool BlackMuraAdjImageCopy()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraAdjImageCopy();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraAutomaticMeasure(int iBrightImage, int iDarkImage, double dTimeDelay)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraAutomaticMeasure(iBrightImage, iDarkImage, dTimeDelay);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraBrightCapture()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraBrightCapture();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraBrightLoad(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraBrightLoad(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraCloseDialog()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraCloseDialog();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraDarkCapture()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraDarkCapture();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraDarkLoad(string qFileName)
        {
            if (IsOpen() != true)
            {
                return false;
            }//else

            if (_lmk6.iBlackMuraDarkLoad(qFileName) != 0)
            {
                return false;
            }//else

            return true;
        }

        public bool BlackMuraGetDstDirectory(string qrDstDirectory)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraGetDstDirectory(ref qrDstDirectory);

            var param = new { qrDstDirectory };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraGetFlagsProject(int irShowCoord, int irShowMeasurementRegion, int irShowAdjustmentRegions, int irShowBrightClassification)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraGetFlagsProject(ref irShowCoord, ref irShowMeasurementRegion, ref irShowAdjustmentRegions, ref irShowBrightClassification);

            var param = new { irShowCoord, irShowMeasurementRegion, irShowAdjustmentRegions, irShowBrightClassification };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraGetMeasureFlags(int irShowDarkClassification, int irSaveAutomatically)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraGetMeasureFlags(ref irShowDarkClassification, ref irSaveAutomatically);

            var param = new { irShowDarkClassification, irSaveAutomatically };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraGetProjectFileName(string qrFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraGetProjectFileName(ref qrFileName);

            var param = new { qrFileName };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraGetSerialNumber(string qrSerialNumber)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraGetSerialNumber(ref qrSerialNumber);

            var param = new { qrSerialNumber };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraLoadAdjFile(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraLoadAdjFile(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraLoadProject(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraLoadProject(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraOpenDialog()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraOpenDialog();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraSaveProject(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraSaveProject(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraSetDstDirectory(string qDstDirectory)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraSetDstDirectory(qDstDirectory);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraSetFlagsProject(int iShowCoord, int iShowMeasurementRegion, int iShowAdjustmentRegions, int iShowBrightClassification)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraSetFlagsProject(iShowCoord, iShowMeasurementRegion, iShowAdjustmentRegions, iShowBrightClassification);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraSetMeasureFlags(int iShowDarkClassification, int iSaveAutomatically)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraSetMeasureFlags(iShowDarkClassification, iSaveAutomatically);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool BlackMuraSetSerialNumber(string qSerialNumber)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iBlackMuraSetSerialNumber(qSerialNumber);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool CameraSetupDialogClose()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupDialogClose();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            Thread.Sleep(_lmkDelay);

            return true;
        }

        public bool CameraSetupDialogOpen()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupDialogOpen();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            Thread.Sleep(_lmkDelay);

            return true;
        }

        public bool CameraSetupLoadProject(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupLoadProject(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool CameraSetupMeasurementStart(int iLuminanceImage)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupMeasurementStart(iLuminanceImage);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool CameraSetupMeasurementStop()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupMeasurementStop();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool CameraSetupSaveResult(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iCameraSetupSaveResult(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool Check_LMKisRun()
        {
            _lmkProcess = Process.GetProcessesByName("lmk4");

            if (_lmkProcess.Length >= 0)
            {
                return true;
            }//else

            return false;
        }

        public bool ColorAutoScanTime2()
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iColorAutoScanTime(ref _qslExposureTimes);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            Thread.Sleep(_lmkDelay);

            return true;
        }

        public bool ColorCaptureHighDyn(int iCountPic = 10, double dStartRatio = 10.0, double dFactor = 3.0)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iColorCaptureHighDyn(ref _qslExposureTimes, dStartRatio, dFactor, iCountPic);

            var param = new { _qslExposureTimes };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ColorCaptureMultiPic(int iCountPic = 10)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iColorCaptureMultiPic(ref _qslExposureTimes, iCountPic);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ColorHighDynPic(double dMaxTime = 15.0, double dMinTime = 0.0, double dFactor = 3.0, int iSmear = 0, double dModFrequency = 0.0)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            if (_qslExposureTimes.GetValue(0) != null)
            {
                dMaxTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));
                foreach (var item in _qslExposureTimes)
                {
                    dMaxTime = Math.Max(dMaxTime, System.Convert.ToDouble(item));
                }
            }//else

            ret = _lmk6.iColorHighDynPic(dMaxTime, dMinTime, dFactor, iSmear, dModFrequency);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ColorHighDynPic2(double dMaxTime = 15.0, double dMinTime = 0.0, double dFactor = 3.0, int iPicCount = 10)
        {
            int ret = 0;

            if (_qslExposureTimes.GetValue(0) != null)
            {
                dMaxTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));
                foreach (var item in _qslExposureTimes)
                {
                    dMaxTime = Math.Max(dMaxTime, System.Convert.ToDouble(item));
                }
            }//else

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iColorHighDynPic2(dMaxTime, dMinTime, dFactor, iPicCount);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelClose()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelClose();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelCopyItem(int iIndex)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelCopyItem(iIndex);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelCopySelected()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelCopySelected();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelGetItemProperties(int iIndex, int irType, string qrName, string qrSheet, int irClear, string qrRange, int irSelected)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelGetItemProperties(iIndex, ref irType, ref qrName, ref qrSheet, ref irClear, ref qrRange, ref irSelected);

            var param = new { iIndex, irType, qrName, qrSheet, irClear, qrRange, irSelected };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelGetNumberItems(ref int irNumber)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelGetNumberItems(ref irNumber);

            var param = new { irNumber };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelOpenFile(string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelOpenFile(qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelSave()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelSave();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelSelectAll()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelSelectAll();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelSetItemProperties(int iIndex, string qSheet, int iClear, string qRange, int iSelect)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelSetItemProperties(iIndex, qSheet, iClear, qRange, iSelect);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelSetVisible(int iVisible)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelSetVisible(iVisible);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExcelUnselectAll()
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExcelUnselectAll();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExecMenuPoint(string qMenuPoint)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExecMenuPoint(qMenuPoint);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ExecTclCommand(string qCommand, int irReturn, string qResult)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iExecTclCommand(qCommand, ref irReturn, ref qResult);

            var param = new { qCommand, irReturn, qResult };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetColor(double iInputR, double iInputG, double iInputB, double iReferenceR, double iReferenceG, double iReferenceB, int iColorSpace, double iOutputColor1, double iOutputColor2, double iOutputColor3)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetColor(iInputR, iInputG, iInputB, iReferenceR, iReferenceG, iReferenceB, iColorSpace, ref iOutputColor1, ref iOutputColor2, ref iOutputColor3);

            var param = new { iInputR, iInputG, iInputB, iReferenceR, iReferenceG, iReferenceB, iColorSpace, iOutputColor1, iOutputColor2, iOutputColor3 };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetFlagsStatistic(int iTyp, int iObject, int irGeo, int irPhoto, int irMinMax, int irTime, int irHorseshoe)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetFlagsStatistic(iTyp, iObject, ref irGeo, ref irPhoto, ref irMinMax, ref irTime, ref irHorseshoe);

            var param = new { iTyp, iObject, irGeo, irPhoto, irMinMax, irTime, irHorseshoe };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetFlagsStatistic2(int iTyp, int iObject, int irGeo, int irPhoto, int irMinMax, int irTime, int irMaxCountTime, int irHorseshoe)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetFlagsStatistic2(iTyp, iObject, ref irGeo, ref irPhoto, ref irMinMax, ref irTime, ref irMaxCountTime, ref irHorseshoe);

            var param = new { iTyp, iObject, irGeo, irPhoto, irMinMax, irTime, irMaxCountTime, irHorseshoe };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetFocusFactorList(Array getArray, int index)
        {
            int ret = 0;

            getArray = new string[0];

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetFocusFactorList(ref getArray, ref index);

            var param = new { getArray, index };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetIndexOfImage(string Imagetype, int returnindex)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetIndexOfImage(Imagetype, ref returnindex);

            var param = new { Imagetype, returnindex };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetPropertiesRegion(int iList, int iIndex, int irType, int irPoints, Array slX, Array slY)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            slX = new string[0];
            slY = new string[0];

            ret = _lmk6.iGetPropertiesRegion(iList, iIndex, ref irType, ref irPoints, ref slX, ref slY);

            var param = new { iList, iIndex, irType, irPoints, slX, slY };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool GetRegionListSize(int iList, int irSize)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iGetRegionListSize(iList, ref irSize);

            var param = new { iList, irSize };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool HighDynPic(double dMaxTime, double dMinTime, double dFactor, int iSmear, double dModFrequency)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iHighDynPic(dMaxTime, dMinTime, dFactor, iSmear, dModFrequency);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool HighDynPic2(double dMaxTime, double dMinTime, double dFactor, int iPicCount)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iHighDynPic2(dMaxTime, dMinTime, dFactor, iPicCount);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool HighDynPic3(int iCountPic = 10, double dStartRatio = 10.0, double dFactor = 3.0)
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iHighDynPic3(dExposureTime, dStartRatio, dFactor, iCountPic);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ImageGetSize(int iImage, int irFirstLine, int irLastLine, int irFirstColumn, int irLastColumn, int irDimensions)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iImageGetSize(iImage, ref irFirstLine, ref irLastLine, ref irFirstColumn, ref irLastColumn, ref irDimensions);

            var param = new { iImage, irFirstLine, irLastLine, irFirstColumn, irLastColumn, irDimensions };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ImageSetRefColorInRGB(int iImage, double dRed, double dGreen, double dBlue)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iImageSetRefColorInRGB(iImage, dRed, dGreen, dBlue);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ImageSetSize(int iIndex, int iFirstLine, int iLastLine, int iFirstColumn, int iLastColumn)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iImageSetSize(iIndex, iFirstLine, iLastLine, iFirstColumn, iLastColumn);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool IsOpen()
        {
            if (!Check_LMKisRun())
            {
                return false;
            }//else

            if (_lmk6.iIsOpen() != 0)
            {
                return false;
            }//else

            return true;
        }

        public bool MergeColorImage(int iSrcGreyImage1, int iSrcGreyImage2, int iSrcGreyImage3, int iDstColorImage, int iColorSpace)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iMergeColorImage(iSrcGreyImage1, iSrcGreyImage2, iSrcGreyImage3, iDstColorImage, iColorSpace);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            Thread.Sleep(_lmkDelay);

            return true;
        }

        public bool MultiPic(int iCountPic = 10, int iSmear = 0, double dModFrequency = 0)
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iMultiPic(dExposureTime, iCountPic, iSmear, dModFrequency);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool MultiPic2(int iCountPic = 10)
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iMultiPic2(dExposureTime, iCountPic);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool ProgramClose(int Question)
        {
            _lmkProcess = Process.GetProcessesByName("lmk4");

            for (int i = 0; i < _lmkProcess.Length; i++)
            {
                try
                {
                    _lmkProcess[i].Kill();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    return false;
                }
            }

            return true;
        }

        public bool ProgramOpen()
        {
            int retry = 0;
            ProgramClose(0);

            Retry:
            try
            {
                _lmk6 = new LMKAxServer();
            }
            catch (Exception)
            {
                if (retry < 3)
                {
                    retry++;
                    goto Retry;
                }//else

                return false;
            }

            ;
            IsOpen();

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            int ret = _lmk6.iOpen();

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SaveImage(int iNumber, string qFileName)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSaveImage(iNumber, qFileName);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SelectImage(int iIndex)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSelectImage(iIndex);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SendCommend(ReciveMethod sendMethod)
        {
            try
            {
                string jsonString = _jsonConvertor.Serialize(sendMethod);

                ProcessMain.ReturnMainQueue.Enqueue(jsonString);
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
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSetFocusFactor(iIndex);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SetImageSize(string sImageName, int Lines, int Columns, int FirstLine, int FirstColumn)
        {
            int ret = 0;

            string sImageSizeMacro = @"source ""$ModulePath/ttip.module""
        set Error """"
        namespace eval ::ttIP {
            AllocImage {" + sImageName + "} {" + Lines + " " + Columns + " " + FirstLine + " " + FirstColumn + @"}
            SelectImage {" + sImageName + @"}
        }";

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            int irReturn = 0;
            string sResult = "";

            ret = _lmk6.iExecTclCommand(sImageSizeMacro, ref irReturn, ref sResult);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            Thread.Sleep(_lmkDelay);

            return true;
        }

        public bool SetNewCamera(string qPathToLens)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSetNewCamera(qPathToLens);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SetNewCamera2(string qNameCamera, string qNameLens)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSetNewCamera2(qNameCamera, qNameLens);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool Show(ShowStatus iShowStatus)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iShow((int)iShowStatus);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SinglePic(int iSmear, double dModFrequency)
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSinglePic(dExposureTime, iSmear, dModFrequency);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SinglePic2()
        {
            int ret = 0;

            double dExposureTime = System.Convert.ToDouble(_qslExposureTimes.GetValue(0));

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSinglePic2(dExposureTime);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool SplitColorImage(int iSrcColorImage, int iDstGreyImage1, int iDstGreyImage2, int iDstGreyImage3, int iColorSpace)
        {
            int ret = 0;

            MethodBase thisMethod = MethodBase.GetCurrentMethod();

            ret = _lmk6.iSplitColorImage(iSrcColorImage, iDstGreyImage1, iDstGreyImage2, iDstGreyImage3, iColorSpace);

            var param = new { };

            ParsingData(thisMethod, ret, param);

            return true;
        }

        public bool StartFunc(string funcname, List<MethodParameter> parameter)
        {
            object returnValue;

            Type t = this.GetType();
            MethodInfo method = t.GetMethod(funcname);

            if (parameter.Count > 0)
            {
                object[] parameters = new object[parameter.Count];

                for (int i = 0; i < parameter.Count; i++)
                {
                    parameters[i] = Convert(parameter[i]._Value, method.GetParameters()[i].ParameterType);
                }

                returnValue = method.Invoke(thisLMK6Item, parameters);
            }
            else
            {
                returnValue = method.Invoke(thisLMK6Item, null);
            }

            if (returnValue.GetType() == typeof(bool))
            {
                return (bool)returnValue;
            }//else

            return true;
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

        private bool ParsingData(MethodBase thisMethod, int ret, object param)
        {
            Type t = this.GetType();
            MethodInfo method = t.GetMethod(thisMethod.Name);

            try
            {
                _returnData._Classname = this.GetType().Name;
                _returnData._Funcname = thisMethod.Name.ToString();
                _returnData._Parameter = new List<MethodParameter>();

                for (int i = 0; i < param.GetType().GetProperties().Length; i++)
                {
                    _returnData._Parameter.Add(new MethodParameter()
                    {
                        _Name = thisMethod.GetParameters()[i].Name,
                        _Value = param.GetType().GetProperties()[i].GetValue(param),
                        _ValueType = method.GetParameters()[i].ParameterType.ToString(),
                    });
                }

                if (ret == 0)
                {
                    _returnData._FunctionResult = true;
                }
                else
                {
                    _returnData._FunctionResult = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }

            if (SendCommend(_returnData) != true)
            {
                Logger.Debug("Error> Server Return Data");

                return false;
            }//else

            return true;
        }

        #endregion Methods
    }
}