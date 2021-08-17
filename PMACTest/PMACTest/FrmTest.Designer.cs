namespace PMACTest
{
    partial class FrmTest
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
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.tabMesData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnHomePick = new System.Windows.Forms.Button();
            this.txtHoimngOffset = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHomingSpeed = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSCurvetime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAcclTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJogAccl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.cmbAxicId = new System.Windows.Forms.ComboBox();
            this.btnMove1 = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkEnableAmp = new System.Windows.Forms.CheckBox();
            this.btnSearchHome = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvIo = new System.Windows.Forms.DataGridView();
            this.colInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkIOAutoCheck = new System.Windows.Forms.CheckBox();
            this.btnJogMinus2 = new System.Windows.Forms.Button();
            this.btnJogPlus2 = new System.Windows.Forms.Button();
            this.lblMotorPosValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_roi_width = new System.Windows.Forms.TextBox();
            this.txt_roi_height = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlToolbar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tabMesData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIo)).BeginInit();
            this.pnlCommand.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlToolbar.Controls.Add(this.btnOpen);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1262, 58);
            this.pnlToolbar.TabIndex = 51;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 7);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(117, 45);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1262, 25);
            this.statusStrip1.TabIndex = 52;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.AutoSize = false;
            this.tslStatus.BackColor = System.Drawing.Color.Maroon;
            this.tslStatus.ForeColor = System.Drawing.Color.White;
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(100, 20);
            this.tslStatus.Text = "Connect";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.rcbOutput);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 596);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1262, 132);
            this.pnlBottom.TabIndex = 53;
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(0, 0);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(1262, 132);
            this.rcbOutput.TabIndex = 47;
            this.rcbOutput.Text = "";
            // 
            // tabMesData
            // 
            this.tabMesData.Controls.Add(this.tabPage1);
            this.tabMesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMesData.Location = new System.Drawing.Point(0, 58);
            this.tabMesData.Name = "tabMesData";
            this.tabMesData.SelectedIndex = 0;
            this.tabMesData.Size = new System.Drawing.Size(1262, 538);
            this.tabMesData.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.splitter3);
            this.tabPage1.Controls.Add(this.pnlCommand);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1254, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Maintance";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.pnlMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1044, 506);
            this.panel3.TabIndex = 52;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Gray;
            this.pnlMain.Controls.Add(this.button2);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.btnHomePick);
            this.pnlMain.Controls.Add(this.txtHoimngOffset);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.txtHomingSpeed);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.txtSCurvetime);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtAcclTime);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.txtJogAccl);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.btnRead);
            this.pnlMain.Controls.Add(this.btnAbort);
            this.pnlMain.Controls.Add(this.cmbAxicId);
            this.pnlMain.Controls.Add(this.btnMove1);
            this.pnlMain.Controls.Add(this.progressBar);
            this.pnlMain.Controls.Add(this.chkEnableAmp);
            this.pnlMain.Controls.Add(this.btnSearchHome);
            this.pnlMain.Controls.Add(this.groupBox2);
            this.pnlMain.Controls.Add(this.btnJogMinus2);
            this.pnlMain.Controls.Add(this.btnJogPlus2);
            this.pnlMain.Controls.Add(this.lblMotorPosValue);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1044, 506);
            this.pnlMain.TabIndex = 4;
            // 
            // btnHomePick
            // 
            this.btnHomePick.Location = new System.Drawing.Point(187, 262);
            this.btnHomePick.Name = "btnHomePick";
            this.btnHomePick.Size = new System.Drawing.Size(159, 45);
            this.btnHomePick.TabIndex = 69;
            this.btnHomePick.Text = "Home Pick";
            this.btnHomePick.UseVisualStyleBackColor = true;
            this.btnHomePick.Click += new System.EventHandler(this.btnHomePick_Click);
            // 
            // txtHoimngOffset
            // 
            this.txtHoimngOffset.Location = new System.Drawing.Point(121, 221);
            this.txtHoimngOffset.Name = "txtHoimngOffset";
            this.txtHoimngOffset.Size = new System.Drawing.Size(140, 21);
            this.txtHoimngOffset.TabIndex = 68;
            this.txtHoimngOffset.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(10, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 14);
            this.label9.TabIndex = 67;
            this.label9.Text = "Offset";
            // 
            // txtHomingSpeed
            // 
            this.txtHomingSpeed.Location = new System.Drawing.Point(121, 190);
            this.txtHomingSpeed.Name = "txtHomingSpeed";
            this.txtHomingSpeed.Size = new System.Drawing.Size(140, 21);
            this.txtHomingSpeed.TabIndex = 66;
            this.txtHomingSpeed.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(10, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 65;
            this.label8.Text = "Speed";
            // 
            // txtSCurvetime
            // 
            this.txtSCurvetime.Location = new System.Drawing.Point(121, 159);
            this.txtSCurvetime.Name = "txtSCurvetime";
            this.txtSCurvetime.Size = new System.Drawing.Size(140, 21);
            this.txtSCurvetime.TabIndex = 64;
            this.txtSCurvetime.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(10, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 14);
            this.label7.TabIndex = 63;
            this.label7.Text = "S-Curve Time";
            // 
            // txtAcclTime
            // 
            this.txtAcclTime.Location = new System.Drawing.Point(121, 128);
            this.txtAcclTime.Name = "txtAcclTime";
            this.txtAcclTime.Size = new System.Drawing.Size(140, 21);
            this.txtAcclTime.TabIndex = 62;
            this.txtAcclTime.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(10, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 61;
            this.label3.Text = "Accl Time";
            // 
            // txtJogAccl
            // 
            this.txtJogAccl.Location = new System.Drawing.Point(121, 97);
            this.txtJogAccl.Name = "txtJogAccl";
            this.txtJogAccl.Size = new System.Drawing.Size(140, 21);
            this.txtJogAccl.TabIndex = 60;
            this.txtJogAccl.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(10, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 59;
            this.label5.Text = "Jog Accl";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(10, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 14);
            this.label4.TabIndex = 58;
            this.label4.Text = "Homing";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(663, 104);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(159, 45);
            this.btnRead.TabIndex = 56;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(409, 216);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(159, 111);
            this.btnAbort.TabIndex = 55;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // cmbAxicId
            // 
            this.cmbAxicId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAxicId.FormattingEnabled = true;
            this.cmbAxicId.Location = new System.Drawing.Point(336, 19);
            this.cmbAxicId.Name = "cmbAxicId";
            this.cmbAxicId.Size = new System.Drawing.Size(159, 20);
            this.cmbAxicId.TabIndex = 54;
            // 
            // btnMove1
            // 
            this.btnMove1.Location = new System.Drawing.Point(603, 282);
            this.btnMove1.Name = "btnMove1";
            this.btnMove1.Size = new System.Drawing.Size(159, 45);
            this.btnMove1.TabIndex = 53;
            this.btnMove1.Text = "Move 1";
            this.btnMove1.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(336, 455);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(490, 23);
            this.progressBar.TabIndex = 52;
            this.progressBar.Value = 50;
            // 
            // chkEnableAmp
            // 
            this.chkEnableAmp.AutoSize = true;
            this.chkEnableAmp.Location = new System.Drawing.Point(409, 91);
            this.chkEnableAmp.Name = "chkEnableAmp";
            this.chkEnableAmp.Size = new System.Drawing.Size(93, 16);
            this.chkEnableAmp.TabIndex = 51;
            this.chkEnableAmp.Text = "Amp Enable";
            this.chkEnableAmp.UseVisualStyleBackColor = true;
            // 
            // btnSearchHome
            // 
            this.btnSearchHome.Location = new System.Drawing.Point(22, 262);
            this.btnSearchHome.Name = "btnSearchHome";
            this.btnSearchHome.Size = new System.Drawing.Size(159, 45);
            this.btnSearchHome.TabIndex = 50;
            this.btnSearchHome.Text = "Home Search";
            this.btnSearchHome.UseVisualStyleBackColor = true;
            this.btnSearchHome.Click += new System.EventHandler(this.btnSearchHome_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvIo);
            this.groupBox2.Controls.Add(this.chkIOAutoCheck);
            this.groupBox2.Location = new System.Drawing.Point(840, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 540);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "I/O";
            // 
            // dgvIo
            // 
            this.dgvIo.AllowUserToResizeColumns = false;
            this.dgvIo.AllowUserToResizeRows = false;
            this.dgvIo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInput,
            this.colOutput});
            this.dgvIo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvIo.Location = new System.Drawing.Point(18, 49);
            this.dgvIo.MultiSelect = false;
            this.dgvIo.Name = "dgvIo";
            this.dgvIo.ReadOnly = true;
            this.dgvIo.RowHeadersVisible = false;
            this.dgvIo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvIo.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvIo.RowTemplate.Height = 18;
            this.dgvIo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvIo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIo.Size = new System.Drawing.Size(165, 480);
            this.dgvIo.TabIndex = 49;
            // 
            // colInput
            // 
            this.colInput.HeaderText = "Input";
            this.colInput.Name = "colInput";
            this.colInput.ReadOnly = true;
            this.colInput.Width = 80;
            // 
            // colOutput
            // 
            this.colOutput.HeaderText = "Output";
            this.colOutput.Name = "colOutput";
            this.colOutput.ReadOnly = true;
            this.colOutput.Width = 80;
            // 
            // chkIOAutoCheck
            // 
            this.chkIOAutoCheck.AutoSize = true;
            this.chkIOAutoCheck.Location = new System.Drawing.Point(18, 24);
            this.chkIOAutoCheck.Name = "chkIOAutoCheck";
            this.chkIOAutoCheck.Size = new System.Drawing.Size(124, 16);
            this.chkIOAutoCheck.TabIndex = 48;
            this.chkIOAutoCheck.Text = "Automatic Check ";
            this.chkIOAutoCheck.UseVisualStyleBackColor = true;
            // 
            // btnJogMinus2
            // 
            this.btnJogMinus2.Location = new System.Drawing.Point(409, 165);
            this.btnJogMinus2.Name = "btnJogMinus2";
            this.btnJogMinus2.Size = new System.Drawing.Size(159, 45);
            this.btnJogMinus2.TabIndex = 47;
            this.btnJogMinus2.Text = "Jog -";
            this.btnJogMinus2.UseVisualStyleBackColor = true;
            this.btnJogMinus2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus_MouseDown);
            this.btnJogMinus2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus_MouseUp);
            // 
            // btnJogPlus2
            // 
            this.btnJogPlus2.Location = new System.Drawing.Point(409, 116);
            this.btnJogPlus2.Name = "btnJogPlus2";
            this.btnJogPlus2.Size = new System.Drawing.Size(159, 45);
            this.btnJogPlus2.TabIndex = 46;
            this.btnJogPlus2.Text = "Jog +";
            this.btnJogPlus2.UseVisualStyleBackColor = true;
            this.btnJogPlus2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus_MouseDown);
            this.btnJogPlus2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus_MouseUp);
            // 
            // lblMotorPosValue
            // 
            this.lblMotorPosValue.BackColor = System.Drawing.Color.White;
            this.lblMotorPosValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotorPosValue.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMotorPosValue.Location = new System.Drawing.Point(154, 17);
            this.lblMotorPosValue.Name = "lblMotorPosValue";
            this.lblMotorPosValue.Size = new System.Drawing.Size(176, 33);
            this.lblMotorPosValue.TabIndex = 1;
            this.lblMotorPosValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(19, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Motor Position";
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(1047, 3);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(4, 506);
            this.splitter3.TabIndex = 51;
            this.splitter3.TabStop = false;
            // 
            // pnlCommand
            // 
            this.pnlCommand.BackColor = System.Drawing.Color.DimGray;
            this.pnlCommand.Controls.Add(this.groupBox1);
            this.pnlCommand.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCommand.Location = new System.Drawing.Point(1051, 3);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(200, 506);
            this.pnlCommand.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_roi_width);
            this.groupBox1.Controls.Add(this.txt_roi_height);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 238);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "width";
            // 
            // txt_roi_width
            // 
            this.txt_roi_width.Location = new System.Drawing.Point(67, 53);
            this.txt_roi_width.Name = "txt_roi_width";
            this.txt_roi_width.Size = new System.Drawing.Size(69, 21);
            this.txt_roi_width.TabIndex = 17;
            this.txt_roi_width.Text = "0.5";
            // 
            // txt_roi_height
            // 
            this.txt_roi_height.Location = new System.Drawing.Point(67, 78);
            this.txt_roi_height.Name = "txt_roi_height";
            this.txt_roi_height.Size = new System.Drawing.Size(69, 21);
            this.txt_roi_height.TabIndex = 18;
            this.txt_roi_height.Text = "0.5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 111);
            this.button1.TabIndex = 70;
            this.button1.Text = "io0";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(187, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 111);
            this.button2.TabIndex = 71;
            this.button2.Text = "kill";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1262, 753);
            this.Controls.Add(this.tabMesData);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlToolbar);
            this.Name = "FrmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTest_FormClosed);
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.pnlToolbar.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.tabMesData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIo)).EndInit();
            this.pnlCommand.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.TabControl tabMesData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_roi_width;
        private System.Windows.Forms.TextBox txt_roi_height;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblMotorPosValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvIo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOutput;
        private System.Windows.Forms.CheckBox chkIOAutoCheck;
        private System.Windows.Forms.Button btnJogMinus2;
        private System.Windows.Forms.Button btnJogPlus2;
        private System.Windows.Forms.Button btnMove1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox chkEnableAmp;
        private System.Windows.Forms.Button btnSearchHome;
        private System.Windows.Forms.ComboBox cmbAxicId;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtHoimngOffset;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHomingSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSCurvetime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAcclTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJogAccl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnHomePick;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}