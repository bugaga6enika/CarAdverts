using CarAdverts.Domain.Core.Models;

namespace CarAdverts.Domain.Core.Persistence
{
    public interface IUnitOfWork<TContext, TAggregateRoot, TKey>
        where TContext : IContext<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : struct
    {
    }
}