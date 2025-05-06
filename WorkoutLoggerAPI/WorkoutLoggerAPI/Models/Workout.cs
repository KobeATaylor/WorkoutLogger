using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutLoggerAPI.Models
{
    [Table("Workout")]
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
    }
}