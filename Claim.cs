using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagementClaimSystem
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Status { get; set; } // 'Pending', 'Approved', 'Rejected'
        public string SupportingDocumentPath { get; set; }
    }
}
