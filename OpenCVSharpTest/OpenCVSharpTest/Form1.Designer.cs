namespace OpenCVSharpTest
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmbH = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbV = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndivide = new System.Windows.Forms.Button();
            this.pnlImages = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUppB = new System.Windows.Forms.TextBox();
            this.txtUppR = new System.Windows.Forms.TextBox();
            this.txtUppG = new System.Windows.Forms.TextBox();
            this.txtLowR = new System.Windows.Forms.TextBox();
            this.txtLowG = new System.Windows.Forms.TextBox();
            this.txtLowB = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 296);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(168, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(103, 43);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
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
            this.cmbH.Location = new System.Drawing.Point(68, 12);
            this.cmbH.Name = "cmbH";
            this.cmbH.Size = new System.Drawing.Size(40, 20);
            this.cmbH.TabIndex = 52;
            this.cmbH.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(110, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 12);
            this.label9.TabIndex = 54;
            this.label9.Text = "X";
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
            this.cmbV.Location = new System.Drawing.Point(127, 12);
            this.cmbV.Name = "cmbV";
            this.cmbV.Size = new System.Drawing.Size(42, 20);
            this.cmbV.TabIndex = 55;
            this.cmbV.Text = "2";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbV);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbH);
            this.groupBox1.Location = new System.Drawing.Point(290, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 50);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // btndivide
            // 
            this.btndivide.Location = new System.Drawing.Point(470, 8);
            this.btndivide.Name = "btndivide";
            this.btndivide.Size = new System.Drawing.Size(79, 43);
            this.btndivide.TabIndex = 52;
            this.btndivide.Text = "Divide";
            this.btndivide.UseVisualStyleBackColor = true;
            this.btndivide.Click += new System.EventHandler(this.btndivide_Click);
            // 
            // pnlImages
            // 
            this.pnlImages.BackColor = System.Drawing.Color.Transparent;
            this.pnlImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImages.Location = new System.Drawing.Point(290, 57);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(259, 296);
            this.pnlImages.TabIndex = 53;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(569, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(259, 296);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 54;
            this.pictureBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(955, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 12);
            this.label8.TabIndex = 68;
            this.label8.Text = "Upper";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(955, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 12);
            this.label5.TabIndex = 67;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(954, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 12);
            this.label6.TabIndex = 66;
            this.label6.Text = "G";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(955, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 12);
            this.label7.TabIndex = 65;
            this.label7.Text = "R";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(834, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 64;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(833, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(834, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 62;
            this.label2.Text = "R";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(834, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 61;
            this.label1.Text = "Lower";
            // 
            // txtUppB
            // 
            this.txtUppB.Location = new System.Drawing.Point(974, 135);
            this.txtUppB.Name = "txtUppB";
            this.txtUppB.Size = new System.Drawing.Size(78, 21);
            this.txtUppB.TabIndex = 60;
            this.txtUppB.Text = "0";
            // 
            // txtUppR
            // 
            this.txtUppR.Location = new System.Drawing.Point(974, 78);
            this.txtUppR.Name = "txtUppR";
            this.txtUppR.Size = new System.Drawing.Size(78, 21);
            this.txtUppR.TabIndex = 58;
            this.txtUppR.Text = "0";
            // 
            // txtUppG
            // 
            this.txtUppG.Location = new System.Drawing.Point(974, 105);
            this.txtUppG.Name = "txtUppG";
            this.txtUppG.Size = new System.Drawing.Size(78, 21);
            this.txtUppG.TabIndex = 59;
            this.txtUppG.Text = "255";
            // 
            // txtLowR
            // 
            this.txtLowR.Location = new System.Drawing.Point(853, 78);
            this.txtLowR.Name = "txtLowR";
            this.txtLowR.Size = new System.Drawing.Size(78, 21);
            this.txtLowR.TabIndex = 55;
            this.txtLowR.Text = "0";
            // 
            // txtLowG
            // 
            this.txtLowG.Location = new System.Drawing.Point(853, 105);
            this.txtLowG.Name = "txtLowG";
            this.txtLowG.Size = new System.Drawing.Size(78, 21);
            this.txtLowG.TabIndex = 56;
            this.txtLowG.Text = "127";
            // 
            // txtLowB
            // 
            this.txtLowB.Location = new System.Drawing.Point(853, 132);
            this.txtLowB.Name = "txtLowB";
            this.txtLowB.Size = new System.Drawing.Size(78, 21);
            this.txtLowB.TabIndex = 57;
            this.txtLowB.Text = "0";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(836, 171);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 43);
            this.btnSearch.TabIndex = 69;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 365);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUppB);
            this.Controls.Add(this.txtUppR);
            this.Controls.Add(this.txtUppG);
            this.Controls.Add(this.txtLowR);
            this.Controls.Add(this.txtLowG);
            this.Controls.Add(this.txtLowB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.btndivide);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cmbH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btndivide;
        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUppB;
        private System.Windows.Forms.TextBox txtUppR;
        private System.Windows.Forms.TextBox txtUppG;
        private System.Windows.Forms.TextBox txtLowR;
        private System.Windows.Forms.TextBox txtLowG;
        private System.Windows.Forms.TextBox txtLowB;
        private System.Windows.Forms.Button btnSearch;
    }
}

