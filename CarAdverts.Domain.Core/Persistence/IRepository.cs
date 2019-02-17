using CarAdverts.Domain.Core.Models;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Core.Persistence
{
    public interface IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : struct
    {
        Task<TAggregateRoot> GetByIdAsync(TKey key);
        Task<TAggregateRoot> Create(TAggregateRoot aggregateRoot);
        Task Update(TAggregateRoot aggregateRoot);
        Task Delete(TKey key);
    }
}
