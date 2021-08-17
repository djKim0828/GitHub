namespace PMACTest
{
    partial class frmMain
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
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnOpen = new System.Windows.Forms.Button();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeviceIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucMotionZ = new PMACTest.UCMotion();
            this.ucMotionY = new PMACTest.UCMotion();
            this.ucMotionX = new PMACTest.UCMotion();
            this.pnlBottom.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
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
            this.rcbOutput.Size = new System.Drawing.Size(1484, 132);
            this.rcbOutput.TabIndex = 47;
            this.rcbOutput.Text = "";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.rcbOutput);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 604);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1484, 132);
            this.pnlBottom.TabIndex = 63;
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
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 736);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1484, 25);
            this.statusStrip1.TabIndex = 62;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(258, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(117, 45);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlToolbar.Controls.Add(this.lblCount);
            this.pnlToolbar.Controls.Add(this.label2);
            this.pnlToolbar.Controls.Add(this.txtDeviceIndex);
            this.pnlToolbar.Controls.Add(this.label1);
            this.pnlToolbar.Controls.Add(this.btnOpen);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1484, 58);
            this.pnlToolbar.TabIndex = 61;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCount.Location = new System.Drawing.Point(454, 15);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(176, 33);
            this.lblCount.TabIndex = 69;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(397, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 14);
            this.label2.TabIndex = 68;
            this.label2.Text = "Count";
            // 
            // txtDeviceIndex
            // 
            this.txtDeviceIndex.Location = new System.Drawing.Point(141, 23);
            this.txtDeviceIndex.Name = "txtDeviceIndex";
            this.txtDeviceIndex.Size = new System.Drawing.Size(100, 21);
            this.txtDeviceIndex.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(30, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 66;
            this.label1.Text = "Device Index";
            // 
            // ucMotionZ
            // 
            this.ucMotionZ.Location = new System.Drawing.Point(830, 64);
            this.ucMotionZ.Name = "ucMotionZ";
            this.ucMotionZ.Size = new System.Drawing.Size(403, 534);
            this.ucMotionZ.TabIndex = 66;
            // 
            // ucMotionY
            // 
            this.ucMotionY.Location = new System.Drawing.Point(421, 64);
            this.ucMotionY.Name = "ucMotionY";
            this.ucMotionY.Size = new System.Drawing.Size(403, 534);
            this.ucMotionY.TabIndex = 65;
            // 
            // ucMotionX
            // 
            this.ucMotionX.Location = new System.Drawing.Point(12, 64);
            this.ucMotionX.Name = "ucMotionX";
            this.ucMotionX.Size = new System.Drawing.Size(403, 534);
            this.ucMotionX.TabIndex = 64;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1484, 761);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.ucMotionZ);
            this.Controls.Add(this.ucMotionY);
            this.Controls.Add(this.ucMotionX);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlBottom.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Panel pnlToolbar;
        public System.Windows.Forms.TextBox txtDeviceIndex;
        public System.Windows.Forms.Label label1;
        private UCMotion ucMotionZ;
        private UCMotion ucMotionY;
        private UCMotion ucMotionX;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCount;
    }
}