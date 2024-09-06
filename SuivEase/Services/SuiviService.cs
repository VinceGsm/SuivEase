using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using SuivEase.Repos;

namespace SuivEase.Services;

/// <summary>
/// Business Logic
/// </summary>
public class SuiviService : ISuiviService
{
    private readonly ISuiviRepository _suiviRepo;

    public SuiviService(ISuiviRepository suiviRepo)
    {
        _suiviRepo = suiviRepo ?? throw new ArgumentNullException(nameof(suiviRepo));
    }

    public async Task<IEnumerable<Suivi>> GetAll(string userId)
    {            
        var userGid = Guid.Parse(userId);

        return await _suiviRepo.GetAll(userGid);            
    }

    public async Task<Suivi> GetById(int id)
    {        
        return await _suiviRepo.GetById(id);
    }

    public async Task<bool> Update(Suivi suivi)
    {
        await _suiviRepo.Update(suivi);

        return true;
    }

    public async Task<Suivi> Create(Suivi suivi)
    {
        var res = await _suiviRepo.Create(suivi);

        return res;
    }

    public async Task<bool> Delete(int suiviId)
    {        
        await _suiviRepo.Delete(suiviId);

        return true;
    }


    // Tools
    public async Task<bool> Exists(int id)
    {
        return await _suiviRepo.IsInDb(id);
    }
}

/* example double dbcontext
 
 public interface IDataService
{
    List<Employee> GetEmployees(string gender);
}
public class DataService : IDataService
{
    private readonly SecondDbContext _context;
    public DataService(SecondDbContext secondDbContext)
    {
        _context = secondDbContext;
    }
    public List<Employee> GetEmployees(string gender)
    {
        var result  = new List<Employee>();
        if(string.IsNullOrEmpty(gender))
        {
            result= _context.Employees.ToList();
        }
        else
        {
          result  = _context.Employees.Where(c => c.Gender == gender).ToList();
        }

        return result;

    }
} 
 */
