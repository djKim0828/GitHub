using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BmpTest
{
    public partial class Form1 : Form
    {

        Form2 frm2;

        public Form1()
        {
            InitializeComponent();

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = OutputPath;
            openFileDialog.Filter = "Bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                pictureBox1.Image = Image.FromFile(filePath);
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);

            int bit = 24;

            int pixelSize = 24 / 8;

            int rowPixel = 256 * pixelSize;  // 한줄의 길이

            int drawRowLineCount = 50;

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.
            int max = rowPixel * drawRowLineCount + hearderSize;

            for (int i = hearderSize; i < max; i++)
            {
                if (i % 3 == 0)
                {
                    //B
                    imageByte[i] = 0x00;
                }
                else if (i % 3 == 1)
                {
                    //G
                    imageByte[i] = 0x00;
                }
                else if (i % 3 == 2)
                {
                    //R
                    imageByte[i] = 0xFF;
                }
            }
            
            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;

        }

        private void tbnEdge_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(pictureBox1.Image);
            int threshold = Convert.ToInt32(txtThreshold.Text);
            pictureBox2.Image = ImgProcessing.myEdge(temp, threshold);
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);
            
            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data = Convert.ToInt16(imageByte[i]);

                int value = 255 - data;

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        int bValue = 0;
        int cValue = 0;

        private void btnBPlus_Click(object sender, EventArgs e)
        {           
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            bValue = bValue + 10;

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data = Convert.ToInt16(imageByte[i]);
                int value = data + bValue;

                if (value > 255)
                {
                    value = 255;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;
        }

        private void btnBMinue_Click(object sender, EventArgs e)
        {
            
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            bValue = bValue - 10;

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data = Convert.ToInt16(imageByte[i]);    

                int value = data + bValue;

                if (value > 255)
                {
                    value = 255;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;
        }

        private void btnCPlus_Click(object sender, EventArgs e)
        {
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            cValue = cValue + 10;

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data = Convert.ToInt16(imageByte[i]);

                int value = data + (data -128) * cValue / 100;

                if (value > 255)
                {
                    value = 255;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;
        }

        private void btnCMinue_Click(object sender, EventArgs e)
        {
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            cValue = cValue - 10;

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data = Convert.ToInt16(imageByte[i]);

                int value = data + (data - 128) * cValue / 100;

                if (value > 255)
                {
                    value = 255;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox2.Image = temp;
        }

        private void btnForm2_Click(object sender, EventArgs e)
        {
            frm2 = new Form2();
            frm2.Show();
        }
    }
}
