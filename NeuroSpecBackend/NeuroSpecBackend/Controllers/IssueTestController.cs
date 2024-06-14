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
    public class IssueTestController : Controller
    {
        private readonly IMongoCollection<IssueTest> _issueTests;

        public IssueTestController(NeuroDbContext context)
        {
            _issueTests = context.IssueTests;
        }

        [HttpPost]
        public async Task<ActionResult<IssueTest>> InsertIssueTest(IssueTest IssueTest)
        {
            await _issueTests.InsertOneAsync(IssueTest);
            return CreatedAtAction(nameof(GetIssueTestById), new { issueID = IssueTest.IssueID }, IssueTest);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueTest>>> GetAllIssueTests()
        {
            var IssueTests = await _issueTests.Find(_ => true).ToListAsync();
            return Ok(IssueTests);
        }

        [HttpGet("{issueID:int}")]
        public async Task<ActionResult<IssueTest>> GetIssueTestById(int issueID)
        {
            var IssueTest = await _issueTests.Find(i => i.IssueID == issueID).FirstOrDefaultAsync();

            if (IssueTest == null)
            {
                return NotFound();
            }

            return Ok(IssueTest);
        }

        [HttpGet("ByPatient/{patientID:int}")]
        public async Task<ActionResult<IEnumerable<IssueTest>>> GetAllIssueTestsByPatientID(int patientID)
        {
            var IssueTests = await _issueTests.Find(i => i.PatientID == patientID).ToListAsync();
            return Ok(IssueTests);
        }

        [HttpGet("ByPrescription/{prescriptionID:int}")]
        public async Task<ActionResult<IEnumerable<IssueTest>>> GetAllIssueTestsByPrescriptionID(int prescriptionID)
        {
            var IssueTests = await _issueTests.Find(i => i.PrescriptionID == prescriptionID).ToListAsync();
            return Ok(IssueTests);
        }

        [HttpPut("{issueID:int}")]
        public async Task<IActionResult> UpdateIssueTest(int issueID, IssueTest IssueTest)
        {
            if (issueID != IssueTest.IssueID)
            {
                return BadRequest();
            }

            var result = await _issueTests.ReplaceOneAsync(i => i.IssueID == issueID, IssueTest);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{issueID:int}")]
        public async Task<IActionResult> DeleteIssueTest(int issueID)
        {
            var result = await _issueTests.DeleteOneAsync(i => i.IssueID == issueID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IssueTestExists(int issueID)
        {
            return _issueTests.Find(i => i.IssueID == issueID).Any();
        }
    }
}
