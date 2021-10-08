using EmWorks.Lib.Common;
using System.Threading;
using System.Windows.Input;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="CommandCheck"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-01-26 15:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class CommandCheck : EmWorks.View.CommandCheckDialog
    {
        #region Fields

        private string _commandName;
        private object _commandObject;

        private bool _IsAutoClose = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  objcet constructor.
        /// </summary>
        public CommandCheck(object commandObject, string commandName, string title, bool isAutoClose = false)
        {
            Initialize();

            _commandObject = commandObject;
            _commandName = commandName;

            _IsAutoClose = isAutoClose;

            lblDlgTitle.Content = title;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~CommandCheck()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods        

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : Mouse Move Event
        /// </summary>
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

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
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
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            this.ShowInTaskbar = false;

            //this.WindowStartupLocation == System.Windows.WindowStartupLocation.Manual;
            //this.Top = 100;
            //this.Left = 100;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
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

        private void InitializeBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();

            try
            {
                ThreadPool.QueueUserWorkItem(RunProcess);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
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
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
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
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;
            this.Loaded += InitializeBox_Loaded;

            this.btnClose.Click += BtnDlgClose_Click;

            this.btnOK.Click += BtnOK_Click;

            return Idx.FunctionResult.True;
        }
        

        private void RunInRotInit()
        {
            //InRobotControl temp = (InRobotControl)_commandObject;

            //temp.LoggerMessage += InRobotLoggerMessage;
            //temp.SetRunInitialize();
        }
        
        private void RunProcess(object parameter)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {                
                //prgWaiting.Begin();
            });

            switch (_commandName)
            {
                case CommandType.InRobotInit:
                    RunInRotInit();
                    break;                

                default:
                    WriteLog("It is not a defined command.");
                    break;
            }

            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                //prgWaiting.Stop();
            });
        }

        private void WriteLog(string data)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                txtOutput.AppendText(data + "\n");
                txtOutput.ScrollToEnd();
            });
        }

        #endregion Methods

        #region Classes

        public class CommandType
        {
            #region Fields

            public const string InRobotCondition1 = nameof(InRobotCondition1);
            public const string InRobotCondition3 = nameof(InRobotCondition3);
            public const string InRobotInit = nameof(InRobotInit);
            public const string OutRobotCondition1 = nameof(OutRobotCondition1);
            public const string OutRobotCondition3 = nameof(OutRobotCondition3);
            public const string OutRobotInit = nameof(OutRobotInit);

            #endregion Fields
        }

        #endregion Classes
    }
}