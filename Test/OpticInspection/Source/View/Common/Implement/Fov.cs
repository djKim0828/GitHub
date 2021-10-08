using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EmWorks.App.OpticInspection
{
    public class Fov : EmWorks.View.FovComponent
    {
        #region Fields

        private int _detailRowCount = 86;
        private int _fovColTotalCount = 69;
        private string _fovComponentHeaderName = "fov";

        // FOV 총 행 갯수
        private int _fovRowTotalCount = 86;

        private System.Windows.Point _lastPoint;
        private int _ledHeight = 6;         // 5 + 1(간격)으로 Design 됨.
        private int _ledWidth = 10;         // 9 + 1(간격)으로 Design 됨.

        // FOV 총 열 갯수
        private System.Windows.Point _originPosition;

        private int _roiBorderThickness = 2;
        private double _scale = 1.1;
        private int _selectColCount = 15;   // 검사 가능한 Col 갯수 : 현재 15행
        private string _selectLedNumber;
        private int _selectRowCount = 2;

        #endregion Fields

        #region Constructors

        // 검사 가능한 Row 개수 : 현재 2열
        public Fov()
        {
            Initialize();
        }

        #endregion Constructors

        #region Delegates

        public delegate void EventLabelClickHandler(object sendor, EventLabelClickMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventLabelClickHandler EventLabelClick;

        #endregion Events

        #region Methods

        private void DrawProc()
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                DrawRetangles();

                ZoomFitFov();
            });
        }

        private void DrawRetangles()
        {
            int count = _detailRowCount / 2; // ForRow 는 2줄로 만들어졌다.

            for (int i = 0; i < count; i++)
            {
                EmWorks.View.FovRowComponent temp = new EmWorks.View.FovRowComponent();
                temp.Name = _fovComponentHeaderName + i.ToString();

                stpFov.Children.Add(temp);
                Func.DoEvents();
            }

            return;
        }

        private void Fov_Loaded(object sender, RoutedEventArgs e)
        {
            Thread drawThread = new Thread(DrawProc);
            drawThread.IsBackground = true;
            drawThread.Start();
        }

        private void GrdMain_MouseLeave(object sender, MouseEventArgs e)
        {
            grdROI.Visibility = Visibility.Hidden;

            _selectLedNumber = string.Empty;
            lblLedNumber.Content = string.Empty;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void GrdMain_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;

            Point p = e.MouseDevice.GetPosition(grdMain);

            // 현재 마우스 위치의 LED 번호 출력
            ShowLedNumber(e.MouseDevice.GetPosition(grdCanvas));

            // Move FOV
            PanningFov(p);

            // Move ROI
            MoveROI(p, e.MouseDevice.GetPosition(grdCanvas));
        }

        private void GrdMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (_selectLedNumber != string.Empty)
                {
                    EventLabelClick?.Invoke(this, new EventLabelClickMessageArgs(_selectLedNumber));
                } // else
            } // else

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
            ZoomFitFov();
        }

        private void GrdMain_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(grdCanvas);

            ZoomInOutFov(p, e.Delta);

            ZoomInOutROI(p, e.Delta);
        }

        private void InitControl()
        {
            grdCanvas.Visibility = Visibility.Hidden;

            grdROI.Width = _selectColCount * _ledWidth + (_roiBorderThickness * 2) - 1; // LED 가로 검사갯수 * LED 가로 간격(현재 10px) * ROI Border (2px)
            grdROI.Height = _selectRowCount * _ledHeight + (_roiBorderThickness * 2) - 1; // LED 세로 검사갯수 * LED 세로 간격(현재 6px) * ROI Border (2px)
            grdROI.VerticalAlignment = VerticalAlignment.Top;
            grdROI.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void Initialize()
        {
            InitControl();

            RegistEvents();
        }

        private void MoveROI(Point mainPos, Point canvasPos)
        {
            // 현재 마우스 포인트가 있는 Led 의 번호 계산
            Matrix g = grdCanvas.RenderTransform.Value;
            double x = System.Math.Truncate((canvasPos.X * g.M11) / (_ledWidth * g.M11));
            double y = System.Math.Truncate((canvasPos.Y * g.M22) / (_ledHeight * g.M11));

            // 마우스와 Led의 차로 ROI위치 수정
            Matrix m = grdROI.RenderTransform.Value;
            double diffW = (canvasPos.X * g.M11) - (x * _ledWidth * g.M11);
            double diffH = (canvasPos.Y * g.M11) - (y * _ledHeight * g.M11);

            m.OffsetX = mainPos.X - diffW - (m.M11 * _roiBorderThickness);
            m.OffsetY = mainPos.Y - diffH - (m.M11 * _roiBorderThickness);

            grdROI.RenderTransform = new MatrixTransform(m);
            grdROI.Visibility = Visibility.Visible;
        }

        private void PanningFov(Point p)
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
            this.Loaded += Fov_Loaded;

            this.grdMain.PreviewMouseWheel += GrdMain_PreviewMouseWheel;
            this.grdMain.PreviewMouseRightButtonUp += GrdMain_PreviewMouseRightButtonUp;
            this.grdMain.MouseLeftButtonDown += GrdMain_PreviewMouseLeftButtonDown;
            this.grdMain.MouseLeftButtonUp += GrdMain_PreviewMouseLeftButtonUp;
            this.grdMain.MouseMove += GrdMain_MouseMove;
            this.grdMain.MouseLeave += GrdMain_MouseLeave;

            return Idx.FunctionResult.True;
        }

        private void ShowLedNumber(Point p)
        {
            Matrix g = grdCanvas.RenderTransform.Value;

            double x = System.Math.Truncate((p.X * g.M11) / (_ledWidth * g.M11));
            double y = System.Math.Truncate((p.Y * g.M22) / (_ledHeight * g.M11));

            if (y > _fovRowTotalCount - 1 || x > _fovColTotalCount - 1)
            {
                // Led위치를 벗어난 경우
                _selectLedNumber = string.Empty;
                lblLedNumber.Content = string.Empty;

                return;
            } // else

            _selectLedNumber = (y * _fovColTotalCount + x).ToString();
            lblLedNumber.Content = _selectLedNumber + " [ X : " + x.ToString() + " Y : " + y.ToString() + " ]";
        }

        private void ZoomFitFov()
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double wZoom = width / this.grdMain.Width;
            double hZoom = height / this.grdMain.Height;
            double zoom = hZoom;

            if (wZoom < hZoom)
            {
                zoom = wZoom;
            } // else

            // 가로 가운데값 구하기
            double initOffsetX = 0;
            double initOffsetY = 0;

            initOffsetX = grdMain.Width / 2 - (grdMain.Width * zoom / 2);
            initOffsetY = grdMain.Height / 2 - (grdMain.Height * zoom / 2);

            //Canvas
            Matrix m = grdCanvas.RenderTransform.Value;
            m.M11 = zoom;
            m.M22 = zoom;
            m.OffsetX = initOffsetX;
            m.OffsetY = initOffsetY;

            grdCanvas.RenderTransform = new MatrixTransform(m);
            grdCanvas.Visibility = Visibility.Visible;

            // ROI
            m = grdROI.RenderTransform.Value;
            m.M11 = zoom;
            m.M22 = zoom;
            grdROI.RenderTransform = new MatrixTransform(m);
        }

        private void ZoomInOutFov(Point p, int delta)
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

            grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        private void ZoomInOutROI(Point p, int delta)
        {
            // ROI
            Matrix m = grdROI.RenderTransform.Value;

            if (delta > 0)
            {
                m.ScaleAtPrepend(_scale, _scale, p.X, p.Y);
            }
            else
            {
                m.ScaleAtPrepend(1 / _scale, 1 / _scale, p.X, p.Y);
            }

            grdROI.RenderTransform = new MatrixTransform(m);
        }

        #endregion Methods

        #region Classes

        public class EventLabelClickMessageArgs : EventArgs
        {
            #region Constructors

            public EventLabelClickMessageArgs(string message)
            {
                Initialize();

                _Message = message;
            }

            #endregion Constructors

            #region Properties

            public string _Message { get; protected set; }
            public DateTime _OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return _OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                
                _OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}