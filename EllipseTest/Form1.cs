using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EllipseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            this.pictureBox2.MouseMove += PictureBox2_MouseMove;
            this.pictureBox2.MouseLeave += PictureBox2_MouseLeave;
            this.pictureBox2.MouseClick += PictureBox2_MouseClick;
        }

        private void PictureBox2_MouseClick(object sender, MouseEventArgs e)
        {


            int x = (e.X - _remainPoint.X) / (_detailRectWidth + _interval);
            int y = (e.Y - _remainPoint.Y) / (_detailRectHeight + _interval);


            lblLed.Text = x.ToString() + "_" + y.ToString();
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;

            pictureBox2.Image = (Bitmap)pictureBox2.InitialImage.Clone();
        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawDetail == false)
            {
                return;
            }

            this.Cursor = Cursors.Cross;

            pictureBox2.Image = (Bitmap)pictureBox2.InitialImage.Clone();

            int width = (_detailRectWidth + _interval) * 15;
            int height = (_detailRectHeight + _interval) * 2;

            Rectangle rectTemp = new Rectangle(e.X, e.Y, width, height);
            Color drawColor = Color.FromArgb(255, 255, 255, 125);
            DrawDetailRetangle(rectTemp, drawColor);

        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Bitmap)pictureBox1.InitialImage.Clone();

            txtEllipse.Text = (Convert.ToInt16(txtWidth.Text) * 25).ToString();


            //보이는 원
            Rectangle originCircle = new Rectangle(0,
                                 0,
                                Convert.ToInt16(txtEllipse.Text),
                                Convert.ToInt16(txtEllipse.Text));

            DrawEllipse(originCircle, true, Color.White);            
            
            DrawRetanglesInSideCircle();
        }

        /// <summary>
        /// 원 그리기
        /// </summary>
        private void DrawEllipse(Rectangle circle, bool isFill, Color fillColor)
        {
            Graphics gfx = Graphics.FromImage(pictureBox1.Image);
            
            if (isFill == true)
            {
                SolidBrush iconBrush = new SolidBrush(fillColor);

                gfx.FillEllipse(iconBrush, circle);
            }
            else
            {
                //Pen pen = new Pen(Color.FromArgb(255, 0, 255, 0), 1);
                Pen pen = new Pen(fillColor, 1);
                gfx.DrawEllipse(pen, circle);
            }

            pictureBox1.Invalidate();
            pictureBox1.Refresh();
        }

        int _detailRectWidth = 0;
        int _detailRectHeight = 0;
        int _interval = 2; // 사이 간격
        Point _remainPoint;

        int _detailCol = 68;
        int _detailRow = 86;

        private void DrawDetailRects()
        {                                  

            // 간격을 포함하여 Rect를 그리기 위해 필요한 사이즈
            int reqWidth = (pictureBox2.Width - _detailCol * _interval);
            int reqHeight = (pictureBox2.Height - _detailRow * _interval);

            // 사각형의 가로/세로 길이 구하기
            _detailRectWidth = reqWidth / _detailCol;
            _detailRectHeight = reqHeight / _detailRow;

            // 그리고 남는 길이를 2로 나누어서 가운데에 그리도록 Remain 계산
            int drawTotalWidth = (_detailRectWidth + _interval) * _detailCol;
            int drawTotalHeight = (_detailRectHeight + _interval) * _detailRow;
            _remainPoint = new Point((pictureBox2.Width - drawTotalWidth) / 2,
                                    (pictureBox2.Height - drawTotalHeight) / 2);

            int count = 0;
            for (int i = 0; i < _detailRow; i++)
            {
                for (int j = 0; j < _detailCol; j++)
                {
                    int x = j * (_detailRectWidth + _interval) + _remainPoint.X;
                    int y = i * (_detailRectHeight + _interval) + _remainPoint.Y;

                    Rectangle rectTemp = new Rectangle(x,
                                                       y,
                                                       _detailRectWidth,
                                                       _detailRectHeight);

                    Color drawColor = Color.FromArgb(125, 255, 255, 255);
                    DrawDetailRetangle(rectTemp, drawColor);
                    count++;
                }
            }

            pictureBox2.InitialImage = (Bitmap)pictureBox2.Image.Clone();

            pictureBox2.Invalidate();
            pictureBox2.Refresh();
        }

        private void DrawDetailRetangle(Rectangle rect, Color color)
        {
            Graphics gfx = Graphics.FromImage(pictureBox2.Image);

            Pen pen = new Pen(color, 1);
            gfx.DrawRectangle(pen, rect);
        }

        private void DrawRetanglesInSideCircle()
        {
            int rectWidth = Convert.ToInt16(txtWidth.Text);
            int rectHeight = Convert.ToInt16(txtHeight.Text);

            int circleMargin = 0; // 일단 둘레 마진

            int circleDiameter = Convert.ToInt16(txtEllipse.Text) - circleMargin; // 마진을 뺀 원 크기
            int countX = circleDiameter / rectWidth;
            int countY = circleDiameter / rectHeight;

            Point remain = new Point();
            remain.X = circleDiameter - (countX * rectWidth);
            remain.Y = circleDiameter - (countY * rectHeight);

            // 마진 적용 원 : 마진을 구해서 센터를 기준으로 사각형이 그려지게한다.
            Rectangle circle = new Rectangle(remain.X / 2,
                                             remain.Y / 2,
                                            circleDiameter - remain.X,
                                            circleDiameter - remain.Y);

            //DrawEllipse(circle, false, Color.Blue);

            int drawCount = 0;

            for (int i = 0; i < countY; i++)
            {
                for (int j = 0; j < countX; j++)
                {
                    Rectangle rectTemp = new Rectangle(j * rectWidth + remain.X,
                       i * rectHeight + remain.Y,
                       rectWidth,
                       rectHeight);

                    if (isRectangleInsideCircle(rectTemp, circle) == false)
                    {
                        continue; ;
                    }

                    //DrawRetangle(rectTemp);
                    AddButton(rectTemp, i , j);

                    drawCount++;
                }
            }

            pictureBox1.Invalidate();
            pictureBox1.Refresh();

            lblTotalCount.Text = (countX * countY).ToString();
            lblResult.Text = drawCount.ToString();

        }

        private void DrawRetangle(Rectangle rect)
        {
            Graphics gfx = Graphics.FromImage(pictureBox1.Image);

            Pen pen = new Pen(Color.FromArgb(255, 255, 0, 0), 1);
            gfx.DrawRectangle(pen, rect);            
        }

        private void AddButton(Rectangle rect, int x, int y)
        {
            Button newButton = new Button();

            newButton.Width = rect.Width;
            newButton.Height = rect.Height;
            newButton.Location = rect.Location;
            newButton.Tag = "X:" + x.ToString() + ",Y:" + y.ToString();
            newButton.Click += NewButton_Click;

            this.pictureBox1.Controls.Add(newButton);

            //pictureBox1.Invalidate();
            //pictureBox1.Refresh();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button temp = (Button)sender;

                int canvasWidth = Convert.ToInt16(txtWidth.Text) * 25;
                int canvasHeght = Convert.ToInt16(txtHeight.Text) * 25;

                pictureBox2.Size = new System.Drawing.Size(canvasWidth, canvasHeght);
                Bitmap bm = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
                Graphics g = Graphics.FromImage(bm);
                g.Clear(Color.Black);

                pictureBox2.Image = bm;

                lblFOV.Text = temp.Tag.ToString();

                DrawDetailRects();

                isDrawDetail = true;
            }
            catch (System.Exception ex)
            {
            	
            }            
        }

        bool isRectangleInsideCircle(Rectangle rect, Rectangle circle)
        {
            bool isResult = false;

            // 4점을 구한다.
            Point leftTop = rect.Location;

            Point rightTop = new Point();
            rightTop.X = rect.Location.X + rect.Width;
            rightTop.Y = rect.Location.Y;

            Point leftButtom = new Point();
            leftButtom.X = rect.X;
            leftButtom.Y = rect.Y + rect.Height;

            Point rightButtom = new Point();
            rightButtom.X = rect.Location.X + rect.Width;
            rightButtom.Y = rect.Y + rect.Height;

            Point centerPoint = new Point();
            centerPoint.X = circle.X + circle.Width / 2;
            centerPoint.Y = circle.Y + circle.Height / 2;

            int halfLength = circle.Width / 2;

            if (Math.Pow((centerPoint.X - leftTop.X),2) + Math.Pow((centerPoint.Y - leftTop.Y),2) < Math.Pow(halfLength,2) == false)
            {
                return false;
            }

            if (Math.Pow((centerPoint.X - rightTop.X), 2) + Math.Pow((centerPoint.Y - rightTop.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            }

            if (Math.Pow((centerPoint.X - leftButtom.X), 2) + Math.Pow((centerPoint.Y - leftButtom.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            }

            if (Math.Pow((centerPoint.X - rightButtom.X), 2) + Math.Pow((centerPoint.Y - rightButtom.Y), 2) < Math.Pow(halfLength, 2) == false)
            {
                return false;
            }

            return true;


            //double fartherstX = max(fabs(R.minX - C.cX), fabs(R.maxX - C.cX));
            //double fartherstY = max(fabs(R.minY - C.cY), fabs(R.maxY - C.cY));
            //return Sqr(fartherstX) + Sqr(fartherstY) < Sqr(C.Radius);
        }


        bool isDrawDetail = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            btnDraw_Click(null, null);
        }
    }
}
