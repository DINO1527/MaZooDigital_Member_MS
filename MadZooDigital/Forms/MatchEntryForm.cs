// Forms/MatchEntryForm.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;

namespace MadZooDigital.Forms
{
    public partial class MatchEntryForm : Form
    {
        private MatchFeeRepository _repo = new MatchFeeRepository();
        private List<Member> _members;

        private const decimal EntryFee = 1500m;

        public MatchEntryForm()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            _members = _repo.GetActiveMembers();

            comboBoxMember1.DataSource = new List<Member>(_members);
            comboBoxMember1.DisplayMember = "FullName";
            comboBoxMember1.ValueMember = "MemberID";

            comboBoxMember2.DataSource = new List<Member>(_members);
            comboBoxMember2.DisplayMember = "FullName";
            comboBoxMember2.ValueMember = "MemberID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBoxMember1.SelectedItem == null || comboBoxMember2.SelectedItem == null)
            {
                MessageBox.Show("Please select 2 members.");
                return;
            }

            Member m1 = (Member)comboBoxMember1.SelectedItem;
            Member m2 = (Member)comboBoxMember2.SelectedItem;

            int matches = Convert.ToInt32(numericUpDownMatches.Value);
            DateTime matchDate = dateTimePickerMatch.Value.Date;

            // Save both members
            Matchfee fee1 = new Matchfee
            {
                MemberID = m1.MemberID,
                FamilyID = m1.FamilyID,
                MatchesPlayed = matches,
                Date = matchDate,
                SubTotal = EntryFee* matches
            };
            _repo.SaveMatchFee(fee1);

            Matchfee fee2 = new Matchfee
            {
                MemberID = m2.MemberID,
                FamilyID = m2.FamilyID,
                MatchesPlayed = matches,
                Date = matchDate,
                SubTotal = EntryFee
            };
            _repo.SaveMatchFee(fee2);

            MessageBox.Show("Match entry saved successfully!");
            this.Close();
        }
    }
}
