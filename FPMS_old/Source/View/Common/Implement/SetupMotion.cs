using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using EmWorks.Lib.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="MtSetupMotion"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class SetupMotion : EmWorks.View.SetupMotionComponent
    {
        #region Fields

        private string _axisName;
        private int _axisNo;
        private string _categoryAxisNumber;
        private bool _isLoad = false;

        private System.Collections.Generic.Dictionary<string, Tag> _motionTags;

        private string _tagInputHeaderName = string.Empty;
        private string _tagOutputHeaderName = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public SetupMotion(int axisNo, Dictionary<string, Tag> motionTags)
        {
            _motionTags = motionTags;

            _axisNo = axisNo;

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~SetupMotion()
        {
            // n/a
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

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

        private void AddPosition(string relativeType, double position)
        {
            string positionName = string.Empty;
            InputBox dlg = new InputBox("New", "Insert Position Name");
            dlg.ShowDialog();
            if (dlg._ReturnValue == InputBox.MessageBoxReturnValue.OK)
            {
                positionName = dlg.OutputData;
            }
            else
            {
                return;
            }

            if (positionName == string.Empty)
            {
                return;
            } // else

            try
            {
                List<MotionPosition> changeList = lstPosition.ItemsSource as List<MotionPosition>;

                MotionPosition motionPosition = new MotionPosition();
                motionPosition.AxisName = _axisName;
                motionPosition.CategoryName = string.Empty;
                motionPosition.Name = positionName;
                motionPosition.MoveType = relativeType;
                motionPosition.Position = position;

                if (rdbFast.IsChecked == true)
                {
                    motionPosition.Speed = Convert.ToDouble(txtJogFastVel.Text);
                }
                else
                {
                    motionPosition.Speed = Convert.ToDouble(txtJogSlowVel.Text);
                }

                motionPosition.Accel = Convert.ToDouble(txtJogAcc.Text);
                motionPosition.Decel = Convert.ToDouble(txtJogDec.Text);

                changeList.Add(motionPosition);

                lstPosition.ItemsSource = null;
                lstPosition.ItemsSource = changeList;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Failed to add Motion position.");
            }
        }

        private void BtnAbsAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPosition(Idx.MotionMoveType.AbsoluteType, Convert.ToDouble(lblCurrentPositionData.Content));
        }

        private void btnAbsUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdatePosition(Idx.MotionMoveType.AbsoluteType, Convert.ToDouble(lblCurrentPositionData.Content));
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteMotionPosition();
        }

        private void BtnJogMinus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckServoOn() == false)
            {
                MessageBox.Show("check the motion status");
                return;
            }// else

            SaveData();

            SetAnalogCommand(Idx.MotionAction.JogAccel, Idx.MotionProperfy.JogAcc, true);
            SetAnalogCommand(Idx.MotionAction.JogDecel, Idx.MotionProperfy.JogDec, true);

            if (rdbFast.IsChecked == true)
            {
                SetAnalogCommand(Idx.MotionAction.JogVelocity, Idx.MotionProperfy.JogFastVel, true, true); // 속도를 설정하면서 동작 실행
            }
            else
            {
                SetAnalogCommand(Idx.MotionAction.JogVelocity, Idx.MotionProperfy.JogSlowVel, true, true); // 속도를 설정하면서 동작 실행
            }
        }

        private void BtnJogMinus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RunMotionStop();
        }

        private void BtnJogPlus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckServoOn() == false)
            {
                MessageBox.Show("check the motion status");
                return;
            }// else

            SaveData();

            SetAnalogCommand(Idx.MotionAction.JogAccel, Idx.MotionProperfy.JogAcc, true);
            SetAnalogCommand(Idx.MotionAction.JogDecel, Idx.MotionProperfy.JogDec, true);

            if (rdbFast.IsChecked == true)
            {
                SetAnalogCommand(Idx.MotionAction.JogVelocity, Idx.MotionProperfy.JogFastVel, true, false); // 속도를 설정하면서 동작 실행
            }
            else
            {
                SetAnalogCommand(Idx.MotionAction.JogVelocity, Idx.MotionProperfy.JogSlowVel, true, false); // 속도를 설정하면서 동작 실행
            }
        }

        private void BtnJogPlus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RunMotionStop();
        }

        private void BtnRelativeAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPosition(Idx.MotionMoveType.RelativeType, Convert.ToDouble(txtRelativeDistance.Text));
        }

        private void BtnRelativeMinus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunRelativeMode(true);
        }

        private void BtnRelativePlus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunRelativeMode(false);
        }

        private void BtnRelativeUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdatePosition(Idx.MotionMoveType.RelativeType, Convert.ToDouble(txtRelativeDistance.Text));
        }

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveData();
        }

        private void ChangeCurrentPosition()
        {
            try
            {
                string tempName = _tagInputHeaderName + Idx.MotionAction.Actual;

                double cmdPosition = 0;

                if (_motionTags[tempName].Is == null)
                {
                    return;
                } // else

                cmdPosition = (double)_motionTags[tempName].Is;

                tempName = _tagInputHeaderName + Idx.MotionAction.Actual;

                lblCurrentPositionData.Content = cmdPosition.ToString();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
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

        private void ChangeStatusServoOn(Tag tag)
        {
            if (tag.Is == null)
            {
                btnServerOn.IsChecked = false;
                return;
            } // else

            string status = tag.Is.ToString();

            // 상태 변경
            if (status == Idx.DigitalValue.Enable.ToString())
            {
                btnServerOn.IsChecked = true;
            }
            else
            {
                btnServerOn.IsChecked = false;
            }
        }

        private void CheckAndChangeDistance(bool isMinus)
        {
            double distance = Convert.ToDouble(txtRelativeDistance.Text);

            distance = Math.Abs(distance);

            if (isMinus == true)
            {
                distance = -distance;
            } // else

            txtRelativeDistance.Text = distance.ToString();
        }

        private bool CheckServoOn()
        {
            try
            {
                string tempName = _tagInputHeaderName + Idx.MotionAction.ServoOn;

                if (_motionTags[tempName].Is == null)
                {
                    return false;
                }// else

                object status = _motionTags[tempName].Is;

                if (status.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    return true;
                }// else

                return false;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void CheckStatus(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Tag tag = (Tag)obj;
                    if (tag.Name == _tagInputHeaderName + Idx.MotionAction.Actual)
                    {
                        ChangeCurrentPosition();
                    }
                    else if (tag.Name == _tagInputHeaderName + Idx.MotionAction.AlarmStatus)
                    {
                        ChangeStatus(tag, System.Windows.Media.Brushes.Red, System.Windows.Media.Brushes.Gray);
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
                } // else

                ChangeStatusServoOn(_motionTags[_tagInputHeaderName + Idx.MotionAction.ServoOn]);

                // 전체 갱신
                ChangeStatus(_motionTags[Idx.MotionAction.sxDeviceMotion],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Red);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.ServoOn],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Red);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.AlarmStatus],
                                System.Windows.Media.Brushes.Red,
                                System.Windows.Media.Brushes.Gray);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalBusy],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Gray);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.HomingStatus],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Red);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalInpos],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Red);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalNegativeLimit],
                                System.Windows.Media.Brushes.Red,
                                System.Windows.Media.Brushes.Gray);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalHome],
                                System.Windows.Media.Brushes.Green,
                                System.Windows.Media.Brushes.Red);
                ChangeStatus(_motionTags[_tagInputHeaderName + Idx.MotionAction.SignalPositiveLimit],
                                System.Windows.Media.Brushes.Red,
                                System.Windows.Media.Brushes.Gray);

                ChangeCurrentPosition();
            }
            catch (System.Exception ex)
            {
                //int a = index;
                Logger.Exception(ex);
            }
        }

        private void CreateDefaultPosition()
        {
            MotionPosition motionPosition = new MotionPosition();
            motionPosition.AxisName = _axisName;
            motionPosition.CategoryName = string.Empty;
            motionPosition.Name = Idx.DefaultPositionName;
            motionPosition.MoveType = Idx.MotionMoveType.AbsoluteType;
            motionPosition.Position = Convert.ToDouble(GetxTagValue(Idx.MotionProperfy.CommandPosisiton));
            motionPosition.Speed = Convert.ToDouble(GetxTagValue(Idx.MotionProperfy.Velocity));
            motionPosition.Accel = Convert.ToDouble(GetxTagValue(Idx.MotionProperfy.Accel));
            motionPosition.Decel = Convert.ToDouble(GetxTagValue(Idx.MotionProperfy.Decel));

            InsertMotionPosition(motionPosition);
            ConfigMotion.Save();
        }

        private void DeleteMotionPosition(List<MotionPosition> beforeList, List<MotionPosition> changeList)
        {
            // 이전과 이후 리스트를 비교하여 삭제
            foreach (MotionPosition mp in beforeList)
            {
                // 같은게 없다면 삭제
                if (changeList.Exists(x => x.CategoryName == mp.CategoryName) != true)
                {
                    if (mp.Name != string.Empty)
                    {
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.Name);
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.AxisName);
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.Position);
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.Speed);
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.Accel);
                        ConfigMotion.Remove(mp.CategoryName, Idx.MotionInfo.Decel);
                    }// else
                }
            }// else
        }

        private void DeleteMotionPosition()
        {
            if (MessageBox.ShowDialog("Do you want to delete the position?", "질문", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.NO)
            {
                return;
            } // else

            try
            {
                int selectIndex = lstPosition.SelectedIndex;

                if (selectIndex < 0)
                {
                    MessageBox.ShowDialog("Please, Select a Position");
                    return;
                }// else

                List<MotionPosition> changeList = lstPosition.ItemsSource as List<MotionPosition>;

                changeList.RemoveAt(selectIndex);
                lstPosition.ItemsSource = null;
                lstPosition.ItemsSource = changeList;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Failed to delete Motion position.");
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-21 15:00 (create or edit date.)
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            // 처음에 한번 읽어준다.
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                CheckStatus(null);
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

        private string GetCategoryName()
        {
            // Position 명칭 중 가장 마지막을 리턴!!
            List<string> CatrgoryList = ConfigMotion.GetCategory();

            string compareString = Idx.MotionInfo.TagCategoryFirstName;

            int max = 0;
            foreach (string categoryName in CatrgoryList)
            {
                if (categoryName.IndexOf(compareString) < 0)
                {
                    continue;
                }// else

                int number;
                string data = categoryName.Substring(compareString.Length, categoryName.Length - compareString.Length);
                if (int.TryParse(data, out number) == true)
                {
                    // 숫자이고 값이 더 크면 변경
                    if (max < number)
                    {
                        max = number;
                    }
                }
            }

            max++; // 제일 높은 숫자를 찾아 그 위 숫자를 만듦
            string result = compareString + max.ToString();
            return result;
        }

        private string GetMotionProperty(string categoryAxisNumber, string propertyName)
        {
            string result = ConfigMotion.GetValue(categoryAxisNumber, propertyName);
            if (result == string.Empty)
            {
                result = GetxTagValue(propertyName).ToString();

                ConfigMotion.insert(_categoryAxisNumber, propertyName, result);
            }// else

            return result;
        }

        private object GetxTagValue(string tagName)
        {
            try
            {
                string tempName = _tagInputHeaderName + tagName;

                if (_motionTags[tempName].Is == null)
                {
                    return 0;
                }// else

                object data = _motionTags[tempName].Is;

                if (data == null)
                {
                    return 0;
                }
                else
                {
                    return data;
                }
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            lblMotionName.Content = _axisName;
            lblAxixNoNo.Content = _axisNo.ToString();

            rdbSlow.IsChecked = true;

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
            }
            else
            {
                _IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                _categoryAxisNumber = Idx.MotionInfo.TagAxisFirstName + _axisNo.ToString();
                _axisName = ConfigMotion.GetValue(_categoryAxisNumber, Idx.MotionInfo.Name);

                List<Tag> axisTag;
                axisTag = _motionTags.Values.Select(i => i)
                                    .Where(d => d.Identity.IoType == EmWorks.Lib.Core.Tag.Idx.IoType.Input &&
                                           d.Identity.Index == _axisNo)
                                    .ToList();

                _tagInputHeaderName = "x" +
                    axisTag[2].Identity.Unit + axisTag[2].Identity.Item;

                _tagOutputHeaderName = "y" +
                    axisTag[2].Identity.Unit + axisTag[2].Identity.Item;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private void InsertMotionPosition(MotionPosition mp)
        {
            if (mp.Name != string.Empty)
            {
                mp.CategoryName = GetCategoryName(); // 추가해야 함으로 새로운 CategoryName을 구한다.
                mp.AxisName = _axisName;

                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.Name, mp.Name);
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.MoveType, mp.MoveType);
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.AxisName, mp.AxisName);
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.Position, mp.Position.ToString());
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.Speed, mp.Speed.ToString());
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.Accel, mp.Accel.ToString());
                ConfigMotion.insert(mp.CategoryName, Idx.MotionInfo.Decel, mp.Decel.ToString());
            }// else
        }

        private void LoadControlsProperty()
        {
            this.txtHomeOffset.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeOffset);
            this.txtHomeAcc1.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeAcc1st);
            this.txtHomeAcc2.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeAcc2nd);
            this.txtHomeVel1.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeVel1st);
            this.txtHomeVel2.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeVel2nd);
            this.txtHomeVel3.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeVel3rd);
            this.txtHomeVel4.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.HomeVel4th);
        }

        private void LoadData()
        {
            LoadControlsProperty();
            LoadPositionProperty();

            _isLoad = true;
        }

        private List<MotionPosition> LoadMotionPosition()
        {
            try
            {
                List<MotionPosition> postionList = new List<MotionPosition>();
                List<ConfigIdentity> CategoryList = ConfigMotion.GetValuefromName(Idx.MotionInfo.AxisName, _axisName);

                MotionPosition motionPosition;

                if (CategoryList.Count == 0)
                {
                    CreateDefaultPosition();
                    CategoryList = ConfigMotion.GetValuefromName(Idx.MotionInfo.AxisName, _axisName);
                } // else

                foreach (ConfigIdentity item in CategoryList)
                {
                    motionPosition = new MotionPosition();

                    motionPosition.AxisName = _axisName;
                    motionPosition.CategoryName = item.Category;
                    motionPosition.Name = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Name);
                    motionPosition.MoveType = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.MoveType);
                    motionPosition.Position = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Position));
                    motionPosition.Speed = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Speed));
                    motionPosition.Accel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Accel));
                    motionPosition.Decel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Decel));

                    postionList.Add(motionPosition);
                }

                return postionList;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        private void LoadPositionProperty()
        {
            this.txtJogAcc.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.JogAcc);
            this.txtJogDec.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.JogDec);
            this.txtJogFastVel.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.JogFastVel);
            this.txtJogSlowVel.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.JogSlowVel);

            this.txtRelativeDistance.Text = GetMotionProperty(_categoryAxisNumber, Idx.MotionProperfy.RelativeDistance);
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-27 12:00
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // Dialog
            this.Loaded += SetupMotion_Loaded;
            this.Unloaded += SetupMotion_Unloaded;

            this.IsVisibleChanged += UcMtSetupMotion_IsVisibleChanged;

            // Information
            btnServerOn.Click += TabControlsBtnServerOn_Click;
            btnAlarmReset.Click += TabControlsBtnAlarmReset_Click;

            //Home
            btnHomeStop.Click += TabControlsBtnStop_Click;
            btnHomeSearch.Click += TabControlsBtnHomeSearch_Click;

            //Position
            btnMove.Click += TabPositionBtnMove_Click;
            btnMoveStop.Click += TabControlsBtnStop_Click;
            btnDelete.Click += BtnDelete_Click;

            // Jog
            btnJogMinus.PreviewMouseDown += BtnJogMinus_MouseDown;
            btnJogMinus.PreviewMouseUp += BtnJogMinus_MouseUp;
            btnJogPlus.PreviewMouseDown += BtnJogPlus_MouseDown;
            btnJogPlus.PreviewMouseUp += BtnJogPlus_MouseUp;

            //Absolute
            btnAbsAdd.Click += BtnAbsAdd_Click;
            btnAbsUpdate.Click += btnAbsUpdate_Click;

            // Relative
            btnRelativeMinus.Click += BtnRelativeMinus_Click;
            btnRelativePlus.Click += BtnRelativePlus_Click;
            btnRelativeAdd.Click += BtnRelativeAdd_Click;
            btnRelativeUpdate.Click += BtnRelativeUpdate_Click;
            btnRelativeMoveStop.Click += TabControlsBtnStop_Click;

            btnSave.Click += BtnSave_Click;

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

        private void RunHomeSearch()
        {
            int timeout = Idx.TimeOut.HomeSearch;

            SetAnalogCommand(Idx.MotionAction.HomeOffset, Idx.MotionProperfy.HomeOffset, true);
            SetAnalogCommand(Idx.MotionAction.HomeVel1st, Idx.MotionProperfy.HomeVel1st, true);
            SetAnalogCommand(Idx.MotionAction.HomeVel2nd, Idx.MotionProperfy.HomeVel2nd, true);
            SetAnalogCommand(Idx.MotionAction.HomeVel3rd, Idx.MotionProperfy.HomeVel3rd, true);
            SetAnalogCommand(Idx.MotionAction.HomeVel4th, Idx.MotionProperfy.HomeVel4th, true);
            SetAnalogCommand(Idx.MotionAction.HomeAcc1st, Idx.MotionProperfy.HomeAcc1st, true);
            SetAnalogCommand(Idx.MotionAction.HomeAcc2nd, Idx.MotionProperfy.HomeAcc2nd, true);

            // Set Home Property(Parameter)
            SetAnalogCommand(Idx.MotionAction.HomeVelocity, Idx.DigitalValue.Enable.ToString());

            // Start Command
            SetDigitalCommand(Idx.MotionAction.Homing, Idx.DigitalValue.Enable.ToString());

            Action action = () =>
            {
                while (true)
                {
                    // Home Check 하는 루틴 필요
                    string tagName = _tagInputHeaderName + Idx.MotionAction.HomingStatus;
                    string homeStatus = _motionTags[tagName].Is.ToString();
                    if (homeStatus == Idx.MotionHomeStatus.Searching)
                    {
                        continue;
                    }
                    else if (homeStatus != Idx.MotionHomeStatus.Complate)
                    {
                        Logger.Error("Failed the home search [Code : " + homeStatus + "]");
                        break;
                    }

                    tagName = _tagInputHeaderName + Idx.MotionAction.SignalHome;
                    if (_motionTags[tagName].Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        // 마무리
                        SetDigitalCommand(Idx.MotionAction.PosMatch, Idx.DigitalValue.Enable.ToString());
                        //SetDigitalCommand(Idx.MotionAction.Homing, Idx.DigitalValue.Disable.ToString());

                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            if (WaitResult(timeout, action) == false)
            {
                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    MessageBox.Show("Failed the home search");
                });
            }
            else
            {
                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    MessageBox.Show("success the home search");
                });
            }
        }

        private void RunMotionStop()
        {
            SetDigitalCommand(Idx.MotionAction.EStop, Idx.DigitalValue.Enable.ToString());
        }

        private void RunMove()
        {
            SaveData();

            if (CheckServoOn() == false)
            {
                MessageBox.Show("check the motion status");
                return;
            }// else

            int selectIndex = lstPosition.SelectedIndex;

            if (selectIndex < 0)
            {
                MessageBox.ShowDialog("Please, Select a Position");
                return;
            }// else

            // 선택된 Position 가져오기
            MotionPosition position = lstPosition.SelectedItem as MotionPosition;

            if (position == null)
            {
                return;
            }// else

            SetDigitalCommand(Idx.MotionAction.MoveMode, position.MoveType);
            SetAnalogCommand(Idx.MotionAction.Velocity, position.Speed.ToString());
            SetAnalogCommand(Idx.MotionAction.Accel, position.Accel.ToString());
            SetAnalogCommand(Idx.MotionAction.Decel, position.Decel.ToString());

            SetAnalogCommand(Idx.MotionAction.Move, position.Position.ToString()); // 동작 실행
        }

        private void RunRelativeMode(bool isMinus)
        {
            try
            {
                CheckAndChangeDistance(isMinus); // 확인하여 Minus로 변경

                SaveData(); // 실행 전 값저장

                if (CheckServoOn() == false)
                {
                    MessageBox.Show("check the motion status");
                    return;
                }// else

                SetDigitalCommand(Idx.MotionAction.MoveMode, Idx.MotionMoveType.RelativeType);

                if (rdbFast.IsChecked == true)
                {
                    SetAnalogCommand(Idx.MotionAction.Velocity, txtJogFastVel.Text);
                }
                else
                {
                    SetAnalogCommand(Idx.MotionAction.Velocity, txtJogSlowVel.Text);
                }

                SetAnalogCommand(Idx.MotionAction.Accel, txtJogAcc.Text);
                SetAnalogCommand(Idx.MotionAction.Decel, txtJogAcc.Text);

                SetAnalogCommand(Idx.MotionAction.Move, txtRelativeDistance.Text.ToString(), false); // 동작 실행
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SaveControlsProperty()
        {
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeOffset, this.txtHomeOffset.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeAcc1st, this.txtHomeAcc1.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeAcc2nd, this.txtHomeAcc2.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeVel1st, this.txtHomeVel1.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeVel2nd, this.txtHomeVel2.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeVel3rd, this.txtHomeVel3.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.HomeVel4th, this.txtHomeVel4.Text);
        }

        private void SaveData()
        {
            try
            {
                if (_isLoad != true)
                {
                    return;
                }// else

                SaveMotionPosition();
                SavePositionProperty();
                SaveControlsProperty();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SaveMotionPosition()
        {
            try
            {
                List<MotionPosition> changeList = lstPosition.ItemsSource as List<MotionPosition>;
                List<MotionPosition> beforeList = LoadMotionPosition();

                if (changeList == null || beforeList == null)
                {
                    return;
                }// else

                DeleteMotionPosition(beforeList, changeList); // 삭제

                foreach (MotionPosition mp in changeList)
                {
                    // 같은게 있으면 업데이트, 없으면 Add 한다.
                    if (beforeList.Exists(x => x.CategoryName == mp.CategoryName) == true)
                    {
                        UpdateMotionPosition(mp);
                    }
                    else
                    {
                        InsertMotionPosition(mp);
                    }
                }

                ConfigMotion.Save();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Failed", "Error", MessageBoxButtonType.OK, MessageBoxImageType.ERROR);
            }
        }

        private void SavePositionProperty()
        {
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.JogAcc, this.txtJogAcc.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.JogDec, this.txtJogDec.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.JogFastVel, this.txtJogFastVel.Text);
            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.JogSlowVel, this.txtJogSlowVel.Text);

            ConfigMotion.SetValue(_categoryAxisNumber, Idx.MotionProperfy.RelativeDistance, this.txtRelativeDistance.Text);
        }

        private void SetAnalogCommand(string commandName, string value, bool isProperty = false, bool isInvert = false)
        {
            string tagName = _tagOutputHeaderName + commandName;

            double dValue;

            if (isProperty == true)
            {
                dValue = Convert.ToDouble(ConfigMotion.GetValue(_categoryAxisNumber, value));
            }
            else
            {
                dValue = Convert.ToDouble(value);
            }

            if (isInvert == true)
            {
                _motionTags[tagName].Is = -dValue;
            }
            else
            {
                _motionTags[tagName].Is = dValue;
            }

            Thread.Sleep(10);
        }

        private void SetDigitalCommand(string commandName, string value, bool isInvert = false)
        {
            string tagName = _tagOutputHeaderName + commandName;

            int nValue = Convert.ToInt32(value);

            if (isInvert == true)
            {
                _motionTags[tagName].Is = -nValue;
            }
            else
            {
                _motionTags[tagName].Is = nValue;
            }

            Thread.Sleep(10);
        }

        private void SetupMotion_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //// Position 읽어 넣기
            lstPosition.Items.Clear();
            lstPosition.ItemsSource = LoadMotionPosition(); // 리스트뷰 연결

            // Data(Property) 읽어서 넣기
            LoadData();
        }

        private void SetupMotion_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveData();
        }

        private void TabControlsBtnAlarmReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetDigitalCommand(Idx.MotionAction.AlarmReset, Idx.DigitalValue.Enable.ToString());
        }

        private void TabControlsBtnHomeSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveData();

            if (CheckServoOn() == false)
            {
                MessageBox.Show("Check the motion status");
                return;
            } // else

            Thread test = new Thread(RunHomeSearch);
            test.Start();
        }

        private void TabControlsBtnServerOn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.btnServerOn.IsChecked == true)
            {
                SetDigitalCommand(Idx.MotionAction.ServoOn, Idx.DigitalValue.Enable.ToString());
            }
            else
            {
                SetDigitalCommand(Idx.MotionAction.ServoOn, Idx.DigitalValue.Disable.ToString());
            }
        }

        private void TabControlsBtnStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunMotionStop();
        }

        private void TabPositionBtnMove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunMove();
        }

        private void TabPositionBtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            lstPosition.UnselectAll();

            SaveMotionPosition();

            // Postion 읽어 넣기
            lstPosition.ItemsSource = null;
            lstPosition.Items.Clear();
            lstPosition.ItemsSource = LoadMotionPosition(); // 리스트뷰 연결
        }

        private void TabPositionBtnStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RunMotionStop();
        }

        private void UcMtSetupMotion_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == false)
            {
                SaveData();
            }
            else
            {
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
        }

        private void UpdateMotionPosition(MotionPosition mp)
        {
            if (mp.Name != string.Empty)
            {
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.Name, mp.Name);
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.MoveType, mp.MoveType);
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.Position, mp.Position.ToString());
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.Speed, mp.Speed.ToString());
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.Accel, mp.Accel.ToString());
                ConfigMotion.SetValue(mp.CategoryName, Idx.MotionInfo.Decel, mp.Decel.ToString());
            }// else
        }

        private void UpdatePosition(string relativeType, double position)
        {
            try
            {
                int selectIndex = lstPosition.SelectedIndex;

                if (selectIndex < 0)
                {
                    MessageBox.ShowDialog("Please, Select a Position");
                    return;
                }// else

                List<MotionPosition> changeList = lstPosition.ItemsSource as List<MotionPosition>;

                MotionPosition motionPosition = changeList[selectIndex];

                motionPosition.MoveType = relativeType;
                motionPosition.Position = position;

                if (rdbFast.IsChecked == true)
                {
                    motionPosition.Speed = Convert.ToDouble(txtJogFastVel.Text);
                }
                else
                {
                    motionPosition.Speed = Convert.ToDouble(txtJogSlowVel.Text);
                }

                motionPosition.Accel = Convert.ToDouble(txtJogAcc.Text);
                motionPosition.Decel = Convert.ToDouble(txtJogDec.Text);

                changeList[selectIndex] = motionPosition;

                lstPosition.ItemsSource = null;
                lstPosition.ItemsSource = changeList;
                lstPosition.SelectedIndex = selectIndex;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Failed to add Motion position.");
            }
        }

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else // timeout
            {
                Logger.Error("Timeout");
                return false;
            }
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