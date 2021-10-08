namespace EmWorks.App.Sample
{
    partial class MainWindow
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
            this.btnOpenMotion = new System.Windows.Forms.Button();
            this.btnOpenIO = new System.Windows.Forms.Button();
            this.btnOpenMCR = new System.Windows.Forms.Button();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.btnOpenNachiOut = new System.Windows.Forms.Button();
            this.btnOpenNachiIn = new System.Windows.Forms.Button();
            this.btnOpenSR5000 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenMotion
            // 
            this.btnOpenMotion.Location = new System.Drawing.Point(374, 12);
            this.btnOpenMotion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenMotion.Name = "btnOpenMotion";
            this.btnOpenMotion.Size = new System.Drawing.Size(106, 79);
            this.btnOpenMotion.TabIndex = 42;
            this.btnOpenMotion.Text = "Open Motion Test Dialog";
            this.btnOpenMotion.UseVisualStyleBackColor = true;
            this.btnOpenMotion.Click += new System.EventHandler(this.btnOpenMotion_Click);
            // 
            // btnOpenIO
            // 
            this.btnOpenIO.Location = new System.Drawing.Point(262, 12);
            this.btnOpenIO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenIO.Name = "btnOpenIO";
            this.btnOpenIO.Size = new System.Drawing.Size(106, 79);
            this.btnOpenIO.TabIndex = 41;
            this.btnOpenIO.Text = "Open IO Test Dialog";
            this.btnOpenIO.UseVisualStyleBackColor = true;
            this.btnOpenIO.Click += new System.EventHandler(this.btnOpenIO_Click);
            // 
            // btnOpenMCR
            // 
            this.btnOpenMCR.Location = new System.Drawing.Point(150, 12);
            this.btnOpenMCR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenMCR.Name = "btnOpenMCR";
            this.btnOpenMCR.Size = new System.Drawing.Size(106, 79);
            this.btnOpenMCR.TabIndex = 40;
            this.btnOpenMCR.Text = "Open MCR Test Dialog";
            this.btnOpenMCR.UseVisualStyleBackColor = true;
            this.btnOpenMCR.Click += new System.EventHandler(this.btnOpenMCR_Click);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Location = new System.Drawing.Point(22, 12);
            this.btnOpenConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(106, 79);
            this.btnOpenConfig.TabIndex = 43;
            this.btnOpenConfig.Text = "Open Config Test Dialog";
            this.btnOpenConfig.UseVisualStyleBackColor = true;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // btnOpenNachiOut
            // 
            this.btnOpenNachiOut.Location = new System.Drawing.Point(503, 12);
            this.btnOpenNachiOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenNachiOut.Name = "btnOpenNachiOut";
            this.btnOpenNachiOut.Size = new System.Drawing.Size(106, 79);
            this.btnOpenNachiOut.TabIndex = 44;
            this.btnOpenNachiOut.Text = "Open (Out) Nachi Test Dialog";
            this.btnOpenNachiOut.UseVisualStyleBackColor = true;
            this.btnOpenNachiOut.Click += new System.EventHandler(this.btnOpenNachiOut_Click);
            // 
            // btnOpenNachiIn
            // 
            this.btnOpenNachiIn.Location = new System.Drawing.Point(625, 12);
            this.btnOpenNachiIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenNachiIn.Name = "btnOpenNachiIn";
            this.btnOpenNachiIn.Size = new System.Drawing.Size(106, 79);
            this.btnOpenNachiIn.TabIndex = 45;
            this.btnOpenNachiIn.Text = "Open (In) Nachi Test Dialog";
            this.btnOpenNachiIn.UseVisualStyleBackColor = true;
            this.btnOpenNachiIn.Click += new System.EventHandler(this.btnOpenNachiIn_Click);
            // 
            // btnOpenSR5000
            // 
            this.btnOpenSR5000.Location = new System.Drawing.Point(817, 11);
            this.btnOpenSR5000.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenSR5000.Name = "btnOpenSR5000";
            this.btnOpenSR5000.Size = new System.Drawing.Size(106, 79);
            this.btnOpenSR5000.TabIndex = 46;
            this.btnOpenSR5000.Text = "Open SR5000 Test Dialog";
            this.btnOpenSR5000.UseVisualStyleBackColor = true;
            this.btnOpenSR5000.Click += new System.EventHandler(this.btnOpenSR5000_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(929, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 79);
            this.button1.TabIndex = 47;
            this.button1.Text = "Open Motion Test Dialog";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1145, 114);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOpenSR5000);
            this.Controls.Add(this.btnOpenNachiIn);
            this.Controls.Add(this.btnOpenNachiOut);
            this.Controls.Add(this.btnOpenConfig);
            this.Controls.Add(this.btnOpenMotion);
            this.Controls.Add(this.btnOpenIO);
            this.Controls.Add(this.btnOpenMCR);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sample";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenMotion;
        private System.Windows.Forms.Button btnOpenIO;
        private System.Windows.Forms.Button btnOpenMCR;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.Button btnOpenNachiOut;
        private System.Windows.Forms.Button btnOpenNachiIn;
        private System.Windows.Forms.Button btnOpenSR5000;
        private System.Windows.Forms.Button button1;
    }
}

