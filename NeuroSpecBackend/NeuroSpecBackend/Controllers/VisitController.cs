using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

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

        [HttpGet("byPatientID/{patientID}")]
        public async Task<ActionResult<List<Visit>>> GetAllVisitsByPatientID(int patientID)
        {
            var visits = await _visits.Find(v => v.PatientID == patientID).ToListAsync();

            if (visits == null)
            {
                return NotFound();
            }

            return visits;
        }
        private bool VisitExists(int visitID)
        {
            return _visits.Find(v => v.VisitID == visitID).Any();
        }
        //get all visits by doctor id
        [HttpGet("byDoctorID/{doctorID}")]
        public async Task<ActionResult<List<Visit>>> GetAllVisitsByDoctorID(int doctorID)
        {
            var visits = await _visits.Find(v => v.DoctorID == doctorID).ToListAsync();

            if (visits == null)
            {
                return NotFound();
            }

            return visits;
        }

        //get all available time slots on day for doctor
        [HttpGet("available-time-slots-on-day/{selectedDay}/{DoctorID}")]
        public async Task<ActionResult<List<string>>> GetAvailableTimeSlotsOnDay(DateTime selectedDay, int DoctorID)
        {
            var visits = await _visits.Find(b => b.TimeStamp.Date == selectedDay.Date && b.DoctorID == DoctorID).ToListAsync();
            if (visits == null)
            {
                return NotFound();
            }
            var availableTimeSlots = new List<string>();
            var timeSlots = new List<string> { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00" };
            foreach (var timeSlot in timeSlots)
            {
                var isAvailable = true;
                foreach (var visit in visits)
                {
                    if (visit.TimeStamp.ToString("HH:mm") == timeSlot)
                    {
                        isAvailable = false;
                        break;
                    }
                }
                if (isAvailable)
                {
                    availableTimeSlots.Add(timeSlot);
                }
            }
            return availableTimeSlots;
        }

        [HttpGet("byDate/{selectedDay}")]
        public async Task<ActionResult<List<Visit>>> GetAllVisitsOnDate(DateTime selectedDay)
        {
            // Define the start and end of the day for the selected date
            var startOfDay = selectedDay.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            // Use a range query to find visits within the given date
            var visits = await _visits.Find(v =>
                v.TimeStamp >= startOfDay &&
                v.TimeStamp <= endOfDay
            ).ToListAsync();

            if (visits == null || visits.Count == 0)
            {
                return NotFound();
            }

            return visits;
        }


        //get all visits by doctor id on date
        [HttpGet("byDoctorID/{doctorID}/onDate/{dateTime}")]
        public async Task<ActionResult<List<Visit>>> GetAllVisitsByDoctorIDOnDate(int doctorID, DateTime dateTime)
        {
            // Define the start and end of the day for the given date
            var startOfDay = dateTime.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            // Use a range query to find visits within the given date
            var visits = await _visits.Find(v =>
                v.DoctorID == doctorID &&
                v.TimeStamp >= startOfDay &&
                v.TimeStamp <= endOfDay
            ).ToListAsync();

            if (visits == null || visits.Count == 0)
            {
                return NotFound();
            }

            return visits;
        }




        //get future doctor visits
        [HttpGet("futureVisitsByDoctorID/{doctorID}")]
        public async Task<ActionResult<List<Visit>>> GetFutureDoctorVisits(int doctorID)
        {
            var now = DateTime.Now;
            var visits = await _visits.Find(v => v.DoctorID == doctorID && v.TimeStamp >= now).ToListAsync();

            if (visits == null || visits.Count == 0)
            {
                return NotFound();
            }

            return visits;
        }
    
    }
}
