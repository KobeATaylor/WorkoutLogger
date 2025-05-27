namespace WorkoutLoggerAPI.DTOs
{
    public class WorkoutDisplayDTO
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public List<ExerciseDisplayDTO> Exercises { get; set; }
    }
}
