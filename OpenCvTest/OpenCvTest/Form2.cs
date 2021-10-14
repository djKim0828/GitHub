using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenCvTest
{
    public partial class Form2 : Form
    {
        #region Fields

        private Config _config;

        private String _path = Application.StartupPath + @"\config2.json";

        private List<Spot> _spotList;
        private Mat _srcImage;

        #endregion Fields

        #region Constructors

        public Form2()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void btnDivide_Click(object sender, EventArgs e)
        {
            RunDivide(Convert.ToInt16(cmbH.Text), Convert.ToInt16(cmbV.Text));
        }

        private void btnInspection_Click(object sender, EventArgs e)
        {
            if (_spotList == null)
            {
                WriteLog("Please, Divide Image");
                return;
            }

            for (int i = 0; i < _spotList.Count; i++)
            {
                if (_spotList[i].isSearch == true)
                {
                    Inspection(_spotList[i]);
                } // else
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dlg.FileName);

                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_spotList == null)
            {
                WriteLog("Please, Divide Image");
                return;
            }

            for (int i = 0; i < _spotList.Count; i++)
            {
                bool isSearch = SearchLed(_spotList[i]);

                if (isSearch == true)
                {
                    WriteLog("******************* Successful Searched - " + i.ToString());
                    _spotList[i].isSearch = true;
                }
                else
                {
                    WriteLog("******************* Failed to Search - " + i.ToString());
                    _spotList[i].resultLable.BackColor = System.Drawing.Color.Red;
                    _spotList[i].resultLable.Text = "FAILED";

                    _spotList[i].isSearch = false;
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();

            LoadConfig();

            LoadImage("RGB2.png");
        }

        private void GetPixelData(Spot spot, Mat img, int rowIndex, int colIndex)
        {
            var pt = img.At<Vec3b>(rowIndex, colIndex);

            spot.ptList.Add(pt);

            pt.Item0 = (byte)(255 - pt.Item0);
            pt.Item1 = (byte)(255 - pt.Item1);
            pt.Item2 = (byte)(255 - pt.Item2);

            img.Set<Vec3b>(rowIndex, colIndex, pt);
        }

        private void Inspection(Spot spot)
        {
            Mat tempImage = spot.img;

            int rowCenter = tempImage.Rows / 2;
            int colQurd = tempImage.Cols / 4;

            spot.ptList = new List<Vec3b>();

            for (int i = 1; i < 4; i++)
            {
                if (i == 2)
                {
                    continue;
                } // else

                int rowIndex = rowCenter;
                int colIndex = colQurd * i;

                GetPixelData(spot, tempImage, rowIndex - 1, colIndex - 1);
                GetPixelData(spot, tempImage, rowIndex - 1, colIndex);
                GetPixelData(spot, tempImage, rowIndex - 1, colIndex + 1);

                GetPixelData(spot, tempImage, rowIndex, colIndex - 1);
                GetPixelData(spot, tempImage, rowIndex, colIndex);
                GetPixelData(spot, tempImage, rowIndex, colIndex + 1);

                GetPixelData(spot, tempImage, rowIndex + 1, colIndex - 1);
                GetPixelData(spot, tempImage, rowIndex + 1, colIndex);
                GetPixelData(spot, tempImage, rowIndex + 1, colIndex + 1);

                spot.isInspection = true;
            }

            spot.pictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(tempImage);
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

        private void LoadImage(string imageFileName)
        {
            if (_srcImage != null)
            {
                _srcImage.Release();
                _srcImage = null;

                GC.Collect();
                System.Threading.Thread.Sleep(100);
            }

            _srcImage = Cv2.ImRead(imageFileName);
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage);
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            Control temp = (Control)sender;

            String strIndex = temp.Name.Substring(2, temp.Name.Length - 2);
            int index = Convert.ToInt16(strIndex);
            if (_spotList[index].isInspection == false)
            {
                return;
            } // else

            int dataHalfCnt = _spotList[Convert.ToInt16(index)].ptList.Count / 2;
            string[] data1 = new string[dataHalfCnt * 3];

            for (int i = 0; i < dataHalfCnt; i++)
            {
                data1[i * 3] = _spotList[Convert.ToInt16(index)].ptList[i].Item0.ToString();
                data1[i * 3 + 1] = _spotList[Convert.ToInt16(index)].ptList[i].Item1.ToString();
                data1[i * 3 + 2] = _spotList[Convert.ToInt16(index)].ptList[i].Item2.ToString();
            }

            string[] data2 = new string[dataHalfCnt * 3];
            for (int i = 0; i < dataHalfCnt; i++)
            {
                data2[i * 3] = _spotList[Convert.ToInt16(index)].ptList[dataHalfCnt + i].Item0.ToString();
                data2[i * 3 + 1] = _spotList[Convert.ToInt16(index)].ptList[dataHalfCnt + i].Item1.ToString();
                data2[i * 3 + 2] = _spotList[Convert.ToInt16(index)].ptList[dataHalfCnt + i].Item2.ToString();
            }

            frmDetail f = new frmDetail("Spot Detail Data - Index : " + strIndex, data1, data2);
            f.Show();
        }

        private void RunDivide(short h, short v)
        {
            pnlImages.Controls.Clear();
            _spotList = new List<Spot>();

            int pbWidth = _srcImage.Width / h;
            int pbHeight = _srcImage.Height / v;

            int pnlWidth = 0;
            int pnlHeight = 0;

            int lineThickness = 4;

            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Name = "pb" + (h * i + j).ToString();
                    pb.Width = pbWidth;
                    pb.Height = pbHeight;
                    pb.Left = pbWidth * j + (lineThickness * j);
                    pb.Top = pbHeight * i + (lineThickness * i);
                    pb.Click += Pb_Click;

                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = pb.Width;
                    lb.Height = pb.Height;
                    lb.Left = pb.Left;
                    lb.Top = pb.Top;
                    lb.Name = "lbl" + i.ToString() + j.ToString();
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lb.BackColor = System.Drawing.Color.Gray;
                    pnlResult.Controls.Add(lb);

                    Rect rect = new Rect(pbWidth * j,
                                         pbHeight * i,
                                         pbWidth,
                                        pbHeight);

                    Spot sp = new Spot();

                    sp.img = _srcImage.SubMat(rect);
                    sp.pictureBox = pb;
                    sp.resultLable = lb;

                    _spotList.Add(sp);

                    pb.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(sp.img);
                    pnlImages.Controls.Add(pb);

                    pnlWidth += pbWidth + lineThickness;
                    pnlHeight += pbHeight + lineThickness;
                }
            }

            pnlImages.Width = pnlWidth / v;
            pnlImages.Height = pnlHeight / h;

            pnlResult.Width = pnlWidth / v;
            pnlResult.Height = pnlHeight / h;
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

        private void SearchContours(Mat yellow, out OpenCvSharp.CPlusPlus.Point[][] contours)
        {
            WriteLog("Start SearchContours");

            HierarchyIndex[] hierarchy;

            Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.External,
                OpenCvSharp.ContourChain.ApproxTC89KCOS);

            if (contours.Length > 0)
            {
                return;
            }
            else
            {
                WriteLog("Failed to searching with default");
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
                    WriteLog("Failed to searching with : " + i.ToString());
                }
            }

            WriteLog("Failed to Search");
        }

        private bool SearchLed(Spot spot)
        {
            Mat yellow = new Mat();
            Mat dst = spot.img.Clone();

            OpenCvSharp.CPlusPlus.Point[][] contours;

            int lowR = Convert.ToInt16(txtLowR.Text);
            int lowG = Convert.ToInt16(txtLowG.Text);
            int lowB = Convert.ToInt16(txtLowB.Text);

            int uppR = Convert.ToInt16(txtUppR.Text);
            int uppG = Convert.ToInt16(txtUppG.Text);
            int uppB = Convert.ToInt16(txtUppB.Text);

            Cv2.InRange(spot.img, new Scalar(lowB, lowG, lowR),
                        new Scalar(uppB, uppG, uppR), yellow);

            SearchContours(yellow, out contours);

            if (contours.Length < 1)
            {
                return false;
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
                            new Scalar(0, 0, 255),   // 라인색
                            1,   // 라인 굵기
                            OpenCvSharp.LineType.AntiAlias,
                            null,
                            1);

            //string a = spot.pictureBox.Name;
            spot.pictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);

            return true;
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