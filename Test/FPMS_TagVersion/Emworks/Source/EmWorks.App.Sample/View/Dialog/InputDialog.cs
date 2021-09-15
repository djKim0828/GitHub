using System;
using System.Windows.Forms;

namespace EmWorks.App.Sample
{
    public partial class InputDialog : Form
    {
        #region Fields

        private int _DigitaldialogResult = -1;

        #endregion Fields

        #region Constructors

        public InputDialog()
        {
            InitializeComponent();

            txtInput.Text = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public int DigitaldialogResult
        {
            get
            {
                return _DigitaldialogResult;
            }
        }

        #endregion Properties

        #region Methods

        public string GetInputData()
        {
            return txtInput.Text;
        }

        public void SetInputData(string inputData)
        {
            txtInput.Text = inputData;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _DigitaldialogResult = digitaldialogResult.Cancel;

            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            _DigitaldialogResult = digitaldialogResult.Ok;
            this.Close();
        }

        #endregion Methods

        #region Classes

        public class digitaldialogResult
        {
            #region Fields

            public const int Cancel = -1;
            public const int Ok = 1;

            #endregion Fields
        }

        #endregion Classes
    }
}