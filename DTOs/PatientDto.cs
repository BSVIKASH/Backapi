using System.ComponentModel.DataAnnotations;

namespace Backapi.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string BloodGroup { get; set; }
    }
}
