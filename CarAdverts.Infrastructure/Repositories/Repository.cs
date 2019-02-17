﻿using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Domain.Core.Validation;
using CarAdverts.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
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

        public virtual Task<TAggregateRoot> GetByIdAsync(TKey key)
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
            var entityEntry = await DbSet.AddAsync(aggregateRoot);
            return entityEntry.Entity;
        }

        public virtual Task UpdateAsync(TAggregateRoot aggregateRoot)
        {
            DbSet.Update(aggregateRoot);

            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TKey key)
        {
            DbSet.Remove(DbSet.Find(key));

            return Task.CompletedTask;
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
                catch (System.Exception e)
                {
                    throw new ValidationException(e.Message, "OrderBy");
                }
            }

            return query;
        }
    }
}
