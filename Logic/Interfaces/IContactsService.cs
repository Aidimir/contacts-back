using Dal.Interfaces;
using Dal.Models;
namespace Logic.Interfaces
{
	public interface IContactsService
	{
        public Task<Contact> CreateContact(Contact contact, string contragentName);
        public Task DeleteContact(int id);
        public Task<Contact> UpdateContact(int id, IPublicContact updatedContact);
        public Task<IEnumerable<Contact>> FetchContacts(int? id = null,
            string? emailFilter = null,
            string? nameFilter = null,
            string? contragentFilter = null);
    }
}

