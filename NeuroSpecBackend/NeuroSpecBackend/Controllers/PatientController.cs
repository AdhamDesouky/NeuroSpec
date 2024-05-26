using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IMongoCollection<Patient> _patients;

        public PatientController(NeuroDbContext context)
        {
            _patients = context.Patients;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patients.Find(_ => true).ToListAsync();
            return Ok(patients);
        }

        [HttpGet("{patientID}")]
        public async Task<ActionResult<Patient>> GetPatientById(string patientID)
        {
            var patient = await _patients.Find(p => p.Identifier.Value == patientID).FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet("{patientID}/{password}")]
        public async Task<ActionResult<bool>> VerifyPatient(string patientID, string password)
        {
            var patient = await _patients.Find(p => p.Identifier.Value == patientID && p.Password == password).FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(true);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> InsertPatient(Patient patient)
        {
            await _patients.InsertOneAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { patientID = patient.Identifier.Value }, patient);
        }

        [HttpPut("{patientID}")]
        public async Task<IActionResult> UpdatePatient(string patientID, Patient patient)
        {
            if (patientID != patient.Identifier.Value)
            {
                return BadRequest();
            }

            var result = await _patients.ReplaceOneAsync(p => p.Identifier.Value == patientID, patient);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{patientID}")]
        public async Task<IActionResult> DeletePatient(string patientID)
        {
            var result = await _patients.DeleteOneAsync(p => p.Identifier.Value == patientID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
