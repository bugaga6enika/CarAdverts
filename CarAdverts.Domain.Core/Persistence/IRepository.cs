using CarAdverts.Domain.Core.Models;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Core.Persistence
{
    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        Task<TEntity> GetByIdAsync(TKey key);
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TKey key);
    }
}
