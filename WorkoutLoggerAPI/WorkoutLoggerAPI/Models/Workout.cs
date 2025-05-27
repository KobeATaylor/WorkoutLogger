using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorkoutLoggerAPI.Models
{
    [Table("Workout")]
    public class Workout
    {
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
    }
}