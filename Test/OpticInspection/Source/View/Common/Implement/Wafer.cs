using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmWorks.App.OpticInspection
{
    public class Wafer : EmWorks.View.WaferComponent
    {
        #region Fields

        private int _buttonHeight = 19;
        private int _buttonWidth = 25;
        private int _colCount;
        private int _drawButtonCount = 0;
        private string _labelHaederName = "lbl";
        private System.Windows.Point _lastPoint;
        private Label _lastSelectLabel;
        private System.Windows.Point _originPoint;
        private int _rowCount;
        private double _scale = 1.1;

        #endregion Fields

        #region Constructors

        public Wafer()
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

        public void SelectLabel(int index)
        {
            if (_lastSelectLabel != null)
            {
                _lastSelectLabel.Background = null;
            } // else

            string findName = _labelHaederName + index.ToString();
            Label foundLabel = (Label)this.grdButtons.FindName(findName);

            if (foundLabel != null)
            {
                foundLabel.Background = new SolidColorBrush(Colors.Red);
            } // else

            _lastSelectLabel = foundLabel;
        }

        private void AddLabel(int colIndex, int rowIndex)
        {
            Label tempLabel = new Label();
            tempLabel.Content = _drawButtonCount.ToString();
            tempLabel.Name = _labelHaederName + _drawButtonCount.ToString();
            tempLabel.Style = (Style)Application.Current.Resources["lblPlat"];
            tempLabel.PreviewMouseDown += TempLabel_PreviewMouseDown;
            tempLabel.FontSize = 6;
            tempLabel.Tag = new fovInformation(_colCount, _rowCount, colIndex, rowIndex, _drawButtonCount);

            _drawButtonCount++;

            Grid.SetColumn(tempLabel, colIndex);
            Grid.SetRow(tempLabel, rowIndex);
            grdButtons.Children.Add(tempLabel);
            RegisterName(tempLabel.Name, tempLabel);
        }

        private void DivideGrid()
        {
            // Button 을 담을 Grid의 크기 재설정 - 크기를 다시 계산해서 좌우상하를 자른다. ( 가운데 버튼 정렬 위함 )
            _colCount = Convert.ToInt16(Math.Truncate(grdMain.Width / _buttonWidth));
            _rowCount = Convert.ToInt16(Math.Truncate(grdMain.Height / _buttonHeight));

            grdButtons.Width = _buttonWidth * _colCount;
            grdButtons.Height = _buttonHeight * _rowCount;

            // 정해진 버튼의 가로/세로 크기로 Grid를 나눔
            for (int i = 0; i < _colCount; i++)
            {
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(_buttonWidth, GridUnitType.Pixel);
                grdButtons.ColumnDefinitions.Add(c1);
            }

            for (int i = 0; i < _rowCount; i++)
            {
                RowDefinition r1 = new RowDefinition();
                r1.Height = new GridLength(_buttonHeight, GridUnitType.Pixel);
                grdButtons.RowDefinitions.Add(r1);
            }
        }

        private void DrawProc()
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                DivideGrid();

                DrawRetanglesInSideCircle();

                ZoomFit();
            });
        }

        private void DrawRetanglesInSideCircle()
        {
            // 마진 적용 원 : 마진을 구해서 센터를 기준으로 사각형이 그려지게한다.
            System.Drawing.Rectangle circle = new System.Drawing.Rectangle(0,
                                    0, Convert.ToInt16(grdButtons.Width), Convert.ToInt16(grdButtons.Height));

            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    System.Drawing.Rectangle rectTemp = new System.Drawing.Rectangle(col * _buttonWidth,
                       row * _buttonHeight,
                       _buttonWidth,
                       _buttonHeight);

                    if (isRectangleInsideCircle(rectTemp, circle) == false)
                    {
                        continue;
                    } // else

                    AddLabel(col, row);
                }
            }
        }

        private void GrdMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (grdCanvas.IsMouseCaptured == false)
            {
                return;
            } // else

            Matrix m = grdCanvas.RenderTransform.Value;
            Point p = e.MouseDevice.GetPosition(grdMain);
            m.OffsetX = _originPoint.X + (p.X - _lastPoint.X);
            m.OffsetY = _originPoint.Y + (p.Y - _lastPoint.Y);

            grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        private void GrdMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdCanvas.IsMouseCaptured)
            {
                return;
            } // else

            _originPoint.X = grdCanvas.RenderTransform.Value.OffsetX;
            _originPoint.Y = grdCanvas.RenderTransform.Value.OffsetY;

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
            grdCanvas.Visibility = Visibility.Hidden;
        }

        private void Initialize()
        {
            InitControl();

            RegistEvents();
        }

        private bool isRectangleInsideCircle(System.Drawing.Rectangle rect, System.Drawing.Rectangle circle)
        {
            // 4점을 구한다.
            System.Drawing.Point leftTop = rect.Location;

            System.Drawing.Point rightTop = new System.Drawing.Point();
            rightTop.X = rect.Location.X + rect.Width;
            rightTop.Y = rect.Location.Y;

            System.Drawing.Point leftButtom = new System.Drawing.Point();
            leftButtom.X = rect.X;
            leftButtom.Y = rect.Y + rect.Height;

            System.Drawing.Point rightButtom = new System.Drawing.Point();
            rightButtom.X = rect.Location.X + rect.Width;
            rightButtom.Y = rect.Y + rect.Height;

            System.Drawing.Point centerPoint = new System.Drawing.Point();
            centerPoint.X = circle.X + circle.Width / 2;
            centerPoint.Y = circle.Y + circle.Height / 2;

            int halfLength = circle.Width / 2;

            if (Math.Pow((centerPoint.X - leftTop.X), 2) + Math.Pow((centerPoint.Y - leftTop.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            } // else

            if (Math.Pow((centerPoint.X - rightTop.X), 2) + Math.Pow((centerPoint.Y - rightTop.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            } // else

            if (Math.Pow((centerPoint.X - leftButtom.X), 2) + Math.Pow((centerPoint.Y - leftButtom.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            } // else

            if (Math.Pow((centerPoint.X - rightButtom.X), 2) + Math.Pow((centerPoint.Y - rightButtom.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            } // else

            return true;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += Wafer_Loaded;

            this.grdMain.PreviewMouseWheel += GrdMain_PreviewMouseWheel;
            this.grdMain.PreviewMouseRightButtonUp += GrdMain_PreviewMouseRightButtonUp;
            this.grdMain.MouseLeftButtonDown += GrdMain_PreviewMouseLeftButtonDown;
            this.grdMain.MouseLeftButtonUp += GrdMain_PreviewMouseLeftButtonUp;
            this.grdMain.MouseMove += GrdMain_MouseMove;

            return Idx.FunctionResult.True;
        }

        private void TempLabel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                object temp = ((Label)sender).Tag;

                EventLabelClick?.Invoke(this, new EventLabelClickMessageArgs(temp));
            } // else
        }

        private void Wafer_Loaded(object sender, RoutedEventArgs e)
        {
            Thread drawThread = new Thread(DrawProc);
            drawThread.IsBackground = true;
            drawThread.Start();
        }

        private void ZoomFit()
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double zoom = height / this.grdMain.Height;

            // 가로 가운데값 구하기
            double initOffsetX = 0;
            //initOffsetX = (grdMain.Width / 2) - (grdButtons.Width / 2 * Math.Round(zoom, 1));
            initOffsetX = grdMain.Width / 2 - (grdMain.Width * zoom / 2);

            Matrix m = grdCanvas.RenderTransform.Value;
            m.M11 = zoom;
            m.M22 = zoom;
            m.OffsetX = initOffsetX;
            m.OffsetY = 0;

            grdCanvas.RenderTransform = new MatrixTransform(m);
            grdCanvas.Visibility = Visibility.Visible;
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

            grdCanvas.RenderTransform = new MatrixTransform(m);
        }

        #endregion Methods

        #region Classes

        public class EventLabelClickMessageArgs : EventArgs
        {
            #region Constructors

            public EventLabelClickMessageArgs(object information)
            {
                Initialize();

                _information = (fovInformation)information;
            }

            #endregion Constructors

            #region Properties

            public fovInformation _information { get; protected set; }

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

        public class fovInformation
        {
            #region Constructors

            public fovInformation(int colCount, int rowCount, int colIndex, int rowIndex, int labelIndex)
            {
                _ColCount = colCount;
                _RowCount = rowCount;
                _ColIndex = colIndex;
                _RowIndex = rowIndex;
                _LabelIndex = labelIndex;
            }

            #endregion Constructors

            #region Properties

            public int _ColCount { get; protected set; }
            public int _ColIndex { get; protected set; }
            public int _LabelIndex { get; protected set; }
            public int _RowCount { get; protected set; }
            public int _RowIndex { get; protected set; }

            #endregion Properties
        }

        #endregion Classes
    }
}