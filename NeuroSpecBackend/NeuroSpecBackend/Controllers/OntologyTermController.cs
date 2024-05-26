using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OntologyTermController : ControllerBase
    {
        private readonly IMongoCollection<OntologyTerm> _ontologyTerms;

        public OntologyTermController(NeuroDbContext context)
        {
            _ontologyTerms = context.OntologyTerms;
        }

        [HttpPost]
        public async Task<ActionResult<OntologyTerm>> InsertOntologyTerm(OntologyTerm ontologyTerm)
        {
            await _ontologyTerms.InsertOneAsync(ontologyTerm);
            return CreatedAtAction(nameof(GetOntologyTermById), new { id = ontologyTerm.Id }, ontologyTerm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOntologyTerm(string id, OntologyTerm ontologyTerm)
        {
            if (id != ontologyTerm.Id)
            {
                return BadRequest();
            }

            var result = await _ontologyTerms.ReplaceOneAsync(t => t.Id == id, ontologyTerm);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOntologyTerm(string id)
        {
            var result = await _ontologyTerms.DeleteOneAsync(t => t.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<OntologyTerm>> GetOntologyTermById([FromQuery] string id)
        {
            var ontologyTerm = await _ontologyTerms.Find(t => t.Id == id).FirstOrDefaultAsync();

            if (ontologyTerm == null)
            {
                return NotFound();
            }

            return ontologyTerm;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OntologyTerm>>> GetAllOntologyTerms()
        //{
        //    var ontologyTerms = await _ontologyTerms.Find(_ => true).ToListAsync();
        //    return ontologyTerms;
        //}

        private bool OntologyTermExists(string id)
        {
            return _ontologyTerms.Find(t => t.Id == id).Any();
        }
    }
}

