namespace MadZooDigital
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button btnMembers;
        private System.Windows.Forms.Button btnPlans;
        private System.Windows.Forms.Button btnCoaching;
        private System.Windows.Forms.Button btnMatches;
        private System.Windows.Forms.Button btnStatements;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.navPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.btnMembers = new System.Windows.Forms.Button();
            this.btnPlans = new System.Windows.Forms.Button();
            this.btnCoaching = new System.Windows.Forms.Button();
            this.btnMatches = new System.Windows.Forms.Button();
            this.btnStatements = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // navPanel
            // 
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.navPanel.Width = 200;
            this.navPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.navPanel.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.navPanel.Controls.Add(this.btnStatements);
            this.navPanel.Controls.Add(this.btnMatches);
            this.navPanel.Controls.Add(this.btnCoaching);
            this.navPanel.Controls.Add(this.btnPlans);
            this.navPanel.Controls.Add(this.btnMembers);
            this.navPanel.Controls.Add(this.titleLabel);
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Text = "MadZoo Digital";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.Height = 60;
            this.titleLabel.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            // 
            // Buttons (basic properties – detailed style applied in StyleButton)
            // 
            this.btnMembers.Text = "Members";
            this.btnPlans.Text = "Plans";
            this.btnCoaching.Text = "Coaching";
            this.btnMatches.Text = "Matches";
            this.btnStatements.Text = "Statements";

            // hook up events
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            this.btnPlans.Click += new System.EventHandler(this.btnPlans_Click);
            this.btnCoaching.Click += new System.EventHandler(this.btnCoaching_Click);
            this.btnMatches.Click += new System.EventHandler(this.btnMatches_Click);
            this.btnStatements.Click += new System.EventHandler(this.btnStatements_Click);

            // 
            // MainForm
            // 
            this.IsMdiContainer = true;
            this.Text = "MadZoo Digital - Admin";
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.navPanel);
            this.ResumeLayout(false);

            // Apply consistent button style after controls are created
            ApplyNavButtonStyle();
        }

        /// <summary>
        /// Give all nav buttons a flat modern style
        /// </summary>
        private void ApplyNavButtonStyle()
        {
            System.Drawing.Font navFont = new System.Drawing.Font("Segoe UI", 11F);
            System.Drawing.Color normalColor = System.Drawing.Color.FromArgb(63, 63, 70);
            System.Drawing.Color hoverColor = System.Drawing.Color.FromArgb(0, 122, 204);

            System.Windows.Forms.Button[] buttons = 
                { btnMembers, btnPlans, btnCoaching, btnMatches, btnStatements };

            foreach (var btn in buttons)
            {
                btn.Dock = System.Windows.Forms.DockStyle.Top;
                btn.Height = 45;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = normalColor;
                btn.ForeColor = System.Drawing.Color.White;
                btn.Font = navFont;
                btn.FlatAppearance.MouseOverBackColor = hoverColor;
            }
        }
    }
}
