using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTool
{
    /// <summary>
    /// Interaction logic for UcCanvas.xaml
    /// </summary>
    public partial class UcCanvas : UserControl
    {
        #region Fields

        private bool _IsImageLoad = false;

        private Point _Last;
        private Point _Origin;
        private double _Scale = 1.1;
        private double _ZoomRadio = 1;

        ImageBrush _LoadImagebrush;

        #endregion Fields

        #region Constructors

        public UcCanvas()
        {
            InitializeComponent();

            RegistEvents();
        }

        #endregion Constructors

        #region Methods

        public bool LoadImage(string imagePath)
        {
            try
            {
                _LoadImagebrush = new ImageBrush();
                _LoadImagebrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                double zoom = this.ActualHeight / _LoadImagebrush.ImageSource.Height;                

                this.cvsImage.Background = _LoadImagebrush;
                this.cvsImage.Width = _LoadImagebrush.ImageSource.Height * zoom;
                this.cvsImage.Height = _LoadImagebrush.ImageSource.Width * zoom;

                _IsImageLoad = true;
                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
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

        public void ZoomFit()
        { 
            double zoom = this.ActualHeight / _LoadImagebrush.ImageSource.Height;

            this.cvsImage.Background = _LoadImagebrush;

            this.cvsImage.Width = _LoadImagebrush.ImageSource.Height * zoom;
            this.cvsImage.Height = _LoadImagebrush.ImageSource.Width * zoom;
                        
        }

        public void ZoomReal()
        {
            _ZoomRadio = 1;

            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleY = _ZoomRadio;
            myScaleTransform.ScaleX = _ZoomRadio;

            TranslateTransform myTranslate = new TranslateTransform();
            myTranslate.X = 0;
            myTranslate.Y = 0;

            TransformGroup myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);
            myTransformGroup.Children.Add(myTranslate);

            cvsImage.RenderTransform = myTransformGroup;
        }

        private void GrdMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsImageLoad == false)
            {
                return;
            }

            if (cvsImage.IsMouseCaptured == false)
            {
                return;
            }

            Matrix m = cvsImage.RenderTransform.Value;
            Point p = e.MouseDevice.GetPosition(grdMain);
            m.OffsetX = _Origin.X + (p.X - _Last.X);
            m.OffsetY = _Origin.Y + (p.Y - _Last.Y);

            cvsImage.RenderTransform = new MatrixTransform(m);
        }

        private void GrdMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_IsImageLoad == false)
            {
                return;
            }

            if (cvsImage.IsMouseCaptured)
            {
                return;
            }

            _Origin.X = cvsImage.RenderTransform.Value.OffsetX;
            _Origin.Y = cvsImage.RenderTransform.Value.OffsetY;

            _Last = e.GetPosition(grdMain);

            cvsImage.CaptureMouse();
        }

        private void GrdMain_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cvsImage.ReleaseMouseCapture();
        }

        private void GrdMain_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_IsImageLoad == false)
            {
                return;
            }

            ZoomFit();
        }

        private void GrdMain_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_IsImageLoad == false)
            {
                return;
            }

            Point p = e.MouseDevice.GetPosition(cvsImage);

            Matrix m = cvsImage.RenderTransform.Value;
            if (e.Delta > 0)
                m.ScaleAtPrepend(_Scale, _Scale, p.X, p.Y);
            else
                m.ScaleAtPrepend(1 / _Scale, 1 / _Scale, p.X, p.Y);

            cvsImage.RenderTransform = new MatrixTransform(m);
        }

        private int RegistEvents()
        {
            this.grdMain.PreviewMouseWheel += GrdMain_PreviewMouseWheel;
            this.grdMain.PreviewMouseRightButtonUp += GrdMain_PreviewMouseRightButtonUp;
            this.grdMain.MouseLeftButtonDown += GrdMain_PreviewMouseLeftButtonDown;
            this.grdMain.MouseLeftButtonUp += GrdMain_PreviewMouseLeftButtonUp;
            this.grdMain.MouseMove += GrdMain_MouseMove;

            return 0;
        }

        #endregion Methods
    }
}