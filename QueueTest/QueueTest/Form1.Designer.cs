namespace QueueTest
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnListUp = new System.Windows.Forms.Button();
            this.btnListDown = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lsbRecipe = new System.Windows.Forms.ListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.ucStage1 = new QueueTest.UCStage();
            this.ucStage2 = new QueueTest.UCStage();
            this.ucStage3 = new QueueTest.UCStage();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(50, 450);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 46);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.Location = new System.Drawing.Point(150, 450);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 46);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button3.Location = new System.Drawing.Point(250, 450);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 46);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button4.Location = new System.Drawing.Point(350, 450);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 46);
            this.button4.TabIndex = 11;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(1000, 40);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(143, 46);
            this.btnInsert.TabIndex = 13;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1000, 94);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(143, 46);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnListUp
            // 
            this.btnListUp.Location = new System.Drawing.Point(776, 234);
            this.btnListUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnListUp.Name = "btnListUp";
            this.btnListUp.Size = new System.Drawing.Size(105, 46);
            this.btnListUp.TabIndex = 15;
            this.btnListUp.Text = "↑";
            this.btnListUp.UseVisualStyleBackColor = true;
            this.btnListUp.Click += new System.EventHandler(this.btnListUp_Click);
            // 
            // btnListDown
            // 
            this.btnListDown.Location = new System.Drawing.Point(888, 234);
            this.btnListDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnListDown.Name = "btnListDown";
            this.btnListDown.Size = new System.Drawing.Size(105, 46);
            this.btnListDown.TabIndex = 16;
            this.btnListDown.Text = "↓";
            this.btnListDown.UseVisualStyleBackColor = true;
            this.btnListDown.Click += new System.EventHandler(this.btnListDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1000, 179);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 101);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lsbRecipe
            // 
            this.lsbRecipe.FormattingEnabled = true;
            this.lsbRecipe.ItemHeight = 15;
            this.lsbRecipe.Location = new System.Drawing.Point(776, 40);
            this.lsbRecipe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lsbRecipe.Name = "lsbRecipe";
            this.lsbRecipe.Size = new System.Drawing.Size(217, 184);
            this.lsbRecipe.TabIndex = 18;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 15);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(143, 46);
            this.btnStart.TabIndex = 21;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(163, 15);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(143, 46);
            this.btnStop.TabIndex = 22;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(313, 15);
            this.btnPause.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(143, 46);
            this.btnPause.TabIndex = 23;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(463, 15);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(143, 46);
            this.btnRestart.TabIndex = 24;
            this.btnRestart.Text = "ReStart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.tbnRestart_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(612, 15);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(143, 46);
            this.btnAbort.TabIndex = 25;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // ucStage1
            // 
            this.ucStage1.BackColor = System.Drawing.Color.White;
            this.ucStage1.Location = new System.Drawing.Point(42, 101);
            this.ucStage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucStage1.Name = "ucStage1";
            this.ucStage1.Size = new System.Drawing.Size(213, 260);
            this.ucStage1.TabIndex = 7;
            this.ucStage1.Tag = "1";
            // 
            // ucStage2
            // 
            this.ucStage2.BackColor = System.Drawing.Color.White;
            this.ucStage2.Location = new System.Drawing.Point(287, 101);
            this.ucStage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucStage2.Name = "ucStage2";
            this.ucStage2.Size = new System.Drawing.Size(213, 260);
            this.ucStage2.TabIndex = 8;
            this.ucStage2.Tag = "2";
            // 
            // ucStage3
            // 
            this.ucStage3.BackColor = System.Drawing.Color.White;
            this.ucStage3.Location = new System.Drawing.Point(528, 101);
            this.ucStage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucStage3.Name = "ucStage3";
            this.ucStage3.Size = new System.Drawing.Size(213, 260);
            this.ucStage3.TabIndex = 9;
            this.ucStage3.Tag = "3";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1153, 602);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lsbRecipe);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnListDown);
            this.Controls.Add(this.btnListUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucStage1);
            this.Controls.Add(this.ucStage2);
            this.Controls.Add(this.ucStage3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private UCStage ucStage1;
        private UCStage ucStage2;
        private UCStage ucStage3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnListUp;
        private System.Windows.Forms.Button btnListDown;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lsbRecipe;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnAbort;
    }
}

