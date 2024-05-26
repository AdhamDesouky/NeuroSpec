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
    public class IssueScanController : Controller
    {
        private readonly IMongoCollection<IssueScan> _issueScans;

        public IssueScanController(NeuroDbContext context)
        {
            _issueScans = context.IssueScans;
        }

        [HttpPost]
        public async Task<ActionResult<IssueScan>> InsertIssueScan(IssueScan issueScan)
        {
            await _issueScans.InsertOneAsync(issueScan);
            return CreatedAtAction(nameof(GetIssueScanById), new { issueID = issueScan.IssueID }, issueScan);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueScan>>> GetAllIssueScans()
        {
            var issueScans = await _issueScans.Find(_ => true).ToListAsync();
            return Ok(issueScans);
        }

        [HttpGet("{issueID:int}")]
        public async Task<ActionResult<IssueScan>> GetIssueScanById(int issueID)
        {
            var issueScan = await _issueScans.Find(i => i.IssueID == issueID).FirstOrDefaultAsync();

            if (issueScan == null)
            {
                return NotFound();
            }

            return Ok(issueScan);
        }

        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<IEnumerable<IssueScan>>> GetAllIssueScansByPatientID(int patientID)
        {
            var issueScans = await _issueScans.Find(i => i.PatientID == patientID).ToListAsync();
            return Ok(issueScans);
        }

        [HttpGet("ByPrescription/{prescriptionID:int}")]
        public async Task<ActionResult<IEnumerable<IssueScan>>> GetAllIssueScansByPrescriptionID(int prescriptionID)
        {
            var issueScans = await _issueScans.Find(i => i.PrescriptionID == prescriptionID).ToListAsync();
            return Ok(issueScans);
        }

        [HttpPut("{issueID:int}")]
        public async Task<IActionResult> UpdateIssueScan(int issueID, IssueScan issueScan)
        {
            if (issueID != issueScan.IssueID)
            {
                return BadRequest();
            }

            var result = await _issueScans.ReplaceOneAsync(i => i.IssueID == issueID, issueScan);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{issueID:int}")]
        public async Task<IActionResult> DeleteIssueScan(int issueID)
        {
            var result = await _issueScans.DeleteOneAsync(i => i.IssueID == issueID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IssueScanExists(int issueID)
        {
            return _issueScans.Find(i => i.IssueID == issueID).Any();
        }
    }
}
