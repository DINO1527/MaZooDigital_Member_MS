namespace MadZooDigital.Forms
{
    partial class MatchFeeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.ComboBox cboMember;
        private System.Windows.Forms.Label lblMatches;
        private System.Windows.Forms.NumericUpDown nudMatches;
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
            this.lblMatches = new System.Windows.Forms.Label();
            this.nudMatches = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatches)).BeginInit();
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
            // lblMatches
            // 
            this.lblMatches.AutoSize = true;
            this.lblMatches.Location = new System.Drawing.Point(20, 60);
            this.lblMatches.Name = "lblMatches";
            this.lblMatches.Size = new System.Drawing.Size(86, 13);
            this.lblMatches.TabIndex = 2;
            this.lblMatches.Text = "Matches Played:";
            // 
            // nudMatches
            // 
            this.nudMatches.Location = new System.Drawing.Point(120, 58);
            this.nudMatches.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.nudMatches.Name = "nudMatches";
            this.nudMatches.Size = new System.Drawing.Size(80, 20);
            this.nudMatches.TabIndex = 3;
            this.nudMatches.ValueChanged += new System.EventHandler(this.nudMatches_ValueChanged);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(20, 100);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(49, 13);
            this.lblSubtotal.TabIndex = 4;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(120, 97);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubtotal.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Fee";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MatchFeeForm
            // 
            this.ClientSize = new System.Drawing.Size(360, 200);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.nudMatches);
            this.Controls.Add(this.lblMatches);
            this.Controls.Add(this.cboMember);
            this.Controls.Add(this.lblMember);
            this.Name = "MatchFeeForm";
            this.Text = "Add Match Fee";
            ((System.ComponentModel.ISupportInitialize)(this.nudMatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
