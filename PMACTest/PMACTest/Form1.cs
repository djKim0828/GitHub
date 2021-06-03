using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PMACTest
{
    public partial class Form1 : Form
    {
        #region Fields

        private bool _isLoop;
        private int _loopInterval = 1000;
        private Int32 m_bDriverOpen;
        private UInt32 m_dwDevice;

        #endregion Fields

        #region Constructors

        public Form1()
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

        private void btnJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            JogMinus(1);
        }

        private void btnJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(1);
        }

        private void btnJogMinus2_MouseDown(object sender, MouseEventArgs e)
        {
            JogMinus(2);
        }

        private void btnJogMinus2_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(2);
        }

        private void btnJogMinus3_MouseDown(object sender, MouseEventArgs e)
        {
            JogMinus(3);
        }

        private void btnJogMinus3_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(3);
        }

        private void btnJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            JogPlus(1);
        }

        private void btnJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(1);
        }

        private void btnJogPlus2_MouseDown(object sender, MouseEventArgs e)
        {
            JogPlus(2);
        }

        private void btnJogPlus2_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(2);
        }

        private void btnJogPlus3_MouseDown(object sender, MouseEventArgs e)
        {
            JogPlus(3);
        }

        private void btnJogPlus3_MouseUp(object sender, MouseEventArgs e)
        {
            JogStop(3);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            m_dwDevice = PMAC.PmacSelect(0);
            m_bDriverOpen = PMAC.OpenPmacDevice(m_dwDevice);

            if (m_bDriverOpen == 0)
            {
                WriteLog("장치가 OPEN되지 않았습니다..");
                return;
            }

            WriteLog("장치가 OPEN되었습니다..");
        }

        private void command(string cmd)
        {
            StringBuilder strResponse = new StringBuilder();
            PMAC.PmacGetResponseA(m_dwDevice, strResponse, 255,
                new StringBuilder(Convert.ToString(cmd)));

            WriteLog(cmd);

            if (strResponse.ToString() != "")
            {
                int aaa = 0;
            }
        }

        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                try
                {
                    //Random rd = new Random();
                    //int temp = rd.Next(0, 1000);
                    //WriteLog("Test : " + temp.ToString());

                    Thread.Sleep(_loopInterval);
                }
                catch (System.Exception ex)
                {
                    WriteLog(ex.ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteLog("Load Program");
            //btnOpen_Click(null, null);

            _isLoop = false;
            ThreadPool.QueueUserWorkItem(EmWorksProc);
        }

        private void JogMinus(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J-";
            command(cmd);
        }

        //string BEL = "0x07";
        private void JogPlus(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J+";
            command(cmd);
        }

        private void JogStop(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J/";
            command(cmd);
        }

        #endregion Methods
    }
}