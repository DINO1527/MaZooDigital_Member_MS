namespace MadZooDigital.Forms
{
    partial class CoachingFeeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.ComboBox cboMember;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.NumericUpDown nudHours;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Button btnSave;

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
            this.lblHours = new System.Windows.Forms.Label();
            this.nudHours = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMember
            // 
            this.lblMember.AutoSize = true;
            this.lblMember.Location = new System.Drawing.Point(20, 20);
            this.lblMember.Name = "lblMember";
            this.lblMember.Size = new System.Drawing.Size(78, 13);
            this.lblMember.TabIndex = 0;
            this.lblMember.Text = "Select Member";
            // 
            // cboMember
            // 
            this.cboMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMember.FormattingEnabled = true;
            this.cboMember.Location = new System.Drawing.Point(120, 17);
            this.cboMember.Name = "cboMember";
            this.cboMember.Size = new System.Drawing.Size(200, 21);
            this.cboMember.TabIndex = 1;
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(20, 106);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(74, 13);
            this.lblHours.TabIndex = 2;
            this.lblHours.Text = "Coaching Hrs:";
            // 
            // nudHours
            // 
            this.nudHours.Location = new System.Drawing.Point(120, 104);
            this.nudHours.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudHours.Name = "nudHours";
            this.nudHours.Size = new System.Drawing.Size(80, 20);
            this.nudHours.TabIndex = 3;
            this.nudHours.ValueChanged += new System.EventHandler(this.nudHours_ValueChanged);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(20, 146);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(49, 13);
            this.lblSubtotal.TabIndex = 4;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(120, 143);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubtotal.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(385, 243);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Fee";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 61);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(153, 20);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // CoachingFeeForm
            // 
            this.ClientSize = new System.Drawing.Size(492, 298);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.nudHours);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.cboMember);
            this.Controls.Add(this.lblMember);
            this.Name = "CoachingFeeForm";
            this.Text = "Add Coaching Fee";
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
