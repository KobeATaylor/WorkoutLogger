using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorkoutLoggerAPI.Models
{
    [Table("Exercises")]
    public class Exercises
    {
        [Key]
        public int ExerciseId { get; set; }
        public string? Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        [JsonIgnore]

        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

    }
}