using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;

namespace MadZooDigital.Forms
{
    public partial class MemberDetailsForm : Form
    {
        // ──────────────────────────────────────────────────────────────
        // ░░  Fields
        // ──────────────────────────────────────────────────────────────
        private readonly MemberRepository _planRepo = new MemberRepository();
        private int _editingMemberId = 0;
        private int _familyId = 0;
        private bool _isEditMode = false;
        private int _selectedPlanId = 0;
        private int _Plandurition = 0;

        // ──────────────────────────────────────────────────────────────
        // ░░  Constructor
        // ──────────────────────────────────────────────────────────────
        public MemberDetailsForm(int memberId = 0)
        {
            InitializeComponent();
            _editingMemberId = memberId;
            _isEditMode = memberId > 0;
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Form Load
        // ──────────────────────────────────────────────────────────────
        private void MemberDetailsForm_Load(object sender, EventArgs e)
        {
            cmbPersonType.Tag = "PersonType"; // mark main combobox
            cmbPersonType.SelectedIndexChanged += cmbPersonType_SelectedIndexChanged;
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;

            LoadPlanTypes();
            SetupButtonVisibility();
            if (_isEditMode) LoadMemberDetails();
        }
        private void LoadMemberDetails()
        {
            var members = _planRepo.GetMembersByFamilyOrMemberId(_editingMemberId);
            if (members == null || members.Count == 0) return;

            // main member = first
            var main = members[0];
            _familyId = main.FamilyID;
            _selectedPlanId = main.PlanID;

            // load plan dropdown but disable editing
            var plan = _planRepo.GetPlanById(main.PlanID);
            if (plan != null)
            {
                cmbPlanType.Items.Clear();
                cmbPlanType.Items.Add(plan.PlanType);
                cmbPlanType.SelectedIndex = 0;
                cmbPlanType.Enabled = false;

                cmbPlanCategory.Items.Clear();
                cmbPlanCategory.Items.Add(plan.Category);
                cmbPlanCategory.SelectedIndex = 0;
                cmbPlanCategory.Enabled = false;

                txtFee.Text = plan.Fee.ToString("F2");
                txtFee.ReadOnly = true;
            }
            // clear old panels
            panel2.Controls.Clear();
            panel2.Controls.Add(panel1);

            // fill main member fields
            txtFullName.Text = main.FullName;
            dtpDOB.Value = main.DOB.HasValue
                ? (main.DOB.Value.Date < dtpDOB.MinDate ? dtpDOB.MinDate
                  : main.DOB.Value.Date > dtpDOB.MaxDate ? dtpDOB.MaxDate
                  : main.DOB.Value.Date)
                : DateTime.Today;
            txtAge.Text = main.Age.ToString();
            txtWeight.Text = main.Weight.ToString();
            txtPhone.Text = main.Phone;
            cmbPersonType.Items.Clear();
            cmbPersonType.Items.Add(main.PersonType);
            cmbPersonType.SelectedIndex = 0;

            // lock plan selection
            btnAddFamilyMember.Visible = false;



            // add family members (skip main one)
            foreach (var m in members.Skip(1))
            {
                Panel famPanel = CreateFamilyPanel();
                var cmb = famPanel.Controls.OfType<ComboBox>().FirstOrDefault(cb => cb.Tag?.ToString() == "PersonType");
                var txtName = famPanel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtFullName.Location);
                var dtp = famPanel.Controls.OfType<DateTimePicker>().FirstOrDefault();
                var txtAgeBox = famPanel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtAge.Location);
                var txtWeightBox = famPanel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtWeight.Location);
                var txtPhoneBox = famPanel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtPhone.Location);

                if (cmb != null) cmb.SelectedItem = m.PersonType;
                if (txtName != null) txtName.Text = m.FullName;
                if (dtp != null)
                    dtp.Value = m.DOB.HasValue
                        ? (m.DOB.Value.Date < dtp.MinDate ? dtp.MinDate
                          : m.DOB.Value.Date > dtp.MaxDate ? dtp.MaxDate
                          : m.DOB.Value.Date)
                        : DateTime.Today;
                if (txtAgeBox != null) txtAgeBox.Text = m.Age.ToString();
                if (txtWeightBox != null) txtWeightBox.Text = m.Weight.ToString();
                if (txtPhoneBox != null) txtPhoneBox.Text = m.Phone;

