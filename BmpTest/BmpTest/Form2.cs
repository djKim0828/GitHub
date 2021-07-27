using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmpTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);
            byte[] imageByte2 = ImgProcessing.ImageToByte(pictureBox2.Image);

            //byte[] imageByte3 = new byte[imageByte2.Length];

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data1 = Convert.ToInt16(imageByte[i]);

                int data2 = Convert.ToInt16(imageByte2[i]);

                int value = data1 + data2;

                if (value > 255)
                {
                    value = 255;
                }

                imageByte[i] = Convert.ToByte(value);

            }

            Bitmap temp = ImgProcessing.ByteToImage(imageByte);
            pictureBox3.Image = temp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] imageByte = ImgProcessing.ImageToByte(pictureBox1.Image);
            byte[] imageByte2 = ImgProcessing.ImageToByte(pictureBox2.Image);

            //byte[] imageByte3 = new byte[imageByte2.Length];

            int hearderSize = 54; // BMP는 24bit 이상인 경우에는 무조건 54byte의 헤더를 갖는다.            

            for (int i = hearderSize; i < imageByte.Length; i++)
            {

                int data1 = Convert.ToInt16(imageByte[i]);

                int data2 = Convert.ToInt16(imageByte2[i]);

                int value = data2 - data1;

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
            pictureBox3.Image = temp;
        }
    }
}
