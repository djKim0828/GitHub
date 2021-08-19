using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace InterfaceTestor
{
    public class Func
    {
        #region Fields

        public static IntPtr _CubiWndHandle = IntPtr.Zero;

        #endregion Fields

        #region Methods

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

        public static bool RunCubiControl(string strArgs, IntPtr mainWndHandle)
        {
            try
            {
                string path = Application.StartupPath + @"\CubiControl.exe";
                _CubiWndHandle = GetWndHandle("CubiControl", path, strArgs, true);

                string wndHandle = string.Format("{0}", mainWndHandle);

                MessageMngr.COPYDATASTRUCT cds;

                cds = new MessageMngr.COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero; //임의값
                cds.cbData = Encoding.Default.GetBytes(wndHandle).Length + 1;
                cds.lpData = wndHandle;
                MessageMngr.SendMessage(_CubiWndHandle, MessageMngr.WM_COPYDATA, new IntPtr(MessageMngr.WM_START), ref cds);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

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