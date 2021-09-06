using EmWorks.Lib.Common;
using System.Windows.Input;


namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="SelectLanguage"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-02-25
    /// Description : object detail description.
    /// </summary>
    public class SelectLanguage : EmWorks.View.SelectLanguageDialog
    {
        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description :  object constructor.
        /// </summary>
        public SelectLanguage()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description :  object destructor.
        /// </summary>
        ~SelectLanguage()
        {
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (btnEng.IsChecked == true)
            {
                StatusMachine.Request(StatusType.Language.English);
            }
            else
            {
                StatusMachine.Request(StatusType.Language.Korean);
            }

            this.Close();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            } //else
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : Mouse Move Event
        /// </summary>
        private void GrdTextDlgTitle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 50)
                {
                    DragMove();
                } // else
            } // else
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            if (Global.ConfigGeneral.Language == i18n.LanguageType.KO.ToString())
            {
                btnKor.IsChecked = true;
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.EN.ToString())
            {
                btnEng.IsChecked = true;
            }

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

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
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-02-25
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.grdDlgTitle.MouseMove += GrdTextDlgTitle_MouseMove;
            this.btnDlgClose.Click += BtnDlgClose_Click;
            this.Loaded += SelectLanguage_Loaded;

            this.btnOk.Click += BtnOk_Click;
            this.btnCancel.Click += BtnCancel_Click;

            return Idx.FunctionResult.True;
        }

        private void SelectLanguage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();
        }

        #endregion Methods
    }
}