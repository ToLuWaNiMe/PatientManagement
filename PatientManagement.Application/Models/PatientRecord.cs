using System;

namespace PatientManagement.Application.Models
{
    public class PatientRecord
    {
        public int PatientRecordId { get; set; }
        public int PatientId { get; set; }  // Foreign Key
        public DateTime? RecordDate { get; set; } // Nullable DateTime
        public string Description { get; set; }
    }
}