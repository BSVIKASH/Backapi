using Backapi.Models;

using System.Threading.Tasks;

namespace Backapi.Repositories
{
    public interface IHospitalRepository
    {
        Task<Hospital> GetByEmailAsync(string email);
        Task AddAsync(Hospital hospital);
        Task<Hospital> GetByIdAsync(int id);
    }
}
