using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutLoggerFrontEnd2.Models
{
    [Table("Exercises")]
    public class Exercises
    {
        [Key]
        public int ExerciseId { get; set; }
        public string? Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int WorkoutId { get; set; }
    }
}