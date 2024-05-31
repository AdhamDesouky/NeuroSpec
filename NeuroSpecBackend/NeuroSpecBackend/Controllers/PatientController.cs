using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
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
        public async Task<ActionResult<Patient>> GetPatientById(int patientID)
        {
            var patient = await _patients.Find(p => p.PatientID== patientID).FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet("onFHIR/{patientID}")]
        public async Task<ActionResult<Hl7.Fhir.Model.Patient>> GetFHIRPatientById(int patientID)
        {
            var patient = await _patients.Find(p => p.PatientID == patientID).FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }
            Hl7.Fhir.Model.Patient returnPatient=FHIRPatient.ToHl7Patient(patient);
            return Ok(returnPatient);
        }

        [HttpGet("{patientID}/{password}")]
        public async Task<ActionResult<bool>> VerifyPatient(int patientID, string password)
        {
            var patient = await _patients.Find(p => p.PatientID== patientID && password==p.Password).FirstOrDefaultAsync(); 

            if (patient == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> InsertPatient(Patient patient)
        {
            await _patients.InsertOneAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { patientID = patient.PatientID }, patient);
        }

        [HttpPut("{patientID}")]
        public async Task<IActionResult> UpdatePatient(int patientID, Patient patient)
        {
            if (patientID != patient.PatientID)
            {
                return BadRequest();
            }

            var result = await _patients.ReplaceOneAsync(p => p.PatientID == patientID, patient);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{patientID}")]
        public async Task<IActionResult> DeletePatient(int patientID)
        {
            var result = await _patients.DeleteOneAsync(p => p.PatientID == patientID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
