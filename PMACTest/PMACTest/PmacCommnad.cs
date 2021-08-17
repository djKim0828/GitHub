using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PMACTest
{
    public class PmacCommnad
    {
        #region Fields

        public bool _isOpen = false;

        private UInt32 _DeviceId;

        #endregion Fields

        #region Constructors

        public PmacCommnad(UInt32 deviceId)
        {
            _DeviceId = deviceId;
        }

        #endregion Constructors

        #region Delegates

        public delegate void EventLoggerMessage(object sendor, EventLoggerMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventLoggerMessage LoggerMessage;

        #endregion Events

        #region Methods

        public bool command(string cmd, uint maxChar = 255)
        {
            WriteLog("start command : " + cmd);

            StringBuilder strResponse = new StringBuilder();
            int result = PMAC.PmacGetResponseA(_DeviceId, strResponse, maxChar,
                new StringBuilder(Convert.ToString(cmd)));

            if (strResponse.ToString() != "")
            {
                WriteLog("Failed command : " + cmd);
                return false;
            }

            WriteLog("Complete command : " + cmd);

            return true;
        }

        public bool command(byte[] cmd, uint maxChar = 255)
        {
            WriteLog("start command : " + cmd);

            byte[] output = new byte[0];
            int result = PMAC.PmacGetResponseA(_DeviceId, output, maxChar, cmd);

            if (output != null)
            {
                WriteLog("Failed command : " + cmd.ToString());
                return false;
            }

            WriteLog("Complete command");

            return true;
        }

        public string GetActPos(string axicNo, uint maxChar = 255)
        {
            string cmd = "M" + axicNo + "62";
            WriteLog("start command1 : " + cmd);

            StringBuilder strResponse = new StringBuilder();
            int result = PMAC.PmacGetResponseA(_DeviceId, strResponse, maxChar,
                new StringBuilder(Convert.ToString(cmd)));

            if (result == 0)
            {
                WriteLog("Failed command");
                return string.Empty;
            } // else

            double mxx62 = Convert.ToDouble(strResponse.ToString());

            cmd = "I" + axicNo + "08";
            WriteLog("start command 2: " + cmd);

            result = PMAC.PmacGetResponseA(_DeviceId, strResponse, maxChar,
                new StringBuilder(Convert.ToString(cmd)));

            if (result == 0)
            {
                WriteLog("Failed command");
                return string.Empty;
            } // else

            double ixx08 = Convert.ToDouble(strResponse.ToString());
            double actPos = mxx62 / (ixx08 * 32);

            WriteLog("Complete command");

            return actPos.ToString();
        }

        public void JogMinus(string axicNo)
        {
            string cmd = "#" + axicNo + "J-";
            command(cmd);
        }

        //string BEL = "0x07";
        public void JogPlus(string axicNo)
        {
            string cmd = "#" + axicNo + "J+";
            command(cmd);
        }

        public void JogStop(string axicNo)
        {
            string cmd = "#" + axicNo + "J/";
            command(cmd);
        }

        public void WriteLog(string msg)
        {
            LoggerMessage?.Invoke(this, new EventLoggerMessageArgs(msg));
            System.Windows.Forms.Application.DoEvents();
        }

        internal void AbsoluteMove(string axicNo, string pos)
        {
            string cmd = "#" + axicNo + "J=" + pos;

            command(cmd);
        }

        internal string GetCount()
        {
            StringBuilder strResponse = new StringBuilder();
            PMAC.PmacGetResponseA(_DeviceId, strResponse, 255, new StringBuilder(Convert.ToString("M8100")));

            return strResponse.ToString();
        }

        internal void GetMem(UInt32 offset, UInt32 length)
        {
            if (length <= 0)
            {
                return;
            } // else

            IntPtr ptrBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)length);

            //PmacDPRGetMem 함수 호출
            PMAC.PmacDPRGetMem(_DeviceId, offset, length, ptrBuffer);

            //Managed 동적영역 할당
            byte[] bBuffer = new byte[(length)];

            //Copy
            Marshal.Copy(ptrBuffer, bBuffer, 0, (int)length);

            string str2 = BitConverter.ToString(bBuffer).Replace("-", " ");

            WriteLog(str2);
        }

        internal void HomePick(string axicNo)
        {
            string cmd = "#" + axicNo + "HMZ";

            command(cmd);
        }

        internal void HomeSearch(string axicNo)
        {
            string cmd = "#" + axicNo + "HM";

            command(cmd);
        }

        internal void RelativeMove(string axicNo, int count, bool isPlus)
        {
            string cmd = "#" + axicNo + "J:";

            if (isPlus == true)
            {
                cmd += "+" + count.ToString();
            }
            else
            {
                cmd += "-" + count.ToString();
            }

            command(cmd);
        }

        internal void SetHomingProp(string axicNo,
                                    string jogTime,
                                    string acclTime,
                                    string sCurveTime,
                                    string homeSpeed,
                                    string offset)
        {
            command("I" + axicNo + CommandNumber.HomingJogTime + jogTime);
            command("I" + axicNo + CommandNumber.HomingAccelTime + acclTime);
            command("I" + axicNo + CommandNumber.HomingSCurveTime + sCurveTime);
            command("I" + axicNo + CommandNumber.HomingSpeed + homeSpeed);
            command("I" + axicNo + CommandNumber.HomingOffset + offset);

            //command("I" + axicNo.ToString() + "19" + jogTime);
            //command("I" + axicNo.ToString() + "20" + acclTime);
            //command("I" + axicNo.ToString() + "21" + sCurveTime);
            //command("I" + axicNo.ToString() + "23" + homeSpeed);
            //command("I" + axicNo.ToString() + "26" + offset);
        }

        internal void SetKill()
        {
            //DeviceGetResponse(m_dwDeviceNo, response, 255, "&1A");
            //command("&1A");

            // Kill 명령어 "^k"
            byte[] cmd = new byte[1];
            cmd[0] = 0x0B; // ctrl + k = 0x0B
            command(cmd);
        }

        internal void SetServoOff(string axicNo)
        {
            string cmd = "#" + axicNo + "o0";
            command(cmd);
        }

        internal void SetSpeed(string axicNo, string speed)
        {
            string cmd = "I" + axicNo + "22=" + speed;
            command(cmd);
        }

        #endregion Methods

        #region Classes

        public class CommandNumber
        {
            #region Fields

            public const string HomingAccelTime = "20";
            public const string HomingJogTime = "19";
            public const string HomingOffset = "26";
            public const string HomingSCurveTime = "21";
            public const string HomingSpeed = "23";

            #endregion Fields
        }

        public class EventLoggerMessageArgs : EventArgs
        {
            #region Constructors

            public EventLoggerMessageArgs(string message)
            {
                Initialize();

                Message = message;
            }

            #endregion Constructors

            #region Properties

            public string Message { get; protected set; }
            public DateTime OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}