namespace OpenCvTest
{
    partial class frmDetail
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
            this.ucDisplayPixel2 = new OpenCvTest.UcDisplayPixel();
            this.ucDisplayPixel1 = new OpenCvTest.UcDisplayPixel();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ucDisplayPixel2
            // 
            this.ucDisplayPixel2.Location = new System.Drawing.Point(318, 24);
            this.ucDisplayPixel2.Name = "ucDisplayPixel2";
            this.ucDisplayPixel2.Size = new System.Drawing.Size(300, 300);
            this.ucDisplayPixel2.TabIndex = 62;
            // 
            // ucDisplayPixel1
            // 
            this.ucDisplayPixel1.Location = new System.Drawing.Point(12, 24);
            this.ucDisplayPixel1.Name = "ucDisplayPixel1";
            this.ucDisplayPixel1.Size = new System.Drawing.Size(300, 300);
            this.ucDisplayPixel1.TabIndex = 61;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 12);
            this.label10.TabIndex = 63;
            this.label10.Text = "Point1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 64;
            this.label1.Text = "Point2";
            // 
            // frmDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 338);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ucDisplayPixel2);
            this.Controls.Add(this.ucDisplayPixel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDetail";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcDisplayPixel ucDisplayPixel2;
        private UcDisplayPixel ucDisplayPixel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}