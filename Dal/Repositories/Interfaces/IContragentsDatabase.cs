using System;
using Dal.Models;

namespace Dal.Repositories
{
	public interface IContragentsDatabase
	{
        public Task<Contragent> UpdateContragentInDbAsync(int id, Contragent updatedContragent);
        public Task RemoveContragentFromDbAsync(int id);
        public Task<IEnumerable<Contragent>> FetchContragentsAsync(int? id = null, string? nameFilter = null);
        public Task<Contragent> CreateContragentAsync(string name);
    }
}

