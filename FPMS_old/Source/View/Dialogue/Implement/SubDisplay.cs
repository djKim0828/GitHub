using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace FPMS.E2105_FS111_121
{
    public class SubDisplay : EmWorks.View.SubDisplayDialog
    {
        #region Fields

        private SolidBrush _colorbrush;
        private Pen _colorpen;
        private int _displayNumber;
        private Graphics _makeImg;
        private List<Point> _setPointList = new List<Point>();
        private Bitmap _showimg;
        private Size _size;

        #endregion Fields

        #region Constructors

        public SubDisplay(Rectangle bound, int formNumber)
        {

            _showimg = new Bitmap(bound.Size.Width, bound.Size.Height);
            _makeImg = Graphics.FromImage(_showimg);
            _colorbrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            _colorpen = new Pen(Color.FromArgb(0, 0, 0));

            _displayNumber = formNumber;

            Initialize(bound);
        }

        private void Initialize(Rectangle bound)
        {

            foreach (ePointList item in Enum.GetValues(typeof(ePointList)))
            {
                switch (item)
                {
                    case ePointList.CenterPoint:
                        _setPointList.Add(new Point(bound.Size.Width / 2, bound.Size.Height / 2));
                        break;

                    case ePointList.LeftTop:
                        _setPointList.Add(new Point(0, 0));
                        break;

                    case ePointList.LeftMid:
                        _setPointList.Add(new Point(0, bound.Size.Height / 2));
                        break;

                    case ePointList.LeftBottom:
                        _setPointList.Add(new Point(0, bound.Size.Height - 1));
                        break;

                    case ePointList.MidTop:
                        _setPointList.Add(new Point(bound.Size.Width / 2, 0));
                        break;

                    case ePointList.MidBottom:
                        _setPointList.Add(new Point(bound.Size.Width / 2, bound.Size.Height - 1));
                        break;

                    case ePointList.RightTop:
                        _setPointList.Add(new Point(bound.Size.Width - 1, 0));
                        break;

                    case ePointList.RightMid:
                        _setPointList.Add(new Point(bound.Size.Width - 1, bound.Size.Height / 2));
                        break;

                    case ePointList.RightBottom:
                        _setPointList.Add(new Point(bound.Size.Width - 1, bound.Size.Height - 1));
                        break;

                    default:
                        break;
                }
            }

            _size = bound.Size;
            //pictureBox1.BackColor = Color.FromArgb(0, 0, 0);
        }

        #endregion Constructors

        #region Enums

        public enum ePointList
        {
            CenterPoint,
            LeftTop,
            LeftMid,
            LeftBottom,
            MidTop,
            MidBottom,
            RightTop,
            RightMid,
            RightBottom,
        }

        #endregion Enums

        #region Methods

        public bool DrawCenterDisplay()
        {
            _colorbrush.Color = Color.FromArgb(0, 0, 0);
            _makeImg.FillRectangle(_colorbrush, 0, 0, _size.Width, _size.Height);

            _colorpen.Color = Color.FromArgb(255, 255, 255);

            _makeImg.DrawLine(_colorpen, _setPointList[(int)ePointList.LeftMid], _setPointList[(int)ePointList.RightMid]);
            _makeImg.DrawLine(_colorpen, _setPointList[(int)ePointList.MidTop], _setPointList[(int)ePointList.MidBottom]);
            _makeImg.DrawLine(_colorpen, _setPointList[(int)ePointList.LeftTop], _setPointList[(int)ePointList.RightBottom]);
            _makeImg.DrawLine(_colorpen, _setPointList[(int)ePointList.LeftBottom], _setPointList[(int)ePointList.RightTop]);

            Size recsize = new Size(_setPointList[(int)ePointList.RightBottom]);
            Rectangle OutLine = new Rectangle(_setPointList[(int)ePointList.LeftTop], recsize);
            _makeImg.DrawRectangle(_colorpen, OutLine);

            _showimg = new Bitmap(_size.Width, _size.Height, _makeImg);

            imgSubDisplay.Source = BitmapConvertBitmapimage(_showimg);

            return true;
        }

        public bool DrawColorDisplay(int red, int green, int blue)
        {
            _colorbrush.Color = Color.FromArgb(red, green, blue);
            _makeImg.FillRectangle(_colorbrush, 0, 0, _size.Width, _size.Height);

            _showimg = new Bitmap(_size.Width, _size.Height, _makeImg);

            imgSubDisplay.Source = BitmapConvertBitmapimage(_showimg);

            return true;
        }

        public bool DrawColorDisplay(string color, int level)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            switch (color)
            {
                case "W":
                    red = level;
                    green = level;
                    blue = level;
                    break;

                case "R":
                    red = level;
                    break;

                case "G":
                    green = level;
                    break;

                case "B":
                    blue = level;
                    break;

                default:
                    return false;
            }

            _colorbrush.Color = System.Drawing.Color.FromArgb(red, green, blue);
            _makeImg.FillRectangle(_colorbrush, 0, 0, _size.Width, _size.Height);

            _showimg = new Bitmap(_size.Width, _size.Height, _makeImg);

            imgSubDisplay.Source = BitmapConvertBitmapimage(_showimg);

            return true;
        }

        public bool DrawDisplayID()
        {
            _colorbrush.Color = Color.FromArgb(0, 0, 0);
            _makeImg.FillRectangle(_colorbrush, 0, 0, _size.Width, _size.Height);

            _colorbrush.Color = Color.FromArgb(255, 255, 255);
            Size recsize = new Size(_setPointList[(int)ePointList.RightBottom]);
            float FontSize = 200;

            Font IDFont = new Font("C:\\Windows\\Fonts\\Calibri.ttf", FontSize);
            _makeImg.DrawString(_displayNumber.ToString(), IDFont, _colorbrush, _size.Width / 2 - FontSize / 2, _size.Height / 2 - FontSize * 4 / 5);

            _showimg = new Bitmap(_size.Width, _size.Height, _makeImg);

            imgSubDisplay.Source = BitmapConvertBitmapimage(_showimg);

            return true;
        }

        public bool DrawFromImgFile(string filepath)
        {
            _showimg = new Bitmap(Image.FromFile(filepath));

            imgSubDisplay.Source = BitmapConvertBitmapimage(_showimg);

            return true;
        }

        private BitmapImage BitmapConvertBitmapimage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        #endregion Methods
    }
}