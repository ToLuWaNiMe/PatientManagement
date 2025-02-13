using Microsoft.EntityFrameworkCore;
using PatientManagement.Application.Interfaces;
using PatientManagement.Application.Models;
using PatientManagement.Infrastructure.Data;
// Your DbContext namespace

namespace PatientManagement.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientManagementContext _context; // Inject your DbContext

        public PatientService(PatientManagementContext context) // Constructor injection
        {
            _context = context;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient); // Or use Attach and Modify
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            patient.IsDeleted = true; // Soft delete
            _context.Patients.Update(patient); // Update the patient entity.
            await _context.SaveChangesAsync();

            return true;
        }
    }
}