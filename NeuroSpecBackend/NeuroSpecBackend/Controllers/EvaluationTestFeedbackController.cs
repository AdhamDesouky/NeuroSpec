using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroSpecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationTestFeedbackController : ControllerBase
    {
        private readonly IMongoCollection<EvaluationTestFeedBack> _evaluationTestFeedbacks;

        public EvaluationTestFeedbackController(NeuroDbContext context)
        {
            _evaluationTestFeedbacks = context.EvaluationTestFeedbacks;
        }

        [HttpPost]
        public async Task<ActionResult<EvaluationTestFeedBack>> InsertFeedback(EvaluationTestFeedBack feedback)
        {
            await _evaluationTestFeedbacks.InsertOneAsync(feedback);
            return CreatedAtAction(nameof(GetFeedbackById), new { feedbackId = feedback.TestFeedBackID }, feedback);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationTestFeedBack>>> GetAllFeedback()
        {
            var feedbacks = await _evaluationTestFeedbacks.Find(_ => true).ToListAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{feedbackId:int}")]
        public async Task<ActionResult<EvaluationTestFeedBack>> GetFeedbackById(int feedbackId)
        {
            var feedback = await _evaluationTestFeedbacks.Find(f => f.TestFeedBackID == feedbackId).FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpGet("ByPatient/{patientId:int}")]
        public async Task<ActionResult<IEnumerable<EvaluationTestFeedBack>>> GetFeedbackByPatient(int patientId)
        {
            var feedbacks = await _evaluationTestFeedbacks.Find(f => f.PatientID == patientId).ToListAsync();
            return Ok(feedbacks);
        }

        [HttpGet("ByVisit/{visitId:int}")]
        public async Task<ActionResult<IEnumerable<EvaluationTestFeedBack>>> GetFeedbackByVisit(int visitId)
        {
            var feedbacks = await _evaluationTestFeedbacks.Find(f => f.VisitID == visitId).ToListAsync();
            return Ok(feedbacks);
        }

        [HttpDelete("{feedbackId:int}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var result = await _evaluationTestFeedbacks.DeleteOneAsync(f => f.TestFeedBackID == feedbackId);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("ByVisit/{visitId:int}")]
        public async Task<IActionResult> DeleteAllVisitFeedback(int visitId)
        {
            var feedbacks = await _evaluationTestFeedbacks.Find(f => f.VisitID == visitId).ToListAsync();
            if (feedbacks == null || !feedbacks.Any())
            {
                return NotFound();
            }

            var feedbackIds = feedbacks.Select(f => f.TestFeedBackID);
            var deleteResult = await _evaluationTestFeedbacks.DeleteManyAsync(f => feedbackIds.Contains(f.TestFeedBackID));

            if (deleteResult.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
