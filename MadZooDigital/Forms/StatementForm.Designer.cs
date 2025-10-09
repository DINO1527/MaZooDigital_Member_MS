namespace MadZooDigital.Forms
{
  
        partial class StatementForm
        {
            private System.ComponentModel.IContainer components = null;
            private System.Windows.Forms.ComboBox cmbMembers;
            private System.Windows.Forms.DateTimePicker dtpMonth;
            private System.Windows.Forms.Button btnLoad;
            private System.Windows.Forms.DataGridView dgvItems;
            private System.Windows.Forms.Label lblTotal;
            private System.Windows.Forms.Button btnPrint;
            private System.Windows.Forms.Button btnExportCsv;
            private System.Windows.Forms.Label lblMember;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null)) components.Dispose();
                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
                this.cmbMembers = new System.Windows.Forms.ComboBox();
                this.dtpMonth = new System.Windows.Forms.DateTimePicker();
                this.btnLoad = new System.Windows.Forms.Button();
                this.dgvItems = new System.Windows.Forms.DataGridView();
                this.lblTotal = new System.Windows.Forms.Label();
                this.btnPrint = new System.Windows.Forms.Button();
                this.btnExportCsv = new System.Windows.Forms.Button();
                this.lblMember = new System.Windows.Forms.Label();
                ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
                this.SuspendLayout();
                // 
                // cmbMembers
                // 
                this.cmbMembers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbMembers.FormattingEnabled = true;
                this.cmbMembers.Location = new System.Drawing.Point(14, 31);
                this.cmbMembers.Name = "cmbMembers";
                this.cmbMembers.Size = new System.Drawing.Size(320, 21);
                this.cmbMembers.TabIndex = 0;
                // 
                // dtpMonth
                // 
                this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.dtpMonth.CustomFormat = "MMMM yyyy"; // show month-year
                this.dtpMonth.ShowUpDown = true;
                this.dtpMonth.Location = new System.Drawing.Point(350, 31);
                this.dtpMonth.Name = "dtpMonth";
                this.dtpMonth.Size = new System.Drawing.Size(150, 20);
                this.dtpMonth.TabIndex = 1;
                // 
                // btnLoad
                // 
                this.btnLoad.Location = new System.Drawing.Point(516, 28);
                this.btnLoad.Name = "btnLoad";
                this.btnLoad.Size = new System.Drawing.Size(75, 24);
                this.btnLoad.TabIndex = 2;
                this.btnLoad.Text = "Load";
                this.btnLoad.UseVisualStyleBackColor = true;
                this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
                // 
                // dgvItems
                // 
                this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                  | System.Windows.Forms.AnchorStyles.Left)
                                  | System.Windows.Forms.AnchorStyles.Right)));
                this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvItems.Location = new System.Drawing.Point(14, 68);
                this.dgvItems.Name = "dgvItems";
                this.dgvItems.Size = new System.Drawing.Size(760, 360);
                this.dgvItems.TabIndex = 3;
                // 
                // lblTotal
                // 
                this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                this.lblTotal.Location = new System.Drawing.Point(14, 435);
                this.lblTotal.Name = "lblTotal";
                this.lblTotal.Size = new System.Drawing.Size(400, 23);
                this.lblTotal.TabIndex = 4;
                this.lblTotal.Text = "Total: Rs. 0.00";
                // 
                // btnPrint
                // 
                this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.btnPrint.Location = new System.Drawing.Point(610, 432);
                this.btnPrint.Name = "btnPrint";
                this.btnPrint.Size = new System.Drawing.Size(80, 26);
                this.btnPrint.TabIndex = 5;
                this.btnPrint.Text = "Print...";
                this.btnPrint.UseVisualStyleBackColor = true;
                this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
                // 
                // btnExportCsv
                // 
                this.btnExportCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.btnExportCsv.Location = new System.Drawing.Point(700, 432);
                this.btnExportCsv.Name = "btnExportCsv";
                this.btnExportCsv.Size = new System.Drawing.Size(74, 26);
                this.btnExportCsv.TabIndex = 6;
                this.btnExportCsv.Text = "Export CSV";
                this.btnExportCsv.UseVisualStyleBackColor = true;
                this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
                // 
                // lblMember
                // 
                this.lblMember.Location = new System.Drawing.Point(14, 9);
                this.lblMember.Name = "lblMember";
                this.lblMember.Size = new System.Drawing.Size(200, 16);
                this.lblMember.TabIndex = 7;
                this.lblMember.Text = "Select Member";
                // 
                // StatementForm
                // 
                this.ClientSize = new System.Drawing.Size(788, 470);
                this.Controls.Add(this.lblMember);
                this.Controls.Add(this.btnExportCsv);
                this.Controls.Add(this.btnPrint);
                this.Controls.Add(this.lblTotal);
                this.Controls.Add(this.dgvItems);
                this.Controls.Add(this.btnLoad);
                this.Controls.Add(this.dtpMonth);
                this.Controls.Add(this.cmbMembers);
                this.Name = "StatementForm";
                this.Text = "Member Monthly Statement";
                this.Load += new System.EventHandler(this.StatementForm_Load);
                ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
                this.ResumeLayout(false);
            }
        }
    }
