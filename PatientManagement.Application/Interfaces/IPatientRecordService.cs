using PatientManagement.Application.Models;

namespace PatientManagement.Application.Interfaces
{
    public interface IPatientRecordService
    {
        Task<PatientRecord> GetPatientRecordByIdAsync(int id);
        Task<List<PatientRecord>> GetAllPatientRecordsByPatientIdAsync(int patientId);
        Task<PatientRecord> CreatePatientRecordAsync(PatientRecord patientRecord);
        Task<PatientRecord> UpdatePatientRecordAsync(PatientRecord patientRecord);
        Task<bool> DeletePatientRecordAsync(int id);
    }
}