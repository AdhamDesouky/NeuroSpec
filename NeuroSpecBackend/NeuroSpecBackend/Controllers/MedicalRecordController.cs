using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : Controller
    {
        private readonly IMongoCollection<MedicalRecord> _medicalRecords;

        public MedicalRecordController(NeuroDbContext context)
        {
            _medicalRecords = context.MedicalRecords;
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> InsertMedicalRecord(MedicalRecord medicalRecord)
        {
            await _medicalRecords.InsertOneAsync(medicalRecord);
            return CreatedAtAction(nameof(GetMedicalRecordByID), new { recordID = medicalRecord.RecordID }, medicalRecord);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetAllMedicalRecords()
        {
            var medicalRecords = await _medicalRecords.Find(_ => true).ToListAsync();
            return Ok(medicalRecords);
        }

        [HttpGet("{recordID:int}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecordByID(int recordID)
        {
            var medicalRecord = await _medicalRecords.Find(m => m.RecordID == recordID).FirstOrDefaultAsync();

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return Ok(medicalRecord);
        }

        [HttpGet("onFHIR/{recordID:int}")]
        public async Task<ActionResult<DiagnosticReport>> GetMedicalRecordOnFHIRByID(int recordID)
        {
            var medicalRecord = await _medicalRecords.Find(m => m.RecordID == recordID).FirstOrDefaultAsync();

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return Ok(FHIRMapper.ToHl7MedicalRecord(medicalRecord));
        }

        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetAllPatientRecords(int patientID)
        {
            var medicalRecords = await _medicalRecords.Find(m => m.PatientID == patientID).ToListAsync();
            return Ok(medicalRecords);
        }

        [HttpPut("{recordID:int}")]
        public async Task<IActionResult> UpdateMedicalRecord(int recordID, MedicalRecord medicalRecord)
        {
            if (recordID != medicalRecord.RecordID)
            {
                return BadRequest();
            }

            var result = await _medicalRecords.ReplaceOneAsync(m => m.RecordID == recordID, medicalRecord);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{recordID:int}")]
        public async Task<IActionResult> DeleteMedicalRecord(int recordID)
        {
            var result = await _medicalRecords.DeleteOneAsync(m => m.RecordID == recordID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool MedicalRecordExists(int recordID)
        {
            return _medicalRecords.Find(m => m.RecordID == recordID).Any();
        }
    }
}
