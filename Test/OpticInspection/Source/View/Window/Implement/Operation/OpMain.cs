using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Windows.Media;
using System.IO;
using EmWorks.Lib.Common;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="OpMain"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class OpMain : View.Window.Design.Operation.OpMainWindow
    {
        #region locale variable

        private CameraStage _CameraStage;
        private InspectorStage _InspectorStage;
        private bool _isLoop;
        private int _loopInterval;

        private ShuttleStage _ShuttleStage;
        private SR5000Stage _SR5000Stage;
        private Wafer _wafer;

        #endregion locale variable

        #region member properties

        public bool _IsInitialled { get; protected set; }

        #endregion member properties

        #region EmWorks convention functions

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public OpMain()
        {
            Initialize();
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~OpMain()
        {
            _isLoop = false;
            // add your code here
        }

        private void DtgLed_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Todo : 테스트 Code 삭제 必
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.            
            string rootPath = Directory.GetParent(Directory.GetParent(
                                Directory.GetParent(Directory.GetParent(
                                Directory.GetParent(currentPath).FullName).FullName).FullName).FullName).FullName;

            //string filePath = @"D:\00_ENC-SW\EmWorks.App.OpticInspection\Source\Resources\Images\Test\Result_RGB.PNG";
            string filePath = rootPath + @"\Source\Resources\Images\Test\Result_RGB.PNG";
            imgResultRGB.LoadImage(filePath);
            imgResultRGB.Visibility = Visibility.Visible;
            lblResultRGB.Visibility = Visibility.Hidden;
            filePath = filePath = rootPath + @"\Source\Resources\Images\Test\Result_Pseudo.PNG";
            imgResultPseudo.LoadImage(filePath);
            imgResultPseudo.Visibility = Visibility.Visible;
            lblResultPseudo.Visibility = Visibility.Hidden;

            filePath = rootPath + @"\Source\Resources\Images\Test\Result_SearchROI.PNG";
            imgResultSearchROI.LoadImage(filePath);
            imgResultSearchROI.Visibility = Visibility.Visible;
            lblResultSearchROI.Visibility = Visibility.Hidden;            

            this.chartXY.DrawPoint(0.4, 0.5);
            this.chartUV.DrawPoint(0.3, 0.2);

            List<double> tempWave = new List<double>();

            for (int i=0;i<100;i++)
            {
                tempWave.Add(0);
            }

            double value = 0.026;

            for (int i = 100; i < 200; i++)
            {
                value = value + 0.002;
                tempWave.Add(value);
            }

            for (int i = 200; i < 300; i++)
            {
                value = value - 0.002;
                tempWave.Add(value);
            }

            for (int i = 200; i < 720; i++)
            {
                tempWave.Add(0.0);
            }

            tempWave.Add(0.2);

            this.chartWave.DrawGraph(tempWave, 380, 780);
            lblWaveChart.Visibility = Visibility.Hidden;
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

        private void OpMain_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _ShuttleStage = new ShuttleStage(this.cpnShtStage);
            _InspectorStage = new InspectorStage(this.cpnInspectorStage);
            _CameraStage = new CameraStage(this.cpnCameraStage);
            _SR5000Stage = new SR5000Stage(this.cpnSR5000Stage);

            this.grdWafer.Children.Add(_wafer = new Wafer());
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += OpMain_Loaded;

            // Todo : 완료 후 삭제 必
            this.dtgLed.MouseDoubleClick += DtgLed_MouseDoubleClick;

            CommandCenter.CommandMessage += CommandCenter_CommandMessage; 

            return Idx.FunctionResult.True;

        }

       
        private void CommandCenter_CommandMessage(object sendor, CommandCenter.EventCommandMessageArgs e)
        {
            if (e._CommandType == CommandCenter.CommandType.ChangeFov)
            {
                ShowSelectFov(Convert.ToInt32(e._Command));
            }
            else if (e._CommandType == CommandCenter.CommandType.StartInspection)
            {
                ShowChangeInspectionGrid(true);
            } // else
            else if (e._CommandType == CommandCenter.CommandType.CompleteInspection)
            {
                ShowChangeInspectionGrid(false);
            } // else
            else if (e._CommandType == CommandCenter.CommandType.PseudoData)
            {
                ShowPseudo(e._Command);
            } // else
            else if (e._CommandType == CommandCenter.CommandType.RGBData)
            {
                ShowRGB(e._Command);
            } // else
            else if (e._CommandType == CommandCenter.CommandType.ResultData)
            {
                UpdateInsepctionList(e._Message);
            } // else
        }

        private void ShowPseudo(object command)
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                // 로드            
                imgResultPseudo.LoadImage(command.ToString());
                imgResultPseudo.Visibility = Visibility.Visible;
                lblResultPseudo.Visibility = Visibility.Hidden;
            });
        }

        private void ShowRGB(object command)
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                // 로드            
                imgResultRGB.LoadImage(command.ToString());
                imgResultRGB.Visibility = Visibility.Visible;
                lblResultRGB.Visibility = Visibility.Hidden;
            });
        }

        private void UpdateInsepctionList(string _Message)
        {
            
        }

        ProcessBox _processDialog;

        private void ShowChangeInspectionGrid(bool isVisible)
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                if (isVisible == true)
                {
                    _processDialog = new ProcessBox();
                    _processDialog.TitleMessage = i18n.GetLanguage("Inspection in progress");
                    _processDialog.Topmost = true;
                    _processDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

                    _processDialog.Show();
                }
                else
                {
                    if (_processDialog != null)
                    {
                        _processDialog.Close();
                    } // else
                }
            });
        }

        private void ShowSelectFov(int fovID)
        {
            App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                _wafer.SelectLabel(fovID);
            });
        }

        #endregion EmWorks convention functions
    }
}