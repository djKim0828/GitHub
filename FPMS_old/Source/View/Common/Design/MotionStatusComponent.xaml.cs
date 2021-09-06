using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using FPMS.E2105_FS111_121;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EmWorks.View
{
    /// <summary>
    /// Interaction logic for MotionStatusComponent.xaml
    /// </summary>
    public partial class MotionStatusComponent : UserControl
    {
        #region Fields

        private bool _isFirstVisible = false;
        private System.Collections.Generic.Dictionary<string, Tag> _motionTags;
        private string _tagInputHeaderName = string.Empty;

        #endregion Fields

        #region Constructors

        public MotionStatusComponent()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

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
                    } // else

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }// else
        }

        public void Initialize(string title,
                                        int axisNo,
                                System.Collections.Generic.Dictionary<string, Tag> motionTags)
        {
            lblTitle.Content = title;
            _motionTags = motionTags;

            if (GetTagHeaderName(axisNo) == true)
            {
                RegistEvents();
            } // else
        }

        private void ChangeStatus(Tag tag, Brush normalColor, Brush errorColor)
        {
            Ellipse checkControl = FindControl(tag.Name);

            if (checkControl == null)
            {
                return;
            } // else

            if (tag.Is == null)
            {
                checkControl.Stroke = errorColor;
                return;
            } // else

            string status = tag.Is.ToString();

            // 상태 변경
            if (status == Idx.DigitalValue.Enable.ToString())
            {
                checkControl.Stroke = normalColor;
            }
            else
            {
                checkControl.Stroke = errorColor;
            }

            return;
        }

        private void CheckStatus(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return;
                } // else

                Tag tag = (Tag)obj;
                if (tag.Name == _tagInputHeaderName + Idx.MotionAction.AlarmStatus)
                {
                    ChangeStatus(tag, System.Windows.Media.Brushes.Red, System.Windows.Media.Brushes.Green);
                }
                else if (tag.Name == _tagInputHeaderName + Idx.MotionAction.SignalBusy)
                {
                    ChangeStatus(tag, System.Windows.Media.Brushes.Green, System.Windows.Media.Brushes.Gray);
                }
                else if (tag.Name == _tagInputHeaderName + Idx.MotionAction.SignalNegativeLimit)
                {
                    ChangeStatus(tag, System.Windows.Media.Brushes.Red, System.Windows.Media.Brushes.Gray);
                }
                else if (tag.Name == _tagInputHeaderName + Idx.MotionAction.SignalPositiveLimit)
                {
                    ChangeStatus(tag, System.Windows.Media.Brushes.Red, System.Windows.Media.Brushes.Gray);
                }
                else
                {
                    ChangeStatus(tag, System.Windows.Media.Brushes.Green, System.Windows.Media.Brushes.Red);
                }

                return;
            }
            catch (System.Exception ex)
            {
                //int a = index;
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            Thread.Sleep(10); // UI Load 후에 실행

            // 처음에 한번 읽어준다.
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                FirstChekStatus();
            });
        }

        private Ellipse FindControl(string inputName)
        {
            string tagName = inputName;
            if (inputName.IndexOf(_tagInputHeaderName) >= 0)
            {
                // name에서 HeaderName을 지워준다.
                tagName = tagName.Substring(_tagInputHeaderName.Length,
                    tagName.Length - _tagInputHeaderName.Length);
            } // else

            try
            {
                // 버튼과 라벨은 자동으로 찾아서 언어 변경
                foreach (Ellipse tb in FindVisualChildren<Ellipse>(this))
                {
                    if (tb.Name.IndexOf(tagName) > 0)
                    {
                        return tb;
                    } //else
                }

                return null;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        private void FirstChekStatus()
        {
            // 전체 갱신
            ChangeStatus(_motionTags[Idx.MotionAction.sxDeviceMotion],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Red);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.ServoOn],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Red);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.AlarmStatus],
                            System.Windows.Media.Brushes.Red,
                            System.Windows.Media.Brushes.Green);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalBusy],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Gray);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.HomingStatus],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Gray);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalInpos],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Gray);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalNegativeLimit],
                            System.Windows.Media.Brushes.Red,
                            System.Windows.Media.Brushes.Gray);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalHome],
                            System.Windows.Media.Brushes.Green,
                            System.Windows.Media.Brushes.Gray);
            ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalPositiveLimit],
                            System.Windows.Media.Brushes.Red,
                            System.Windows.Media.Brushes.Gray);
        }

        private bool GetTagHeaderName(int axisNo)
        {
            try
            {
                List<Tag> axisTag = _motionTags.Values.Select(i => i)
                        .Where(d => d.Identity.IoType == EmWorks.Lib.Core.Tag.Idx.IoType.Input &&
                        d.Identity.Index == axisNo)
                        .ToList();

                _tagInputHeaderName = "x" +
                        axisTag[2].Identity.Unit + axisTag[2].Identity.Item;

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void MotionStatusComponent_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == true)
            {
                if (_isFirstVisible == false)
                {
                    ThreadPool.QueueUserWorkItem(EmWorksProc);
                    _isFirstVisible = true;
                } // else
            } // else
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.IsVisibleChanged += MotionStatusComponent_IsVisibleChanged;

            // Status Check
            _motionTags[Idx.MotionAction.sxDeviceMotion].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.ServoOn].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.AlarmStatus].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.SignalBusy].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.SignalInpos].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.SignalNegativeLimit].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.SignalPositiveLimit].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.SignalHome].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.HomingStatus].OnChanged += xTag_OnChanged;
            _motionTags[_tagInputHeaderName + Idx.MotionAction.Actual].OnChanged += xTag_OnChanged;

            return Idx.FunctionResult.True;
        }

        private void xTag_OnChanged(object sendor, EventTagChangedArgs e)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                CheckStatus(sendor);
            });
        }

        #endregion Methods
    }
}