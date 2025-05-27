using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutLoggerAPI.Data;
using WorkoutLoggerAPI.Models;

namespace WorkoutLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly WorkoutLoggerDb _context;

        public ExerciseController(WorkoutLoggerDb context)
        {
            _context = context;
        }

        [HttpGet("GetExercise")]
        public async Task<IActionResult> GetExercise()
        {
            try
            {
                var exercises = await _context.Exercises.ToListAsync();
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostExercise")]
        public async Task<IActionResult> PostExercise(Exercises exercises)
        {
            try
            {
                _context.Exercises.Add(exercises);
                await _context.SaveChangesAsync();

                return Ok(exercises);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}