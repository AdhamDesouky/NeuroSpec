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
    public class PrescriptionController : ControllerBase
    {
        private readonly IMongoCollection<Prescription> _prescriptions;

        public PrescriptionController(NeuroDbContext context)
        {
            _prescriptions = context.Prescriptions;
        }

        [HttpPost]
        public async Task<ActionResult<Prescription>> InsertPrescription(Prescription prescription)
        {
            await _prescriptions.InsertOneAsync(prescription);
            return CreatedAtAction(nameof(GetPrescriptionByID), new { prescriptionID = prescription.PrescriptionID }, prescription);
        }

        [HttpPut("{prescriptionID}")]
        public async Task<IActionResult> UpdatePrescription(int prescriptionID, Prescription prescription)
        {
            if (prescriptionID != prescription.PrescriptionID)
            {
                return BadRequest();
            }

            var result = await _prescriptions.ReplaceOneAsync(p => p.PrescriptionID == prescriptionID, prescription);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{prescriptionID}")]
        public async Task<IActionResult> DeletePrescription(int prescriptionID)
        {
            var result = await _prescriptions.DeleteOneAsync(p => p.PrescriptionID == prescriptionID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{prescriptionID}")]
        public async Task<ActionResult<Prescription>> GetPrescriptionByID(int prescriptionID)
        {
            var prescription = await _prescriptions.Find(p => p.PrescriptionID == prescriptionID).FirstOrDefaultAsync();

            if (prescription == null)
            {
                return NotFound();
            }

            return prescription;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptions.Find(_ => true).ToListAsync();
            return Ok(prescriptions);
        }

        [HttpGet("patient/{patientID}")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllPrescriptionsByPatientID(int patientID)
        {
            var prescriptions = await _prescriptions.Find(p => p.PatientID == patientID).ToListAsync();
            return Ok(prescriptions);
        }

        [HttpGet("visit/{visitID}")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllPrescriptionsByVisitID(int visitID)
        {
            var prescriptions = await _prescriptions.Find(p => p.VisitID == visitID).ToListAsync();
            return Ok(prescriptions);
        }
    }
}
