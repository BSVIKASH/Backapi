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

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Patients.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DTOs.PatientDto dto)
        {
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DTOs.PatientDto dto)
        {
            var p = await _db.Patients.FindAsync(id);
            if (p == null) return NotFound();
            p.Name = dto.Name;
            p.Age = dto.Age;
            p.PhoneNumber = dto.PhoneNumber;
            p.Gender = dto.Gender;
            p.BloodGroup = dto.BloodGroup;
            await _db.SaveChangesAsync();
            return Ok(p);
        }

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
