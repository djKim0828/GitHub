using System;
using System.Windows.Forms;

namespace EmWorks.App.Sample
{
    public partial class DigitalCommandDialog : Form
    {
        #region Fields

        private int _DigitaldialogResult = 0;

        #endregion Fields

        // 0: off, 1:on -1 : cancel

        #region Constructors

        public DigitalCommandDialog()
        {
            InitializeComponent();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _DigitaldialogResult = digitaldialogResult.Cancel;
            this.Close();
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            _DigitaldialogResult = digitaldialogResult.Off;
            this.Close();
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            _DigitaldialogResult = digitaldialogResult.On;
            this.Close();
        }

        #endregion Methods

        #region Classes

        public class digitaldialogResult
        {
            #region Fields

            public const int Cancel = -1;
            public const int Off = 0;
            public const int On = 1;

            #endregion Fields
        }

        #endregion Classes
    }
}