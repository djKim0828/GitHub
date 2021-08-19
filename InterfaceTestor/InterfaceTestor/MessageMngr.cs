using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTestor
{
    public class MessageMngr
    {
        public const int WM_USER = 0x400;
        public const int WM_COPYDATA = 0x4A;

        // ENC -> Cubixel
        public const int WM_START = WM_USER + 1000;

        // Cubixel -> ENC
        public const int WM_CUBIEVENT1 = WM_USER + 5000;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);        
    }
}
