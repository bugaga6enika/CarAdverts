namespace CarAdverts.Domain.Core.Models
{
    public interface IEntity<TKey> where TKey : struct
    {
        TKey Id { get; }
    }
}