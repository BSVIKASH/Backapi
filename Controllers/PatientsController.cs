using Backapi.Data;
using Backapi.Models;
using Backapi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PatientsController(AppDbContext db) => _db = db;

        // ✅ 1. Get All Patients
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Patients.ToListAsync());

        // ✅ 2. Get Patient by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        // ✅ 3. NEW: Get Patient by Phone Number (Crucial for User Dashboard)
        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<IActionResult> GetByPhone(string phoneNumber)
        {
            var p = await _db.Patients.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (p == null)
                return NotFound(new { message = "Patient not found with this number" });

            return Ok(p);
        }

        // ✅ 4. Create New Patient (Signup)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDto dto)
        {
            // Optional: Check if phone already exists
            if (await _db.Patients.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber))
            {
                return BadRequest(new { message = "Phone number already registered" });
            }

            var p = new Patient
            {
                Name = dto.Name,
                Age = dto.Age,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                BloodGroup = dto.BloodGroup
            };
            _db.Patients.Add(p);
            await _db.SaveChangesAsync();
            return Ok(p);
        }

        // ✅ 5. Update Patient (Edit Profile)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return NotFound();

            p.Name = dto.Name;
            p.Age = dto.Age;
            p.Gender = dto.Gender;
            p.BloodGroup = dto.BloodGroup;
            // Note: We usually don't update PhoneNumber if it's their login ID, 
            // but we can leave it here if you want to allow it.
            p.PhoneNumber = dto.PhoneNumber;

            await _db.SaveChangesAsync();
            return Ok(p);
        }

        // ✅ 6. Delete Patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return NotFound();
            _db.Patients.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}