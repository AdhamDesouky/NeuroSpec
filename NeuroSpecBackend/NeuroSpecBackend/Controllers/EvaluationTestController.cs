using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationTestController : ControllerBase
    {
        private readonly IMongoCollection<EvaluationTest> _evaluationTests;

        public EvaluationTestController(NeuroDbContext context)
        {
            _evaluationTests = context.EvaluationTests;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationTest>>> GetAllTests()
        {
            var tests = await _evaluationTests.Find(_ => true).ToListAsync();
            return Ok(tests);
        }

        [HttpGet("{testId:int}")]
        public async Task<ActionResult<EvaluationTest>> GetTestById(int testId)
        {
            var test = await _evaluationTests.Find(t => t.TestID == testId).FirstOrDefaultAsync();

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [HttpPost]
        public async Task<ActionResult<EvaluationTest>> InsertTest(EvaluationTest test)
        {
            await _evaluationTests.InsertOneAsync(test);
            return CreatedAtAction(nameof(GetTestById), new { testId = test.TestID }, test);
        }

        [HttpPut("{testId:int}")]
        public async Task<IActionResult> UpdateTest(int testId, EvaluationTest test)
        {
            if (testId != test.TestID)
            {
                return BadRequest();
            }

            var result = await _evaluationTests.ReplaceOneAsync(t => t.TestID == testId, test);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{testId:int}")]
        public async Task<IActionResult> DeleteTest(int testId)
        {
            var result = await _evaluationTests.DeleteOneAsync(t => t.TestID == testId);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> TestExists(int testId)
        {
            var count = await _evaluationTests.CountDocumentsAsync(t => t.TestID == testId);
            return count > 0;
        }
    }
}
