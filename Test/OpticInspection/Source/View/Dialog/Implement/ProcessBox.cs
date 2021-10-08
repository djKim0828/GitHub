using EmWorks.Lib.Common;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="ProcessBox"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-28
    /// Description : object detail description.
    /// </summary>
    public class ProcessBox : EmWorks.View.ProcessDialog
    {
        #region Fields

        private string titleMessage;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-28
        /// Description :  objcet constructor.
        /// </summary>
        public ProcessBox()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-28
        /// Description :  object destructor.
        /// </summary>
        ~ProcessBox()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-02 13:00
        /// Description :  property detail description.
        /// </summary>
        public string TitleMessage
        {
            get
            {
                return titleMessage;
            }
            set
            {
                txtLoading.Text = value;
                titleMessage = value;
            }
        }

        #endregion Properties

        #region Methods

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dialog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();
        }

        private void Dialog_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 50)
                {
                    DragMove();
                }
            }
        }

        private void Dialog_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-28
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-28
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-28
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-28
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-28
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += Dialog_Loaded;
            this.PreviewKeyDown += Dialog_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}