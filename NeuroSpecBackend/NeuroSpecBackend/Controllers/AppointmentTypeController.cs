using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly NeuroDbContext _context;

        public AppointmentTypeController(NeuroDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentType>> InsertAppointmentType(AppointmentType appointmentType)
        {
            await _context.AppointmentTypes.InsertOneAsync(appointmentType);
            return CreatedAtAction(nameof(GetAppointmentTypeByID), new { id = appointmentType.TypeID }, appointmentType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentType>>> GetAllAppointmentTypes()
        {
            var appointmentTypes = await _context.AppointmentTypes.Find(_ => true).ToListAsync();
            return Ok(appointmentTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentType>> GetAppointmentTypeByID(int id)
        {
            var appointmentType = await _context.AppointmentTypes.Find(at => at.TypeID == id).FirstOrDefaultAsync();

            if (appointmentType == null)
            {
                return NotFound();
            }

            return Ok(appointmentType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointmentType(int id, AppointmentType appointmentType)
        {
            if (id != appointmentType.TypeID)
            {
                return BadRequest();
            }

            var result = await _context.AppointmentTypes.ReplaceOneAsync(at => at.TypeID == id, appointmentType);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentType(int id)
        {
            var result = await _context.AppointmentTypes.DeleteOneAsync(at => at.TypeID == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> AppointmentTypeExists(int id)
        {
            var count = await _context.AppointmentTypes.CountDocumentsAsync(at => at.TypeID == id);
            return count > 0;
        }
    }
}
