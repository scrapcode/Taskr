using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskr.API.Entities;
using Taskr.API.Models;

namespace Taskr.API.Data
{
    public class TaskrDbContext : IdentityDbContext<User>
    {
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();

        public TaskrDbContext(DbContextOptions<TaskrDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Test Task 1",
                    Content = "Test content 1"
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Test Task 2",
                    Content = "Test content 2"
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Test Task 3",
                    Content = "Test content 3"
                }
             );
        }
    }
}
