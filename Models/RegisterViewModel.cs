using System.ComponentModel.DataAnnotations;

namespace JPCS.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string StudentFacultyId { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ContactNumber { get; set; }
    }
}
