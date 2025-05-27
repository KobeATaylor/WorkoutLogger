using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutLoggerAPI.Data;
using WorkoutLoggerAPI.DTOs;
using WorkoutLoggerAPI.Models;

namespace WorkoutLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController:ControllerBase
    {
        private readonly WorkoutLoggerDb _context;

        public WorkoutController(WorkoutLoggerDb context)
        {
            _context = context;
        }

        [HttpGet("GetWorkout")]

        public async Task<IActionResult> getWorkouts()
        {
            try
            {
                var workouts = await _context.Workouts.ToListAsync();
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostWorkout")]

        public async Task<IActionResult> PostWorkout(Workout workout)
        {
            try
            {
                _context.Workouts.AddAsync(workout);
                await _context.SaveChangesAsync();
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateWorkout")]
        public async Task<IActionResult> UpdateWorkout([FromBody] Workout updateWorkout)
        {
            var update = await _context.Workouts.FindAsync(updateWorkout.WorkoutId);
            if(update == null)
            {
                return NotFound();
            }
            update.Name = updateWorkout.Name;
            await _context.SaveChangesAsync();

            return Ok(update);
        }

        [HttpGet("GetWorkoutById")]
        public async Task<IActionResult> GetWorkoutById(int id)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercises)
                .FirstOrDefaultAsync(w => w.WorkoutId == id);

            if (workout == null)
                return NotFound();

            var dto = new WorkoutDetailDTO
            {
                WorkoutId = workout.WorkoutId,
                Name = workout.Name,
                Exercises = workout.WorkoutExercises.Select(we => new ExerciseDisplayDTO
                {
                    ExerciseId = we.ExerciseId,
                    Name = we.Exercises.Name,
                    Sets = we.Exercises.Sets,
                    Reps = we.Exercises.Reps
                }).ToList()
            };

            return Ok(dto);
        }



    }
}
