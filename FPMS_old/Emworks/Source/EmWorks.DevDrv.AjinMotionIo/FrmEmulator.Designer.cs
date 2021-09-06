namespace EmWorks.DevDrv.AjinMotionIo
{
    partial class FrmEmulator
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
            this.dgvInputList = new System.Windows.Forms.DataGridView();
            this.lblInput = new System.Windows.Forms.Label();
            this.dgvOutputList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInputList
            // 
            this.dgvInputList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputList.Location = new System.Drawing.Point(12, 27);
            this.dgvInputList.Name = "dgvInputList";
            this.dgvInputList.RowTemplate.Height = 27;
            this.dgvInputList.Size = new System.Drawing.Size(419, 696);
            this.dgvInputList.TabIndex = 38;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(12, 9);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(38, 15);
            this.lblInput.TabIndex = 39;
            this.lblInput.Text = "Input";
            // 
            // dgvOutputList
            // 
            this.dgvOutputList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutputList.Location = new System.Drawing.Point(448, 27);
            this.dgvOutputList.Name = "dgvOutputList";
            this.dgvOutputList.RowTemplate.Height = 27;
            this.dgvOutputList.Size = new System.Drawing.Size(419, 696);
            this.dgvOutputList.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(451, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 41;
            this.label1.Text = "Output";
            // 
            // FrmEmulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 770);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOutputList);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.dgvInputList);
            this.MaximizeBox = false;
            this.Name = "FrmEmulator";
            this.Text = "FrmEmulator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutputList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInputList;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.DataGridView dgvOutputList;
        private System.Windows.Forms.Label label1;
    }
}