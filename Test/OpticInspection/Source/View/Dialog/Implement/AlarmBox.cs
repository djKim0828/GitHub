using EmWorks.Lib.Common;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="AlarmBox"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-03-30 11:30 (create or edit date.)
    /// Description : object detail description.
    /// </summary>
    public class AlarmBox : EmWorks.View.AlarmBoxDialog
    {
        #region Fields

        private string _alarmId;
        private AlarmModel _alarmModel;
        private bool _isLoad = false;
        private object[] _parameters;
        private string _Type = Idx.AlarmType.Normal;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description :  objcet constructor.
        /// </summary>
        public AlarmBox(object[] parameters)
        {
            Initialize();

            _parameters = parameters;

            _alarmId = parameters[Idx.AlarmEventIndex.Id].ToString();
        }

        public AlarmBox(AlarmModel alarmModel)
        {
            Initialize();

            _alarmModel = alarmModel;

            _alarmId = _alarmModel._Id;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description :  object destructor.
        /// </summary>
        ~AlarmBox()
        {
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void AlarmBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LoadAlarmInfo() == true)
            {
                _isLoad = true;

                LoadImage();
                SetButtons();
            }
            else
            {
                _isLoad = false;

                ChangeModifyMode();
            }

            InitLocale();
        }

        private void Btn1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnModify_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_isLoad == true)
            {
                ChangeModifyMode();
            }
        }

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Alarm을 추가한다.
            InsertAlarm();
        }

        private void ChangeModifyMode()
        {
            btnModify.Visibility = System.Windows.Visibility.Hidden;

            grdName.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            grdDesc.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);

            txtAlarmName.IsEnabled = true;

            if (string.IsNullOrEmpty(_alarmModel._Name) == false)
            {
                txtAlarmName.Text = _alarmModel._Name;
            }
            else
            {
                txtAlarmName.Text = "Insert Name.";
            }

            txtAlarmDesc.IsEnabled = true;

            if (string.IsNullOrEmpty(_alarmModel._Description) == false)
            {
                txtAlarmDesc.Text = _alarmModel._Description;
            }
            else
            {
                txtAlarmDesc.Text = "Insert Description.";
            }

            btnSave.Visibility = System.Windows.Visibility.Visible;
            /*
            btnModify.Visibility = System.Windows.Visibility.Hidden;

            grdName.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            grdDesc.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);

            txtAlarmName.IsEnabled = true;
            if (txtAlarmName.Text == string.Empty)
            {
                if (_parameters.Length > Idx.AlarmEventIndex.Name &&
                    _parameters[Idx.AlarmEventIndex.Name] != null)
                {
                    txtAlarmName.Text = _parameters[Idx.AlarmEventIndex.Name].ToString();
                }
                else
                {
                    txtAlarmName.Text = "Insert Name.";
                }
            }

            txtAlarmDesc.IsEnabled = true;
            if (txtAlarmDesc.Text == string.Empty)
            {
                if (_parameters.Length > Idx.AlarmEventIndex.Description &&
                    _parameters[Idx.AlarmEventIndex.Description] != null)
                {
                    txtAlarmDesc.Text = _parameters[Idx.AlarmEventIndex.Description].ToString();
                }
                else
                {
                    txtAlarmDesc.Text = "Insert Description.";
                }
            }

            btnSave.Visibility = System.Windows.Visibility.Visible;
            */
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : Mouse Move Event
        /// </summary>
        private void GrdTextDlgTitle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 50)
                {
                    DragMove();
                }
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // add your code here

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

            //int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                // add your code here

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        private void InsertAlarm()
        {
            _alarmModel._Name = txtAlarmName.Text;
            _alarmModel._Type = (AlarmType)Enum.Parse(typeof(AlarmType), _Type);
            _alarmModel._Description = txtAlarmDesc.Text;
            _alarmModel._ImgPath = string.Empty;

            AlarmDefine.UpdateAlarm(_alarmModel);
        }

        private bool LoadAlarmInfo()
        {
            lblMessageBoxDlgTitle.Content = "Alarm" + " [ " + _alarmId + " ]";

            if (AlarmDefine.GetData(_alarmId, ref _alarmModel) == false)
            {
                return false;
            }
            txtAlarmName.Text = _alarmModel._Name;
            lblAlarmTime.Content = _alarmModel._DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtAlarmDesc.Text = _alarmModel._Description;

            return true;
        }

        private void LoadImage()
        {
            try
            {
                string imgPath = _alarmModel._ImgPath;
                if (imgPath == string.Empty)
                {
                    imgPath = AlarmDefine.GetAlarmRootPath() + @"\Images\" + _alarmId + ".png";
                } // else

                if (File.Exists(imgPath) == true)
                {
                    var uri = new Uri(imgPath);
                    var bitmap = new BitmapImage(uri);

                    imgAlarm.Width = bitmap.Width;
                    imgAlarm.Height = bitmap.Height;
                    imgAlarm.Source = bitmap;
                }
                else
                {
                    imgAlarm.Height = 0;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-03-30 11:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.grdMessageBoxDlgTitle.MouseMove += GrdTextDlgTitle_MouseMove;
            this.btnMessageBoxDlgClose.Click += BtnDlgClose_Click;

            this.btn1.Click += Btn1_Click;
            this.btn2.Click += Btn2_Click;
            this.btn3.Click += Btn3_Click;
            this.btnSave.Click += BtnSave_Click;

            this.btnModify.Click += BtnModify_Click;

            this.Loaded += AlarmBox_Loaded;

            return Idx.FunctionResult.True;
        }

        private void SetButtons()
        {
            btnSave.Visibility = System.Windows.Visibility.Hidden;

            // Todo : 추후 재정의 필요
            _Type = _alarmModel._Type.ToString();

            if (_Type == Idx.AlarmType.Heavy)
            {
                // 무조건 확인하여 OK를 눌러야 한다.
                btn3.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (_Type == Idx.AlarmType.Light)
            {
                // 무시할 수 있다.
            }
            else
            {
                // 그냥 닫기만 있다.
                btn2.Visibility = System.Windows.Visibility.Hidden;
                btn3.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        #endregion Methods
    }
}