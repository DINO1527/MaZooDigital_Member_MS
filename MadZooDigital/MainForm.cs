using System;
using System.Windows.Forms;
using MadZooDigital.Forms;
using System.Drawing;

namespace MadZooDigital
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            // Optional: close any existing child first so only one is visible
            foreach (Form child in this.MdiChildren) child.Close();

            var f = new MemberForm
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized,
                  // Set the size and location
                Width = this.ClientSize.Width,        // Fill parent width
                Height = this.ClientSize.Height - 20, // Fill parent height minus 20px
                Location = new Point(0, 20)

            };
            f.Show();
        }


        private void btnPlans_Click(object sender, EventArgs e)
        {
            var f = new PlanForm();
            f.MdiParent = this;
            f.Show();
        }

        private void btnCoaching_Click(object sender, EventArgs e)
        {
            var f = new CoachingForm();
            f.MdiParent = this;
            f.Show();
        }

        private void btnMatches_Click(object sender, EventArgs e)
        {
            var f = new MatchForm();
            f.MdiParent = this;
            f.Show();
        }

        private void btnStatements_Click(object sender, EventArgs e)
        {
            var f = new StatementForm();
            f.MdiParent = this;
            f.Show();
        }
    }
}
