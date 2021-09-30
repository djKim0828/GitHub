namespace OpenCvTest
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
            this.btnSearchDef = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImage1 = new System.Windows.Forms.Button();
            this.btnImage2 = new System.Windows.Forms.Button();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.txtLowR = new System.Windows.Forms.TextBox();
            this.txtLowG = new System.Windows.Forms.TextBox();
            this.txtLowB = new System.Windows.Forms.TextBox();
            this.txtUppB = new System.Windows.Forms.TextBox();
            this.txtUppG = new System.Windows.Forms.TextBox();
            this.txtUppR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCut = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(658, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(103, 43);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSearchDef
            // 
            this.btnSearchDef.Location = new System.Drawing.Point(660, 144);
            this.btnSearchDef.Name = "btnSearchDef";
            this.btnSearchDef.Size = new System.Drawing.Size(103, 43);
            this.btnSearchDef.TabIndex = 2;
            this.btnSearchDef.Text = "Search(defalt)";
            this.btnSearchDef.UseVisualStyleBackColor = true;
            this.btnSearchDef.Click += new System.EventHandler(this.btnSearchDef_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(235, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 99);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImage1
            // 
            this.btnImage1.Location = new System.Drawing.Point(659, 61);
            this.btnImage1.Name = "btnImage1";
            this.btnImage1.Size = new System.Drawing.Size(103, 30);
            this.btnImage1.TabIndex = 4;
            this.btnImage1.Text = "Image1";
            this.btnImage1.UseVisualStyleBackColor = true;
            this.btnImage1.Click += new System.EventHandler(this.btnImage1_Click);
            // 
            // btnImage2
            // 
            this.btnImage2.Location = new System.Drawing.Point(660, 97);
            this.btnImage2.Name = "btnImage2";
            this.btnImage2.Size = new System.Drawing.Size(103, 30);
            this.btnImage2.TabIndex = 5;
            this.btnImage2.Text = "Image2";
            this.btnImage2.UseVisualStyleBackColor = true;
            this.btnImage2.Click += new System.EventHandler(this.btnImage2_Click);
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(0, 377);
            this.rcbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(1156, 146);
            this.rcbOutput.TabIndex = 39;
            this.rcbOutput.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 374);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1156, 3);
            this.splitter1.TabIndex = 40;
            this.splitter1.TabStop = false;
            // 
            // txtLowR
            // 
            this.txtLowR.Location = new System.Drawing.Point(30, 31);
            this.txtLowR.Name = "txtLowR";
            this.txtLowR.Size = new System.Drawing.Size(78, 21);
            this.txtLowR.TabIndex = 41;
            this.txtLowR.Text = "0";
            // 
            // txtLowG
            // 
            this.txtLowG.Location = new System.Drawing.Point(30, 58);
            this.txtLowG.Name = "txtLowG";
            this.txtLowG.Size = new System.Drawing.Size(78, 21);
            this.txtLowG.TabIndex = 42;
            this.txtLowG.Text = "127";
            // 
            // txtLowB
            // 
            this.txtLowB.Location = new System.Drawing.Point(30, 85);
            this.txtLowB.Name = "txtLowB";
            this.txtLowB.Size = new System.Drawing.Size(78, 21);
            this.txtLowB.TabIndex = 43;
            this.txtLowB.Text = "0";
            // 
            // txtUppB
            // 
            this.txtUppB.Location = new System.Drawing.Point(151, 88);
            this.txtUppB.Name = "txtUppB";
            this.txtUppB.Size = new System.Drawing.Size(78, 21);
            this.txtUppB.TabIndex = 46;
            this.txtUppB.Text = "0";
            // 
            // txtUppG
            // 
            this.txtUppG.Location = new System.Drawing.Point(151, 58);
            this.txtUppG.Name = "txtUppG";
            this.txtUppG.Size = new System.Drawing.Size(78, 21);
            this.txtUppG.TabIndex = 45;
            this.txtUppG.Text = "255";
            // 
            // txtUppR
            // 
            this.txtUppR.Location = new System.Drawing.Point(151, 31);
            this.txtUppR.Name = "txtUppR";
            this.txtUppR.Size = new System.Drawing.Size(78, 21);
            this.txtUppR.TabIndex = 44;
            this.txtUppR.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 47;
            this.label1.Text = "Lower";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUppB);
            this.groupBox1.Controls.Add(this.txtUppR);
            this.groupBox1.Controls.Add(this.txtUppG);
            this.groupBox1.Controls.Add(this.txtLowR);
            this.groupBox1.Controls.Add(this.txtLowG);
            this.groupBox1.Controls.Add(this.txtLowB);
            this.groupBox1.Location = new System.Drawing.Point(769, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 115);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "R";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "G";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 12);
            this.label6.TabIndex = 52;
            this.label6.Text = "G";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 12);
            this.label7.TabIndex = 51;
            this.label7.Text = "R";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 12);
            this.label8.TabIndex = 54;
            this.label8.Text = "Upper";
            // 
            // btnCut
            // 
            this.btnCut.Location = new System.Drawing.Point(986, 202);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(103, 43);
            this.btnCut.TabIndex = 49;
            this.btnCut.Text = "Cut";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(660, 188);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 180);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 523);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnCut);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.rcbOutput);
            this.Controls.Add(this.btnImage2);
            this.Controls.Add(this.btnImage1);
            this.Controls.Add(this.btnSearchDef);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSearchDef;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnImage1;
        private System.Windows.Forms.Button btnImage2;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox txtLowR;
        private System.Windows.Forms.TextBox txtLowG;
        private System.Windows.Forms.TextBox txtLowB;
        private System.Windows.Forms.TextBox txtUppB;
        private System.Windows.Forms.TextBox txtUppG;
        private System.Windows.Forms.TextBox txtUppR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

