using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class frmMain : Form
    {
        #region Fields

        private Thread _CheckCountThread;
        private Config _config;
        private UInt32 _deviceId;
        private Int32 _driverOpen;
        private bool _isLoop = false;
        private String _path = Application.StartupPath + @"\config.json";

        private PmacCommnad _pmacCommand;

        #endregion Fields

        #region Constructors

        public frmMain()
        {
            InitializeComponent();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Todo : 테스트에 따라 아래를 사용
            //_deviceId = PMAC.PmacSelect(0);
            _deviceId = Convert.ToUInt16(txtDeviceIndex.Text);

            _driverOpen = PMAC.OpenPmacDevice(_deviceId);

            if (_driverOpen == 0)
            {
                WriteLog("장치가 OPEN되지 않았습니다..");
                return;
            }

            _pmacCommand = new PmacCommnad(_deviceId);
            _pmacCommand.LoggerMessage += _pmacCommand_LoggerMessage;

            tslStatus.BackColor = Color.Green;

            _CheckCountThread = new Thread(runCheckCount);
            _CheckCountThread.Start();

            WriteLog("장치가 OPEN되었습니다..");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();

            _isLoop = false;

            if (_CheckCountThread == null)
            {
                _CheckCountThread.Abort();
            } // else
                
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.ucMotionX.initControl("Motion X", _pmacCommand);
            this.ucMotionY.initControl("Motion Y", _pmacCommand);
            this.ucMotionZ.initControl("Motion Z", _pmacCommand);

            LoadConfig();
        }

        private void LoadConfig()
        {
            // 설정 읽어오기
            _config = JSON.Load(_path);

            if (_config == null)
            {
                SaveConfig();
            }

            txtDeviceIndex.Text = _config.DeviceIndex;

            ucMotionX.txtMosionIndex.Text = _config.MotionIndexX;
            ucMotionX.txtComPos.Text = _config.MotionCmdPosX;
            ucMotionX.txtRelPos.Text = _config.MotionRelPosX;

            ucMotionY.txtMosionIndex.Text = _config.MotionIndexY;
            ucMotionY.txtComPos.Text = _config.MotionCmdPosY;
            ucMotionY.txtRelPos.Text = _config.MotionRelPosY;

            ucMotionZ.txtMosionIndex.Text = _config.MotionIndexZ;
            ucMotionZ.txtComPos.Text = _config.MotionCmdPosZ;
            ucMotionZ.txtRelPos.Text = _config.MotionRelPosZ;
        }

        private void runCheckCount()
        {
            _isLoop = true;

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

                lblCount.Invoke(new MethodInvoker(() =>
                {
                    lblCount.Text = Convert.ToString(result);
                }));
            }
        }

        private void SaveConfig()
        {
            if (_config == null)
            {
                _config = new Config();
            }

            _config.DeviceIndex = txtDeviceIndex.Text;

            _config.MotionIndexX = ucMotionX.txtMosionIndex.Text;
            _config.MotionCmdPosX = ucMotionX.txtComPos.Text;
            _config.MotionRelPosX = ucMotionX.txtRelPos.Text;

            _config.MotionIndexY = ucMotionY.txtMosionIndex.Text;
            _config.MotionCmdPosY = ucMotionY.txtComPos.Text;
            _config.MotionRelPosY = ucMotionY.txtRelPos.Text;

            _config.MotionIndexZ = ucMotionZ.txtMosionIndex.Text;
            _config.MotionCmdPosZ = ucMotionZ.txtComPos.Text;
            _config.MotionRelPosZ = ucMotionZ.txtRelPos.Text;

            JSON.Save(_path, _config);
        }

        #endregion Methods
    }
}