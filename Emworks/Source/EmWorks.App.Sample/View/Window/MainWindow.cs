using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmWorks.App.Sample.View.Window;
using EmWorks.Lib.Common;

namespace EmWorks.App.Sample
{
    public partial class MainWindow : Form
    {
        ConfigTestWindow _configTestWindow;
        McrTestWindow _mcrTestWinodw;
        AjinIoTestWinodw _ajinIoTestWinodw;
        AjinMotionTestWindow _ajinMotionTestWindow;
        NachiRobotTestWindow _nachiRobotTestWindow;
        InRobotTestWindow _inRobotTestWindow;
        TopconSR500Window _topconSR500Window;
        MotionStandardTestWindow _MotionStandardTestWindow;

        public MainWindow()
        {
            Logger.Infomation("Start SoGen Program");
            InitializeComponent();
        }

        public Size GetWorkingAreaSize()
        {
            Size workingAreaSize = Screen.PrimaryScreen.WorkingArea.Size;

            return workingAreaSize;
        }

        public Size GetScreenSize()
        {
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            return screenSize;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // 초기화 테스트             
            Logger.Infomation("Start Sample");

            Size screenSize = new Size();
            int left = GetScreenLeftPosition(ref screenSize);            

            this.Left = left + screenSize.Width / 2 - this.Size.Width / 2;
            this.Top = this.Height;           
        }

        public int GetScreenLeftPosition(ref Size screenSize)
        {            
            Screen[] screens = Screen.AllScreens;
            int left = 0;

            if (screens.Length > 1)
            {
                for (int i=0; i<screens.Length;i++)
                {
                    if (left > screens[i].WorkingArea.Left)
                    {
                        left = screens[i].WorkingArea.Left;
                        screenSize = screens[i].WorkingArea.Size;
                    } // else
                }
            } // else
            
            return left;
            
        }
        private void btnOpenMCR_Click(object sender, EventArgs e)
        {
            _mcrTestWinodw = new McrTestWindow();
            _mcrTestWinodw.Show();
        }


        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            _configTestWindow = new ConfigTestWindow();
            _configTestWindow.Show();
        }

        private void btnOpenIO_Click(object sender, EventArgs e)
        {
            _ajinIoTestWinodw = new AjinIoTestWinodw();
            _ajinIoTestWinodw.Show();
        }

        private void btnOpenMotion_Click(object sender, EventArgs e)
        {
            _ajinMotionTestWindow = new AjinMotionTestWindow();
            _ajinMotionTestWindow.Show();
        }

        private void btnOpenNachiOut_Click(object sender, EventArgs e)
        {
            _nachiRobotTestWindow = new NachiRobotTestWindow();
            _nachiRobotTestWindow.Show();
        }

        private void btnOpenNachiIn_Click(object sender, EventArgs e)
        {
            _inRobotTestWindow = new InRobotTestWindow();
            _inRobotTestWindow.Show();
        }

        private void btnOpenSR5000_Click(object sender, EventArgs e)
        {
            _topconSR500Window = new TopconSR500Window();
            _topconSR500Window.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _MotionStandardTestWindow = new MotionStandardTestWindow();
            _MotionStandardTestWindow.Show();
        }
    }
}
