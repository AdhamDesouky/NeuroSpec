using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueExerciseController : Controller
    {
        private readonly IMongoCollection<IssueExercise> _IssueExercises;

        public IssueExerciseController(NeuroDbContext context)
        {
            _IssueExercises = context.IssueExercises;
        }

        [HttpPost]
        public async Task<ActionResult<IssueExercise>> InsertIssueExercise(IssueExercise IssueExercise)
        {
            await _IssueExercises.InsertOneAsync(IssueExercise);
            return CreatedAtAction(nameof(GetIssueExerciseById), new { issueID = IssueExercise.IssueID }, IssueExercise);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueExercise>>> GetAllIssueExercises()
        {
            var IssueExercises = await _IssueExercises.Find(_ => true).ToListAsync();
            return Ok(IssueExercises);
        }

        [HttpGet("{issueID:int}")]
        public async Task<ActionResult<IssueExercise>> GetIssueExerciseById(int issueID)
        {
            var IssueExercise = await _IssueExercises.Find(i => i.IssueID == issueID).FirstOrDefaultAsync();

            if (IssueExercise == null)
            {
                return NotFound();
            }

            return Ok(IssueExercise);
        }

        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<IEnumerable<IssueExercise>>> GetAllIssueExercisesByPatientID(int patientID)
        {
            var IssueExercises = await _IssueExercises.Find(i => i.PatientID == patientID).ToListAsync();
            return Ok(IssueExercises);
        }

        [HttpGet("ByPrescription/{prescriptionID:int}")]
        public async Task<ActionResult<IEnumerable<IssueExercise>>> GetAllIssueExercisesByPrescriptionID(int prescriptionID)
        {
            var IssueExercises = await _IssueExercises.Find(i => i.PrescriptionID == prescriptionID).ToListAsync();
            return Ok(IssueExercises);
        }

        [HttpPut("{issueID:int}")]
        public async Task<IActionResult> UpdateIssueExercise(int issueID, IssueExercise IssueExercise)
        {
            if (issueID != IssueExercise.IssueID)
            {
                return BadRequest();
            }

            var result = await _IssueExercises.ReplaceOneAsync(i => i.IssueID == issueID, IssueExercise);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{issueID:int}")]
        public async Task<IActionResult> DeleteIssueExercise(int issueID)
        {
            var result = await _IssueExercises.DeleteOneAsync(i => i.IssueID == issueID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IssueExerciseExists(int issueID)
        {
            return _IssueExercises.Find(i => i.IssueID == issueID).Any();
        }
    }
}
