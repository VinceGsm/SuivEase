using DAL.Models;

namespace Business.Services;


public interface IContactService : IService 
{
    Task<IEnumerable<Contact>> GetAll(string userId);

    Task<Contact> GetById(int id);

    Task<Contact> Create(Contact contact);

    Task<Contact> Update(Contact contact);

    Task<Contact> Delete(int id);
}
