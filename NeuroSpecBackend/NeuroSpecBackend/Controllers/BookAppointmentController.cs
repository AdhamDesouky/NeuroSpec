using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAppointmentController : Controller
    {
        private readonly IMongoCollection<BookAppointmentRequest> _bookAppointmentRequests;
        public BookAppointmentController(NeuroDbContext context)
        {
            _bookAppointmentRequests = context.BookAppointmentRequests;
        }

        [HttpPost]
        public async Task<ActionResult<BookAppointmentRequest>> InsertBookAppointmentRequest(BookAppointmentRequest bookAppointmentRequest)
        {
            await _bookAppointmentRequests.InsertOneAsync(bookAppointmentRequest);
            return CreatedAtAction(nameof(GetBookAppointmentRequestByID), new { bookAppointmentRequestID = bookAppointmentRequest.BookAppointmentRequestID }, bookAppointmentRequest);
        }
        [HttpGet]
        public async Task<ActionResult<BookAppointmentRequest>> GetBookAppointmentRequestByID(int bookAppointmentRequestID)
        {
            var bookAppointmentRequest = await _bookAppointmentRequests.Find(b => b.BookAppointmentRequestID == bookAppointmentRequestID).FirstOrDefaultAsync();
            if (bookAppointmentRequest == null)
            {
                return NotFound();
            }
            return bookAppointmentRequest;
        }
        [HttpPut("{bookAppointmentRequestID}")]
        public async Task<IActionResult> UpdateBookAppointmentRequest(int bookAppointmentRequestID, BookAppointmentRequest bookAppointmentRequest)
        {
            if (bookAppointmentRequestID != bookAppointmentRequest.BookAppointmentRequestID)
            {
                return BadRequest();
            }
            var result = await _bookAppointmentRequests.ReplaceOneAsync(b => b.BookAppointmentRequestID == bookAppointmentRequestID, bookAppointmentRequest);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{bookAppointmentRequestID}")]
        public async Task<IActionResult> DeleteBookAppointmentRequest(int bookAppointmentRequestID)
        {
            var result = await _bookAppointmentRequests.DeleteOneAsync(b => b.BookAppointmentRequestID == bookAppointmentRequestID);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        //confirm by receptionist
        [HttpPut("{bookAppointmentRequestID}/mark-as-confirmed")]
        public async Task<IActionResult> MarkAsDone(int bookAppointmentRequestID)
        {
            var bookAppointmentRequest = await _bookAppointmentRequests.Find(b => b.BookAppointmentRequestID == bookAppointmentRequestID).FirstOrDefaultAsync();
            if (bookAppointmentRequest == null)
            {
                return NotFound();
            }
            bookAppointmentRequest.IsConfirmed = true;
            var result = await _bookAppointmentRequests.ReplaceOneAsync(b => b.BookAppointmentRequestID == bookAppointmentRequestID, bookAppointmentRequest);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        //get all not confirmed appointments
        [HttpGet("not-confirmed")]
        public async Task<ActionResult<List<BookAppointmentRequest>>> GetAllNotConfirmedBookAppointmentRequests()
        {
            var bookAppointmentRequests = await _bookAppointmentRequests.Find(b => b.IsConfirmed == false).ToListAsync();
            if (bookAppointmentRequests == null)
            {
                return NotFound();
            }
            return bookAppointmentRequests;
        }

        //get all patient appointments
        [HttpGet("by-patient-id/{patientID}")]
        public async Task<ActionResult<List<BookAppointmentRequest>>> GetAllBookAppointmentRequestsByPatientID(int patientID)
        {
            var bookAppointmentRequests = await _bookAppointmentRequests.Find(b => b.PatientID == patientID).ToListAsync();
            if (bookAppointmentRequests == null)
            {
                return NotFound();
            }
            return bookAppointmentRequests;
        }



        //in the visit controller
        //[HttpGet("available-time-slots-on-day/{selectedDay}/{DoctorID}")]
        //public async Task<ActionResult<List<string>>> GetAvailableTimeSlotsOnDay(DateTime selectedDay, int DoctorID)
        //{
        //    var bookAppointmentRequests = await _bookAppointmentRequests.Find(b => b.AppointmentTime.Date == selectedDay.Date && b.DoctorID == DoctorID).ToListAsync();
        //    if (bookAppointmentRequests == null)
        //    {
        //        return NotFound();
        //    }
        //    var availableTimeSlots = new List<string>();
        //    var timeSlots = new List<string> { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00" };
        //    foreach (var timeSlot in timeSlots)
        //    {
        //        var isAvailable = true;
        //        foreach (var bookAppointmentRequest in bookAppointmentRequests)
        //        {
        //            if (bookAppointmentRequest.AppointmentTime.ToString("HH:mm") == timeSlot)
        //            {
        //                isAvailable = false;
        //                break;
        //            }
        //        }
        //        if (isAvailable)
        //        {
        //            availableTimeSlots.Add(timeSlot);
        //        }
        //    }
        //    return availableTimeSlots;
        //}


    }
}
