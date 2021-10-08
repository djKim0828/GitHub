using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EmWorks.View
{
    /// <summary>
    /// Interaction logic for UcWaitingControl.xaml
    /// </summary>
    public partial class WaittingComponent : UserControl
    {
        private Storyboard storyboard;
        public bool _IsStart = false;

        public WaittingComponent()
        {
            InitializeComponent();

            storyboard = (Resources["waiting"] as Storyboard);
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        public void Begin()
        {
            _IsStart = true;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.Visibility = Visibility.Visible;
                storyboard.Begin(image, true);
            }));
        }

        public void Stop()
        {
            _IsStart = false;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.Visibility = Visibility.Hidden;
                storyboard.Pause(image);
            }));
        }

    }
}
