using System;

namespace JPCS.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; } = false;
    }
}
