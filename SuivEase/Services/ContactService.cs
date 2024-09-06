using DAL.Models;
using SuivEase.Repos;

namespace SuivEase.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepo;

        public ContactService(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo ?? throw new ArgumentNullException(nameof(contactRepo));
        }

        public async Task<Contact> Create(Contact contact)
        {
            //return await _contactRepo.Create(contact);
            throw new NotImplementedException();

        }

        public async Task<Contact> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetAll(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> Update(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
