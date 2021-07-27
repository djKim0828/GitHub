namespace BmpTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbnEdge = new System.Windows.Forms.Button();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.btnInvert = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnBPlus = new System.Windows.Forms.Button();
            this.btnBMinue = new System.Windows.Forms.Button();
            this.btnCMinue = new System.Windows.Forms.Button();
            this.btnCPlus = new System.Windows.Forms.Button();
            this.btnForm2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Get and Draw";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbnEdge
            // 
            this.tbnEdge.Location = new System.Drawing.Point(1287, 14);
            this.tbnEdge.Name = "tbnEdge";
            this.tbnEdge.Size = new System.Drawing.Size(135, 23);
            this.tbnEdge.TabIndex = 4;
            this.tbnEdge.Text = "Edge";
            this.tbnEdge.UseVisualStyleBackColor = true;
            this.tbnEdge.Click += new System.EventHandler(this.tbnEdge_Click);
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(1428, 14);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(100, 25);
            this.txtThreshold.TabIndex = 5;
            this.txtThreshold.Text = "1";
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(234, 11);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(135, 23);
            this.btnInvert.TabIndex = 6;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(786, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(768, 768);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // btnBPlus
            // 
            this.btnBPlus.Location = new System.Drawing.Point(375, 13);
            this.btnBPlus.Name = "btnBPlus";
            this.btnBPlus.Size = new System.Drawing.Size(135, 23);
            this.btnBPlus.TabIndex = 7;
            this.btnBPlus.Text = "Brightness +";
            this.btnBPlus.UseVisualStyleBackColor = true;
            this.btnBPlus.Click += new System.EventHandler(this.btnBPlus_Click);
            // 
            // btnBMinue
            // 
            this.btnBMinue.Location = new System.Drawing.Point(516, 13);
            this.btnBMinue.Name = "btnBMinue";
            this.btnBMinue.Size = new System.Drawing.Size(135, 23);
            this.btnBMinue.TabIndex = 8;
            this.btnBMinue.Text = "Brightness -";
            this.btnBMinue.UseVisualStyleBackColor = true;
            this.btnBMinue.Click += new System.EventHandler(this.btnBMinue_Click);
            // 
            // btnCMinue
            // 
            this.btnCMinue.Location = new System.Drawing.Point(798, 13);
            this.btnCMinue.Name = "btnCMinue";
            this.btnCMinue.Size = new System.Drawing.Size(135, 23);
            this.btnCMinue.TabIndex = 10;
            this.btnCMinue.Text = "Contrast -";
            this.btnCMinue.UseVisualStyleBackColor = true;
            this.btnCMinue.Click += new System.EventHandler(this.btnCMinue_Click);
            // 
            // btnCPlus
            // 
            this.btnCPlus.Location = new System.Drawing.Point(657, 13);
            this.btnCPlus.Name = "btnCPlus";
            this.btnCPlus.Size = new System.Drawing.Size(135, 23);
            this.btnCPlus.TabIndex = 9;
            this.btnCPlus.Text = "Contrast +";
            this.btnCPlus.UseVisualStyleBackColor = true;
            this.btnCPlus.Click += new System.EventHandler(this.btnCPlus_Click);
            // 
            // btnForm2
            // 
            this.btnForm2.Location = new System.Drawing.Point(1124, 4);
            this.btnForm2.Name = "btnForm2";
            this.btnForm2.Size = new System.Drawing.Size(145, 37);
            this.btnForm2.TabIndex = 11;
            this.btnForm2.Text = "Form2 Open";
            this.btnForm2.UseVisualStyleBackColor = true;
            this.btnForm2.Click += new System.EventHandler(this.btnForm2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::BmpTest.Properties.Resources._1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(768, 768);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1605, 848);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnForm2);
            this.Controls.Add(this.btnCMinue);
            this.Controls.Add(this.btnCPlus);
            this.Controls.Add(this.btnBMinue);
            this.Controls.Add(this.btnBPlus);
            this.Controls.Add(this.btnInvert);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.tbnEdge);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button tbnEdge;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnBPlus;
        private System.Windows.Forms.Button btnBMinue;
        private System.Windows.Forms.Button btnCMinue;
        private System.Windows.Forms.Button btnCPlus;
        private System.Windows.Forms.Button btnForm2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

