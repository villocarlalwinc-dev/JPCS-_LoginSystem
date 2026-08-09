using System;
using System.Collections.Generic;

namespace JPCS.Models
{
    public static class DataStore
    {
        public static List<User> Users { get; set; } = new List<User>();

        public static List<Announcement> Announcements { get; set; } = new List<Announcement>
        {
            new Announcement
            {
                Id = 1,
                Title = "Welcome!",
                Content = "Welcome to the  Home Page.",
                DatePosted = new DateTime(2026, 8, 1)
            },
            new Announcement
            {
                Id = 2,
                Title = "Enrollment Update",
                Content = "Enrollment for next semester starts soon.",
                DatePosted = new DateTime(2026, 8, 5)
            }
        };

        private static int _nextUserId = 1;

        public static int GetNextUserId() => _nextUserId++;
    }
}
