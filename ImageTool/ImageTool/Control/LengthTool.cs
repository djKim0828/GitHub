using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageTool
{
    public class LengthTool : ToolBase
    {
        #region Fields

        private Line _line;

        #endregion Fields

        #region Constructors

        public LengthTool(object window) : base(window)
        {
            _window = (UcCanvas)window;
        }

        #endregion Constructors

        #region Methods

        public Line CreateLine(double x1, double y1, double x2, double y2, Brush brush, double thickness, DoubleCollection dashStyle)
        {
            Line line = new Line();

            //첫번째 좌표 설정
            line.X1 = x1;
            line.Y1 = y1;

            //두번째 좌표 설정
            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = brush;//선색 지정
            line.StrokeThickness = thickness;//선 두께 지정
            line.StrokeDashArray = dashStyle;//점선 설정 - new DoubleCollection { 점 길이, 점 간격}

            return line;
        }

        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeCursor(Cursors.Cross);
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeCursor(Cursors.Arrow);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (_window.grdCanvas.IsMouseCaptured == false)
            {
                return;
            } // else

            Point p = e.MouseDevice.GetPosition(_window.grdCanvas);

            _line.X2 = p.X;
            _line.Y2 = p.Y;
        }

        public override void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_window.grdCanvas.IsMouseCaptured)
            {
                return;
            } // else

            Point startPoint = e.GetPosition(_window.grdCanvas);

            _line = CreateLine(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, Brushes.Green, 1, null);

            _window.grdCanvas.Children.Add(_line);

            _window.grdCanvas.CaptureMouse();
        }

        public override void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _window.grdCanvas.ReleaseMouseCapture();
        }

        #endregion Methods
    }
}