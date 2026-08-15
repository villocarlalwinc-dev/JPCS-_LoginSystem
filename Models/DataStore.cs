using System;
using System.Collections.Generic;

namespace JPCS.Models
{
    public static class DataStore
    {
        public static List<User> Users { get; set; } = new List<User>();

        public static List<Book> Books { get; set; } = new List<Book>
        {
            new Book { Id = 1, Title = "Introduction to Algorithms", Author = "Thomas Cormen" },
            new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin" },
            new Book { Id = 3, Title = "The Pragmatic Programmer", Author = "Andrew Hunt" },
            new Book { Id = 4, Title = "Design Patterns", Author = "Erich Gamma" },
            new Book { Id = 5, Title = "Database System Concepts", Author = "Abraham Silberschatz" },
            new Book { Id = 6, Title = "Computer Networking", Author = "James Kurose" }
        };

        public static List<Rental> Rentals { get; set; } = new List<Rental>();

        public static List<Announcement> Announcements { get; set; } = new List<Announcement>
        {
            new Announcement { Id = 1, Title = "Welcome!", Content = "Welcome to the LSHCG Student Portal.", DatePosted = new DateTime(2026, 8, 1) },
            new Announcement { Id = 2, Title = "Enrollment Update", Content = "Enrollment for next semester starts soon.", DatePosted = new DateTime(2026, 8, 5) }
        };

        private static int _nextUserId = 1;
        private static int _nextBookId = 7;
        private static int _nextRentalId = 1;

        public static int GetNextUserId() => _nextUserId++;
        public static int GetNextBookId() => _nextBookId++;
        public static int GetNextRentalId() => _nextRentalId++;
    }
}
