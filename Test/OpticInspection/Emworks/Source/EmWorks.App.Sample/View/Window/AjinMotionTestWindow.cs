using EmWorks.DevDrv.AjinMotionIo;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EmWorks.App.Sample.View.Window
{
    public partial class AjinMotionTestWindow : Form
    {
        #region Fields

        private List<IoInputStatus> _IoInputStatus;
        private List<IoOutputStatus> _IoOutputStatus;
        private bool _isOpen = false;
        private AjinMotion _motion;
        private Thread _ThreadIoCheck;

        #endregion Fields

        #region Constructors

        public AjinMotionTestWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public string HomeDir()
        {
            string currentPath = System.Windows.Forms.Application.StartupPath;
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            return currentPath;
        }

        private void AjinMotionTestWindow_Load(object sender, EventArgs e)
        {
            txtPath.Text = HomeDir() + @"\Config\Tag\AjinMotion.json";
            btnLoad_Click(null, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;

            _motion = new AjinMotion(filePath, chkSim.Checked); // 객체 생성시에 설정파일(Json)을 파라미터로 넣으면 자동으로 Load된다.

            dgvList.DataSource = new List<TagIdentity>(_motion.GetAllData().Values);

            try
            {
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }

            gridBox.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadInputOutputList();

            if (_motion.SetsyDeviceMotion(true) == true)
            {
                lblStatus1.BackColor = Color.Green;
                _isOpen = true;

                _ThreadIoCheck = new Thread(RunIOCheck);
                _ThreadIoCheck.IsBackground = true;
                _ThreadIoCheck.Start();
            }
            else
            {
                MessageBox.Show("Failed");
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _motion.Save();
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
                    //if (name == "xInRobotPad1Down")
                    //{
                    //    int tt = 0;
                    //}

                    object temp = new object();
                    temp = _motion.GetType().GetProperty(name).GetValue(_motion);

                    Tag checkTag = ((Tag)temp);

                    if (checkTag.Is == null)
                    {
                        checkTag.Is = 0;
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
                        dataGridView.Rows[i].Cells["Status"].Style.BackColor = Color.Red;
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
            foreach (TagIdentity tg in _motion.GetAllData().Values)
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
                    _motion.GetType().GetProperty(name).SetValue(_motion, id.GetInputData());
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
                    _motion.GetType().GetProperty(name).SetValue(_motion, dd.DigitaldialogResult);
                } // else
            }
            catch
            {
            }

            dd.Close();
        }

        private void RunIOCheck()
        {
            try
            {
                while (_isOpen)
                {
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

        #endregion Methods

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

        private void btnClose1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_motion.SetsyDeviceMotion(false, 1000) == true)
                {
                    lblStatus1.BackColor = Color.Crimson;
                    _isOpen = false;

                    _ThreadIoCheck.Abort();
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
            catch
            {
                MessageBox.Show("설정로드 후 재시도 하십시오.");
            }
        }

        private void btnCheckStatus1_Click(object sender, EventArgs e)
        {
            if (_motion.sxDeviceMotion.Is.ToString() == "1")
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
    }
}