using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EmWorks.App.Sample.View.Window
{
    public partial class McrTestWindow : Form
    {
        #region Fields

        private bool _isOpen1 = false;
        private bool _isOpen2 = false;
        private Mcr _mcr;

        #endregion Fields

        #region Constructors

        public McrTestWindow()
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
            if (_mcr.sxDeviceMCR1.Is.ToString() == "1")
            {
                lblStatus1.BackColor = Color.Green;
                _isOpen1 = true;
            }
            else
            {
                lblStatus1.BackColor = Color.Crimson;
                _isOpen1 = false;
            }
        }

        private void btnCheckStatus2_Click(object sender, EventArgs e)
        {
            if (_mcr.sxDeviceMCR2.Is.ToString() == "1")
            {
                lblStatus2.BackColor = Color.Green;
                _isOpen2 = true;
            }
            else
            {
                lblStatus2.BackColor = Color.Crimson;
                _isOpen2 = false;
            }
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mcr.SetsyDeviceMCR1(false, 1000) == true)
                {
                    lblStatus1.BackColor = Color.Crimson;
                    _isOpen1 = false;
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

        private void btnClose2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mcr.SetsyDeviceMCR2(false, 1000) == true)
                {
                    lblStatus2.BackColor = Color.Crimson;
                    _isOpen2 = false;
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;

            _mcr = new Mcr(filePath); // 객체 생성시에 설정파일(Json)을 파라미터로 넣으면 자동으로 Load된다.

            dgvList.DataSource = new List<TagIdentity>(_mcr.GetAllData().Values);

            try
            {
                string[] Options = ((TagIdentity)_mcr.syDeviceMCR1).Options.Split('/');

                txtIp1.Text = Options[1];
                txtPort1.Text = Options[2];

                Options = ((TagIdentity)_mcr.syDeviceMCR2).Options.Split('/');

                txtIp2.Text = Options[1];
                txtPort2.Text = Options[2];
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }

            gridBox.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mcr.SetsyDeviceMCR1(true, 1000) == true)
                {
                    lblStatus1.BackColor = Color.Green;
                    _isOpen1 = true;
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

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            try
            {
                int On = 1;
                _mcr.syDeviceMCR2 = On;

                Logger.Infomation("main 1");

                Thread.Sleep(3000);

                Logger.Infomation("main 2");

                if (_mcr.sxDeviceMCR2.Is.ToString() == On.ToString())
                {
                    lblStatus2.BackColor = Color.Green;
                    _isOpen2 = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정로드 후 재시도 하십시오. " + ex.ToString());
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
            _mcr.Save();
        }

        private void btnTrigger1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isOpen1 == false) return;

                lblResult1.Text = string.Empty;

                string data = string.Empty;
                if (_mcr.SetyMCR1TriggerOn(true, out data) == true)
                {
                    lblResult1.Text = data;
                }
                else
                {
                    lblResult1.Text = "Failed";
                }
            }
            catch
            {
                MessageBox.Show("설정로드 후 재시도 하십시오.");
            }
        }

        private void btnTrigger2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isOpen2 == false) return;

                lblResult2.Text = string.Empty;

                string data = string.Empty;
                if (_mcr.SetyMCR2TriggerOn(true, out data) == true)
                {
                    lblResult2.Text = data;
                }
                else
                {
                    lblResult2.Text = "Failed";
                }
            }
            catch
            {
                MessageBox.Show("설정로드 후 재시도 하십시오.");
            }
        }

        private void McrTestWindow_Load(object sender, EventArgs e)
        {
            txtPath.Text = HomeDir() + @"\Config\Tag\Mcr.json";
            btnLoad_Click(null, null);
        }

        #endregion Methods

        private bool isTest = false;
        private Thread threadTest;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isTest = checkBox1.Checked;

            if (isTest == true)
            {
                checkBox1.BackColor = Color.Green;
                threadTest = new Thread(RunTest);
                threadTest.Start();
            }
            else
            {
                checkBox1.BackColor = Color.Red;
            }
        }

        private void RunTest()
        {
            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    int totalcount = Convert.ToInt32(txtTestCount.Text);
                    int count = totalcount;

                    for (int i = 0; i < totalcount; i++)
                    {
                        btnTrigger1_Click(null, null);
                        btnTrigger2_Click(null, null);

                        count--;

                        txtTestCount.Text = count.ToString();
                        Application.DoEvents();

                        Thread.Sleep(500);

                        if (isTest == false)
                        {
                            return;
                        }
                    }

                    checkBox1.Checked = false;
                    //checkBox1.BackColor = Color.Red;
                    txtTestCount.Text = totalcount.ToString();
                }));
            }
            catch
            {
            }
        }
    }
}