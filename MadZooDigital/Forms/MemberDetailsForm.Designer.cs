namespace MadZooDigital.Forms
{
    partial class MemberDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.ComboBox cmbPlanType;
        private System.Windows.Forms.ComboBox cmbPlanCategory;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.ComboBox cmbPersonMode;
        private System.Windows.Forms.ComboBox cmbPersonType;
        private System.Windows.Forms.Button btnAddFamilyMember;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblDOB = new System.Windows.Forms.Label();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.cmbPlanType = new System.Windows.Forms.ComboBox();
            this.cmbPlanCategory = new System.Windows.Forms.ComboBox();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.cmbPersonMode = new System.Windows.Forms.ComboBox();
            this.cmbPersonType = new System.Windows.Forms.ComboBox();
            this.btnAddFamilyMember = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.removeBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFullName
            // 
            this.lblFullName.Location = new System.Drawing.Point(184, 12);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(100, 18);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(187, 33);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(200, 20);
            this.txtFullName.TabIndex = 1;
            this.txtFullName.TextChanged += new System.EventHandler(this.txtFullName_leave);
            // 
            // lblDOB
            // 
            this.lblDOB.Location = new System.Drawing.Point(9, 66);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(100, 17);
            this.lblDOB.TabIndex = 2;
            this.lblDOB.Text = "DOB";
            // 
            // dtpDOB
            // 
            this.dtpDOB.Location = new System.Drawing.Point(12, 86);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(120, 20);
            this.dtpDOB.TabIndex = 3;
            this.dtpDOB.ValueChanged += new System.EventHandler(this.dtpDOB_ValueChanged);
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(202, 66);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(45, 17);
            this.lblAge.TabIndex = 4;
            this.lblAge.Text = "Age";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(205, 86);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(50, 20);
            this.txtAge.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(357, 66);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(100, 17);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(360, 86);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 20);
            this.txtPhone.TabIndex = 7;
            // 
            // cmbPlanType
            // 
            this.cmbPlanType.Location = new System.Drawing.Point(45, 12);
            this.cmbPlanType.Name = "cmbPlanType";
            this.cmbPlanType.Size = new System.Drawing.Size(121, 21);
            this.cmbPlanType.TabIndex = 8;
            this.cmbPlanType.SelectedIndexChanged += new System.EventHandler(this.cmbPlanType_SelectedIndexChanged);
            // 
            // cmbPlanCategory
            // 
            this.cmbPlanCategory.Location = new System.Drawing.Point(204, 12);
            this.cmbPlanCategory.Name = "cmbPlanCategory";
            this.cmbPlanCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbPlanCategory.TabIndex = 9;
            this.cmbPlanCategory.SelectedIndexChanged += new System.EventHandler(this.cmbPlanCategory_SelectedIndexChanged);
            // 
            // txtFee
            // 
            this.txtFee.Location = new System.Drawing.Point(45, 48);
            this.txtFee.Name = "txtFee";
            this.txtFee.Size = new System.Drawing.Size(100, 20);
            this.txtFee.TabIndex = 10;
            // 
            // cmbPersonMode
            // 
            this.cmbPersonMode.Location = new System.Drawing.Point(204, 48);
            this.cmbPersonMode.Name = "cmbPersonMode";
            this.cmbPersonMode.Size = new System.Drawing.Size(228, 21);
            this.cmbPersonMode.TabIndex = 11;
            this.cmbPersonMode.Visible = false;
            // 
            // cmbPersonType
            // 
            this.cmbPersonType.Location = new System.Drawing.Point(11, 32);
            this.cmbPersonType.Name = "cmbPersonType";
            this.cmbPersonType.Size = new System.Drawing.Size(121, 21);
            this.cmbPersonType.TabIndex = 12;
            this.cmbPersonType.SelectedIndexChanged += new System.EventHandler(this.cmbPersonType_SelectedIndexChanged);
            // 
            // btnAddFamilyMember
            // 
            this.btnAddFamilyMember.Location = new System.Drawing.Point(381, 127);
            this.btnAddFamilyMember.Name = "btnAddFamilyMember";
            this.btnAddFamilyMember.Size = new System.Drawing.Size(122, 23);
            this.btnAddFamilyMember.TabIndex = 13;
            this.btnAddFamilyMember.Text = "Add Family Member";
            this.btnAddFamilyMember.Click += new System.EventHandler(this.btnAddFamilyMember_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(90, 460);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(170, 460);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtWeight);
            this.panel1.Controls.Add(this.lblWeight);
            this.panel1.Controls.Add(this.removeBtn);
            this.panel1.Controls.Add(this.cmbPersonType);
            this.panel1.Controls.Add(this.lblFullName);
            this.panel1.Controls.Add(this.txtFullName);
            this.panel1.Controls.Add(this.btnAddFamilyMember);
            this.panel1.Controls.Add(this.lblDOB);
            this.panel1.Controls.Add(this.txtPhone);
            this.panel1.Controls.Add(this.dtpDOB);
            this.panel1.Controls.Add(this.lblPhone);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.txtAge);
            this.panel1.Location = new System.Drawing.Point(31, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 162);
            this.panel1.TabIndex = 19;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(279, 84);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(53, 20);
            this.txtWeight.TabIndex = 16;
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(286, 68);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(35, 13);
            this.lblWeight.TabIndex = 15;
            this.lblWeight.Text = "Wight";
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(424, 12);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(79, 29);
            this.removeBtn.TabIndex = 14;
            this.removeBtn.Text = "remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Visible = false;
            this.removeBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(34, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 355);
            this.panel2.TabIndex = 20;
            // 
            // MemberDetailsForm
            // 
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmbPlanType);
            this.Controls.Add(this.cmbPlanCategory);
            this.Controls.Add(this.txtFee);
            this.Controls.Add(this.cmbPersonMode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Name = "MemberDetailsForm";
            this.Text = "Member Details";
            this.Load += new System.EventHandler(this.MemberDetailsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblWeight;
    }
}
