using System;
using System.Runtime.InteropServices;

namespace AppWpf
{
    public class MessageMngr
    {
        #region Fields

        // ENC -> Cubixel
        public const int START = WM_USER + 1000;

        public const int WM_COPYDATA = 0x4A;

        // Cubixel -> ENC
        public const int WM_CUBIEVENT1 = WM_USER + 5000;

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