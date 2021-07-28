using System;
using System.Threading;
using System.Windows.Forms;

namespace LiveView
{
    public partial class ProcessingBox : Form
    {

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
            //this.Width = pbProcess.Left + pbProcess.Width + lblMessage.Width + 100;// pbProcess.Left;
            //panel2.Width = this.Width - 4;
        }
        

        private void ProcessingBox_Load(object sender, EventArgs e)
        {
            
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