using EmWorks.Lib.Common;
using EmWorks.View;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="MtMotion"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class MtMotion : EmWorks.View.MtMotionWindow
    {
        #region Fields

        private SetupAllMotion _allMotion;
        private bool _isLoop;
        private int _locale;
        private int _loopInterval;
        private SetupMotionComponent _motionH;
        private SetupMotionComponent _motionR;
        private SetupMotionComponent _motionV;
        private SetupMotionComponent _motionX;
        private SetupMotionComponent _motionY;
        private SetupMotionComponent _motionZ;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public MtMotion()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~MtMotion()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public void setMotionComponent()
        {
            AddMotionComponent();
            ChangeTabControl(rdbAll.Uid);
        }

        private void AddMotionComponent()
        {
            if (_allMotion == null)
            {
                _allMotion = new SetupAllMotion();
                _allMotion.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_allMotion);
            } // else

            if (_motionX == null)
            {
                _motionX = new SetupMotion(Idx.MotionAxisNo.X, Global.AjinMotionX.GetTags());
                _motionX.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionX);
            } // else

            if (_motionY == null)
            {
                _motionY = new SetupMotion(Idx.MotionAxisNo.Y, Global.AjinMotionY.GetTags());
                _motionY.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionY);
            } // else

            if (_motionZ == null)
            {
                _motionZ = new SetupMotion(Idx.MotionAxisNo.Z, Global.AjinMotionZ.GetTags());
                _motionZ.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionZ);
            } // else

            if (_motionV == null)
            {
                _motionV = new SetupMotion(Idx.MotionAxisNo.V, Global.AjinMotionV.GetTags());
                _motionV.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionV);
            } // else

            if (_motionH == null)
            {
                _motionH = new SetupMotion(Idx.MotionAxisNo.H, Global.AjinMotionH.GetTags());
                _motionH.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionH);
            } // else

            if (_motionR == null)
            {
                _motionR = new SetupMotion(Idx.MotionAxisNo.R, Global.AjinMotionR.GetTags());
                _motionR.Tag = Idx.IsVibibleChange.NoChange;
                grdSetupMotion.Children.Add(_motionR);
            } // else
        }

        private void ChangeTabControl(string controlIndex)
        {
            try
            {
                // 일단 모두 안보이게
                ChangeVisibleAllUc(System.Windows.Visibility.Hidden);

                int index = Convert.ToInt16(controlIndex);

                switch (index)
                {
                    case Idx.MotionAxisNo.X:
                        _motionX.Visibility = System.Windows.Visibility.Visible;
                        rdbX.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.Y:
                        _motionY.Visibility = System.Windows.Visibility.Visible;
                        rdbY.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.Z:
                        _motionZ.Visibility = System.Windows.Visibility.Visible;
                        rdbZ.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.V:
                        _motionV.Visibility = System.Windows.Visibility.Visible;
                        rdbV.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.H:
                        _motionH.Visibility = System.Windows.Visibility.Visible;
                        rdbH.IsChecked = true;
                        break;

                    case Idx.MotionAxisNo.R:
                        _motionR.Visibility = System.Windows.Visibility.Visible;
                        rdbR.IsChecked = true;
                        break;

                    default:
                        _allMotion.Visibility = System.Windows.Visibility.Visible;
                        rdbAll.IsChecked = true;
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
            _allMotion.Visibility = visible;
            _motionX.Visibility = visible;
            _motionY.Visibility = visible;
            _motionZ.Visibility = visible;
            _motionV.Visibility = visible;
            _motionH.Visibility = visible;
            _motionR.Visibility = visible;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                Thread.Sleep(_loopInterval);
            }
        }

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            grdSetupMotion.Children.Clear();

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

            return Idx.FunctionResult.True;
        }        

        private void RadioButton_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangeTabControl(((Control)sender).Uid.ToString());
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {            
            this.rdbAll.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbX.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbY.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbZ.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbV.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbH.PreviewMouseUp += RadioButton_PreviewMouseUp;
            this.rdbR.PreviewMouseUp += RadioButton_PreviewMouseUp;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}