using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EmWorks.App.OpticInspection
{
    public class AutoLocale
    {
        public void Start(Window control)
        {
            ChangeLocaleButton(control);
            ChangeLocaleToggleButton(control);
            ChangeLocaleLabel(control);

            ChangeLocaleListView(control);
            ChangeLocaleDataGrid(control);
        }
        private void ChangeLocaleLabel(Window control)
        {
            foreach (Label tb in FindVisualChildren<Label>(control))
            {
                if (tb.Content != null && tb.Content.ToString() != string.Empty)
                {
                    // 언어 타입의 실기간 변경을 위하여 Default Content를 Tag에 저장한다.
                    if (tb.Tag == null)
                    {
                        tb.Tag = tb.Content;
                        tb.Content = i18n.GetLanguage(tb.Content.ToString());
                    }
                    else
                    {
                        tb.Content = i18n.GetLanguage(tb.Tag.ToString());
                    }
                } // else
            }
        }

        private void ChangeLocaleToggleButton(Window control)
        {
            foreach (ToggleButton tb in FindVisualChildren<ToggleButton>(control))
            {
                if (tb.Content != null)
                {
                    if (tb.Content.ToString() != String.Empty &&
                    tb.Content.ToString() != "System.Windows.Shapes.Path")  // ICON만 있는 Button은 예외처리
                    {
                        // 언어 타입의 실기간 변경을 위하여 Default Content를 Tag에 저장한다.
                        if (tb.Tag == null)
                        {
                            tb.Tag = tb.Content;
                            tb.Content = i18n.GetLanguage(tb.Content.ToString());
                        }
                        else
                        {
                            tb.Content = i18n.GetLanguage(tb.Tag.ToString());
                        }
                    } // else
                } //else
            }
        }

        private void ChangeLocaleButton(Window control)
        {
            // 버튼과 라벨은 자동으로 찾아서 언어 변경
            foreach (Button tb in FindVisualChildren<Button>(control))
            {
                if (tb.Content != null)
                {
                    if (tb.Content.ToString() != String.Empty &&
                    tb.Content.ToString() != "System.Windows.Shapes.Path")  // ICON만 있는 Button은 예외처리
                    {
                        // 언어 타입의 실기간 변경을 위하여 Default Content를 Tag에 저장한다.
                        if (tb.Tag == null)
                        {
                            tb.Tag = tb.Content;
                            tb.Content = i18n.GetLanguage(tb.Content.ToString());
                        }
                        else
                        {
                            tb.Content = i18n.GetLanguage(tb.Tag.ToString());
                        }
                    } // else
                } //else
            }
        }

        private void ChangeLocaleListView(Window control)
        {
            try
            {
                foreach (ListView tb in FindVisualChildren<ListView>(control))
                {
                    GridView gridView = (GridView)tb.View;

                    foreach (GridViewColumn column in gridView.Columns)
                    {
                        if (column.HeaderStringFormat == null)
                        {
                            column.HeaderStringFormat = column.Header.ToString();
                            column.Header = i18n.GetLanguage(column.Header.ToString());
                        }
                        else
                        {
                            column.Header = i18n.GetLanguage(column.HeaderStringFormat);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void ChangeLocaleDataGrid(Window control)
        {
            try
            {
                foreach (DataGrid tb in FindVisualChildren<DataGrid>(control))
                {
                    foreach (DataGridTextColumn column in tb.Columns)
                    {
                        if (column.HeaderStringFormat == null)
                        {
                            column.HeaderStringFormat = column.Header.ToString();
                            column.Header = i18n.GetLanguage(column.Header.ToString());
                        }
                        else
                        {
                            column.Header = i18n.GetLanguage(column.HeaderStringFormat);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject odject) where T : DependencyObject
        {
            if (odject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(odject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(odject, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
