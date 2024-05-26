using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceRecordController : ControllerBase
    {
        private readonly NeuroDbContext _context;

        public AttendanceRecordController(NeuroDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<AttendanceRecord>> InsertAttendanceRecord(AttendanceRecord attendanceRecord)
        {
            await _context.AttendanceRecords.InsertOneAsync(attendanceRecord);
            return CreatedAtAction(nameof(GetAttendanceRecordByID), new { recordID = attendanceRecord.RecordID }, attendanceRecord);
        }

        [HttpPut("{recordID}")]
        public async Task<IActionResult> UpdateAttendanceRecord(int recordID, AttendanceRecord attendanceRecord)
        {
            if (recordID != attendanceRecord.RecordID)
            {
                return BadRequest();
            }

            var result = await _context.AttendanceRecords.ReplaceOneAsync(ar => ar.RecordID == recordID, attendanceRecord);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{recordID}")]
        public async Task<IActionResult> DeleteAttendanceRecord(int recordID)
        {
            var result = await _context.AttendanceRecords.DeleteOneAsync(ar => ar.RecordID == recordID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{recordID}")]
        public async Task<ActionResult<AttendanceRecord>> GetAttendanceRecordByID(int recordID)
        {
            var attendanceRecord = await _context.AttendanceRecords.Find(ar => ar.RecordID == recordID).FirstOrDefaultAsync();

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return Ok(attendanceRecord);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetAllAttendanceRecords()
        {
            var attendanceRecords = await _context.AttendanceRecords.Find(_ => true).ToListAsync();
            return Ok(attendanceRecords);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetAttendanceRecordsByDate(DateTime date)
        {
            var attendanceRecords = await _context.AttendanceRecords.Find(ar => ar.TimeStamp.Date == date.Date).ToListAsync();
            return Ok(attendanceRecords);
        }

        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetUserAttendanceRecords(int userID)
        {
            var attendanceRecords = await _context.AttendanceRecords.Find(ar => ar.UserID == userID).ToListAsync();
            return Ok(attendanceRecords);
        }

        private async Task<bool> AttendanceRecordExists(int recordID)
        {
            var count = await _context.AttendanceRecords.CountDocumentsAsync(ar => ar.RecordID == recordID);
            return count > 0;
        }
    }
}
