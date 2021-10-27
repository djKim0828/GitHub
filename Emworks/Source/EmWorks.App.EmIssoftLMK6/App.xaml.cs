using System;
using System.Drawing;
using System.Windows;
using Winforms = System.Windows.Forms;

namespace EmWorks.App.EmIssoftLMK6
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        public static Winforms.NotifyIcon noti;

        #endregion Fields

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (noti == null)
            {
                SetNoti();
            }
        }

        private void SetNoti()
        {
            noti = new Winforms.NotifyIcon();
            var stream = GetResourceStream(new Uri("pack://application:,,,/EmWorks.App.EmIssoftLMK6;component/Resources/Images/Common/Logo/logoEnc64.png")).Stream;
            var bitmap = new Bitmap(stream);
            noti.Icon = Icon.FromHandle(bitmap.GetHicon());
            noti.Visible = true;
        }

        #endregion Methods
    }
}