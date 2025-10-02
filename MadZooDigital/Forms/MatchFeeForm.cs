using System;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;

namespace MadZooDigital.Forms
{
    public partial class MatchFeeForm : Form
    {
        private MatchFeeRepository _repo = new MatchFeeRepository();

        public MatchFeeForm()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            // Example only: in real app, load from DB
            cboMember.Items.Add(new { Text = "Member 1", Value = 1 });
            cboMember.Items.Add(new { Text = "Member 2", Value = 2 });
            cboMember.DisplayMember = "Text";
            cboMember.ValueMember = "Value";
        }

        private void nudMatches_ValueChanged(object sender, EventArgs e)
        {
            decimal subtotal = nudMatches.Value * 1500m;
            txtSubtotal.Text = subtotal.ToString("C2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboMember.SelectedValue == null)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            Matchfee fee = new Matchfee();
            fee.MemberID = (int)cboMember.SelectedValue;
            fee.MatchesPlayed = (int)nudMatches.Value;
            fee.FeePerMatch = 1500m;
            fee.MonthYear = DateTime.Now.ToString("yyyy-MM");

            _repo.Add(fee);
            MessageBox.Show("Match Fee saved!");
        }
    }
}
