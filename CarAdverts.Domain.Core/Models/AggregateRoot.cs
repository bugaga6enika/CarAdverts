namespace CarAdverts.Domain.Core.Models
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey> where TKey : struct
    {
    }
}
