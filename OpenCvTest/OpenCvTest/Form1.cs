using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenCvTest
{
    public partial class Form1 : Form
    {
        #region Fields

        private Config _config;

        private String _path = Application.StartupPath + @"\config.json";

        private Mat _srcImage;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void btnImage1_Click(object sender, EventArgs e)
        {
            _srcImage = Cv2.ImRead("hex.jpg");
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage);
        }

        private void btnImage2_Click(object sender, EventArgs e)
        {
            _srcImage = Cv2.ImRead("RGB.png");
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _srcImage = Cv2.ImRead(dlg.FileName);

                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Mat yellow = new Mat();
            Mat dst = _srcImage.Clone();

            OpenCvSharp.CPlusPlus.Point[][] contours;

            int lowR = Convert.ToInt16(txtLowR.Text);
            int lowG = Convert.ToInt16(txtLowG.Text);
            int lowB = Convert.ToInt16(txtLowB.Text);

            int uppR = Convert.ToInt16(txtUppR.Text);
            int uppG = Convert.ToInt16(txtUppG.Text);
            int uppB = Convert.ToInt16(txtUppB.Text);

            Cv2.InRange(_srcImage, new Scalar(lowR, lowG, lowB),
                        new Scalar(uppR, uppG, uppB), yellow);

            SearchContours0(yellow, out contours);

            if (contours.Length < 1)
            {
                return;
            } // else

            List<OpenCvSharp.CPlusPlus.Point[]> new_contours = new List<OpenCvSharp.CPlusPlus.Point[]>();
            foreach (OpenCvSharp.CPlusPlus.Point[] p in contours)
            {
                double length = Cv2.ArcLength(p, true);
                if (length > 100)
                {
                    new_contours.Add(p);
                }
            }

            Cv2.DrawContours(dst,
                            new_contours,
                            -1,
                            new Scalar(255, 0, 0),
                            1,
                            OpenCvSharp.LineType.AntiAlias,
                            null,
                            1);

            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);

            //Cv2.ImShow("dst", dst);
            //Cv2.WaitKey(0);
        }

        private void btnSearchDef_Click(object sender, EventArgs e)
        {
            Mat yellow = new Mat();
            Mat dst = _srcImage.Clone();

            OpenCvSharp.CPlusPlus.Point[][] contours;
            HierarchyIndex[] hierarchy;

            Cv2.InRange(_srcImage, new Scalar(0, 127, 127), new Scalar(100, 255, 255), yellow);
            Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.Tree,
                OpenCvSharp.ContourChain.ApproxTC89KCOS);

            List<OpenCvSharp.CPlusPlus.Point[]> new_contours = new List<OpenCvSharp.CPlusPlus.Point[]>();
            foreach (OpenCvSharp.CPlusPlus.Point[] p in contours)
            {
                double length = Cv2.ArcLength(p, true);
                if (length > 100)
                {
                    new_contours.Add(p);
                }
            }

            Cv2.DrawContours(dst,
                            new_contours,
                            -1, new Scalar(255, 0, 0), 2, OpenCvSharp.LineType.AntiAlias, null, 1);

            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);

            Cv2.ImShow("dst", dst);

            Cv2.WaitKey(0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();

            btnImage2_Click(null, null);
        }

        private void LoadConfig()
        {
            // 설정 읽어오기
            _config = JSON.Load(_path);
            if (_config != null)
            {
                txtLowR.Text = _config.LowR;
                txtLowG.Text = _config.LowG;
                txtLowB.Text = _config.LowB;

                txtUppR.Text = _config.UppR;
                txtUppG.Text = _config.UppG;
                txtUppB.Text = _config.UppB;
            }
        }

        private void SaveConfig()
        {
            if (_config == null)
            {
                _config = new Config();
            }

            _config.LowR = txtLowR.Text;
            _config.LowG = txtLowG.Text;
            _config.LowB = txtLowB.Text;

            _config.UppR = txtUppR.Text;
            _config.UppG = txtUppG.Text;
            _config.UppB = txtUppB.Text;

            JSON.Save(_path, _config);
        }

        private void SearchContours0(Mat yellow, out OpenCvSharp.CPlusPlus.Point[][] contours)
        {
            WriteLog("Start searching 0 ");

            HierarchyIndex[] hierarchy;

            Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.External,
                OpenCvSharp.ContourChain.ApproxTC89KCOS);

            if (contours.Length > 1)
            {
                WriteLog("searching success - Tree");
                return;
            }
            else
            {
                WriteLog("Can not searching - Tree");
            }

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.ApproxTC89KCOS);
                        break;

                    case 1:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.List,
                            OpenCvSharp.ContourChain.ApproxTC89KCOS);
                        break;

                    case 2:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.FloodFill,
                            OpenCvSharp.ContourChain.ApproxTC89KCOS);
                        break;

                    default:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.Tree,
                            OpenCvSharp.ContourChain.ApproxTC89KCOS);
                        break;
                }

                if (contours.Length > 1)
                {
                    WriteLog("searching success : " + i.ToString());
                    break;
                }
                else
                {
                    WriteLog("Can not searching : " + i.ToString());
                }
            }
        }

        private void SearchContours1(Mat yellow, out OpenCvSharp.CPlusPlus.Point[][] contours)
        {
            WriteLog("Start searching 1 ");

            HierarchyIndex[] hierarchy;

            Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.Tree,
                OpenCvSharp.ContourChain.ApproxTC89KCOS);

            if (contours.Length > 1)
            {
                WriteLog("searching success - ApproxTC89KCOS");
                return;
            }
            else
            {
                WriteLog("Can not searching - ApproxTC89KCOS");
            }

            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.ApproxNone);
                        break;

                    case 1:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.ApproxSimple);
                        break;

                    case 2:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.ApproxTC89L1);
                        break;

                    case 3:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.Code);
                        break;

                    default:
                        Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.CComp,
                            OpenCvSharp.ContourChain.LinkRuns);
                        break;
                }

                if (contours.Length > 1)
                {
                    WriteLog("searching success : " + i.ToString());
                    break;
                }
                else
                {
                    WriteLog("Can not searching : " + i.ToString());
                }
            }
        }

        private void WriteLog(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");

                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        #endregion Methods
    }
}