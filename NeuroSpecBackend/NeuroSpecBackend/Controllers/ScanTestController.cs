using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanTestController : ControllerBase
    {
        private readonly IMongoCollection<ScanTest> _scanTests;

        public ScanTestController(NeuroDbContext context)
        {
            _scanTests = context.ScanTests;
        }

        [HttpPost]
        public async Task<ActionResult<ScanTest>> InsertScanTest(ScanTest scanTest)
        {
            await _scanTests.InsertOneAsync(scanTest);
            return CreatedAtAction(nameof(GetScanTestById), new { scanTestID = scanTest.ScanTestID }, scanTest);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScanTest>>> GetAllScanTests()
        {
            var scanTests = await _scanTests.Find(_ => true).ToListAsync();
            return Ok(scanTests);
        }

        [HttpGet("{scanTestID}")]
        public async Task<ActionResult<ScanTest>> GetScanTestById(int scanTestID)
        {
            var scanTest = await _scanTests.Find(s => s.ScanTestID == scanTestID).FirstOrDefaultAsync();

            if (scanTest == null)
            {
                return NotFound();
            }

            return scanTest;
        }

        [HttpPut("{scanTestID}")]
        public async Task<IActionResult> UpdateScanTest(int scanTestID, ScanTest scanTest)
        {
            if (scanTestID != scanTest.ScanTestID)
            {
                return BadRequest();
            }

            var result = await _scanTests.ReplaceOneAsync(s => s.ScanTestID == scanTestID, scanTest);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{scanTestID}")]
        public async Task<IActionResult> DeleteScanTest(int scanTestID)
        {
            var result = await _scanTests.DeleteOneAsync(s => s.ScanTestID == scanTestID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool ScanTestExists(int scanTestID)
        {
            return _scanTests.Find(s => s.ScanTestID == scanTestID).Any();
        }
    }
}
