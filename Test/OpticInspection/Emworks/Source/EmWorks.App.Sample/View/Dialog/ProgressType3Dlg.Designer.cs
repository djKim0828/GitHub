namespace EmWorks.App.Sample
{
    partial class ProgressType3Dlg
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
            this.FunctionprogressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(12, 12);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(426, 214);
            this.rcbOutput.TabIndex = 105;
            this.rcbOutput.Text = "";
            // 
            // FunctionprogressBar
            // 
            this.FunctionprogressBar.Location = new System.Drawing.Point(12, 232);
            this.FunctionprogressBar.Name = "FunctionprogressBar";
            this.FunctionprogressBar.Size = new System.Drawing.Size(426, 23);
            this.FunctionprogressBar.TabIndex = 106;
            // 
            // ProgressType3Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 267);
            this.Controls.Add(this.FunctionprogressBar);
            this.Controls.Add(this.rcbOutput);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressType3Dlg";
            this.Text = "ProgressType3";
            this.Load += new System.EventHandler(this.ProgressType3Dlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.ProgressBar FunctionprogressBar;
    }
}