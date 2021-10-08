namespace EmWorks.App.Sample.View.Window
{
    partial class InRobotTestWindow
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
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
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExitP1 = new System.Windows.Forms.Button();
            this.btnComP1 = new System.Windows.Forms.Button();
            this.btnGetP1 = new System.Windows.Forms.Button();
            this.btnConP1 = new System.Windows.Forms.Button();
            this.btnCondition3 = new System.Windows.Forms.Button();
            this.btnCondition2 = new System.Windows.Forms.Button();
            this.BtnCondition1 = new System.Windows.Forms.Button();
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.lblConnect1 = new System.Windows.Forms.Label();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnCheckStatus1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExitP3 = new System.Windows.Forms.Button();
            this.btnComP3 = new System.Windows.Forms.Button();
            this.btnPutP3 = new System.Windows.Forms.Button();
            this.btnConP3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gridBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(10, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1064, 630);
            this.tabControl1.TabIndex = 99;
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
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1056, 645);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(928, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 26);
            this.btnSearch.TabIndex = 106;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(773, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(149, 21);
            this.txtSearch.TabIndex = 105;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // chkSim
            // 
            this.chkSim.AutoSize = true;
            this.chkSim.Location = new System.Drawing.Point(669, 19);
            this.chkSim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSim.Name = "chkSim";
            this.chkSim.Size = new System.Drawing.Size(83, 16);
            this.chkSim.TabIndex = 102;
            this.chkSim.Text = "Simulation";
            this.chkSim.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(571, 18);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 21);
            this.btnSave.TabIndex = 100;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(396, 16);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(62, 20);
            this.btnOpenFile.TabIndex = 101;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(478, 17);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 20);
            this.btnLoad.TabIndex = 99;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(5, 17);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(386, 21);
            this.txtPath.TabIndex = 81;
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.dgvList);
            this.gridBox.Enabled = false;
            this.gridBox.Location = new System.Drawing.Point(8, 43);
            this.gridBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridBox.Name = "gridBox";
            this.gridBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridBox.Size = new System.Drawing.Size(1035, 406);
            this.gridBox.TabIndex = 78;
            this.gridBox.TabStop = false;
            // 
            // dgvList
            // 
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(5, 16);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 27;
            this.dgvList.Size = new System.Drawing.Size(1025, 375);
            this.dgvList.TabIndex = 37;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.btnStop);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnCondition3);
            this.tabPage2.Controls.Add(this.btnCondition2);
            this.tabPage2.Controls.Add(this.BtnCondition1);
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
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1056, 604);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Command";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(122, 546);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(106, 45);
            this.btnStop.TabIndex = 119;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExitP1);
            this.groupBox1.Controls.Add(this.btnComP1);
            this.groupBox1.Controls.Add(this.btnGetP1);
            this.groupBox1.Controls.Add(this.btnConP1);
            this.groupBox1.Location = new System.Drawing.Point(536, 498);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(452, 54);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get Cell";
            // 
            // btnExitP1
            // 
            this.btnExitP1.Location = new System.Drawing.Point(339, 18);
            this.btnExitP1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExitP1.Name = "btnExitP1";
            this.btnExitP1.Size = new System.Drawing.Size(106, 28);
            this.btnExitP1.TabIndex = 122;
            this.btnExitP1.Text = "Exit P1";
            this.btnExitP1.UseVisualStyleBackColor = true;
            this.btnExitP1.Click += new System.EventHandler(this.btnExitP1_Click);
            // 
            // btnComP1
            // 
            this.btnComP1.Location = new System.Drawing.Point(228, 18);
            this.btnComP1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnComP1.Name = "btnComP1";
            this.btnComP1.Size = new System.Drawing.Size(106, 28);
            this.btnComP1.TabIndex = 121;
            this.btnComP1.Text = "Complete P1";
            this.btnComP1.UseVisualStyleBackColor = true;
            this.btnComP1.Click += new System.EventHandler(this.btnComP1_Click);
            // 
            // btnGetP1
            // 
            this.btnGetP1.Location = new System.Drawing.Point(116, 18);
            this.btnGetP1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetP1.Name = "btnGetP1";
            this.btnGetP1.Size = new System.Drawing.Size(106, 28);
            this.btnGetP1.TabIndex = 120;
            this.btnGetP1.Text = "Get P1";
            this.btnGetP1.UseVisualStyleBackColor = true;
            this.btnGetP1.Click += new System.EventHandler(this.btnGetP1_Click);
            // 
            // btnConP1
            // 
            this.btnConP1.Location = new System.Drawing.Point(5, 18);
            this.btnConP1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConP1.Name = "btnConP1";
            this.btnConP1.Size = new System.Drawing.Size(106, 28);
            this.btnConP1.TabIndex = 119;
            this.btnConP1.Text = "Condition1";
            this.btnConP1.UseVisualStyleBackColor = true;
            this.btnConP1.Click += new System.EventHandler(this.btnConP1_Click);
            // 
            // btnCondition3
            // 
            this.btnCondition3.Location = new System.Drawing.Point(433, 498);
            this.btnCondition3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCondition3.Name = "btnCondition3";
            this.btnCondition3.Size = new System.Drawing.Size(88, 45);
            this.btnCondition3.TabIndex = 117;
            this.btnCondition3.Text = "Condition3";
            this.btnCondition3.UseVisualStyleBackColor = true;
            this.btnCondition3.Click += new System.EventHandler(this.btnCondition3_Click);
            // 
            // btnCondition2
            // 
            this.btnCondition2.Location = new System.Drawing.Point(339, 498);
            this.btnCondition2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCondition2.Name = "btnCondition2";
            this.btnCondition2.Size = new System.Drawing.Size(88, 45);
            this.btnCondition2.TabIndex = 116;
            this.btnCondition2.Text = "Condition2";
            this.btnCondition2.UseVisualStyleBackColor = true;
            this.btnCondition2.Click += new System.EventHandler(this.btnCondition2_Click);
            // 
            // BtnCondition1
            // 
            this.BtnCondition1.Location = new System.Drawing.Point(245, 498);
            this.BtnCondition1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCondition1.Name = "BtnCondition1";
            this.BtnCondition1.Size = new System.Drawing.Size(88, 45);
            this.BtnCondition1.TabIndex = 115;
            this.BtnCondition1.Text = "Condition1";
            this.BtnCondition1.UseVisualStyleBackColor = true;
            this.BtnCondition1.Click += new System.EventHandler(this.BtnCondition1_Click);
            // 
            // chkPause
            // 
            this.chkPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPause.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chkPause.Location = new System.Drawing.Point(122, 498);
            this.chkPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(106, 45);
            this.chkPause.TabIndex = 110;
            this.chkPause.Text = "Pause";
            this.chkPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPause.UseVisualStyleBackColor = false;
            this.chkPause.CheckedChanged += new System.EventHandler(this.chkPause_CheckedChanged);
            // 
            // dgvOutput
            // 
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Location = new System.Drawing.Point(536, 47);
            this.dgvOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.RowTemplate.Height = 27;
            this.dgvOutput.Size = new System.Drawing.Size(515, 447);
            this.dgvOutput.TabIndex = 106;
            // 
            // dgvInput
            // 
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Location = new System.Drawing.Point(11, 47);
            this.dgvInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowTemplate.Height = 27;
            this.dgvInput.Size = new System.Drawing.Size(515, 447);
            this.dgvInput.TabIndex = 105;
            // 
            // lblConnect1
            // 
            this.lblConnect1.BackColor = System.Drawing.Color.Crimson;
            this.lblConnect1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnect1.Location = new System.Drawing.Point(186, 12);
            this.lblConnect1.Name = "lblConnect1";
            this.lblConnect1.Size = new System.Drawing.Size(165, 28);
            this.lblConnect1.TabIndex = 104;
            this.lblConnect1.Text = "Connect";
            this.lblConnect1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose1
            // 
            this.btnClose1.Location = new System.Drawing.Point(544, 12);
            this.btnClose1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(72, 28);
            this.btnClose1.TabIndex = 101;
            this.btnClose1.Text = "Close";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
            // 
            // btnCheckStatus1
            // 
            this.btnCheckStatus1.Location = new System.Drawing.Point(356, 12);
            this.btnCheckStatus1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckStatus1.Name = "btnCheckStatus1";
            this.btnCheckStatus1.Size = new System.Drawing.Size(72, 28);
            this.btnCheckStatus1.TabIndex = 100;
            this.btnCheckStatus1.Text = "Refresh";
            this.btnCheckStatus1.UseVisualStyleBackColor = true;
            this.btnCheckStatus1.Click += new System.EventHandler(this.btnCheckStatus1_Click);
            // 
            // lblStatus1
            // 
            this.lblStatus1.BackColor = System.Drawing.Color.Crimson;
            this.lblStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus1.Location = new System.Drawing.Point(17, 12);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(165, 28);
            this.lblStatus1.TabIndex = 99;
            this.lblStatus1.Text = "Status";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(433, 12);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(106, 28);
            this.btnOpen.TabIndex = 98;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(11, 498);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(106, 45);
            this.btnInit.TabIndex = 102;
            this.btnInit.Text = "Initialize";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(10, 644);
            this.rcbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(1060, 150);
            this.rcbOutput.TabIndex = 104;
            this.rcbOutput.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExitP3);
            this.groupBox2.Controls.Add(this.btnComP3);
            this.groupBox2.Controls.Add(this.btnPutP3);
            this.groupBox2.Controls.Add(this.btnConP3);
            this.groupBox2.Location = new System.Drawing.Point(536, 554);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(452, 54);
            this.groupBox2.TabIndex = 123;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Put Cell";
            // 
            // btnExitP3
            // 
            this.btnExitP3.Location = new System.Drawing.Point(339, 18);
            this.btnExitP3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExitP3.Name = "btnExitP3";
            this.btnExitP3.Size = new System.Drawing.Size(106, 28);
            this.btnExitP3.TabIndex = 122;
            this.btnExitP3.Text = "Exit P3";
            this.btnExitP3.UseVisualStyleBackColor = true;
            this.btnExitP3.Click += new System.EventHandler(this.btnExitP3_Click);
            // 
            // btnComP3
            // 
            this.btnComP3.Location = new System.Drawing.Point(228, 18);
            this.btnComP3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnComP3.Name = "btnComP3";
            this.btnComP3.Size = new System.Drawing.Size(106, 28);
            this.btnComP3.TabIndex = 121;
            this.btnComP3.Text = "Complete P3";
            this.btnComP3.UseVisualStyleBackColor = true;
            this.btnComP3.Click += new System.EventHandler(this.btnComP3_Click);
            // 
            // btnPutP3
            // 
            this.btnPutP3.Location = new System.Drawing.Point(116, 18);
            this.btnPutP3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPutP3.Name = "btnPutP3";
            this.btnPutP3.Size = new System.Drawing.Size(106, 28);
            this.btnPutP3.TabIndex = 120;
            this.btnPutP3.Text = "Put P3";
            this.btnPutP3.UseVisualStyleBackColor = true;
            this.btnPutP3.Click += new System.EventHandler(this.btnPutP3_Click);
            // 
            // btnConP3
            // 
            this.btnConP3.Location = new System.Drawing.Point(5, 18);
            this.btnConP3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConP3.Name = "btnConP3";
            this.btnConP3.Size = new System.Drawing.Size(106, 28);
            this.btnConP3.TabIndex = 119;
            this.btnConP3.Text = "Condition3";
            this.btnConP3.UseVisualStyleBackColor = true;
            this.btnConP3.Click += new System.EventHandler(this.btnConP3_Click);
            // 
            // InRobotTestWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1089, 805);
            this.Controls.Add(this.rcbOutput);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InRobotTestWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InRobotTestWindow";
            this.Load += new System.EventHandler(this.InRobotTestWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gridBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkSim;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.Label lblConnect1;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnCheckStatus1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button BtnCondition1;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnCondition2;
        private System.Windows.Forms.Button btnCondition3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetP1;
        private System.Windows.Forms.Button btnConP1;
        private System.Windows.Forms.Button btnExitP1;
        private System.Windows.Forms.Button btnComP1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExitP3;
        private System.Windows.Forms.Button btnComP3;
        private System.Windows.Forms.Button btnPutP3;
        private System.Windows.Forms.Button btnConP3;
    }
}