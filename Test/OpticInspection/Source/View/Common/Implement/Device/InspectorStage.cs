using EmWorks.Lib.Common;
using EmWorks.View;
using System.Threading;
using System.Windows.Controls;

namespace EmWorks.App.OpticInspection
{
    public class InspectorStage : UserControl
    {
        #region Fields

        private bool _isLoop;
        private int _loopInterval;

        private double _maxPositon;
        private double _minPositon;
        private InspectorStageComponent _uiComponent;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Description :  object constructor.
        /// </summary>
        public InspectorStage(InspectorStageComponent uiComponent)
        {
            _uiComponent = uiComponent; // 실제 디자인 UI

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Description :  object destructor.
        /// </summary>
        ~InspectorStage()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private bool CheckDeviceReadyStatus()
        {
            if (Global._PmacX3.sxDeviceMotion.Is == null ||
                        Global._PmacX3.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Disable.ToString())
            {
                return false;
            }

            return true;
        }

        private void CheckStatus()
        {
            try
            {
                // Sht X1
                Global._MotionControl.ChangStatus(_uiComponent.tgbServo, Global._PmacX3.xMotionServoOn);
                Global._MotionControl.ChangStatus(_uiComponent.tgbAlarm, Global._PmacX3.xMotionAlarmStatus, true);
                Global._MotionControl.ChangStatus(_uiComponent.tgbBusy, Global._PmacX3.xMotionSignalBusy);
                Global._MotionControl.ChangStatus(_uiComponent.tgbNeg, Global._PmacX3.xMotionSignalNegativeLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.tgbPos, Global._PmacX3.xMotionSignalPositiveLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.tgbOrg, Global._PmacX3.xMotionSignalInpos);
                Global._MotionControl.ChangeCurrentPosition(_uiComponent.sldShtX3,
                                                            Global._PmacX3.xMotionActual,
                                                            _maxPositon, _minPositon);
            }
            catch (System.Exception ex)
            {
                //int a = index;
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-01 11:30 (create or edit date.)
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            try
            {
                while (_isLoop)
                {
                    if (Global._IsProgramLoadComplete == false)
                    {
                        continue;
                    }

                    if (CheckDeviceReadyStatus() == false)
                    {
                        continue;
                    }

                    GetMotionData();

                    this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        CheckStatus();
                    });

                    _loopInterval = 100;

                    Thread.Sleep(_loopInterval);
                    Func.DoEvents();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void GetMotionData()
        {
            Global._MotionControl.GetMaxMinPosition(Idx.MotionAxisName.X3,
                                                    out _maxPositon,
                                                    out _minPositon);
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-01 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // ShtX1 Component
            _uiComponent.tgbAlarm.IsChecked = false;
            _uiComponent.tgbBusy.IsChecked = false;
            _uiComponent.tgbNeg.IsChecked = false;
            _uiComponent.tgbOrg.IsChecked = false;
            _uiComponent.tgbPos.IsChecked = false;
            _uiComponent.tgbServo.IsChecked = false;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-01 11:30 (create or edit date.)
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
                _isLoop = true;

                ThreadPool.QueueUserWorkItem(EmWorksProc);
                Func.DoEvents();
            }
            else
            {
                _IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-01 11:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;
                _loopInterval = 5;
                _isLoop = false;

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
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}