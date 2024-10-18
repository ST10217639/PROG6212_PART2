using ContractManagementClaimSystem;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ContractManagementClaimSystem
{
    public partial class MainWindow : Window
    {
        private List<Claim> _claims;

        public MainWindow()
        {
            InitializeComponent();
            _claims = new List<Claim>(); // Initialize the claims list
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF Files|*.pdf|Word Files|*.docx|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    FileNameTextBlock.Text = System.IO.Path.GetFileName(filePath); // Display the uploaded file name
                    MessageBox.Show("File uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while uploading file: {ex.Message}");
            }
        }

        private void SubmitClaimButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Collect data from the form
                string lecturerName = LecturerNameTextBox.Text;
                string hoursWorkedText = HoursWorkedTextBox.Text;

                if (string.IsNullOrEmpty(lecturerName) || string.IsNullOrEmpty(hoursWorkedText))
                {
                    MessageBox.Show("Please fill all the required fields.");
                    return;
                }

                // Add new claim
                int hoursWorked = int.Parse(hoursWorkedText); // Possible exception if the input is not a valid number
                var newClaim = new Claim
                {
                    Id = _claims.Count + 1, // Simulate auto-increment for claim ID
                    LecturerName = lecturerName,
                    HoursWorked = hoursWorked,
                    Status = "Pending",
                    SupportingDocumentPath = FileNameTextBlock.Text // Store the file name
                };
                _claims.Add(newClaim);

                StatusTextBlock.Text = "Status: Claim Submitted Successfully!";
                MessageBox.Show("Claim submitted successfully!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for hours worked.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while submitting claim: {ex.Message}");
            }
        }

        private void OpenApprovalWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var approvalWindow = new ApprovalWindow(_claims);
                approvalWindow.ClaimStatusChanged += OnClaimStatusChanged;
                approvalWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening the approval window: {ex.Message}");
            }
            this.Close();
        }

        private void OnClaimStatusChanged(object sender, EventArgs e)
        {
            // Update the status of the claim in real-time after approval/rejection
            MessageBox.Show("Claim status updated. Please refresh to view the latest information.");
            StatusTextBlock.Text = "Status: Updated in Real-Time!";
        }
    }
}
