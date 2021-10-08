namespace EmWorks.App.Sample.View.Window
{
    partial class NachiRobotTestWindow
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.chkSim = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCondition1 = new System.Windows.Forms.Button();
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.lblConnect1 = new System.Windows.Forms.Label();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.btnCheckStatus1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCondition2 = new System.Windows.Forms.Button();
            this.btnAlarmReset = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnCondition3 = new System.Windows.Forms.Button();
            this.btnCondition4 = new System.Windows.Forms.Button();
            this.btnCondition5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.gridBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
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
            // chkSim
            // 
            this.chkSim.AutoSize = true;
            this.chkSim.Location = new System.Drawing.Point(765, 24);
            this.chkSim.Name = "chkSim";
            this.chkSim.Size = new System.Drawing.Size(83, 16);
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
            // btnCondition1
            // 
            this.btnCondition1.Location = new System.Drawing.Point(184, 459);
            this.btnCondition1.Name = "btnCondition1";
            this.btnCondition1.Size = new System.Drawing.Size(121, 56);
            this.btnCondition1.TabIndex = 115;
            this.btnCondition1.Text = "Condition 1";
            this.btnCondition1.UseVisualStyleBackColor = true;
            this.btnCondition1.Click += new System.EventHandler(this.BtnCondition1_Click);
            // 
            // chkPause
            // 
            this.chkPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPause.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chkPause.Location = new System.Drawing.Point(166, 373);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(121, 35);
            this.chkPause.TabIndex = 110;
            this.chkPause.Text = "Pause";
            this.chkPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPause.UseVisualStyleBackColor = false;
            this.chkPause.CheckedChanged += new System.EventHandler(this.chkPause_CheckedChanged);
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
            this.btnClose1.Location = new System.Drawing.Point(622, 15);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(82, 35);
            this.btnClose1.TabIndex = 101;
            this.btnClose1.Text = "Close";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
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
            this.txtPath.Size = new System.Drawing.Size(441, 21);
            this.txtPath.TabIndex = 81;
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
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(12, 617);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(1212, 151);
            this.rcbOutput.TabIndex = 106;
            this.rcbOutput.Text = "";
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
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(13, 373);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(121, 56);
            this.btnInit.TabIndex = 102;
            this.btnInit.Text = "Initialize";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1208, 573);
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
            this.txtSearch.Size = new System.Drawing.Size(170, 21);
            this.txtSearch.TabIndex = 103;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCondition5);
            this.tabPage2.Controls.Add(this.btnCondition4);
            this.tabPage2.Controls.Add(this.btnCondition3);
            this.tabPage2.Controls.Add(this.btnCondition2);
            this.tabPage2.Controls.Add(this.btnAlarmReset);
            this.tabPage2.Controls.Add(this.btnCondition1);
            this.tabPage2.Controls.Add(this.chkPause);
            this.tabPage2.Controls.Add(this.dgvOutput);
            this.tabPage2.Controls.Add(this.dgvInput);
            this.tabPage2.Controls.Add(this.lblConnect1);
            this.tabPage2.Controls.Add(this.btnClose1);
            this.tabPage2.Controls.Add(this.btnCheckStatus1);
            this.tabPage2.Controls.Add(this.lblStatus1);
            this.tabPage2.Controls.Add(this.btnOpen);
            this.tabPage2.Controls.Add(this.btnInit);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1208, 573);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Command";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCondition2
            // 
            this.btnCondition2.Location = new System.Drawing.Point(311, 459);
            this.btnCondition2.Name = "btnCondition2";
            this.btnCondition2.Size = new System.Drawing.Size(121, 56);
            this.btnCondition2.TabIndex = 117;
            this.btnCondition2.Text = "Condition 2";
            this.btnCondition2.UseVisualStyleBackColor = true;
            this.btnCondition2.Click += new System.EventHandler(this.btnCondition2_Click);
            // 
            // btnAlarmReset
            // 
            this.btnAlarmReset.Location = new System.Drawing.Point(293, 373);
            this.btnAlarmReset.Name = "btnAlarmReset";
            this.btnAlarmReset.Size = new System.Drawing.Size(121, 56);
            this.btnAlarmReset.TabIndex = 116;
            this.btnAlarmReset.Text = "Reset Alarm";
            this.btnAlarmReset.UseVisualStyleBackColor = true;
            this.btnAlarmReset.Click += new System.EventHandler(this.btnAlarmReset_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1216, 599);
            this.tabControl1.TabIndex = 105;
            // 
            // btnCondition3
            // 
            this.btnCondition3.Location = new System.Drawing.Point(438, 459);
            this.btnCondition3.Name = "btnCondition3";
            this.btnCondition3.Size = new System.Drawing.Size(121, 56);
            this.btnCondition3.TabIndex = 118;
            this.btnCondition3.Text = "Condition 3";
            this.btnCondition3.UseVisualStyleBackColor = true;
            this.btnCondition3.Click += new System.EventHandler(this.btnCondition3_Click);
            // 
            // btnCondition4
            // 
            this.btnCondition4.Location = new System.Drawing.Point(565, 459);
            this.btnCondition4.Name = "btnCondition4";
            this.btnCondition4.Size = new System.Drawing.Size(121, 56);
            this.btnCondition4.TabIndex = 119;
            this.btnCondition4.Text = "Condition 4";
            this.btnCondition4.UseVisualStyleBackColor = true;
            // 
            // btnCondition5
            // 
            this.btnCondition5.Location = new System.Drawing.Point(692, 459);
            this.btnCondition5.Name = "btnCondition5";
            this.btnCondition5.Size = new System.Drawing.Size(121, 56);
            this.btnCondition5.TabIndex = 120;
            this.btnCondition5.Text = "Condition 5";
            this.btnCondition5.UseVisualStyleBackColor = true;
            // 
            // NachiRobotTestWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1236, 782);
            this.Controls.Add(this.rcbOutput);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NachiRobotTestWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NachiRobotTestWindow";
            this.Load += new System.EventHandler(this.NachiRobotTestWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gridBox.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.CheckBox chkSim;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCondition1;
        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.Label lblConnect1;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Button btnCheckStatus1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnAlarmReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnCondition2;
        private System.Windows.Forms.Button btnCondition5;
        private System.Windows.Forms.Button btnCondition4;
        private System.Windows.Forms.Button btnCondition3;
    }
}