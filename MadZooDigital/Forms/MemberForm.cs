using System;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;

namespace MadZooDigital.Forms
{
    public partial class MemberForm : Form
    {
        private readonly MemberRepository _repo = new MemberRepository();

        public MemberForm()
        {
            InitializeComponent();
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            LoadMembers();
        }

        private void LoadMembers(string search = null)
        {
            var members = _repo.GetAll(search);
            dgvMembers.DataSource = members;
            dgvMembers.Columns["PlanID"].Visible = false;
            dgvMembers.Columns["Status"].Visible = false;
            dgvMembers.Columns["FamilyID"].Visible = false;
            dgvMembers.Columns["DOB"].Visible = true;
            dgvMembers.Columns["Phone"].Visible = true;
            dgvMembers.Columns["Email"].Visible = false;
            dgvMembers.Columns["Weight"].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMembers(txtSearch.Text.Trim());
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            var detailsForm = new MemberDetailsForm();
            detailsForm.ShowDialog();
            LoadMembers();
        }

        private void dgvMembers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int memberId = Convert.ToInt32(dgvMembers.Rows[e.RowIndex].Cells["MemberID"].Value);
                var detailsForm = new MemberDetailsForm(memberId);
                detailsForm.ShowDialog();
                LoadMembers();
            }
        }

    }
}
