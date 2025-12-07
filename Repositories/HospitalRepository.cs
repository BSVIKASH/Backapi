using Backapi.Data;
using Backapi.Models;
using Backapi.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backapi.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly AppDbContext _db;
        public HospitalRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Hospital hospital)
        {
            await _db.Hospitals.AddAsync(hospital);
            await _db.SaveChangesAsync();
        }

        public async Task<Hospital> GetByEmailAsync(string email)
            => await _db.Hospitals.FirstOrDefaultAsync(h => h.Email == email);

        public async Task<Hospital> GetByIdAsync(int id)
            => await _db.Hospitals.FindAsync(id);
    }
}
