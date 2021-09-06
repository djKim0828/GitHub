using EmWorks.Lib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FPMS.E2105_FS111_121
{
    public class ListViewEditor : EmWorks.View.ListViewEditorComponent
    {
        #region Enum

        public enum ViewType
        {
            List = 0,
            Editor,
        }

        #endregion Enum

        #region Fields

        private string _extension;
        private string _filePath;
        private Point _firstPoint;
        private bool _isDrag;
        private List<object> _resource;
        private ViewType _viewType;

        #endregion Fields

        #region Constructors

        public ListViewEditor()
        {
            Initialize();

            grdMain.RowDefinitions[int.Parse(grdUpDown.Tag.ToString())].Height = new GridLength(0);
            grdMain.RowDefinitions[int.Parse(grdButton.Tag.ToString())].Height = new GridLength(0);
        }

        public ListViewEditor(string filePath, string extension, ViewType viewType)
        {
            Initialize();

            _filePath = filePath;
            _extension = extension;
            _viewType = viewType;

            if (viewType == ViewType.List)
            {
                btnDelete.Visibility = Visibility.Visible;
                btnRefresh.Visibility = Visibility.Visible;
                grdMain.RowDefinitions[int.Parse(grdUpDown.Tag.ToString())].Height = new GridLength(0);
            }
            else if (viewType == ViewType.Editor)
            {
                btnUp.Visibility = Visibility.Visible;
                btnDown.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnNew.Visibility = Visibility.Visible;
                txbFileName.Visibility = Visibility.Visible;
            } // else
        }

        #endregion Constructors

        #region Destructors

        ~ListViewEditor()
        {
        }

        #endregion Destructors

        #region Methods

        public List<object> GetList()
        {
            return _resource;
        }

        public void LoadFileList()
        {
            try
            {
                DirectoryInfo fileDirectory = new DirectoryInfo(Global.ConfigGeneral.RootPath + _filePath);
                if (fileDirectory.Exists == false)
                {
                    fileDirectory.Create();
                } // else

                List<string> list = new List<string>();
                foreach (var item in fileDirectory.GetFiles())
                {
                    if (item.Extension.ToLower() == _extension)
                    {
                        string name = Path.GetFileNameWithoutExtension(item.Name);
                        list.Add(name);
                    } // else
                }

                SetList(list);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public void SetList(List<string> resource)
        {
            _resource = resource.Cast<object>().ToList();

            lsvMain.ItemsSource = _resource;

            var scrollViwer = GetScrollViewer(lsvMain) as ScrollViewer;
            if (scrollViwer != null)
            {
                scrollViwer.ScrollToLeftEnd();
            } // else
        }

        public void SetList(List<CheckBox> resource)
        {
            _resource = resource.Cast<object>().ToList();

            lsvMain.ItemsSource = _resource;

            var scrollViwer = GetScrollViewer(lsvMain) as ScrollViewer;
            if (scrollViwer != null)
            {
                scrollViwer.ScrollToLeftEnd();
            } // else
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

        private static DependencyObject GetScrollViewer(DependencyObject control)
        {
            if (control is ScrollViewer)
            {
                return control;
            } // else

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                var child = VisualTreeHelper.GetChild(control, i);
                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsvMain.SelectedIndex < 0 || _resource == null)
            {
                return;
            } // else

            if (MessageBox.ShowDialog("Delete the selected files?", "File Delete", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.NO)
            {
                return;
            } // else

            try
            {
                string fileName = _resource[lsvMain.SelectedIndex].ToString();
                string filePath = Global.ConfigGeneral.RootPath + _filePath + "\\" + fileName + _extension;

                FileInfo csvFile = new FileInfo(filePath);
                if (false == csvFile.Exists)
                {
                    LoadFileList();

                    return;
                } // else

                File.Delete(filePath);

                LoadFileList();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
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

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            txbFileName.Text = string.Empty;
            lsvMain.ItemsSource = null;
            _resource = null;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFileList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbFileName.Text == string.Empty || _resource == null)
            {
                return;
            } // else

            try
            {
                string fileName = txbFileName.Text.Trim();
                string filePath = Global.ConfigGeneral.RootPath + _filePath + "\\" + fileName + _extension;

                FileInfo newCsvFile = new FileInfo(filePath);
                if (newCsvFile.Exists == false)
                {
                    FileStream ioFileCreate = newCsvFile.Create();
                    ioFileCreate.Close();
                } // else

                switch (_extension)
                {
                    case ".sequence":
                        SaveSequence(newCsvFile);
                        break;

                    case ".macro":
                        SaveRecipe(newCsvFile);
                        break;

                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
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

        private void FindItem(ref Point secondPoint, ref ListBoxItem secondItem, bool upDown)
        {
            if (upDown == false)
            {
                secondPoint.Y = secondPoint.Y - 2; // Drop할때 ListItem 사이 빈공간으로 이동할 경우 Y축 pixel 계산해서 Index 선택되게끔..
                secondItem = FindAncestor<ListBoxItem>((DependencyObject)(lsvMain.InputHitTest(secondPoint)));
            }
            else
            {
                secondPoint.Y = secondPoint.Y + 2;
                secondItem = FindAncestor<ListBoxItem>((DependencyObject)(lsvMain.InputHitTest(secondPoint)));
            }
        }

        private void Initialize()
        {
            RegistEvents();
        }

        private List<object> Insert(List<object> list, int indexA, int indexB, bool upDown)
        {
            if (upDown == false)
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
            if (_viewType != ViewType.Editor)
            {
                return;
            } //else

            if (e.Data.GetDataPresent("DragDrop"))
            {
                int firstIndex = -1;
                int secondIndex = -1;
                bool upDown; // 이동 방향이 up이면 true, down 이면 false

                Point secondPoint = e.GetPosition(lsvMain);
                upDown = MovementUpdown(_firstPoint.Y, secondPoint.Y);

                var firstItem = FindAncestor<ListBoxItem>((DependencyObject)(lsvMain.InputHitTest(_firstPoint)));
                var secondItem = FindAncestor<ListBoxItem>((DependencyObject)(lsvMain.InputHitTest(secondPoint)));

                if (firstItem == null)
                {
                    return;
                } // else

                firstIndex = lsvMain.Items.IndexOf(firstItem.Content);

                while (true)
                {
                    if (secondItem == null && firstIndex >= 0)
                    {
                        FindItem(ref secondPoint, ref secondItem, upDown);
                    }
                    else
                    {
                        break;
                    }
                }

                if (secondItem == null)
                {
                    secondIndex = firstIndex;
                }
                else
                {
                    secondIndex = lsvMain.Items.IndexOf(secondItem.Content);
                }

                if (firstIndex >= 0 && secondIndex >= 0)
                {
                    _resource = Insert(_resource, firstIndex, secondIndex, upDown);
                    RefreshList(secondIndex);
                } // else

                _isDrag = false;
            }
        }

        private void LsvMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewType != ViewType.Editor)
            {
                return;
            } //else

            var temp = FindAncestor<ListBoxItem>((DependencyObject)(lsvMain.InputHitTest(e.GetPosition(lsvMain))));
            if (temp != null)
            {
                _isDrag = true;

                if (_extension == ".sequence")
                {
                    Point position = e.GetPosition(lsvMain);
                    List<CheckBox> ckbMacroList = _resource.Cast<CheckBox>().ToList();
                    int index = lsvMain.Items.IndexOf(temp.Content);
                    const int checkBoxPointY = 22;

                    for (int i = 0; i < ckbMacroList.Count; i++)
                    {
                        ckbMacroList[i].Foreground = Brushes.White;
                    }
                    ckbMacroList[index].Foreground = (Brush)Application.Current.Resources["colorENC"];

                    if (position.X < checkBoxPointY)
                    {
                        bool isChecked = (bool)ckbMacroList[index].IsChecked;
                        ckbMacroList[index].IsChecked = !isChecked;
                    } // else
                } // else
            } // else
        }

        private void LsvMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_viewType != ViewType.Editor)
            {
                return;
            } //else

            if ((e.LeftButton == MouseButtonState.Pressed))
            {
                if (_isDrag == true)
                {
                    ListBoxItem listBoxItem = FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                    if (listBoxItem == null)
                    {
                        return;
                    } // else

                    _firstPoint = e.GetPosition(lsvMain);

                    object content = (object)(listBoxItem.Content);

                    DataObject data = new DataObject("DragDrop", content);
                    DragDrop.DoDragDrop(listBoxItem, data, DragDropEffects.Move);
                }
            } // else
        }

        private bool MovementUpdown(double first, double second)
        {
            bool upDown;

            if (first <= second)
            {
                upDown = false;
            }
            else
            {
                upDown = true;
            }

            return upDown;
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
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnNew.Click += BtnNew_Click;
            btnSave.Click += BtnSave_Click;

            lsvMain.PreviewMouseLeftButtonDown += LsvMain_PreviewMouseLeftButtonDown;
            lsvMain.PreviewMouseMove += LsvMain_PreviewMouseMove;
            lsvMain.Drop += LsvMain_Drop;
        }

        private void SaveRecipe(FileInfo newCsvFile)
        {
            //Todo : recipeFile 저장
        }

        private void SaveSequence(FileInfo newCsvFile)
        {
            List<MacroFile> macrolist = new List<MacroFile>();
            List<CheckBox> ckbMacroList = _resource.Cast<CheckBox>().ToList();

            for (int i = 0; i < ckbMacroList.Count; i++)
            {
                var tempItem = new MacroFile(ckbMacroList[i].Content.ToString());
                tempItem._todoCheck = (bool)ckbMacroList[i].IsChecked;
                macrolist.Add(tempItem);
            }

            using (StreamWriter streamWriter = new StreamWriter(newCsvFile.FullName, false))
            {
                foreach (var item in macrolist)
                {
                    streamWriter.Write(item._todoCheck);
                    streamWriter.Write(",");
                    streamWriter.Write(item._macroName);
                    streamWriter.Write(",");
                    streamWriter.WriteLine();
                }
            }
        }

        private List<object> Swap(List<object> list, int indexA, int indexB)
        {
            object tmp = list[indexA];

            list[indexA] = list[indexB];
            list[indexB] = tmp;

            return list;
        }

        #endregion Methods
    }
}