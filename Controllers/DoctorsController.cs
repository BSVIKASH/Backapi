using Backapi.Data;
using Backapi.DTOs;
using Backapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ✅ Required for .Include()
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public DoctorsController(AppDbContext db) => _db = db;

        private int GetHospitalIdFromClaim()
        {
            var claim = User.FindFirst("hospitalId")?.Value;
            if (claim == null) return 0; // Safety check
            return int.Parse(claim);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyDoctors()
        {
            var hid = GetHospitalIdFromClaim();

            var doctors = await _db.Doctors
                .Include(d => d.Hospital) // 👈 THIS LINE FIXES THE NULL ISSUE
                .Where(d => d.HospitalId == hid)
                .ToListAsync();

            return Ok(doctors);
        }

        // ... (Keep your HttpPost, HttpPut, and HttpDelete exactly as they are) ...
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            var hid = GetHospitalIdFromClaim();
            var doctor = new Doctor
            {
                HospitalId = hid,
                Name = dto.Name,
                Specialization = dto.Specialization,
                LicenceNumber = dto.LicenceNumber,
                PhoneNumber = dto.PhoneNumber
            };
            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();
            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorDto dto)
        {
            var hid = GetHospitalIdFromClaim();
            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id && d.HospitalId == hid);
            if (doctor == null) return NotFound();

            doctor.Name = dto.Name;
            doctor.Specialization = dto.Specialization;
            doctor.LicenceNumber = dto.LicenceNumber;
            doctor.PhoneNumber = dto.PhoneNumber;
            await _db.SaveChangesAsync();
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var hid = GetHospitalIdFromClaim();
            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id && d.HospitalId == hid);
            if (doctor == null) return NotFound();
            _db.Doctors.Remove(doctor);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}