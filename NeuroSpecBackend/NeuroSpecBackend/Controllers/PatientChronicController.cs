using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientChronicController : Controller
    {
        private readonly IMongoCollection<PatientChronic> _collection;
        public PatientChronicController(NeuroDbContext context)
        {
            _collection = context.PatientChronics;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientChronic>>> GetAllChronics()
        {
            var patientChronics = await _collection.Find(_ => true).ToListAsync();
            return Ok(patientChronics);
        }
        [HttpPost]
        public async Task<ActionResult<PatientChronic>> InsertChronic(PatientChronic patientChronic)
        {
            await _collection.InsertOneAsync(patientChronic);
            return CreatedAtAction(nameof(GetChronicById), new { chronicId = patientChronic.Id }, patientChronic);
        }
        [HttpGet("{chronicId}")]
        public async Task<ActionResult<PatientChronic>> GetChronicById(string chronicId)
        {
            var patientChronic = await _collection.Find(c => c.Id == chronicId).FirstOrDefaultAsync();
            if (patientChronic == null)
            {
                return NotFound();
            }
            return Ok(patientChronic);
        }
        [HttpPut("{chronicId}")]
        public async Task<IActionResult> UpdateChronic(string chronicId, PatientChronic patientChronic)
        {
            if (chronicId != patientChronic.Id)
            {
                return BadRequest();
            }
            var result = await _collection.ReplaceOneAsync(c => c.Id == chronicId, patientChronic);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{chronicId}")]
        public async Task<IActionResult> DeleteChronic(string chronicId)
        {
            var result = await _collection.DeleteOneAsync(c => c.Id == chronicId);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        //get all chronics of a patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientChronic>>> GetPatientChronics(int patientId)
        {
            var patientChronics = await _collection.Find(c => c.PatientID == patientId).ToListAsync();
            return Ok(patientChronics);
        }
    }
}
