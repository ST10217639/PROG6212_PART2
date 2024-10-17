using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using ContractManagementClaimSystem;

namespace ContractManagementClaimSystem
{
    public partial class MainWindow : Window
    {
        private string _uploadedFilePath;
        private List<Claim> _claims = new List<Claim>();
        private int _claimIdCounter = 1;
        private Claim _currentClaim;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files|*.pdf|Word Files|*.docx|All Files|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _uploadedFilePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(_uploadedFilePath);
                FileNameTextBlock.Text = $"File uploaded: {fileName}";
            }
        }

        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            string lecturerName = LecturerNameTextBox.Text;
            string hoursWorkedText = HoursWorkedTextBox.Text;
            string hourlyRateText = HourlyRateTextBox.Text;

            if (string.IsNullOrEmpty(lecturerName) || string.IsNullOrEmpty(hoursWorkedText) || string.IsNullOrEmpty(hourlyRateText))
            {
                MessageBox.Show("Please fill all the required fields.");
                return;
            }

            if (string.IsNullOrEmpty(_uploadedFilePath))
            {
                MessageBox.Show("Please upload a supporting document.");
                return;
            }

            // Create a new claim
            _currentClaim = new Claim
            {
                Id = _claimIdCounter++,
                LecturerName = lecturerName,
                HoursWorked = int.Parse(hoursWorkedText),
                HourlyRate = decimal.Parse(hourlyRateText),
                Status = "Pending",
                SupportingDocumentPath = _uploadedFilePath
            };
            _claims.Add(_currentClaim);

            // Set data context to allow real-time updates
            DataContext = _currentClaim;

            StatusTextBlock.Text = "Status: Claim Submitted Successfully!";
            MessageBox.Show("Claim submitted successfully!");
        }

        private void OpenApprovalWindow_Click(object sender, RoutedEventArgs e)
        {
            ApprovalWindow approvalWindow = new ApprovalWindow(_claims);
            approvalWindow.ClaimStatusChanged += ApprovalWindow_ClaimStatusChanged;
            approvalWindow.Show();
        }

        // This event handler updates the status in real-time when a claim's status changes
        private void ApprovalWindow_ClaimStatusChanged(object sender, EventArgs e)
        {
            // Refresh the current claim's status
            StatusTextBlock.Text = $"Status: {_currentClaim.Status}";
        }
    }
}
