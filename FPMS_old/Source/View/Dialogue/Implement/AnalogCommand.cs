using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="InputBox"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-03 11:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class AnalogCommand : EmWorks.View.AnalogCommandDialog
    {
        #region Fields

        private bool _isFirst = true;
        private double _result;

        private Tag _tempTag;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  objcet constructor.
        /// </summary>
        public AnalogCommand(Tag tag)
        {
            _tempTag = tag;

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~AnalogCommand()
        {
            
        }

        #endregion Destructors

        #region Properties

        public bool _IsEnter { get; set; }
        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnBackspace_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txbInputText.Text.Length > 1)
            {
                txbInputText.Text = txbInputText.Text.Substring(0, txbInputText.Text.Length - 1);
            }
            else
            {
                txbInputText.Text = "0";
            }
        }

        private void BtnCe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txbInputText.Text = "0";
        }

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _IsEnter = false;

            this.Close();
        }

        private void btnDot_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txbInputText.Text.Contains("."))
            {
                return;
            }
            else
            {
                txbInputText.Text += ".";
            }
        }

        private void BtnEnter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (_tempTag != null)
                {
                    _tempTag.Is = Convert.ToDouble(txbInputText.Text);
                } // else
            }
            catch
            {
                // N/A
            }
            finally
            {
                _IsEnter = true;

                this.Close();
            }
        }

        private void BtnNumber_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                MakeValue(btn.Content.ToString());
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            try
            {
                if (_tempTag != null)
                {
                    txbInputText.Text = _tempTag.d.ToString();
                } // else
            }
            catch
            {
                 // n/a
            }

            txbInputText.Text = Emu.CommaProcedure(txbInputText.Text);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
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
            }
            else
            {
                _IsInitialled = false;
            }

        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            _IsInitialled = false;

            _result = 0;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {

            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        private void InputBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    BtnNumber_Click(btn0, null);
                    break;

                case Key.D1:
                case Key.NumPad1:
                    BtnNumber_Click(btn1, null);
                    break;

                case Key.D2:
                case Key.NumPad2:
                    BtnNumber_Click(btn2, null);
                    break;

                case Key.D3:
                case Key.NumPad3:
                    BtnNumber_Click(btn3, null);
                    break;

                case Key.D4:
                case Key.NumPad4:
                    BtnNumber_Click(btn4, null);
                    break;

                case Key.D5:
                case Key.NumPad5:
                    BtnNumber_Click(btn5, null);
                    break;

                case Key.D6:
                case Key.NumPad6:
                    BtnNumber_Click(btn6, null);
                    break;

                case Key.D7:
                case Key.NumPad7:
                    BtnNumber_Click(btn7, null);
                    break;

                case Key.D8:
                case Key.NumPad8:
                    BtnNumber_Click(btn8, null);
                    break;

                case Key.D9:
                case Key.NumPad9:
                    BtnNumber_Click(btn9, null);
                    break;

                case Key.Back:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        BtnCe_Click(this, null);
                    else
                        BtnBackspace_Click(this, null);
                    break;

                case Key.Escape:
                    BtnDlgClose_Click(this, null);
                    break;

                case Key.Enter:
                    BtnEnter_Click(this, null);
                    break;

                default:
                    break;
            }
        }

        private double MakeValue(string value)
        {
            try
            {
                if (txbInputText.Text != "0.")
                {
                    if (txbInputText.Text == "0" || (Convert.ToDouble(txbInputText.Text) == _tempTag.d && _isFirst == true) || Convert.ToDouble(txbInputText.Text) == _result)
                    {
                        txbInputText.Text = "";
                        _isFirst = false;
                    } // else
                } // else

                txbInputText.Text += value;

                txbInputText.Text = Emu.CommaProcedure(txbInputText.Text);
            }
            catch
            {
                // n/a
            }

            return Convert.ToDouble(txbInputText.Text);
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += InputBox_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;
            this.Loaded += AnalogCommand_Loaded;
            this.btnDlgClose.Click += BtnDlgClose_Click;
            this.btnEnter.Click += BtnEnter_Click;

            this.btnCe.Click += BtnCe_Click;
            this.btnBackspace.Click += BtnBackspace_Click;

            this.btn1.Click += BtnNumber_Click;
            this.btn2.Click += BtnNumber_Click;
            this.btn3.Click += BtnNumber_Click;
            this.btn4.Click += BtnNumber_Click;
            this.btn5.Click += BtnNumber_Click;
            this.btn6.Click += BtnNumber_Click;
            this.btn7.Click += BtnNumber_Click;
            this.btn8.Click += BtnNumber_Click;
            this.btn9.Click += BtnNumber_Click;
            this.btn0.Click += BtnNumber_Click;
            this.btn00.Click += BtnNumber_Click;
            this.btnDot.Click += btnDot_Click;

            return Idx.FunctionResult.True;
        }

        private void AnalogCommand_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();
        }

        private void Dialog_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 50)
                {
                    DragMove();
                } // else
            } // else
        }
        #endregion Methods
    }
}
