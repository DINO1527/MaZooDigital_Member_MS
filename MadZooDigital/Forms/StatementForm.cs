using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MadZooDigital.Data;
using MadZooDigital.Models;
using System.Drawing.Printing;
namespace MadZooDigital.Forms
{
    
        public partial class StatementForm : Form
        {
            private readonly MemberRepository _memberRepo = new MemberRepository();
            private readonly StatementRepository _statementRepo = new StatementRepository();
            private MemberStatement _currentStatement;
            private PrintDocument _printDoc;
            private int _printRowIndex;

            public StatementForm()
            {
                InitializeComponent();
                dtpMonth.Value = DateTime.Today;
            }

            private void StatementForm_Load(object sender, EventArgs e)
            {
                LoadMembers();
                SetupGrid();
            }

            private void LoadMembers()
            {
                try
                {
                    var members = _memberRepo.GetAll();
                    cmbMembers.DataSource = members;
                    cmbMembers.DisplayMember = "FullName";
                    cmbMembers.ValueMember = "MemberID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading members: " + ex.Message);
                }
            }

            private void SetupGrid()
            {
                dgvItems.AutoGenerateColumns = false;
                dgvItems.Columns.Clear();

                var colDate = new DataGridViewTextBoxColumn { HeaderText = "Date", DataPropertyName = "Date", Width = 120 };
                var colDesc = new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill };
                var colType = new DataGridViewTextBoxColumn { HeaderText = "Type", DataPropertyName = "ItemType", Width = 100 };
                var colAmount = new DataGridViewTextBoxColumn { HeaderText = "Amount (Rs.)", DataPropertyName = "Amount", Width = 120, DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } };

                dgvItems.Columns.Add(colDate);
                dgvItems.Columns.Add(colDesc);
                dgvItems.Columns.Add(colType);
                dgvItems.Columns.Add(colAmount);
            }

            private void btnLoad_Click(object sender, EventArgs e)
            {
                LoadStatement();
            }

            private void LoadStatement()
            {
                if (cmbMembers.SelectedItem == null) { MessageBox.Show("Select a member."); return; }
                int memberId = (int)cmbMembers.SelectedValue;
                var selDate = dtpMonth.Value;
                var month = selDate.Month;
                var year = selDate.Year;

                try
                {
                    _currentStatement = _statementRepo.GetMemberStatement(memberId, month, year);
                    dgvItems.DataSource = _currentStatement.Items.Select(i => new
                    {
                        Date = i.Date.ToString("yyyy-MM-dd"),
                        Description = i.Description,
                        ItemType = i.ItemType,
                        Amount = i.Amount
                    }).ToList();

                    lblTotal.Text = $"Total: Rs. {_currentStatement.Total:N2}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading statement: " + ex.Message);
                }
            }

            #region Print
            private void btnPrint_Click(object sender, EventArgs e)
            {
                if (_currentStatement == null || _currentStatement.Items.Count == 0)
                {
                    MessageBox.Show("No statement to print. Load a member & month first.");
                    return;
                }

                _printDoc = new PrintDocument();
                _printDoc.DefaultPageSettings.Landscape = false;
                _printDoc.PrintPage += _printDoc_PrintPage;
                _printRowIndex = 0;

                var pd = new PrintPreviewDialog { Document = _printDoc, Width = 900, Height = 700 };
                pd.ShowDialog();
                // To actually print: _printDoc.Print();
            }

            private void _printDoc_PrintPage(object sender, PrintPageEventArgs e)
            {
                int left = e.MarginBounds.Left;
                int top = e.MarginBounds.Top;
                int lineHeight = (int)Font.GetHeight(e.Graphics) + 6;
                int y = top;

                // Header
                e.Graphics.DrawString("Monthly Statement", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, left, y);
                y += lineHeight;
                e.Graphics.DrawString($"Member: {_currentStatement.MemberName}  Month: {_currentStatement.Month}/{_currentStatement.Year}", new Font("Arial", 10), Brushes.Black, left, y);
                y += lineHeight * 2;

                // Column headers
                e.Graphics.DrawString("Date", Font, Brushes.Black, left, y);
                e.Graphics.DrawString("Description", Font, Brushes.Black, left + 120, y);
                e.Graphics.DrawString("Type", Font, Brushes.Black, left + 420, y);
                e.Graphics.DrawString("Amount (Rs.)", Font, Brushes.Black, left + 520, y);
                y += lineHeight;

                // Items
                while (_printRowIndex < _currentStatement.Items.Count)
                {
                    var it = _currentStatement.Items[_printRowIndex];
                    e.Graphics.DrawString(it.Date.ToString("yyyy-MM-dd"), Font, Brushes.Black, left, y);
                    e.Graphics.DrawString(it.Description, Font, Brushes.Black, left + 120, y);
                    e.Graphics.DrawString(it.ItemType, Font, Brushes.Black, left + 420, y);
                    e.Graphics.DrawString(it.Amount.ToString("N2"), Font, Brushes.Black, left + 520, y);
                    y += lineHeight;
                    _printRowIndex++;

                    if (y + lineHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                // Totals
                y += lineHeight;
                e.Graphics.DrawString($"Total: Rs. {_currentStatement.Total:N2}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, left, y);

                e.HasMorePages = false;
                _printRowIndex = 0;
            }
            #endregion

            #region Export CSV
            private void btnExportCsv_Click(object sender, EventArgs e)
            {
                if (_currentStatement == null || _currentStatement.Items.Count == 0)
                {
                    MessageBox.Show("No statement to export. Load a member & month first.");
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    sfd.FileName = $"{_currentStatement.MemberName}_{_currentStatement.Year}_{_currentStatement.Month}.csv";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    try
                    {
                        using (var sw = new StreamWriter(sfd.FileName))
                        {
                            sw.WriteLine("Date,Description,Type,Amount");
                            foreach (var it in _currentStatement.Items)
                            {
                                string line = $"{it.Date:yyyy-MM-dd},\"{it.Description}\",{it.ItemType},{it.Amount:N2}";
                                sw.WriteLine(line);
                            }
                            sw.WriteLine($",,Total,{_currentStatement.Total:N2}");
                        }

                        MessageBox.Show("Exported CSV successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error exporting CSV: " + ex.Message);
                    }
                }
            }
            #endregion
        }
    }
