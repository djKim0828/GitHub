namespace EmWorks.App.LicenseGenerator
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteReqCode = new System.Windows.Forms.Button();
            this.txtRequestCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportLicenseFile = new System.Windows.Forms.Button();
            this.btnCopyClipboard = new System.Windows.Forms.Button();
            this.txtActivationCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.txtTestCode = new System.Windows.Forms.TextBox();
            this.btnInputTestCode = new System.Windows.Forms.Button();
            this.btnCreateRequestCode = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 32);
            this.label4.TabIndex = 45;
            this.label4.Text = "Request Code";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteReqCode
            // 
            this.btnDeleteReqCode.Location = new System.Drawing.Point(500, 47);
            this.btnDeleteReqCode.Name = "btnDeleteReqCode";
            this.btnDeleteReqCode.Size = new System.Drawing.Size(78, 32);
            this.btnDeleteReqCode.TabIndex = 44;
            this.btnDeleteReqCode.Text = "Delete";
            this.btnDeleteReqCode.UseVisualStyleBackColor = true;
            // 
            // txtRequestCode
            // 
            this.txtRequestCode.Font = new System.Drawing.Font("Gulim", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRequestCode.Location = new System.Drawing.Point(107, 47);
            this.txtRequestCode.Name = "txtRequestCode";
            this.txtRequestCode.Size = new System.Drawing.Size(392, 32);
            this.txtRequestCode.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(566, 32);
            this.label1.TabIndex = 42;
            this.label1.Text = "Request";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExportLicenseFile
            // 
            this.btnExportLicenseFile.Location = new System.Drawing.Point(425, 226);
            this.btnExportLicenseFile.Name = "btnExportLicenseFile";
            this.btnExportLicenseFile.Size = new System.Drawing.Size(154, 32);
            this.btnExportLicenseFile.TabIndex = 48;
            this.btnExportLicenseFile.Text = "Export License File";
            this.btnExportLicenseFile.UseVisualStyleBackColor = true;
            this.btnExportLicenseFile.Visible = false;
            this.btnExportLicenseFile.Click += new System.EventHandler(this.btnExportLicenseFile_Click);
            // 
            // btnCopyClipboard
            // 
            this.btnCopyClipboard.Location = new System.Drawing.Point(173, 226);
            this.btnCopyClipboard.Name = "btnCopyClipboard";
            this.btnCopyClipboard.Size = new System.Drawing.Size(222, 32);
            this.btnCopyClipboard.TabIndex = 47;
            this.btnCopyClipboard.Text = "Copy Clipboard";
            this.btnCopyClipboard.UseVisualStyleBackColor = true;
            this.btnCopyClipboard.Click += new System.EventHandler(this.btnCopyClipboard_Click);
            // 
            // txtActivationCode
            // 
            this.txtActivationCode.Font = new System.Drawing.Font("Gulim", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtActivationCode.Location = new System.Drawing.Point(14, 188);
            this.txtActivationCode.Name = "txtActivationCode";
            this.txtActivationCode.ReadOnly = true;
            this.txtActivationCode.Size = new System.Drawing.Size(565, 32);
            this.txtActivationCode.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SteelBlue;
            this.label2.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(566, 32);
            this.label2.TabIndex = 49;
            this.label2.Text = "Activation Code";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Location = new System.Drawing.Point(173, 85);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(222, 32);
            this.btnGenerateCode.TabIndex = 50;
            this.btnGenerateCode.Text = "Generate";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // txtTestCode
            // 
            this.txtTestCode.Font = new System.Drawing.Font("Gulim", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTestCode.Location = new System.Drawing.Point(6, 20);
            this.txtTestCode.Name = "txtTestCode";
            this.txtTestCode.Size = new System.Drawing.Size(384, 32);
            this.txtTestCode.TabIndex = 51;
            // 
            // btnInputTestCode
            // 
            this.btnInputTestCode.Location = new System.Drawing.Point(126, 60);
            this.btnInputTestCode.Name = "btnInputTestCode";
            this.btnInputTestCode.Size = new System.Drawing.Size(118, 32);
            this.btnInputTestCode.TabIndex = 52;
            this.btnInputTestCode.Text = "Input";
            this.btnInputTestCode.UseVisualStyleBackColor = true;
            this.btnInputTestCode.Click += new System.EventHandler(this.btnInputTestCode_Click);
            // 
            // btnCreateRequestCode
            // 
            this.btnCreateRequestCode.Location = new System.Drawing.Point(6, 60);
            this.btnCreateRequestCode.Name = "btnCreateRequestCode";
            this.btnCreateRequestCode.Size = new System.Drawing.Size(118, 32);
            this.btnCreateRequestCode.TabIndex = 53;
            this.btnCreateRequestCode.Text = "Create";
            this.btnCreateRequestCode.UseVisualStyleBackColor = true;
            this.btnCreateRequestCode.Click += new System.EventHandler(this.btnCreateRequestCode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTestCode);
            this.groupBox1.Controls.Add(this.btnCreateRequestCode);
            this.groupBox1.Controls.Add(this.btnInputTestCode);
            this.groupBox1.Location = new System.Drawing.Point(612, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 100);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TestCode";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 267);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExportLicenseFile);
            this.Controls.Add(this.btnCopyClipboard);
            this.Controls.Add(this.txtActivationCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDeleteReqCode);
            this.Controls.Add(this.txtRequestCode);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDeleteReqCode;
        private System.Windows.Forms.TextBox txtRequestCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportLicenseFile;
        private System.Windows.Forms.Button btnCopyClipboard;
        private System.Windows.Forms.TextBox txtActivationCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.TextBox txtTestCode;
        private System.Windows.Forms.Button btnInputTestCode;
        private System.Windows.Forms.Button btnCreateRequestCode;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

