using WorkoutLoggerAPI.Models;

public class WorkoutExercise
{
    public int WorkoutId { get; set; }
    public Workout? Workout { get; set; }

    public int ExerciseId { get; set; }
    public Exercises? Exercises { get; set; }
  
}
