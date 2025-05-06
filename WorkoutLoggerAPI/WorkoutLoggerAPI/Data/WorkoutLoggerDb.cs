using Microsoft.EntityFrameworkCore;
using WorkoutLoggerAPI.Models;

namespace WorkoutLoggerAPI.Data
{
    public class WorkoutLoggerDb : DbContext
    {
        public WorkoutLoggerDb(DbContextOptions<WorkoutLoggerDb> options) :base(options){}

        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}