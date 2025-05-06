using Microsoft.AspNetCore.Mvc;
using WorkoutLoggerAPI.Data;
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
        public IActionResult GetWorkout()
        {
            return Ok(_context.Workouts.ToList());
        }
        [HttpGet("GetExercises")]
        public IActionResult GetExercises()
        {
            return Ok(_context.Exercises.ToList());
        }
        [HttpPost("PostWorkout")]
        public void PostWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }
        [HttpPost("PostExercise")]
        public void PostExercise(Exercises exercises)
        {
            _context.Exercises.Add(exercises);
            _context.SaveChanges();
        }

        [HttpPut("UpdateWorkout")]
        public void UpdateWorkout(Workout workout)
        {
            Workout workoutUpdate = _context.Workouts.FirstOrDefault((x) => x.WorkoutId == workout.WorkoutId);
            workoutUpdate.Name = workout.Name;
            _context.SaveChanges();
        }
        [HttpPut("UpdateExercise")]
        public void UpdateExercise(Exercises exercise)
        {
            Exercises exerciseUpdate = _context.Exercises.FirstOrDefault((x) => x.ExerciseId == exercise.WorkoutId);
            exerciseUpdate.Name = exercise.Name;
            exerciseUpdate.Sets = exercise.Sets;
            exerciseUpdate.Reps = exercise.Reps;
            exerciseUpdate.WorkoutId = exercise.WorkoutId;
            _context.SaveChanges();
        }
        [HttpDelete("DeleteWorkout")]
        public void DeleteWorkout(long id)
        {
            var workout = _context.Workouts.FirstOrDefault(c => c.WorkoutId == id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }
        }
        [HttpDelete("DeleteExercise")]
        public void DeleteExercise(long id)
        {
            var exercise = _context.Exercises.FirstOrDefault(c => c.ExerciseId == id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                _context.SaveChanges();
            }
        }
    }
}