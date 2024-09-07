using DAL.Models;

namespace DAL.Repos;

public interface ISuiviRepository
{        
    Task<IEnumerable<Suivi>> GetAll(Guid userGid);

    Task<Suivi> GetById(int suiviId);

    Task<Suivi> Create(Suivi suivi);

    Task Update(Suivi suivi);

    Task Delete(int suiviId);

    Task<bool> IsInDb(int suiviId);
}
