namespace PMACTest
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
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnJogPlus = new System.Windows.Forms.Button();
            this.btnJogMinus = new System.Windows.Forms.Button();
            this.btnJogMinus2 = new System.Windows.Forms.Button();
            this.btnJogPlus2 = new System.Windows.Forms.Button();
            this.btnJogMinus3 = new System.Windows.Forms.Button();
            this.btnJogPlus3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(159, 45);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(0, 0);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(725, 160);
            this.rcbOutput.TabIndex = 39;
            this.rcbOutput.Text = "";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.rcbOutput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(729, 164);
            this.panel1.TabIndex = 40;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 474);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(729, 6);
            this.splitter1.TabIndex = 41;
            this.splitter1.TabStop = false;
            // 
            // btnJogPlus
            // 
            this.btnJogPlus.Location = new System.Drawing.Point(282, 133);
            this.btnJogPlus.Name = "btnJogPlus";
            this.btnJogPlus.Size = new System.Drawing.Size(159, 45);
            this.btnJogPlus.TabIndex = 42;
            this.btnJogPlus.Text = "Jog +";
            this.btnJogPlus.UseVisualStyleBackColor = true;
            this.btnJogPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus_MouseDown);
            this.btnJogPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus_MouseUp);
            // 
            // btnJogMinus
            // 
            this.btnJogMinus.Location = new System.Drawing.Point(447, 133);
            this.btnJogMinus.Name = "btnJogMinus";
            this.btnJogMinus.Size = new System.Drawing.Size(159, 45);
            this.btnJogMinus.TabIndex = 43;
            this.btnJogMinus.Text = "Jog -";
            this.btnJogMinus.UseVisualStyleBackColor = true;
            this.btnJogMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus_MouseDown);
            this.btnJogMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus_MouseUp);
            // 
            // btnJogMinus2
            // 
            this.btnJogMinus2.Location = new System.Drawing.Point(447, 184);
            this.btnJogMinus2.Name = "btnJogMinus2";
            this.btnJogMinus2.Size = new System.Drawing.Size(159, 45);
            this.btnJogMinus2.TabIndex = 45;
            this.btnJogMinus2.Text = "Jog -";
            this.btnJogMinus2.UseVisualStyleBackColor = true;
            this.btnJogMinus2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus2_MouseDown);
            this.btnJogMinus2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus2_MouseUp);
            // 
            // btnJogPlus2
            // 
            this.btnJogPlus2.Location = new System.Drawing.Point(282, 184);
            this.btnJogPlus2.Name = "btnJogPlus2";
            this.btnJogPlus2.Size = new System.Drawing.Size(159, 45);
            this.btnJogPlus2.TabIndex = 44;
            this.btnJogPlus2.Text = "Jog +";
            this.btnJogPlus2.UseVisualStyleBackColor = true;
            this.btnJogPlus2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus2_MouseDown);
            this.btnJogPlus2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus2_MouseUp);
            // 
            // btnJogMinus3
            // 
            this.btnJogMinus3.Location = new System.Drawing.Point(447, 235);
            this.btnJogMinus3.Name = "btnJogMinus3";
            this.btnJogMinus3.Size = new System.Drawing.Size(159, 45);
            this.btnJogMinus3.TabIndex = 47;
            this.btnJogMinus3.Text = "Jog -";
            this.btnJogMinus3.UseVisualStyleBackColor = true;
            this.btnJogMinus3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus3_MouseDown);
            this.btnJogMinus3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus3_MouseUp);
            // 
            // btnJogPlus3
            // 
            this.btnJogPlus3.Location = new System.Drawing.Point(282, 235);
            this.btnJogPlus3.Name = "btnJogPlus3";
            this.btnJogPlus3.Size = new System.Drawing.Size(159, 45);
            this.btnJogPlus3.TabIndex = 46;
            this.btnJogPlus3.Text = "Jog +";
            this.btnJogPlus3.UseVisualStyleBackColor = true;
            this.btnJogPlus3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus3_MouseDown);
            this.btnJogPlus3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus3_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 644);
            this.Controls.Add(this.btnJogMinus3);
            this.Controls.Add(this.btnJogPlus3);
            this.Controls.Add(this.btnJogMinus2);
            this.Controls.Add(this.btnJogPlus2);
            this.Controls.Add(this.btnJogMinus);
            this.Controls.Add(this.btnJogPlus);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnJogPlus;
        private System.Windows.Forms.Button btnJogMinus;
        private System.Windows.Forms.Button btnJogMinus2;
        private System.Windows.Forms.Button btnJogPlus2;
        private System.Windows.Forms.Button btnJogMinus3;
        private System.Windows.Forms.Button btnJogPlus3;
    }
}

