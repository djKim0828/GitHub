using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmWorks.View
{
    /// <summary>
    /// Interaction logic for ListViewComponent.xaml
    /// </summary>
    public partial class ListViewComponent : UserControl
    {
        #region Fields

        private const int _sequenceFileList = 0;
        private const int _sequenceEdit = 2;
        private const int _recipeFileList = 4;
        private const int _recipeEdit = 6;
        private const int _itemList = 8;

        private int _indexDrag;
        private bool _isDrag;
        private List<string> _resource;

        #endregion Fields

        #region Constructors

        public ListViewComponent()
        {
            InitializeComponent();

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~ListViewComponent()
        {
        }

        #endregion Destructors

        #region Methods

        public List<string> GetList(List<string> resource)
        {
            return _resource;
        }

        public void SetList(List<string> resource)
        {
            _resource = resource;

            lsvMain.ItemsSource = _resource;
        }

        public void SetSplitVisible(bool isVisible)
        {
            if (isVisible == true)
            {
                grdSplit.Width = 10;
                grdSplit.Visibility = Visibility.Visible;
            }
            else
            {
                grdSplit.Width = 0;
                grdSplit.Visibility = Visibility.Hidden;
            }
        }

        private static T FindAncestor<T>(DependencyObject obj) where T : DependencyObject
        {
            do
            {
                if (obj is T)
                {
                    return (T)obj;
                } // else

                obj = VisualTreeHelper.GetParent(obj);
            }
            while (obj != null);

            return null;
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            int selectIndex = lsvMain.SelectedIndex;
            if (selectIndex < 0 || selectIndex + 1 > lsvMain.Items.Count - 1)
            {
                return;
            } // else

            _resource = Swap(_resource, selectIndex, selectIndex + 1);
            RefreshList(selectIndex + 1);
        }

        private void BtnSplit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid parent = (Grid)this.Parent;

                if (parent.ColumnDefinitions[int.Parse(Tag.ToString())].Width != new GridLength(10))
                {
                    parent.ColumnDefinitions[int.Parse(Tag.ToString())].Width = new GridLength(10);
                }
                else
                {
                    parent.ColumnDefinitions[int.Parse(Tag.ToString())].Width = new GridLength(1, GridUnitType.Star);
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            int selectIndex = lsvMain.SelectedIndex;

            if (selectIndex < -1 || selectIndex - 1 < 0)
            {
                return;
            } // else

            _resource = Swap(_resource, selectIndex, selectIndex - 1);
            RefreshList(selectIndex - 1);
        }

        private void ChangeCursor(Cursor changeCursor)
        {
            Mouse.OverrideCursor = changeCursor;
        }

        private void Initialize()
        {
            RegistEvents();
        }

        private List<string> Insert(List<string> list, int indexA, int indexB)
        {
            if (indexA <= indexB)
            {
                list.Insert(indexB + 1, list[indexA]);
                list.RemoveAt(indexA);
            }
            else
            {
                list.Insert(indexB, list[indexA]);
                list.RemoveAt(indexA + 1);
            }

            return list;
        }

        private void LsvMain_Drop(object sender, DragEventArgs e)
        {
            if (int.Parse(Tag.ToString()) == _sequenceEdit || int.Parse(Tag.ToString()) == _recipeEdit)
            {
                if (e.Data.GetDataPresent("DragDrop"))
                {
                    string content = e.Data.GetData("DragDrop") as string;

                    ListView listView = sender as ListView;

                    int index = -1;
                    var temp = FindAncestor<ListViewItem>
                                    ((DependencyObject)(lsvMain.InputHitTest(e.GetPosition(lsvMain))));

                    if (temp == null)
                        index = lsvMain.Items.Count - 1;
                    else
                        index = lsvMain.Items.IndexOf(temp.Content.ToString());

                    _resource = Insert(_resource, _indexDrag, index);
                    RefreshList(index);

                    _indexDrag = -1;
                    _isDrag = false;
                } // else
            } // else
        }

        private void LsvMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(Tag.ToString()) == _sequenceEdit || int.Parse(Tag.ToString()) == _recipeEdit)
            {
                var temp = FindAncestor<ListViewItem>((DependencyObject)(lsvMain.InputHitTest(e.GetPosition(lsvMain))));

                if (temp != null)
                {
                    _isDrag = true;
                } // else
            } // else
        }

        private void LsvMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (int.Parse(Tag.ToString()) == _sequenceEdit || int.Parse(Tag.ToString()) == _recipeEdit)
            {
                if ((e.LeftButton == MouseButtonState.Pressed) && _isDrag == true)
                {
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem = FindAncestor<ListViewItem>
                                                    ((DependencyObject)e.OriginalSource);

                    if (listViewItem == null)
                    {
                        return;
                    } // else

                    _indexDrag = lsvMain.Items.IndexOf(listViewItem.Content.ToString());

                    string content = (string)(listViewItem.Content);

                    DataObject data = new DataObject("DragDrop", content);
                    DragDrop.DoDragDrop(listViewItem, data, DragDropEffects.Move);
                } // else
            } // else
        }

        private void RefreshList(int changeIndex)
        {
            lsvMain.ItemsSource = null;
            lsvMain.ItemsSource = _resource;
            lsvMain.SelectedIndex = changeIndex;
        }

        private void RegistEvents()
        {
            btnSplit.Click += BtnSplit_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            btnClear.Click += BtnClear_Click;

            lsvMain.PreviewMouseLeftButtonDown += LsvMain_PreviewMouseLeftButtonDown;
            lsvMain.PreviewMouseMove += LsvMain_PreviewMouseMove;
            lsvMain.Drop += LsvMain_Drop;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txbFileName.Text = "";
            lsvMain.Items.Clear();
        }

        private List<string> Swap(List<string> list, int indexA, int indexB)
        {
            string tmp = list[indexA];

            list[indexA] = list[indexB];
            list[indexB] = tmp;

            return list;
        }

        #endregion Methods
    }
}
