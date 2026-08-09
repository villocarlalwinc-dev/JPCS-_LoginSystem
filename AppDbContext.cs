using Microsoft.EntityFrameworkCore;
using JPCS.Models;
using System;

namespace JPCS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed sample announcements so the dashboard has content right away
            modelBuilder.Entity<Announcement>().HasData(
                new Announcement
                {
                    Id = 1,
                    Title = "Welcome!",
                    Content = "Welcome to the LSHCG Student Portal.",
                    DatePosted = new DateTime(2026, 8, 1)
                },
                new Announcement
                {
                    Id = 2,
                    Title = "Enrollment Update",
                    Content = "Enrollment for next semester starts soon.",
                    DatePosted = new DateTime(2026, 8, 5)
                }
            );
        }
    }
}
