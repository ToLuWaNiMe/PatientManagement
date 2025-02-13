using Microsoft.EntityFrameworkCore;
using PatientManagement.Application.Interfaces;
using PatientManagement.Application.Models;
using PatientManagement.Infrastructure.Data;

namespace PatientManagement.Infrastructure.Services
{
    public class PatientRecordService : IPatientRecordService
    {
        private readonly PatientManagementContext _context;

        public PatientRecordService(PatientManagementContext context)
        {
            _context = context;
        }

        public async Task<PatientRecord> GetPatientRecordByIdAsync(int id)
        {
            return await _context.PatientRecords.FindAsync(id);
        }

        public async Task<List<PatientRecord>> GetAllPatientRecordsByPatientIdAsync(int patientId)
        {
            return await _context.PatientRecords.Where(r => r.PatientId == patientId).ToListAsync();
        }

        public async Task<PatientRecord> CreatePatientRecordAsync(PatientRecord patientRecord)
        {
            _context.PatientRecords.Add(patientRecord);
            await _context.SaveChangesAsync();
            return patientRecord;
        }

        public async Task<PatientRecord> UpdatePatientRecordAsync(PatientRecord patientRecord)
        {
            _context.PatientRecords.Update(patientRecord);
            await _context.SaveChangesAsync();
            return patientRecord;
        }

        public async Task<bool> DeletePatientRecordAsync(int id)
        {
            var patientRecord = await _context.PatientRecords.FindAsync(id);
            if (patientRecord == null)
            {
                return false;
            }

            _context.PatientRecords.Remove(patientRecord);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}