using EmWorks.Lib.Common;

namespace FPMS.E2105_FS111_121
{
    public class SetupAllMotion : EmWorks.View.SetupAllMotionComponent
    {
        #region Fields

        private bool _isLoad = false;
        private JogControl ucJog;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : dongjun, Kim
        /// Description :  object constructor.
        /// </summary>
        public SetupAllMotion()
        {
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnCenterGetPos_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtCenterPosX.Text = ucJog.lblCurPosDataX.Content.ToString();
            txtCenterPosY.Text = ucJog.lblCurPosDataY.Content.ToString();
            txtCenterPosZ.Text = ucJog.lblCurPosDataZ.Content.ToString();
            txtCenterPosV.Text = ucJog.lblCurPosDataV.Content.ToString();
            txtCenterPosH.Text = ucJog.lblCurPosDataH.Content.ToString();
            txtCenterPosR.Text = ucJog.lblCurPosDataR.Content.ToString();
        }

        private void BtnHomeSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommandHome(Global.AjinMotionX, Idx.MotionAxisNo.X);
            CommandHome(Global.AjinMotionY, Idx.MotionAxisNo.Y);
            CommandHome(Global.AjinMotionZ, Idx.MotionAxisNo.Z);
            CommandHome(Global.AjinMotionV, Idx.MotionAxisNo.V);
            CommandHome(Global.AjinMotionH, Idx.MotionAxisNo.H);
            CommandHome(Global.AjinMotionR, Idx.MotionAxisNo.R);
        }

        private void BtnHomeStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommandStop();
        }

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveData();
        }

        private void BtnServerOn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (btnServerOn.IsChecked == true)
            {
                CommandServoOn(Idx.DigitalValue.Enable);
            }
            else
            {
                CommandServoOn(Idx.DigitalValue.Disable);
            }
        }

        private void CommandHome(AjinMotion ajinMotion, int axisNo)
        {

            //Todo : 장비 테스트시 순서 등 재검토 필요
            //Todo : Opertiation 작업시 재검토 필요
            //Todo : 홈서치 타임아웃 등 검증 루틴 추가 필요
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

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            grdjog.Children.Clear();
            grdjog.Children.Add(ucJog);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
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
        /// Date :  2021-01-27 12:00
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                ucJog = new JogControl();
                ucJog.Tag = Idx.IsVibibleChange.NoChange;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private void LoadData()
        {
            txtCenterPosX.Text = Global.ConfigGeneral.CenterPosX;
            txtCenterPosY.Text = Global.ConfigGeneral.CenterPosY;
            txtCenterPosZ.Text = Global.ConfigGeneral.CenterPosZ;
            txtCenterPosV.Text = Global.ConfigGeneral.CenterPosV;
            txtCenterPosH.Text = Global.ConfigGeneral.CenterPosH;
            txtCenterPosR.Text = Global.ConfigGeneral.CenterPosR;

            _isLoad = true;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // Dialog
            this.Loaded += SetupAllMotion_Loaded;
            this.IsVisibleChanged += SetupAllMotion_IsVisibleChanged;

            // Command
            this.btnServerOn.Click += BtnServerOn_Click;
            this.btnHomeSearch.Click += BtnHomeSearch_Click;
            this.btnHomeStop.Click += BtnHomeStop_Click;

            //Center
            this.btnCenterGetPos.Click += BtnCenterGetPos_Click;
            this.btnSave.Click += BtnSave_Click;

            return Idx.FunctionResult.True;
        }

        private void SaveData()
        {
            if (_isLoad != true)
            {
                return;
            }// else

            Global.ConfigGeneral.CenterPosX = txtCenterPosX.Text;
            Global.ConfigGeneral.CenterPosY = txtCenterPosY.Text;
            Global.ConfigGeneral.CenterPosZ = txtCenterPosZ.Text;
            Global.ConfigGeneral.CenterPosV = txtCenterPosV.Text;
            Global.ConfigGeneral.CenterPosH = txtCenterPosH.Text;
            Global.ConfigGeneral.CenterPosR = txtCenterPosR.Text;

            Global.ConfigGeneral.Save();
        }

        private void SetupAllMotion_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == false)
            {
                SaveData();
            } // else
        }

        private void SetupAllMotion_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion Methods
    }
}