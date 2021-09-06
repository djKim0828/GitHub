using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EmWorks.DevDrv.TopconSR5000.EmTopconSR5000;

namespace EmWorks.App.Sample.View.Dialog
{
    public partial class AutoSpotResultDialog : Form
    {

        Topcon_SR5000 _topcon_SR5000;
        private List<SpotStatisticsData> _resultDataList;
        private Bitmap _initImage;

        public AutoSpotResultDialog(Topcon_SR5000 sr5000, Bitmap image)
        {
            InitializeComponent();

            _topcon_SR5000 = sr5000;
            _initImage = image;

            pBMeasurePseudo.Image = image;

            initConttrol();

            

        }

        private void initConttrol()
        {
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.MultiSelect = false;
            dgvData.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvData.CellClick += DgvData_CellClick;

            this.pnlXY.Controls.Add(_xy);
            this.pnlUV.Controls.Add(_uv);
        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectIndex = dgvData.SelectedRows[0].Index;

            ShowWaveGraph(selectIndex);

            ShowXYPoint(selectIndex);

            ShowUVPoint(selectIndex);
        }

        private void btnSearchROI_Click(object sender, EventArgs e)
        {
            SetAutoSpotProferty();

            pBMeasurePseudo.Image = _initImage;

            ProcessingBox prBox = new ProcessingBox("Searching...");

            bool result = false;

            Task task = Task.Run(() => result = _topcon_SR5000.SetySR5000ROISearch(true, 120000));
            task.GetAwaiter().OnCompleted(() =>
            {
                prBox.Close();
            });

            prBox.ShowDialog();

            if (result == false)
            {
                MessageBox.Show("Search ROI Failed");
                //WriteLog("Search ROI failed");
            }

            object getObject = _topcon_SR5000.xSR5000ROISearchResult.Is;
            Type temp = getObject.GetType();

            _resultDataList = (List<SpotStatisticsData>)getObject;
            //WriteLog("start Measurement complete");

            DrawROI();

            ShowData();
        }

        private void SetAutoSpotProferty()
        {
            // 프로퍼티 설정
            _topcon_SR5000.ySR5000AutoSpotShapeType = 0;
            _topcon_SR5000.ySR5000AutoSpotWidth = Convert.ToDouble(txt_roi_width.Text);
            _topcon_SR5000.ySR5000AutoSpotHeight = Convert.ToDouble(txt_roi_height.Text);
            _topcon_SR5000.ySR5000AutoSpotMin = Convert.ToInt32(txt_roi_min.Text);
            _topcon_SR5000.ySR5000AutoSpotMax = Convert.ToInt32(txt_roi_max.Text);
            _topcon_SR5000.ySR5000AutoSpotYTh = Convert.ToInt32(txt_y_threshold.Text);
        }

        public bool ShowWaveGraph(int spotIndex)
        {
            try
            {
                //Chart 그리기
                string seriesName = "srsWave";

                int waveCount = _resultDataList[spotIndex].Spectral.Count;

                chtWave.Series[seriesName].BorderWidth = 1;
                chtWave.Series[seriesName].Color = Color.Red;

                //Todo : 레시피 설정을 읽을 것인가?
                chtWave.ChartAreas["ChartArea"].AxisX.Minimum = 380; // _topcon_SR5000._device._Recipe.measurement.measurement_wavelength_min;
                chtWave.ChartAreas["ChartArea"].AxisX.Maximum = 780; // _topcon_SR5000._device._Recipe.measurement.measurement_wavelength_max;

                chtWave.ChartAreas["ChartArea"].AxisY.Minimum = 0;
                chtWave.ChartAreas["ChartArea"].AxisY.Maximum = 0.012;

                chtWave.Series[seriesName].Points.Clear();

                for (int x = 0; x < waveCount; x++)
                {
                    double waveValue = _resultDataList[spotIndex].Spectral[x];

                    if (waveValue > 0.012)
                    {
                        waveValue = 0;
                    }
                    
                    chtWave.Series[seriesName].Points.AddXY((double)x + 380, waveValue);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        public void ShowData()
        {
            if (_resultDataList.Count < 1)
            {
                return;
            } // else                        

            dgvData.DataSource = _resultDataList;

            //WriteLog("Complete Show Spot Data");


            ShowWaveGraph(0);
            ShowXYPoint(0);
            ShowUVPoint(0);
        }
        private ChartXY _xy = new ChartXY();
        private ChartUV _uv = new ChartUV();

        private void ShowXYPoint(int selectIndex)
        {
            _xy.DrawPoint(_resultDataList[selectIndex].CIE_X, _resultDataList[selectIndex].CIE_Y);
        }

        private void ShowUVPoint(int selectIndex)
        {
            _uv.DrawPoint(_resultDataList[selectIndex].CIE_U, _resultDataList[selectIndex].CIE_V);
        }

        public void DrawROI()
        {
            if (_resultDataList.Count < 1)
            {
                return;
            } // else            

            //WriteLog("Search spot count = " + _spotDataList.spot_data.Count.ToString());

            Graphics gfx = Graphics.FromImage(pBMeasurePseudo.Image);
            foreach (SpotStatisticsData sd in _resultDataList)
            {
                Pen pen = new Pen(Color.FromArgb(255, 255, 255, 0), 1);

                double op = 0.0064; // Todo : Optical Resolution ?
                

                int spotWidth = (int)(Convert.ToDouble(txt_roi_width.Text) / op);
                int spotHeigth = (int)(Convert.ToDouble(txt_roi_height.Text) / op);

                Rectangle rectTemp = new Rectangle(sd.LocationX - spotWidth / 2,
                                   sd.LocationY - spotHeigth / 2,
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

            //WriteLog("Complete Draw ROI");
        }
    }
}