                panel2.Controls.Add(famPanel);
            }

            RelayoutFamilyPanels();
        }


        // ──────────────────────────────────────────────────────────────
        // ░░  Plan Loading & Selection
        // ──────────────────────────────────────────────────────────────
        private void LoadPlanTypes()
        {
            cmbPlanType.Items.Clear();
            var types = _planRepo.GetPlanTypes();
            foreach (var t in types) cmbPlanType.Items.Add(t);
            if (cmbPlanType.Items.Count > 0) cmbPlanType.SelectedIndex = 0;
        }

        private void cmbPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlanType = (cmbPlanType.SelectedItem ?? string.Empty).ToString();
            LoadCategoriesForPlanType(selectedPlanType);
        }

        private void LoadCategoriesForPlanType(string planType)
        {
            cmbPlanCategory.Items.Clear();
            txtFee.Text = string.Empty;
            cmbPersonType.Items.Clear();
            _selectedPlanId = 0;

            if (string.IsNullOrWhiteSpace(planType)) return;

            var categories = _planRepo.GetCategoriesByPlanType(planType);
            foreach (var c in categories) cmbPlanCategory.Items.Add(c);
            if (cmbPlanCategory.Items.Count > 0) cmbPlanCategory.SelectedIndex = 0;
        }
        // ──────────────────────────────────────────────────────────────
        // ░░  Button Visibility Handler
        // ──────────────────────────────────────────────────────────────
        private void SetupButtonVisibility()
        {
            btnSave.Visible = !_isEditMode;   // Save only in Add Mode
            btnUpdate.Visible = _isEditMode;  // Update only in Edit Mode
            btnDelete.Visible = _isEditMode;  // Delete only in Edit Mode
            removeBtn.Visible = _isEditMode;    //  main member remove btn

            // Optional: align buttons at bottom right
            int margin = 10;
            int y = this.ClientSize.Height - btnSave.Height - margin;

            if (btnSave.Visible)
                btnSave.Location = new Point(this.ClientSize.Width - btnSave.Width - margin, y);

            if (btnUpdate.Visible && btnDelete.Visible)
            {
                btnDelete.Location = new Point(this.ClientSize.Width - btnDelete.Width - margin, y);
                btnUpdate.Location = new Point(btnDelete.Left - btnUpdate.Width - margin, y);
            }
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Person Type Handling
        // ──────────────────────────────────────────────────────────────
        private void SetPersonTypeOptions(string personMode)
        {
            cmbPersonType.Items.Clear();
            cmbPersonType.Tag = "PersonType";

            if (string.IsNullOrWhiteSpace(personMode))
            {
                cmbPersonType.Items.AddRange(new[] { "Adult", "Student" });
            }
            else
            {
                var mode = personMode.Trim();
                if (string.Equals(mode, "Both", StringComparison.OrdinalIgnoreCase))
                    cmbPersonType.Items.AddRange(new[] { "Adult", "Student" });
                else
                    cmbPersonType.Items.Add(mode);
            }

            if (cmbPersonType.Items.Count > 0) cmbPersonType.SelectedIndex = 0;
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Fee Calculation (Plan Category Change)
        // ──────────────────────────────────────────────────────────────
        private void cmbPlanCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var planType = (cmbPlanType.SelectedItem ?? string.Empty).ToString();
            var category = (cmbPlanCategory.SelectedItem ?? string.Empty).ToString();

            if (string.IsNullOrWhiteSpace(planType) || string.IsNullOrWhiteSpace(category))
            {
                txtFee.Text = "";
                cmbPersonType.Items.Clear();
                btnAddFamilyMember.Visible = false;
                return;
            }

            var plan = _planRepo.GetPlanByTypeAndCategory(planType, category);
            if (plan == null)
            {
                MessageBox.Show("No plan found for " + planType + " / " + category);
                return;
            }

            txtFee.Text = plan.Fee.ToString("F2");
            _selectedPlanId = plan.PlanID;
            SetPersonTypeOptions(plan.PersonMode);

            // 👇 Only show Add Family button if category = Family
            btnAddFamilyMember.Visible = category.Equals("Family", StringComparison.OrdinalIgnoreCase);
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Dynamic Family Panel Management
        // ──────────────────────────────────────────────────────────────
        private void btnAddFamilyMember_Click(object sender, EventArgs e)
        {
            AddNewFamilyPanel();
        }

        private void AddNewFamilyPanel()
        {
            Panel newPanel = CreateFamilyPanel();
            panel2.Controls.Add(newPanel);
            RelayoutFamilyPanels();
        }

        private void RelayoutFamilyPanels()
        {
            int y = panel1.Bottom + 10;
            foreach (var p in panel2.Controls.OfType<Panel>()
                                     .Where(x => (x.Tag as ExistingMemberTag)?.Type == "FamilyPanel")
                                     .OrderBy(x => x.Top))
            {
                p.Location = new Point(panel1.Left, y);
                y += p.Height + 10;
            }
        }

        private Panel CreateFamilyPanel()
        {
            var p = new Panel
            {
                Size = panel1.Size,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = new ExistingMemberTag { Type = "FamilyPanel" }
            };

            // ----- Person Type -----
            ComboBox cmbPersonTypeNew = new ComboBox
            {
                Location = cmbPersonType.Location,
                Size = cmbPersonType.Size,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Tag = "PersonType"
            };
            cmbPersonTypeNew.Items.AddRange(new string[] { "Adult", "Student" });

            // default: if 2 adults already exist → Student
            int defIndex = CountAdults() >= 2 ? cmbPersonTypeNew.Items.IndexOf("Student") : 0;
            cmbPersonTypeNew.SelectedIndex = defIndex >= 0 ? defIndex : 0;
            p.Controls.Add(cmbPersonTypeNew);

            // ----- Full Name -----
            Label lblFullNameNew = new Label
            {
                Text = "Full Name",
                Location = lblFullName.Location,
                Size = lblFullName.Size
            };
            p.Controls.Add(lblFullNameNew);

            TextBox txtFullNameNew = new TextBox
            {
                Location = txtFullName.Location,
                Size = txtFullName.Size
            };
            p.Controls.Add(txtFullNameNew);

            // ----- DOB -----
            Label lblDOBNew = new Label
            {
                Text = "DOB",
                Location = lblDOB.Location,
                Size = lblDOB.Size
            };
            p.Controls.Add(lblDOBNew);

            DateTimePicker dtpDOBNew = new DateTimePicker
            {
                Location = dtpDOB.Location,
                Size = dtpDOB.Size
            };
            p.Controls.Add(dtpDOBNew);

            // ----- Age -----
            Label lblAgeNew = new Label
            {
                Text = "Age",
                Location = lblAge.Location,
                Size = lblAge.Size
            };
            p.Controls.Add(lblAgeNew);

            TextBox txtAgeNew = new TextBox
            {
                Location = txtAge.Location,
                Size = txtAge.Size,
                ReadOnly = true
            };
            p.Controls.Add(txtAgeNew);

            // ----- wight -----
            Label lblWeightNew = new Label
            {
                Text = "Weight",
                Location = lblWeight.Location,
                Size = lblWeight.Size
            };
            p.Controls.Add(lblWeightNew);

            TextBox txtWeightNew = new TextBox
            {
                Location = txtWeight.Location,
                Size = txtWeight.Size
            };
            p.Controls.Add(txtWeightNew);

            // ----- Phone -----
            Label lblPhoneNew = new Label
            {
                Text = "Phone",
                Location = lblPhone.Location,
                Size = lblPhone.Size
            };
            p.Controls.Add(lblPhoneNew);

            TextBox txtPhoneNew = new TextBox
            {
                Location = txtPhone.Location,
                Size = txtPhone.Size
            };
            p.Controls.Add(txtPhoneNew);

            // ----- Remove Button -----
            Button btnRemove = new Button
            {
                Text = "Remove",
                Location = new Point(p.Width - 80, 10)
            };
            btnRemove.Click += (s, e) =>
            {

                if (MessageBox.Show("Do you want to remove this family member?",
                      "Confirm Remove",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_isEditMode)
                    {
                        // find MemberID from DB if available
                        var member = _planRepo.GetMembersByFamilyOrMemberId(_editingMemberId)
                                              .FirstOrDefault(x => x.FullName == txtFullNameNew.Text);

                        if (member != null)
                            _planRepo.DeactivateMember(member.MemberID, member.FamilyID);
                    }
                    panel2.Controls.Remove(p);
                    RelayoutFamilyPanels();
                }

            };
            p.Controls.Add(btnRemove);


            // after txtFullNameNew is created
            // after creating txtFullNameNew in CreateFamilyPanel()
            txtFullNameNew.Leave += (s, e) =>
            {
                var name = txtFullNameNew.Text?.Trim();
                if (string.IsNullOrWhiteSpace(name)) return;

                var existing = _planRepo.GetMemberByFullName(name);
                if (existing == null) return;


                if (_isEditMode) { }
                else
                {
                    // check if they have active plan
                    if (_planRepo.MemberHasActiveEnrollPlan(existing.MemberID))
                    {
                        MessageBox.Show($"Member '{existing.FullName}' currently has an active plan and cannot be added to another active plan.", "Active Plan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ask user if they want to reuse existing member
                    var res = MessageBox.Show($"A member named '{existing.FullName}' already exists. Do you want to add this plan for them and auto-fill their details?",
                                              "Existing Member Found",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        // set panel fields from existing
                        txtFullNameNew.Text = existing.FullName;
                        if (dtpDOBNew != null && existing.DOB.HasValue) dtpDOBNew.Value = existing.DOB.Value;
                        if (txtAgeNew != null) txtAgeNew.Text = existing.Age.ToString();
                        if (txtWeightNew != null) txtWeightNew.Text = existing.Weight.ToString();
                        if (txtPhoneNew != null) txtPhoneNew.Text = existing.Phone;

                        // mark the panel as representing an existing member
                        // store the MemberID in the Panel's Tag (we'll read it later on Save)
                        p.Tag = new ExistingMemberTag
                        {
                            Type = "FamilyPanel",
                            MemberID = existing.MemberID
                        };

                    }
                    // ensure the PlanID is set later on save from the selected plan (_selectedPlanId)
                }
            };



            // initial constraints
            ApplyDobConstraints(cmbPersonTypeNew, dtpDOBNew);
            ValidateAndSetAge(dtpDOBNew, txtAgeNew, cmbPersonTypeNew);

            // events
            cmbPersonTypeNew.SelectedIndexChanged += (s, e) =>
            {
                ApplyDobConstraints(cmbPersonTypeNew, dtpDOBNew);
                ValidateAndSetAge(dtpDOBNew, txtAgeNew, cmbPersonTypeNew);
                EnforceAdultLimit(cmbPersonTypeNew);
            };
            dtpDOBNew.ValueChanged += (s, e) =>
            {
                ValidateAndSetAge(dtpDOBNew, txtAgeNew, cmbPersonTypeNew);
            };

            return p;
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  DOB + Age Validation
        // ──────────────────────────────────────────────────────────────
        private void ApplyDobConstraints(ComboBox personCombo, DateTimePicker dobPicker)
        {
            var selected = (personCombo.SelectedItem ?? "").ToString();
            if (string.Equals(selected, "Adult", StringComparison.OrdinalIgnoreCase))
            {
                dobPicker.MaxDate = DateTime.Now.AddYears(-18);
                dobPicker.MinDate = DateTime.Now.AddYears(-100);
                if (dobPicker.Value > dobPicker.MaxDate) dobPicker.Value = dobPicker.MaxDate;
            }
            else
            {
                dobPicker.MinDate = DateTime.Now.AddYears(-25);
                dobPicker.MaxDate = DateTime.Now;
                if (dobPicker.Value < dobPicker.MinDate) dobPicker.Value = dobPicker.MinDate;
            }
        }

        private void ValidateAndSetAge(DateTimePicker dobPicker, TextBox ageBox, ComboBox personTypeCombo)
        {
            var dob = dobPicker.Value.Date;
            int age = DateTime.Now.Year - dob.Year;
            if (dob > DateTime.Now.AddYears(-age)) age--;

            ageBox.Text = age.ToString();

            var type = (personTypeCombo.SelectedItem ?? "").ToString();
            if (string.Equals(type, "Adult", StringComparison.OrdinalIgnoreCase) && age < 18)
            {
                MessageBox.Show("Adults must be at least 18 years old.");
                dobPicker.Value = DateTime.Now.AddYears(-18);
                ageBox.Text = "18";
            }
            else if (string.Equals(type, "Student", StringComparison.OrdinalIgnoreCase) && age > 25)
            {
                MessageBox.Show("Students must be 25 years or younger.");
                dobPicker.Value = DateTime.Now.AddYears(-25);
                ageBox.Text = "25";
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            ValidateAndSetAge(dtpDOB, txtAge, cmbPersonType);
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Adult Limit Enforcement
        // ──────────────────────────────────────────────────────────────
        private int CountAdults()
        {
            var combos = new List<ComboBox>();
            if (cmbPersonType?.Tag?.ToString() == "PersonType")
                combos.Add(cmbPersonType);

            combos.AddRange(
                panel2.Controls.OfType<Panel>()
                      .Where(p => string.Equals(p.Tag as string, "FamilyPanel"))
                      .SelectMany(p => p.Controls.OfType<ComboBox>().Where(cb => cb.Tag?.ToString() == "PersonType"))
            );

            return combos.Count(cb => string.Equals((cb.SelectedItem ?? "").ToString(), "Adult", StringComparison.OrdinalIgnoreCase));
        }

        private void cmbPersonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyDobConstraints(cmbPersonType, dtpDOB);
            ValidateAndSetAge(dtpDOB, txtAge, cmbPersonType);
            EnforceAdultLimit(cmbPersonType);
        }

        private void EnforceAdultLimit(ComboBox combo)
        {
            if (string.Equals((combo.SelectedItem ?? "").ToString(), "Adult", StringComparison.OrdinalIgnoreCase) && CountAdults() > 2)
            {
                MessageBox.Show("Only 2 adults allowed in the membership.");
                int idx = combo.Items.IndexOf("Student");
                combo.SelectedIndex = idx >= 0 ? idx : 0;
            }
        }

        // ──────────────────────────────────────────────────────────────
        // ░░  Placeholder Event Handlers  remove butn main membr
        // ──────────────────────────────────────────────────────────────

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to remove this main member and deactivate the plan?",
"Confirm Remove",
MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // find MemberID from DB if available
                var member = _planRepo.GetMembersByFamilyOrMemberId(_editingMemberId)
                                      .FirstOrDefault(x => x.FullName == txtFullName.Text);

                if (member != null)
                    _planRepo.DeactivateMember(member.MemberID, member.FamilyID);


                MessageBox.Show(" memberplan deactivated.");

                // remove panel1 visually
                panel2.Controls.Remove(panel1);
                RelayoutFamilyPanels();
            }
        }



        private Member GetMainMemberFromForm()
        {
            var existing = _planRepo.GetMemberByFullName(txtFullName.Text);
            return new Member
            {   
          
                MemberID = existing?.MemberID ?? 0,
                PlanID = _selectedPlanId,
                FullName = txtFullName.Text,
                Age = int.Parse(txtAge.Text),
                DOB = dtpDOB.Value,
                Weight = string.IsNullOrWhiteSpace(txtWeight.Text) ? 0 : Convert.ToDecimal(txtWeight.Text),
                Phone = txtPhone.Text,
                StartDate = DateTime.Now,
                Status = "Active",       // or cmbStatus.SelectedValue if bound
                PersonType = cmbPersonType.Text
            };
        }
        private List<Member> GetFamilyMembersFromForm()
        {
            var list = new List<Member>();

            foreach (var panel in panel2.Controls.OfType<Panel>()
     .Where(x => (x.Tag as ExistingMemberTag)?.Type == "FamilyPanel"))
            {
                var tag = panel.Tag as ExistingMemberTag;
                int memberId = tag?.MemberID ?? 0;
                var txtNameNew = panel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtFullName.Location);
                var dtpDOBNew = panel.Controls.OfType<DateTimePicker>().FirstOrDefault();
                var txtAgeNew = panel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtAge.Location);
                var txtPhoneNew = panel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtPhone.Location);
                var cmbPersonTypeNew = panel.Controls.OfType<ComboBox>().FirstOrDefault(cb => cb.Tag?.ToString() == "PersonType");
                var txtWeightNew = panel.Controls.OfType<TextBox>().FirstOrDefault(t => t.Location == txtWeight.Location);

                if (txtNameNew != null && !string.IsNullOrWhiteSpace(txtNameNew.Text))
                {
                    int age = 0;
                    decimal weight = 0M;

                    if (!string.IsNullOrWhiteSpace(txtAgeNew?.Text))
                        int.TryParse(txtAgeNew.Text, out age);

                    if (!string.IsNullOrWhiteSpace(txtWeightNew?.Text))
                        decimal.TryParse(txtWeightNew.Text, out weight);
                    var existing = _planRepo.GetMemberByFullName(txtNameNew.Text);
                    var member = new Member
                    {
                        MemberID = existing?.MemberID ?? 0,
                        PlanID = _selectedPlanId,
                        FullName = txtNameNew.Text,
                        Age = age,
                        DOB = dtpDOBNew?.Value ?? DateTime.Now,
                        Weight = weight,
                        Phone = txtPhoneNew?.Text,
                        StartDate = DateTime.Now,
                        Status = "Active",
                        PersonType = cmbPersonTypeNew?.Text
                    };



                    list.Add(member);
                }
            }

            return list;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var mainMember = GetMainMemberFromForm();
                var familyMembers = GetFamilyMembersFromForm();

                // set PlanID on mainMember (already done in GetMainMemberFromForm)
                mainMember.PlanID = _selectedPlanId;

                // call repository which handles existing vs new members
                _planRepo.SaveMemberWithFamily(mainMember, familyMembers);

                MessageBox.Show("Member and family saved successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException inv)
            {
                MessageBox.Show(
                    $"Warning: {inv.Message}\n\nStack Trace:\n{inv.StackTrace}",
                    "Unable to Save",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                string fullError = $"Error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";

                if (ex.InnerException != null)
                {
                    fullError += $"\n\nInner Exception: {ex.InnerException.Message}\n\nInner Stack Trace:\n{ex.InnerException.StackTrace}";
                }

                MessageBox.Show(
                    fullError,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the updated main member from the form
            var mainMember = GetMainMemberFromForm();
            mainMember.MemberID = _editingMemberId;  // keep existing ID
            mainMember.FamilyID = _familyId;         // keep existing family group

            // Get family members from form
            var familyMembers = GetFamilyMembersFromForm();
            foreach (var f in familyMembers)
            {
                f.FamilyID = _familyId;   // attach to same family
            }

            try
            {
                _planRepo.UpdateMemberWithFamily(mainMember, familyMembers);
                MessageBox.Show("Member and family updated successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating: " + ex.Message);
            }
        }

        private void txtFullName_leave(object sender, EventArgs e)
        {
            var name = txtFullName.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name)) return;

            var existing = _planRepo.GetMemberByFullName(name);
            if (existing == null) return;


            if (_isEditMode) { }
            else
            {
                // check if they have active plan
                if (_planRepo.MemberHasActiveEnrollPlan(existing.MemberID))
                {
                    MessageBox.Show($"Member '{existing.FullName}' currently has an active plan and cannot be added to another active plan.", "Active Plan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ask user if they want to reuse existing member
                var res = MessageBox.Show($"A member named '{existing.FullName}' already exists. Do you want to add this plan for them and auto-fill their details?",
                                          "Existing Member Found",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // set panel fields from existing
                    _editingMemberId = existing.MemberID;
                    txtFullName.Text = existing.FullName;
                    if (dtpDOB != null && existing.DOB.HasValue) dtpDOB.Value = existing.DOB.Value;
                    if (txtAge != null) txtAge.Text = existing.Age.ToString();
                    if (txtWeight != null) txtWeight.Text = existing.Weight.ToString();
                    if (txtPhone != null) txtPhone.Text = existing.Phone;

                    // mark the panel as representing an existing member
                    // store the MemberID in the Panel's Tag (we'll read it later on Save)


                   
                    

                }
            }
        }
    }
}
