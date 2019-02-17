using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarAdverts.Infrastructure.Repositories
{
    public abstract class Repository<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TKey : struct
    {
        private readonly DbContext _context;
        protected readonly DbSet<TAggregateRoot> DbSet;

        protected Repository(IContext<TAggregateRoot, TKey> context)
        {
            _context = (DbContext)context;
            DbSet = _context.Set<TAggregateRoot>();
        }

        public async Task<TAggregateRoot> Create(TAggregateRoot aggregateRoot)
        {
            var entityEntry = await DbSet.AddAsync(aggregateRoot);
            return entityEntry.Entity;
        }

        public Task Delete(TKey key)
        {
            DbSet.Remove(DbSet.Find(key));

            return Task.CompletedTask;
        }

        public Task<TAggregateRoot> GetByIdAsync(TKey key)
        {
            return DbSet.FindAsync(key);
        }

        public Task Update(TAggregateRoot aggregateRoot)
        {
            DbSet.Update(aggregateRoot);

            return Task.CompletedTask;
        }
    }
}
