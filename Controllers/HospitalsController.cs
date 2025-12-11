using Backapi.Models;
using Backapi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalRepository _hospitalRepo;

        public HospitalsController(IHospitalRepository hospitalRepo)
        {
            _hospitalRepo = hospitalRepo;
        }

        // Endpoint: GET api/hospitals/search?specialty=Cardiology
        [HttpGet("search")]
        public async Task<IActionResult> SearchHospitals([FromQuery] string specialty)
        {
            var results = await _hospitalRepo.GetHospitalsBySpecialtyAsync(specialty);

            if (results == null)
                return NotFound("No hospitals found matching criteria.");

            return Ok(results);
        }
    }
}