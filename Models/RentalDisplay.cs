using System;

namespace JPCS.Models
{
    public class RentalDisplay
    {
        public int RentalId { get; set; }
        public string BookTitle { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsOverdue { get; set; }
    }
}
