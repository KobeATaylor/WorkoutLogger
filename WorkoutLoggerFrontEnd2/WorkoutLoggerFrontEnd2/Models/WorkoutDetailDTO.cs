namespace WorkoutLoggerFrontEnd2.Models
{
    public class WorkoutDetailDTO
    {
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
        public List<ExerciseDisplayDTO> Exercises { get; set; } = new();
    }
}
