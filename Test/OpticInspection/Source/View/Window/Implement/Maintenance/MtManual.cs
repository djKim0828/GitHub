using EmWorks.Lib.Common;
using EmWorks.View;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="MtManual"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class MtManual : EmWorks.View.MtManualWindow
    {
        #region Fields

        private MtSetupMotionWindow _CameraZ4;
        private MtSetupMotionWindow _InspectorX3;
        private int _isFirstVisible = 0;
        private bool _isLoop;

        private int _loopInterval;
        private MtSetupMotionWindow _ShuttleX1;
        private MtSetupMotionWindow _ShuttleY2;
        private MtSetupMotionWindow _Sr5000Z5;

        #endregion Fields

        //private MtSetupMotionWindow _ProveX6;
        //private MtSetupMotionWindow _ProbeY7;
        //private MtSetupMotionWindow _ProbeC8;
        //private MtSetupMotionWindow _ProbeZ9;

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public MtManual()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~MtManual()
        {
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void ChangeTabControl(string controlIndex)
        {
            try
            {
                InitUserControl();

                // 일단 모두 안보이게
                ChangeVisibleAllUc(System.Windows.Visibility.Hidden);

                int index = Convert.ToInt16(controlIndex);

                switch (index)
                {
                    case Idx.MotionAxisNo.ShuttleX1:
                        _ShuttleX1.Visibility = System.Windows.Visibility.Visible;
                        btnShtX1.IsChecked = true;
                        rdbShtX1.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.ShuttleY2:
                        _ShuttleY2.Visibility = System.Windows.Visibility.Visible;
                        btnShtY2.IsChecked = true;
                        rdbShtY2.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.InspectorX3:
                        _InspectorX3.Visibility = System.Windows.Visibility.Visible;
                        btnInspX3.IsChecked = true;
                        rdbInspX3.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.CameraZ4:
                        _CameraZ4.Visibility = System.Windows.Visibility.Visible;
                        btnCameraZ4.IsChecked = true;
                        rdbCameraZ4.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.Sr5000Z5:
                        _Sr5000Z5.Visibility = System.Windows.Visibility.Visible;
                        btnSr5000Z5.IsChecked = true;
                        rdbSr5000Z5.IsChecked = true;
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
            _ShuttleX1.Visibility = visible;
            _ShuttleY2.Visibility = visible;
            _InspectorX3.Visibility = visible;
            _Sr5000Z5.Visibility = visible;
            _CameraZ4.Visibility = visible;
        }

        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                // add your code here
                Thread.Sleep(_loopInterval);
            }
        }

        /// <summary>
        /// Author : Hans, KIM
        /// Date :  2020-09-14 20:15 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            grdSetup.Children.Clear();

            return Idx.FunctionResult.True;
        }

        private void Initialize()
        {
            int resultInstance = InitInstance();

            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                _IsInitialled = false;
            }
        }

        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;
                _loopInterval = 5;
                _isLoop = false;

                return Idx.FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private void InitUserControl()
        {
            if (_ShuttleX1 == null)
            {
                _ShuttleX1 = new MtSetupMotion(Idx.MotionAxisNo.ShuttleX1, Global._PmacX1.GetTags());
                _ShuttleX1.Tag = Idx.IsVibibleChange.NoChange;
                grdSetup.Children.Add(_ShuttleX1);
            } // else

            if (_ShuttleY2 == null)
            {
                _ShuttleY2 = new MtSetupMotion(Idx.MotionAxisNo.ShuttleY2, Global._PmacY2.GetTags());
                _ShuttleY2.Tag = Idx.IsVibibleChange.NoChange;
                grdSetup.Children.Add(_ShuttleY2);
            } // else

            if (_InspectorX3 == null)
            {
                _InspectorX3 = new MtSetupMotion(Idx.MotionAxisNo.InspectorX3, Global._PmacX3.GetTags());
                _InspectorX3.Tag = Idx.IsVibibleChange.NoChange;
                grdSetup.Children.Add(_InspectorX3);
            } // else

            if (_CameraZ4 == null)
            {
                _CameraZ4 = new MtSetupMotion(Idx.MotionAxisNo.CameraZ4, Global._PmacZ4.GetTags());
                _CameraZ4.Tag = Idx.IsVibibleChange.NoChange;
                grdSetup.Children.Add(_CameraZ4);
            } // else

            if (_Sr5000Z5 == null)
            {
                _Sr5000Z5 = new MtSetupMotion(Idx.MotionAxisNo.Sr5000Z5, Global._PmacZ5.GetTags());
                _Sr5000Z5.Tag = Idx.IsVibibleChange.NoChange;
                grdSetup.Children.Add(_Sr5000Z5);
            } // else
        }

        private void MtManual_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
                    ChangeTabControl(btnShtX1.Uid);

                    _isFirstVisible = 2;
                } //else
            }
        }

        private void RadioButton_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangeTabControl(((Control)sender).Uid.ToString());
        }

        private int RegistEvents()
        {
            this.IsVisibleChanged += MtManual_IsVisibleChanged; ;

            this.btnShtX1.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.btnShtY2.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.btnInspX3.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.btnCameraZ4.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.btnSr5000Z5.PreviewMouseUp += RadioButton_PreviewMouseUp;

            this.rdbShtX1.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbShtY2.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbInspX3.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbCameraZ4.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbSr5000Z5.PreviewMouseUp += RadioButton_PreviewMouseUp;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}