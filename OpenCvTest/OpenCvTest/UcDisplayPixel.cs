using OpenCvSharp.CPlusPlus;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenCvTest
{
    public partial class UcDisplayPixel : UserControl
    {
        #region Constructors

        public UcDisplayPixel()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public void SetAllData(Mat img)
        {
            pnlPixel.Controls.Clear();

            int width = 20 * img.Cols;
            int height = 10 * img.Rows;

            this.Width = width;
            this.Height = height;

            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    var pt = img.At<Vec3b>(i, j);

                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = 20;
                    lb.Height = 10;
                    lb.Left = lb.Width * j;
                    lb.Top = lb.Height * i;
                    lb.Name = "lbl" + i.ToString() + j.ToString();
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lb.BackColor = System.Drawing.Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    lb.Font = new System.Drawing.Font("Arial", 6);
                    //lb.Text = pt.Item0.ToString() + " " + pt.Item1.ToString() + " " + pt.Item2.ToString();
                    lb.Text = pt.Item1.ToString();

                    pnlPixel.Controls.Add(lb);
                }
            }
        }

        public void SetData(string[] data)
        {
            int rowCnt = 3;
            int colCnt = 3;
            int lblWidth = 90;
            int lblHeight = 20;

            pnlPixel.Controls.Clear();
            
            this.Width = lblWidth * colCnt;
            this.Height = lblHeight * rowCnt;

            for (int i = 0; i < rowCnt; i++)
            {                
                for (int j = 0; j < colCnt; j++)
                {
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = lblWidth;
                    lb.Height = lblHeight;
                    lb.Left = lb.Width * j;
                    lb.Top = lb.Height * i;
                    lb.Name = "lbl" + i.ToString();
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lb.BackColor = System.Drawing.Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    lb.Font = new System.Drawing.Font("Arial", 10);

                    string content = string.Empty;
                    content = data[i * rowCnt] + " " 
                            + data[i * rowCnt + 1] + " " 
                            + data[i * rowCnt + 2];

                    lb.Text = content.Trim();
                    pnlPixel.Controls.Add(lb);
                }
            }
        }

        #endregion Methods
    }
}