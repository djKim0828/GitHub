using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            RegistEvents();
        }

        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;

            // Topbar
            this.grdDlgTitle.MouseLeftButtonDown += GrdDlgTitle_MouseLeftButtonDown;
            this.grdDlgTitle.MouseMove += GrdDlgTitle_MouseMove;
            this.btnDlgClose.Click += BtnDlgClose_Click;
            this.btnChange.Click += BtnDialogWindowStatueChange_Click;
            this.btnMini.Click += BtnMini_Click;

            this.btnLoad.Click += BtnLoad_Click;

            //1
            this.btnNormal.Click += BtnNormal_Click;
            this.btnZoom.Click += BtnZoom_Click;

            // 2
            this.btnDeleteAll.Click += BtnDeleteAll_Click;

            //3
            this.btnLength.Click += BtnLength_Click;
            this.btnAngle.Click += BtnAngle_Click;

            //4
            this.btnRotationCCW.Click += BtnRotationCCW_Click;
            this.btnRotationCW.Click += BtnRotationCW_Click;
            

            return 0;
        }

        private void BtnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.DeleteAll);
        }

        private void BtnAngle_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.Angle);
        }

        private void BtnLength_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.Length);
        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.None);
        }

        private void BtnZoom_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.ZoomFit);
        }

        private void BtnRotationCCW_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.RotateCCW);
            
        }

        private void BtnRotationCW_Click(object sender, RoutedEventArgs e)
        {
            ucCanvas.Command(ImageModel.ToolType.RotateCW);
            
        }

        private void BtnMini_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void ChangeWindowStatus()
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;

                btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnMax"];
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;

                btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnWindowNormal"];
            }
        }

        private void BtnDialogWindowStatueChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeWindowStatus();
        }

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();   
        }

        private void GrdDlgTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    ChangeWindowStatus();
                }
            }
        }

        private void GrdDlgTitle_MouseMove(object sender, MouseEventArgs e)
        {
            System.Threading.Thread.Sleep(10); // 더블클릭 오동작 방지
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 32)
                {
                    if (this.WindowState == System.Windows.WindowState.Maximized)
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.Top = 0;

                        btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnMax"];
                    }

                    DragMove();
                }
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                BtnDlgClose_Click(null, null);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = OutputPath;
            openFileDialog.Filter = "Bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            if (this.ucCanvas.LoadImage(filePath) == true)
            {
                btnNormal.IsChecked = true;
                ucCanvas.Command(ImageModel.ToolType.None);

                this.grdLeft.IsEnabled = true;

            }
        }
    }
}
