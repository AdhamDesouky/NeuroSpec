using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueSNOMEDController : Controller
    {

        private readonly IMongoCollection<IssueSNOMED> _issues;
        public IssueSNOMEDController(NeuroDbContext context)
        {
            _issues = context.IssueSNOMEDs;
        }

        [HttpPost]
        public async Task<ActionResult<IssueSNOMED>> InsertIssue(IssueSNOMED issue)
        {
            await _issues.InsertOneAsync(issue);
            return CreatedAtAction(nameof(GetIssueById), new { issueID = issue.IssueID }, issue);
        }

        [HttpGet]
        public async Task<ActionResult<List<IssueSNOMED>>> GetAllIssues()
        {
            var issues = await _issues.Find(_ => true).ToListAsync();
            return Ok(issues);
        }
        [HttpGet("{issueID}")]
        public async Task<ActionResult<IssueSNOMED>> GetIssueById(int issueID)
        {
            var issue = await _issues.Find(i => i.IssueID == issueID).FirstOrDefaultAsync();
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }
        [HttpDelete("{issueID}")]
        public async Task<ActionResult> DeleteIssue(int issueID)
        {
            var result = await _issues.DeleteOneAsync(i => i.IssueID == issueID);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<IssueSNOMED>> UpdateIssue(IssueSNOMED issue)
        {
            var result = await _issues.ReplaceOneAsync(i => i.IssueID == issue.IssueID, issue);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return Ok(issue);
        }
        [HttpGet("ByPrescription/{prescriptionID:int}")]
        public async Task<ActionResult<List<IssueSNOMED>>> GetAllIssuesByPrescriptionID(int prescriptionID)
        {
            var issues = await _issues.Find(i => i.PrescriptionID == prescriptionID).ToListAsync();
            return Ok(issues);
        }
        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<List<IssueSNOMED>>> GetAllIssuesByPatientID(int patientID)
        {
            var issues = await _issues.Find(i => i.PatientID == patientID).ToListAsync();
            return Ok(issues);
        }



    }
}
