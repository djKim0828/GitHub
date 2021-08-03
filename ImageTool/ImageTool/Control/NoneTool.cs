using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageTool
{
    public class NoneTool : ToolBase
    {
        #region Fields

        private double _initScaleValue = 0;
        private System.Windows.Point _lastPoint;
        private System.Windows.Point _originPosition;
        private double _scale = 1.1;

        #endregion Fields

        #region Constructors

        public NoneTool(object window) : base(window)
        {
            _window = (UcCanvas)window;
        }

        #endregion Constructors

        #region Methods

        public void DeleteAll()
        {
            _window.grdAnno.Children.Clear();
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(_window.grdMain);

            // Move FOV
            Panning(p);
        }

        public override void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_window.grdCanvas.IsMouseCaptured)
            {
                return;
            } // else

            _originPosition.X = _window.grdCanvas.RenderTransform.Value.OffsetX;
            _originPosition.Y = _window.grdCanvas.RenderTransform.Value.OffsetY;

            _lastPoint = e.GetPosition(_window.grdMain);

            _window.grdCanvas.CaptureMouse();
        }

        public override void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _window.grdCanvas.ReleaseMouseCapture();
        }

        public void Rotate(bool isCW)
        {
            Matrix m = _window.grdCanvas.RenderTransform.Value;

            if (isCW == true)
            {
                m.RotateAt(90, _window.grdCanvas.ActualWidth / 2, _window.grdCanvas.ActualHeight / 2);
            }
            else
            {
                m.RotateAt(-90, _window.grdCanvas.ActualWidth / 2, _window.grdCanvas.ActualHeight / 2);
            }

            _window.grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        public void ZoomFit(ImageBrush imageBrush)
        {
            if (imageBrush == null)
            {
                return;
            }

            double width = _window.ActualWidth;
            double height = _window.ActualHeight;
            double hZoom = height / imageBrush.ImageSource.Height; // 세로를 기준으로
            double zoom = hZoom;

            // 가로 가운데값 구하기
            double initOffsetX = 0;
            double initOffsetY = 0;

            // 실제 크기 절반 - 줄어들 컨트롤의 절반
            initOffsetX = width / 2 - (_window.grdCanvas.ActualWidth * zoom / 2);
            initOffsetY = height / 2 - (_window.grdCanvas.ActualHeight * zoom / 2);

            //Canvas
            Matrix m = _window.grdCanvas.RenderTransform.Value;
            m.M11 = zoom;
            m.M22 = zoom;
            m.OffsetX = initOffsetX;
            m.OffsetY = initOffsetY;

            _initScaleValue = m.M11;

            _window.grdCanvas.RenderTransform = new MatrixTransform(m);
            _window.grdCanvas.Visibility = Visibility.Visible;
        }

        public void ZoomInOut(MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(_window.grdCanvas);

            Matrix m = _window.grdCanvas.RenderTransform.Value;

            if (e.Delta > 0)
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

            _window.grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        private void Panning(Point p)
        {
            if (_window.grdCanvas.IsMouseCaptured == false)
            {
                return;
            } // else

            Matrix m = _window.grdCanvas.RenderTransform.Value;
            m.OffsetX = _originPosition.X + (p.X - _lastPoint.X);
            m.OffsetY = _originPosition.Y + (p.Y - _lastPoint.Y);

            _window.grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        #endregion Methods
    }
}