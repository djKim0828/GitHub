using System.Windows.Input;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="InputBox"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-03 11:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class InputBox : InputBoxDialog
    {
        #region Fields

        public MessageBoxReturnValue ReturnValue = MessageBoxReturnValue.CANCEL;
        private int locale;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  objcet constructor.
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
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  kind of language. [0=English,1=Korea,2=Chinese,3=Vietnam,4=India]
        /// </summary>
        public int Locale
        {
            get
            {
                return locale;
            }
            set
            {
                locale = value;
                InitLocale();
            }
        }

        public string outputData
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
            ReturnValue = MessageBoxReturnValue.CANCEL;
            this.Close();
        }

        private void BtnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReturnValue = MessageBoxReturnValue.OK;
            this.Close();
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // add your code here

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = resultInstance = InitInstance();
            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == 0 && resultLocale == 0 && resultControls == 0 && resultEvent == 0)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            IsInitialled = false;

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            locale = 0;

            return 0;
        }

        private void InputBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
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

            return 0;
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