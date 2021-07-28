namespace LiveView
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabMesData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlPseudo = new System.Windows.Forms.Panel();
            this.pBMeasurePseudo = new System.Windows.Forms.PictureBox();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_roi_max = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_roi_min = new System.Windows.Forms.TextBox();
            this.btnSearchClear = new System.Windows.Forms.Button();
            this.btnSearchROI = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rd_Circle = new System.Windows.Forms.RadioButton();
            this.txt_y_threshold = new System.Windows.Forms.TextBox();
            this.rd_Square = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_roi_width = new System.Windows.Forms.TextBox();
            this.txt_roi_height = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbcChro = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.chtWave = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pnlXY = new System.Windows.Forms.Panel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pnlUV = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.pictureBoxLive = new System.Windows.Forms.PictureBox();
            this.pnlRgb = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pBMeasureRGB = new System.Windows.Forms.PictureBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnGetRGB = new System.Windows.Forms.Button();
            this.btnLoadRecipe = new System.Windows.Forms.Button();
            this.btnSaveMes = new System.Windows.Forms.Button();
            this.btnLoadMesFile = new System.Windows.Forms.Button();
            this.btnDestroyMes = new System.Windows.Forms.Button();
            this.btnStopMes = new System.Windows.Forms.Button();
            this.btnStartMeasurement = new System.Windows.Forms.Button();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1.SuspendLayout();
            this.tabMesData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPseudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasurePseudo)).BeginInit();
            this.pnlCommand.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbcChro.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtWave)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).BeginInit();
            this.pnlRgb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasureRGB)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
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
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 696);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1262, 25);
            this.statusStrip1.TabIndex = 46;
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
            // tabMesData
            // 
            this.tabMesData.Controls.Add(this.tabPage1);
            this.tabMesData.Controls.Add(this.tabPage3);
            this.tabMesData.Controls.Add(this.tabPage2);
            this.tabMesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMesData.Location = new System.Drawing.Point(0, 39);
            this.tabMesData.Name = "tabMesData";
            this.tabMesData.SelectedIndex = 0;
            this.tabMesData.Size = new System.Drawing.Size(1262, 521);
            this.tabMesData.TabIndex = 49;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.splitter3);
            this.tabPage1.Controls.Add(this.pnlCommand);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1254, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Measurement";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.pnlPseudo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1044, 486);
            this.panel3.TabIndex = 52;
            // 
            // pnlPseudo
            // 
            this.pnlPseudo.BackColor = System.Drawing.Color.Black;
            this.pnlPseudo.Controls.Add(this.pBMeasurePseudo);
            this.pnlPseudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPseudo.Location = new System.Drawing.Point(0, 0);
            this.pnlPseudo.Name = "pnlPseudo";
            this.pnlPseudo.Size = new System.Drawing.Size(1044, 486);
            this.pnlPseudo.TabIndex = 4;
            // 
            // pBMeasurePseudo
            // 
            this.pBMeasurePseudo.BackColor = System.Drawing.Color.Black;
            this.pBMeasurePseudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBMeasurePseudo.Location = new System.Drawing.Point(0, 0);
            this.pBMeasurePseudo.Margin = new System.Windows.Forms.Padding(4);
            this.pBMeasurePseudo.Name = "pBMeasurePseudo";
            this.pBMeasurePseudo.Size = new System.Drawing.Size(1044, 486);
            this.pBMeasurePseudo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMeasurePseudo.TabIndex = 3;
            this.pBMeasurePseudo.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(1047, 3);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(4, 486);
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
            this.pnlCommand.Size = new System.Drawing.Size(200, 486);
            this.pnlCommand.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_roi_max);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_roi_min);
            this.groupBox1.Controls.Add(this.btnSearchClear);
            this.groupBox1.Controls.Add(this.btnSearchROI);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rd_Circle);
            this.groupBox1.Controls.Add(this.txt_y_threshold);
            this.groupBox1.Controls.Add(this.rd_Square);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_roi_width);
            this.groupBox1.Controls.Add(this.txt_roi_height);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 238);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Spot";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "max";
            // 
            // txt_roi_max
            // 
            this.txt_roi_max.Location = new System.Drawing.Point(67, 128);
            this.txt_roi_max.Name = "txt_roi_max";
            this.txt_roi_max.Size = new System.Drawing.Size(69, 25);
            this.txt_roi_max.TabIndex = 20;
            this.txt_roi_max.Text = "3000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "min";
            // 
            // txt_roi_min
            // 
            this.txt_roi_min.Location = new System.Drawing.Point(67, 103);
            this.txt_roi_min.Name = "txt_roi_min";
            this.txt_roi_min.Size = new System.Drawing.Size(69, 25);
            this.txt_roi_min.TabIndex = 19;
            this.txt_roi_min.Text = "200";
            // 
            // btnSearchClear
            // 
            this.btnSearchClear.Location = new System.Drawing.Point(119, 185);
            this.btnSearchClear.Name = "btnSearchClear";
            this.btnSearchClear.Size = new System.Drawing.Size(67, 45);
            this.btnSearchClear.TabIndex = 23;
            this.btnSearchClear.Text = "Clear";
            this.btnSearchClear.UseVisualStyleBackColor = true;
            this.btnSearchClear.Click += new System.EventHandler(this.btnSearchClear_Click);
            // 
            // btnSearchROI
            // 
            this.btnSearchROI.Location = new System.Drawing.Point(3, 185);
            this.btnSearchROI.Name = "btnSearchROI";
            this.btnSearchROI.Size = new System.Drawing.Size(110, 45);
            this.btnSearchROI.TabIndex = 22;
            this.btnSearchROI.Text = "Search";
            this.btnSearchROI.UseVisualStyleBackColor = true;
            this.btnSearchROI.Click += new System.EventHandler(this.btnSearchROI_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "y trh.";
            // 
            // rd_Circle
            // 
            this.rd_Circle.AutoSize = true;
            this.rd_Circle.Location = new System.Drawing.Point(19, 24);
            this.rd_Circle.Name = "rd_Circle";
            this.rd_Circle.Size = new System.Drawing.Size(64, 19);
            this.rd_Circle.TabIndex = 24;
            this.rd_Circle.Text = "Circle";
            this.rd_Circle.UseVisualStyleBackColor = true;
            // 
            // txt_y_threshold
            // 
            this.txt_y_threshold.Location = new System.Drawing.Point(67, 154);
            this.txt_y_threshold.Name = "txt_y_threshold";
            this.txt_y_threshold.Size = new System.Drawing.Size(69, 25);
            this.txt_y_threshold.TabIndex = 21;
            this.txt_y_threshold.Text = "200";
            // 
            // rd_Square
            // 
            this.rd_Square.AutoSize = true;
            this.rd_Square.Checked = true;
            this.rd_Square.Location = new System.Drawing.Point(101, 24);
            this.rd_Square.Name = "rd_Square";
            this.rd_Square.Size = new System.Drawing.Size(74, 19);
            this.rd_Square.TabIndex = 23;
            this.rd_Square.TabStop = true;
            this.rd_Square.Text = "Square";
            this.rd_Square.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "width";
            // 
            // txt_roi_width
            // 
            this.txt_roi_width.Location = new System.Drawing.Point(67, 53);
            this.txt_roi_width.Name = "txt_roi_width";
            this.txt_roi_width.Size = new System.Drawing.Size(69, 25);
            this.txt_roi_width.TabIndex = 17;
            this.txt_roi_width.Text = "0.5";
            // 
            // txt_roi_height
            // 
            this.txt_roi_height.Location = new System.Drawing.Point(67, 78);
            this.txt_roi_height.Name = "txt_roi_height";
            this.txt_roi_height.Size = new System.Drawing.Size(69, 25);
            this.txt_roi_height.TabIndex = 18;
            this.txt_roi_height.Text = "0.5";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitter4);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.dgvData);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1254, 492);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitter4
            // 
            this.splitter4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter4.Location = new System.Drawing.Point(787, 3);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 486);
            this.splitter4.TabIndex = 2;
            this.splitter4.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbcChro);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(787, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 486);
            this.panel2.TabIndex = 1;
            // 
            // tbcChro
            // 
            this.tbcChro.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcChro.Controls.Add(this.tabPage6);
            this.tbcChro.Controls.Add(this.tabPage4);
            this.tbcChro.Controls.Add(this.tabPage5);
            this.tbcChro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcChro.Location = new System.Drawing.Point(0, 0);
            this.tbcChro.Name = "tbcChro";
            this.tbcChro.SelectedIndex = 0;
            this.tbcChro.Size = new System.Drawing.Size(464, 486);
            this.tbcChro.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.chtWave);
            this.tabPage6.Location = new System.Drawing.Point(4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(456, 457);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Wave";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // chtWave
            // 
            this.chtWave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chtWave.BorderlineWidth = 5;
            this.chtWave.BorderSkin.BackColor = System.Drawing.Color.White;
            this.chtWave.BorderSkin.BorderColor = System.Drawing.Color.White;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.chtWave.ChartAreas.Add(chartArea1);
            this.chtWave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtWave.Location = new System.Drawing.Point(3, 3);
            this.chtWave.Name = "chtWave";
            series1.BorderColor = System.Drawing.Color.Gold;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Name = "srsWave";
            this.chtWave.Series.Add(series1);
            this.chtWave.Size = new System.Drawing.Size(450, 451);
            this.chtWave.TabIndex = 1;
            this.chtWave.Text = "chart1";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.pnlXY);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(456, 457);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "XY";
            // 
            // pnlXY
            // 
            this.pnlXY.BackColor = System.Drawing.Color.Black;
            this.pnlXY.Location = new System.Drawing.Point(30, 4);
            this.pnlXY.Name = "pnlXY";
            this.pnlXY.Size = new System.Drawing.Size(400, 444);
            this.pnlXY.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.pnlUV);
            this.tabPage5.Location = new System.Drawing.Point(4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(456, 457);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "UV";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pnlUV
            // 
            this.pnlUV.BackColor = System.Drawing.Color.Black;
            this.pnlUV.Location = new System.Drawing.Point(4, 4);
            this.pnlUV.Name = "pnlUV";
            this.pnlUV.Size = new System.Drawing.Size(453, 445);
            this.pnlUV.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvData.Location = new System.Drawing.Point(3, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 27;
            this.dgvData.Size = new System.Drawing.Size(784, 486);
            this.dgvData.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitter2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.pnlCenter);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1254, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LiveView";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(1100, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(4, 486);
            this.splitter2.TabIndex = 50;
            this.splitter2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1104, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 486);
            this.panel1.TabIndex = 49;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(123, 61);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 79);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(123, 61);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Gray;
            this.pnlCenter.Controls.Add(this.pictureBoxLive);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(3, 3);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(1248, 486);
            this.pnlCenter.TabIndex = 51;
            // 
            // pictureBoxLive
            // 
            this.pictureBoxLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLive.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLive.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLive.Name = "pictureBoxLive";
            this.pictureBoxLive.Size = new System.Drawing.Size(1248, 486);
            this.pictureBoxLive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLive.TabIndex = 2;
            this.pictureBoxLive.TabStop = false;
            // 
            // pnlRgb
            // 
            this.pnlRgb.BackColor = System.Drawing.Color.White;
            this.pnlRgb.Controls.Add(this.label6);
            this.pnlRgb.Controls.Add(this.pBMeasureRGB);
            this.pnlRgb.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRgb.Location = new System.Drawing.Point(1051, 0);
            this.pnlRgb.Name = "pnlRgb";
            this.pnlRgb.Size = new System.Drawing.Size(211, 132);
            this.pnlRgb.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "RGB";
            // 
            // pBMeasureRGB
            // 
            this.pBMeasureRGB.BackColor = System.Drawing.Color.Black;
            this.pBMeasureRGB.Location = new System.Drawing.Point(4, 31);
            this.pBMeasureRGB.Margin = new System.Windows.Forms.Padding(4);
            this.pBMeasureRGB.Name = "pBMeasureRGB";
            this.pBMeasureRGB.Size = new System.Drawing.Size(200, 97);
            this.pBMeasureRGB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMeasureRGB.TabIndex = 2;
            this.pBMeasureRGB.TabStop = false;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(933, 7);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(160, 26);
            this.btnSaveFile.TabIndex = 10;
            this.btnSaveFile.Text = "Save As Image";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnGetRGB
            // 
            this.btnGetRGB.Location = new System.Drawing.Point(1098, 7);
            this.btnGetRGB.Name = "btnGetRGB";
            this.btnGetRGB.Size = new System.Drawing.Size(160, 26);
            this.btnGetRGB.TabIndex = 3;
            this.btnGetRGB.Text = "Get Image from Mea.";
            this.btnGetRGB.UseVisualStyleBackColor = true;
            this.btnGetRGB.Click += new System.EventHandler(this.btnGetRGB_Click);
            // 
            // btnLoadRecipe
            // 
            this.btnLoadRecipe.Location = new System.Drawing.Point(12, 7);
            this.btnLoadRecipe.Name = "btnLoadRecipe";
            this.btnLoadRecipe.Size = new System.Drawing.Size(117, 26);
            this.btnLoadRecipe.TabIndex = 0;
            this.btnLoadRecipe.Text = "Load Recipe";
            this.btnLoadRecipe.UseVisualStyleBackColor = true;
            this.btnLoadRecipe.Click += new System.EventHandler(this.btnLoadRecipe_Click);
            // 
            // btnSaveMes
            // 
            this.btnSaveMes.Location = new System.Drawing.Point(278, 7);
            this.btnSaveMes.Name = "btnSaveMes";
            this.btnSaveMes.Size = new System.Drawing.Size(117, 26);
            this.btnSaveMes.TabIndex = 9;
            this.btnSaveMes.Text = "Save Mea. File";
            this.btnSaveMes.UseVisualStyleBackColor = true;
            this.btnSaveMes.Click += new System.EventHandler(this.btnSaveMes_Click);
            // 
            // btnLoadMesFile
            // 
            this.btnLoadMesFile.Location = new System.Drawing.Point(161, 7);
            this.btnLoadMesFile.Name = "btnLoadMesFile";
            this.btnLoadMesFile.Size = new System.Drawing.Size(117, 26);
            this.btnLoadMesFile.TabIndex = 8;
            this.btnLoadMesFile.Text = "Load Mea. File";
            this.btnLoadMesFile.UseVisualStyleBackColor = true;
            this.btnLoadMesFile.Click += new System.EventHandler(this.btnLoadMesFile_Click);
            // 
            // btnDestroyMes
            // 
            this.btnDestroyMes.Location = new System.Drawing.Point(395, 7);
            this.btnDestroyMes.Name = "btnDestroyMes";
            this.btnDestroyMes.Size = new System.Drawing.Size(117, 26);
            this.btnDestroyMes.TabIndex = 5;
            this.btnDestroyMes.Text = "Destroy Mea.";
            this.btnDestroyMes.UseVisualStyleBackColor = true;
            this.btnDestroyMes.Click += new System.EventHandler(this.btnDestroyMes_Click);
            // 
            // btnStopMes
            // 
            this.btnStopMes.Location = new System.Drawing.Point(659, 7);
            this.btnStopMes.Name = "btnStopMes";
            this.btnStopMes.Size = new System.Drawing.Size(117, 26);
            this.btnStopMes.TabIndex = 4;
            this.btnStopMes.Text = "Stop";
            this.btnStopMes.UseVisualStyleBackColor = true;
            this.btnStopMes.Click += new System.EventHandler(this.btnStopMes_Click);
            // 
            // btnStartMeasurement
            // 
            this.btnStartMeasurement.Location = new System.Drawing.Point(542, 7);
            this.btnStartMeasurement.Name = "btnStartMeasurement";
            this.btnStartMeasurement.Size = new System.Drawing.Size(117, 26);
            this.btnStartMeasurement.TabIndex = 1;
            this.btnStartMeasurement.Text = "Start Mea";
            this.btnStartMeasurement.UseVisualStyleBackColor = true;
            this.btnStartMeasurement.Click += new System.EventHandler(this.btnStartMeasurement_Click);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 50;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlToolbar.Controls.Add(this.btnLoadMesFile);
            this.pnlToolbar.Controls.Add(this.btnLoadRecipe);
            this.pnlToolbar.Controls.Add(this.btnGetRGB);
            this.pnlToolbar.Controls.Add(this.btnSaveFile);
            this.pnlToolbar.Controls.Add(this.btnSaveMes);
            this.pnlToolbar.Controls.Add(this.btnStopMes);
            this.pnlToolbar.Controls.Add(this.btnDestroyMes);
            this.pnlToolbar.Controls.Add(this.btnStartMeasurement);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1262, 39);
            this.pnlToolbar.TabIndex = 50;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlRgb);
            this.pnlBottom.Controls.Add(this.rcbOutput);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 564);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1262, 132);
            this.pnlBottom.TabIndex = 51;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 560);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1262, 4);
            this.splitter1.TabIndex = 48;
            this.splitter1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1262, 721);
            this.Controls.Add(this.tabMesData);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlToolbar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabMesData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlPseudo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasurePseudo)).EndInit();
            this.pnlCommand.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tbcChro.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtWave)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).EndInit();
            this.pnlRgb.ResumeLayout(false);
            this.pnlRgb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMeasureRGB)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.TabControl tabMesData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.PictureBox pictureBoxLive;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.Button btnLoadRecipe;
        private System.Windows.Forms.Button btnStartMeasurement;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pBMeasureRGB;
        private System.Windows.Forms.Button btnGetRGB;
        private System.Windows.Forms.Button btnStopMes;
        private System.Windows.Forms.Button btnDestroyMes;
        private System.Windows.Forms.Button btnLoadMesFile;
        private System.Windows.Forms.Button btnSaveMes;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.PictureBox pBMeasurePseudo;
        private System.Windows.Forms.Panel pnlPseudo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearchROI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rd_Circle;
        private System.Windows.Forms.TextBox txt_y_threshold;
        private System.Windows.Forms.RadioButton rd_Square;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_roi_width;
        private System.Windows.Forms.TextBox txt_roi_height;
        private System.Windows.Forms.Button btnSearchClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_roi_max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_roi_min;
        private System.Windows.Forms.Panel pnlRgb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tbcChro;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtWave;
        private System.Windows.Forms.Panel pnlXY;
        private System.Windows.Forms.Panel pnlUV;
    }
}