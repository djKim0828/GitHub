using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using HsApiNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace EmWorks.DevDrv.TopconSR5000
{
    public partial class EmTopconSR5000
    {
        #region Methods

        public int Connect(Tag commandTag)
        {
            try
            {
                HsApiNet.Error ret = HsApiNet.Error.NONE;

                if (_config.connected_devices.Count <= 0)
                {
                    Logger.Infomation("No Devices detected.");
                    goto ReturnFailed;
                }

                _ActiveDeviceNo = 0;

                ret = HsApi.hsOpenDevice(_config.connected_devices[_ActiveDeviceNo], out _DeviceInfo);
                if (ret != HsApiNet.Error.NONE)
                {
                    goto ReturnFailed;
                }

                // Update Connect Tag
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    commandTag.Identity.Action, OpenClose.Open);

                Logger.Infomation("SR5000 - Connect [" + commandTag.Name + "]");

                return FunctionResult.True;

                ReturnFailed:
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    Close(commandTag);

                    UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                        commandTag.Identity.Action, OpenClose.Error);

                    Logger.Infomation("SR5000 - Failed Connection [" + commandTag.Name + "]");
                    return FunctionResult.False;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return FunctionResult.False;
            }
        }

        public void WriteLog(string msg)
        {
            DriverMessage?.Invoke(this, new EventDriverMessageArgs(msg));
            //System.Windows.Forms.Application.DoEvents();
        }

        private SpotDef CreateSpotDef()
        {
            SpotDef spotDef = new SpotDef();
            spotDef.type = SpotDataType.RANDOM;
            spotDef.rdm_spot_setting.measurement_roi = new Rect32n(0, 0,
                                        _Recipe.measurement.measurement_range.width,
                                        _Recipe.measurement.measurement_range.height);
            spotDef.rdm_spot_setting.binning_type = BinningType.TYPE1;
            spotDef.rdm_spot_setting.trimming = new Rect32n(0, 0, 0, 0);
            spotDef.rdm_spot_setting.init_shape = AreaShapeType.CIRCLE;
            spotDef.rdm_spot_setting.init_size = new Size64f(8, 8);
            spotDef.rdm_spot_setting.renumbering = true;
            spotDef.rdm_spot_setting.apply_threshold = false;

            spotDef.rdm_spot_setting.auto_spot_setting[0].enable = true;
            spotDef.rdm_spot_setting.auto_spot_setting[0].mode = 0;
            spotDef.rdm_spot_setting.auto_spot_setting[0].option = 0;
            spotDef.rdm_spot_setting.auto_spot_setting[0].spot_shape = _autoSporProperties.shapeType == 0 ? AreaShapeType.SQUARE : AreaShapeType.CIRCLE;
            spotDef.rdm_spot_setting.auto_spot_setting[0].spot_size = new Size64f(_autoSporProperties.width,
                                                                                  _autoSporProperties.height);
            spotDef.rdm_spot_setting.auto_spot_setting[0].area_min = _autoSporProperties.min;
            spotDef.rdm_spot_setting.auto_spot_setting[0].area_max = _autoSporProperties.max;
            spotDef.rdm_spot_setting.auto_spot_setting[0].missing_spot_enable = false;
            spotDef.rdm_spot_setting.auto_spot_setting[0].outside_spot_enable = false;
            spotDef.rdm_spot_setting.auto_spot_setting[0].pitch = new Size32n(0, 0);
            spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].enable = true;

            spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].data_type = DataType.Y;
            spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].threshold = Convert.ToSingle(_autoSporProperties.yTh);
            spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].threshold_type = AutoSpotThresholdType.LOWER_BOUND_VALUE;

            return spotDef;
        }

        private int CreatMeasurementData(Tag commandTag)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsCreateMeasurementData(_Recipe, out _measurementDataId);

            if (ret != Error.NONE)
            {
                ShowError(ret, commandTag, FunctionResult.False);
                return (int)ret;
            }

            this._wavelength = (_Recipe.measurement.measurement_wavelength_max -
                _Recipe.measurement.measurement_wavelength_min) /
                _Recipe.measurement.measurement_wavelength_step + 1;

            //SuccessCommand(commandTag);

            return (int)Error.NONE;
        }

        private void DestroyMeasurement(Tag commandTag)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsDestroyMeasurementData(_measurementDataId);

            if (ret != Error.NONE)
            {
                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }

            SuccessCommand(commandTag);

            // 해당 명령은 Load Mes를 꺼주어야한다.
            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                "LoadMeasurement", FunctionResult.False);

            return;
        }

        private void DeviceClose()
        {
            HsApiNet.Error ret = HsApiNet.Error.NONE;

            // This is a procedure to close device.
            if (_DeviceInfo != null)
            {
                ret = HsApi.hsCloseDevice(_DeviceInfo.device_id);
                if (ret != HsApiNet.Error.NONE)
                {
                    ShowError(ret, null, FunctionResult.False);
                    return;
                }
            }

            // This is a procedure for system shutdown.
            ret = HsApi.hsFinalize();
            if (ret != HsApiNet.Error.NONE)
            {
                ShowError(ret, null, FunctionResult.False);
                return;
            }
        }

        private void GetLiveViewImage(Tag commadnTag)
        {
            Error ret = Error.NONE;

            try
            {
                Image8u3 captureImage = new Image8u3();
                ret = HsApi.hsCaptureLiveImage(_DeviceInfo.device_id,
                                                _liveViewFocusSetting,
                                                out captureImage,
                                                out _liveViewFocusResult);
                if (ret != Error.NONE)
                {
                    ShowError(ret, commadnTag, null);
                    return;
                }

                if (captureImage == null)
                {
                    ShowError(ret, commadnTag, null);
                    return;
                }

                // Focus
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    "GetLiveViewFocus", _liveViewFocusResult.focus_ratio);

                // Convert image from HsApi to bitmap
                Bitmap resultImage = ToBitmap(captureImage);

                SuccessCommand(commadnTag, resultImage);

                return;
            }
            catch (System.Exception ex)
            {
                ShowError(ret, commadnTag, null);

                Logger.Exception(ex);

                return;
            }
        }

        private void GetPseudoImage(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                // Tag 초기화
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    commandTag.Identity.Action, null);

                Image32f measureImage = new Image32f();
                HsApi.hsGetMeasurementImage(_measurementDataId, DataType.Y, out measureImage);

                Image8u3 captureImage = new Image8u3();
                PseudoColorSetting psSetting = new PseudoColorSetting();
                psSetting.color_setting.range_min = 0;
                psSetting.color_setting.range_max = 100;
                psSetting.color_setting.level_min = measureImage.pixels.Min();
                psSetting.color_setting.level_max = measureImage.pixels.Max();
                HsApi.hsToPseudoColor(psSetting, measureImage, null, out captureImage);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                if (captureImage == null)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                Bitmap resultImage = ToBitmap(captureImage);

                SuccessCommand(commandTag, resultImage);

                return;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                ShowError(ret, commandTag, null);

                return;
            }
        }

        private void GetRGBImage(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    commandTag.Identity.Action, null);

                Image8u3 captureImage = new Image8u3();
                RGBColorSetting colorSetting = new RGBColorSetting();
                //colorSetting.rgb_r_adjustment = 0;
                //colorSetting.rgb_g_adjustment = 0;
                //colorSetting.rgb_b_adjustment = 0;

                ret = HsApi.hsGetRGBColorImage(_measurementDataId, colorSetting, out captureImage);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, null);

                    return;
                }

                if (captureImage == null)
                {
                    ShowError(ret, commandTag, null);

                    return;
                }

                Bitmap resultImage = ToBitmap(captureImage);

                SuccessCommand(commandTag, resultImage);

                return;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                ShowError(ret, commandTag, null);

                return;
            }
        }

        private void LoadMeasurement(Tag commandTag)
        {
            Error ret = Error.NONE;

            ProgressCallback proecess_callback = new ProgressCallback(Progress);

            string filePath = commandTag.Identity.Options.ToString();

            ret = HsApi.hsLoadMeasurementData(filePath, proecess_callback, out _measurementDataId);

            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, FunctionResult.True);

            if (ret != Error.NONE)
            {
                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }

            SuccessCommand(commandTag);

            // 해당 명령은 Desc Mes를 꺼주어야한다.
            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                "DestroyMeasurement", FunctionResult.False);
        }

        private void LoadRecipe(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                string filePath = commandTag.Identity.Options.ToString();

                ret = HsApi.hsLoadRecipe(filePath, out _Recipe);

                int topX = _Recipe.measurement.measurement_range.x;
                int topY = _Recipe.measurement.measurement_range.y;

                int ROIWidth = _Recipe.measurement.measurement_range.width;
                int ROIHeight = _Recipe.measurement.measurement_range.height;

                Logger.Infomation("Range X =" + topX + " Y=" + topY + " Width=" + ROIWidth + " Height =" + ROIHeight);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    return;
                }

                SuccessCommand(commandTag);

                Logger.Infomation("SR5000 - Load Recipe  [" + commandTag.Name + "]");

                return;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }
        }

        private void Progress(RunningStatus status, float progress)
        {
            string msg = string.Empty;

            if (this._lastStatus == status)
            {
                // Overwrite console line if status is not changed.
                //      Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }

            if (status == RunningStatus.LOADING_DATA)
            {
                msg = "loading data... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.CREATING_SPOT_DATA)
            {
                msg = "creating spot data... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.WRITING_CSV)
            {
                msg = "writing csv... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.OPTIMIZING)
            {
                msg = "optimizing... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.SCANNING)
            {
                msg = "scanning... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.PROCESSING)
            {
                msg = "processing... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.SEARCH_SPECTRUM)
            {
                msg = "search spectrum... ";
                WriteLog(msg);
            }
            else if (status == RunningStatus.SIMULATION)
            {
                msg = "simulation... ";
                WriteLog(msg);
            }
            else
            {
                msg = "none ... ";
                WriteLog(msg);
            }

            WriteLog(progress.ToString() + "%");

            this._lastStatus = status;
        }

        private void SaveMeasurement(Tag commandTag)
        {
            Error ret = Error.NONE;

            string filePath = commandTag.Identity.Options;
            ret = HsApi.hsSaveMeasurementData(filePath, _measurementDataId);

            if (ret != Error.NONE)
            {
                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }

            SuccessCommand(commandTag);

            return;
        }

        private void SaveRecipe(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                string filePath = commandTag.Identity.Options;
                ret = HsApi.hsSaveRecipe(filePath, _Recipe);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    return;
                }

                SuccessCommand(commandTag);
            }
            catch
            {
                Logger.Error("Error Code [" + Error.NOT_SUPPORTED.ToString() + "]");

                UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    commandTag.Identity.Action, FunctionResult.False);

                return;
            }
        }

        private void SearchROI(Tag commandTag)
        {
            Error ret = Error.NONE;

            // Tag 초기화
            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, null);

            try
            {
                SpotDef spotDef = CreateSpotDef();

                SpotDataList spotDataList;

                spotDataList = null;

                ret = HsApi.hsCreateAutoSpot(_measurementDataId, out spotDef);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                ret = HsApi.hsSetMeasurementRandomSpotDef(_measurementDataId, spotDef);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                ProgressCallback progressCallback = new ProgressCallback(Progress);
                ret = HsApi.hsCreateSpotDataList(_measurementDataId, SpotDataType.RANDOM, progressCallback, out spotDataList);

                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                List<SpotStatisticsData> dataList = new List<SpotStatisticsData>();
                bool result = ParsingSpotData(spotDataList.spot_data, out dataList);

                if (result == false)
                {
                    ShowError(ret, commandTag, null);
                    return;
                }

                SuccessCommand(commandTag, dataList);

                return;
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);

                ShowError(ret, commandTag, null);

                return;
            }
        }

        private void SetMotionProperty(Tag commandTag)
        {
            string propertyName = commandTag.Identity.Action.ToString();

            switch (propertyName)
            {
                case "AutoSpotShapeType":
                    _autoSporProperties.shapeType = commandTag.n;
                    break;

                case "AutoSpotWidth":
                    _autoSporProperties.width = commandTag.d;
                    break;

                case "AutoSpotHeight":
                    _autoSporProperties.height = commandTag.d;
                    break;

                case "AutoSpotMin":
                    _autoSporProperties.min = commandTag.n;
                    break;

                case "AutoSpotMax":
                    _autoSporProperties.max = commandTag.n;
                    break;

                case "AutoSpotYTh":
                    _autoSporProperties.yTh = commandTag.n;
                    break;
            }
        }

        private void StartLiveView(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                // Stop capturing before changing parameters
                ret = HsApi.hsStopCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);
                    return;
                }

                // Set parameters for live image
                _liveViewSetting.binning_type = BinningType.TYPE1;
                _liveViewSetting.gain = GainType.TYPE8;
                _liveViewSetting.trigger_config.mode_type = TriggerModeType.SOFT;
                _liveViewSetting.integration_time = 5;

                // ROI size need to be suitable for image size of device
                Size32n size = null;
                HsApi.hsGetImageSize(_DeviceInfo.device_type, _liveViewSetting.binning_type, out size);
                _liveViewSetting.roi.x = 0;
                _liveViewSetting.roi.y = 0;
                _liveViewSetting.roi.width = size.width;
                _liveViewSetting.roi.height = size.height;

                _liveViewFocusSetting.enabled = true;
                _liveViewFocusSetting.focus_rect = _liveViewSetting.roi;

                // Set parameters
                ret = HsApi.hsSetLiveSetting(_DeviceInfo.device_id, _liveViewSetting);
                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);
                    return;
                }

                // If setting for integraion time is "Auto",
                // we need to call optimization for live image.
                if (_isAutoIntegrationTime)
                {
                    // Call optimazation for live
                    OptimizationResult optimizationResult = null;
                    ret = HsApi.hsOptimizeThroughLive(_DeviceInfo.device_id, out optimizationResult);
                    if (ret != Error.NONE)
                    {
                        ShowError(ret, commandTag, FunctionResult.False);
                        return;
                    }

                    // Update integration time by the result of optimization
                    _liveViewSetting.integration_time = optimizationResult.integration_time;

                    // Set parameters because integration time is updated.
                    ret = HsApi.hsSetLiveSetting(_DeviceInfo.device_id, _liveViewSetting);
                    if (ret != Error.NONE)
                    {
                        ShowError(ret, commandTag, FunctionResult.False);
                        return;
                    }
                }

                // Start capturing
                ret = HsApi.hsStartCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);
                    return;
                }

                SuccessCommand(commandTag);

                return;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }
        }

        private void StartMeasurement(Tag commandTag, int Seq_no = 1) // ProgressCallback process_callback,
        {
            Error ret = Error.NONE;

            // Tag 초기화
            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                commandTag.Identity.Action, FunctionResult.False);
            try
            {
                if (CreatMeasurementData(commandTag) != (int)Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    DestroyMeasurement(commandTag);

                    return;
                }

                ProgressCallback progressCallback = new ProgressCallback(Progress);

                ret = HsApi.hsStartMeasurement(_DeviceInfo.device_id, _measurementDataId, Seq_no, progressCallback);

                if (ret < 0)
                {
                    ShowError(ret, commandTag, FunctionResult.False);

                    DestroyMeasurement(commandTag);

                    return;
                }

                SuccessCommand(commandTag);

                return;
            }
            catch (System.Exception ex)
            {
                ShowError(ret, commandTag, FunctionResult.False);

                Logger.Exception(ex);
            }
        }

        private void StopLiveView(Tag commandTag)
        {
            Error ret = Error.NONE;

            try
            {
                // Stop capturing before changing parameters
                ret = HsApi.hsStopCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret, commandTag, FunctionResult.False);
                    return;
                }

                SuccessCommand(commandTag);

                return;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                ShowError(ret, commandTag, FunctionResult.False);

                return;
            }
        }

        private void StopMeasurement(Tag commandTag)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsStopMeasurement(_DeviceInfo.device_id);

            if (ret != Error.NONE)
            {
                ShowError(ret, commandTag, FunctionResult.False);
                return;
            }

            UpdateTagByAction(EmWorks.Lib.Core.Tag.Idx.IoType.Input,
                    "StartMeasurement", OpenClose.Close);

            SuccessCommand(commandTag);

            return;
        }

        private Bitmap ToBitmap(Image8u3 image8u3)
        {
            Bitmap bitmap = new Bitmap(image8u3.width, image8u3.height, PixelFormat.Format24bppRgb);
            BitmapData bits = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);
            IntPtr bitsAddr = bits.Scan0;

            int length = bitmap.Width * bitmap.Height * Constants.BytesPerPixel8u3;
            Marshal.Copy(image8u3.pixels, 0, bitsAddr, length);
            bitmap.UnlockBits(bits);

            return bitmap;
        }

        #endregion Methods
    }
}