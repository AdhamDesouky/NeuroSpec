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
    public class IssueDrugController : Controller
    {
        private readonly IMongoCollection<IssueDrug> _IssueDrugs;

        public IssueDrugController(NeuroDbContext context)
        {
            _IssueDrugs = context.IssueDrugs;
        }

        [HttpPost]
        public async Task<ActionResult<IssueDrug>> InsertIssueDrug(IssueDrug IssueDrug)
        {
            await _IssueDrugs.InsertOneAsync(IssueDrug);
            return CreatedAtAction(nameof(GetIssueDrugById), new { issueID = IssueDrug.IssueID }, IssueDrug);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueDrug>>> GetAllIssueDrugs()
        {
            var IssueDrugs = await _IssueDrugs.Find(_ => true).ToListAsync();
            return Ok(IssueDrugs);
        }

        [HttpGet("{issueID:int}")]
        public async Task<ActionResult<IssueDrug>> GetIssueDrugById(string issueID)
        {
            var IssueDrug = await _IssueDrugs.Find(i => i.IssueID == issueID).FirstOrDefaultAsync();

            if (IssueDrug == null)
            {
                return NotFound();
            }

            return Ok(IssueDrug);
        }

        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<IEnumerable<IssueDrug>>> GetAllIssueDrugsByPatientID(int patientID)
        {
            var IssueDrugs = await _IssueDrugs.Find(i => i.PatientID == patientID).ToListAsync();
            return Ok(IssueDrugs);
        }

        [HttpGet("ByPrescription/{prescriptionID:int}")]
        public async Task<ActionResult<IEnumerable<IssueDrug>>> GetAllIssueDrugsByPrescriptionID(int prescriptionID)
        {
            var IssueDrugs = await _IssueDrugs.Find(i => i.PrescriptionID == prescriptionID).ToListAsync();
            return Ok(IssueDrugs);
        }

        [HttpPut("{issueID:int}")]
        public async Task<IActionResult> UpdateIssueDrug(string issueID, IssueDrug IssueDrug)
        {
            if (issueID != IssueDrug.IssueID)
            {
                return BadRequest();
            }

            var result = await _IssueDrugs.ReplaceOneAsync(i => i.IssueID == issueID, IssueDrug);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{issueID:int}")]
        public async Task<IActionResult> DeleteIssueDrug(string issueID)
        {
            var result = await _IssueDrugs.DeleteOneAsync(i => i.IssueID == issueID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IssueDrugExists(string issueID)
        {
            return _IssueDrugs.Find(i => i.IssueID == issueID).Any();
        }
    }
}
