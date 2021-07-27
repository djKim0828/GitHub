namespace FFTTest
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.ImageInput = new System.Windows.Forms.PictureBox();
            this.txtScalepercentage = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtToolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.FourierMag = new System.Windows.Forms.PictureBox();
            this.FourierPhase = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.InvFourier = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageInput)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FourierMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvFourier)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(679, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(140, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ImageInput
            // 
            this.ImageInput.Location = new System.Drawing.Point(12, 41);
            this.ImageInput.Name = "ImageInput";
            this.ImageInput.Size = new System.Drawing.Size(256, 256);
            this.ImageInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageInput.TabIndex = 2;
            this.ImageInput.TabStop = false;
            this.ImageInput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageInput_MouseMove);
            // 
            // txtScalepercentage
            // 
            this.txtScalepercentage.Location = new System.Drawing.Point(93, 13);
            this.txtScalepercentage.Name = "txtScalepercentage";
            this.txtScalepercentage.Size = new System.Drawing.Size(100, 25);
            this.txtScalepercentage.TabIndex = 3;
            this.txtScalepercentage.Text = "50";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtToolStripStatusLabel2,
            this.txtToolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 610);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(831, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtToolStripStatusLabel2
            // 
            this.txtToolStripStatusLabel2.Name = "txtToolStripStatusLabel2";
            this.txtToolStripStatusLabel2.Size = new System.Drawing.Size(115, 20);
            this.txtToolStripStatusLabel2.Text = "Width X Height";
            // 
            // txtToolStripStatusLabel4
            // 
            this.txtToolStripStatusLabel4.Name = "txtToolStripStatusLabel4";
            this.txtToolStripStatusLabel4.Size = new System.Drawing.Size(115, 20);
            this.txtToolStripStatusLabel4.Text = "Width X Height";
            // 
            // FourierMag
            // 
            this.FourierMag.Location = new System.Drawing.Point(12, 312);
            this.FourierMag.Name = "FourierMag";
            this.FourierMag.Size = new System.Drawing.Size(256, 256);
            this.FourierMag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FourierMag.TabIndex = 5;
            this.FourierMag.TabStop = false;
            // 
            // FourierPhase
            // 
            this.FourierPhase.Location = new System.Drawing.Point(274, 312);
            this.FourierPhase.Name = "FourierPhase";
            this.FourierPhase.Size = new System.Drawing.Size(256, 256);
            this.FourierPhase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FourierPhase.TabIndex = 6;
            this.FourierPhase.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(679, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Inverse FFT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InvFourier
            // 
            this.InvFourier.Location = new System.Drawing.Point(536, 312);
            this.InvFourier.Name = "InvFourier";
            this.InvFourier.Size = new System.Drawing.Size(256, 256);
            this.InvFourier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InvFourier.TabIndex = 8;
            this.InvFourier.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 635);
            this.Controls.Add(this.InvFourier);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FourierPhase);
            this.Controls.Add(this.FourierMag);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtScalepercentage);
            this.Controls.Add(this.ImageInput);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageInput)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FourierMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvFourier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox ImageInput;
        private System.Windows.Forms.TextBox txtScalepercentage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtToolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel txtToolStripStatusLabel4;
        private System.Windows.Forms.PictureBox FourierMag;
        private System.Windows.Forms.PictureBox FourierPhase;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox InvFourier;
    }
}

