using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCvTest
{
    public partial class frmDetail : Form
    {
        public frmDetail(string title, string[] data1, string[] data2)
        {
            InitializeComponent();

            ucDisplayPixel1.SetData(data1);
            ucDisplayPixel2.SetData(data2);

            this.Text = title;

            this.KeyPreview = true;
            this.KeyDown += FrmDetail_KeyDown;
        }

        private void FrmDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmDetail_Load(object sender, EventArgs e)
        {
            this.Height = ucDisplayPixel1.Height + 80;
        }
        

    }
}
