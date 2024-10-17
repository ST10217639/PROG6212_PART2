using ContractManagementClaimSystem;
using System.Collections.Generic;
using System.Windows;

namespace ContractManagementClaimSystem
{
    public partial class ApprovalWindow : Window
    {
        private List<Claim> _claims;

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

        private void RejectClaimButton_Click(object sender, RoutedEventArgs e)
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

        private void UpdateClaimStatus(int id, string status)
        {
            var claim = _claims.Find(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = status;
            }
        }
    }
}
