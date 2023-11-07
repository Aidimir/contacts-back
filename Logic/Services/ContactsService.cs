using Dal.Interfaces;
using Dal.Models;
using Dal.Repositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsDatabase _database;

        public ContactsService(IMainDatabase database)
        {
            _database = database;
        }

        public async Task<Contact> CreateContact(Contact contact, string contragentName)
        {
            contact.Contragent = await _database.FindOrCreateContragentAsync(contragentName);
            var result = await _database.AddContactToDbAsync(contact);

            return result;
        }

        public async Task DeleteContact(int id)
        {
            await _database.RemoveContactFromDbAsync(id);
        }

        public async Task<IEnumerable<Contact>> FetchContacts(int? id,
            string? emailFilter,
            string? nameFilter,
            string? contragentFilter)
        {
            var result = await _database.FetchContactsAsync(id: id,
                emailFilter: emailFilter,
                nameFilter: nameFilter,
                contragentFilter: contragentFilter);

            return result;
        }

        public async Task<Contact> UpdateContact(int id, IPublicContact updatedContact)
        {
            var existingContact = (await _database.FetchContactsAsync(id: id)).First();
            var existingContragent = await _database.FindOrCreateContragentAsync(updatedContact.Contragent);

            existingContact.Fullname = updatedContact.Fullname;
            existingContact.Email = updatedContact.Email;
            existingContact.Contragent = existingContragent;

            var updated = await _database.UpdateContactInDbAsync(id, existingContact);

            return updated;
        }
    }
}

