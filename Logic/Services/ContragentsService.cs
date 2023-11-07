using System;
using Dal.Interfaces;
using Dal.Models;
using Dal.Repositories;
using Logic.Interfaces;

namespace Logic.Services
{
	public class ContragentsService : IContragentsService
	{
        private readonly IContragentsDatabase _database;

        public ContragentsService(IMainDatabase database)
        {
            _database = database;
        }

        public async Task<Contragent> UpdateContragent(int id, IPublicContragent updatedContragent)
        {
            var existingContragent = (await _database.FetchContragentsAsync(id: id)).First();

            existingContragent.Name = updatedContragent.Name;

            var updated = await _database.UpdateContragentInDbAsync(id, existingContragent);

            return updated;
        }

        public async Task<IEnumerable<Contragent>> FetchContragents(int? id, string? nameFilter)
        {
            var result = await _database.FetchContragentsAsync(id: id, nameFilter: nameFilter);

            return result;
        }

        public async Task<Contragent> CreateContragent(Contragent contragent)
        {
            var result = await _database.CreateContragentAsync(contragent.Name);

            return result;
        }

        public async Task DeleteContragent(int id)
        {
            await _database.RemoveContragentFromDbAsync(id);
        }
    }
}

