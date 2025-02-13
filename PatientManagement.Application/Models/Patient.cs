using System;
using System.Collections.Generic;

namespace PatientManagement.Application.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } // Make nullable for cases where DOB is not known.
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; } // For soft delete
        public List<PatientRecord> PatientRecords { get; set; } = new List<PatientRecord>(); // Initialize the list
    }
}