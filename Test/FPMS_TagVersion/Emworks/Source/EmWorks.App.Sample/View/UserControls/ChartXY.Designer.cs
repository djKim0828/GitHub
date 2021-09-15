namespace EmWorks.App.Sample
{
    partial class ChartXY
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
            this.pnlY = new System.Windows.Forms.Panel();
            this.pnlX = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlY
            // 
            this.pnlY.BackColor = System.Drawing.Color.Red;
            this.pnlY.Location = new System.Drawing.Point(29, 22);
            this.pnlY.Name = "pnlY";
            this.pnlY.Size = new System.Drawing.Size(2, 392);
            this.pnlY.TabIndex = 1;
            // 
            // pnlX
            // 
            this.pnlX.BackColor = System.Drawing.Color.Blue;
            this.pnlX.Location = new System.Drawing.Point(27, 414);
            this.pnlX.Name = "pnlX";
            this.pnlX.Size = new System.Drawing.Size(352, 2);
            this.pnlX.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::EmWorks.App.Sample.Properties.Resources.ChromaticityXY;
            this.pictureBox.InitialImage = global::EmWorks.App.Sample.Properties.Resources.ChromaticityXY;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(402, 443);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // ChartXY
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlX);
            this.Controls.Add(this.pnlY);
            this.Controls.Add(this.pictureBox);
            this.Name = "ChartXY";
            this.Size = new System.Drawing.Size(400, 444);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel pnlY;
        private System.Windows.Forms.Panel pnlX;
    }
}
