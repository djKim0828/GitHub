using HsApiNet;
using LiveView.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveView
{
    public partial class Form1 : Form
    {
        //public ChartXY _cXy = new ChartXY();
        public ChartUV _cUv = new ChartUV();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Controls.Add(_cXy);
            this.Controls.Add(_cUv);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //_cXy.DrawPoint(0.8, 0.8);

            _cUv.DrawPoint(0.65, 0.65);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //_cXy.DrawPoint(0, 0);

            _cUv.DrawPoint(0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _cUv.DrawPoint(0.4, 0.4);
        }
    }    
}
