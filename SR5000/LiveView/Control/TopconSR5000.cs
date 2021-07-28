using EmWorks.Lib.Common;
using HsApiNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

//using System.ComponentModel;
//using System.Data;

namespace LiveView
{
    public class TopconSR5000
    {
        #region Fields

        public int _ActiveDeviceNo = 0;

        public DeviceInfo _DeviceInfo = null;

        public Recipe _Recipe;

        private Error _errorCode;

        /// <summary>
        /// Use auto integration time or not
        /// </summary>
        private bool _isAutoIntegrationTime = false;

        private RunningStatus _lastStatus = RunningStatus.NONE;

        /// <summary>
        /// Result of focus score calculation
        /// </summary>
        private FocusResult _liveViewFocusResult = null;

        /// <summary>
        /// Parameters for focus score calculation
        /// </summary>
        private FocusSetting _liveViewFocusSetting = new FocusSetting();

        /// <summary>
        /// Parameters to grab live image from the device
        /// </summary>
        private LiveSetting _liveViewSetting = new LiveSetting();

        private DetectorDataContents _resultData;

        // SR5000 관련
        private int _wavelength = 0;

        #endregion Fields

        #region Delegates

        public delegate void EventLoggerMessage(object sendor, EventLoggerMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventLoggerMessage LoggerMessage;

        #endregion Events

        #region Enums

        public enum StatisticsIndex
        {
            TrsistimulsX = 0,
            TrsistimulsY_Luminance,
            TrsistimulsZ,
            CIE_x,
            CIE_y,
            CIE_u_,
            CIE_v_,
            CCT,
            Deviation,
            Dominant_Wavelength,
            ExcitationPurityValue,
            LAB_L, //L_Indicateds_Lightness,
            LAB_a, //a_red_grren_coordinate,
            LAB_b, //b_yellow_blue_Coordinate,
            Choroma,
            Hue,
            R,
            G,
            B,
        }

        #endregion Enums

        #region Methods

        public void CloseDevice()
        {
            // Open 되지 않은 경우는,
            if (_DeviceInfo == null)
            {
                return;
            }

            Error ret = Error.NONE;

            // This is a procedure to close device.
            ret = HsApi.hsCloseDevice(_DeviceInfo.device_id);
            if (ret != Error.NONE)
            {
                ShowError(ret);
                return;
            }

            // This is a procedure for system shutdown.
            ret = HsApi.hsFinalize();
            if (ret != Error.NONE)
            {
                ShowError(ret);
                return;
            }
        }

        /// <summary>
        /// Convert SR-5000 Measure Data to IVL-System Data Foramt
        /// </summary>
        /// <param name="value"> SR-5000 Measrued Data </param>)
        /// <returns></returns>
        public DetectorDataContents ConvertMeastoOutputFormat(MeasurementData value)
        {
            DetectorDataContents output = new DetectorDataContents();

            //output.Trsistimuls_X = value.statistics[(int)OutputDataStatic.TrsistimulsX].average;
            //output.Trsistimuls_Y_Luminance = value.statistics[(int)OutputDataStatic.TrsistimulsY_Luminance].average;
            //output.Trsistimuls_Y_Max = value.statistics[(int)OutputDataStatic.TrsistimulsY_Luminance].max;
            ////output.Trsistimuls_Y_Min = value.statistics[(int)OutputDataStatic.TrsistimulsY_Luminance].min;
            //output.Trsistimuls_Z = value.statistics[(int)OutputDataStatic.TrsistimulsZ].average;

            //output.CIE_x = value.statistics[(int)OutputDataStatic.CIE_x].average;
            //output.CIE_y = value.statistics[(int)OutputDataStatic.CIE_y].average;
            //output.CIE_u_ = value.statistics[(int)OutputDataStatic.CIE_u_].average;
            //output.CIE_v_ = value.statistics[(int)OutputDataStatic.CIE_v_].average;
            //output.CCT = value.statistics[(int)OutputDataStatic.CCT].average;
            //output.Deviation = value.statistics[(int)OutputDataStatic.Deviation].average;
            //output.Dominant_Wavelength = value.statistics[(int)OutputDataStatic.Dominant_Wavelength].average;
            //output.ExcitationPurityValue = value.statistics[(int)OutputDataStatic.ExcitationPurityValue].average;
            //output.LAB_L = value.statistics[(int)OutputDataStatic.LAB_L].average;
            //output.LAB_a = value.statistics[(int)OutputDataStatic.LAB_a].average;
            //output.LAB_b = value.statistics[(int)OutputDataStatic.LAB_b].average;
            //output.Choroma = value.statistics[(int)OutputDataStatic.Choroma].average;
            //output.Hue = value.statistics[(int)OutputDataStatic.Hue].average;
            //output.R = value.statistics[(int)OutputDataStatic.R].average;
            //output.G = value.statistics[(int)OutputDataStatic.G].average;
            //output.B = value.statistics[(int)OutputDataStatic.B].average;

            //output.Spectral = new double[m_Wavelength];

            //for (int i = 0; i < m_Wavelength; i++)
            //{
            //    output.Spectral[i] = value.spectral_statistics[i].average;
            //    output.Calculate.Sum_Wavelength += output.Spectral[i];
            //}

            return output;
        }

        /// <summary>
        /// Creat measurment data.
        /// </summary>
        ///  <param name="rcp"> recipe info </param>
        /// <param name="measure_data_id"> measured data id </param>
        /// <returns></returns>
        public int CreatMeasurementData(Recipe rcp, ref int measure_data_id)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsCreateMeasurementData(rcp, out measure_data_id);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            this._wavelength = (rcp.measurement.measurement_wavelength_max - rcp.measurement.measurement_wavelength_min) / rcp.measurement.measurement_wavelength_step + 1;

            return (int)ret;
        }

