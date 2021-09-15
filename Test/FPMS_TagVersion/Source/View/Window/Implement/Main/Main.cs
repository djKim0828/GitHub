using EmWorks.Lib.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="Main"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class Main : EmWorks.View.MainWindow
    {
        #region Fields

        private AutoLocale _autoLocale;
        private bool _isLoop;
        private int _locale;
        private int _loopInterval;
        private MainStadium _mainStadium;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public Main()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~Main()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }
        public DtAlarm DtAlarm { get; set; }
        public DtLogger DtLogger { get; set; }
        public DtMenu DtMenu { get; set; }

        public MtChamber MtChamber { get; set; }
        public MtDetector MtDetector { get; set; }
        public MtIo MtIo { get; set; }
        public MtMenu MtMenu { get; set; }
        public MtMonitoring MtMonitoring { get; set; }
        public MtMotion MtMotion { get; set; }
        public OpMain OpMain { get; set; }
        public OpMenu OpMenu { get; set; }

        public RpMain RpMain { get; set; }
        public RpMenu RpMenu { get; set; }

        #endregion Properties

        #region Methods

        public void BtnData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Data.ChangedView);
        }

        public void BtnMaintenance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (MtMenu.Visibility == Visibility.Visible)
            //{
            //    return;
            //}
            // // else

            //string password = string.Empty;
            //InputBox dlg = new InputBox("Login", "Password", isPassword:true);
            //dlg.ShowDialog();
            //if (dlg._ReturnValue == InputBox.MessageBoxReturnValue.OK)
            //{
            //    password = dlg.outputPassword;
            //}
            //else
            //{
            //    return;
            //}

            ////Todo: Password Check
            //if (password == "fpms" || password == "FPMS")
            //{
            //    StatusMachine.Request(StatusType.Maint.ChangedView);
            //}
            //else
            //{
            //    MessageBox.Show("패스워드가 일치하지 않습니다.");
            //}

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
            _autoLocale.Start(control);

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
                case (int)StatusType.Maint.Motion:
                case (int)StatusType.Maint.Chamber:
                    this.Bottombar.btnMaintenance.IsChecked = true;
                    break;

                case (int)StatusType.Recipe.ChangedView:
                    this.Bottombar.btnRecipe.IsChecked = true;
                    break;

                case (int)StatusType.Data.ChangedView:
                case (int)StatusType.Data.Logger:
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
            foreach (UserControl menu in _autoLocale.FindVisualChildren<UserControl>(grdSubMenu))
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
            foreach (UserControl menu in _autoLocale.FindVisualChildren<UserControl>(grdView))
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

        public bool LoadConfig()
        {
            //Todo: InitInstance 확인할것. ysh_Andrew
            //Global.ConfigGeneral.InitInstance();

            Global.ConfigGeneral = new ConfigGeneralModel();
            Global.ConfigGeneral.Load(ref Global.ConfigGeneral);

            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더

            Global.ConfigGeneral.RootPath = newPath;
            Global.ConfigGeneral.Save();
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

        public void RdbChamber_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Chamber);
            MtMenu.rdbChamber.IsChecked = true;
        }

        internal void rdbDetector()
        {
            StatusMachine.Request(StatusType.Maint.Detector);
            MtMenu.rdbDetector.IsChecked = true;
        }

        internal void rdbDtLogger()
        {
            StatusMachine.Request(StatusType.Data.Logger);
            DtMenu.rdbLogger.IsChecked = true;
        }

        internal void rdbMotion()
        {
            StatusMachine.Request(StatusType.Maint.Motion);
            MtMenu.rdbMotion.IsChecked = true;
        }

        internal void rdbOpMain()
        {
            StatusMachine.Request(StatusType.Operation.OpMain);
            OpMenu.rdbOperation.IsChecked = true;
        }

        internal void rdbRpMain()
        {
            StatusMachine.Request(StatusType.Recipe.RpMain);
            RpMenu.rdbRecipe.IsChecked = true;
        }

        private void BtnDlgClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGrdTopClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnHomeSearch_Click(object sender, RoutedEventArgs e)
        {
            // Todo : 일단 서버온을 하고 Home 명령 내림. 장비 테스트시 인터락이 없으므로 추가하여 하나씩 테스트하기 바람.
            CommandServoOn(Idx.DigitalValue.Enable);

            CommandHome(Global.AjinMotionX, Idx.MotionAxisNo.X);
            CommandHome(Global.AjinMotionY, Idx.MotionAxisNo.Y);
            CommandHome(Global.AjinMotionZ, Idx.MotionAxisNo.Z);
            CommandHome(Global.AjinMotionV, Idx.MotionAxisNo.V);
            CommandHome(Global.AjinMotionH, Idx.MotionAxisNo.H);
            CommandHome(Global.AjinMotionR, Idx.MotionAxisNo.R);
        }

        private void BtnHomeStop_Click(object sender, RoutedEventArgs e)
        {
            CommandStop();
        }

        private void BtnLocale_Click(object sender, RoutedEventArgs e)
        {
            SelectLanguage dlg = new SelectLanguage();
            dlg.ShowDialog();
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindowStatus();
        }

        private void BtnMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ChangeLocaleLanguage()
        {
            if (Global.ConfigGeneral.Language == i18n.LanguageType.KO.ToString())
            {
                this.Bottombar.btnLocale.Content = "[KOR]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.EN.ToString())
            {
                this.Bottombar.btnLocale.Content = "[ENG]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.CN.ToString())
            {
                this.Bottombar.btnLocale.Content = "[CHN]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.IN.ToString())
            {
                this.Bottombar.btnLocale.Content = "[IND]";
            }
            else if (Global.ConfigGeneral.Language == i18n.LanguageType.VN.ToString())
            {
                this.Bottombar.btnLocale.Content = "[VNM]";
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
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void CommandHome(AjinMotion ajinMotion, int axisNo)
        {
            string category = Idx.MotionInfo.TagAxisFirstName + axisNo;

            ajinMotion.yMotionHomeVel1st = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeVel1st);
            ajinMotion.yMotionHomeVel2nd = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeVel2nd);
            ajinMotion.yMotionHomeVel3rd = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeVel3rd);
            ajinMotion.yMotionHomeVel4th = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeVel4th);
            ajinMotion.yMotionHomeAcc1st = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeAcc1st);
            ajinMotion.yMotionHomeAcc2nd = ConfigMotion.GetValue(category, Idx.MotionProperfy.HomeAcc2nd);

            ajinMotion.yMotionHomeVelocity = Idx.DigitalValue.Enable;
            ajinMotion.yMotionHoming = Idx.DigitalValue.Enable;
        }

        private void CommandServoOn(int enable)
        {
            Global.AjinMotionX.yMotionServoOn = enable;
            Global.AjinMotionY.yMotionServoOn = enable;
            Global.AjinMotionZ.yMotionServoOn = enable;
            Global.AjinMotionV.yMotionServoOn = enable;
            Global.AjinMotionH.yMotionServoOn = enable;
            Global.AjinMotionR.yMotionServoOn = enable;
        }

        private void CommandStop()
        {
            Global.AjinMotionX.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionY.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionZ.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionV.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionH.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionR.yMotionSStop = Idx.DigitalValue.Enable;
        }

        private void CreateDataWindows()
        {
            this.grdView.Children.Add(DtAlarm = new DtAlarm());
            this.grdView.Children.Add(DtLogger = new DtLogger());
            this.grdSubMenu.Children.Add(DtMenu = new DtMenu());
        }

        private void CreateMaintenanceWindows()
        {
            this.grdView.Children.Add(MtMotion = new MtMotion());
            this.grdView.Children.Add(MtIo = new MtIo());
            this.grdView.Children.Add(MtMonitoring = new MtMonitoring());
            this.grdView.Children.Add(MtDetector = new MtDetector());
            this.grdView.Children.Add(MtChamber = new MtChamber());
            this.grdSubMenu.Children.Add(MtMenu = new MtMenu());
        }

        private void CreateOperationWindows()
        {
            this.grdView.Children.Add(OpMain = new OpMain());
            this.grdSubMenu.Children.Add(OpMenu = new OpMenu());
        }

        private void CreateReceipeWindows()
        {
            this.grdView.Children.Add(RpMain = new RpMain());
            this.grdSubMenu.Children.Add(RpMenu = new RpMenu());
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            Thread.Sleep(10); // UI 로딩을 위한 Delay 시간

            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                ChangeLocale(this);

                var mainType = typeof(Main);
                var mainNamespace = mainType.Namespace;
                InitializeBox ib = new InitializeBox(mainNamespace, "MainInitialize", "Open Drivers");
                ib.ShowDialog();

                MtMotion.setMotionComponent(); // 장비 연결 후에 Motion component를 set 한다.

                SetStatus();

                StatusMachine.Request(StatusType.Operation.ChangedView); // 프로그램 로딩시 첫 화면
            });

            Global.IsProgramLoadComplete = true;

            _isLoop = true;

            while (_isLoop)
            {
                App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    this.Bottombar.lblNowTime.Content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                });

                Thread.Sleep(_loopInterval);
            }
        }

        private void Form_PreviewKeyDown(object sender, KeyEventArgs e)
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
                    var mainType = typeof(Main);
                    var mainNamespace = mainType.Namespace;
                    InitializeBox ib = new InitializeBox(mainNamespace, "MainInitialize", "Open Drivers");
                    ib.ShowDialog();
                }));

                Global.IsProgramLoadComplete = true;
            }
        }

        private string GetExecutingAssemblyVersion()
        {
            string version = "Version - ";

            try
            {
                // 버전 정보를 얻어온다.
                version = version + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return version;
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

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            this.Bottombar.lblVersion.Content = GetExecutingAssemblyVersion();

            //WindowState = WindowState.Maximized;

            CreateDataWindows();
            CreateReceipeWindows();
            CreateMaintenanceWindows();
            CreateOperationWindows(); // Defualt 화면을 가장 마지막에 추가[2020/09/03 djkim]

            InitControlsViewTitle();

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author : Hans, Kim
        /// Date :  2020-09-26 16:55
        /// Description : initialize view main and sub title.
        /// </summary>
        private void InitControlsViewTitle()
        {
            OpMain.lblMainTitle.Content = Bottombar.btnOperation.Content;
            OpMain.lblSubTitle.Content = OpMenu.rdbOperation.Content;

            MtMotion.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtMotion.lblSubTitle.Content = MtMenu.rdbMotion.Content;

            MtIo.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtIo.lblSubTitle.Content = MtMenu.rdbIo.Content;

            MtMonitoring.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtMonitoring.lblSubTitle.Content = MtMenu.rdbMonitoring.Content;

            MtDetector.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtDetector.lblSubTitle.Content = MtMenu.rdbDetector.Content;
            MtChamber.lblMainTitle.Content = Bottombar.btnMaintenance.Content;
            MtChamber.lblSubTitle.Content = MtMenu.rdbChamber.Content;

            DtLogger.lblMainTitle.Content = Bottombar.btnData.Content;
            DtLogger.lblSubTitle.Content = DtMenu.rdbLogger.Content;
            DtAlarm.lblMainTitle.Content = Bottombar.btnData.Content;
            DtAlarm.lblSubTitle.Content = DtMenu.rdbAlarm.Content;

            RpMain.lblMainTitle.Content = Bottombar.btnRecipe.Content;
            RpMain.lblSubTitle.Content = RpMenu.rdbRecipe.Content;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
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
                _IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                _IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            _IsInitialled = false;
            _loopInterval = 5;
            _isLoop = false; // if do you want to use the EmWorksProc(), chage to true.
                             // add your code here

            // Main
            _mainStadium = new MainStadium(this);
            _autoLocale = new AutoLocale();
            // 1 : Config
            LoadConfig();

            // 2 : Logger
            Logger.InitInstance(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.LogFilePath, Global.ConfigGeneral.LogStoragePeriodDate);

            // 3 : i18n(Multi Laguang)
            Loadi18n();

            // 4 : Alarm
            LaodAlarm();

            // 5 : Writers
            Global.AlarmWriter = new AlarmModel(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AlarmDataFilePath);

            //Todo : 나중에 변경
            if (MessageBox.ShowDialog("시뮬레이션 모드로 실행할까요?", "질문", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.YES)
            {
                Global.ConfigGeneral.RunMode = Idx.RunMode.Simulation;
            }
            else
            {
                Global.ConfigGeneral.RunMode = Idx.RunMode.Dry;
            }

            //Config Load

            bool isSimulation;

            Global.ConfigSamwonTemp1500 = new SamwonTemp1500Model();
            Global.ConfigSamwonTemp1500.Load(ref Global.ConfigSamwonTemp1500);
            Global.ConfigSamwonTemp1500.Save();

            isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

            if (Global.ConfigSamwonTemp1500.RunMode == Idx.RunMode.Simulation)
            {
                isSimulation = true;
            } // else

            Global.SamwonTemp1500 = new SamwonTemp1500(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SamwonTemp1500TagFilePath, isSimulation);

            Global.ConfigSamwonSDR100 = new SamwonSDR100Model();
            Global.ConfigSamwonSDR100.Load(ref Global.ConfigSamwonSDR100);
            Global.ConfigSamwonSDR100.Save();

            isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

            if (Global.ConfigSamwonSDR100.RunMode == Idx.RunMode.Simulation)
            {
                isSimulation = true;
            } // else

            Global.SamwonSDR100 = new SamwonSDR100(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SamwonSDR100TagFilePath, isSimulation);

            Global.ConfigAutonicsTK4S = new AutonicsTK4SModel();
            Global.ConfigAutonicsTK4S.Load(ref Global.ConfigAutonicsTK4S);
            Global.ConfigAutonicsTK4S.Save();

            Global.ConfigKeyenceIL100 = new KeyenceIL100Model();
            Global.ConfigKeyenceIL100.Load(ref Global.ConfigKeyenceIL100);
            Global.ConfigKeyenceIL100.Save();

            return Idx.FunctionResult.True;
        }

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

        /// <summary>
        /// Author :  $author$
        /// Date :  $date$
        /// Description : KeyDown Event
        /// </summary>
        private void Main_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                }//else
            }//else
        }

        private void RdbAlarm_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Data.Alarm);
            DtMenu.rdbAlarm.IsChecked = true;
        }

        private void RdbDetector_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Detector);
            MtMenu.rdbDetector.IsChecked = true;
        }

        private void RdbIo_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Io);
            MtMenu.rdbIo.IsChecked = true;
        }

        private void RdbLogger_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Data.Logger);
            DtMenu.rdbLogger.IsChecked = true;
        }

        private void RdbMonitoring_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Monitoring);
            MtMenu.rdbMonitoring.IsChecked = true;
        }

        private void RdbMotion_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Motion);
            MtMenu.rdbMotion.IsChecked = true;
        }

        private void RdbShutdown_Click(object sender, RoutedEventArgs e)
        {
            StatusMachine.Request(StatusType.Maint.Exit);
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // Main
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.Titlebar.grdTopView.MouseLeftButtonDown += GrdTop_MouseLeftButtonDown;
            this.Titlebar.MouseMove += GrdTop_MouseMove;

            this.Bottombar.btnOperation.Click += BtnOperation_Click;
            this.Bottombar.btnMaintenance.Click += BtnMaintenance_Click;
            this.Bottombar.btnRecipe.Click += BtnRecipe_Click;
            this.Bottombar.btnData.Click += BtnData_Click;
            this.Bottombar.btnLocale.Click += BtnLocale_Click;

            this.MtMenu.rdbMotion.Click += RdbMotion_Click;
            this.MtMenu.rdbIo.Click += RdbIo_Click;
            this.MtMenu.rdbMonitoring.Click += RdbMonitoring_Click;
            this.MtMenu.rdbDetector.Click += RdbDetector_Click;
            this.MtMenu.rdbChamber.Click += RdbChamber_Click;
            this.MtMenu.rdbShutdown.Click += RdbShutdown_Click;

            this.DtMenu.rdbAlarm.Click += RdbAlarm_Click;
            this.DtMenu.rdbLogger.Click += RdbLogger_Click;

            // Status
            this.btnHomeSearch.Click += BtnHomeSearch_Click;
            this.btnHomeStop.Click += BtnHomeStop_Click;

            StatusMachine.ChangedStatus += StatusMachine_ChangeStatus;

            return Idx.FunctionResult.True;
        }

        private void SetStatus()
        {
            StatusX.Initialize("X", Idx.MotionAxisNo.X, Global.AjinMotionX.GetTags());
            StatusY.Initialize("Y", Idx.MotionAxisNo.Y, Global.AjinMotionY.GetTags());
            StatusZ.Initialize("Z", Idx.MotionAxisNo.Z, Global.AjinMotionZ.GetTags());
            StatusV.Initialize("V", Idx.MotionAxisNo.V, Global.AjinMotionV.GetTags());
            StatusH.Initialize("H", Idx.MotionAxisNo.H, Global.AjinMotionH.GetTags());
            StatusR.Initialize("R", Idx.MotionAxisNo.R, Global.AjinMotionR.GetTags());
        }

        private void StatusMachine_ChangeStatus(object sendor, StatusMachine.EventChangedStatusArgs e)
        {
            _mainStadium.Switching(sendor, e);
        }

        //public void ShowRdbManual()
        //{
        //    StatusMachine.Request(StatusType.Maint.Motion);
        //    MtMenu.rdbMotion.IsChecked = true;
        //}

        //public void RdbMotion_Click(object sender, RoutedEventArgs e)
        //{
        //    ShowRdbManual();
        //}

        #endregion Methods
    }
}