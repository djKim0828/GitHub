using System;
using System.Drawing;
using System.Windows.Forms;

namespace StEm
{
    public partial class Form1 : Form
    {
        #region Fields

        public int a;
        private Config _config;
        private String _path = Application.StartupPath + @"\config.json";
        private SerialControl _serial;

        #endregion Fields

        #region Constructors

        public Form1(string path)
        {
            _path = path;

            InitializeComponent();

            Initial();
        }

        #endregion Constructors

        #region Destructors

        ~Form1()
        {
        }

        #endregion Destructors

        #region Methods

        private void _serial_LogMessage(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        private void _serial_ReceiveMessage(string message)
        {
            for (int i = 0; i < dgv1.RowCount - 1; i++)
            {
                if (dgv1.Rows[i].Cells[0].Value.ToString() == message)
                {
                    string sendMessage = dgv2.Rows[i].Cells[0].Value.ToString();
                    _serial.Send(sendMessage, chkIsHex.Checked);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int result = _serial.Open(txtPort.Text, txtBarurate.Text, txtNewLine.Text);

            if (result == SerialControl.result.True)
            {
                tslStatus.BackColor = Color.Green;
            } // else
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _serial.Send(txtSendData.Text, chkIsHex.Checked);
        }

        private void Initial()
        {
            // 인스턴스 초기화
            _serial = new SerialControl();
            _config = new Config();
            _serial.ReceiveMessage += _serial_ReceiveMessage;
            _serial.LogMessage += _serial_LogMessage;

            // UI 초기화
            txtPort.Text = string.Empty;
            txtBarurate.Text = string.Empty;

            LoadConfig();
        }

        private void LoadConfig()
        {
            // 설정 읽어오기
            _config = JSON.Load(_path);
            if (_config != null)
            {
                txtPort.Text = _config.Port;
                txtBarurate.Text = _config.Bardrate;
                txtSendData.Text = _config.sendData;
                txtNewLine.Text = _config.newLine;

                chkIsHex.Checked = _config.isHex;

                if (_config.receiveDatas != null)
                {
                    for (int i = 0; i < _config.receiveDatas.Length - 1; i++)
                    {
                        dgv1.Rows.Add(_config.receiveDatas[i]);
                    }
                }

                if (_config.sendDatas != null)
                {
                    for (int i = 0; i < _config.sendDatas.Length - 1; i++)
                    {
                        dgv2.Rows.Add(_config.sendDatas[i]);
                    }
                }
            }
        }

        private void SaveConfig()
        {
            if (_config == null)
            {
                _config = new Config();
            }

            _config.Port = txtPort.Text;
            _config.Bardrate = txtBarurate.Text;
            _config.sendData = txtSendData.Text;

            _config.isHex = chkIsHex.Checked;
            _config.receiveDatas = new string[0];
            _config.sendDatas = new string[0];

            _config.receiveDatas = new string[dgv1.Rows.Count];
            for (int i = 0; i < dgv1.Rows.Count - 1; i++)
            {
                _config.receiveDatas[i] = dgv1.Rows[i].Cells[0].Value.ToString();
            }

            _config.sendDatas = new string[dgv2.Rows.Count];
            for (int i = 0; i < dgv2.Rows.Count - 1; i++)
            {
                _config.sendDatas[i] = dgv2.Rows[i].Cells[0].Value.ToString();
            }

            JSON.Save(_path, _config);
        }

        #endregion Methods
    }
}