using PatientManagement.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientManagement.Application.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByIdAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id); // Soft delete
    }
}