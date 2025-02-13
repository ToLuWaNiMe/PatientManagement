using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.Interfaces;
using PatientManagement.Application.Models;
using System.Threading.Tasks;

namespace PatientManagement.API.Controllers
{
    [ApiController]
    [Route("api/[patients]")] // or [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            var createdPatient = await _patientService.CreatePatientAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = createdPatient.PatientId }, createdPatient); // 201 Created
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            var updatedPatient = await _patientService.UpdatePatientAsync(patient);
            return Ok(updatedPatient); // 200 OK or 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent(); // 204 No Content
        }
    }
}