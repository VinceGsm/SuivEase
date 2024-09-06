using DAL.Models;

namespace SuivEase.Services
{
    public interface ISuiviService : IService
    {
        Task<IEnumerable<Suivi>> GetAll(string userId);

        Task<Suivi> GetById(int id);

        Task<bool> Update(Suivi suivi);

        Task<Suivi> Create(Suivi suivi);

        Task<bool> Delete(int id);
    }
}