using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Backapi.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }             // Primary Key
        [Required] public string Name { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string LicenceNumber { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; } // hashed password
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Facility> Facilities { get; set; }
    }
}
