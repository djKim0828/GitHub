using System;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class UCMotion : UserControl
    {
        #region Fields

        private PmacCommnad _pmacCommand;

        #endregion Fields

        #region Constructors

        public UCMotion()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public void initControl(string name, PmacCommnad pmacCommand)
        {
            this.groupBox1.Text = name;

            _pmacCommand = pmacCommand;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.SetKill();
            } // else
        }

        private void btnGetPos_Click(object sender, EventArgs e)
        {
            if (_pmacCommand != null)
            {
                string actPos = _pmacCommand.GetActPos(txtMosionIndex.Text);
                lblMotorPosValue.Text = actPos;
            } // else
        }

        private void btnJogMinusX_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogMinus(txtMosionIndex.Text);
            } // else
        }

        private void btnJogMinusX_MouseUp(object sender, MouseEventArgs e)
        {
            Stop();
        }

        private void btnJogPlusX_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogPlus(txtMosionIndex.Text);
            } // else
        }

        private void btnJogPlusX_MouseUp(object sender, MouseEventArgs e)
        {
            Stop();
        }

        private void btnMoveX_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pmacCommand != null)
                {
                    _pmacCommand.AbsoluteMove(txtMosionIndex.Text,
                                                txtAbsPos.Text);
                } // else
            }
            catch (System.Exception ex)
            {
                _pmacCommand.WriteLog("exception : " + ex.ToString());
            }
        }

        private void btnRMoveMinueX_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pmacCommand != null)
                {
                    int count = Convert.ToInt32(txtCont.Text) * Convert.ToInt32(txtCnt.Text);

                    _pmacCommand.RelativeMove(txtMosionIndex.Text, count, false);
                } // else
            }
            catch (System.Exception ex)
            {
                _pmacCommand.WriteLog("exception : " + ex.ToString());
            }
        }

        private void btnRMovePlueX_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pmacCommand != null)
                {
                    int count = Convert.ToInt32(txtCont.Text) * Convert.ToInt32(txtCnt.Text);

                    _pmacCommand.RelativeMove(txtMosionIndex.Text, count, true);
                } // else
            }
            catch (System.Exception ex)
            {
                _pmacCommand.WriteLog("exception : " + ex.ToString());
            }
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.SetServoOff(txtMosionIndex.Text);
            } // else
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.SetSpeed(txtMosionIndex.Text, txtSpeed.Text);
            } // else
        }

        private void btnStopX_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(txtMosionIndex.Text);
            } // else
        }

        #endregion Methods
    }
}