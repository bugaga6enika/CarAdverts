using CarAdverts.Domain.Core.Models;

namespace CarAdverts.Domain.Core.Persistence
{
    public interface IContext<TAggregateRoot, TKey> 
        where TAggregateRoot : IAggregateRoot<TKey> 
        where TKey : struct
    {
    }
}
