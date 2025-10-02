using System;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;

namespace MadZooDigital.Forms
{
    public partial class CoachingFeeForm : Form
    {
        private CoachingFeeRepository _repo = new CoachingFeeRepository();

        public CoachingFeeForm()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            string filter = txtSearch.Text.Trim();
            var members = _repo.SearchActiveMembers(filter);
            cboMember.DataSource = members;
            cboMember.DisplayMember = "FullName";
            txtSearch.Text = cboMember.DisplayMember.ToString();
            cboMember.ValueMember = "MemberID";
        }

        private void nudHours_ValueChanged(object sender, EventArgs e)
        {
            decimal subtotal = nudHours.Value * 1000m;
            txtSubtotal.Text = subtotal.ToString("C2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboMember.SelectedValue == null)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            CoachingFee fee = new CoachingFee();
            fee.MemberID = (int)cboMember.SelectedValue;
            fee.CoachingHours = (int)nudHours.Value;
            fee.FeePerHour = 1000m;
            fee.MonthYear = DateTime.Now.ToString("yyyy-MM");

            _repo.Add(fee);
            MessageBox.Show("Coaching Fee saved!");
        }
    }
}
