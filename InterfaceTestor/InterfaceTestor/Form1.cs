using System;
using System.Windows.Forms;

namespace InterfaceTestor
{
    public partial class Form1 : Form
    {
        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MessageMngr.WM_CUBIEVENT1:
                    WriteLog("Receive " + m.Msg.ToString());
                    WriteLog("Receive w=" + m.WParam.ToString());
                    WriteLog("Receive l=" + m.LParam.ToString());

                    // Invalidate to get new text painted.
                    this.Invalidate();

                    break;
            }
            base.WndProc(ref m);
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            if (Func.KillProcess("CubiControl") == true)
            {
                WriteLog("Complete Run Command");
            }
            else
            {
                WriteLog("Failed Run Command");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string args = txtArg1.Text + "/" + txtArg2.Text + "/" + txtArg3.Text;

            WriteLog(this.Handle.ToString());

            if (Func.RunCubiControl(args, this.Handle) == true)
            {
                WriteLog("Complete Run Command");
            }
            else
            {
                WriteLog("Failed Run Command");
            }
        }

        private void WriteLog(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        #endregion Methods
    }
}