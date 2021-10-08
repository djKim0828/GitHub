namespace EmWorks.App.Sample.View.Window
{
    partial class AjinIoTestWinodw
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnCheckStatus1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.dgvOutput = new System.Windows.Forms.DataGridView();
            this.dgvInput = new System.Windows.Forms.DataGridView();
            this.chkSim = new System.Windows.Forms.CheckBox();
            this.gridBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(1106, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(1019, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(71, 25);
            this.btnOpenFile.TabIndex = 27;
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
            this.txtPath.TabIndex = 26;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 25);
            this.btnLoad.TabIndex = 23;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.dgvList);
            this.gridBox.Enabled = false;
            this.gridBox.Location = new System.Drawing.Point(6, 43);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(1462, 358);
            this.gridBox.TabIndex = 25;
            this.gridBox.TabStop = false;
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(6, 24);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 27;
            this.dgvList.Size = new System.Drawing.Size(1450, 319);
            this.dgvList.TabIndex = 37;
            // 
            // btnClose1
            // 
            this.btnClose1.Location = new System.Drawing.Point(432, 407);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(82, 56);
            this.btnClose1.TabIndex = 31;
            this.btnClose1.Text = "Close";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
            // 
            // btnCheckStatus1
            // 
            this.btnCheckStatus1.Location = new System.Drawing.Point(206, 407);
            this.btnCheckStatus1.Name = "btnCheckStatus1";
            this.btnCheckStatus1.Size = new System.Drawing.Size(82, 35);
            this.btnCheckStatus1.TabIndex = 30;
            this.btnCheckStatus1.Text = "Refresh";
            this.btnCheckStatus1.UseVisualStyleBackColor = true;
            this.btnCheckStatus1.Click += new System.EventHandler(this.btnCheckStatus1_Click);
            // 
            // lblStatus1
            // 
            this.lblStatus1.BackColor = System.Drawing.Color.Crimson;
            this.lblStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus1.Location = new System.Drawing.Point(12, 407);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(188, 35);
            this.lblStatus1.TabIndex = 29;
            this.lblStatus1.Text = "Status";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(305, 407);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(121, 56);
            this.btnOpen.TabIndex = 28;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // dgvOutput
            // 
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Location = new System.Drawing.Point(743, 486);
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.RowTemplate.Height = 27;
            this.dgvOutput.Size = new System.Drawing.Size(725, 308);
            this.dgvOutput.TabIndex = 41;
            // 
            // dgvInput
            // 
            this.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInput.Location = new System.Drawing.Point(12, 486);
            this.dgvInput.Name = "dgvInput";
            this.dgvInput.RowTemplate.Height = 27;
            this.dgvInput.Size = new System.Drawing.Size(725, 308);
            this.dgvInput.TabIndex = 40;
            // 
            // chkSim
            // 
            this.chkSim.AutoSize = true;
            this.chkSim.Location = new System.Drawing.Point(1232, 16);
            this.chkSim.Name = "chkSim";
            this.chkSim.Size = new System.Drawing.Size(96, 19);
            this.chkSim.TabIndex = 42;
            this.chkSim.Text = "Simulation";
            this.chkSim.UseVisualStyleBackColor = true;
            // 
            // AjinIoTestWinodw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 979);
            this.Controls.Add(this.chkSim);
            this.Controls.Add(this.dgvOutput);
            this.Controls.Add(this.dgvInput);
            this.Controls.Add(this.btnClose1);
            this.Controls.Add(this.btnCheckStatus1);
            this.Controls.Add(this.lblStatus1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.gridBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjinIoTestWinodw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AjinIoTestWinodw";
            this.Load += new System.EventHandler(this.AjinIoTestWinodw_Load);
            this.gridBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnCheckStatus1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.DataGridView dgvOutput;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.CheckBox chkSim;
    }
}