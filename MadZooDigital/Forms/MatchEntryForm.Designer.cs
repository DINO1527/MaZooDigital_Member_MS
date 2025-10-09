// MatchEntryForm.Designer.cs
namespace MadZooDigital.Forms
{
    partial class MatchEntryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxMember1;
        private System.Windows.Forms.ComboBox comboBoxMember2;
        private System.Windows.Forms.DateTimePicker dateTimePickerMatch;
        private System.Windows.Forms.NumericUpDown numericUpDownMatches;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        private void InitializeComponent()
        {
            this.comboBoxMember1 = new System.Windows.Forms.ComboBox();
            this.comboBoxMember2 = new System.Windows.Forms.ComboBox();
            this.dateTimePickerMatch = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownMatches = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMatches)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxMember1
            // 
            this.comboBoxMember1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMember1.Location = new System.Drawing.Point(150, 30);
            this.comboBoxMember1.Name = "comboBoxMember1";
            this.comboBoxMember1.Size = new System.Drawing.Size(200, 21);
            // 
            // comboBoxMember2
            // 
            this.comboBoxMember2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMember2.Location = new System.Drawing.Point(150, 70);
            this.comboBoxMember2.Name = "comboBoxMember2";
            this.comboBoxMember2.Size = new System.Drawing.Size(200, 21);
            // 
            // dateTimePickerMatch
            // 
            this.dateTimePickerMatch.Location = new System.Drawing.Point(150, 110);
            this.dateTimePickerMatch.Name = "dateTimePickerMatch";
            this.dateTimePickerMatch.Size = new System.Drawing.Size(200, 20);
            // 
            // numericUpDownMatches
            // 
            this.numericUpDownMatches.Location = new System.Drawing.Point(150, 150);
            this.numericUpDownMatches.Minimum = 1;
            this.numericUpDownMatches.Maximum = 100;
            this.numericUpDownMatches.Value = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Labels
            // 
            this.label1.Text = "Member 1:";
            this.label1.Location = new System.Drawing.Point(50, 30);
            this.label2.Text = "Member 2:";
            this.label2.Location = new System.Drawing.Point(50, 70);
            this.label3.Text = "Match Date:";
            this.label3.Location = new System.Drawing.Point(50, 110);
            this.label4.Text = "Matches Played:";
            this.label4.Location = new System.Drawing.Point(50, 150);
            // 
            // MatchEntryForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.comboBoxMember1);
            this.Controls.Add(this.comboBoxMember2);
            this.Controls.Add(this.dateTimePickerMatch);
            this.Controls.Add(this.numericUpDownMatches);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Text = "Match Entry Form";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMatches)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
