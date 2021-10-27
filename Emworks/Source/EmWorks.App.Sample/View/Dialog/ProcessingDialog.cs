using System;
using System.Threading;
using System.Windows.Forms;

namespace EmWorks.App.Sample
{
    public partial class ProcessingBox : Form
    {
        #region Fields

        public int _result;        
        private bool _isLoop = false;
        private Thread _processThd;

        #endregion Fields

        #region Constructors

        public ProcessingBox(string message)
        {
            InitializeComponent();

            lblMessage.Text = message;

            initControl();
        }

        #endregion Constructors

        #region Methods

        private void initControl()
        {
            // N/A
        }
        

        private void ProcessingBox_Load(object sender, EventArgs e)
        {
            //_processThd = new Thread(StartFunc);
            //_processThd.IsBackground = true;

            //_processThd.Start();
        }

        private void Connect()
        {
            lblMessage.Invoke(new MethodInvoker(() =>
            {


                this.Close();
            }));
        }

        private void StartFunc()
        { 
            Connect();            
        }

        #endregion Methods

        #region Classes

        public class processResult
        {
            #region Fields

            public const int Failed = 0;
            public const int Success = 1;

            #endregion Fields
        }

        #endregion Classes
    }
}