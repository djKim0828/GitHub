using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="InputSimCmd"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2021-01-20
    /// Description : object detail description.
    /// </summary>
    public class InputSimCmd : InputSimCmdDialog
    {
        #region Fields

        private List<string> _cmds;
        private string[] _nameList;
        private string _simCmd;
        private int locale;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description :  objcet constructor.
        /// </summary>
        public InputSimCmd(string[] nameList, string simCmd)
        {
            _simCmd = simCmd;
            _nameList = nameList;

            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description :  object destructor.
        /// </summary>
        ~InputSimCmd()
        {
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description :  kind of language. [0=English,1=Korea,2=Chinese,3=Vietnam,4=India]
        /// </summary>
        public int Locale
        {
            get
            {
                return locale;
            }
            set
            {
                locale = value;
                InitLocale();
            }
        }

        public string SimCmd
        {
            get
            {
                return _simCmd;
            }
        }

        #endregion Properties

        #region Methods

        public MessageBoxReturnValue ReturnValue = MessageBoxReturnValue.CANCEL;

        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnValue = MessageBoxReturnValue.CANCEL;
            this.Close();
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lsvCmd.SelectedIndex >= 0)
            {
                _cmds.RemoveAt(lsvCmd.SelectedIndex);
                InitControls();
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : Close Button Event
        /// </summary>
        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnInsert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string cmd = string.Empty;

            if (checkInputData(ref cmd) == false)
            {
                System.Windows.Forms.MessageBox.Show("Check the data");
                return;
            }

            _cmds.Add(cmd);

            InitControls();
        }

        private void BtnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _simCmd = string.Empty;
            foreach (string cmd in _cmds)
            {
                string data = cmd.Replace(';', ' ').Trim();
                _simCmd += data + ";";
            }

            this.ReturnValue = MessageBoxReturnValue.OK;
            this.Close();
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lsvCmd.SelectedIndex >= 0)
            {
                string cmd = string.Empty;

                if (checkInputData(ref cmd) == false)
                {
                    System.Windows.Forms.MessageBox.Show("Check the data");
                    return;
                }

                _cmds[lsvCmd.SelectedIndex] = cmd;

                InitControls();
            }
        }

        private bool checkInputData(ref string cmd)
        {
            if (cmbInputName.SelectedValue.ToString() == string.Empty)
            {
                return false;
            }

            if (cmbInputType.SelectedValue.ToString() == string.Empty)
            {
                return false;
            }

            if (txbInputDefault.Text.ToString() == string.Empty)
            {
                return false;
            }

            if (txbInputReAct.Text.ToString() == string.Empty)
            {
                return false;
            }

            if (txbIpnutDelay.Text.ToString() == string.Empty)
            {
                return false;
            }

            cmd += cmbInputName.SelectedValue.ToString() + "/";
            cmd += cmbInputType.SelectedValue.ToString() + "/";
            cmd += txbInputDefault.Text + "/";
            cmd += txbInputReAct.Text + "/";
            cmd += txbIpnutDelay.Text + ";";

            return true;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : Mouse Move Event
        /// </summary>
        private void Form_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 32)
                {
                    DragMove();
                }
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void InitCmdList()
        {
            this.lsvCmd.Items.Clear();

            // 좌측 리스트
            foreach (string cmd in _cmds)
            {
                String[] data = cmd.Split('/');
                this.lsvCmd.Items.Add(data[0]);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            InitCmdList();

            // Name
            this.cmbInputName.Items.Clear();
            foreach (string cmd in _nameList)
            {
                this.cmbInputName.Items.Add(cmd);
            }

            // Type
            cmbInputType.Items.Clear();
            for (int i = 0; i <= 5; i++)
            {
                cmbInputType.Items.Add(i.ToString());
            }

            // Default Value
            txbInputDefault.Text = string.Empty;

            // ReAct Value
            txbInputReAct.Text = string.Empty;

            // Delay Time
            txbIpnutDelay.Text = string.Empty;

            return 0;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();

            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == 0 || resultLocale == 0 || resultControls == 0 || resultEvent == 0)
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
        /// Date :  2021-01-20
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                _cmds = new List<string>();
                _cmds.Clear();

                string[] temp = _simCmd.Split(';');

                foreach (string cmd in temp)
                {
                    if (cmd != string.Empty)
                    {
                        _cmds.Add(cmd);
                    }
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            locale = 0;
            // add your code here

            return 0;
        }

        private void LsvCmd_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void LsvCmd_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                String selectData = _cmds[lsvCmd.SelectedIndex];

                cmbInputName.SelectedValue = selectData.Split('/')[0];

                cmbInputType.SelectedValue = selectData.Split('/')[1];

                txbInputDefault.Text = selectData.Split('/')[2];

                txbInputReAct.Text = selectData.Split('/')[3];

                txbIpnutDelay.Text = selectData.Split('/')[4].Replace(';', ' ').Trim();
            }
            catch
            {
                Logger.Error("Check the input data");
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2021-01-20
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.MouseMove += Form_MouseMove;
            this.btnInputTextDlgClose.Click += BtnDlgClose_Click;

            lsvCmd.SelectionChanged += LsvCmd_SelectionChanged;

            btnInsert.Click += BtnInsert_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;

            // Botton
            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;

            return 0;
        }

        #endregion Methods

        #region Enums

        public enum MessageBoxReturnValue
        {
            OK,
            CANCEL
        }

        #endregion Enums
    }
}