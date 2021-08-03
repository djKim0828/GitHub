using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageTool
{
    public class LengthTool : ToolBase
    {
        #region Fields

        public LengthObject _LengthObject;

        #endregion Fields

        #region Classes

        public class LengthObject
        {
            #region Fields

            public System.Windows.Controls.Label label;
            public Line line;

            #endregion Fields
        }

        #endregion Classes

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
            if (_window.grdAnno.IsMouseCaptured == false)
            {
                return;
            } // else

            Point p = e.MouseDevice.GetPosition(_window.grdAnno);

            _LengthObject.line.X2 = p.X;
            _LengthObject.line.Y2 = p.Y;
        }

        public override void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_window.grdAnno.IsMouseCaptured)
            {
                return;
            } // else

            _LengthObject = new LengthObject();

            Point startPoint = e.GetPosition(_window.grdAnno);

            _LengthObject.line = CreateLine(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, Brushes.Green, 1, null);

            _window.grdAnno.Children.Add(_LengthObject.line);

            _window.grdAnno.CaptureMouse();
        }

        public override void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 길이 구하기
            double distance = GetLength();

            // 라벨 출력
            insertLabel(distance);

            if (distance == 0)
            {
                _window.grdAnno.Children.Remove(_LengthObject.line);
                _window.grdAnno.Children.Remove(_LengthObject.label);
            }

            _window.grdAnno.ReleaseMouseCapture();
        }

        private double GetLength()
        {
            double deltaX = this._LengthObject.line.X2 - this._LengthObject.line.X1;
            double deltaY = this._LengthObject.line.Y2 - this._LengthObject.line.Y1;

            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;
        }

        private void insertLabel(double distance)
        {
            _LengthObject.label = new System.Windows.Controls.Label();
            double displayValue = Math.Round(distance, 2);
            _LengthObject.label.Content = displayValue.ToString() + "px";
            _LengthObject.label.Style = (Style)Application.Current.Resources["lblToolResult"];
            _LengthObject.label.FontSize = 8;
            _LengthObject.label.Margin = new System.Windows.Thickness(_LengthObject.line.X2, _LengthObject.line.Y2, 0, 0);

            _window.grdAnno.Children.Add(_LengthObject.label);
        }

        #endregion Methods
    }
}