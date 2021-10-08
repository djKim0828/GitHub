using EmWorks.Lib.Common;
using EmWorks.View;
using System.Threading;
using System.Windows.Controls;

namespace EmWorks.App.OpticInspection
{
    public class ShuttleStage : UserControl
    {
        #region Fields

        private bool _isLoop;
        private int _loopInterval;

        private double _shtX1MaxPositon;
        private double _shtX1MinPositon;
        private double _shtY2MaxPositon;
        private double _shtY2MinPositon;
        private ShuttleStageComponent _uiComponent;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Description :  object constructor.
        /// </summary>
        public ShuttleStage(ShuttleStageComponent uiComponent)
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
        ~ShuttleStage()
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
            if (Global._PmacX1.sxDeviceMotion.Is == null ||
                        Global._PmacX1.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Disable.ToString())
            {
                return false;
            }

            if (Global._PmacY2.sxDeviceMotion.Is == null ||
                        Global._PmacY2.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Disable.ToString())
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
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbServo, Global._PmacX1.xMotionServoOn);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbAlarm, Global._PmacX1.xMotionAlarmStatus, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbBusy, Global._PmacX1.xMotionSignalBusy);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbNeg, Global._PmacX1.xMotionSignalNegativeLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbPos, Global._PmacX1.xMotionSignalPositiveLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtX1.tgbOrg, Global._PmacX1.xMotionSignalInpos);
                Global._MotionControl.ChangeCurrentPosition(_uiComponent.sldShtX1, Global._PmacX1.xMotionActual, _shtX1MaxPositon, _shtX1MinPositon);

                // Sht Y2
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbServo, Global._PmacY2.xMotionServoOn);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbAlarm, Global._PmacY2.xMotionAlarmStatus, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbBusy, Global._PmacY2.xMotionSignalBusy);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbNeg, Global._PmacY2.xMotionSignalNegativeLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbPos, Global._PmacY2.xMotionSignalPositiveLimit, true);
                Global._MotionControl.ChangStatus(_uiComponent.cpnShtY2.tgbOrg, Global._PmacY2.xMotionSignalInpos);
                Global._MotionControl.ChangeCurrentPosition(_uiComponent.sldShtY2, Global._PmacY2.xMotionActual, _shtY2MaxPositon, _shtX1MinPositon);
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
                    } //else

                    if (CheckDeviceReadyStatus() == false)
                    {
                        continue;
                    } // else

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
            Global._MotionControl.GetMaxMinPosition(Idx.MotionAxisName.X1, out _shtX1MaxPositon, out _shtX1MinPositon);
            Global._MotionControl.GetMaxMinPosition(Idx.MotionAxisName.Y2, out _shtY2MaxPositon, out _shtY2MinPositon);
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-01 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // ShtX1 Component
            _uiComponent.cpnShtX1.tgbAlarm.IsChecked = false;
            _uiComponent.cpnShtX1.tgbBusy.IsChecked = false;
            _uiComponent.cpnShtX1.tgbNeg.IsChecked = false;
            _uiComponent.cpnShtX1.tgbOrg.IsChecked = false;
            _uiComponent.cpnShtX1.tgbPos.IsChecked = false;
            _uiComponent.cpnShtX1.tgbServo.IsChecked = false;

            // ShtY2 Component
            _uiComponent.cpnShtY2.tgbAlarm.IsChecked = false;
            _uiComponent.cpnShtY2.tgbBusy.IsChecked = false;
            _uiComponent.cpnShtY2.tgbNeg.IsChecked = false;
            _uiComponent.cpnShtY2.tgbOrg.IsChecked = false;
            _uiComponent.cpnShtY2.tgbPos.IsChecked = false;
            _uiComponent.cpnShtY2.tgbServo.IsChecked = false;

            // WaferChuck Component
            _uiComponent.cpnwaferChuck.tgbReady.IsChecked = true;
            _uiComponent.cpnwaferChuck.tgbWafer.IsChecked = false;

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