namespace EmWorks.App.Sample.View.Dialog
{
    partial class AutoSpotResultDialog
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pnlXY = new System.Windows.Forms.Panel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pnlUV = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chtWave = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtWave)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMesData
            // 
            this.tabMesData.Controls.Add(this.tabPage1);
            this.tabMesData.Controls.Add(this.tabPage3);
            this.tabMesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMesData.Location = new System.Drawing.Point(0, 0);
            this.tabMesData.Name = "tabMesData";
            this.tabMesData.SelectedIndex = 0;
            this.tabMesData.Size = new System.Drawing.Size(1270, 518);
            this.tabMesData.TabIndex = 50;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.splitter3);
            this.tabPage1.Controls.Add(this.pnlCommand);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1262, 489);
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
            this.panel3.Size = new System.Drawing.Size(1052, 483);
            this.panel3.TabIndex = 52;
            // 
            // pnlPseudo
            // 
            this.pnlPseudo.BackColor = System.Drawing.Color.Black;
            this.pnlPseudo.Controls.Add(this.pBMeasurePseudo);
            this.pnlPseudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPseudo.Location = new System.Drawing.Point(0, 0);
            this.pnlPseudo.Name = "pnlPseudo";
            this.pnlPseudo.Size = new System.Drawing.Size(1052, 483);
            this.pnlPseudo.TabIndex = 4;
            // 
            // pBMeasurePseudo
            // 
            this.pBMeasurePseudo.BackColor = System.Drawing.Color.Black;
            this.pBMeasurePseudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBMeasurePseudo.Location = new System.Drawing.Point(0, 0);
            this.pBMeasurePseudo.Margin = new System.Windows.Forms.Padding(4);
            this.pBMeasurePseudo.Name = "pBMeasurePseudo";
            this.pBMeasurePseudo.Size = new System.Drawing.Size(1052, 483);
            this.pBMeasurePseudo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBMeasurePseudo.TabIndex = 3;
            this.pBMeasurePseudo.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(1055, 3);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(4, 483);
            this.splitter3.TabIndex = 51;
            this.splitter3.TabStop = false;
            // 
            // pnlCommand
            // 
            this.pnlCommand.BackColor = System.Drawing.Color.DimGray;
            this.pnlCommand.Controls.Add(this.groupBox1);
            this.pnlCommand.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCommand.Location = new System.Drawing.Point(1059, 3);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(200, 483);
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
            this.txt_roi_width.Text = "0.3";
            // 
            // txt_roi_height
            // 
            this.txt_roi_height.Location = new System.Drawing.Point(67, 78);
            this.txt_roi_height.Name = "txt_roi_height";
            this.txt_roi_height.Size = new System.Drawing.Size(69, 25);
            this.txt_roi_height.TabIndex = 18;
            this.txt_roi_height.Text = "0.3";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitter4);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.dgvData);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1262, 489);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitter4
            // 
            this.splitter4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter4.Location = new System.Drawing.Point(787, 3);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 483);
            this.splitter4.TabIndex = 2;
            this.splitter4.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbcChro);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(787, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 483);
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
            this.tbcChro.Size = new System.Drawing.Size(472, 483);
            this.tbcChro.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.chtWave);
            this.tabPage6.Location = new System.Drawing.Point(4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(464, 454);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Wave";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.pnlXY);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(469, 454);
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
            this.tabPage5.Size = new System.Drawing.Size(464, 454);
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
            this.dgvData.Size = new System.Drawing.Size(784, 483);
            this.dgvData.TabIndex = 0;
            // 
            // chtWave
            // 
            chartArea6.Name = "ChartArea";
            this.chtWave.ChartAreas.Add(chartArea6);
            this.chtWave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtWave.Location = new System.Drawing.Point(3, 3);
            this.chtWave.Name = "chtWave";
            series6.ChartArea = "ChartArea";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "srsWave";
            this.chtWave.Series.Add(series6);
            this.chtWave.Size = new System.Drawing.Size(458, 448);
            this.chtWave.TabIndex = 2;
            this.chtWave.Text = "chart1";
            // 
            // AutoSpotResultDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1270, 518);
            this.Controls.Add(this.tabMesData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoSpotResultDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoSpotResultDialog";
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
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtWave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMesData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlPseudo;
        private System.Windows.Forms.PictureBox pBMeasurePseudo;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_roi_max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_roi_min;
        private System.Windows.Forms.Button btnSearchClear;
        private System.Windows.Forms.Button btnSearchROI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rd_Circle;
        private System.Windows.Forms.TextBox txt_y_threshold;
        private System.Windows.Forms.RadioButton rd_Square;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_roi_width;
        private System.Windows.Forms.TextBox txt_roi_height;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tbcChro;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel pnlXY;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel pnlUV;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtWave;
    }
}