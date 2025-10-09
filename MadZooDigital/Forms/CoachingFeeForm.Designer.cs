namespace MadZooDigital.Forms
{
    partial class CoachingFeeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.ComboBox cboMember;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.NumericUpDown nudHours;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblFamilyID;
        private System.Windows.Forms.TextBox familyID;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMember = new System.Windows.Forms.Label();
            this.cboMember = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblHours = new System.Windows.Forms.Label();
            this.nudHours = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblFamilyID = new System.Windows.Forms.Label();
            this.familyID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMember
            // 
            this.lblMember.AutoSize = true;
            this.lblMember.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMember.Location = new System.Drawing.Point(30, 30);
            this.lblMember.Name = "lblMember";
            this.lblMember.Size = new System.Drawing.Size(89, 15);
            this.lblMember.TabIndex = 0;
            this.lblMember.Text = "Select Member:";
            // 
            // cboMember
            // 
            this.cboMember.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboMember.FormattingEnabled = true;
            this.cboMember.Location = new System.Drawing.Point(130, 27);
            this.cboMember.Name = "cboMember";
            this.cboMember.Size = new System.Drawing.Size(220, 23);
            this.cboMember.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDate.Location = new System.Drawing.Point(30, 75);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(68, 15);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Select Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDate.Location = new System.Drawing.Point(130, 72);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(220, 23);
            this.dtpDate.TabIndex = 5;
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHours.Location = new System.Drawing.Point(30, 120);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(91, 15);
            this.lblHours.TabIndex = 6;
            this.lblHours.Text = "Coaching Hour:";
            // 
            // nudHours
            // 
            this.nudHours.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudHours.Location = new System.Drawing.Point(130, 118);
            this.nudHours.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHours.Name = "nudHours";
            this.nudHours.Size = new System.Drawing.Size(80, 23);
            this.nudHours.TabIndex = 7;
            this.nudHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtotal.Location = new System.Drawing.Point(30, 165);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(54, 15);
            this.lblSubtotal.TabIndex = 8;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSubtotal.Location = new System.Drawing.Point(130, 162);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(120, 23);
            this.txtSubtotal.TabIndex = 9;
            this.txtSubtotal.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(508, 233);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 35);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "💾 Save Fee";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblFamilyID
            // 
            this.lblFamilyID.AutoSize = true;
            this.lblFamilyID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFamilyID.Location = new System.Drawing.Point(370, 30);
            this.lblFamilyID.Name = "lblFamilyID";
            this.lblFamilyID.Size = new System.Drawing.Size(59, 15);
            this.lblFamilyID.TabIndex = 2;
            this.lblFamilyID.Text = "Family ID:";
            // 
            // familyID
            // 
            this.familyID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.familyID.Location = new System.Drawing.Point(435, 27);
            this.familyID.Name = "familyID";
            this.familyID.ReadOnly = true;
            this.familyID.Size = new System.Drawing.Size(60, 23);
            this.familyID.TabIndex = 3;
            this.familyID.TabStop = false;
            // 
            // CoachingFeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(720, 317);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.nudHours);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.familyID);
            this.Controls.Add(this.lblFamilyID);
            this.Controls.Add(this.cboMember);
            this.Controls.Add(this.lblMember);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoachingFeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Coaching Fee";
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
