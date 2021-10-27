using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="Setting"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2020-12-01
    /// Description : object detail description.
    /// </summary>
    public class Setting : SettingWindow
    {
        #region Fields

        private int locale;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description :  objcet constructor.
        /// </summary>
        public Setting()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description :  object destructor.
        /// </summary>
        ~Setting()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
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

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnInit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Message dlg = new Message("설정을 초기화 하시겠습니까?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
            dlg.ShowDialog();
            if (dlg.ReturnValue == MessageBoxReturnValue.YES)
            {
                Properties.Settings.Default.RootPath = System.Windows.Forms.Application.StartupPath;
                Properties.Settings.Default.ConfigPath = @"\Config";
                Properties.Settings.Default.AlarmPath = @"\Config\Alarm";
                Properties.Settings.Default.TagPath = @"\Config\Tag";
                Properties.Settings.Default.Save();

                InitControls();
            }
        }

        private void BtnOpenFolderDlg_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) // Test result.
            {
                txbPath.Text = openFileDialog.SelectedPath;
            }
        }

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.RootPath = this.txbPath.Text;
            Properties.Settings.Default.Save();

            this.Close();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : Mouse Move Event
        /// </summary>
        private void Form_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 32)
                {
                    DragMove();
                }
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            txbPath.Text = Properties.Settings.Default.RootPath;
            txbConfigPath.Text = Properties.Settings.Default.ConfigPath;
            txbAlarmPath.Text = Properties.Settings.Default.AlarmPath;
            txbTagPath.Text = Properties.Settings.Default.TagPath;

            return 0;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == 0 || resultLocale == 0 || resultControls == 0 || resultEvent == 0)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            locale = 0;

            return 0;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-12-01
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.MouseMove += Form_MouseMove;
            this.btnDlgClose.Click += BtnDlgClose_Click;

            this.btnOpenFolderDlg.Click += BtnOpenFolderDlg_Click;

            this.btnInit.Click += BtnInit_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnClose.Click += BtnDlgClose_Click;
            return 0;
        }

        #endregion Methods
    }
}