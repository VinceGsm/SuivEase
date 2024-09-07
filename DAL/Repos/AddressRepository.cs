using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos;


public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Address> Create(Address address)
    {
        var res = _context.Addresses.Add(address);

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

    public async Task<Address> GetById(int id)
    {
        return await _context.Addresses.Where(x => x.AddressId == id)
            .FirstAsync();
    }

    public async Task<bool> IsInDb(int id)
    {
        return await _context.Addresses.AnyAsync(x => x.AddressId == id);
    }

    public async Task Update(Address address)
    {
        try
        {
            _context.Entry(address).State = EntityState.Modified;                

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {                
            // TO DO : Log
            throw;
        }
    }
}
