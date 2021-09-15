using EmWorks.Lib.Common;
using EmWorks.View;
using System;
using System.Threading;
using System.Windows;

namespace FPMS.E2105_FS111_121
{    /// <summary>
     /// Copyright @ ENC Technology Co.,Ltd.
     /// Class Name : <see cref="MtDetector"/>
     /// Author : Andrew, Yoon
     /// Description : object detail description.
     /// </summary>
    public class MtDetector : EmWorks.View.MtDetectorWindow
    {
        #region Fields

        private AlignLMKComponent _alignLMK;
        private ControlPatternComponent _controlPattern;
        private int _isFirstVisible = 0;
        private bool _isLoop;
        private KeyenceIL100Component _keyenceIl100;
        private int _locale;
        private int _loopInterval;
        private MicroshakeComponent _microshake;

        enum LMKView
        {
            Measure =0,
            Setup =1,
        }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public MtDetector()
        {
            Initialize();
            // add your code here
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~MtDetector()
        {
            _isLoop = false;
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        private MeasureLMK _measureLMK { get; set; }
        private SetupLMK _setupLMK { get; set; }

        #endregion Properties

        #region Methods

        private void ChangeTabControl(string controlIndex)
        {
            try
            {
                InitUserControl();

                // 일단 모두 안보이게
                ChangeVisibleAllUc(System.Windows.Visibility.Hidden);

                LMKView index = (LMKView)Convert.ToInt16(controlIndex);

                switch (index)
                {
                    case LMKView.Measure:
                        _measureLMK.Visibility = System.Windows.Visibility.Visible;
                        this.rdbBasicMeasure.IsChecked = true;
                        break;

                    case LMKView.Setup:
                        _setupLMK.Visibility = System.Windows.Visibility.Visible;
                        this.rdbSetupCamera.IsChecked = true;
                        break;

                    default:
                        // N/A
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void ChangeVisibleAllUc(Visibility visible)
        {
            _measureLMK.Visibility = visible;
            _setupLMK.Visibility = visible;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                // add your code here
                Thread.Sleep(_loopInterval);
            }
        }

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // add your code here

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
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

            return Idx.FunctionResult.True;
        }

        private void InitUserControl()
        {
            if (_alignLMK == null)
            {
                //_alignLMK = new SetupMotion(Idx.MotionAxisNo.X, Global.AlignLMK.GetTags());
            } // else

            if (_controlPattern == null)
            {
                // _controlPattern = new SetupMotion(Idx.MotionAxisNo.X, Global.AlignLMK.GetTags());
            } // else

            if (_keyenceIl100 == null)
            {
                //_keyenceIl100 = new SetupMotion(Idx.MotionAxisNo.X, Global.KeyenceIL100.GetTags());
            } // else

            if (_microshake == null)
            {
                // _microshake = new SetupMotion(Idx.MotionAxisNo.X, Global.AlignLMK.GetTags());
            } // else

            if (_measureLMK == null)
            {
                //_motionX = new SetupMotion(Idx.MotionAxisNo.X, Global._AjinMotionX.GetTags());
                _measureLMK = new MeasureLMK();
                _measureLMK.Tag = Idx.IsVibibleChange.NoChange;
                grdTabControl.Children.Add(_measureLMK);
            } // else

            if (_setupLMK == null)
            {
                //_motionY = new SetupMotion(Idx.MotionAxisNo.X, Global._AjinMotionY.GetTags());
                _setupLMK = new SetupLMK();
                _setupLMK.Tag = Idx.IsVibibleChange.NoChange;
                grdTabControl.Children.Add(_setupLMK);
            } // else
        }

        private void MtLMK_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == true)
            {
                if (_isFirstVisible == 0)
                {
                    _isFirstVisible = 1;
                }
                else if (_isFirstVisible == 1)
                {
                    // 한번만 실행
                    ChangeTabControl(rdbBasicMeasure.Uid);

                    _isFirstVisible = 2;
                } //else
            }
        }

        private void RdbBasicMeasure_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeTabControl(rdbBasicMeasure.Uid);
        }

        private void RdbSetupCamera_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeTabControl(rdbSetupCamera.Uid);
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.IsVisibleChanged += MtLMK_IsVisibleChanged;

            this.rdbBasicMeasure.Click += RdbBasicMeasure_Click; ;
            this.rdbSetupCamera.Click += RdbSetupCamera_Click; ;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}