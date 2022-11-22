using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Domain.Core.Validation;
using CarAdverts.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public virtual ValueTask<TAggregateRoot> GetByIdAsync(TKey key)
        {
            return DbSet.FindAsync(key);
        }

        public virtual Task<IQueryable<TAggregateRoot>> GetAsync(string sortOptions)
        {
            var query = DbSet.AsQueryable();

            query = OrderBy(query, sortOptions);

            return Task.FromResult(query);
        }

        public virtual async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot)
        {
            var entityEntry = await DbSet.AddAsync(aggregateRoot).ConfigureAwait(false);
            return entityEntry.Entity;
        }

        public virtual Task UpdateAsync(TAggregateRoot aggregateRoot)
        {
            DbSet.Update(aggregateRoot);

            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(TKey key)
        {
            var entity = await DbSet.FindAsync(key).ConfigureAwait(false);

            if (entity == default(TAggregateRoot))
            {
                throw new NoEntryException<TKey>("No entry found for given key", key);
            }

            DbSet.Remove(entity);

            // ToDo: raise delete event
        }

        protected virtual IQueryable<TAggregateRoot> OrderBy(IQueryable<TAggregateRoot> query, string sortOptions)
        {
            var sortByOpions = new SortOptions(sortOptions);

            if (sortByOpions.IsValid)
            {
                try
                {
                    query = query.OrderBy(sortByOpions.ToString());
                }
                catch (Exception e)
                {
                    throw new ValidationException(e.Message, "OrderBy");
                }
            }

            return query;
        }
    }
}
