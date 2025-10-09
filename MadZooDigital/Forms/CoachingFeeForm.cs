using MadZooDigital.Data;
using MadZooDigital.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MadZooDigital.Forms
{
    public partial class CoachingFeeForm : Form
    {
        private readonly CoachingFeeRepository _repo = new CoachingFeeRepository();
        private const decimal HourlyRate = 1000m; // Fee per hour
        private List<Member> currentMembers = new List<Member>();

        public CoachingFeeForm()
        {
            InitializeComponent();
            cboMember.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboMember.AutoCompleteSource = AutoCompleteSource.CustomSource;

            cboMember.TextChanged += CboMember_TextChanged;
            cboMember.Leave += CboMember_Leave;
            cboMember.KeyDown += CboMember_KeyDown; // Enter key support
            nudHours.ValueChanged += NudHours_ValueChanged;
        }

        // 🔹 Fetch suggestions while typing
        private void CboMember_TextChanged(object sender, EventArgs e)
        {
            string search = cboMember.Text.Trim();
            if (string.IsNullOrEmpty(search)) return;

            currentMembers = _repo.SearchActiveMembers(search);

            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (Member m in currentMembers)
            {
                autoComplete.Add(m.FullName);
            }
            cboMember.AutoCompleteCustomSource = autoComplete;
        }

        // 🔹 Enter key selects the member
        private void CboMember_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectMemberByName();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 🔹 When focus leaves combo box
        private void CboMember_Leave(object sender, EventArgs e)
        {
            SelectMemberByName();
        }

        // 🔹 Match typed name to a member
        private void SelectMemberByName()
        {
            string typedName = cboMember.Text.Trim();
            Member selectedMember = null;
            foreach (Member m in currentMembers)
            {
                if (m.FullName.Equals(typedName, StringComparison.OrdinalIgnoreCase))
                {
                    selectedMember = m;
                    break;
                }
            }

            if (selectedMember != null)
            {
                // Fill Family ID and recalc subtotal
                familyID.Text = selectedMember.FamilyID.ToString();
                UpdateSubtotal();
            }
            else
            {
                familyID.Text = "";
                txtSubtotal.Text = "0";
            }
        }

        private void NudHours_ValueChanged(object sender, EventArgs e)
        {
            UpdateSubtotal();
        }

        private void UpdateSubtotal()
        {
            string typedName = cboMember.Text.Trim();
            Member selectedMember = null;
            foreach (Member m in currentMembers)
            {
                if (m.FullName.Equals(typedName, StringComparison.OrdinalIgnoreCase))
                {
                    selectedMember = m;
                    break;
                }
            }

            if (selectedMember != null)
            {
                int hoursSelected = Convert.ToInt32(nudHours.Value);
                int weeklyHours = _repo.GetWeeklyHours(selectedMember.MemberID, dtpDate.Value);

                if (hoursSelected + weeklyHours > 4)
                {
                    MessageBox.Show(
                        "This member has already booked " + weeklyHours + " hour(s) this week.\nMaximum 4 hours per week.",
                        "Hour Limit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    nudHours.Value = Math.Max(1, 4 - weeklyHours);
                    hoursSelected = Convert.ToInt32(nudHours.Value);
                }

                txtSubtotal.Text = (hoursSelected * HourlyRate).ToString("F0");
            }
            else
            {
                txtSubtotal.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string typedName = cboMember.Text.Trim();
            Member selectedMember = null;
            foreach (Member m in currentMembers)
            {
                if (m.FullName.Equals(typedName, StringComparison.OrdinalIgnoreCase))
                {
                    selectedMember = m;
                    break;
                }
            }

            if (selectedMember == null)
            {
                MessageBox.Show("Please select a valid member.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int subtotalValue = 0;
            if (!int.TryParse(txtSubtotal.Text, out subtotalValue) || subtotalValue <= 0)
            {
                MessageBox.Show("Invalid subtotal. Please check hours.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int familyIdValue = 0;
            int? fid = null;
            if (int.TryParse(familyID.Text, out familyIdValue))
            {
                fid = familyIdValue;
            }

            CoachingFee fee = new CoachingFee
            {
                MemberID = selectedMember.MemberID,
                FamilyID = fid,
                CoachingHours = Convert.ToInt32(nudHours.Value),
                SubTotal = subtotalValue,
                Date = dtpDate.Value.Date
            };

            try
            {
                _repo.Add(fee);
                MessageBox.Show("Coaching fee saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving coaching fee:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cboMember.Text = "";
            familyID.Text = "";
            nudHours.Value = 1;
            txtSubtotal.Text = "0";
            dtpDate.Value = DateTime.Today;
            currentMembers.Clear();
        }
    }
}
