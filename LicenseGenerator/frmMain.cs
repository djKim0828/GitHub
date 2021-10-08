using System;
using System.IO;
using System.Windows.Forms;

namespace EmWorks.App.LicenseGenerator
{
    public partial class frmMain : Form
    {
        #region Constructors

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtActivationCode.Text);
        }

        private void btnCreateRequestCode_Click(object sender, EventArgs e)
        {
            txtTestCode.Text = Util.CreateRequestCode(Global.RequestCode);
        }

        private void btnExportLicenseFile_Click(object sender, EventArgs e)
        {
            string savePath = Environment.CurrentDirectory;
            SaveFileDialog dlgSavefile = new SaveFileDialog();
            dlgSavefile.InitialDirectory = savePath;
            dlgSavefile.Title = "Save License File";
            dlgSavefile.Filter = "License Files (*.lic)|*.lic";
            dlgSavefile.DefaultExt = "lic";
            dlgSavefile.AddExtension = false;

            if (dlgSavefile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileStream fileStream = new FileStream(dlgSavefile.FileName, FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine("[LICENSE]\nACTIVATION_CODE=" + txtActivationCode.Text);
            streamWriter.Close();

            MessageBox.Show("Success");
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                Generate();

                btnCopyClipboard.Enabled = true;
                btnExportLicenseFile.Enabled = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Invalid Request Code.");
                btnGenerateCode.Enabled = true;
            }
        }

        private void btnInputTestCode_Click(object sender, EventArgs e)
        {
            txtRequestCode.Text = txtTestCode.Text;
        }

        /// <summary>
        /// Create Active Code
        /// </summary>
        /// <param name="RequestCode">request code</param>
        /// <param name="ACTIVE_PASS">Active Regist name</param>
        /// <returns>Active Code</returns>
        private string CreateActiveCode(string RequestCode, string ACTIVE_PASS)
        {
            string strActiveCode = null;

            string strHddidTicks = null;

            strHddidTicks = DecryptRequestCode(RequestCode);
            strActiveCode = Util.EncryptString(strHddidTicks, ACTIVE_PASS);

            return strActiveCode;
        }

        /// <summary>
        /// Decrypt Request Code
        /// </summary>
        /// <param name="RequestCode">Encrypt Request Code</param>
        /// <returns>Request Code</returns>
        private string DecryptRequestCode(string RequestCode)
        {
            string strDecryptResult = Util.DecryptString(RequestCode, Global.RequestCode);
            return strDecryptResult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnCopyClipboard.Enabled = false;
            btnExportLicenseFile.Enabled = false;


            frmLogin lg = new frmLogin();

            if (lg.ShowDialog() != DialogResult.OK)
            {
                this.Close();
            } // else
        }

        private void Generate()
        {
            if (txtRequestCode.Text.Length == 0)
            {
                MessageBox.Show("Insert Request code.");
                return;
            }

            btnGenerateCode.Enabled = false;

            txtActivationCode.Text = "";
            txtActivationCode.Refresh();
            System.Threading.Thread.Sleep(100);

            string strActiveCode;
            string strRequestCode;

            strRequestCode = txtRequestCode.Text;

            strActiveCode = CreateActiveCode(strRequestCode, Global.ActivateCode);

            if (strActiveCode == null)
            {
                MessageBox.Show("This code is invalid.");
                return;
            }

            txtActivationCode.Text = strActiveCode;
            btnGenerateCode.Enabled = true;
        }

        #endregion Methods
    }
}