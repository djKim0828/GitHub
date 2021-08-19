namespace InterfaceTestor
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnRun = new System.Windows.Forms.Button();
            this.txtArg1 = new System.Windows.Forms.TextBox();
            this.txtArg2 = new System.Windows.Forms.TextBox();
            this.txtArg3 = new System.Windows.Forms.TextBox();
            this.rcbOutput = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnKill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(36, 31);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(166, 60);
            this.btnRun.TabIndex = 42;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtArg1
            // 
            this.txtArg1.Location = new System.Drawing.Point(36, 5);
            this.txtArg1.Name = "txtArg1";
            this.txtArg1.Size = new System.Drawing.Size(100, 21);
            this.txtArg1.TabIndex = 48;
            this.txtArg1.Text = "1";
            // 
            // txtArg2
            // 
            this.txtArg2.Location = new System.Drawing.Point(142, 5);
            this.txtArg2.Name = "txtArg2";
            this.txtArg2.Size = new System.Drawing.Size(100, 21);
            this.txtArg2.TabIndex = 49;
            this.txtArg2.Text = "2";
            // 
            // txtArg3
            // 
            this.txtArg3.Location = new System.Drawing.Point(248, 5);
            this.txtArg3.Name = "txtArg3";
            this.txtArg3.Size = new System.Drawing.Size(100, 21);
            this.txtArg3.TabIndex = 50;
            this.txtArg3.Text = "3";
            // 
            // rcbOutput
            // 
            this.rcbOutput.BackColor = System.Drawing.Color.Black;
            this.rcbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rcbOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rcbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.rcbOutput.Location = new System.Drawing.Point(0, 188);
            this.rcbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcbOutput.Name = "rcbOutput";
            this.rcbOutput.Size = new System.Drawing.Size(372, 200);
            this.rcbOutput.TabIndex = 52;
            this.rcbOutput.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 180);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(372, 8);
            this.splitter1.TabIndex = 53;
            this.splitter1.TabStop = false;
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(36, 95);
            this.btnKill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(166, 60);
            this.btnKill.TabIndex = 54;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 388);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.rcbOutput);
            this.Controls.Add(this.txtArg3);
            this.Controls.Add(this.txtArg2);
            this.Controls.Add(this.txtArg1);
            this.Controls.Add(this.btnRun);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtArg1;
        private System.Windows.Forms.TextBox txtArg2;
        private System.Windows.Forms.TextBox txtArg3;
        private System.Windows.Forms.RichTextBox rcbOutput;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnKill;
    }
}

