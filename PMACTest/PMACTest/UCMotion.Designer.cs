namespace PMACTest
{
    partial class UCMotion
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
            this.btnAbort = new System.Windows.Forms.Button();
            this.txtMosionIndex = new System.Windows.Forms.TextBox();
            this.txtAbsPos = new System.Windows.Forms.TextBox();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.btnRMovePlueX = new System.Windows.Forms.Button();
            this.btnRMoveMinueX = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMoveX = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStopX = new System.Windows.Forms.Button();
            this.lblMotorPosValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHomeX = new System.Windows.Forms.Button();
            this.btnJogMinusX = new System.Windows.Forms.Button();
            this.btnJogPlusX = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCont = new System.Windows.Forms.TextBox();
            this.btnServoOff = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGetPos = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(312, 140);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(76, 111);
            this.btnAbort.TabIndex = 66;
            this.btnAbort.Text = "Abort(Kill)";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // txtMosionIndex
            // 
            this.txtMosionIndex.Location = new System.Drawing.Point(81, 20);
            this.txtMosionIndex.Name = "txtMosionIndex";
            this.txtMosionIndex.Size = new System.Drawing.Size(100, 21);
            this.txtMosionIndex.TabIndex = 65;
            // 
            // txtAbsPos
            // 
            this.txtAbsPos.Location = new System.Drawing.Point(6, 357);
            this.txtAbsPos.Name = "txtAbsPos";
            this.txtAbsPos.Size = new System.Drawing.Size(77, 21);
            this.txtAbsPos.TabIndex = 64;
            // 
            // txtCnt
            // 
            this.txtCnt.Location = new System.Drawing.Point(3, 490);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.Size = new System.Drawing.Size(77, 21);
            this.txtCnt.TabIndex = 4;
            this.txtCnt.Text = "10";
            // 
            // btnRMovePlueX
            // 
            this.btnRMovePlueX.Location = new System.Drawing.Point(262, 450);
            this.btnRMovePlueX.Name = "btnRMovePlueX";
            this.btnRMovePlueX.Size = new System.Drawing.Size(123, 45);
            this.btnRMovePlueX.TabIndex = 63;
            this.btnRMovePlueX.Text = "Move(+)";
            this.btnRMovePlueX.UseVisualStyleBackColor = true;
            this.btnRMovePlueX.Click += new System.EventHandler(this.btnRMovePlueX_Click);
            // 
            // btnRMoveMinueX
            // 
            this.btnRMoveMinueX.Location = new System.Drawing.Point(133, 450);
            this.btnRMoveMinueX.Name = "btnRMoveMinueX";
            this.btnRMoveMinueX.Size = new System.Drawing.Size(123, 45);
            this.btnRMoveMinueX.TabIndex = 62;
            this.btnRMoveMinueX.Text = "Move(-)";
            this.btnRMoveMinueX.UseVisualStyleBackColor = true;
            this.btnRMoveMinueX.Click += new System.EventHandler(this.btnRMoveMinueX_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(1, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(384, 23);
            this.label7.TabIndex = 60;
            this.label7.Text = "Relative";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMoveX
            // 
            this.btnMoveX.Location = new System.Drawing.Point(89, 357);
            this.btnMoveX.Name = "btnMoveX";
            this.btnMoveX.Size = new System.Drawing.Size(123, 32);
            this.btnMoveX.TabIndex = 59;
            this.btnMoveX.Text = "Move";
            this.btnMoveX.UseVisualStyleBackColor = true;
            this.btnMoveX.Click += new System.EventHandler(this.btnMoveX_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(0, 473);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 57;
            this.label4.Text = "count";
            // 
            // btnStopX
            // 
            this.btnStopX.Location = new System.Drawing.Point(7, 197);
            this.btnStopX.Name = "btnStopX";
            this.btnStopX.Size = new System.Drawing.Size(201, 54);
            this.btnStopX.TabIndex = 56;
            this.btnStopX.Text = "Stop";
            this.btnStopX.UseVisualStyleBackColor = true;
            this.btnStopX.Click += new System.EventHandler(this.btnStopX_Click);
            // 
            // lblMotorPosValue
            // 
            this.lblMotorPosValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblMotorPosValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotorPosValue.Enabled = false;
            this.lblMotorPosValue.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMotorPosValue.Location = new System.Drawing.Point(81, 44);
            this.lblMotorPosValue.Name = "lblMotorPosValue";
            this.lblMotorPosValue.Size = new System.Drawing.Size(175, 33);
            this.lblMotorPosValue.TabIndex = 55;
            this.lblMotorPosValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 14);
            this.label2.TabIndex = 54;
            this.label2.Text = "Position";
            // 
            // btnHomeX
            // 
            this.btnHomeX.Location = new System.Drawing.Point(7, 80);
            this.btnHomeX.Name = "btnHomeX";
            this.btnHomeX.Size = new System.Drawing.Size(85, 54);
            this.btnHomeX.TabIndex = 53;
            this.btnHomeX.Text = "Home Search";
            this.btnHomeX.UseVisualStyleBackColor = true;
            // 
            // btnJogMinusX
            // 
            this.btnJogMinusX.Location = new System.Drawing.Point(7, 140);
            this.btnJogMinusX.Name = "btnJogMinusX";
            this.btnJogMinusX.Size = new System.Drawing.Size(96, 51);
            this.btnJogMinusX.TabIndex = 52;
            this.btnJogMinusX.Text = "Jog -";
            this.btnJogMinusX.UseVisualStyleBackColor = true;
            this.btnJogMinusX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinusX_MouseDown);
            this.btnJogMinusX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogMinusX_MouseUp);
            // 
            // btnJogPlusX
            // 
            this.btnJogPlusX.Location = new System.Drawing.Point(112, 140);
            this.btnJogPlusX.Name = "btnJogPlusX";
            this.btnJogPlusX.Size = new System.Drawing.Size(96, 51);
            this.btnJogPlusX.TabIndex = 51;
            this.btnJogPlusX.Text = "Jog +";
            this.btnJogPlusX.UseVisualStyleBackColor = true;
            this.btnJogPlusX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlusX_MouseDown);
            this.btnJogPlusX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogPlusX_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Index";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetSpeed);
            this.groupBox1.Controls.Add(this.txtSpeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnGetPos);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCont);
            this.groupBox1.Controls.Add(this.btnServoOff);
            this.groupBox1.Controls.Add(this.btnAbort);
            this.groupBox1.Controls.Add(this.txtMosionIndex);
            this.groupBox1.Controls.Add(this.txtAbsPos);
            this.groupBox1.Controls.Add(this.txtCnt);
            this.groupBox1.Controls.Add(this.btnRMovePlueX);
            this.groupBox1.Controls.Add(this.btnRMoveMinueX);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnMoveX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnStopX);
            this.groupBox1.Controls.Add(this.lblMotorPosValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnHomeX);
            this.groupBox1.Controls.Add(this.btnJogMinusX);
            this.groupBox1.Controls.Add(this.btnJogPlusX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 514);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MotionX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(1, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 14);
            this.label3.TabIndex = 69;
            this.label3.Text = "constant";
            // 
            // txtCont
            // 
            this.txtCont.Location = new System.Drawing.Point(3, 450);
            this.txtCont.Name = "txtCont";
            this.txtCont.Size = new System.Drawing.Size(77, 21);
            this.txtCont.TabIndex = 68;
            this.txtCont.Text = "400";
            // 
            // btnServoOff
            // 
            this.btnServoOff.Location = new System.Drawing.Point(230, 140);
            this.btnServoOff.Name = "btnServoOff";
            this.btnServoOff.Size = new System.Drawing.Size(76, 111);
            this.btnServoOff.TabIndex = 67;
            this.btnServoOff.Text = "Servo Off";
            this.btnServoOff.UseVisualStyleBackColor = true;
            this.btnServoOff.Click += new System.EventHandler(this.btnServoOff_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(4, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(384, 23);
            this.label5.TabIndex = 70;
            this.label5.Text = "Absolute";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetPos
            // 
            this.btnGetPos.Location = new System.Drawing.Point(262, 41);
            this.btnGetPos.Name = "btnGetPos";
            this.btnGetPos.Size = new System.Drawing.Size(132, 41);
            this.btnGetPos.TabIndex = 71;
            this.btnGetPos.Text = "Get Pos";
            this.btnGetPos.UseVisualStyleBackColor = true;
            this.btnGetPos.Click += new System.EventHandler(this.btnGetPos_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Font = new System.Drawing.Font("Gulim", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(6, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(384, 23);
            this.label6.TabIndex = 72;
            this.label6.Text = "Speed";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(3, 287);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(77, 21);
            this.txtSpeed.TabIndex = 73;
            this.txtSpeed.Text = "0";
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(86, 287);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(123, 32);
            this.btnSetSpeed.TabIndex = 74;
            this.btnSetSpeed.Text = "Set Speed";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // UCMotion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox1);
            this.Name = "UCMotion";
            this.Size = new System.Drawing.Size(407, 520);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        public System.Windows.Forms.TextBox txtMosionIndex;
        public System.Windows.Forms.TextBox txtAbsPos;
        public System.Windows.Forms.TextBox txtCnt;
        public System.Windows.Forms.Button btnRMovePlueX;
        public System.Windows.Forms.Button btnRMoveMinueX;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button btnMoveX;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnStopX;
        public System.Windows.Forms.Label lblMotorPosValue;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnHomeX;
        public System.Windows.Forms.Button btnJogMinusX;
        public System.Windows.Forms.Button btnJogPlusX;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnServoOff;
        public System.Windows.Forms.TextBox txtCont;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnGetPos;
        public System.Windows.Forms.Button btnSetSpeed;
        public System.Windows.Forms.TextBox txtSpeed;
        public System.Windows.Forms.Label label6;
    }
}
