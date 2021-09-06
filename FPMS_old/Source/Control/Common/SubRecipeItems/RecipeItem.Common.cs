using System;
using System.Threading;

namespace FPMS.E2105_FS111_121
{
    public partial class RecipeItem
    {
        private DateTime _targetTime = DateTime.Now;
        private DateTime _startTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now;
        private TimeSpan _term = new TimeSpan(0);

        /*
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        */

        #region Methods

        [IsItem(true)]
        public bool Wait(double Hour = 0, double Min = 0, double Sec = 0, double miliSec = 0)
        {
            _startTime = DateTime.Now;
            _targetTime = _startTime.AddHours(Hour).AddMinutes(Min).AddSeconds(Sec).AddMilliseconds(miliSec);
            DateTime checkTime = _startTime;

            while (checkTime < _targetTime)
            {
                Thread.Sleep(1);
                checkTime = DateTime.Now;

                if (false)//Todo : Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }// else
            }
            return true;
        }

        #endregion Methods

        /*
        [IsItem(true)]
        [IsComboBox("ClickPoint")]
        //[IsComboBox("ClickPoint", typeof(EMouseClickPoint))]
        public bool Mouse_Click(string ClickPoint)
        {
            Bitmap Macro_Img;

            //switch (ClickPoint)
            //{
            //    case EMouseClickPoint.Sticking3Teach:
            //        Macro_Img = new Bitmap(Application.StartupPath + "\\MacroIMG\\TeachNonuniformity.PNG");
            //        break;
            //    case EMouseClickPoint.Sticking3TeachYes:
            //        Macro_Img = new Bitmap(Application.StartupPath + "\\MacroIMG\\TeachNonuniformityYes.PNG");
            //        break;
            //    case EMouseClickPoint.Sticking3StropMeasure:
            //        Macro_Img = new Bitmap(Application.StartupPath + "\\MacroIMG\\Sticking3StopMeasure.PNG");
            //        break;
            //    default:
            //        return false;
            //}

            Bitmap bmp = new Bitmap(Global.Global.Listscreens[0].Bounds.Width, Global.Global.Listscreens[0].Bounds.Height);
            var captureimg = Graphics.FromImage(bmp);
            captureimg.CopyFromScreen(Global.Global.Listscreens[0].Bounds.X, Global.Global.Listscreens[0].Bounds.Y, 0, 0, Global.Global.Listscreens[0].Bounds.Size);

            if (!searchIMG(bmp, Macro_Img))
            {
                return false;
            }

            MacroMouseClick(AutoClickPoint);
            return true;
        }
        public bool searchIMG(Bitmap screen_img, Bitmap find_img)
        {
            //스크린 이미지 선언
            using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
            //찾을 이미지 선언
            using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
            //스크린 이미지에서 FindMat 이미지를 찾아라
            using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
            {
                //찾은 이미지의 유사도를 담을 더블형 최대 최소 값을 선언합니다.
                double minval, maxval = 0;
                //찾은 이미지의 위치를 담을 포인트형을 선업합니다.
                OpenCvSharp.Point minloc, maxloc;
                //찾은 이미지의 유사도 및 위치 값을 받습니다.
                Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

                if (maxval >= 0.9)
                {
                    AutoClickPoint = new OpenCvSharp.Point(maxloc.X + FindMat.Width / 2, maxloc.Y + FindMat.Height / 2);
                    return true;
                }
            }
            return false;
        }
        private void MacroMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void MacroMouseClick(OpenCvSharp.Point savePoint)
        {
            SetCursorPos(savePoint.X, savePoint.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        */
    }
}