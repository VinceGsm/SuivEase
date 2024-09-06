using DAL.Models;

namespace Business.I.Services;


public interface IAddressService : IService
{
    Task<Address> GetById(int id);

    Task<bool> Update(Address address);    
    
    Task<Address> Create(Address address);
}
