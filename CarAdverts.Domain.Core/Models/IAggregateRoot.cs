namespace CarAdverts.Domain.Core.Models
{
    public interface IAggregateRoot<TKey> : IEntity<TKey> where TKey : struct
    {
    }
}
