using System.Windows.Input;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="InputBox"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-03 11:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class InputBox : EmWorks.View.InputBoxDialog
    {
        #region Fields

        public MessageBoxReturnValue _ReturnValue = MessageBoxReturnValue.CANCEL;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object constructor.
        /// </summary>
        public InputBox(string title, string inputName, string inputData = "", bool isPassword = false)
        {
            Initialize();

            lblInputTextDlgTitle.Content = title;
            lblInputText.Content = inputName;
            txbInputText.Text = inputData;

            if (isPassword == true)
            {
                pwbInputText.Visibility = System.Windows.Visibility.Visible;
                txbInputText.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                pwbInputText.Visibility = System.Windows.Visibility.Hidden;
                txbInputText.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~InputBox()
        {

        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }


        public string OutputData
        {
            get { return txbInputText.Text; }
        }

        public string outputPassword
        {
            get { return pwbInputText.Password; }
        }

        #endregion Properties

        #region Methods

        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ReturnValue = MessageBoxReturnValue.CANCEL;
            this.Close();
        }

        private void BtnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ReturnValue = MessageBoxReturnValue.OK;
            this.Close();
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
            }
            else
            {
                _IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            _IsInitialled = false;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {

            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        private void InputBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();

            if (txbInputText.Visibility == System.Windows.Visibility.Visible)
            {
                txbInputText.Focus();
            }
            else
            {
                pwbInputText.Focus();
            }
        }

        private void InputBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Enter)
            {
                BtnOK_Click(null, null);
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += InputBox_PreviewKeyDown;
            this.btnInputTextDlgClose.Click += BtnCancel_Click;
            this.Loaded += InputBox_Loaded;

            this.btnOK.Click += BtnOK_Click;
            this.btnCancel.Click += BtnCancel_Click;

            return Idx.FunctionResult.True;
        }

        #endregion Methods

        public enum MessageBoxReturnValue
        {
            OK,
            CANCEL,
            YES,
            NO,
            DONT,
        }
    }
}
