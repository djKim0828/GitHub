using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmWorks.View
{
    /// <summary>
    /// Interaction logic for ChartBar.xaml
    /// </summary>
    public partial class ChartWave : UserControl
    {
        #region Fields

        private double _thisHZoom;
        private double _thisWZoom;
        private System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        private System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

        #endregion Fields

        #region Constructors

        public ChartWave()
        {
            InitializeComponent();

            InitChart();

            this.SizeChanged += ChartWave_SizeChanged;
        }

        #endregion Constructors

        #region Methods

        public bool DrawGraph(List<double> waveValue, int min, int max)
        {
            try
            {
                int waveCount = max - min; // range 안의 값만 유효

                //Chart 그리기
                string seriesName = "srsWave";

                chtWave.Series[seriesName].BorderWidth = 1;
                chtWave.Series[seriesName].Color = System.Drawing.Color.Red;

                chtWave.ChartAreas["ChartArea"].AxisX.Minimum = min;
                chtWave.ChartAreas["ChartArea"].AxisX.Maximum = max;

                //Y축 최대값 구하기
                double yMaxValue = 0;
                for (int i = 0; i < waveCount; i++)
                {
                    double tempValue = waveValue[i];

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
                    double value = Math.Round(waveValue[x], 3);

                    chtWave.Series[seriesName].Points.AddXY((double)x + min, value);
                }

                ShowGraph(true);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public void ShowGraph(bool isVisible)
        {
            if (isVisible == true)
            {
                this.wfhMain.Visibility = Visibility.Visible;
                this.lblBackground.Visibility = Visibility.Hidden;
            }
            else
            {
                this.wfhMain.Visibility = Visibility.Hidden;
                this.lblBackground.Visibility = Visibility.Visible;
            }
        }

        private void ChartWave_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            _thisWZoom = width / this.grdMain.Width;
            _thisHZoom = height / this.grdMain.Height;

            var scaler = grdMain.LayoutTransform as ScaleTransform;
            scaler = new ScaleTransform(_thisWZoom, _thisHZoom);
            grdMain.LayoutTransform = scaler;
        }

        private void InitChart()
        {
            //
            // chtWave
            //
            this.chtWave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chtWave.BorderlineWidth = 2;
            this.chtWave.BorderSkin.BackColor = System.Drawing.Color.LightGray;
            this.chtWave.BorderSkin.BorderColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.LightGray;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackImageTransparentColor = System.Drawing.Color.LightGray;
            chartArea1.BackSecondaryColor = System.Drawing.Color.LightGray;
            chartArea1.BorderColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea";
            chartArea1.ShadowColor = System.Drawing.Color.LightGray;
            this.chtWave.ChartAreas.Add(chartArea1);
            series1.BorderColor = System.Drawing.Color.LightGray;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.LabelForeColor = System.Drawing.Color.LightGray;
            series1.Name = "srsWave";
            this.chtWave.Series.Add(series1);
            this.chtWave.Size = new System.Drawing.Size(450, 451);
            this.chtWave.TabIndex = 1;
            this.chtWave.Text = "chartWave";

            ShowGraph(false);
        }

        #endregion Methods
    }
}