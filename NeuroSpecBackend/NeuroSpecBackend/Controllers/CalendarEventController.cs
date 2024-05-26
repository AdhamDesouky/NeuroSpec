using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarEventController : ControllerBase
    {
        private readonly IMongoCollection<CalendarEvent> _calendarEvents;

        public CalendarEventController(NeuroDbContext context)
        {
            _calendarEvents = context.CalendarEvents;
        }

        [HttpPost]
        public async Task<ActionResult<CalendarEvent>> InsertCalendarEvent(CalendarEvent calendarEvent)
        {
            await _calendarEvents.InsertOneAsync(calendarEvent);
            return CreatedAtAction(nameof(GetCalendarEventByID), new { eventID = calendarEvent.EventID }, calendarEvent);
        }

        [HttpPut("{eventID}")]
        public async Task<IActionResult> UpdateCalendarEvent(int eventID, CalendarEvent calendarEvent)
        {
            if (eventID != calendarEvent.EventID)
            {
                return BadRequest();
            }

            var result = await _calendarEvents.ReplaceOneAsync(ce => ce.EventID == eventID, calendarEvent);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{eventID}")]
        public async Task<ActionResult<CalendarEvent>> GetCalendarEventByID(int eventID)
        {
            var calendarEvent = await _calendarEvents.Find(ce => ce.EventID == eventID).FirstOrDefaultAsync();

            if (calendarEvent == null)
            {
                return NotFound();
            }

            return Ok(calendarEvent);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEvent>>> GetAllCalendarEvents()
        {
            var calendarEvents = await _calendarEvents.Find(_ => true).ToListAsync();
            return Ok(calendarEvents);
        }

        [HttpDelete("{eventID}")]
        public async Task<IActionResult> DeleteCalendarEvent(int eventID)
        {
            var result = await _calendarEvents.DeleteOneAsync(ce => ce.EventID == eventID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> CalendarEventExists(int eventID)
        {
            var count = await _calendarEvents.CountDocumentsAsync(ce => ce.EventID == eventID);
            return count > 0;
        }
    }
}
