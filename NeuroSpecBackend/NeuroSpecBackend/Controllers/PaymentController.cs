using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IMongoCollection<Payment> _payments;

        public PaymentController(NeuroDbContext context)
        {
            _payments = context.Payments;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> InsertPayment(Payment payment)
        {
            await _payments.InsertOneAsync(payment);
            return CreatedAtAction(nameof(GetPaymentByID), new { paymentID = payment.PaymentID }, payment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            var payments = await _payments.Find(_ => true).ToListAsync();
            return Ok(payments);
        }

        [HttpGet("{paymentID}")]
        public async Task<ActionResult<Payment>> GetPaymentByID(int paymentID)
        {
            var payment = await _payments.Find(p => p.PaymentID == paymentID).FirstOrDefaultAsync();

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpGet("ByPatient/{patientID}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPatientPayments(int patientID)
        {
            var payments = await _payments.Find(p => p.PatientID == patientID).ToListAsync();
            return Ok(payments);
        }

        [HttpGet("ByDoctor/{doctorID}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetDoctorPayments(int doctorID)
        {
            var payments = await _payments.Find(p => p.DoctorID == doctorID).ToListAsync();
            return Ok(payments);
        }

        [HttpPut("{paymentID}")]
        public async Task<IActionResult> UpdatePayment(int paymentID, Payment payment)
        {
            if (paymentID != payment.PaymentID)
            {
                return BadRequest();
            }

            var result = await _payments.ReplaceOneAsync(p => p.PaymentID == paymentID, payment);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{paymentID}")]
        public async Task<IActionResult> DeletePayment(int paymentID)
        {
            var result = await _payments.DeleteOneAsync(p => p.PaymentID == paymentID);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
