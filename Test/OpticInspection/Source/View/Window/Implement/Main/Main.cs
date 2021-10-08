using EmWorks.Lib.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="Main"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-01-21 15:00 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class Main : EmWorks.View.MainWindow
    {
        #region Fields

        private AutoLocale _AutoLocale;
        private bool _isLoop;
        private int _locale;
        private int _loopInterval;
        private MainStadium _mainStadium;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description :  objcet constructor.
        /// </summary>
        public Main()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~Main()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Properties

        public DtAlarm DtAlarm { get; set; }
        public DtLog DtLog { get; set; }
        public DtMenu DtMenu { get; set; }
        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description :  kind of language. [0=English,1=Korea,2=Chinese,3=Vietnam,4=India]
        /// </summary>
        public int Locale
        {
            get
            {
                return _locale;
            }
            set
            {
                _locale = value;
                InitLocale();
            }
        }

        public MtManual MtManual { get; set; }

        public MtMenu MtMenu { get; set; }

        public MaintIO MtViewIo { get; set; }

        public OpMenu OpMenu { get; set; }

        public OpMain OpView { get; set; }

        public RpMenu RpMenu { get; set; }

        public RpMain RpView { get; set; }

        #endregion Properties

        #region Methods

        public void BtnData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Data.ChangedView);
        }

        public void BtnMaintenance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.ChangedView);
        }

        public void BtnOperation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Operation.ChangedView);
        }

        public void BtnRecipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Recipe.ChangedView);
        }

        public void ChangeLocale(Window control)
        {
            _AutoLocale.Start(control);

            // 별도로 번역되어야 하는 것은 아래 넣는다.
            ChangeLocaleRunMode();
            ChangeLocaleLanguage();

            return;
        }

        public void ChangeMainControls(int statusId)
        {
            this.Bottombar.btnOperation.IsChecked = false;
            this.Bottombar.btnMaintenance.IsChecked = false;
            this.Bottombar.btnRecipe.IsChecked = false;
            this.Bottombar.btnData.IsChecked = false;

            switch (statusId)
            {
                case (int)StatusType.Operation.ChangedView:
                    this.Bottombar.btnOperation.IsChecked = true;
                    break;

                case (int)StatusType.Maint.ChangedView:
                case (int)StatusType.Maint.Manual:
                case (int)StatusType.Maint.Io:
                    this.Bottombar.btnMaintenance.IsChecked = true;
                    break;

                case (int)StatusType.Recipe.ChangedView:
                    this.Bottombar.btnRecipe.IsChecked = true;
                    break;

                case (int)StatusType.Data.ChangedView:
                case (int)StatusType.Data.Log:
                case (int)StatusType.Data.Alarm:
                case (int)StatusType.Data.Product:
                case (int)StatusType.Data.Mcc:
                    this.Bottombar.btnData.IsChecked = true;
                    break;

                default:
                    Logger.Debug($"{nameof(ChangeMainControls)}() - Undefined Status ID");
                    break;
            }
        }

        public void HiddenAllSubMenuWindows()
        {
            // sub menu hide all.
            foreach (UserControl menu in _AutoLocale.FindVisualChildren<UserControl>(grdSubMenu))
            {
                if (menu is UserControl)
                {
                    if (menu.Tag == null)
                    {
                        menu.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        string a = menu.Tag.ToString();
                    }
                }
            }
        }

        public void HiddenAllSubWindows(bool isHidden = true)
        {
            // view hide all.
            //Todo : 오늘 확인 필
            foreach (UserControl menu in _AutoLocale.FindVisualChildren<UserControl>(grdView))
            {
                if (menu is UserControl)
                {
                    if (menu.Tag == null)
                    {
                        if (isHidden == true)
                        {
                            menu.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            menu.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        string a = menu.Tag.ToString();
                    }
                }
            }
        }

        public void LblMenuTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StatusMachine.Request(StatusType.Application.ExeLogger);
        }

        public bool LoadConfig()
        {
            //Todo: InitInstance 확인할것. ysh_Andrew
            //Global.ConfigGeneral.InitInstance();

            Global.ConfigGeneral = new ConfigGeneralModel();
            Global.ConfigGeneral.Load( ref Global.ConfigGeneral);

            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더

            Global.ConfigGeneral.RootPath = newPath;

            return true;
        }

        public bool Loadi18n()
        {
            i18n.InitInstance(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.LanguageFilePath);

            if (Global.ConfigGeneral.Language == i18n.LanguageType.KO.ToString())
            {
                i18n.SetLanguageType(i18n.LanguageType.KO);
            }
            else
            {
                i18n.SetLanguageType(i18n.LanguageType.EN);
            }

            return true;
        }

        public void RdbAlarm_Click(object sender, RoutedEventArgs e)
        {
            ShowRdbAlarm();
        }

        public void RdbInit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.ShowDialog("Do you want to initialize?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.YES)
            {
                App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    InitializeBox ib = new InitializeBox("EmWorks.App.OpticInspection", "MainInitialize", "Open Drivers");
                    ib.ShowDialog();

                    //CommandCenter.ReInitialize();

                    StatusMachine.Request(StatusType.Operation.Stop);
                });
            } // else
        }

        public void RdbIo_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Io);
            MtMenu.rdbIo.IsChecked = true;
        }

        public void RdbLog_Click(object sender, RoutedEventArgs e)
        {
            ShowRdbLog();
        }

        public void RdbManual_Click(object sender, RoutedEventArgs e)
        {
            ShowRdbManual();
        }

        public void RdbShutdown_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Application.Exit);
        }

        public void ShowRdbAlarm()
        {
            StatusMachine.Request(StatusType.Data.Alarm);
            this.DtMenu.rdbAlarm.IsChecked = true;
        }

        public void ShowRdbLog()
        {
            StatusMachine.Request(StatusType.Data.Log);
            this.DtMenu.rdbLog.IsChecked = true;
        }

        public void ShowRdbManual()
        {
            StatusMachine.Request(StatusType.Maint.Manual);
            MtMenu.rdbManual.IsChecked = true;
        }

        private void BtnAlarm_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Alarm.Occurred, "EMOAlarm");
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLocale_Click(object sender, RoutedEventArgs e)
        {
            SelectLanguage dlg = new SelectLanguage();
            dlg.ShowDialog();
        }

        private void BtnMax_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeWindowStatus();
        }

        private void BtnMinimise_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ChangeLocaleLanguage()
        {
            if (Global.ConfigGeneral.Language == i18n.LanguageType.KO.ToString())
            {
                this.Titlebar.btnLocale.Content = "[KOR]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.EN.ToString())
            {
                this.Titlebar.btnLocale.Content = "[ENG]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.CN.ToString())
            {
                this.Titlebar.btnLocale.Content = "[CHN]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.IN.ToString())
            {
                this.Titlebar.btnLocale.Content = "[IND]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.VN.ToString())
            {
                this.Titlebar.btnLocale.Content = "[VNM]";
            }
        }

        private void ChangeLocaleRunMode()
        {
            string runMode = "Run Mode";

            switch (Global.ConfigGeneral.RunMode)
            {
                case Idx.RunMode.Dry:
                    runMode = "Dry Run";
                    break;

                case Idx.RunMode.Simulation:
                    runMode = "Simulation";
                    break;

                default:
                    break;
            }

            //this.OpMenu.lblRunMode.Content = i18n.GetLanguage(runMode);
        }

        private void ChangeWindowStatus()
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.Titlebar.btnMax.Style = (Style)Application.Current.Resources["btnMax"];
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                this.Titlebar.btnMax.Style = (Style)Application.Current.Resources["btnWindowNormal"];
            }
        }

        private void ChangeWindowStatus(WindowState winstate)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (winstate == System.Windows.WindowState.Minimized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.Titlebar.btnMax.Style = (Style)Application.Current.Resources["btnMax"];
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                this.Titlebar.btnMax.Style = (Style)Application.Current.Resources["btnWindowNormal"];
            }
        }

        private void CreateDataWindows()
        {
            this.grdView.Children.Add(DtLog = new DtLog());
            this.grdView.Children.Add(DtAlarm = new DtAlarm());
            this.grdSubMenu.Children.Add(DtMenu = new DtMenu());
        }

        private void CreateMaintenanceWindows()
        {
            this.grdView.Children.Add(MtManual = new MtManual());
            this.grdView.Children.Add(MtViewIo = new MaintIO());
            this.grdSubMenu.Children.Add(MtMenu = new MtMenu());
        }

        private void CreateOperationWindows()
        {
            this.grdView.Children.Add(OpView = new OpMain());
            this.grdSubMenu.Children.Add(OpMenu = new OpMenu());
        }

        private void CreateReceipeWindows()
        {
            this.grdView.Children.Add(RpView = new RpMain());
            this.grdSubMenu.Children.Add(RpMenu = new RpMenu());
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                InitializeBox ib = new InitializeBox("EmWorks.App.OpticInspection", "MainInitialize", "Open Drivers");
                ib.ShowDialog();
            });

            Global._IsProgramLoadComplete = true;

            //_isLoop = true;
            //_loopInterval = 100;

            ////Todo: 테스트 용 - 삭제 必
            //int testV = 0;

            //while (_isLoop)
            //{
            //    //Todo: 테스트 용 - 삭제 必
            //    if (testV < 50)
            //    {
            //        testV = testV + 1;
            //        Global._PmacX1.yShuttleX1Move = testV;
            //        Global._PmacY2.yShuttleY2Move = testV;

            //        Global._PmacX3.yInspectorX3Move = testV;
            //        Global._PmacZ4.yCameraZ4Move = testV;
            //        Global._PmacZ5.ySR5000Z5Move = testV;
            //    }

            //    App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            //    {
            //        // Todo : 테스트 Code. 삭제 必
            //        if (this.Titlebar.btnAlarm.IsChecked == true)
            //        {
            //            this.Titlebar.btnAlarm.IsChecked = false;
            //        }
            //        else
            //        {
            //            this.Titlebar.btnAlarm.IsChecked = true;
            //        }
            //    });

            //    // add your code here
            //    Thread.Sleep(_loopInterval);
            //}
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBox dlg = new MessageBox("Are you sure you want to exit?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
                dlg.ShowDialog();
                if (dlg.ReturnValue == MessageBoxReturnValue.YES)
                {
                    Environment.Exit(0);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    this.Close();
                }
            }
            else if (e.Key == Key.F2)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    InitializeBox ib = new InitializeBox("EmWorks.App.OpticInspection", "MainInitialize", "Open Drivers");
                    ib.ShowDialog();
                }));

                Global._IsProgramLoadComplete = true;
            }
        }

        private void GrdTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    ChangeWindowStatus();
                }
            }
        }

        private void GrdTop_MouseMove(object sender, MouseEventArgs e)
        {
            System.Threading.Thread.Sleep(50); // 더블클릭 오동작 방지
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 32)
                {
                    if (this.WindowState == System.Windows.WindowState.Maximized)
                    {
                        if (this.Top != 0)
                        {
                            this.WindowState = System.Windows.WindowState.Normal;
                            this.Top = 0;
                        }
                    }

                    DragMove();
                }
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            ChangeWindowStatus(WindowState.Maximized);

            //this.grdTopView.Children.Add(TopView = new BottomMenuWindow());
            //this.grdMainMenu.Children.Add(MainMenu = new UcMainMenu());
            //this.TopTitle = TopTitle = new TopTitleWindow()

            CreateDataWindows();
            CreateReceipeWindows();
            CreateMaintenanceWindows();
            CreateOperationWindows(); // Defualt 화면을 가장 마지막에 추가[2020/09/03 djkim]

            InitControlsViewTitle();

            return Idx.InitResult.True;
        }

        /// <summary>
        /// Author : Hans, Kim
        /// Date :  2020-09-26 16:55
        /// Description : initialize view main and sub title.
        /// </summary>
        private void InitControlsViewTitle()
        {
            OpView.lblMainTitle.Content = Bottombar.btnOperation.Content;

            MtManual.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtManual.lblSubTitle.Content = MtMenu.rdbManual.Content;

            MtViewIo.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtViewIo.lblSubTitle.Content = MtMenu.rdbIo.Content;

            DtLog.lblMainTitle.Content = Bottombar.btnData.Content;
            DtLog.lblSubTitle.Content = DtMenu.rdbLog.Content;

            DtAlarm.lblMainTitle.Content = Bottombar.btnData.Content;
            DtAlarm.lblSubTitle.Content = DtMenu.rdbAlarm.Content;

            RpView.lblMainTitle.Content = Bottombar.btnRecipe.Content;
            RpView.lblSubTitle.Content = RpMenu.rdbRecipe.Content;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
                resultLocale == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                IsInitialled = false;
            }

            Logger.Infomation("***** Start Application *****");
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;
                _loopInterval = 5;
                _isLoop = false;

                // Main
                _mainStadium = new MainStadium(this);
                _AutoLocale = new AutoLocale();

                // 1 : Config
                LoadConfig();

                // 2 : Logger
                Logger.InitInstance(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.LogFilePath);

                // 3 : i18n(Multi Laguang)
                Loadi18n();

                // 4 : Alarm
                LaodAlarm();

                // 5 : Writers
                Global._CellInfoWriter = new DataWriter(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.CellDataFilePath);
                Global._AlarmWriter = new AlarmModel(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AlarmDataFilePath);

                //Todo : 나중에 변경
                if (MessageBox.ShowDialog("시뮬레이션 모드로 실행할까요?", "질문", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.YES)
                {
                    Global.ConfigGeneral.RunMode = Idx.RunMode.Simulation;
                }
                else
                {
                    Global.ConfigGeneral.RunMode = Idx.RunMode.Dry;
                }

                // 6 : Etc
                Global._MotionControl = new MotionControl();

                return Idx.InitResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            try
            {
                _locale = 0;

                return Idx.InitResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.InitResult.False;
            }
        }

        private bool LaodAlarm()
        {
            AlarmDefine.InitInstance(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AlarmListFilePath);
            return true;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            // 실행시 언어 변경
            ChangeLocale(this);
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // Main
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.Titlebar.grdTopView01.MouseLeftButtonDown += GrdTop_MouseLeftButtonDown;
            this.Titlebar.MouseMove += GrdTop_MouseMove;
            this.Titlebar.btnDlgClose.Click += BtnDlgClose_Click;
            this.Titlebar.btnMax.Click += BtnMax_Click;
            this.Titlebar.btnMinimise.Click += BtnMinimise_Click;
            this.Bottombar.btnOperation.Click += BtnOperation_Click;
            this.Bottombar.btnMaintenance.Click += BtnMaintenance_Click;
            this.Bottombar.btnRecipe.Click += BtnRecipe_Click;
            this.Bottombar.btnData.Click += BtnData_Click;
            //this.MouseMove += Form_MouseMove;
            this.Loaded += Main_Loaded;

            this.Titlebar.btnLocale.Click += BtnLocale_Click;
            this.Titlebar.btnAlarm.Click += BtnAlarm_Click;

            // click event of the  maintenance menu.
            this.MtMenu.rdbManual.Click += RdbManual_Click;
            this.MtMenu.rdbIo.Click += RdbIo_Click;
            this.MtMenu.rdbInit.Click += RdbInit_Click;
            this.MtMenu.rdbShutdown.Click += RdbShutdown_Click;

            // click event of the  recipe menu.

            // click event of the data menu.
            this.DtMenu.lblMenuTitle.MouseDoubleClick += LblMenuTitle_MouseDoubleClick;
            this.DtMenu.rdbLog.Click += RdbLog_Click;
            this.DtMenu.rdbAlarm.Click += RdbAlarm_Click;

            StatusMachine.ChangedStatus += StatusMachine_ChangeStatus;

            return Idx.FunctionResult.True;
        }

        private void StatusMachine_ChangeStatus(object sendor, StatusMachine.EventChangedStatusArgs e)
        {
            _mainStadium.Switching(sendor, e);
        }

        #endregion Methods
    }
}