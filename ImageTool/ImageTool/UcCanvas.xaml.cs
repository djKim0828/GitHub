using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTool
{
    public partial class UcCanvas : UserControl
    {
        #region Fields

        private ImageBrush _LoadImagebrush;

        private int _toolMode = ImageModel.ToolType.None;

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

        private LengthTool lengthTool;
        private AngleTool angleTool;
        private NoneTool noneTool;

        public void Command(int commandId)
        {
            noneTool.ChangeCursor(Cursors.Arrow);

            switch (commandId)
            {
                case ImageModel.ToolType.ZoomFit:
                    noneTool.ZoomFit(_LoadImagebrush);
                    break;

                case ImageModel.ToolType.RotateCW:
                    noneTool.Rotate(true);
                    break;

                case ImageModel.ToolType.RotateCCW:
                    noneTool.Rotate(false);
                    break;
                case ImageModel.ToolType.DeleteAll:
                    noneTool.DeleteAll();
                    break;
                default:
                    _toolMode = commandId;
                    break;
            }
        }

        public bool LoadImage(string imagePath)
        {
            try
            {
                _LoadImagebrush = new ImageBrush();
                _LoadImagebrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                _LoadImagebrush.Stretch = Stretch.None;

                this.cvsImage.Background = _LoadImagebrush;

                noneTool.ZoomFit(_LoadImagebrush);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void GrdMain_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_toolMode == ImageModel.ToolType.Length)
            {
                lengthTool.MouseEnter(sender, e);
            }
            else if (_toolMode == ImageModel.ToolType.Angle)
            {
                angleTool.MouseEnter(sender, e);
            }
            else
            {
            }
        }

        private void GrdMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_toolMode == ImageModel.ToolType.Length)
            {
                lengthTool.MouseLeave(sender, e);
            }
            else if (_toolMode == ImageModel.ToolType.Angle)
            {
                angleTool.MouseLeave(sender, e);
            }
            else
            {
            }
        }

        private void GrdMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_toolMode == ImageModel.ToolType.Length)
            {
                lengthTool.MouseMove(sender, e);
            }
            else if (_toolMode == ImageModel.ToolType.Angle)
            {
                angleTool.MouseMove(sender, e);
            }
            else
            {
                noneTool.MouseMove(sender, e);
            }
        }

        private void GrdMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolMode == ImageModel.ToolType.Length)
            {
                lengthTool.PreviewMouseLeftButtonDown(sender, e);
            }
            else if (_toolMode == ImageModel.ToolType.Angle)
            {
                angleTool.PreviewMouseLeftButtonDown(sender, e);
            }
            else
            {
                noneTool.PreviewMouseLeftButtonDown(sender, e);
            }
        }

        private void GrdMain_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_toolMode == ImageModel.ToolType.Length)
            {
                lengthTool.PreviewMouseLeftButtonUp(sender, e);
            }
            else if (_toolMode == ImageModel.ToolType.Angle)
            {
                angleTool.PreviewMouseLeftButtonUp(sender, e);
            }
            else
            {
                noneTool.PreviewMouseLeftButtonUp(sender, e);
            }
        }

        private void GrdMain_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            noneTool.ZoomFit(_LoadImagebrush);
        }

        private void GrdMain_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            noneTool.ZoomInOut(e);
        }

        private void InitControl()
        {
        }

        private void Initialize()
        {
            noneTool = new NoneTool(this);
            lengthTool = new LengthTool(this);
            angleTool = new AngleTool(this);

            InitControl();

            RegistEvents();
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
            this.grdMain.MouseEnter += GrdMain_MouseEnter;
            this.grdMain.MouseLeave += GrdMain_MouseLeave;

            return 1;
        }

        #endregion Methods
    }
}