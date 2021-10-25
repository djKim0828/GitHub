namespace OpenCvTest
{
    partial class Form2
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbV = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbH = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUppB = new System.Windows.Forms.TextBox();
            this.txtUppR = new System.Windows.Forms.TextBox();
            this.txtUppG = new System.Windows.Forms.TextBox();
            this.txtLowR = new System.Windows.Forms.TextBox();
            this.txtLowG = new System.Windows.Forms.TextBox();
            this.txtLowB = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btndivide = new System.Windows.Forms.Button();
            this.pnlImages = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.btnInspection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(338, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(103, 27);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 180);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbV);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbH);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUppB);
            this.groupBox1.Controls.Add(this.txtUppR);
            this.groupBox1.Controls.Add(this.txtUppG);
            this.groupBox1.Controls.Add(this.txtLowR);
            this.groupBox1.Controls.Add(this.txtLowG);
            this.groupBox1.Controls.Add(this.txtLowB);
            this.groupBox1.Location = new System.Drawing.Point(472, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 192);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 12);
            this.label10.TabIndex = 59;
            this.label10.Text = "Divide";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 12);
            this.label8.TabIndex = 54;
            this.label8.Text = "Upper";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 12);
            this.label6.TabIndex = 52;
            this.label6.Text = "G";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 12);
            this.label7.TabIndex = 51;
            this.label7.Text = "R";
            // 
            // cmbV
            // 
            this.cmbV.FormattingEnabled = true;
            this.cmbV.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbV.Location = new System.Drawing.Point(111, 41);
            this.cmbV.Name = "cmbV";
            this.cmbV.Size = new System.Drawing.Size(80, 20);
            this.cmbV.TabIndex = 55;
            this.cmbV.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "G";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(92, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 12);
            this.label9.TabIndex = 54;
            this.label9.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "R";
            // 
            // cmbH
            // 
            this.cmbH.FormattingEnabled = true;
            this.cmbH.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbH.Location = new System.Drawing.Point(9, 41);
            this.cmbH.Name = "cmbH";
            this.cmbH.Size = new System.Drawing.Size(80, 20);
            this.cmbH.TabIndex = 52;
            this.cmbH.Text = "6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "Lower";
            // 
            // txtUppB
            // 
            this.txtUppB.Location = new System.Drawing.Point(146, 163);
            this.txtUppB.Name = "txtUppB";
            this.txtUppB.Size = new System.Drawing.Size(78, 21);
            this.txtUppB.TabIndex = 46;
            this.txtUppB.Text = "0";
            // 
            // txtUppR
            // 
            this.txtUppR.Location = new System.Drawing.Point(146, 106);
            this.txtUppR.Name = "txtUppR";
            this.txtUppR.Size = new System.Drawing.Size(78, 21);
            this.txtUppR.TabIndex = 44;
            this.txtUppR.Text = "0";
            // 
            // txtUppG
            // 
            this.txtUppG.Location = new System.Drawing.Point(146, 133);
            this.txtUppG.Name = "txtUppG";
            this.txtUppG.Size = new System.Drawing.Size(78, 21);
            this.txtUppG.TabIndex = 45;
            this.txtUppG.Text = "255";
            // 
            // txtLowR
            // 
            this.txtLowR.Location = new System.Drawing.Point(25, 106);
            this.txtLowR.Name = "txtLowR";
            this.txtLowR.Size = new System.Drawing.Size(78, 21);
            this.txtLowR.TabIndex = 41;
            this.txtLowR.Text = "0";
            // 
            // txtLowG
            // 
            this.txtLowG.Location = new System.Drawing.Point(25, 133);
            this.txtLowG.Name = "txtLowG";
            this.txtLowG.Size = new System.Drawing.Size(78, 21);
            this.txtLowG.TabIndex = 42;
            this.txtLowG.Text = "127";
            // 
            // txtLowB
            // 
            this.txtLowB.Location = new System.Drawing.Point(25, 160);
            this.txtLowB.Name = "txtLowB";
            this.txtLowB.Size = new System.Drawing.Size(78, 21);
            this.txtLowB.TabIndex = 43;
            this.txtLowB.Text = "0";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(121, 193);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 43);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btndivide
            // 
            this.btndivide.Location = new System.Drawing.Point(12, 193);
            this.btndivide.Name = "btndivide";
            this.btndivide.Size = new System.Drawing.Size(103, 43);
            this.btndivide.TabIndex = 50;
            this.btndivide.Text = "Divide";
            this.btndivide.UseVisualStyleBackColor = true;
            this.btndivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // pnlImages
            // 
            this.pnlImages.BackColor = System.Drawing.Color.Transparent;
            this.pnlImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImages.Location = new System.Drawing.Point(13, 242);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(320, 180);
            this.pnlImages.TabIndex = 51;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 576);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(895, 3);
            this.splitter1.TabIndex = 57;
            this.splitter1.TabStop = false;
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(0, 579);
            this.rcbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(895, 146);
            this.rcbOutput.TabIndex = 56;
            this.rcbOutput.Text = "";
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.Transparent;
            this.pnlResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResult.Location = new System.Drawing.Point(343, 242);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(320, 180);
            this.pnlResult.TabIndex = 52;
            // 
            // btnInspection
            // 
            this.btnInspection.Location = new System.Drawing.Point(230, 193);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(103, 43);
            this.btnInspection.TabIndex = 58;
            this.btnInspection.Text = "Inspection";
            this.btnInspection.UseVisualStyleBackColor = true;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 725);
            this.Controls.Add(this.btnInspection);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.rcbOutput);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.btndivide);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUppB;
        private System.Windows.Forms.TextBox txtUppR;
        private System.Windows.Forms.TextBox txtUppG;
        private System.Windows.Forms.TextBox txtLowR;
        private System.Windows.Forms.TextBox txtLowG;
        private System.Windows.Forms.TextBox txtLowB;
        private System.Windows.Forms.Button btndivide;
        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.ComboBox cmbH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbV;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Label label10;
    }
}