using System;
using System.Windows;
using System.Windows.Controls;

namespace EmWorks.View
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextBlockComponent : UserControl
    {
        #region Fields

        private double prescroolbarsize = 0;

        #endregion Fields

        #region Constructors

        public TextBlockComponent()
        {
            InitializeComponent();
            RegistEvents();
        }

        #endregion Constructors

        #region Methods

        public void addText(string text)
        {
            this.txbLog.Inlines.Add(text);
            this.txbLog.Inlines.Add(Environment.NewLine);
        }

        public void clearText()
        {
            this.txbLog.Text = "";
        }

        private void BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.txbLog.Text = "";
        }

        private void BtnCopy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.txbLog.Text != "")
            {
                Clipboard.SetText(this.txbLog.Text);
            }
        }

        private void RegistEvents()
        {
            scroll.ScrollChanged += Scroll_ScrollChanged;
            btnCopy.Click += BtnCopy_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (prescroolbarsize != scroll.ScrollableHeight)
            {
                if (autoscroll.IsChecked == true)
                {
                    scroll.ScrollToEnd();
                }
                prescroolbarsize = scroll.ScrollableHeight;
            }
        }

        #endregion Methods
    }
}