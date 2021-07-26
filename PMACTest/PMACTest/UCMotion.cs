using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class UCMotion : UserControl
    {
        private PmacCommnad _pmacCommand;

        public UCMotion()
        {
            InitializeComponent();
        }

        public void initControl(string name, PmacCommnad pmacCommand)
        {
            this.groupBox1.Text = name;

            _pmacCommand = pmacCommand;
        }

        private void btnJogPlusX_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogPlus(Convert.ToInt32(txtMosionIndex.Text));
            } // else
        }

        private void btnJogPlusX_MouseUp(object sender, MouseEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(Convert.ToInt32(txtMosionIndex.Text));
            } // else
        }

        private void btnJogMinusX_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogMinus(Convert.ToInt32(txtMosionIndex.Text));
            } // else
        }

        private void btnJogMinusX_MouseUp(object sender, MouseEventArgs e)
        {
            Stop();
        }

        private void btnStopX_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.SetAbort(Convert.ToInt32(txtMosionIndex.Text));
            } // else
        }
    }
}
