using ContractManagementClaimSystem;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ContractManagementClaimSystem
{
    public partial class ApprovalWindow : Window
    {
        private List<Claim> _claims;

        // Declare the event to notify when a claim status changes
        public event EventHandler ClaimStatusChanged;

        public ApprovalWindow(List<Claim> claims)
        {
            InitializeComponent();
            _claims = claims;
            LoadClaims();
        }

        private void LoadClaims()
        {
            ClaimsListBox.Items.Clear();
            foreach (var claim in _claims)
            {
                string fileName = System.IO.Path.GetFileName(claim.SupportingDocumentPath); // Extract file name
                ClaimsListBox.Items.Add($"Claim {claim.Id} - Lecturer: {claim.LecturerName}, Hours: {claim.HoursWorked}, Status: {claim.Status}, File: {fileName}");
            }
        }

        private void ApproveClaimButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaimsListBox.SelectedItem != null)
                {
                    var selectedClaim = ClaimsListBox.SelectedItem.ToString();
                    int claimId = int.Parse(selectedClaim.Split(' ')[1]);
                    UpdateClaimStatus(claimId, "Approved");
                    MessageBox.Show("Claim Approved!");
                    LoadClaims();
                }
                else
                {
                    MessageBox.Show("Please select a claim to approve.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while approving claim: {ex.Message}");
            }
        }

        private void RejectClaimButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClaimsListBox.SelectedItem != null)
                {
                    var selectedClaim = ClaimsListBox.SelectedItem.ToString();
                    int claimId = int.Parse(selectedClaim.Split(' ')[1]);
                    UpdateClaimStatus(claimId, "Rejected");
                    MessageBox.Show("Claim Rejected!");
                    LoadClaims();
                }
                else
                {
                    MessageBox.Show("Please select a claim to reject.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while rejecting claim: {ex.Message}");
            }
        }

        private void UpdateClaimStatus(int id, string status)
        {
            var claim = _claims.Find(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = status;

                // Trigger the event to notify MainWindow
                ClaimStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
