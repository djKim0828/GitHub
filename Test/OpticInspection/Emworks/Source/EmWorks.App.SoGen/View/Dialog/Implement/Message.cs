using System.Windows;
using System.Windows.Input;

namespace EmWorks.App.SoGen
{
    public enum MessageBoxButtonType
    {
        OK,
        CANCEL,
        OKCANCEL,
        YES,
        YESNO,
        YESNOCALCEL,
        YESNODONT //YES, NO, 다시는 보이지않을때
    }

    public enum MessageBoxImageType
    {
        INFORMATION,
        QUESTION,
        WARNING,
        ERROR
    }

    public enum MessageBoxReturnValue
    {
        OK,
        CANCEL,
        YES,
        NO,
        DONT,
    }

    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="MessageBox"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-08 11:15
    /// Description : object detail description.
    /// </summary>
    public class Message : MessageBoxDialog
    {
        #region Fields

        public MessageBoxButtonType BtnType;
        public MessageBoxReturnValue ReturnValue = MessageBoxReturnValue.CANCEL;
        private int locale;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description :  objcet constructor.
        /// </summary>
        public Message(string message, string title = "Information", MessageBoxButtonType btnType = MessageBoxButtonType.OK, MessageBoxImageType ImgType = MessageBoxImageType.INFORMATION)
        {
            Initialize(); // 초기화 함수를 호출

            txtMsg.Text = message;
            lblMessageBoxDlgTitle.Content = title;
            BtnType = btnType;

            SetMessageBoxImageType(ImgType);
            SetMessageBoxButtonType(btnType);
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description :  object destructor.
        /// </summary>
        ~Message()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-08 11:15
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

        #endregion Properties

        #region Methods

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            switch (BtnType)
            {
                case MessageBoxButtonType.OK:
                    ReturnValue = MessageBoxReturnValue.OK;
                    break;

                case MessageBoxButtonType.YES:
                    ReturnValue = MessageBoxReturnValue.YES;
                    break;

                case MessageBoxButtonType.CANCEL:
                    ReturnValue = MessageBoxReturnValue.CANCEL;
                    break;

                case MessageBoxButtonType.OKCANCEL:
                    ReturnValue = MessageBoxReturnValue.OK;
                    break;

                case MessageBoxButtonType.YESNO:
                    ReturnValue = MessageBoxReturnValue.YES;
                    break;

                case MessageBoxButtonType.YESNOCALCEL:
                    ReturnValue = MessageBoxReturnValue.YES;
                    break;

                case MessageBoxButtonType.YESNODONT:
                    ReturnValue = MessageBoxReturnValue.YES;
                    break;
            }

            Close();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            switch (BtnType)
            {
                case MessageBoxButtonType.OK:
                    ReturnValue = MessageBoxReturnValue.OK;
                    break;

                case MessageBoxButtonType.YES:
                    ReturnValue = MessageBoxReturnValue.YES;
                    break;

                case MessageBoxButtonType.YESNO:
                    ReturnValue = MessageBoxReturnValue.NO;
                    break;

                case MessageBoxButtonType.CANCEL:
                case MessageBoxButtonType.OKCANCEL:
                    ReturnValue = MessageBoxReturnValue.CANCEL;
                    break;

                case MessageBoxButtonType.YESNOCALCEL:
                case MessageBoxButtonType.YESNODONT:
                    ReturnValue = MessageBoxReturnValue.NO;
                    break;
            }
            Close();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            switch (BtnType)
            {
                case MessageBoxButtonType.YESNOCALCEL:
                    ReturnValue = MessageBoxReturnValue.CANCEL;
                    break;

                case MessageBoxButtonType.YESNODONT:
                    ReturnValue = MessageBoxReturnValue.DONT;
                    break;
            }
            Close();
        }

        private void BtnMessageBoxDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void CMessageBoxDlg_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SetWindowLocation();
        }

        private void CMessageBoxDlg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            Topmost = true;
            ShowInTaskbar = false;

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = 0;
            resultInstance = InitInstance();

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
        /// Date :  2020-09-08 11:15
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            IsInitialled = false;

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            locale = 0;
            // add your code here

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 11:15
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += CMessageBoxDlg_Loaded;
            this.MouseMove += CMessageBoxDlg_MouseMove;
            this.btn1.Click += Btn1_Click;
            this.btn2.Click += Btn2_Click;
            this.btn3.Click += Btn3_Click;
            this.btnMessageBoxDlgClose.Click += BtnMessageBoxDlgClose_Click;

            return 0;
        }

        /// <summary>
        /// 출력된 다이얼로그(버튼)의 타입을 설정
        /// </summary>
        /// <param name="BtnType"></param>
        private void SetMessageBoxButtonType(MessageBoxButtonType btnType)
        {
            string btnContent = string.Empty;
            BtnType = btnType;

            switch (BtnType)
            {
                case MessageBoxButtonType.OK:
                    btn2.Visibility = Visibility.Hidden;
                    btn3.Visibility = Visibility.Hidden;

                    btn1.Content = "OK";
                    btn1.IsDefault = true;
                    break;

                case MessageBoxButtonType.CANCEL:
                    btn2.Visibility = Visibility.Hidden;
                    btn3.Visibility = Visibility.Hidden;

                    btn1.Content = "CANCEL";
                    btn1.IsDefault = true;
                    break;

                case MessageBoxButtonType.OKCANCEL:
                    btn3.Visibility = Visibility.Hidden;

                    btn2.Content = "CANCEL";
                    btn1.Content = "OK";

                    btn2.IsDefault = true;
                    break;

                case MessageBoxButtonType.YES:
                    btn2.Visibility = Visibility.Hidden;
                    btn3.Visibility = Visibility.Hidden;

                    btn1.Content = "YES";
                    btn1.IsDefault = true;
                    break;

                case MessageBoxButtonType.YESNO:
                    btn3.Visibility = Visibility.Hidden;
                    btn2.Content = "NO";
                    btn1.Content = "YES";
                    btn2.IsDefault = true;
                    break;

                case MessageBoxButtonType.YESNOCALCEL:
                    btn3.Content = "CANCEL";
                    btn2.Content = "NO";
                    btn1.Content = "YES";
                    btn3.IsDefault = true;
                    break;

                case MessageBoxButtonType.YESNODONT:
                    btn3.Content = "Don't show again";
                    btn2.Content = "NO";
                    btn1.Content = "YES";
                    btn3.IsDefault = true;
                    break;
            }
        }

        /// <summary>
        /// 출력할 메시지박스 타입 설정
        /// </summary>
        /// <param name="ImgType"></param>
        private void SetMessageBoxImageType(MessageBoxImageType imgType)
        {
            imgError.Visibility = Visibility.Collapsed;
            imgInfo.Visibility = Visibility.Collapsed;
            imgQuestion.Visibility = Visibility.Collapsed;
            imgWarning.Visibility = Visibility.Collapsed;

            switch (imgType)
            {
                case MessageBoxImageType.INFORMATION:
                    imgInfo.Visibility = Visibility.Visible;
                    break;

                case MessageBoxImageType.QUESTION:
                    imgQuestion.Visibility = Visibility.Visible;
                    break;

                case MessageBoxImageType.WARNING:
                    imgWarning.Visibility = Visibility.Visible;
                    break;

                case MessageBoxImageType.ERROR:
                    imgError.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SetWindowLocation()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion Methods
    }
}