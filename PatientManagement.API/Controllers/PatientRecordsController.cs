using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.Interfaces;
using PatientManagement.Application.Models;
using System.Threading.Tasks;

namespace PatientManagement.API.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId}/records")] // Nested route
    public class PatientRecordsController : ControllerBase
    {
        private readonly IPatientRecordService _patientRecordService;

        public PatientRecordsController(IPatientRecordService patientRecordService)
        {
            _patientRecordService = patientRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientRecords(int patientId)
        {
            var records = await _patientRecordService.GetAllPatientRecordsByPatientIdAsync(patientId);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientRecord(int patientId, int id)
        {
            var record = await _patientRecordService.GetPatientRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatientRecord(int patientId, PatientRecord patientRecord)
        {
            patientRecord.PatientId = patientId; // Important: Set the PatientId
            var createdRecord = await _patientRecordService.CreatePatientRecordAsync(patientRecord);
            return CreatedAtAction(nameof(GetPatientRecord), new { patientId, id = createdRecord.PatientRecordId }, createdRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatientRecord(int patientId, int id, PatientRecord patientRecord)
        {
            if (id != patientRecord.PatientRecordId || patientId != patientRecord.PatientId)
            {
                return BadRequest();
            }

            var updatedRecord = await _patientRecordService.UpdatePatientRecordAsync(patientRecord);
            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientRecord(int id)
        {
            var deleted = await _patientRecordService.DeletePatientRecordAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}