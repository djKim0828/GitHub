using System.Windows.Controls;

namespace EmWorks.View
{
    /// <summary>
    /// labeledComboboxComponent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class labeledComboboxComponent : UserControl
    {
        #region Constructors

        public labeledComboboxComponent()
        {
            InitializeComponent();
        }

        public labeledComboboxComponent(bool editable = false)
        {
            InitializeComponent();
            comboBox.IsEditable = editable;
        }

        #endregion Constructors
    }
}