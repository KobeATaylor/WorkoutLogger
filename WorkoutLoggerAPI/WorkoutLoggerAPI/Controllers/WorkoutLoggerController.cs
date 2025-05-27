using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutLoggerAPI.Data;
using WorkoutLoggerAPI.DTOs;
using WorkoutLoggerAPI.Models;

namespace WorkoutLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutLoggerController : ControllerBase
    {
        private readonly WorkoutLoggerDb _context;

        public WorkoutLoggerController(WorkoutLoggerDb context)
        {
            _context = context;
        }

        [HttpGet("GetWorkout")]
        public async Task<IActionResult> GetWorkout()
        {
            var workouts = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercises)
                .ToListAsync();

            var result = workouts.Select(w => new WorkoutDisplayDTO
            {
                WorkoutId = w.WorkoutId,
                Name = w.Name,
                Exercises = w.WorkoutExercises.Select(we => new ExerciseDisplayDTO
                {
                    Name = we.Exercises.Name,
                    Sets = we.Exercises.Sets,
                    Reps = we.Exercises.Reps
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpDelete("DeleteWorkout")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.WorkoutId == id);

            if (workout == null) return NotFound();

            // Remove linked WorkoutExercises first (if cascade delete isn't on)
            _context.WorkoutExercises.RemoveRange(workout.WorkoutExercises);

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("AssignExerciseToWorkout")]
        public async Task <IActionResult> AssignExerciseToWorkout(WorkoutExerciseDTO workoutexercise)
        {
            Workout? workout = await _context.Workouts.FindAsync(workoutexercise.WorkoutId);
            Exercises? exercises = await _context.Exercises.FindAsync(workoutexercise.ExerciseId);

            if (workout == null)
            {
                return NotFound("Workout Not Found");
            }
            if (exercises == null)
            {
                return NotFound("Exercise Not Found");
            }
            WorkoutExercise workoutExercise = new WorkoutExercise
            {
                WorkoutId = workoutexercise.WorkoutId,
                ExerciseId = workoutexercise.ExerciseId,
            };
            await _context.WorkoutExercises.AddAsync(workoutExercise);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("RemoveExerciseFromWorkout")]
        public IActionResult RemoveExerciseFromWorkout(int workoutId, int exerciseId)
        {
            var we = _context.WorkoutExercises.FirstOrDefault(
                x => x.WorkoutId == workoutId && x.ExerciseId == exerciseId);

            if (we == null) return NotFound();

            _context.WorkoutExercises.Remove(we);
            _context.SaveChanges();
            return Ok();
        }
    }
}