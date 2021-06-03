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

        private UInt32 _DeviceId;

        public delegate void EventLoggerMessage(object sendor, EventLoggerMessageArgs e);

        public event EventLoggerMessage LoggerMessage;

        public PmacCommnad(UInt32 deviceId)
        {
            _DeviceId = deviceId;
        }

        public void command(string cmd,uint maxChar= 255)
        {
            StringBuilder strResponse = new StringBuilder();
            PMAC.PmacGetResponseA(_DeviceId, strResponse, maxChar,
                new StringBuilder(Convert.ToString(cmd)));

            WriteLog(cmd);

            if (strResponse.ToString() != "")
            {
                //int aaa = 0;
            }
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

        internal void HomeSearch(int v)
        {
            //command("M", 239");
        }
    }
}
