using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backapi.Models
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; } // Nav prop

        [Required] public string FacilityName { get; set; }

        // Maps to SQL 'bit'
        [Required] public bool Availability { get; set; } = true;
    }
}