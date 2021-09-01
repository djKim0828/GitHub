using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AppWpf
{
    public class Func
    {
        #region Fields

        public static readonly uint _IMAGE_BUFFER_MAX = 2000 * 2000;
        public static IntPtr _CubiWndHandle = IntPtr.Zero;
        public static IntPtr _INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion Fields

        #region Enums

        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 0x0001,
            FileMapWrite = 0x0002,
            FileMapRead = 0x0004,
            FileMapAllAccess = 0x001f,
            fileMapExecute = 0x0020,
        }

        [Flags]
        public enum FileMapProtection : uint
        {
            PageReadonly = 0x02,
            PageReadWrite = 0x04,
            PageWriteCopy = 0x08,
            PageExecuteRead = 0x20,
            PageExecuteReadWrite = 0x40,
            SectionCommit = 0x8000000,
            SectionImage = 0x1000000,
            SectionNoCache = 0x10000000,
            SectionReserve = 0x4000000,
        }

        #endregion Enums

        #region Methods

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpFileMappingAttributes,
            FileMapProtection flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        public static bool CreateMemory(ref IntPtr memory, string keyName, uint bufferSize)
        {
            try
            {
                if (memory != IntPtr.Zero)
                {
                    CloseHandle(memory);
                }// else

                UIntPtr uBytes = new UIntPtr(0);
                memory = CreateFileMapping(_INVALID_HANDLE_VALUE,
                                                IntPtr.Zero,
                                                FileMapProtection.PageReadWrite,
                                                0,
                                                bufferSize,
                                                keyName);

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static IntPtr GetWndHandle(string strProcName, string strProcPath, string strArg, bool bWaitForIdle)
        {
            IntPtr hWnd = IntPtr.Zero;

            Process[] processList = Process.GetProcessesByName(strProcName);

            foreach (Process process in processList)
            {
                try
                {
                    hWnd = process.MainWindowHandle;
                    if (hWnd.ToInt32() > 0) // ACQModule 이 동작되고 있는 경우
                        break;
                    else
                        process.Kill();     //
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }

            if (hWnd.Equals(IntPtr.Zero))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.Arguments = strArg;
                    process.StartInfo.FileName = strProcPath;
                    process.Start();

                    if (bWaitForIdle)
                    {
                        process.WaitForInputIdle();
                    }

                    hWnd = process.MainWindowHandle;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }

            processList = null;

            return hWnd;
        }

        public static bool KillProcess(string ProcName)
        {
            try
            {
                Process[] CheckProc = new Process[Process.GetProcesses().Length];
                CheckProc = Process.GetProcessesByName(ProcName);
                if (CheckProc.Length > 0)
                {
                    for (int i = 0; i < CheckProc.Length; i++)
                    {
                        CheckProc[i].Kill();
                    }
                }

                CheckProc = Process.GetProcessesByName(ProcName);

                if (CheckProc != null && CheckProc.Length > 0)
                {
                    RetryKillProcess(ProcName);
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                return false;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
                                                FileMapAccess dwDesiredAccess,
                                                UInt32 dwFileOffsetHigh,
                                                UInt32 dwFileOffsetLow,
                                                UIntPtr dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenFileMapping(uint dwDesiredAccess, bool bInheritHandle, string lpName);

        public static bool RunCubiControl(string strArgs, IntPtr mainWndHandle, string memoryKeyName, string pixelValue)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"CubiControl.exe";
                _CubiWndHandle = GetWndHandle("CubiControl", path, strArgs, true);

                string wndHandle = string.Format("{0}#{1}#{2}", mainWndHandle, memoryKeyName, pixelValue);

                MessageMngr.COPYDATASTRUCT cds;

                cds = new MessageMngr.COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero; //임의값
                cds.cbData = Encoding.Default.GetBytes(wndHandle).Length + 1;
                cds.lpData = wndHandle;
                MessageMngr.SendMessage(_CubiWndHandle, MessageMngr.WM_COPYDATA, new IntPtr(MessageMngr.START), ref cds);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        private static void RetryKillProcess(string ProcName)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan timeSpan;

            Process[] CheckProc;

            while (true)
            {
                timeSpan = DateTime.Now - startTime;

                if (timeSpan.TotalMilliseconds >= 1000)
                {
                    break;
                }

                CheckProc = Process.GetProcessesByName(ProcName);

                if (CheckProc == null || CheckProc.Length == 0)
                {
                    break;
                } // else

                for (int i = 0; i < CheckProc.Length; i++)
                {
                    CheckProc[i].Kill();
                }

                System.Threading.Thread.Sleep(50);
            }
        }

        #endregion Methods
    }
}