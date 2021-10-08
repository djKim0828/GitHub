namespace EmWorks.App.Sample.View.Window
{
    partial class TopconSR500Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopconSR500Window));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkSim = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCaptureStop = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.lblFocus = new System.Windows.Forms.Label();
            this.btnLiveStop = new System.Windows.Forms.Button();
            this.btnLiveStart = new System.Windows.Forms.Button();
            this.pictureBoxLive = new System.Windows.Forms.PictureBox();
            this.btmClearImage = new System.Windows.Forms.Button();
            this.btnSearchROI = new System.Windows.Forms.Button();
            this.btnGetRGB = new System.Windows.Forms.Button();
            this.btnGetPseudo = new System.Windows.Forms.Button();
            this.pBMeasurePseudo = new System.Windows.Forms.PictureBox();
            this.btnLodaMes = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnLoadRecipe = new System.Windows.Forms.Button();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.lblConnect1 = new System.Windows.Forms.Label();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnCheckStatus1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.tabPage1.SuspendLayout();
            this.gridBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasurePseudo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.chkSim);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnOpenFile);
            this.tabPage1.Controls.Add(this.btnLoad);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Controls.Add(this.gridBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1208, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1090, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 32);
            this.btnSearch.TabIndex = 104;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(912, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(170, 25);
            this.txtSearch.TabIndex = 103;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // chkSim
            // 
            this.chkSim.AutoSize = true;
            this.chkSim.Location = new System.Drawing.Point(765, 24);
            this.chkSim.Name = "chkSim";
            this.chkSim.Size = new System.Drawing.Size(96, 19);
            this.chkSim.TabIndex = 102;
            this.chkSim.Text = "Simulation";
            this.chkSim.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(653, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 26);
            this.btnSave.TabIndex = 100;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(453, 20);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(71, 25);
            this.btnOpenFile.TabIndex = 101;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(546, 21);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(101, 25);
            this.btnLoad.TabIndex = 99;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(6, 21);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(441, 25);
            this.txtPath.TabIndex = 81;
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.dgvList);
            this.gridBox.Enabled = false;
            this.gridBox.Location = new System.Drawing.Point(9, 54);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(1183, 507);
            this.gridBox.TabIndex = 78;
            this.gridBox.TabStop = false;
            // 
            // dgvList
            // 
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(6, 20);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 27;
            this.dgvList.Size = new System.Drawing.Size(1171, 469);
            this.dgvList.TabIndex = 37;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCaptureStop);
            this.tabPage2.Controls.Add(this.btnCapture);
            this.tabPage2.Controls.Add(this.lblFocus);
            this.tabPage2.Controls.Add(this.btnLiveStop);
            this.tabPage2.Controls.Add(this.btnLiveStart);
            this.tabPage2.Controls.Add(this.pictureBoxLive);
            this.tabPage2.Controls.Add(this.btmClearImage);
            this.tabPage2.Controls.Add(this.btnSearchROI);
            this.tabPage2.Controls.Add(this.btnGetRGB);
            this.tabPage2.Controls.Add(this.btnGetPseudo);
            this.tabPage2.Controls.Add(this.pBMeasurePseudo);
            this.tabPage2.Controls.Add(this.btnLodaMes);
            this.tabPage2.Controls.Add(this.btnConnect);
            this.tabPage2.Controls.Add(this.btnLoadRecipe);
            this.tabPage2.Controls.Add(this.dgvOutput);
            this.tabPage2.Controls.Add(this.dgvInput);
            this.tabPage2.Controls.Add(this.lblConnect1);
            this.tabPage2.Controls.Add(this.btnClose1);
            this.tabPage2.Controls.Add(this.btnCheckStatus1);
            this.tabPage2.Controls.Add(this.lblStatus1);
            this.tabPage2.Controls.Add(this.btnOpen);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1208, 570);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Command";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCaptureStop
            // 
            this.btnCaptureStop.Enabled = false;
            this.btnCaptureStop.Location = new System.Drawing.Point(13, 487);
            this.btnCaptureStop.Name = "btnCaptureStop";
            this.btnCaptureStop.Size = new System.Drawing.Size(121, 37);
            this.btnCaptureStop.TabIndex = 128;
            this.btnCaptureStop.Text = "Stop Capture";
            this.btnCaptureStop.UseVisualStyleBackColor = true;
            this.btnCaptureStop.Click += new System.EventHandler(this.btnCaptureStop_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Enabled = false;
            this.btnCapture.Location = new System.Drawing.Point(13, 425);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(121, 56);
            this.btnCapture.TabIndex = 127;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // lblFocus
            // 
            this.lblFocus.BackColor = System.Drawing.Color.DimGray;
            this.lblFocus.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFocus.ForeColor = System.Drawing.Color.Lime;
            this.lblFocus.Location = new System.Drawing.Point(931, 516);
            this.lblFocus.Name = "lblFocus";
            this.lblFocus.Size = new System.Drawing.Size(121, 37);
            this.lblFocus.TabIndex = 126;
            this.lblFocus.Text = "--";
            this.lblFocus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLiveStop
            // 
            this.btnLiveStop.Enabled = false;
            this.btnLiveStop.Location = new System.Drawing.Point(931, 444);
            this.btnLiveStop.Name = "btnLiveStop";
            this.btnLiveStop.Size = new System.Drawing.Size(121, 56);
            this.btnLiveStop.TabIndex = 125;
            this.btnLiveStop.Text = "LiveView Stop";
            this.btnLiveStop.UseVisualStyleBackColor = true;
            this.btnLiveStop.Click += new System.EventHandler(this.btnLiveStop_Click);
            // 
            // btnLiveStart
            // 
            this.btnLiveStart.Enabled = false;
            this.btnLiveStart.Location = new System.Drawing.Point(931, 382);
            this.btnLiveStart.Name = "btnLiveStart";
            this.btnLiveStart.Size = new System.Drawing.Size(121, 56);
            this.btnLiveStart.TabIndex = 124;
            this.btnLiveStart.Text = "LiveView Start";
            this.btnLiveStart.UseVisualStyleBackColor = true;
            this.btnLiveStart.Click += new System.EventHandler(this.btnLiveStart_Click);
            // 
            // pictureBoxLive
            // 
            this.pictureBoxLive.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxLive.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLive.Image")));
            this.pictureBoxLive.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxLive.InitialImage")));
            this.pictureBoxLive.Location = new System.Drawing.Point(673, 382);
            this.pictureBoxLive.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLive.Name = "pictureBoxLive";
            this.pictureBoxLive.Size = new System.Drawing.Size(251, 171);
            this.pictureBoxLive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLive.TabIndex = 123;
            this.pictureBoxLive.TabStop = false;
            // 
            // btmClearImage
            // 
            this.btmClearImage.Location = new System.Drawing.Point(513, 444);
            this.btmClearImage.Name = "btmClearImage";
            this.btmClearImage.Size = new System.Drawing.Size(121, 37);
            this.btmClearImage.TabIndex = 122;
            this.btmClearImage.Text = "Clear";
            this.btmClearImage.UseVisualStyleBackColor = true;
            this.btmClearImage.Click += new System.EventHandler(this.btmClearImage_Click);
            // 
            // btnSearchROI
            // 
            this.btnSearchROI.Location = new System.Drawing.Point(513, 382);
            this.btnSearchROI.Name = "btnSearchROI";
            this.btnSearchROI.Size = new System.Drawing.Size(121, 56);
            this.btnSearchROI.TabIndex = 121;
            this.btnSearchROI.Text = "Search ROI";
            this.btnSearchROI.UseVisualStyleBackColor = true;
            this.btnSearchROI.Click += new System.EventHandler(this.btnSearchROI_Click);
            // 
            // btnGetRGB
            // 
            this.btnGetRGB.Location = new System.Drawing.Point(140, 468);
            this.btnGetRGB.Name = "btnGetRGB";
            this.btnGetRGB.Size = new System.Drawing.Size(121, 32);
            this.btnGetRGB.TabIndex = 120;
            this.btnGetRGB.Text = "Get RGB";
            this.btnGetRGB.UseVisualStyleBackColor = true;
            this.btnGetRGB.Click += new System.EventHandler(this.btnGetRGB_Click);
            // 
            // btnGetPseudo
            // 
            this.btnGetPseudo.Location = new System.Drawing.Point(140, 425);
            this.btnGetPseudo.Name = "btnGetPseudo";
            this.btnGetPseudo.Size = new System.Drawing.Size(121, 37);
            this.btnGetPseudo.TabIndex = 119;
            this.btnGetPseudo.Text = "Get Pseudo";
            this.btnGetPseudo.UseVisualStyleBackColor = true;
            this.btnGetPseudo.Click += new System.EventHandler(this.btnGetPseudo_Click);
            // 
            // pBMeasurePseudo
            // 
            this.pBMeasurePseudo.BackColor = System.Drawing.Color.Black;
            this.pBMeasurePseudo.Image = ((System.Drawing.Image)(resources.GetObject("pBMeasurePseudo.Image")));
            this.pBMeasurePseudo.InitialImage = ((System.Drawing.Image)(resources.GetObject("pBMeasurePseudo.InitialImage")));
            this.pBMeasurePseudo.Location = new System.Drawing.Point(268, 382);
            this.pBMeasurePseudo.Margin = new System.Windows.Forms.Padding(4);
            this.pBMeasurePseudo.Name = "pBMeasurePseudo";
            this.pBMeasurePseudo.Size = new System.Drawing.Size(238, 171);
            this.pBMeasurePseudo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMeasurePseudo.TabIndex = 118;
            this.pBMeasurePseudo.TabStop = false;
            // 
            // btnLodaMes
            // 
            this.btnLodaMes.Location = new System.Drawing.Point(140, 382);
            this.btnLodaMes.Name = "btnLodaMes";
            this.btnLodaMes.Size = new System.Drawing.Size(121, 37);
            this.btnLodaMes.TabIndex = 117;
            this.btnLodaMes.Text = "Load Mes.";
            this.btnLodaMes.UseVisualStyleBackColor = true;
            this.btnLodaMes.Click += new System.EventHandler(this.btnLodaMes_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(622, 15);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(121, 35);
            this.btnConnect.TabIndex = 116;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnLoadRecipe
            // 
            this.btnLoadRecipe.Location = new System.Drawing.Point(13, 382);
            this.btnLoadRecipe.Name = "btnLoadRecipe";
            this.btnLoadRecipe.Size = new System.Drawing.Size(121, 37);
            this.btnLoadRecipe.TabIndex = 115;
            this.btnLoadRecipe.Text = "Load Recipe";
            this.btnLoadRecipe.UseVisualStyleBackColor = true;
            this.btnLoadRecipe.Click += new System.EventHandler(this.btnLoadRecipe_Click);
            // 
            // dgvOutput
            // 
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Location = new System.Drawing.Point(613, 59);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.RowTemplate.Height = 27;
            this.dgvOutput.Size = new System.Drawing.Size(589, 308);
            this.dgvOutput.TabIndex = 106;
            // 
            // dgvInput
            // 
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Location = new System.Drawing.Point(13, 59);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowTemplate.Height = 27;
            this.dgvInput.Size = new System.Drawing.Size(589, 308);
            this.dgvInput.TabIndex = 105;
            // 
            // lblConnect1
            // 
            this.lblConnect1.BackColor = System.Drawing.Color.Crimson;
            this.lblConnect1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnect1.Location = new System.Drawing.Point(213, 15);
            this.lblConnect1.Name = "lblConnect1";
            this.lblConnect1.Size = new System.Drawing.Size(188, 35);
            this.lblConnect1.TabIndex = 104;
            this.lblConnect1.Text = "Connect";
            this.lblConnect1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose1
            // 
            this.btnClose1.Location = new System.Drawing.Point(800, 18);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(82, 35);
            this.btnClose1.TabIndex = 101;
            this.btnClose1.Text = "Close";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
            // 
            // btnCheckStatus1
            // 
            this.btnCheckStatus1.Location = new System.Drawing.Point(407, 15);
            this.btnCheckStatus1.Name = "btnCheckStatus1";
            this.btnCheckStatus1.Size = new System.Drawing.Size(82, 35);
            this.btnCheckStatus1.TabIndex = 100;
            this.btnCheckStatus1.Text = "Refresh";
            this.btnCheckStatus1.UseVisualStyleBackColor = true;
            this.btnCheckStatus1.Click += new System.EventHandler(this.btnCheckStatus1_Click);
            // 
            // lblStatus1
            // 
            this.lblStatus1.BackColor = System.Drawing.Color.Crimson;
            this.lblStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus1.Location = new System.Drawing.Point(19, 15);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(188, 35);
            this.lblStatus1.TabIndex = 99;
            this.lblStatus1.Text = "Status";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(495, 15);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(121, 35);
            this.btnOpen.TabIndex = 98;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1216, 599);
            this.tabControl1.TabIndex = 107;
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(12, 617);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(1212, 151);
            this.rcbOutput.TabIndex = 108;
            this.rcbOutput.Text = "";
            // 
            // TopconSR500Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 776);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rcbOutput);
            this.Name = "TopconSR500Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TopconSR500Window";
            this.Load += new System.EventHandler(this.TopconSR500Window_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gridBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasurePseudo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkSim;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnLoadRecipe;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.Label lblConnect1;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnCheckStatus1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnLodaMes;
        private System.Windows.Forms.Button btnGetPseudo;
        private System.Windows.Forms.PictureBox pBMeasurePseudo;
        private System.Windows.Forms.Button btnGetRGB;
        private System.Windows.Forms.Button btnSearchROI;
        private System.Windows.Forms.Button btmClearImage;
        private System.Windows.Forms.Label lblFocus;
        private System.Windows.Forms.Button btnLiveStop;
        private System.Windows.Forms.Button btnLiveStart;
        private System.Windows.Forms.PictureBox pictureBoxLive;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnCaptureStop;
    }
}