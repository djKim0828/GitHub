using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PMACTest
{
    public class PmacCommnad
    {
        public bool _isOpen = false;

        private UInt32 _DeviceId;

        public delegate void EventLoggerMessage(object sendor, EventLoggerMessageArgs e);

        public event EventLoggerMessage LoggerMessage;

        public PmacCommnad(UInt32 deviceId)
        {
            _DeviceId = deviceId;
        }

        public bool command(string cmd,uint maxChar= 255)
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

            //if (result != 0)
            //{
            //    return false;
            //}
            
        }


        public void JogMinus(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J-";
            command(cmd);
        }

        //string BEL = "0x07";
        public void JogPlus(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J+";
            command(cmd);
        }

        public void JogStop(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "J/";
            command(cmd);
        }

        internal string GetCount()
        {
            StringBuilder strResponse = new StringBuilder();
            PMAC.PmacGetResponseA(_DeviceId, strResponse, 255, new StringBuilder(Convert.ToString("M8100")));

            return strResponse.ToString();

        }

        public void WriteLog(string msg)
        {
            LoggerMessage?.Invoke(this, new EventLoggerMessageArgs(msg));
            System.Windows.Forms.Application.DoEvents();
        }

        public class EventLoggerMessageArgs : EventArgs
        {

            public EventLoggerMessageArgs(string message)
            {
                Initialize();

                Message = message;
            }

            public string Message { get; protected set; }
            public DateTime OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

        }

        internal void SetAbort(int v)
        {
            //DeviceGetResponse(m_dwDeviceNo, response, 255, "&1A");
            command("&1A");
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

        internal void Move(int v)
        {
            //DeviceGetResponse(m_dwDeviceNo, response, 255, "P8000=0");
        }

        internal void HomeSearch(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "HM";

            command(cmd);
        }

        internal void SetHomingProp(int axicNo,
                                    string jogTime,
                                    string acclTime,
                                    string sCurveTime,
                                    string homeSpeed,
                                    string offset)
        {
            command("I" + axicNo.ToString() + CommandNumber.HomingJogTime + jogTime);
            command("I" + axicNo.ToString() + CommandNumber.HomingAccelTime + acclTime);
            command("I" + axicNo.ToString() + CommandNumber.HomingSCurveTime + sCurveTime);
            command("I" + axicNo.ToString() + CommandNumber.HomingSpeed + homeSpeed);
            command("I" + axicNo.ToString() + CommandNumber.HomingOffset + offset);

            //command("I" + axicNo.ToString() + "19" + jogTime);
            //command("I" + axicNo.ToString() + "20" + acclTime);
            //command("I" + axicNo.ToString() + "21" + sCurveTime);
            //command("I" + axicNo.ToString() + "23" + homeSpeed);
            //command("I" + axicNo.ToString() + "26" + offset);
        }


        public class CommandNumber
        {
            public const string HomingJogTime = "19";
            public const string HomingAccelTime = "20";
            public const string HomingSCurveTime = "21";
            public const string HomingSpeed = "23";
            public const string HomingOffset = "26";

        }

        internal void HomePick(int axicNo)
        {
            string cmd = "#" + axicNo.ToString() + "HMZ";

            command(cmd);
        }
    }
}
