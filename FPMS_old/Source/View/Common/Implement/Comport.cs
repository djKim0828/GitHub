using EmWorks.Lib.Common;
using EmWorks.View;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace FPMS.E2105_FS111_121
{
    public class Comport : EmWorks.View.ComportComponent
    {
        #region Enums

        private enum comList
        {
            PortNum,
            Baudrate,
            DataBits,
            ParityBitsString,
            StopBitsString,
            DtrEnable,
            RtsEnable,
            RecvTerminateString,
            SendTerminateString
        }

        #endregion Enums

        #region Fields

        private ComportModel _comport;
        private ComportModel _defaultPort;
        private bool _mode;

        private List<labeledComboboxComponent> cmbList = new List<labeledComboboxComponent>();

        #endregion Fields

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Constructors

        public Comport(ComportModel port, ComportModel defaultPort, bool mode = false)
        {
            _comport = port;
            _defaultPort = defaultPort;
            _mode = mode;
            Initialize();
            // add your code here
        }

        #endregion Constructors

        #region Destructors

        ~Comport()
        {
            // add your code here
        }

        #endregion Destructors

        #region Methods

        private void Comport_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            cmbList[(int)comList.PortNum].comboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                cmbList[(int)comList.PortNum].comboBox.Items.Add(port);
            }
        }

        private int InitControls()
        {
            string[] namelist = { "Port Number", "Baud Rate", "Data Bits", "Parity Bit", "Stop Bit", "Dtr Enable", "Rts Enable", "Receive Terminate", "Send Terminate" };
            //판넬 선언
            ListStackPanel.Children.Clear();

            for (int i = 0; i < Enum.GetValues(typeof(comList)).Length; i++)
            {
            }
            foreach (int item in Enum.GetValues(typeof(comList)))
            {
                bool editablecombo = false;

                if (item >= (int)comList.RecvTerminateString)
                {
                    editablecombo = true;
                }

                cmbList.Add(new labeledComboboxComponent(editablecombo));

                if (_mode == true && item >= (int)comList.DtrEnable)
                {
                    continue;
                }

                ListStackPanel.Children.Add(cmbList[item]);

                cmbList[item].nameLabel.Content = namelist[item];
                cmbList[item].defaultLabel.Content = _defaultPort.PortNum;
            }

            //ComboBox List 추가
            string[] ports = SerialPort.GetPortNames();

            foreach (var port in ports)
            {
                cmbList[(int)comList.PortNum].comboBox.Items.Add(port);
            }

            string[] BaudrateList = { "110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "57600", "115200", "128000", "256000" };

            foreach (var port in BaudrateList)
            {
                cmbList[(int)comList.Baudrate].comboBox.Items.Add(port);
            }

            string[] DataBitsList = { "6", "7", "8" };

            foreach (var port in DataBitsList)
            {
                cmbList[(int)comList.DataBits].comboBox.Items.Add(port);
            }

            foreach (var port in Enum.GetNames(typeof(Parity)))
            {
                cmbList[(int)comList.ParityBitsString].comboBox.Items.Add(port);
            }

            foreach (var port in Enum.GetNames(typeof(StopBits)))
            {
                cmbList[(int)comList.StopBitsString].comboBox.Items.Add(port);
            }
            cmbList[(int)comList.DtrEnable].comboBox.Items.Add("False");
            cmbList[(int)comList.DtrEnable].comboBox.Items.Add("True");
            cmbList[(int)comList.RtsEnable].comboBox.Items.Add("False");
            cmbList[(int)comList.RtsEnable].comboBox.Items.Add("True");

            cmbList[(int)comList.RecvTerminateString].comboBox.Items.Add("CR ( \\r, 0D )");
            cmbList[(int)comList.RecvTerminateString].comboBox.Items.Add("LF ( \\n, 0A )");
            cmbList[(int)comList.RecvTerminateString].comboBox.Items.Add("CR+LF ( \\r+\\n, 0D 0A )");

            cmbList[(int)comList.SendTerminateString].comboBox.Items.Add("CR ( \\r, 0D )");
            cmbList[(int)comList.SendTerminateString].comboBox.Items.Add("LF ( \\n, 0A )");
            cmbList[(int)comList.SendTerminateString].comboBox.Items.Add("CR+LF ( \\r+\\n, 0D 0A )");

            //ComboBox 초기값 설정.
            cmbList[(int)comList.PortNum].comboBox.SelectedIndex = cmbList[(int)comList.PortNum].comboBox.Items.IndexOf(_comport.PortNum);
            cmbList[(int)comList.Baudrate].comboBox.SelectedIndex = cmbList[(int)comList.Baudrate].comboBox.Items.IndexOf(_comport.BaudRate);
            cmbList[(int)comList.DataBits].comboBox.SelectedIndex = cmbList[(int)comList.DataBits].comboBox.Items.IndexOf(_comport.DataBits);
            cmbList[(int)comList.ParityBitsString].comboBox.SelectedIndex = cmbList[(int)comList.ParityBitsString].comboBox.Items.IndexOf(_comport.ParityBitsString);
            cmbList[(int)comList.StopBitsString].comboBox.SelectedIndex = cmbList[(int)comList.StopBitsString].comboBox.Items.IndexOf(_comport.StopBitsString);
            cmbList[(int)comList.DtrEnable].comboBox.SelectedIndex = cmbList[(int)comList.DtrEnable].comboBox.Items.IndexOf(_comport.DtrEnable.ToString());
            cmbList[(int)comList.RtsEnable].comboBox.SelectedIndex = cmbList[(int)comList.RtsEnable].comboBox.Items.IndexOf(_comport.RtsEnable.ToString());

            //View Default Data 입력
            cmbList[(int)comList.PortNum].defaultLabel.Content = _defaultPort.PortNum;
            cmbList[(int)comList.Baudrate].defaultLabel.Content = _defaultPort.BaudRate;
            cmbList[(int)comList.DataBits].defaultLabel.Content = _defaultPort.DataBits;
            cmbList[(int)comList.ParityBitsString].defaultLabel.Content = _defaultPort.ParityBitsString;
            cmbList[(int)comList.StopBitsString].defaultLabel.Content = _defaultPort.StopBitsString;
            cmbList[(int)comList.DtrEnable].defaultLabel.Content = _defaultPort.DtrEnable;
            cmbList[(int)comList.RtsEnable].defaultLabel.Content = _defaultPort.RtsEnable;
            cmbList[(int)comList.RecvTerminateString].defaultLabel.Content = _defaultPort.RecvTerminateString;
            cmbList[(int)comList.SendTerminateString].defaultLabel.Content = _defaultPort.SendTerminateString;

            return Idx.FunctionResult.True;
        }

        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
                //ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                _IsInitialled = false;
            }

            // add your code here
        }

        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;
                //_loopInterval = 5;
                //_isLoop = false;

                return Idx.FunctionResult.True;
            }
            catch (Exception ex)
            {
                //Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private int RegistEvents()
        {
            this.IsVisibleChanged += Comport_IsVisibleChanged;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}