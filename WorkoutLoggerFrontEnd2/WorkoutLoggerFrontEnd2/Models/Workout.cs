using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutLoggerFrontEnd2.Models
{
    [Table("Workout")]
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
        public List<Exercises> Exercises { get; set; } = new();
    }
}