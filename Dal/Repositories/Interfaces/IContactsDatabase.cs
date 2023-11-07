using System;
using Dal.Models;

namespace Dal.Repositories
{
	public interface IContactsDatabase
	{
        public Task<Contact> AddContactToDbAsync(Contact contact);
        public Task<Contact> UpdateContactInDbAsync(int id, Contact updatedContact);
        public Task RemoveContactFromDbAsync(int id);
        public Task<IEnumerable<Contact>> FetchContactsAsync(int? id = null,
                                                string? emailFilter = null,
                                                string? nameFilter = null,
                                                string? contragentFilter = null);
        public Task<Contragent> FindOrCreateContragentAsync(string name);
    }
}

