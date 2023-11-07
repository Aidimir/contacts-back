using Dal.Exceptions;
using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class MainDatabase : DbContext, IMainDatabase
    {
        private DbSet<Contact> _contacts { get; set; }

        private DbSet<Contragent> _contragents { get; set; }

        public MainDatabase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(c => c.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contragent>()
                .Property(c => c.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contact>()
                .Property(c => c.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contragent>()
                .Property(c => c.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
        }

        public async Task<Contact> AddContactToDbAsync(Contact contact)
        {
            var sameContactsInDb = await _contacts.FirstOrDefaultAsync(g => g.Email == contact.Email);

            if (sameContactsInDb != null)
            {
                throw new ObjectAlreadyExistsException("Contact with this email address is already in database");
            }

            contact.Contragent.Contacts.Add(contact);

            await _contacts.AddAsync(contact);
            await SaveChangesAsync();

            return await FetchContactById(contact.Id);
        }

        private async Task<Contact> FetchContactById(int id)
        {
            var result = await _contacts.Include(x => x.Contragent).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new NotFoundException("Couldn't find any contact with this id");
            }

            return result;
        }

        public async Task<IEnumerable<Contact>> FetchContactsAsync(int? id,
            string? emailFilter,
            string? nameFilter,
            string? contragentFilter)
        {
            if (_contacts.Count() == 0)
            {
                return new List<Contact>();
            }

            IQueryable<Contact> result = _contacts.Include(c => c.Contragent);

            if (id is not null && id != 0)
            {
                var possibleResult = result.Where(c => c.Id == id).FirstOrDefault();
                if (possibleResult is not null)
                {
                    return new List<Contact> { possibleResult };
                }
            }

            if (!string.IsNullOrEmpty(emailFilter))
            {
                result = result.Where(c => c.Email.Contains(emailFilter));
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(c => c.Fullname.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(contragentFilter))
            {
                result = result.Where(c => c.Contragent.Name.Contains(contragentFilter));
            }

            return await result.ToListAsync();
        }

        public async Task<Contragent> FindOrCreateContragentAsync(string name)
        {
            var contragentInDb = await _contragents.FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
            if (contragentInDb == null)
            {
                var newContragent = new Contragent { Name = name };
                await _contragents.AddAsync(newContragent);
                await SaveChangesAsync();
                return newContragent;
            }
            else
            {
                return contragentInDb;
            }
        }

        public async Task RemoveContactFromDbAsync(int id)
        {
            var neededContact = await FetchContactById(id);
            _contacts.Remove(neededContact);
            await SaveChangesAsync();
        }

        public async Task RemoveContragentFromDbAsync(int id)
        {
            var neededContragent = (await FetchContragentsAsync(id: id)).First();
            _contragents.Remove(neededContragent);
            await SaveChangesAsync();
        }

        public async Task<Contact> UpdateContactInDbAsync(int id, Contact updatedContact)
        {
            updatedContact.UpdatedAt = DateTime.Now;
            _contacts.Update(updatedContact);
            await SaveChangesAsync();
            var existingContact = await FetchContactById(id);

            return existingContact;
        }

        public async Task<Contragent> UpdateContragentInDbAsync(int id, Contragent updatedContragent)
        {
            updatedContragent.UpdatedAt = DateTime.Now;
            _contragents.Update(updatedContragent);
            await SaveChangesAsync();
            var existingContragent = (await FetchContragentsAsync(id: id)).First();

            return existingContragent;
        }

        public async Task<IEnumerable<Contragent>> FetchContragentsAsync(int? id = null, string? nameFilter = null)
        {
            if (_contragents.Count() == 0)
            {
                return new List<Contragent>();
            }

            IQueryable<Contragent> result = _contragents.Include(c => c.Contacts);

            if (id is not null && id != 0)
            {
                var possibleResult = result.Where(c => c.Id == id).FirstOrDefault();
                if (possibleResult is not null)
                {
                    return new List<Contragent> { possibleResult };
                }
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(c => c.Name.Contains(nameFilter));
            }
            return await result.ToListAsync();
        }

        public async Task<Contragent> CreateContragentAsync(string name)
        {
            var contragentInDb = await _contragents.FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());

            if (contragentInDb != null)
            {
                throw new ObjectAlreadyExistsException("Contragent is already in database");
            }

            var newContragent = new Contragent { Name = name };
            await _contragents.AddAsync(newContragent);
            await SaveChangesAsync();

            return newContragent;
        }
    }
}

