namespace OpenCvTest
{
    partial class UcDisplayPixel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlPixel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlPixel
            // 
            this.pnlPixel.BackColor = System.Drawing.Color.White;
            this.pnlPixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPixel.Location = new System.Drawing.Point(0, 0);
            this.pnlPixel.Name = "pnlPixel";
            this.pnlPixel.Size = new System.Drawing.Size(300, 300);
            this.pnlPixel.TabIndex = 0;
            // 
            // UcDisplayPixel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPixel);
            this.Name = "UcDisplayPixel";
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPixel;
    }
}
