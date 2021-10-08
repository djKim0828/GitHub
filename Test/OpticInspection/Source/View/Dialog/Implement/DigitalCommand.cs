using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="DigitalCommand"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-08 12:40
    /// Description : object detail description.
    /// </summary>
    public class DigitalCommand : EmWorks.View.DigitalCommandDialog
    {
        #region Fields

        private int _interval = 1000;
        private bool _toggle = false;
        private Tag TempTag;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description :  objcet constructor.
        /// </summary>
        public DigitalCommand(Tag tag)
        {
            Initialize();

            TempTag = tag;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description :  object destructor.
        /// </summary>
        ~DigitalCommand()
        {
            // N/A
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public void Toggle()
        {
            if (_interval <= 0)
                _interval = 100;
            while (_toggle == true)
            {
                TempTag.Is = TempTag.n == 0 ? Idx.DigitalValue.Enable : Idx.DigitalValue.Disable;
                Thread.Sleep(_interval);
                if (_toggle == false)
                    break;
            }
        }

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeValue(Idx.DigitalValue.Disable);
        }

        private void BtnON_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeValue(Idx.DigitalValue.Enable);
        }

        private void ChangeValue(int value)
        {
            try
            {                

                _toggle = false;

                TempTag.Is = value;

                if (chkExit.IsChecked == true)
                {
                    if (txtInterval.IsFocused == false)
                        BtnDlgClose_Click(null, null);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }


        private void DigitalCommand_Closed(object sender, EventArgs e)
        {
            _toggle = false;
        }

        private void DigitalCommand_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    if (txtInterval.IsFocused)
                        return;
                    BtnOff_Click(this, null);
                    break;

                case Key.D1:
                case Key.NumPad1:
                    if (txtInterval.IsFocused)
                        return;
                    BtnON_Click(this, null);
                    break;

                case Key.Escape:
                    BtnDlgClose_Click(this, null);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            chkExit.IsChecked = true;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == 0 && resultLocale == 0 && resultControls == 0 && resultEvent == 0)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            IsInitialled = false;
            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {

            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-08 12:40
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += DigitalCommand_Loaded;
            this.MouseMove += Dialog_MouseMove;
            this.PreviewKeyDown += DigitalCommand_PreviewKeyDown;
            this.Closed += DigitalCommand_Closed;

            this.btnOff.Click += BtnOff_Click;
            this.btnON.Click += BtnON_Click;
            this.btnDlgClose.Click += BtnDlgClose_Click;

            return Idx.FunctionResult.True;
        }


        private void Dialog_MouseMove(object sender, MouseEventArgs e)
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

        private void DigitalCommand_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();

            if (TempTag.Identity.IoType != EmWorks.Lib.Core.Tag.Idx.IoType.Output || TempTag.Identity.DataType != EmWorks.Lib.Core.Tag.Idx.DataType.Int)
            {
                Logger.Error("Can't open the DigitalCommanddialgo : check the command type");
                this.Close();
            }
            else
            {
                lblDlgTitle.Content = TempTag.Name;
            }
        }

        #endregion Methods
    }
}