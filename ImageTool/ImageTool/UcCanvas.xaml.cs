using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTool
{
    public partial class UcCanvas : UserControl
    {
        #region Fields

        private double _initScaleValue = 0;
        private System.Windows.Point _lastPoint;
        private ImageBrush _LoadImagebrush;
        private System.Windows.Point _originPosition;
        private double _scale = 1.1;

        #endregion Fields

        #region Constructors

        // 검사 가능한 Row 개수 : 현재 2열
        public UcCanvas()
        {
            InitializeComponent();

            Initialize();
        }

        #endregion Constructors

        #region Methods

        public bool LoadImage(string imagePath)
        {
            try
            {
                _LoadImagebrush = new ImageBrush();
                _LoadImagebrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                _LoadImagebrush.Stretch = Stretch.None;

                this.cvsImage.Background = _LoadImagebrush;

                ZoomFit();

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void GrdMain_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(grdMain);

            // Move FOV
            Panning(p);
        }

        private void GrdMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdCanvas.IsMouseCaptured)
            {
                return;
            } // else

            _originPosition.X = grdCanvas.RenderTransform.Value.OffsetX;
            _originPosition.Y = grdCanvas.RenderTransform.Value.OffsetY;

            _lastPoint = e.GetPosition(grdMain);

            grdCanvas.CaptureMouse();
        }

        private void GrdMain_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grdCanvas.ReleaseMouseCapture();
        }

        private void GrdMain_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ZoomFit();
        }

        private void GrdMain_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(grdCanvas);

            ZoomInOut(p, e.Delta);
        }

        private void InitControl()
        {
        }

        private void Initialize()
        {
            InitControl();

            RegistEvents();
        }

        private void Panning(Point p)
        {
            if (grdCanvas.IsMouseCaptured == false)
            {
                return;
            } // else

            Matrix m = grdCanvas.RenderTransform.Value;
            m.OffsetX = _originPosition.X + (p.X - _lastPoint.X);
            m.OffsetY = _originPosition.Y + (p.Y - _lastPoint.Y);

            grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.grdMain.PreviewMouseWheel += GrdMain_PreviewMouseWheel;
            this.grdMain.PreviewMouseRightButtonUp += GrdMain_PreviewMouseRightButtonUp;
            this.grdMain.MouseLeftButtonDown += GrdMain_PreviewMouseLeftButtonDown;
            this.grdMain.MouseLeftButtonUp += GrdMain_PreviewMouseLeftButtonUp;
            this.grdMain.MouseMove += GrdMain_MouseMove;

            return 1;
        }


        private void ZoomFit()
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double hZoom = height / _LoadImagebrush.ImageSource.Height; // 세로를 기준으로 
            double zoom = hZoom;            

            // 가로 가운데값 구하기
            double initOffsetX = 0;
            double initOffsetY = 0;

            // 실제 크기 절반 - 줄어들 컨트롤의 절반
            initOffsetX = width / 2 - (grdCanvas.ActualWidth * zoom / 2);
            initOffsetY = height / 2 - (grdCanvas.ActualHeight * zoom / 2);

            //Canvas
            Matrix m = grdCanvas.RenderTransform.Value;
            m.M11 = zoom;
            m.M22 = zoom;
            m.OffsetX = initOffsetX;
            m.OffsetY = initOffsetY;

            _initScaleValue = m.M11;

            grdCanvas.RenderTransform = new MatrixTransform(m);
            grdCanvas.Visibility = Visibility.Visible;
        }

        public void Rotate(bool isCW)
        {
            Matrix m = cvsImage.RenderTransform.Value;

            if (isCW == true)
            {
                m.RotateAt(90, cvsImage.ActualWidth / 2, cvsImage.ActualHeight / 2);
            }
            else
            {
                m.RotateAt(-90, cvsImage.ActualWidth / 2, cvsImage.ActualHeight / 2);
            }

            cvsImage.RenderTransform = new MatrixTransform(m);

        }

        private void ZoomInOut(Point p, int delta)
        {
            Matrix m = grdCanvas.RenderTransform.Value;

            if (delta > 0)
            {
                m.ScaleAtPrepend(_scale, _scale, p.X, p.Y);
            }
            else
            {
                m.ScaleAtPrepend(1 / _scale, 1 / _scale, p.X, p.Y);
            }

            if (m.M11 <= _initScaleValue)
            {
                // Fit 보다 작게 축소는 리턴
                return;
            } // else

            grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        #endregion Methods

    }
}