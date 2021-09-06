using System;
using System.Runtime.InteropServices;

namespace AppWpf
{
    public class MessageMngr
    {
        #region Fields

        // ENC -> Cubixel
        public const int START = WM_USER + 1;
        public const int CONNECT = WM_USER + 2;
        public const int DISCONNECT = WM_USER + 3;
        public const int GET_STATUS = WM_USER + 4;
        public const int PREPARE_SCAN = WM_USER + 5;
        public const int LASER_ON_OFF = WM_USER + 6;
        public const int LED_ON_OFF = WM_USER + 7;
        public const int GRAP_START = WM_USER + 8;
        public const int GRAP_STOP = WM_USER + 9;
        public const int SCAN_START = WM_USER + 10;
        public const int SCAN_STOP = WM_USER + 11;
        public const int SAVE = WM_USER + 12;
        public const int SAVE_PARAMETERS = WM_USER + 13;
        public const int LOAD_PARAMETERS = WM_USER + 14;
        public const int SAVE_ZCALPARAM = WM_USER + 15;

        public const int WM_COPYDATA = 0x004A;

        // Cubixel -> ENC
        public const int RETURN = WM_USER + 240;
        public const int RETURN_STATUS = WM_USER + 241; //F1
        public const int RETURN_VALUE = WM_USER + 242; //F2
        public const int CAPTURE_COMPLETE = WM_USER + 243;
        public const int ERROR_EVENT = WM_USER + 244;

        public const int WM_USER = 0x400;

        #endregion Fields

        #region Methods

        // 1024
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        #endregion Methods

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        #endregion Structs
    }
}