using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecBackend.Model;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : Controller
    {
        private readonly IMongoCollection<Exercise> _exercises;
        public ExerciseController(NeuroDbContext context)
        {
            _exercises = context.Exercises;
        }
        [HttpPost]
        public async Task<ActionResult<Exercise>> InsertExercise(Exercise exercise)
        {
            await _exercises.InsertOneAsync(exercise);
            return CreatedAtAction(nameof(GetExerciseById), new { exerciseID = exercise.ExerciseID }, exercise);
        }
        [HttpGet]
        public async Task<ActionResult<List<Exercise>>> GetAllExercises()
        {
            var exercises = await _exercises.Find(_ => true).ToListAsync();
            return Ok(exercises);
        }
        [HttpGet("{exerciseID:int}")]
        public async Task<ActionResult<Exercise>> GetExerciseById(int exerciseID)
        {
            var exercise = await _exercises.Find(e => e.ExerciseID == exerciseID).FirstOrDefaultAsync();
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }
        [HttpDelete("{exerciseID:int}")]
        public async Task<ActionResult> DeleteExercise(int exerciseID)
        {
            var result = await _exercises.DeleteOneAsync(e => e.ExerciseID == exerciseID);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
