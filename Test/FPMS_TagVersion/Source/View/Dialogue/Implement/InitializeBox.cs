using EmWorks.Lib.Common;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="InitializeBox"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-01-26 15:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class InitializeBox : EmWorks.View.InitalizeBoxDialog
    {
        #region Fields

        private string _className;
        private object _classObject;
        private MethodInfo[] _functions;
        private bool _isAutoClose = false;
        private string _namespaceName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  objcet constructor.
        /// </summary>
        public InitializeBox(string namespaceName, string className, string title, bool isAutoClose = false)
        {
            Initialize();

            _namespaceName = namespaceName;
            _className = className;

            _isAutoClose = isAutoClose;

            lblDlgTitle.Content = title;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~InitializeBox()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private int ChangStatus(Ellipse control, int enable)
        {
            if (enable == Idx.DigitalValue.Enable)
            {
                control.Stroke = System.Windows.Media.Brushes.Green;
                return Idx.DigitalValue.Enable;
            }
            else if (enable == Idx.DigitalValue.Disable)
            {
                control.Stroke = System.Windows.Media.Brushes.Maroon;
                return Idx.DigitalValue.Disable;
            }
            else
            {
                control.Stroke = System.Windows.Media.Brushes.Gray;
                return Idx.DigitalValue.UnKnow;
            }
        }

        private void CheckDevices()
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                CheckIoStatus();
                CheckMotionStatus(Global.AjinMotionX, elsMotionX);
                CheckMotionStatus(Global.AjinMotionY, elsMotionY);
                CheckMotionStatus(Global.AjinMotionZ, elsMotionZ);
                CheckMotionStatus(Global.AjinMotionV, elsMotionV);
                CheckMotionStatus(Global.AjinMotionH, elsMotionH);
                CheckMotionStatus(Global.AjinMotionR, elsMotionR);
            });
        }

        private void CheckIoStatus()
        {
            if (Global.AjinIo != null)
            {
                if (Global.AjinIo.sxDeviceIo.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    ChangStatus(elsIo, Idx.DigitalValue.Enable);
                } // else
            }// else
        }

        private void CheckMotionStatus(AjinMotion motoion, Ellipse control)
        {
            if (motoion != null)
            {
                if (motoion.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    ChangStatus(control, Idx.DigitalValue.Enable);
                } // else
            }// else
        }        

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : Mouse Move Event
        /// </summary>
        private void Dialog_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
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

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            } // else
        }

        private bool GetFunctions(object targetClass)
        {
            try
            {
                _functions = targetClass.GetType().
                    GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            this.ShowInTaskbar = false;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            
            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private void InitlizeBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();

            try
            {
                Assembly assembly = Assembly.Load(_namespaceName);

                Type type = assembly.GetType(_namespaceName + "." + _className);// 클래스 이름에 해당하는 Type을 가져옮
                _classObject = Activator.CreateInstance(type);

                if (GetFunctions(_classObject) == false)
                {
                    MessageBox.Show("Check a namespace name or a class name.");
                    this.Close();
                    return;
                } // else

                pgbProcessStatue.Maximum = _functions.Length;

                Action action = () =>
                {
                    RunProcess();
                };

                action.BeginInvoke(null, null);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Check a namespace name or a class name.");
                this.Close();
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-26 15:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += InitlizeBox_Loaded;
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;

            this.btnClose.Click += BtnDlgClose_Click;
            this.btnOK.Click += BtnOK_Click;

            return Idx.FunctionResult.True;
        }

        private void RunProcess()
        {
            foreach (MethodInfo mi in _functions)
            {
                WriteLog("Start Function [ " + mi.Name + "]");

                object[] parametersArray = new object[] { "" };

                object result = mi.Invoke(_classObject, new object[] { });

                if ((bool)result == false)
                {
                    WriteLog("***** Failed [ " + mi.Name + "]");
                }
                else
                {
                    WriteLog("Success [ " + mi.Name + "]");
                }

                CheckDevices();

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    pgbProcessStatue.Value++;
                });
            }

            WriteLog("**** Initialize Done ****");

            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                if (_isAutoClose == true)
                {
                    this.Close();
                }
            });
        }

        private void WriteLog(string data)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                txtOutput.AppendText(data + "\n");
                txtOutput.ScrollToEnd();
            });
        }

        #endregion Methods
    }
}