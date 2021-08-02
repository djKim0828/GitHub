using System.Windows.Input;

namespace ImageTool
{
    public abstract class ToolBase
    {
        #region Fields

        public UcCanvas _window;

        #endregion Fields

        #region Constructors

        public ToolBase(object window)
        {
            _window = (UcCanvas)window;
        }

        #endregion Constructors

        #region Methods

        public void ChangeCursor(Cursor changeCursor)
        {
            Mouse.OverrideCursor = changeCursor;
        }

        public abstract void MouseMove(object sender, MouseEventArgs e);

        public abstract void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e);

        public abstract void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e);

        #endregion Methods
    }
}