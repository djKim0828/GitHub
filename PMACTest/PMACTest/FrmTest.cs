using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class FrmTest : Form
    {
        #region Fields

        private Thread _CheckCountThread;
        private UInt32 _deviceId;
        private Int32 _driverOpen;
        private bool _isLoop = false;
        private PmacCommnad _pmacCommand;

        #endregion Fields

        #region Constructors

        public FrmTest()
        {
            InitializeComponent();

            initInstance();

            initControl();
        }

        #endregion Constructors

        #region Methods

        public void WriteLog(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        private void _pmacCommand_LoggerMessage(object sendor, PmacCommnad.EventLoggerMessageArgs e)
        {
            WriteLog(e.Message);
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            _pmacCommand.SetKill();
        }

        private void btnHomePick_Click(object sender, EventArgs e)
        {
            _pmacCommand.HomePick(cmbAxicId.SelectedItem.ToString());
        }

        private void btnJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogMinus(cmbAxicId.SelectedItem.ToString());
            }
        }

        private void btnJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(cmbAxicId.SelectedItem.ToString());
            }
        }

        private void btnJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogPlus(cmbAxicId.SelectedItem.ToString());
            }
        }

        private void btnJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(cmbAxicId.SelectedItem.ToString());
            }
        }
        

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Todo : 테스트에 따라 아래를 사용
            //_deviceId = PMAC.PmacSelect(0);
            _deviceId = 0;

            _driverOpen = PMAC.OpenPmacDevice(_deviceId);

            if (_driverOpen == 0)
            {
                WriteLog("장치가 OPEN되지 않았습니다..");
                return;
            }

            _pmacCommand = new PmacCommnad(_deviceId);
            _pmacCommand.LoggerMessage += _pmacCommand_LoggerMessage;

            _pmacCommand._isOpen = true;
            tslStatus.BackColor = Color.Green;

            WriteLog("장치가 OPEN되었습니다..");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (_driverOpen == 0)
                return;

            UInt32 offset = 0;// Convert.ToUInt32(string_offset, 16);
            UInt32 length = 1; // Convert.ToUInt32(string_length, 10);

            _pmacCommand.GetMem(offset, length);
        }

        private void btnSearchHome_Click(object sender, EventArgs e)
        {
            _pmacCommand.SetHomingProp(cmbAxicId.SelectedItem.ToString(),
                                       txtJogAccl.Text,
                                       txtAcclTime.Text,
                                       txtSCurvetime.Text,
                                       txtHoimngOffset.Text,
                                       txtHomingSpeed.Text
                                       );
            _pmacCommand.HomeSearch(cmbAxicId.SelectedItem.ToString());
        }

        private void FrmTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isLoop = false;

            _CheckCountThread.Abort();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            _isLoop = true;
            _CheckCountThread = new Thread(runCheckCount);
            _CheckCountThread.Start();
        }

        private void initControl()
        {
            dgvIo.Rows.Clear();

            for (int i = 1; i < 25; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgvIo.Rows[0].Clone();
                row.Cells[0].Value = i.ToString();
                row.Cells[1].Value = i.ToString();

                dgvIo.Rows.Add(row);
            }

            cmbAxicId.Items.Clear();

            for (int i = 1; i < 4; i++)
            {
                cmbAxicId.Items.Add(i.ToString());
            }

            cmbAxicId.SelectedIndex = 1;
        }

        private void initInstance()
        {
        }

        private void runCheckCount()
        {
            while (_isLoop)
            {
                Thread.Sleep(1000);

                if (_pmacCommand == null)
                {
                    continue;
                }

                if (_pmacCommand._isOpen == false)
                {
                    continue;
                }

                string result = _pmacCommand.GetCount();

                lblMotorPosValue.Invoke(new MethodInvoker(() =>
                {
                    lblMotorPosValue.Text = Convert.ToString(result);
                }));
            }
        }

        #endregion Methods

        private void button1_Click(object sender, EventArgs e)
        {
            _pmacCommand.command("#1o0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _pmacCommand.command("^k");
        }
    }
}