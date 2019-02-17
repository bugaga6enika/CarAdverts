using CarAdverts.Domain.Core.Models;
using System;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Core.Persistence
{
    /// <summary>
    /// Apply UoW per context
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TAggregateRoot"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IUnitOfWork<TContext, TAggregateRoot, TKey> : IDisposable
        where TContext : IContext<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
        where TKey : struct
    {
        Task CommitAsync();
    }
}