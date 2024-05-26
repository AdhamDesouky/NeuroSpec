using Microsoft.AspNetCore.Mvc;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IMongoCollection<Visit> _visits;

        public VisitController(NeuroDbContext context)
        {
            _visits = context.Visits;
        }

        [HttpPost]
        public async Task<ActionResult<Visit>> InsertVisit(Visit visit)
        {
            await _visits.InsertOneAsync(visit);
            return CreatedAtAction(nameof(GetVisitByID), new { visitID = visit.VisitID }, visit);
        }

        [HttpPut("{visitID}")]
        public async Task<IActionResult> UpdateVisit(int visitID, Visit visit)
        {
            if (visitID != visit.VisitID)
            {
                return BadRequest();
            }

            var result = await _visits.ReplaceOneAsync(v => v.VisitID == visitID, visit);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{visitID}")]
        public async Task<IActionResult> DeleteVisit(int visitID)
        {
            var result = await _visits.DeleteOneAsync(v => v.VisitID == visitID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{visitID}")]
        public async Task<ActionResult<Visit>> GetVisitByID(int visitID)
        {
            var visit = await _visits.Find(v => v.VisitID == visitID).FirstOrDefaultAsync();

            if (visit == null)
            {
                return NotFound();
            }

            return visit;
        }

        // Add other actions similar to above...

        private bool VisitExists(int visitID)
        {
            return _visits.Find(v => v.VisitID == visitID).Any();
        }
    }
}
