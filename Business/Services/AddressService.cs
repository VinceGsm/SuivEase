using DAL.Models;
using DAL.Repos;

namespace Business.Services;


public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepo;

    public AddressService(IAddressRepository addressRepo)
    {
        _addressRepo = addressRepo ?? throw new ArgumentNullException(nameof(addressRepo));
    }


    public async Task<Address> GetById(int id)
    {
        return await _addressRepo.GetById(id);
    }

    public async Task<bool> Update(Address address)
    {
        await _addressRepo.Update(address);

        return true;
    }


    public async Task<bool> Exists(int id)
    {
        return await _addressRepo.IsInDb(id);
    }

    public async Task<Address> Create(Address address)
    {
        var res = await _addressRepo.Create(address);

        return res;
    }
}
