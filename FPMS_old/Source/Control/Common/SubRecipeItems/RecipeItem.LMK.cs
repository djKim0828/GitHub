using System;
using System.IO;

namespace FPMS.E2105_FS111_121
{
    public partial class RecipeItem
    {
        #region Fields
        
        private DirectoryInfo _resultDirectory;
        private string _saveSampleName;
        private bool _checkTerm;

        #endregion Fields

        #region Enums

        public enum Epictype
        {
            SinglePic,
            MultiPic,
            HighDynPic,
            ColorHighDynSimplePic,
            ColorMultiPic,
            ColorHighDynPic,
        }

        #endregion Enums

        /*
        [IsItem(true)]
        [IsComboBox("pictype", typeof(Epictype))]
        public bool Measure_basic(Epictype pictype, int Count = 10)
        {
            //SetCursorPos(Global.Global.Listscreens[0].Bounds.Width / 2, Global.Global.Listscreens[0].Bounds.Height / 2);
            Task<bool> resultTask = new TaskFactory<bool>().StartNew(() =>
            {
                mLMK.ColorAutoScanTime();
                switch (pictype)
                {
                    case Epictype.SinglePic:
                        mLMK.SinglePic2();
                        break;

                    case Epictype.MultiPic:
                        mLMK.MultiPic2(Count);
                        break;

                    case Epictype.HighDynPic:
                        mLMK.HighDynPic3(Count);
                        break;

                    case Epictype.ColorHighDynSimplePic:
                        mLMK.ColorHighDynPic();
                        break;

                    case Epictype.ColorMultiPic:
                        mLMK.ColorCaptureMultiPic(Count);
                        break;

                    case Epictype.ColorHighDynPic:
                        mLMK.ColorCaptureHighDyn(Count);
                        break;

                    default:
                        break;
                }
                return true;
            });
            return resultTask.Result;
        }

        [IsItem(true)]
        [IsComboBox("Color", typeof(EColor))]
        public bool Measure_Sticking3(EColor Color, int Level, double Hour = 0, double Min = 0, double Sec = 0)
        {
            string burninimg = String.Format("{0}_Burn3Level.png", Path.GetFileNameWithoutExtension(Global.Global.CurrentModel.FilePath));
            Global.Global.DrawFromImgFile(burninimg);
            StartTime = DateTime.Now;
            TargetTime = StartTime.AddHours(Hour).AddMinutes(Min).AddSeconds(Sec);
            DateTime ReadyTime = TargetTime.AddMinutes(-1.0);
            DateTime CheckTime = StartTime;

            while (CheckTime < ReadyTime)
            {
                Thread.Sleep(1);
                CheckTime = DateTime.Now;
                if (Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }
            }

            Bitmap Macro_Img = new Bitmap(Application.StartupPath + "\\MacroIMG\\TimeControlledCaptureStart.png");
            Bitmap bmp = new Bitmap(Global.Global.Listscreens[0].Bounds.Width, Global.Global.Listscreens[0].Bounds.Height);
            var captureimg = Graphics.FromImage(bmp);
            captureimg.CopyFromScreen(Global.Global.Listscreens[0].Bounds.X, Global.Global.Listscreens[0].Bounds.Y, 0, 0, Global.Global.Listscreens[0].Bounds.Size);
            if (!searchIMG(bmp, Macro_Img))
            {
                return false;
            }

            while (CheckTime < TargetTime)
            {
                Thread.Sleep(1);
                CheckTime = DateTime.Now;
                if (Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }
            }
            SetCursorPos(AutoClickPoint.X, AutoClickPoint.Y);
            Global.Global.DrawColorDisplay(Color.ToString(), Level);
            MacroMouseClick(AutoClickPoint);
            return true;
        }

        [IsItem(true)]
        [IsComboBox("Color", typeof(EColor))]
        public bool Measure_Sticking3_Ex(EColor Color, int Level, int Delay_ms = -1, double Hour = 0, double Min = 0, double Sec = 0)
        {
            string burninimg = String.Format("{0}_Burn3Level.png", Path.GetFileNameWithoutExtension(Global.Global.CurrentModel.FilePath));
            Global.Global.DrawFromImgFile(burninimg);
            StartTime = DateTime.Now;
            TargetTime = StartTime.AddHours(Hour).AddMinutes(Min).AddSeconds(Sec);
            DateTime ReadyTime = TargetTime.AddMinutes(-1.0);
            DateTime CheckTime = StartTime;
            while (CheckTime < ReadyTime)
            {
                Thread.Sleep(1);
                CheckTime = DateTime.Now;
                if (Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }
            }

            Bitmap Macro_Img = new Bitmap(Application.StartupPath + "\\MacroIMG\\TimeControlledCaptureStart.png");
            Bitmap bmp = new Bitmap(Global.Global.Listscreens[0].Bounds.Width, Global.Global.Listscreens[0].Bounds.Height);
            var captureimg = Graphics.FromImage(bmp);
            captureimg.CopyFromScreen(Global.Global.Listscreens[0].Bounds.X, Global.Global.Listscreens[0].Bounds.Y, 0, 0, Global.Global.Listscreens[0].Bounds.Size);
            if (!searchIMG(bmp, Macro_Img))
            {
                return false;
            }

            while (CheckTime < TargetTime)
            {
                Thread.Sleep(1);
                CheckTime = DateTime.Now;
                if (Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }
            }
            if (Delay_ms < 0)
            {
                dstarttime = DateTime.Now;
                SetCursorPos(AutoClickPoint.X, AutoClickPoint.Y);
                Global.Global.DrawColorDisplay(Color.ToString(), Level);
                MacroMouseClick(AutoClickPoint);
                dendtime = DateTime.Now;
            }
            else
            {
                dstarttime = DateTime.Now;
                SetCursorPos(AutoClickPoint.X, AutoClickPoint.Y);
                Global.Global.DrawColorDisplay(Color.ToString(), Level);
                Thread.Sleep(Delay_ms);
                MacroMouseClick(AutoClickPoint);
                dendtime = DateTime.Now;
            }
            dterm = dendtime.Subtract(dstarttime);

            Checkterm = true;
            return true;
        }
        [IsItem(true)]
        public bool Measure_BlakMura()
        {
            return true;
        }

        [IsItem(true)]
        public bool Calculate_Glass_Lum()
        {
            string qCommand, qResult = "";
            int irReturn = 0;
            // Cropping images
            qCommand = @"source ""$ModulePath / ttip.module""
                        set Error """"
                        namespace eval::ttIP{
                            CreateImage {Original_Color} {rgbfloat} {2999 4082 15 6}
                            CopyImage {Original_Color} {_COLPIC_}
                            CreateImage {Cal_X} {float} {2999 4082 15 6}
                            CreateImage {Cal_Y} {float} {2999 4082 15 6}
                            CreateImage {Cal_Z} {float} {2999 4082 15 6}
                            SplitColorImage {Original_Color} {XYZ} {17.7 17.7 17.7} {Cal_X} {Cal_Y} {Cal_Z}
                            MulImgConst {Cal_X} {Cal_X} {1.256} {0}
                            MulImgConst {Cal_Y} {Cal_Y} {1.252} {0}
                            MulImgConst {Cal_Z} {Cal_Z} {1.298} {0}
                            MergeColorImage {Cal_X} {Cal_Y} {Cal_Z} {XYZ} {17.7 17.7 17.7} {_COLPIC_}
                        }";
            mLMK.ExecTclCommand(qCommand, ref irReturn, ref qResult);

            return true;
        }

        [IsItem(true)]
        public bool Search_for_Region(string Enlarge)
        {
            string sColorImage = "Color image";
            int iColorImage = 0;

            string qCommand, qResult = "";
            int irReturn = 0;

            int iImage = -100;
            if (mLMK.GetIndexOfImage(sColorImage, ref iImage) == 0)
            {
                iColorImage = iImage;
            }
            else
            {
                return false;
            }

            // Search for region
            string[] args =
                {
                    sColorImage,
                    "0",
                    "3", // Smooth
                    Enlarge, // Enlarge //여유 픽셀
                    "3", // Smooth Contour
                    "700", // Min Size
                    "4000", // Max Size
                    "1", // Rectangle
                    "0", // Convex
                    "0", // Use Region
                    "0", // Stat
                    "1" // Sep
                };
            qCommand = @"source ""$ModulePath/ttip.module""
                set Error """"
                namespace eval ::ttIP {
                    SearchRegions_Create
                    #Parameter: {Image name} {Threshold} {Smooth}
                    SearchRegions_Image {" + args[0] + @"} {" + args[1] + @"} {" + args[2] + @"}
                    #Parameter: {Enlarge} {Smooth} {Min} {Max} {Rectangle} {Convex}
                    SearchRegions_Regions {" + args[3] + @"} {" + args[4] + @"} {" + args[5] + @"} {" + args[6] + @"} {" + args[7] + @"} {" + args[8] + @"}
                    #Parameter: {UseReg} {Statistic} {Separate}
                    SearchRegions_Statistics {" + args[9] + @"} {" + args[10] + @"} {" + args[11] + @"}
                    #Parameter: {Delete} {Create} {Dist} {Area} {Circum} {Moment} {Rota} {Maximum}
                    SearchRegions_Parameter {0} {0} {1.00} {1.00} {1.00} {1.00} {1.00} {2.50}
                    SearchRegions_Execute
                    SearchRegions_Delete
                }";
            mLMK.ExecTclCommand(qCommand, ref irReturn, ref qResult);

            return true;
        }

        [IsItem(true)]
        public bool SaveExel(string FileMiddlename = "")
        {
            CheckFilePathFolder();
            if (FileMiddlename != "")
            {
                FileMiddlename = FileMiddlename + "_";
            }
            string tempfilename = string.Format("{0}_{1}{2}", SaveSampleName, FileMiddlename, sDateTime) + Global.Global.IsExcelTemplete.Extension;
            string sfileName = Path.GetFullPath(ResultDirectory + "\\" + tempfilename);

            Global.Global.IsExcelTemplete.CopyTo(sfileName);

            mLMK.ExcelOpenFile(sfileName);
            mLMK.ExcelSetVisible(0);
            mLMK.ExcelSelectAll();
            mLMK.ExcelCopySelected();
            mLMK.ExcelSave();
            mLMK.ExcelClose();
            if (Checkterm)
            {
                MessageBox.Show(dterm.TotalMilliseconds.ToString() + " mS");
            }
            Checkterm = false;
            return true;
        }

        [IsItem(true)]
        public bool End()
        {
            return true;
        }
        private void CheckFilePathFolder()
        {
            sDateTime = Global.Global.MeasureStartTime.ToString("yyMMddhhmmss");
            SaveSampleName = "";
            if (Global.Global.CurrentModel.SampleName.Length > 4)
            {
                SaveSampleName = Global.Global.CurrentModel.SampleName.Substring(Global.Global.CurrentModel.SampleName.Length - 4);
            }
            else
            {
                SaveSampleName = Global.Global.CurrentModel.SampleName;
            }

            string directorytoday = Global.Global.MeasureStartTime.ToString("yyyyMMdd");
            ResultDirectory = new DirectoryInfo(Path.GetFullPath(Global.Global.Config.ResultPath + "\\" + directorytoday + "\\" + Global.Global.CurrentModel.SampleName + "\\"));
            if (ResultDirectory.Exists == false)
            {
                ResultDirectory.Create();
            }
        }
        */
    }
}