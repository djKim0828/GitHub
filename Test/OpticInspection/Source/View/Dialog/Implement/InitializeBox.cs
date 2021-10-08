using EmWorks.Lib.Common;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmWorks.App.OpticInspection
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
        private bool _IsAutoClose = false;
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

            _IsAutoClose = isAutoClose;

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
                }
            }
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
            }
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

            this.Top = 100;
            this.Left = 100;

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
                }

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
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;

            this.btnClose.Click += BtnDlgClose_Click;

            this.btnOK.Click += BtnOK_Click;

            this.Loaded += InitlizeBox_Loaded;

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
                if (_IsAutoClose == true)
                {
                    this.Close();
                }
            });
        }

        private void CheckDevices()
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                if (Global._PmacX1 != null)
                {
                    if (Global._PmacX1.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdShtX1Status, Idx.DigitalValue.Enable);
                    } // else
                }

                if (Global._PmacY2 != null)
                {
                    if (Global._PmacY2.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdShtY2Status, Idx.DigitalValue.Enable);
                    } // else
                }

                if (Global._PmacX3 != null)
                {
                    if (Global._PmacX3.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdShtX3Status, Idx.DigitalValue.Enable);
                    } // else
                }

                if (Global._PmacZ4 != null)
                {
                    if (Global._PmacZ4.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdCameraZ4Status, Idx.DigitalValue.Enable);
                    } // else
                }

                if (Global._PmacZ5 != null)
                {
                    if (Global._PmacZ5.sxDeviceMotion.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdSr5000Z5Status, Idx.DigitalValue.Enable);
                    } // else   
                }

                if (Global._TopconSR5000 != null)
                {
                    if (Global._TopconSR5000.sxDeviceSR5000.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        ChangStatus(grdSr5000Z5Status, Idx.DigitalValue.Enable);
                    } // else   
                }

            });
        }

        private int ChangStatus(Grid grid, int enable)
        {

            if (enable == Idx.DigitalValue.Enable)
            {
                grid.Background = System.Windows.Media.Brushes.Green;
                return Idx.DigitalValue.Enable;
            }
            else if (enable == Idx.DigitalValue.Disable)
            {
                grid.Background = System.Windows.Media.Brushes.Maroon;
                return Idx.DigitalValue.Disable;
            }
            else
            {
                grid.Background = System.Windows.Media.Brushes.Gray;
                return Idx.DigitalValue.UnKnow;
            }
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