        /// <summary>
        /// Dipose the spot data list
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <returns></returns>
        public int DestroyMeasurementData(int measure_data_id)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsDestroyMeasurementData(measure_data_id);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            return (int)ret;
        }

        /// <summary>
        /// Error Code 내용을 String 값으로 변환 합니다.
        /// </summary>
        /// <param name="errorCode"> SR-5000 ErrorCode </param>
        /// <returns></returns>
        public string ErrorCodeToString()
        {
            Error ret = Error.NONE;
            string errorTitle = "";

            ret = HsApi.hsGetErrorString(_errorCode, out errorTitle);

            if (ret != Error.NONE)
            {
                return "Error to String Change Error";
            }

            return errorTitle;
        }

        public bool GetLiveImage(out Bitmap resultImage)
        {
            try
            {
                Image8u3 captureImage = new Image8u3();
                Error ret = HsApi.hsCaptureLiveImage(_DeviceInfo.device_id, _liveViewFocusSetting, out captureImage, out _liveViewFocusResult);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    resultImage = null;
                    return false;
                }

                if (captureImage == null)
                {
                    resultImage = null;
                    return false;
                }

                // Convert image from HsApi to bitmap
                resultImage = ToBitmap(captureImage);

                return true;
            }
            catch (System.Exception ex)
            {
                resultImage = null;
                return false;
            }
        }

        /// <summary>
        /// Check if measurement data is changed or not.
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="measured_data">  measured data. </param>
        /// <returns></returns>
        public int GetMeasurementData(int measure_data_id)
        {
            Error ret = Error.NONE;
            MeasurementData meas_data = new MeasurementData();

            ret = HsApi.hsGetMeasurementData(measure_data_id, out meas_data);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            _resultData = new DetectorDataContents();
            _resultData = ConvertMeastoOutputFormat(meas_data);

            return (int)ret;
        }

        /// <summary>
        /// Start the measurement
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="Seq_no"> Sequence Number </param>
        /// <param name="process_callback"> Process callback </param>
        /// <returns></returns>
        public bool GetPixelData(int measure_data_id, int x, int y) // ProgressCallback process_callback,
        {
            try
            {
                Error ret = Error.NONE;

                Point32n p32 = new Point32n();
                p32.x = x;
                p32.y = y;

                PixelData pix = new PixelData();

                ret = HsApi.hsGetPixelData((int)_DeviceInfo.device_id, p32, out pix);
                _errorCode = ret;

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// Start the measurement
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="Seq_no"> Sequence Number </param>
        /// <param name="process_callback"> Process callback </param>
        /// <returns></returns>
        public bool GetPseudoImage(int measure_data_id, out Bitmap resultImage) // ProgressCallback process_callback,
        {
            try
            {
                Error ret = Error.NONE;

                Image32f measureImage = new Image32f();
                HsApi.hsGetMeasurementImage(measure_data_id, DataType.Y, out measureImage);

                Image8u3 captureImage = new Image8u3();
                PseudoColorSetting psSetting = new PseudoColorSetting();
                psSetting.color_setting.range_min = 0;
                psSetting.color_setting.range_max = 100;
                psSetting.color_setting.level_min = measureImage.pixels.Min();
                psSetting.color_setting.level_max = measureImage.pixels.Max();
                HsApi.hsToPseudoColor(psSetting, measureImage, null, out captureImage);

                _errorCode = ret;

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    resultImage = null;
                    return false;
                }

                if (captureImage == null)
                {
                    resultImage = null;
                    return false;
                }

                resultImage = ToBitmap(captureImage);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                resultImage = null;
                return false;
            }
        }

        /// <summary>
        /// Start the measurement
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="Seq_no"> Sequence Number </param>
        /// <param name="process_callback"> Process callback </param>
        /// <returns></returns>
        public bool GetRGBColorImage(int measure_data_id, out Bitmap resultImage) // ProgressCallback process_callback,
        {
            try
            {
                Error ret = Error.NONE;

                Image8u3 captureImage = new Image8u3();
                RGBColorSetting colorSetting = new RGBColorSetting();
                //colorSetting.rgb_r_adjustment = 0;
                //colorSetting.rgb_g_adjustment = 0;
                //colorSetting.rgb_b_adjustment = 0;

                ret = HsApi.hsGetRGBColorImage(measure_data_id, colorSetting, out captureImage);
                _errorCode = ret;

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    resultImage = null;
                    return false;
                }

                if (captureImage == null)
                {
                    resultImage = null;
                    return false;
                }

                resultImage = ToBitmap(captureImage);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                resultImage = null;
                return false;
            }
        }

        /// <summary>
        /// Start the measurement
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="Seq_no"> Sequence Number </param>
        /// <param name="process_callback"> Process callback </param>
        /// <returns></returns>
        public bool GetSpectralImage(int measure_data_id, int wavelength, out Bitmap resultImage) // ProgressCallback process_callback,
        {
            try
            {
                Error ret = Error.NONE;

                Image32f captureImage = new Image32f();

                ret = HsApi.hsGetSpectralImage(measure_data_id, wavelength, out captureImage);
                _errorCode = ret;

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    resultImage = null;
                    return false;
                }

                if (captureImage == null)
                {
                    resultImage = null;
                    return false;
                }

                resultImage = ToBitmap(captureImage);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                resultImage = null;
                return false;
            }
        }

        /// <summary>
        /// Load the Measurement Data
        /// </summary>
        /// <param name="file_path"> Loading spot file path </param>
      //  /// <param name="proecess_callback"> process callback </param>
        /// <param name="measure_data_id"> measured data id </param>
        /// <returns></returns>
        public int LoadMeasurementData(String file_path, ref int measure_data_id) // ProgressCallback proecess_callback,
        {
            Error ret = Error.NONE;

            ProgressCallback proecess_callback = new ProgressCallback(Progress);

            ret = HsApi.hsLoadMeasurementData(file_path, proecess_callback, out measure_data_id);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            return (int)ret;
        }

        /// <summary>
        /// Loads the recipe Data
        /// </summary>
        /// <param name="file_path"> Load Recipe file path </param>
        /// <param name="rcp"> recipe data </param>
        /// <returns></returns>
        public int LoadRecipe(string file_path)
        {
            try
            {
                Error ret = Error.NONE;

                ret = HsApi.hsLoadRecipe(file_path, out _Recipe);

                int topX = _Recipe.measurement.measurement_range.x;
                int topY = _Recipe.measurement.measurement_range.y;

                int ROIWidth = _Recipe.measurement.measurement_range.width;
                int ROIHeight = _Recipe.measurement.measurement_range.height;

                WriteLog("Range X =" + topX + " Y=" + topY + " Width=" + ROIWidth + " Height =" + ROIHeight);

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return -1;
                }

                return (int)ret;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        public bool OpenDevice(string calibrationPath)
        {
            try
            {
                WhiteLog("Start OpenDevice");

                Error ret = Error.NONE;

                Configuration Config = null;

                // Initializing whole system is necesarry before starting everything.
                ret = HsApi.hsInitialize(calibrationPath, out Config);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                // If there aren't any connected devices,
                // we cannot start measurement.
                if (Config.connected_devices.Count <= 0)
                {
                    CloseDevice();
                    return false;
                }

                // IDs to identify each device is stored in a array.
                // To access this ID, we need index number.
                // We use number "0" for now.
                _ActiveDeviceNo = 0;

                // This is a procedure to open connection and get device information.
                ret = HsApi.hsOpenDevice(Config.connected_devices[_ActiveDeviceNo], out _DeviceInfo);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    CloseDevice();
                    return false;
                }

                WhiteLog("Complete OpenDevice");

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool ParsingSpotData(List<SpotData> sdList, out List<SpotStatisticsData> dataList)
        {
            try
            {
                dataList = new List<SpotStatisticsData>();

                foreach (SpotData sd in sdList)
                {
                    SpotStatisticsData temp = new SpotStatisticsData();

                    temp.SpotNo = sd.spot_id;
                    temp.R = sd.statistics[(int)StatisticsIndex.R].average;
                    temp.G = sd.statistics[(int)StatisticsIndex.G].average;
                    temp.B = sd.statistics[(int)StatisticsIndex.B].average;

                    temp.CIE_U = sd.statistics[(int)StatisticsIndex.CIE_u_].average;
                    temp.CIE_V = sd.statistics[(int)StatisticsIndex.CIE_v_].average;
                    temp.CIE_X = sd.statistics[(int)StatisticsIndex.CIE_x].average;
                    temp.CIE_Y = sd.statistics[(int)StatisticsIndex.CIE_y].average;
                    temp.Dominant_Wavelength = sd.statistics[(int)StatisticsIndex.Dominant_Wavelength].average;
                    temp.Trsistimuls_X = sd.statistics[(int)StatisticsIndex.TrsistimulsX].average;
                    temp.Trsistimuls_Y_Luminance = sd.statistics[(int)StatisticsIndex.TrsistimulsY_Luminance].average;
                    temp.Trsistimuls_Z = sd.statistics[(int)StatisticsIndex.TrsistimulsZ].average;

                    temp.Spectral = new List<double>();
                    foreach (Statistics data in sd.spectral_statistics)
                    {
                        temp.Spectral.Add(data.average);
                    }

                    dataList.Add(temp);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                dataList = null;
                return false;
            }
        }

        /// <summary>
        /// Saves the Measurement Data
        /// </summary>
        /// <param name="file_path"> Loading spot file path </param>
        /// <param name="measure_data_id"> measured data id </param>
        /// <returns></returns>
        public int SaveMeasurementData(String file_path, int measure_data_id)
        {
            Error ret = Error.NONE;

            ret = HsApi.hsSaveMeasurementData(file_path, measure_data_id);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            return (int)ret;
        }

        /// <summary>
        /// Saves the recipe Data, Exception Error : exist parater is null in Recipe File(*.xml)
        /// </summary>
        /// <param name="file_path"> Save Recipe file path </param>
        /// <param name="rcp"> recipe data </param>
        /// <returns></returns>
        public int SaveRecipe(string file_path, Recipe rcp)
        {
            Error ret = Error.NONE;

            try
            {
                ret = HsApi.hsSaveRecipe(file_path, rcp);

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return -1;
                }
            }
            catch
            {
                ret = Error.NOT_SUPPORTED;
            }

            return (int)ret;
        }

        public void ShowError(Error errorCode)
        {
            this._errorCode = errorCode;

            // This is the procedure to get the error message.
            string errorMessage;
            HsApi.hsGetErrorString(errorCode, out errorMessage);

            LoggerMessage?.Invoke(this, new EventLoggerMessageArgs("Error : " + errorMessage));
        }

        public bool StartLiveView()
        {
            try
            {
                Error ret = Error.NONE;

                // Stop capturing before changing parameters
                ret = HsApi.hsStopCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
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
                    ShowError(ret);
                    return false;
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
                        ShowError(ret);
                        return false;
                    }

                    // Update integration time by the result of optimization
                    _liveViewSetting.integration_time = optimizationResult.integration_time;

                    // Set parameters because integration time is updated.
                    ret = HsApi.hsSetLiveSetting(_DeviceInfo.device_id, _liveViewSetting);
                    if (ret != Error.NONE)
                    {
                        ShowError(ret);
                        return false;
                    }
                }

                // Start capturing
                ret = HsApi.hsStartCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Start the measurement
        /// </summary>
        /// <param name="measure_data_id"> measured data id </param>
        /// <param name="Seq_no"> Sequence Number </param>
        /// <param name="process_callback"> Process callback </param>
        /// <returns></returns>
        public int StartMeasurement(int measure_data_id, int Seq_no = 1) // ProgressCallback process_callback,
        {
            Error ret = Error.NONE;

            ProgressCallback progressCallback = new ProgressCallback(Progress);

            ret = HsApi.hsStartMeasurement(_DeviceInfo.device_id, measure_data_id, Seq_no, progressCallback);
            _errorCode = ret;

            return (int)ret;
        }

        public bool StopLiveView()
        {
            try
            {
                Error ret = Error.NONE;

                // Stop capturing before changing parameters
                ret = HsApi.hsStopCapture(_DeviceInfo.device_id);
                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public int StopMeasurement()
        {
            Error ret = Error.NONE;

            //if (this.m_Simulation == true) return (int)ret;
            //if (this.m_IsConnected == false) return -1;
            //if (this.m_Device_id == "") return -1;

            ret = HsApi.hsStopMeasurement(_DeviceInfo.device_id);

            if (ret != Error.NONE)
            {
                ShowError(ret);
                return -1;
            }

            return (int)ret;
        }

        public void WhiteLog(string msg)
        {
            LoggerMessage?.Invoke(this, new EventLoggerMessageArgs(msg));
        }

        public void WriteLog(string msg)
        {
            LoggerMessage?.Invoke(this, new EventLoggerMessageArgs(msg));
            System.Windows.Forms.Application.DoEvents();
        }

        internal bool SearchROI(int measurementDataId, SpotDef spotDef, out SpotDataList spotDataList)
        {
            try
            {
                spotDataList = null;

                Error ret = Error.NONE;

                ret = HsApi.hsCreateAutoSpot(measurementDataId, out spotDef);

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                ret = HsApi.hsSetMeasurementRandomSpotDef(measurementDataId, spotDef);

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                ProgressCallback progressCallback = new ProgressCallback(Progress);
                ret = HsApi.hsCreateSpotDataList(measurementDataId, SpotDataType.RANDOM, progressCallback, out spotDataList);

                if (ret != Error.NONE)
                {
                    ShowError(ret);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);

                spotDataList = null;

                return false;
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

        private Bitmap ToBitmap(Image32f image32f)
        {
            Image32f image32f2 = image32f.Clone();
            Bitmap bitmap = new Bitmap(image32f.width, image32f.height, PixelFormat.Format8bppIndexed);
            BitmapData bits = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);
            IntPtr bitsAddr = bits.Scan0;

            int length = bitmap.Width * bitmap.Height;
            Marshal.Copy(image32f2.pixels, 0, bitsAddr, length);
            bitmap.UnlockBits(bits);

            return bitmap;
        }

        #endregion Methods

        #region Classes

        [Serializable]
        public class DetectorDataContents
        {
            #region Fields

            private double m_B;
            private SpectralCalcaulte m_Calculate;
            private double m_CCT;
            private double m_Choroma;
            private double m_CIE_u_;
            private double m_CIE_v_;
            private double m_CIE_x;
            private double m_CIE_x_Correction;
            private double m_CIE_y;
            private double m_CIE_y_Correction;
            private double[] m_ColSpectral;
            private SpectralCalcaulte m_CorrectionCalculate;
            private double m_Deviation;
            private double m_Dominant_Wavelength;
            private double m_ExcitationPurityValue;
            private double m_G;
            private double m_Hue;
            private double m_LAB_a;

            //m_a_red_green_coordinate
            private double m_LAB_b;

            private double m_LAB_L;

            //m_L_Indicateds_Lightness
            //m_b_yellow_blue_Coordinate
            private double m_R;

            private double[] m_Spectral;
            private double m_Trsistimuls_X;
            private double m_Trsistimuls_Y_Correction;
            private double m_Trsistimuls_Y_Luminance;
            private double m_Trsistimuls_Y_Max;
            private double m_Trsistimuls_Y_Min;
            private double m_Trsistimuls_Y_Prime;
            private double m_Trsistimuls_Z;

            #endregion Fields

            #region Constructors

            public DetectorDataContents()
            {
                this.m_Trsistimuls_X = 0.0;
                this.m_Trsistimuls_Y_Luminance = 0.0;
                this.m_Trsistimuls_Y_Max = 0.0;
                this.m_Trsistimuls_Y_Min = 0.0;
                this.m_Trsistimuls_Y_Correction = 0.0;
                this.m_Trsistimuls_Y_Prime = 0.0;
                this.m_Trsistimuls_Z = 0.0;
                this.m_CIE_x = 0.0;
                this.m_CIE_y = 0.0;
                this.m_CIE_x_Correction = 0.0;
                this.m_CIE_y_Correction = 0.0;
                this.m_CIE_u_ = 0.0;
                this.m_CIE_v_ = 0.0;
                this.m_CCT = 0.0;
                this.m_Deviation = 0.0;
                this.m_Dominant_Wavelength = 0.0;
                this.m_ExcitationPurityValue = 0.0;
                this.m_LAB_L = 0.0;
                this.m_LAB_a = 0.0;
                this.m_LAB_b = 0.0;
                this.m_Choroma = 0.0;
                this.m_Hue = 0.0;
                this.m_R = 0.0;
                this.m_G = 0.0;
                this.m_B = 0.0;
                this.m_Spectral = new double[401];
                this.m_ColSpectral = new double[401];
                this.m_Calculate = new SpectralCalcaulte();
                this.m_CorrectionCalculate = new SpectralCalcaulte();
            }

            #endregion Constructors

            #region Properties

            public double B
            {
                get { return this.m_B; }
                set { this.m_B = value; }
            }

            public SpectralCalcaulte Calculate
            {
                get { return this.m_Calculate; }
                set { this.m_Calculate = value; }
            }

            public double CCT
            {
                get { return this.m_CCT; }
                set { this.m_CCT = value; }
            }

            public double Choroma
            {
                get { return this.m_Choroma; }
                set { this.m_Choroma = value; }
            }

            public double CIE_u_
            {
                get { return this.m_CIE_u_; }
                set { this.m_CIE_u_ = value; }
            }

            public double CIE_v_
            {
                get { return this.m_CIE_v_; }
                set { this.m_CIE_v_ = value; }
            }

            public double CIE_x
            {
                get { return this.m_CIE_x; }
                set { this.m_CIE_x = value; }
            }

            public double CIE_x_Correction
            {
                get { return this.m_CIE_x_Correction; }
                set { this.m_CIE_x_Correction = value; }
            }

            public double CIE_y
            {
                get { return this.m_CIE_y; }
                set { this.m_CIE_y = value; }
            }

            public double CIE_y_Correction
            {
                get { return this.m_CIE_y_Correction; }
                set { this.m_CIE_y_Correction = value; }
            }

            public double[] ColSpectral
            {
                get { return this.m_ColSpectral; }
                set { this.m_ColSpectral = value; }
            }

            public SpectralCalcaulte CorrectionCalculate
            {
                get { return this.m_CorrectionCalculate; }
                set { this.m_CorrectionCalculate = value; }
            }

            public double Deviation
            {
                get { return this.m_Deviation; }
                set { this.m_Deviation = value; }
            }

            public double Dominant_Wavelength
            {
                get { return this.m_Dominant_Wavelength; }
                set { this.m_Dominant_Wavelength = value; }
            }

            public double ExcitationPurityValue
            {
                get { return this.m_ExcitationPurityValue; }
                set { this.m_ExcitationPurityValue = value; }
            }

            public double G
            {
                get { return this.m_G; }
                set { this.m_G = value; }
            }

            public double Hue
            {
                get { return this.m_Hue; }
                set { this.m_Hue = value; }
            }

            public double LAB_a // a_red_green_coordinate
            {
                get { return this.m_LAB_a; }
                set { this.m_LAB_a = value; }
            }

            public double LAB_b // b_yellow_blue_Coordinate
            {
                get { return this.m_LAB_b; }
                set { this.m_LAB_b = value; }
            }

            public double LAB_L//L_Indicateds_Lightness
            {
                get { return this.m_LAB_L; }
                set { this.m_LAB_L = value; }
            }

            public double R
            {
                get { return this.m_R; }
                set { this.m_R = value; }
            }

            public double[] Spectral
            {
                get { return this.m_Spectral; }
                set { this.m_Spectral = value; }
            }

            public double Trsistimuls_X
            {
                get { return this.m_Trsistimuls_X; }
                set { this.m_Trsistimuls_X = value; }
            }

            public double Trsistimuls_Y_Correction
            {
                get { return this.m_Trsistimuls_Y_Correction; }
                set { this.m_Trsistimuls_Y_Correction = value; }
            }

            public double Trsistimuls_Y_Luminance
            {
                get { return this.m_Trsistimuls_Y_Luminance; }
                set { this.m_Trsistimuls_Y_Luminance = value; }
            }

            public double Trsistimuls_Y_Max
            {
                get { return this.m_Trsistimuls_Y_Max; }
                set { this.m_Trsistimuls_Y_Max = value; }
            }

            public double Trsistimuls_Y_Min
            {
                get { return this.m_Trsistimuls_Y_Min; }
                set { this.m_Trsistimuls_Y_Min = value; }
            }

            public double Trsistimuls_Y_Prime
            {
                get { return this.m_Trsistimuls_Y_Prime; }
                set { this.m_Trsistimuls_Y_Prime = value; }
            }

            public double Trsistimuls_Z
            {
                get { return this.m_Trsistimuls_Z; }
                set { this.m_Trsistimuls_Z = value; }
            }

            #endregion Properties

            #region Classes

            [Serializable]
            public class SpectralCalcaulte
            {
                #region Fields

                private double m_ELmax;
                private double m_FWHM;
                private double m_Half_Left;
                private double m_Half_Right;
                private double m_Sum_Wavelength;

                #endregion Fields

                #region Constructors

                public SpectralCalcaulte()
                {
                    this.m_FWHM = 0.0;
                    this.m_ELmax = 0.0;
                    this.m_Half_Left = 0.0;
                    this.m_Half_Right = 0.0;
                    this.m_Sum_Wavelength = 0.0;
                }

                #endregion Constructors

                #region Properties

                public double ELmax
                {
                    get { return this.m_ELmax; }
                    set { this.m_ELmax = value; }
                }

                public double FWHM
                {
                    get { return this.m_FWHM; }
                    set { this.m_FWHM = value; }
                }

                public double Half_Left
                {
                    get { return this.m_Half_Left; }
                    set { this.m_Half_Left = value; }
                }

                public double Half_Right
                {
                    get { return this.m_Half_Right; }
                    set { this.m_Half_Right = value; }
                }

                public double Sum_Wavelength
                {
                    get { return this.m_Sum_Wavelength; }
                    set { this.m_Sum_Wavelength = value; }
                }

                #endregion Properties
            }

            #endregion Classes
        }

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

        public class SpotStatisticsData
        {
            #region Properties

            public int SpotNo { get; set; }
            public double CIE_U { get; set; }
            public double CIE_V { get; set; }
            public double CIE_X { get; set; }
            public double CIE_Y { get; set; }
            public double Dominant_Wavelength { get; set; }
            public double G { get; set; }
            public double R { get; set; }
            public double B { get; set; }
            public double LUV { get; set; }
            public List<double> Spectral { get; set; }
            public double Trsistimuls_X { get; set; }
            public double Trsistimuls_Y_Luminance { get; set; }
            public double Trsistimuls_Z { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}