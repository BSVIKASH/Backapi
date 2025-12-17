using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backapi.Models
{
    public class EmergencyRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int HospitalId { get; set; }

        // Use ? because DB allows NULL for these
        public string? PatientName { get; set; }
        public string? ContactNumber { get; set; }
        public string? SymptomDescription { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
    }
}