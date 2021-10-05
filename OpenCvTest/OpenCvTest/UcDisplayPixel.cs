using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.CPlusPlus;

namespace OpenCvTest
{
    public partial class UcDisplayPixel : UserControl
    {
        public UcDisplayPixel()
        {
            InitializeComponent();
        }

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

        public void SetData(List<string[]> data)
        {
            int lblWidth = 90;
            int lblHeight = 20;

            pnlPixel.Controls.Clear();

            int rows = data.Count;
            int cols = data[0].Length;

            this.Width = cols * lblWidth;
            this.Height = rows * lblHeight;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {                    
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = lblWidth;
                    lb.Height = lblHeight;
                    lb.Left = lb.Width * j;
                    lb.Top = lb.Height * i;
                    lb.Name = "lbl" + i.ToString() + j.ToString();
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lb.BackColor = System.Drawing.Color.White;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                    lb.Font = new System.Drawing.Font("Arial", 10);
                    //lb.Text = pt.Item0.ToString() + " " + pt.Item1.ToString() + " " + pt.Item2.ToString();
                    lb.Text = data[i].GetValue(j).ToString();

                    pnlPixel.Controls.Add(lb);
                }
            }
        }
    }
}

