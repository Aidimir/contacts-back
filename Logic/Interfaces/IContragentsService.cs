using System;
using Dal.Interfaces;
using Dal.Models;

namespace Logic.Interfaces
{
    public interface IContragentsService
    {
        public Task<Contragent> CreateContragent(Contragent contragent);
        public Task DeleteContragent(int id);
        public Task<Contragent> UpdateContragent(int id, IPublicContragent updatedContragent);
        public Task<IEnumerable<Contragent>> FetchContragents(int? id = null, string? nameFilter = null);
    }
}

