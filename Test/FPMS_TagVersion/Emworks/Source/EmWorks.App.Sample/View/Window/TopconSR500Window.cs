using EmWorks.App.Sample.View.Dialog;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EmWorks.DevDrv.TopconSR5000.EmTopconSR5000;

namespace EmWorks.App.Sample.View.Window
{
    public partial class TopconSR500Window : Form
    {
        #region Fields

        private List<IoInputStatus> _IoInputStatus;
        private List<IoOutputStatus> _IoOutputStatus;
        private bool _isOpen = false;

        private Thread _ThreadIoCheck;
        private Topcon_SR5000 _Topcon_SR5000;

        

        #endregion Fields

        #region Constructors

        public TopconSR500Window()
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

        private void btnCheckStatus1_Click(object sender, EventArgs e)
        {
            if (_Topcon_SR5000.sxDeviceSR5000.Is.ToString() == "1")
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
                if (_Topcon_SR5000.SetsyDeviceSR5000(false, 1000) == true)
                {
                    lblStatus1.BackColor = Color.Crimson;
                    _isOpen = false;

                    _ThreadIoCheck.Abort();

                    dgvInput.DataSource = null;
                    dgvOutput.DataSource = null;

                    lblConnect1.BackColor = Color.Crimson;

                    btnConnect.Enabled = false;

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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool result = false;

            ProcessingBox prBox = new ProcessingBox("Connect...");

            Task task = Task.Run(() => result = _Topcon_SR5000.SetySR5000Connect(true, 60000));
            task.GetAwaiter().OnCompleted(() =>
            {                
                prBox.Close();
            });

            prBox.ShowDialog();
            
            if (result == true)
            {
                WriteLog("Open Complete");
            }
            else
            {
                WriteLog("Open Fail");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;

            _Topcon_SR5000 = new Topcon_SR5000(filePath, chkSim.Checked); // 객체 생성시에 설정파일(Json)을 파라미터로 넣으면 자동으로 Load된다.            
            _Topcon_SR5000._device.DriverMessage += _device_DriverMessage;


            dgvList.DataSource = new List<TagIdentity>(_Topcon_SR5000.GetAllData().Values);

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

        private void _device_DriverMessage(object sendor, DevDrv.TopconSR5000.EmTopconSR5000.EventDriverMessageArgs e)
        {
            WriteLog(e.Message);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadInputOutputList();

            if (_Topcon_SR5000.SetsyDeviceSR5000(true, 2000))
            {
                _isOpen = true;

                _ThreadIoCheck = new Thread(RunStatusCheck);
                _ThreadIoCheck.IsBackground = true;
                _ThreadIoCheck.Start();

                WriteLog("Open Complete");

                btnLoadRecipe_Click(null, null);

                btnConnect.Enabled = true;
            }
            else
            {
                WriteLog("Open Fail");
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
            _Topcon_SR5000.Save();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                return;
            }

            //int rowIndex = -1;
            int selectIndex = dgvList.CurrentCell.RowIndex + 1;

            for (int i = selectIndex; i < dgvList.RowCount; i++)
            {
                if (dgvList.Rows[i].Cells[0].Value.ToString().ToUpper().IndexOf(txtSearch.Text.ToUpper()) >= 0)
                {
                    dgvList.CurrentCell = dgvList.Rows[i].Cells[0];
                    dgvList.Rows[i].Selected = true;
                    return;
                }
            }

            WriteLog("검색을 완료 했습니다.");
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
                    temp = _Topcon_SR5000.GetType().GetProperty(name).GetValue(_Topcon_SR5000);

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
            foreach (TagIdentity tg in _Topcon_SR5000.GetAllData().Values)
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
                    _Topcon_SR5000.GetType().GetProperty(name).SetValue(_Topcon_SR5000, value);
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
                    _Topcon_SR5000.GetType().GetProperty(name).SetValue(_Topcon_SR5000, dd.DigitaldialogResult);
                } // else
            }
            catch
            {
            }

            dd.Close();
        }

        private void RunStatusCheck()
        {
            try
            {
                while (_isOpen)
                {
                    if (_Topcon_SR5000.sxDeviceSR5000.Is != null)
                    {
                        if (_Topcon_SR5000.sxDeviceSR5000.Is.ToString() == OpenClose.Open.ToString())
                        {
                            lblStatus1.BackColor = Color.Green;
                        }
                        else
                        {
                            lblStatus1.BackColor = Color.Crimson;
                        }
                    }

                    if (_Topcon_SR5000.xSR5000Connect.Is != null)
                    {
                        if (_Topcon_SR5000.xSR5000Connect.Is.ToString() == OpenClose.Open.ToString())
                        {
                            lblConnect1.BackColor = Color.Green;

                            UpdateCaptureButtonEnable(true);
                        }
                        else
                        {
                            lblConnect1.BackColor = Color.Crimson;

                            UpdateCaptureButtonEnable(false);
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

        private void UpdateCaptureButtonEnable(bool isEnable)
        {
            Invoke(new MethodInvoker(() =>
            {
                btnCapture.Enabled = isEnable;
                btnCaptureStop.Enabled = isEnable;

                btnLiveStart.Enabled = isEnable;
                btnLiveStop.Enabled = isEnable;
            }));
        }

        private void TopconSR500Window_Load(object sender, EventArgs e)
        {
            txtPath.Text = HomeDir() + @"\Config\Tag\Topcon_SR5000.json";
            btnLoad_Click(null, null);

            tabControl1.SelectedIndex = 1;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
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

        public class OpenClose
        {
            #region Fields

            public const int Close = 0;
            public const int Open = 1;

            #endregion Fields
        }

        #endregion Classes

        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            if (_Topcon_SR5000.SetySR5000RecipeLoad(true, 1000) == true)
            {
                WriteLog("Complete load recipe");
            }
            else
            {
                WriteLog("Fail load recipe");
            }
        }

        private void btnLodaMes_Click(object sender, EventArgs e)
        {
            bool result = false;

            ProcessingBox prBox = new ProcessingBox("Loading...");

            Task task = Task.Run(() => result = _Topcon_SR5000.SetySR5000MeasurementLoad(true, 60000));
            task.GetAwaiter().OnCompleted(() =>
            {
                prBox.Close();
            });

            prBox.ShowDialog();

            if (result == true)
            {
                WriteLog("Complete load Measurement");

                GetPseudoImage();
            }
            else
            {
                WriteLog("Fail load Measurement");
            }            
        }

        private void btnGetPseudo_Click(object sender, EventArgs e)
        {
            GetPseudoImage();
        }

        private void GetPseudoImage()
        {
            bool result = false;

            ProcessingBox prBox = new ProcessingBox("Get Pseudo Image...");

            Task task = Task.Run(() => result = _Topcon_SR5000.SetySR5000PseudoImageRequest(true, 10000));
            task.GetAwaiter().OnCompleted(() =>
            {
                prBox.Close();
            });

            prBox.ShowDialog();

            if (result == false)
            {
                WriteLog("Fail Get PseudoImage");
            }

            object getObject = _Topcon_SR5000.xSR5000PseudoImageResult.Is;
            Type temp = getObject.GetType();

            if (temp.Equals(typeof(Bitmap)))
            {
                pBMeasurePseudo.Image = (Bitmap)getObject;
                WriteLog("Complete Get PseudoImage");
            }
            else
            {
                WriteLog("Fail Get PseudoImage");
            }
        }

        private void btnGetRGB_Click(object sender, EventArgs e)
        {
            bool result = false;

            ProcessingBox prBox = new ProcessingBox("Get RGB Image...");

            Task task = Task.Run(() => result = _Topcon_SR5000.SetySR5000RGBImageRequest(true, 10000));
            task.GetAwaiter().OnCompleted(() =>
            {
                prBox.Close();
            });

            prBox.ShowDialog();

            if (result == false)
            {
                WriteLog("Fail Get RGB Image");
            }

            object getObject = _Topcon_SR5000.xSR5000RGBImageResult.Is;
            Type temp = getObject.GetType();

            if (temp.Equals(typeof(Bitmap)))
            {
                pBMeasurePseudo.Image = (Bitmap)getObject;
                WriteLog("Complete Get RGB");
            }
            else
            {
                WriteLog("Fail Get RGB");
            }
        }

        private void btnSearchROI_Click(object sender, EventArgs e)
        {
            AutoSpotResultDialog autoSpotResultDialog =
                new AutoSpotResultDialog(_Topcon_SR5000,
                (Bitmap)pBMeasurePseudo.Image);
            autoSpotResultDialog.Show();
        }



        Thread _liveViewThread;
        bool _isLiveViewStop = false;

        private void btnLiveStart_Click(object sender, EventArgs e)
        {
            if (_Topcon_SR5000.SetySR5000LiveViewStart(true, 10000) == false)
            {
                WriteLog("Failed Live Start");
            } // else

            Thread.Sleep(500);

            _isLiveViewStop = false;

            _liveViewThread = new Thread(GetLiveViewImage);
            _liveViewThread.Start();

            WriteLog("Complete Live Start");
            
        }

        private void GetLiveViewImage()
        {
            while(true)
            {
                if (_isLiveViewStop == true)
                {
                    break;
                }

                bool result;
                result = _Topcon_SR5000.SetySR5000LiveViewImageRequest(true, 10000);
                                
                if (result == false)
                {
                    continue;
                }

                object getObject = _Topcon_SR5000.xSR5000LiveViewImage.Is;
                Type temp = getObject.GetType();

                if (temp.Equals(typeof(Bitmap)))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        lblFocus.Text = _Topcon_SR5000.xSR5000LiveViewFocusData.Is.ToString();
                        pictureBoxLive.Image = (Bitmap)getObject;
                    }));
                }

                Thread.Sleep(50);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void btnLiveStop_Click(object sender, EventArgs e)
        {
            _isLiveViewStop = true;

            _liveViewThread.Abort();

            if (_Topcon_SR5000.SetySR5000LiveViewStop(true, 10000) == false)
            {
                WriteLog("Failed Live Start");
            } // else
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            bool result = false;

            ProcessingBox prBox = new ProcessingBox("Capture...");

            Task task = Task.Run(() => result = _Topcon_SR5000.SetySR5000MeasurementStart(true, 120000));
            task.GetAwaiter().OnCompleted(() =>
            {
                prBox.Close();
            });

            prBox.ShowDialog();

            if (result == true)
            {
                WriteLog("start Measurement complete");
            }
            else
            {
                WriteLog("start Measurement failed");
            }
        }

        private void btnCaptureStop_Click(object sender, EventArgs e)
        {
            if (_Topcon_SR5000.SetySR5000MeasurementStop(true, 10000) == false)
            {
                WriteLog("Failed stop Measurement");
            } // else
        }

        private void btmClearImage_Click(object sender, EventArgs e)
        {
            pBMeasurePseudo.Image = pBMeasurePseudo.InitialImage;
        }
    }
}