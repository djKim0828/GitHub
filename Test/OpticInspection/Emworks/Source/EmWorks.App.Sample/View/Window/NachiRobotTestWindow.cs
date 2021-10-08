using EmWorks.DevDrv.NachiRobot;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmWorks.App.Sample.View.Window
{
    public partial class NachiRobotTestWindow : Form
    {
        //private List<IoInputStatus> _IoInputStatus;
        //private List<IoOutputStatus> _IoOutputStatus;
        private bool _isOpen = false;
        private Nachi_Out _NachiRobot;
        private Thread _ThreadIoCheck;

        public NachiRobotTestWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;

            _NachiRobot = new Nachi_Out(filePath, chkSim.Checked); // 객체 생성시에 설정파일(Json)을 파라미터로 넣으면 자동으로 Load된다.

            dgvList.DataSource = new List<TagIdentity>(_NachiRobot.GetAllData().Values);

            try
            {
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }

            gridBox.Enabled = true;
            btnSave.Enabled = true;


            WriteLog("Config Load Complete");
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = HomeDir() + @"\Config\Tag";
            openFileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = openFileDialog.FileName;
            } //else
        }

        public string HomeDir()
        {
            string currentPath = System.Windows.Forms.Application.StartupPath;
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            return currentPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _NachiRobot.Save();
        }

        OutRobotControl _OutRobotControl;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadInputOutputList();

            _OutRobotControl = new OutRobotControl(_NachiRobot, chkSim.Checked);
            _OutRobotControl.LoggerMessage += _robotControl_LoggerMessage;

            if (_OutRobotControl.Open() == true)
            {
                _isOpen = true;

                _ThreadIoCheck = new Thread(RunStatusCheck);
                _ThreadIoCheck.IsBackground = true;
                _ThreadIoCheck.Start();

                WriteLog("Open Complete");
            }
            else
            {
                WriteLog("Open Fail");
            }
        }

        private List<IoInputStatus> _IoInputStatus;
        private List<IoOutputStatus> _IoOutputStatus;

        private void initAllGridView(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            dataGridView.AutoResizeColumns();
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.AllowUserToOrderColumns = true;
        }

        private void LoadInputOutputList()
        {
            _IoInputStatus = new List<IoInputStatus>();
            _IoOutputStatus = new List<IoOutputStatus>();

            int inputIndex = 0;
            int outputIndex = 0;
            foreach (TagIdentity tg in _NachiRobot.GetAllData().Values)
            {
                if (tg.IoType == 0 || tg.IoType == 2)
                {
                    IoInputStatus temp = new IoInputStatus();

                    temp.No = inputIndex.ToString();
                    temp.Offset = tg.Offset.ToString();
                    temp.Name = tg.Name;
                    temp.DataType = tg.DataType.ToString();
                    temp.Status = "0";
                    temp.Value = "0";
                    temp.Desc = tg.Desc;

                    //if (tg.Name == "xTowerRedOn")
                    //{
                    //    int ii = 0;
                    //}

                    //if (tg.Default != null && tg.Default.ToString() != string.Empty)
                    //{
                    //    tg.Is = tg.Default;
                    //}
                    //else
                    //{
                    //    tg.Is = "0";
                    //}

                    _IoInputStatus.Add(temp);

                    inputIndex++;
                }
                else
                {
                    if (tg.Item == "OffsetData")
                    {
                        continue;
                    }

                    IoOutputStatus temp = new IoOutputStatus();

                    //if (tg.Name == "yOutRobotPad1Return")
                    //{
                    //    int ii = 0;
                    //}

                    temp.No = outputIndex.ToString();
                    temp.Offset = tg.Offset.ToString();
                    temp.Name = tg.Name;
                    temp.DataType = tg.DataType.ToString();
                    temp.Status = "0";
                    temp.Value = "0";
                    temp.Desc = tg.Desc;

                    _IoOutputStatus.Add(temp);

                    outputIndex++;
                }
            }

            dgvInput.DataSource = _IoInputStatus;
            dgvOutput.DataSource = _IoOutputStatus;

            initAllGridView(dgvInput);
            initAllGridView(dgvOutput);

            dgvInput.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInput.MultiSelect = false;
            dgvInput.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgvOutput.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOutput.MultiSelect = false;
            dgvOutput.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvOutput.CellMouseDoubleClick += DgvOutput_CellMouseDoubleClick;
        }

        private void RunAnalogCommand()
        {
            InputDialog id = new InputDialog();
            id.ShowDialog();

            try
            {
                if (id.DigitaldialogResult != InputDialog.digitaldialogResult.Cancel)
                {
                    string name = dgvOutput.SelectedRows[0].Cells["Name"].Value.ToString();
                    double value = Convert.ToDouble(id.GetInputData());
                    _NachiRobot.GetType().GetProperty(name).SetValue(_NachiRobot, value);
                } // else
            }
            catch
            {
            }

            id.Close();
        }

        private void RunDigitalCommand()
        {
            DigitalCommandDialog dd = new DigitalCommandDialog();
            dd.ShowDialog();

            try
            {
                if (dd.DigitaldialogResult != DigitalCommandDialog.digitaldialogResult.Cancel)
                {
                    string name = dgvOutput.SelectedRows[0].Cells["Name"].Value.ToString();
                    _NachiRobot.GetType().GetProperty(name).SetValue(_NachiRobot, dd.DigitaldialogResult);
                } // else
            }
            catch
            {
            }

            dd.Close();
        }

        private void DgvOutput_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvOutput.SelectedRows[0].Cells["DataType"].Value.ToString() == EmWorks.Lib.Core.Tag.Idx.DataType.Int.ToString())
            {
                RunDigitalCommand();
            }
            else
            {
                RunAnalogCommand();
            }
        }

        private void RunStatusCheck()
        {
            try
            {
                while (_isOpen)
                {
                    if (_NachiRobot.sxDeviceOutRobot.Is != null)
                    {
                        if (_NachiRobot.sxDeviceOutRobot.Is.ToString() == OpenClose.Open.ToString())
                        {
                            lblStatus1.BackColor = Color.Green;
                        }
                        else
                        {
                            lblStatus1.BackColor = Color.Crimson;
                        }
                    }

                    if (_NachiRobot.xOutRobotClient1Connect.Is != null)
                    {
                        if (_NachiRobot.xOutRobotClient1Connect.Is.ToString() == OpenClose.Open.ToString())
                        {
                            lblConnect1.BackColor = Color.Green;
                        }
                        else
                        {
                            lblConnect1.BackColor = Color.Crimson;
                        }
                    }

                    checkStatus(dgvInput, true);
                    checkStatus(dgvOutput, false);

                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                _isOpen = false;
            }
        }

        private void checkStatus(DataGridView dataGridView, bool isInput)
        {
            int checkIndex = 0;

            try
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    checkIndex++;

                    string name = dataGridView.Rows[i].Cells["Name"].Value.ToString();

                    // 테스트를 위해 주석처리 ( 추후 삭제 )
                    //if (name == "xOutRobotCURRENT_POS_BITS_1")
                    //{
                    //    int tt = 0;
                    //}

                    object temp = new object();
                    temp = _NachiRobot.GetType().GetProperty(name).GetValue(_NachiRobot);

                    Tag checkTag = ((Tag)temp);

                    if (checkTag.Is == null)
                    {
                        continue;
                    }

                    string status = checkTag.Is.ToString();

                    if (isInput == true)
                    {
                        _IoInputStatus[i].Status = status;
                    }
                    else
                    {
                        _IoOutputStatus[i].Status = status;
                    }

                    if (status == "1")
                    {
                        dataGridView.Rows[i].Cells["Status"].Style.BackColor = Color.Green;
                    }
                    else if (status == "0")
                    {
                        dataGridView.Rows[i].Cells["Status"].Style.BackColor = Color.Crimson;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells["Status"].Style.BackColor = Color.Gray;
                    }
                }
            }
            catch (System.Exception ex)
            {
                int a = checkIndex;
                Logger.Exception(ex);
            }
        }

        private void _robotControl_LoggerMessage(object sendor, OutRobotControl.EventLoggerMessageArgs e)
        {
            WriteLog(e.Message);
        }

        private void NachiRobotTestWindow_Load(object sender, EventArgs e)
        {
            txtPath.Text = HomeDir() + @"\Config\Tag\Nachi_Out.json";
            btnLoad_Click(null, null);

            tabControl1.SelectedIndex = 1;
        }

        private void btnCheckStatus1_Click(object sender, EventArgs e)
        {
            if (_NachiRobot.sxDeviceOutRobot.Is.ToString() == "1")
            {
                lblStatus1.BackColor = Color.Green;
                _isOpen = true;
            }
            else
            {
                lblStatus1.BackColor = Color.Crimson;
                _isOpen = false;
            }
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_NachiRobot.SetsyDeviceOutRobot(false, 1000) == true)
                {
                    lblStatus1.BackColor = Color.Crimson;
                    _isOpen = false;

                    _ThreadIoCheck.Abort();

                    dgvInput.DataSource = null;
                    dgvOutput.DataSource = null;

                    lblConnect1.BackColor = Color.Crimson;

                    WriteLog("Close Complate");
                }
                else
                {
                    WriteLog("Failed");
                }
            }
            catch
            {
                WriteLog("설정로드 후 재시도 하십시오.");
            }
        }

        private void WriteLog(string data)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + data + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                Action action = () =>
                {
                    _OutRobotControl.SetRunInitialize();
                };

                action.BeginInvoke(null, null);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                WriteLog("Open 후에 다시 시도하세요.");
            }
            
        }

        #region Classes
        public class IoInputStatus
        {
            #region Properties

            public string No { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Offset { get; set; }
            public string Value { get; set; }
            public string DataType { get; set; }
            public string Desc { get; set; }

            #endregion Properties
        }

        public class IoOutputStatus
        {
            #region Properties

            public string No { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Offset { get; set; }
            public string Value { get; set; }
            public string DataType { get; set; }
            public string Desc { get; set; }

            #endregion Properties
        }

        #endregion Classes
       

        private void chkPause_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPause.Checked == true)
            {
                chkPause.BackColor = Color.Crimson;
                
            }
            else
            {
                chkPause.BackColor = Color.Gray;
            }
        }

        private void BtnUnloadPositionA_Click(object sender, EventArgs e)
        {
            _NachiRobot.yOutRobotOffsetdataUNLOAD_POSITION_A = OnOff.On;

            // 정지 확인 필요

        }

        private void btnLdEndWait_Click(object sender, EventArgs e)
        {
            
        }
        private void btnLdApproach_Click(object sender, EventArgs e)
        {

        }

        private void checkMoveComplete()
        {
            // Complete 확인
            for (int i = 0; i < 100; i++)
            {

                if (_NachiRobot.xOutRobotMANUAL_MOVE_COMPLETE.Is.ToString() == OnOff.On.ToString())
                {
                    break;
                }

                Thread.Sleep(50);
            }            
        }


        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            _OutRobotControl.SetAlarmReset();
        }

  

        private bool checkIO(Tag tag, int isOn)
        {
            // Complete 확인
            for (int i = 0; i < 100; i++)
            {
                if (tag.Is != null)
                {
                    if (tag.Is.ToString() == isOn.ToString())
                    {
                        return true;
                    }
                }

                Thread.Sleep(50);
            }


            return false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                return;
            }

            //int rowIndex = -1;
            int selectIndex = dgvList.CurrentCell.RowIndex + 1;

            for (int i=selectIndex;i<dgvList.RowCount;i++)
            {
                if (dgvList.Rows[i].Cells[0].Value.ToString().ToUpper().IndexOf(txtSearch.Text.ToUpper()) >= 0)
                {
                    dgvList.CurrentCell = dgvList.Rows[i].Cells[0];
                    dgvList.Rows[i].Selected = true;
                    return;
                }
            }

            MessageBox.Show("검색을 완료 했습니다.");

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }
        private void BtnCondition1_Click(object sender, EventArgs e)
        {
            _OutRobotControl.RunLoad();
        }

        private void btnCondition2_Click(object sender, EventArgs e)
        {
            //_OutRobotControl.RunMoveCondition2();
        }

        private void btnCondition3_Click(object sender, EventArgs e)
        {
            _OutRobotControl.RunUnload();
        }
    }
}
