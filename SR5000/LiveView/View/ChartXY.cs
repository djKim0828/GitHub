using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveView.View
{
    public partial class ChartXY : UserControl
    {
        public ChartXY()
        {
            InitializeComponent();
            
            this.pnlX.Visible = false;
            this.pnlY.Visible = false;
        }

        public void DrawPoint(double x, double y)
        {
            pictureBox.Image = (Image)pictureBox.InitialImage.Clone();
            Graphics gfx = Graphics.FromImage(pictureBox.Image);

            int pointSize = 3;
            Pen pen = new Pen(Color.FromArgb(180, 0, 0, 255), pointSize);

            Point p = new Point();

            // 0점
            int zeroPointX = pnlX.Left;
            int zeroPointY = pnlY.Top + pnlY.Height;

            int AxisCntX = 8;
            int AxisCntY = 9;

            double pointX = (pnlX.Width / AxisCntX) * x;
            double pointY = (pnlY.Height / AxisCntY) * y;
            
            p.X = zeroPointX + Convert.ToInt32(pointX * 10);
            p.Y = zeroPointY - Convert.ToInt32(pointY * 10);

            Rectangle rectTemp = new Rectangle(p.X - 1, p.Y - 1, pointSize, pointSize);
            gfx.DrawEllipse(pen, rectTemp);

            pictureBox.Invalidate();
            pictureBox.Refresh();
        }

    }
}
