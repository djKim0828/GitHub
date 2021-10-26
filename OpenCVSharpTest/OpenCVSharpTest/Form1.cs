using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpTest
{
    public partial class Form1 : Form
    {
        private Mat _srcImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dlg.FileName);
            } // else
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

        private void btndivide_Click(object sender, EventArgs e)
        {
            RunDivide(Convert.ToInt16(cmbH.Text), Convert.ToInt16(cmbV.Text));
        }

        private void RunDivide(short h, short v)
        {
            pnlImages.Controls.Clear();

            int pbWidth = _srcImage.Width / h;
            int pbHeight = _srcImage.Height / v;

            int pnlWidth = 0;
            int pnlHeight = 0;

            int lineThickness = 2;

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

                    Rect rect = new Rect(pbWidth * j,
                                         pbHeight * i,
                                         pbWidth,
                                        pbHeight);

                    pb.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_srcImage.SubMat(rect));
                    pnlImages.Controls.Add(pb);

                    pnlWidth += pbWidth + lineThickness;
                    pnlHeight += pbHeight + lineThickness;
                }
            }

            pnlImages.Width = pnlWidth / v;
            pnlImages.Height = pnlHeight / h;
           
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

            Cv2.InRange(_srcImage, new Scalar(lowB, lowG, lowR),
                        new Scalar(uppB, uppG, uppR), yellow);

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
                            new Scalar(0, 0, 255),   // 라인색
                            1,   // 라인 굵기
                            OpenCvSharp.LineType.AntiAlias,
                            null,
                            1);

            pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);
        }

        private void SearchContours0(Mat yellow, out OpenCvSharp.CPlusPlus.Point[][] contours)
        {

            HierarchyIndex[] hierarchy;

            Cv2.FindContours(yellow, out contours, out hierarchy, OpenCvSharp.ContourRetrieval.External,
                OpenCvSharp.ContourChain.ApproxTC89KCOS);

            if (contours.Length > 1)
            {
                return;
            } // else            

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
                    break;
                } // else
            }
        }
    }
}
