using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class FrmTest : Form
    {
        PmacCommnad _pmacCommand;

        public FrmTest()
        {
            InitializeComponent();

            initInstance();

            initControl();
        }

        private void initInstance()
        {
            
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

            cmbDeviceId.Items.Clear();

            for (int i = 1;i<4;i++)
            {
                cmbDeviceId.Items.Add(i.ToString());
            }

            cmbDeviceId.SelectedIndex = 1;


        }

        private void FrmTest_Load(object sender, EventArgs e)
        {

        }

        private Int32 _driverOpen;
        private UInt32 _deviceId;

        private void btnJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogPlus(Convert.ToInt32(cmbDeviceId.SelectedItem));
            }            
        }

        private void btnJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(Convert.ToInt32(cmbDeviceId.SelectedItem));
            }
        }
        private void btnJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogStop(Convert.ToInt32(cmbDeviceId.SelectedItem));
            }
        }

        private void btnJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_pmacCommand != null)
            {
                _pmacCommand.JogMinus(Convert.ToInt32(cmbDeviceId.SelectedItem));
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

            tslStatus.BackColor = Color.Green;
            

            WriteLog("장치가 OPEN되었습니다..");
        }

        private void _pmacCommand_LoggerMessage(object sendor, PmacCommnad.EventLoggerMessageArgs e)
        {
            WriteLog(e.Message);
        }

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

        private void btnSearchHome_Click(object sender, EventArgs e)
        {
            _pmacCommand.HomeSearch(Convert.ToInt32(cmbDeviceId.SelectedItem));
            //DeviceGetVariableLong(m_dwDeviceNo, 'M', 237, 0) == 1
            //DeviceGetResponse(m_dwDeviceNo, chrResponse, 255, "P345=1 ENAPLC13");
            //Sleep(10);
        }

        private void btnMove1_Click(object sender, EventArgs e)
        {
            
            _pmacCommand.Move(Convert.ToInt32(cmbDeviceId.SelectedItem));

        }

        private void btnAbort_Click(object sender, EventArgs e)
        {

            _pmacCommand.SetAbort(Convert.ToInt32(cmbDeviceId.SelectedItem));
            //DeviceGetResponse(m_dwDeviceNo, response, 255, "&1A");




        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (_driverOpen == 0)
                return;

            UInt32 offset = 0;// Convert.ToUInt32(string_offset, 16);
            UInt32 length = 1; // Convert.ToUInt32(string_length, 10);

            _pmacCommand.GetMem(offset, length);




        }
    }
}
