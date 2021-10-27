using EmWorks.Lib.Common;
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

namespace EmWorks.App.EmIssoftLMK6
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProcessMain _ProcessMain = new ProcessMain();

        public MainWindow()
        {
            InitializeComponent();

            Window_Loaded();

            this.ShowInTaskbar = false;
        }

        private void Window_Loaded()
        {
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                item1.Index = 0;
                item1.Text = "프로그램 종료";
                item1.Click += delegate (object click, EventArgs eClick)
                {
                    _ProcessMain.Close();
                    Application.Current.Shutdown();
                };
                item2.Index = 0;
                item2.Text = "프로그램 열기";
                item2.Click += delegate (object click, EventArgs eClick)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };

                App.noti.DoubleClick += delegate (object senders, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
                App.noti.ContextMenu = menu;
                App.noti.Text = "LMKServer";

                this.Hide();

            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState.Minimized.Equals(WindowState))
            {
                this.Hide();
            }//else

            base.OnStateChanged(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
            base.OnClosing(e);
        }
    }
}
