using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageTool
{
    public class AngleTool : ToolBase
    {
        #region Fields

        public AngleObject _AngleObject;
        public int _drawStep = 0;

        #endregion Fields

        #region Constructors

        public AngleTool(object window) : base(window)
        {
            _window = (UcCanvas)window;
        }

        #endregion Constructors

        #region Methods

        public void Cancel()
        {
            if (_AngleObject != null)
            {
                _window.grdAnno.Children.Remove(_AngleObject.line1);
                _window.grdAnno.Children.Remove(_AngleObject.line2);
                _window.grdAnno.Children.Remove(_AngleObject.label);
            } // else

            _window.grdAnno.ReleaseMouseCapture();
        }

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
            Point p = e.MouseDevice.GetPosition(_window.grdAnno);

            if (_drawStep == 1)
            {
                _AngleObject.line1.X2 = p.X;
                _AngleObject.line1.Y2 = p.Y;
            }
            else if (_drawStep == 2)
            {
                _AngleObject.line2.X2 = p.X;
                _AngleObject.line2.Y2 = p.Y;

                double angle = GetAngle();
                _AngleObject.label.Content = angle.ToString() + "º";
            }
        }

        public override void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_window.grdAnno.IsMouseCaptured)
            {
                return;
            } // else

            Point startPoint = e.GetPosition(_window.grdAnno);

            if (_drawStep == 0)
            {
                _AngleObject = new AngleObject();

                _AngleObject.line1 = CreateLine(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, Brushes.Green, 1, null);

                _window.grdAnno.Children.Add(_AngleObject.line1);

                _drawStep++;
            }
            else if (_drawStep == 1)
            {
                _AngleObject.line2 = CreateLine(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, Brushes.Green, 1, null);

                _window.grdAnno.Children.Add(_AngleObject.line2);

                // 길이 구하기
                double angle = GetAngle();

                // 라벨 출력
                insertLabel(angle);

                //if (angle == 0)
                //{
                //    _window.grdAnno.Children.Remove(_AngleObject.line1);
                //    _window.grdAnno.Children.Remove(_AngleObject.line2);
                //    _window.grdAnno.Children.Remove(_AngleObject.label);
                //}

                _drawStep++;
            }
            else if (_drawStep == 2)
            {
                _drawStep = 0;
            }
        }

        public override void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private double GetAngle()
        {
            //double dy = _AngleObject.line2.Y2 - _AngleObject.line2.Y1;
            //double dx = _AngleObject.line2.X2 - _AngleObject.line2.X1;

            //return Math.Atan2(dy, dx) * (180.0 / Math.PI);

            double v1 = (_AngleObject.line2.Y2 - _AngleObject.line1.Y2) /
                (_AngleObject.line2.X2 - _AngleObject.line1.X2);
            double v2 = (_AngleObject.line1.Y1 - _AngleObject.line1.Y2) /
                (_AngleObject.line1.X1 - _AngleObject.line1.X2);

            double result = Math.Atan(v1) - Math.Atan(v2);

            return result * (180.0 / Math.PI);
        }

        private void insertLabel(double angle)
        {
            _AngleObject.label = new System.Windows.Controls.Label();
            double displayValue = Math.Round(angle, 2);
            _AngleObject.label.Content = displayValue.ToString() + "º";
            _AngleObject.label.Style = (Style)Application.Current.Resources["lblToolResult"];
            _AngleObject.label.FontSize = 8;
            _AngleObject.label.Margin = new System.Windows.Thickness(_AngleObject.line1.X2, _AngleObject.line1.Y2, 0, 0);

            _window.grdAnno.Children.Add(_AngleObject.label);
        }

        #endregion Methods

        #region Classes

        public class AngleObject
        {
            #region Fields

            public System.Windows.Controls.Label label;
            public Line line1;
            public Line line2;
            public Line line3;

            #endregion Fields
        }

        #endregion Classes
    }
}