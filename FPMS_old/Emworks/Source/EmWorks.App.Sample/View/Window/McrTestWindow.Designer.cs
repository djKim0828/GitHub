namespace EmWorks.App.Sample.View.Window
{
    partial class McrTestWindow
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
            this.btnCheckStatus2 = new System.Windows.Forms.Button();
            this.lblResult2 = new System.Windows.Forms.Label();
            this.btnTrigger2 = new System.Windows.Forms.Button();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.txtPort2 = new System.Windows.Forms.TextBox();
            this.txtIp2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOpen2 = new System.Windows.Forms.Button();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnCheckStatus1 = new System.Windows.Forms.Button();
            this.lblResult1 = new System.Windows.Forms.Label();
            this.btnTrigger1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnClose2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPort1 = new System.Windows.Forms.TextBox();
            this.txtIp1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpen1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.txtTestCount = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.gridBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckStatus2
            // 
            this.btnCheckStatus2.Location = new System.Drawing.Point(384, 21);
            this.btnCheckStatus2.Name = "btnCheckStatus2";
            this.btnCheckStatus2.Size = new System.Drawing.Size(82, 35);
            this.btnCheckStatus2.TabIndex = 18;
            this.btnCheckStatus2.Text = "Refresh";
            this.btnCheckStatus2.UseVisualStyleBackColor = true;
            this.btnCheckStatus2.Click += new System.EventHandler(this.btnCheckStatus2_Click);
            // 
            // lblResult2
            // 
            this.lblResult2.BackColor = System.Drawing.Color.White;
            this.lblResult2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult2.Location = new System.Drawing.Point(6, 195);
            this.lblResult2.Name = "lblResult2";
            this.lblResult2.Size = new System.Drawing.Size(460, 52);
            this.lblResult2.TabIndex = 16;
            this.lblResult2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrigger2
            // 
            this.btnTrigger2.Location = new System.Drawing.Point(6, 138);
            this.btnTrigger2.Name = "btnTrigger2";
            this.btnTrigger2.Size = new System.Drawing.Size(460, 54);
            this.btnTrigger2.TabIndex = 15;
            this.btnTrigger2.Text = "Trigger";
            this.btnTrigger2.UseVisualStyleBackColor = true;
            this.btnTrigger2.Click += new System.EventHandler(this.btnTrigger2_Click);
            // 
            // lblStatus2
            // 
            this.lblStatus2.BackColor = System.Drawing.Color.Crimson;
            this.lblStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus2.Location = new System.Drawing.Point(12, 21);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(366, 35);
            this.lblStatus2.TabIndex = 14;
            this.lblStatus2.Text = "Status";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPort2
            // 
            this.txtPort2.Enabled = false;
            this.txtPort2.Location = new System.Drawing.Point(62, 100);
            this.txtPort2.Name = "txtPort2";
            this.txtPort2.Size = new System.Drawing.Size(189, 25);
            this.txtPort2.TabIndex = 13;
            // 
            // txtIp2
            // 
            this.txtIp2.Enabled = false;
            this.txtIp2.Location = new System.Drawing.Point(62, 69);
            this.txtIp2.Name = "txtIp2";
            this.txtIp2.Size = new System.Drawing.Size(189, 25);
            this.txtIp2.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Port";
            // 
            // btnOpen2
            // 
            this.btnOpen2.Location = new System.Drawing.Point(257, 69);
            this.btnOpen2.Name = "btnOpen2";
            this.btnOpen2.Size = new System.Drawing.Size(121, 56);
            this.btnOpen2.TabIndex = 9;
            this.btnOpen2.Text = "Open(Connect)";
            this.btnOpen2.UseVisualStyleBackColor = true;
            this.btnOpen2.Click += new System.EventHandler(this.btnOpen2_Click);
            // 
            // btnClose1
            // 
            this.btnClose1.Location = new System.Drawing.Point(384, 69);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(82, 56);
            this.btnClose1.TabIndex = 18;
            this.btnClose1.Text = "Close";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
            // 
            // btnCheckStatus1
            // 
            this.btnCheckStatus1.Location = new System.Drawing.Point(384, 21);
            this.btnCheckStatus1.Name = "btnCheckStatus1";
            this.btnCheckStatus1.Size = new System.Drawing.Size(82, 35);
            this.btnCheckStatus1.TabIndex = 17;
            this.btnCheckStatus1.Text = "Refresh";
            this.btnCheckStatus1.UseVisualStyleBackColor = true;
            this.btnCheckStatus1.Click += new System.EventHandler(this.btnCheckStatus1_Click);
            // 
            // lblResult1
            // 
            this.lblResult1.BackColor = System.Drawing.Color.White;
            this.lblResult1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult1.Location = new System.Drawing.Point(12, 195);
            this.lblResult1.Name = "lblResult1";
            this.lblResult1.Size = new System.Drawing.Size(454, 52);
            this.lblResult1.TabIndex = 16;
            this.lblResult1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrigger1
            // 
            this.btnTrigger1.Location = new System.Drawing.Point(12, 140);
            this.btnTrigger1.Name = "btnTrigger1";
            this.btnTrigger1.Size = new System.Drawing.Size(454, 52);
            this.btnTrigger1.TabIndex = 15;
            this.btnTrigger1.Text = "Trigger";
            this.btnTrigger1.UseVisualStyleBackColor = true;
            this.btnTrigger1.Click += new System.EventHandler(this.btnTrigger1_Click);
            // 
            // lblStatus1
            // 
            this.lblStatus1.BackColor = System.Drawing.Color.Crimson;
            this.lblStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus1.Location = new System.Drawing.Point(12, 21);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(366, 35);
            this.lblStatus1.TabIndex = 14;
            this.lblStatus1.Text = "Status";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose2
            // 
            this.btnClose2.Location = new System.Drawing.Point(384, 69);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.Size = new System.Drawing.Size(82, 56);
            this.btnClose2.TabIndex = 19;
            this.btnClose2.Text = "Close";
            this.btnClose2.UseVisualStyleBackColor = true;
            this.btnClose2.Click += new System.EventHandler(this.btnClose2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose2);
            this.groupBox2.Controls.Add(this.btnCheckStatus2);
            this.groupBox2.Controls.Add(this.lblResult2);
            this.groupBox2.Controls.Add(this.btnTrigger2);
            this.groupBox2.Controls.Add(this.lblStatus2);
            this.groupBox2.Controls.Add(this.txtPort2);
            this.groupBox2.Controls.Add(this.txtIp2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnOpen2);
            this.groupBox2.Location = new System.Drawing.Point(493, 407);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 271);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MCR2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "IP";
            // 
            // txtPort1
            // 
            this.txtPort1.Enabled = false;
            this.txtPort1.Location = new System.Drawing.Point(62, 100);
            this.txtPort1.Name = "txtPort1";
            this.txtPort1.Size = new System.Drawing.Size(189, 25);
            this.txtPort1.TabIndex = 13;
            // 
            // txtIp1
            // 
            this.txtIp1.Enabled = false;
            this.txtIp1.Location = new System.Drawing.Point(62, 69);
            this.txtIp1.Name = "txtIp1";
            this.txtIp1.Size = new System.Drawing.Size(189, 25);
            this.txtIp1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose1);
            this.groupBox1.Controls.Add(this.btnCheckStatus1);
            this.groupBox1.Controls.Add(this.lblResult1);
            this.groupBox1.Controls.Add(this.btnTrigger1);
            this.groupBox1.Controls.Add(this.lblStatus1);
            this.groupBox1.Controls.Add(this.txtPort1);
            this.groupBox1.Controls.Add(this.txtIp1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOpen1);
            this.groupBox1.Location = new System.Drawing.Point(12, 407);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 271);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MCR1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Port";
            // 
            // btnOpen1
            // 
            this.btnOpen1.Location = new System.Drawing.Point(257, 69);
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(121, 56);
            this.btnOpen1.TabIndex = 9;
            this.btnOpen1.Text = "Open(Connect)";
            this.btnOpen1.UseVisualStyleBackColor = true;
            this.btnOpen1.Click += new System.EventHandler(this.btnOpen1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(1106, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(1019, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(71, 25);
            this.btnOpenFile.TabIndex = 22;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPath.Location = new System.Drawing.Point(218, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(795, 25);
            this.txtPath.TabIndex = 21;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 25);
            this.btnLoad.TabIndex = 18;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(6, 24);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 27;
            this.dgvList.Size = new System.Drawing.Size(1239, 319);
            this.dgvList.TabIndex = 37;
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.dgvList);
            this.gridBox.Enabled = false;
            this.gridBox.Location = new System.Drawing.Point(6, 43);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(1251, 358);
            this.gridBox.TabIndex = 20;
            this.gridBox.TabStop = false;
            this.gridBox.Text = "MCR";
            // 
            // txtTestCount
            // 
            this.txtTestCount.Location = new System.Drawing.Point(990, 428);
            this.txtTestCount.Name = "txtTestCount";
            this.txtTestCount.Size = new System.Drawing.Size(121, 25);
            this.txtTestCount.TabIndex = 25;
            this.txtTestCount.Text = "10";
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.BackColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(990, 459);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(121, 66);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Test";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // McrTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 714);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtTestCount);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.gridBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "McrTestWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "McrTestWindow";
            this.Load += new System.EventHandler(this.McrTestWindow_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.gridBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckStatus2;
        private System.Windows.Forms.Label lblResult2;
        private System.Windows.Forms.Button btnTrigger2;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.TextBox txtIp2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOpen2;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnCheckStatus1;
        private System.Windows.Forms.Label lblResult1;
        private System.Windows.Forms.Button btnTrigger1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Button btnClose2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPort1;
        private System.Windows.Forms.TextBox txtIp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpen1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.TextBox txtTestCount;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}