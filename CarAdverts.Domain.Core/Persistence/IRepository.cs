using CarAdverts.Domain.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Core.Persistence
{
    public interface IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : struct
    {
        Task<TAggregateRoot> GetByIdAsync(TKey key);
        Task<IQueryable<TAggregateRoot>> GetAsync(string sortOptions);
        Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot);
        Task UpdateAsync(TAggregateRoot aggregateRoot);
        Task DeleteAsync(TKey key);
    }
}
