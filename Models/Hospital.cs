using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backapi.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string LicenceNumber { get; set; }
        [Required] public string Email { get; set; } // Your DB set nvarchar(450)
        [Required] public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ CRITICAL FIX: Map Uppercase C# to Lowercase SQL Column
        // ✅ CRITICAL FIX: Precision matched to your screenshot (10,8)
        [Column("latitude", TypeName = "decimal(10, 8)")]
        public decimal Latitude { get; set; }

        [Column("longitude", TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }

        // --- NAVIGATION ---
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Facility> Facilities { get; set; }
    }
}