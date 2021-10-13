using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StyleTest
{
    public partial class App : Application
    {
        public App()
        {
            SetWPFResource(); // WPF 즉시 적용

            MainWindow main = new MainWindow();
            main.ShowDialog();
        }

        private void SetWPFResource()
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/CStyle;component/resStyleCommon.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/CStyle;component/resIconPathData.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/CStyle;component/resBrushColor.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }        
}
