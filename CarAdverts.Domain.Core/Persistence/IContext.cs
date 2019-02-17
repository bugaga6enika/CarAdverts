using CarAdverts.Domain.Core.Models;

namespace CarAdverts.Domain.Core.Persistence
{
    /// <summary>
    /// Represents isolated context per AggregateRoot
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IContext<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : struct
    {
    }
}
