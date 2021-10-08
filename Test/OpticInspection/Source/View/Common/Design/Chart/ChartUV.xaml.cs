using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmWorks.View
{
    /// <summary>
    /// Interaction logic for ChartUV.xaml
    /// </summary>
    public partial class ChartUV : UserControl
    {
        #region Fields

        private const int _AsisXMargin = 27;
        private const int _AsisYMargin = 25;

        //UI에 정해진 값들
        private const int _borderThickness = 8;

        private const int _chartXLength = 394;
        private const int _chartYLength = 394;
        private double _thisHZoom;
        private double _thisWZoom;

        // Point
        private double _xValue = 0;

        private double _yValue = 0;

        #endregion Fields

        #region Constructors

        public ChartUV()
        {
            InitializeComponent();

            this.SizeChanged += ChartUV_SizeChanged;
        }

        #endregion Constructors

        #region Methods

        public void DrawPoint(double x, double y)
        {
            if (x == 0 && y == 0)
            {
                this.elPoint.Visibility = Visibility.Hidden;
                return;
            } // else

            _xValue = x;
            _yValue = y;

            // 0점
            double zeroLeft = (_AsisXMargin - _borderThickness / 2) * _thisWZoom;
            double zeroTop = (_AsisYMargin + _chartYLength - _borderThickness / 2) * _thisHZoom;

            int axisCntX = 7;
            int axisCntY = 7;

            double pointX = ((_chartXLength / axisCntX) * x) * _thisWZoom;
            double pointY = ((_chartYLength / axisCntY) * y) * _thisHZoom;

            double pX = zeroLeft + Convert.ToInt32(pointX * 10);
            double pY = zeroTop - Convert.ToInt32(pointY * 10);

            this.elPoint.Margin = new Thickness(pX, pY, 0, 0);
            this.elPoint.Visibility = Visibility.Visible;
        }

        private void ChartUV_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            _thisWZoom = width / this.grdMain.Width;
            _thisHZoom = height / this.grdMain.Height;

            //Image
            Matrix m = imgBack.RenderTransform.Value;
            m.M11 = _thisWZoom;
            m.M22 = _thisHZoom;

            imgBack.RenderTransform = new MatrixTransform(m);
            imgBack.Visibility = Visibility.Visible;

            DrawPoint(_xValue, _yValue);
        }

        #endregion Methods
    }
}