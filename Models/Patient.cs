using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backapi.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required] public string Name { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string BloodGroup { get; set; }

        // ✅ Matches SQL nvarchar(255)
        public string? Email { get; set; }

        // ✅ Matches SQL nvarchar(max)
        public string? PasswordHash { get; set; }
    }
}