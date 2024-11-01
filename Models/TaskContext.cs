using Microsoft.EntityFrameworkCore;

namespace TaskManagementApplication.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Not Started" },
                new Status { Id = 2, Name = "In Progress" },
                new Status { Id = 3, Name = "Completed" },
                new Status { Id = 4, Name = "On Hold" },
                new Status { Id = 5, Name = "In Review" },
                new Status { Id = 6, Name = "Pending" }
            );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "High" },
                new Priority { Id = 2, Name = "Medium" },
                new Priority { Id = 3, Name = "Low" }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Project Zoo" },
                new Project { Id = 2, Name = "Project BirdPark" }
            );
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}