namespace LiveView.View
{
    partial class ChartUV
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
            this.pnlX = new System.Windows.Forms.Panel();
            this.pnlY = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlX
            // 
            this.pnlX.BackColor = System.Drawing.Color.Blue;
            this.pnlX.Location = new System.Drawing.Point(27, 416);
            this.pnlX.Name = "pnlX";
            this.pnlX.Size = new System.Drawing.Size(392, 2);
            this.pnlX.TabIndex = 5;
            // 
            // pnlY
            // 
            this.pnlY.BackColor = System.Drawing.Color.Red;
            this.pnlY.Location = new System.Drawing.Point(28, 24);
            this.pnlY.Name = "pnlY";
            this.pnlY.Size = new System.Drawing.Size(2, 392);
            this.pnlY.TabIndex = 4;
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::LiveView.Properties.Resources.ChromaticityUV;
            this.pictureBox.InitialImage = global::LiveView.Properties.Resources.ChromaticityUV;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(452, 444);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // ChartUV
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlX);
            this.Controls.Add(this.pnlY);
            this.Controls.Add(this.pictureBox);
            this.Name = "ChartUV";
            this.Size = new System.Drawing.Size(453, 445);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlX;
        private System.Windows.Forms.Panel pnlY;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}
