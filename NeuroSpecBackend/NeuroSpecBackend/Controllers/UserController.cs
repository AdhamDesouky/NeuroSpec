using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly NeuroDbContext _context;

        public UserController(NeuroDbContext context)
        {
            _context = context;
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                await _context.Users.InsertOneAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _context.Users.DeleteOneAsync(u => u.UserID == id);
                if (result.DeletedCount == 0)
                {
                    return NotFound();
                }
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.Find(_ => true).ToListAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.Find(u => u.UserID == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/User/GetAllEmployees
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Users.Find(u => u.UserID.ToString().StartsWith("3")).ToListAsync();
            return Ok(employees);
        }

        // GET: api/User/GetAllDoctors
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Users.Find(u => u.UserID.ToString().StartsWith("2")).ToListAsync();
            return Ok(doctors);
        }

        // GET: api/User/GetAllAdmins
        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _context.Users.Find(u => u.UserID.ToString().StartsWith("1")).ToListAsync();
            return Ok(admins);
        }

        // GET: api/User/GetDoctorsWithVisitsOnDate/2024-05-01
        //[HttpGet("GetDoctorsWithVisitsOnDate/{specificDate}")]
        //public async Task<IActionResult> GetDoctorsWithVisitsOnDate(DateTime specificDate)
        //{
        //    var doctors = await _context.Users
        //        .Find(u => u.UserID.ToString().StartsWith("2") && u.Visits.Any(v => v.TimeStamp.Date == specificDate.Date))
        //        .ToListAsync();
        //    return Ok(doctors);
        //}

        // GET: api/User/GetUserByNID/12345
        [HttpGet("GetUserByNID/{nid}")]
        public async Task<IActionResult> GetUserByNID(string nid)
        {
            var user = await _context.Users.Find(u => u.NationalID == nid).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
