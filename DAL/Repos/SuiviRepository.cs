using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL.I.Repos;

namespace DAL.Repos;


public class SuiviRepository : ISuiviRepository
{
    private readonly ApplicationDbContext _context;

    public SuiviRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<IEnumerable<Suivi>> GetAll(Guid userId)
    {
        return await _context.Suivis.Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Suivi> GetById(int id)
    {
        return await _context.Suivis.Where(x => x.SuiviId == id)
            .FirstAsync();
    }

    public async Task Update(Suivi suivi)
    {
        // TO DO : See if relevant = transaction
        //  using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Entry(suivi).State = EntityState.Modified;
            suivi.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // await transaction.RollbackAsync();
            // TO DO : Log
            throw;
        }
    }

    public async Task<Suivi> Create(Suivi suivi)
    {
        var res = _context.Suivis.Add(suivi);

        try
        {
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        catch (DbUpdateConcurrencyException)
        {
            // TO DO : Log
            throw;
        }
    }

    public async Task Delete(int id)
    {        
        var suivi = await _context.Suivis.SingleAsync(x => x.SuiviId == id);
        var address = await _context.Addresses.SingleAsync(y => y.AddressId == suivi.AddressId);        

        try
        {
            _context.Suivis.Remove(suivi);
            _context.Addresses.Remove(address); // look out : cascading
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // TO DO : Log
            throw;
        }
    }

    public async Task<bool> IsInDb(int id)
    {
        return await _context.Suivis.AnyAsync(x => x.SuiviId == id);
    }
}
