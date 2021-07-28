using EmWorks.Lib.Common;
using HsApiNet;
using LiveView.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveView
{
    public partial class MainForm : Form
    {
        #region Fields

        private String _ConfigFilePath = Application.StartupPath + @"\config.json";
        private String _DataFilePath = Application.StartupPath + @"\Data\result.hsm";

        private int _measurementDataId = 0;

        private SpotDataList _spotDataList = new SpotDataList();

        private SpotDef _spotDef = new SpotDef();

        private ChartXY _xy = new ChartXY();
        private ChartUV _uv = new ChartUV();

        #endregion Fields

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            initInstance();

            initControl();
        }

        private void initControl()
        {
            this.pnlXY.Controls.Add(_xy);
            this.pnlUV.Controls.Add(_uv);

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.MultiSelect = false;
            dgvData.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvData.CellClick += DgvData_CellClick;
        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectIndex = dgvData.SelectedRows[0].Index;

            ShowWaveGraph(selectIndex);

            ShowXYPoint(selectIndex);

            ShowUVPoint(selectIndex);
        }

        private void ShowXYPoint(int selectIndex)
        {
            _xy.DrawPoint(_SpotStatisticsData[selectIndex].CIE_X, _SpotStatisticsData[selectIndex].CIE_Y);
        }
        private void ShowUVPoint(int selectIndex)
        {
            _uv.DrawPoint(_SpotStatisticsData[selectIndex].CIE_U, _SpotStatisticsData[selectIndex].CIE_V);
        }

        #endregion Constructors

        #region Methods

        private int mmToPixel(double mm, float dpi)
        {
            return (int)Math.Round((mm / 25.4) * dpi);
        }

        public void DrawROI()
        {
            if (_spotDataList.spot_data.Count < 1)
            {
                return;
            } // else            

            WriteLog("Search spot count = " + _spotDataList.spot_data.Count.ToString());

            Graphics gfx = Graphics.FromImage(pBMeasurePseudo.Image);
            foreach (SpotData sd in _spotDataList.spot_data)
            {
                Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);

                double op = 0.0064; // Todo : Optical Resolution ?
                int spotWidth = (int)(_spotDef.spot[0].width / op);
                int spotHeigth = (int)(_spotDef.spot[0].height / op);
                
                Rectangle rectTemp = new Rectangle(sd.location.x - spotWidth / 2,
                                   sd.location.y - spotHeigth / 2,
                                   spotWidth,
                                   spotHeigth);

                if (rd_Square.Checked == true)
                {
                    gfx.DrawRectangle(pen, rectTemp);
                }
                else
                {
                    gfx.DrawEllipse(pen, rectTemp);
                }
            }

            pBMeasurePseudo.Invalidate();
            pBMeasurePseudo.Refresh();

            WriteLog("Complete Draw ROI");
        }

        public List<TopconSR5000.SpotStatisticsData> _SpotStatisticsData;

        public void ShowData()
        {
            if (_spotDataList.spot_data.Count < 1)
            {
                return;
            } // else            

            _SpotStatisticsData = new List<TopconSR5000.SpotStatisticsData>();

            if (Global._TopconSR5000.ParsingSpotData(_spotDataList.spot_data, out _SpotStatisticsData) == false)
            {
                WriteLog("Failed Show Spot Data");
                return;
            }

            dgvData.DataSource = _SpotStatisticsData;

            WriteLog("Complete Show Spot Data");


            ShowWaveGraph(0);
            ShowXYPoint(0);
            ShowUVPoint(0);
        }

        public bool ShowWaveGraph(int spotIndex)
        {
            try
            {
                //Chart 그리기
                string seriesName = "srsWave";

                int waveCount = _SpotStatisticsData[spotIndex].Spectral.Count;

                chtWave.Series[seriesName].BorderWidth = 1;
                chtWave.Series[seriesName].Color = Color.Red;

                chtWave.ChartAreas["ChartArea"].AxisX.Minimum = Global._TopconSR5000._Recipe.measurement.measurement_wavelength_min;
                chtWave.ChartAreas["ChartArea"].AxisX.Maximum = Global._TopconSR5000._Recipe.measurement.measurement_wavelength_max;

                //Y축 최대값 구하기
                double yMaxValue = 0;
                for (int i=0;i<waveCount;i++)
                {
                    double tempValue = _SpotStatisticsData[spotIndex].Spectral[i];

                    if (yMaxValue < tempValue)
                    {
                        yMaxValue = tempValue;
                    } // else
                }

                chtWave.ChartAreas["ChartArea"].AxisY.Minimum = 0;
                chtWave.ChartAreas["ChartArea"].AxisY.Maximum = Math.Round(yMaxValue * 1.1, 3); // 1.1배 크게 그린다.

                chtWave.Series[seriesName].Points.Clear();

                for (int x = 0; x < waveCount; x++)
                {
                    double waveValue = Math.Round(_SpotStatisticsData[spotIndex].Spectral[x], 3);

                    //if (waveValue > 0.012)
                    //{
                    //    waveValue = 0;
                    //}
                    //}

                    //if (x+380 == 380)
                    //{
                    //    int bbb = 0;
                    //}

                    chtWave.Series[seriesName].Points.AddXY((double)x +380, waveValue);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
        }

        public void LoadMeasurementData()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                string filePath = OpenFilePath();
            
                if (filePath == string.Empty)
                {
                    return;
                }

                int errorIndex = 0;
                errorIndex = Global._TopconSR5000.LoadMeasurementData(filePath, ref _measurementDataId);

                if (errorIndex < 0)
                {
                    WriteLog("Failed Load MeasurementData" + Global._TopconSR5000.ErrorCodeToString());
                }
                else
                {
                    WriteLog("Complete Load MeasurementData");
                }

                // Get Image
                if (LoadPseudoImage(_measurementDataId) == false)
                {
                    WriteLog("Failed Load PseudoImage" + Global._TopconSR5000.ErrorCodeToString());
                }//else

                if (LoadRGBImage(_measurementDataId) == false)
                {
                    WriteLog("Failed Load RGBImage" + Global._TopconSR5000.ErrorCodeToString());
                }
            }));
        }

        public string OpenFilePath()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public string SaveFilePath(string fileType)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.Filter = fileType + " files (*." + fileType + ")|*." + fileType + "|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public void Search()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (SearchRoi() == false)
                {
                    MessageBox.Show("Failed ROI Search");
                    return;
                }

                DrawROI();

                ShowData();

            }));
        }

        public bool SearchRoi()
        {
            _spotDef.type = SpotDataType.RANDOM;
            _spotDef.rdm_spot_setting.measurement_roi = new Rect32n(0, 0,
                                        pBMeasurePseudo.Image.Width,
                                        pBMeasurePseudo.Image.Height);
            _spotDef.rdm_spot_setting.binning_type = BinningType.TYPE1;
            _spotDef.rdm_spot_setting.trimming = new Rect32n(0, 0, 0, 0);
            _spotDef.rdm_spot_setting.init_shape = AreaShapeType.CIRCLE;
            _spotDef.rdm_spot_setting.init_size = new Size64f(8, 8);
            _spotDef.rdm_spot_setting.renumbering = true;
            _spotDef.rdm_spot_setting.apply_threshold = false;

            _spotDef.rdm_spot_setting.auto_spot_setting[0].enable = true;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].mode = 0;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].option = 0;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].spot_shape = rd_Square.Checked ? AreaShapeType.SQUARE : AreaShapeType.CIRCLE;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].spot_size = new Size64f(Convert.ToDouble(txt_roi_width.Text), Convert.ToDouble(txt_roi_height.Text));
            _spotDef.rdm_spot_setting.auto_spot_setting[0].area_min = Convert.ToInt32(txt_roi_min.Text);
            _spotDef.rdm_spot_setting.auto_spot_setting[0].area_max = Convert.ToInt32(txt_roi_max.Text);
            _spotDef.rdm_spot_setting.auto_spot_setting[0].missing_spot_enable = false;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].outside_spot_enable = false;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].pitch = new Size32n(0, 0);
            _spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].enable = true;

            _spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].data_type = DataType.Y;
            _spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].threshold = Convert.ToSingle(txt_y_threshold.Text);
            _spotDef.rdm_spot_setting.auto_spot_setting[0].image_setting[0].threshold_type = AutoSpotThresholdType.LOWER_BOUND_VALUE;

            _spotDataList = new SpotDataList();
            if (Global._TopconSR5000.SearchROI(_measurementDataId, _spotDef, out _spotDataList) == true)
            {
                WriteLog("Complete Search ROI");

                return true;
            }
            else
            {
                WriteLog("Failed Search ROI");

                return false;
            }
        }

        public void WriteLog(string msg)
        {
            Logger.Debug(msg);

            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + msg + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        private void _topconSR5000_LoggerMessage(object sendor, TopconSR5000.EventLoggerMessageArgs e)
        {
            WriteLog(e.Message);
        }

        private void btnDestroyMes_Click(object sender, EventArgs e)
        {
            Global._TopconSR5000.DestroyMeasurementData(_measurementDataId);
        }

        private void btnGetRGB_Click(object sender, EventArgs e)
        {
            pBMeasurePseudo.Image = null;
            if (LoadPseudoImage(_measurementDataId) == false)
            {
                WriteLog("Failed Pseudo get");
            }
            else
            {
                WriteLog("Complete Pseudo get");
            }

            pBMeasureRGB.Image = null;
            if (LoadRGBImage(_measurementDataId) == false)
            {
                WriteLog("Failed Rgb get");
            }
            else
            {
                WriteLog("Complete Rgb get");
            }
        }

        private void btnGetSpect_Click(object sender, EventArgs e)
        {
            if (LoadSpectralImage(_measurementDataId) == false)
            {
                WriteLog("Failed Spectral get");
            }
            else
            {
                WriteLog("Complete Spectral get");
            }
        }

        private void btnLoadMesFile_Click(object sender, EventArgs e)
        {
            ProcessingBox pb = new ProcessingBox("Measurement data Loading...");

            Task task = Task.Run(() => LoadMeasurementData());
            task.GetAwaiter().OnCompleted(() =>
            {
                pb.Close();
            });

            pb.ShowDialog();

        }

        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            Global._Config._RecipeFilePath = OpenFilePath();
            Json.Save(_ConfigFilePath, Global._Config);

            LoadRecipe();
        }

        private void btnMes2_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;

            int errorIndex = Global._TopconSR5000.CreatMeasurementData(Global._TopconSR5000._Recipe,
                ref _measurementDataId);

            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            errorIndex = Global._TopconSR5000.StartMeasurement(_measurementDataId);

            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            // Get Image
            if (LoadSpectralImage(_measurementDataId) == false)
            {
                goto Error_Status;
            }

            // Save
            errorIndex = Global._TopconSR5000.SaveMeasurementData(_DataFilePath, _measurementDataId);

            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            //nError = m_SR5000.GetMeasurementData(measurementDataId, ref resultData);
            //if (nError < 0) goto Error_Status;

            DateTime endTime = DateTime.Now;

            TimeSpan ts = startTime - endTime;

            WriteLog("End Measurement - " + ts.Minutes + "m" + ts.Seconds + "." + ts.TotalMilliseconds);

            return;

            // Error시 처리
            Error_Status:
            {
                WriteLog("Error " + Global._TopconSR5000.ErrorCodeToString());

                errorIndex = Global._TopconSR5000.DestroyMeasurementData(_measurementDataId);
                if (errorIndex < 0)
                {
                    WriteLog("Error [DestroyMeasurementData] - " + Global._TopconSR5000.ErrorCodeToString());
                }

                return;
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            string filePath = SaveFilePath("bmp");

            pBMeasurePseudo.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void btnSaveMes_Click(object sender, EventArgs e)
        {
            string filePath = SaveFilePath("hsm");
            // Save
            int errorIndex = Global._TopconSR5000.SaveMeasurementData(filePath, _measurementDataId);

            if (errorIndex < 0)
            {
                WriteLog("Failed Save MeasurementData" + Global._TopconSR5000.ErrorCodeToString());
            }
            else
            {
                WriteLog("Complete Save MeasurementData");
            }
        }

        private void btnSearchClear_Click(object sender, EventArgs e)
        {
            LoadPseudoImage(_measurementDataId);
        }

        private void btnSearchROI_Click(object sender, EventArgs e)
        {
            if (pBMeasurePseudo.Image == null)
            {
                WriteLog("Please, Check load image");
                return;
            }

            LoadPseudoImage(_measurementDataId);  // Clear

            ProcessingBox pb = new ProcessingBox("ROI Searching...");

            Task task = Task.Run(() => Search());
            task.GetAwaiter().OnCompleted(() =>
            {
                pb.Close();
            });

            pb.ShowDialog();            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Stop the timer before changing parameters
            timerUpdate.Enabled = false;

            Global._TopconSR5000.StartLiveView();

            // Start time to update image on form
            timerUpdate.Enabled = true;
        }

        private void btnStartMeasurement_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;

            int errorIndex = Global._TopconSR5000.CreatMeasurementData(Global._TopconSR5000._Recipe,
                ref _measurementDataId);

            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            // Start
            errorIndex = Global._TopconSR5000.StartMeasurement(_measurementDataId);
            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            // Get Image
            if (LoadPseudoImage(_measurementDataId) == false)
            {
                goto Error_Status;
            }//else

            if (LoadRGBImage(_measurementDataId) == false)
            {
                goto Error_Status;
            }

            // Save
            errorIndex = Global._TopconSR5000.SaveMeasurementData(_DataFilePath, _measurementDataId);
            if (errorIndex < 0)
            {
                goto Error_Status;
            } // else

            errorIndex = Global._TopconSR5000.GetMeasurementData(_measurementDataId);
            if (errorIndex < 0)
            {
                goto Error_Status;
            }

            DateTime endTime = DateTime.Now;

            TimeSpan ts = startTime - endTime;

            WriteLog("End Measurement - " + ts.Minutes + "m" + ts.Seconds + "." + ts.TotalMilliseconds);

            return;

            // Error시 처리
            Error_Status:
            {
                WriteLog("Error " + Global._TopconSR5000.ErrorCodeToString());

                errorIndex = Global._TopconSR5000.DestroyMeasurementData(_measurementDataId);
                if (errorIndex < 0)
                {
                    WriteLog("Error [DestroyMeasurementData] - " + Global._TopconSR5000.ErrorCodeToString());
                }

                //Global._TopconSR5000.StopMeasurement();
                return;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Global._TopconSR5000.StopLiveView();

            timerUpdate.Stop();
        }

        private void btnStopMes_Click(object sender, EventArgs e)
        {
            Global._TopconSR5000.StopMeasurement();
        }

        private void initInstance()
        {
            LoadConfig();

            Global._TopconSR5000 = new TopconSR5000();
            Global._TopconSR5000.LoggerMessage += _topconSR5000_LoggerMessage;
        }

        private void LoadConfig()
        {
            Global._Config = Json.Load(_ConfigFilePath);
            if (Global._Config == null)
            {
                Global._Config = new Config();
            }

            if (Global._Config._AutoSpotSelectType == "0")
            {
                rd_Square.Checked = true;
            }
            else
            {
                rd_Circle.Checked = true;
            }

            txt_roi_width.Text = Global._Config._AutoSpotWidth;
            txt_roi_height.Text = Global._Config._AutoSpotHeigth;
            txt_roi_min.Text = Global._Config._AutoSpotMin;
            txt_roi_max.Text = Global._Config._AutoSpotMax;
            txt_y_threshold.Text = Global._Config._AutoSpotYth;
        }

        private bool LoadPseudoImage(int measureId)
        {
            try
            {
                Bitmap captureImage;
                pBMeasurePseudo.Image = null;

                if (Global._TopconSR5000.GetPseudoImage(measureId, out captureImage) == true)
                {
                    pBMeasurePseudo.Image = captureImage;

                    return true;
                }
                else
                {
                    pBMeasurePseudo.Image = null;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void LoadRecipe()
        {
            if (Global._Config._RecipeFilePath == string.Empty)
            {
                Global._Config._RecipeFilePath = OpenFilePath();
                Json.Save(_ConfigFilePath, Global._Config);
            }

            if (Global._Config._RecipeFilePath != string.Empty)
            {
                Global._TopconSR5000.LoadRecipe(Global._Config._RecipeFilePath);
                WriteLog("Success recipe load");
            }
            else
            {
                WriteLog("Cancel recipe load");
            }

            //txt_roi_width.Text = Global._TopconSR5000._Recipe.measurement.processing_rois.data[0].width.ToString();
            //txt_roi_height.Text = Global._TopconSR5000._Recipe.measurement.processing_rois.data[0].height.ToString();
        }

        private bool LoadRGBImage(int measureId)
        {
            try
            {
                Bitmap captureImage;
                pBMeasureRGB.Image = null;

                if (Global._TopconSR5000.GetRGBColorImage(measureId, out captureImage) == true)
                {
                    pBMeasureRGB.Image = captureImage;

                    return true;
                }
                else
                {
                    pBMeasureRGB.Image = null;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private bool LoadSpectralImage(int _measurementDataId)
        {
            try
            {
                Bitmap captureImage;
                pBMeasureRGB.Image = null;
                if (Global._TopconSR5000.GetSpectralImage(_measurementDataId, 400, out captureImage) == true)
                {
                    pBMeasureRGB.Image = captureImage;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global._TopconSR5000.CloseDevice();

            SaveConfig();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RunResize();

            string calibrationPath = "C:\\Program Files\\TopconTechnohouse\\SR-5000\\Calibration";
            
            ProcessingBox pb = new ProcessingBox("Device Opening...");
            bool result = false;

            Task task = Task.Run(() => result = Global._TopconSR5000.OpenDevice(calibrationPath));
            task.GetAwaiter().OnCompleted(() =>
            {
                pb.Close();
            });

            pb.ShowDialog();

            if (result == true)
            {
                tslStatus.BackColor = Color.Green;
            }

            LoadRecipe();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            RunResize();
        }

        private void RunResize()
        {
            //pnlRgb.Top = 10;
            //pnlRgb.Left = 10;            
        }

        private void SaveConfig()
        {
            Global._Config._AutoSpotSelectType = rd_Square.Checked ? "0" : "1";
            Global._Config._AutoSpotWidth = txt_roi_width.Text;
            Global._Config._AutoSpotHeigth = txt_roi_height.Text;
            Global._Config._AutoSpotMin = txt_roi_min.Text;
            Global._Config._AutoSpotMax = txt_roi_max.Text;
            Global._Config._AutoSpotYth = txt_y_threshold.Text;

            Json.Save(_ConfigFilePath, Global._Config);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            Bitmap captureImage;
            if (Global._TopconSR5000.GetLiveImage(out captureImage) == false)
            {
                timerUpdate.Enabled = false;
                return;
            }

            // Show created bitmap on picture box.
            pictureBoxLive.Image = captureImage;
        }

        #endregion Methods
    }
}